using System;
using System.Collections.Generic;
using System.Linq;
using OneSoftCrossPromotion.Scripts.Structs;
using OneSoftCrossPromotion.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace OneSoftCrossPromotion.Scripts.Controller
{
	public class VideoController : MonoBehaviour
	{
		private void Start()
		{
			this._childPromo = base.transform.GetChild(0);
			this.rawImage.color = Color.white;
			this.videoPlayer = base.gameObject.GetComponent<VideoPlayer>();
			this.videoPlayer.playOnAwake = true;
			this.videoPlayer.isLooping = true;
			this.videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
			this.videoPlayer.prepareCompleted += this.OnPrepareComplete;
			this.ChoseVideo(null);
		}

		public void ChoseVideo(GamePromoInfo? newPromoInfo)
		{
			this.promoInfo = ((newPromoInfo == null) ? this.GetRandomPromo() : newPromoInfo.Value);
			this.PrepareVideo();
		}

		private void PrepareVideo()
		{
			this.videoPlayer.url = this.promoInfo.cachePath;
			this.videoPlayer.Prepare();
		}

		private void OnPrepareComplete(VideoPlayer source)
		{
			this._childPromo.gameObject.SetActive(true);
			if (this.gameName != null)
			{
				this.gameName.GetComponent<Text>().text = this.promoInfo.name;
			}
			this.rawImage.texture = this.videoPlayer.texture;
			this.videoPlayer.Play();
		}

		public void StopVideo()
		{
			if (!this.videoPlayer.isPlaying)
			{
				return;
			}
			this.videoPlayer.Stop();
		}

		private GamePromoInfo GetRandomPromo()
		{
			string text = CacheController.ReadJson("promo.json");
			List<GamePromoInfo> list = (text != null) ? JsonHelper.FromJson<GamePromoInfo>(text).ToList<GamePromoInfo>() : new List<GamePromoInfo>();
			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		[HideInInspector]
		public RawImage rawImage;

		[HideInInspector]
		public VideoPlayer videoPlayer;

		[HideInInspector]
		public GamePromoInfo promoInfo;

		[HideInInspector]
		public GameObject gameName;

		private VideoSource _videoSource;

		private Transform _childPromo;
	}
}
