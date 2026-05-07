using System;
using System.Collections.Generic;
using System.Linq;
using FalconSDK.CrossPromotion.cs_sc.item;
using FalconSDK.CrossPromotion.Scripts.Enum;
using FalconSDK.CrossPromotion.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace FalconSDK.CrossPromotion.Scripts.Controller
{
	public class VideoController : MonoBehaviour
	{
		private void Start()
		{
			this._childPromo = base.transform.GetChild(0);
			this.rawImage.color = Color.white;
			this.EnableBackground(false);
			this._childPromo.gameObject.SetActive(false);
			this.videoPlayer = base.GetComponent<VideoPlayer>();
			this.videoPlayer.waitForFirstFrame = true;
			this.videoPlayer.playOnAwake = false;
			this.videoPlayer.isLooping = true;
			this.videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
			this.videoPlayer.prepareCompleted += this.PrepareComplete;
			this.ChoseVideo(null);
		}

		private void EnableBackground(bool show)
		{
			Image component = base.GetComponent<Image>();
			if (component != null)
			{
				component.enabled = show;
			}
		}

		public void ChoseVideo(CrossGameItem newPromoInfo)
		{
			this.promoInfo = (newPromoInfo ?? this.GetRandomPromo());
			this.PrepareVideo();
		}

		private void PrepareVideo()
		{
			this.videoPlayer.url = ((this.promoType != PromoType.Float) ? this.promoInfo.videoFixedUrl : this.promoInfo.videoFloatUrl);
			this.videoPlayer.Prepare();
		}

		private void PrepareComplete(VideoPlayer source)
		{
			this.EnableBackground(true);
			if (this.gameName != null)
			{
				this.gameName.GetComponent<Text>().text = this.promoInfo.name;
			}
			Texture texture = this.videoPlayer.texture;
			if (this.promoType == PromoType.Fixed)
			{
				float num = (float)texture.width;
				float num2 = (float)texture.height;
				float num3 = num / num2;
				float num4 = 880f;
				base.transform.localScale = new Vector3(1f, 1f, 1f);
				this._childPromo.GetComponent<RectTransform>().sizeDelta = new Vector2(num4, num4 / num3);
				this.rawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(num4 - 12f, num4 / num3 - 12f);
			}
			this.rawImage.texture = texture;
			this.videoPlayer.Play();
			this._childPromo.gameObject.SetActive(true);
		}

		public void StopVideo()
		{
			if (!this.videoPlayer.isPlaying)
			{
				return;
			}
			this.videoPlayer.Stop();
		}

		private CrossGameItem GetRandomPromo()
		{
			string text = CacheController.ReadJson("promo.json");
			List<CrossGameItem> list = (text != null) ? JsonHelper.FromJson<CrossGameItem>(text).ToList<CrossGameItem>() : new List<CrossGameItem>();
			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		[HideInInspector]
		public RawImage rawImage;

		[HideInInspector]
		public VideoPlayer videoPlayer;

		[HideInInspector]
		public CrossGameItem promoInfo;

		[HideInInspector]
		public GameObject gameName;

		[HideInInspector]
		public PromoType promoType;

		private VideoSource _videoSource;

		private Transform _childPromo;
	}
}
