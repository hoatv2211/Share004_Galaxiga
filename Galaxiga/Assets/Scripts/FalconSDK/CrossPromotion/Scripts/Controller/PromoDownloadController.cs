using System;
using System.Collections.Generic;
using FalconSDK.CrossPromotion.cs_sc.item;
using FalconSDK.CrossPromotion.Scripts.Enum;
using FalconSDK.CrossPromotion.Scripts.Singleton;
using FalconSDK.CrossPromotion.Scripts.Structs;
using FalconSDK.CrossPromotion.Scripts.Utils;
using UnityEngine;

namespace FalconSDK.CrossPromotion.Scripts.Controller
{
	public class PromoDownloadController : PersistentSingleton<PromoDownloadController>
	{
		protected override void Awake()
		{
			base.Awake();
			this.CheckToDownloadVideo();
		}

		private void CheckToDownloadVideo()
		{
			if (CrossPromoFlag.IsDownloading)
			{
				return;
			}
			CrossPromoFlag.NoInternet = false;
			if (FSDK.CrossPromotion.CrossGameItems == null || FSDK.CrossPromotion.CrossGameItems.Count == 0)
			{
				UnityEngine.Debug.LogWarning("Received data failed");
				CrossPromoFlag.NoInternet = true;
				return;
			}
			List<CrossGameItem> list = CacheController.CompareToGetNewPromo();
			if (list.Count == 0)
			{
				UnityEngine.Debug.Log("Have not new video");
				CrossPromoFlag.ReadyToShowPromo = true;
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				this.StartDownloadVideos(list);
			}
		}

		private void StartDownloadVideos(List<CrossGameItem> newPromos)
		{
			CrossPromoFlag.IsDownloading = true;
			VideoDownloader videoDownloader = new VideoDownloader();
			foreach (CrossGameItem crossGameItem in newPromos)
			{
				if (!string.IsNullOrEmpty(crossGameItem.videoFixedUrl) && string.IsNullOrEmpty(crossGameItem.videoFixedCache))
				{
					base.StartCoroutine(videoDownloader.DownloadVideo(PromoType.Fixed, crossGameItem, newPromos, new Action<CrossGameItem[]>(this.DownloadAllVideoSuccess), new Action<HashSet<CrossGameItem>>(this.DownloadAllVideoError), new Action<CrossGameItem>(this.NetworkError), new Action<CrossGameItem>(this.HttpError)));
				}
				if (!string.IsNullOrEmpty(crossGameItem.videoFloatUrl) && string.IsNullOrEmpty(crossGameItem.videoFloatCache))
				{
					base.StartCoroutine(videoDownloader.DownloadVideo(PromoType.Float, crossGameItem, newPromos, new Action<CrossGameItem[]>(this.DownloadAllVideoSuccess), new Action<HashSet<CrossGameItem>>(this.DownloadAllVideoError), new Action<CrossGameItem>(this.NetworkError), new Action<CrossGameItem>(this.HttpError)));
				}
			}
		}

		private void NetworkError(CrossGameItem crossGameItem)
		{
			UnityEngine.Debug.LogWarning("Network Error: " + crossGameItem.name);
			CrossPromoFlag.IsDownloading = false;
			List<CrossGameItem> cachePromoInfos = CacheController.GetCachePromoInfos();
			CrossPromoFlag.ReadyToShowPromo = (cachePromoInfos.Count != 0);
			UnityEngine.Object.Destroy(base.gameObject);
		}

		private void HttpError(CrossGameItem crossGameItem)
		{
			UnityEngine.Debug.LogWarning("Http Error, please check url and try again: " + crossGameItem.name);
			CrossPromoFlag.IsDownloading = false;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		private void DownloadAllVideoSuccess(CrossGameItem[] videoDownloaded)
		{
			UnityEngine.Debug.LogWarning("Downloaded All Video Success");
			CrossPromoFlag.IsDownloading = false;
			CrossPromoFlag.ReadyToShowPromo = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		private void DownloadAllVideoError(HashSet<CrossGameItem> videoDownloadError)
		{
			UnityEngine.Debug.Log("Downloaded Video Error");
			CrossPromoFlag.IsDownloading = false;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		private void OnDestroy()
		{
			CrossPromoFlag.IsDownloading = false;
		}
	}
}
