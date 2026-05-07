using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FalconSDK.CrossPromotion.cs_sc.item;
using FalconSDK.CrossPromotion.Scripts.Controller;
using FalconSDK.CrossPromotion.Scripts.Enum;
using UnityEngine;
using UnityEngine.Networking;

namespace FalconSDK.CrossPromotion.Scripts.Utils
{
	public class VideoDownloader
	{
		public IEnumerator DownloadVideo(PromoType promoType, CrossGameItem crossGameItem, List<CrossGameItem> newPromos, Action<CrossGameItem[]> downloadSuccess, Action<HashSet<CrossGameItem>> downloadError, Action<CrossGameItem> onNetworkError, Action<CrossGameItem> onHttpError)
		{
			string url = (promoType != PromoType.Fixed) ? crossGameItem.videoFloatUrl : crossGameItem.videoFixedUrl;
			UnityWebRequest webRequest = UnityWebRequest.Get(url);
			UnityEngine.Debug.Log("Video Start Downloading: " + url);
			yield return webRequest.SendWebRequest();
			if (webRequest.isNetworkError || webRequest.isHttpError)
			{
				if (webRequest.isHttpError)
				{
					onHttpError(crossGameItem);
				}
				if (webRequest.isNetworkError)
				{
					onNetworkError(crossGameItem);
				}
				this._videoDownloadError.Add(crossGameItem);
			}
			else
			{
				byte[] data = webRequest.downloadHandler.data;
				UnityEngine.Debug.Log("Download done: " + crossGameItem.name);
				string text = string.Format("{0}.mp4", crossGameItem.name);
				string text2 = string.Format("{0}{1}/{2}", CacheController.CachePath, promoType, text);
				CacheController.WriteFile(string.Format("{0}/{1}", promoType, text), data);
				if (promoType == PromoType.Fixed)
				{
					crossGameItem.videoFixedCache = text2;
				}
				else if (promoType == PromoType.Float)
				{
					crossGameItem.videoFloatCache = text2;
				}
				List<CrossGameItem> cachePromoInfos = CacheController.GetCachePromoInfos();
				bool flag = true;
				foreach (CrossGameItem crossGameItem2 in cachePromoInfos)
				{
					if (crossGameItem2.name == crossGameItem.name && crossGameItem2.version == crossGameItem.version)
					{
						crossGameItem2.videoFixedCache = crossGameItem.videoFixedCache;
						crossGameItem2.videoFloatCache = crossGameItem.videoFloatCache;
						flag = false;
					}
				}
				if (flag)
				{
					cachePromoInfos.Add(crossGameItem);
				}
				CacheController.WriteJson("promo.json", JsonHelper.ToJson<CrossGameItem>(cachePromoInfos.ToArray(), true));
				this._videoDownloaded.Add(crossGameItem);
				if (this._videoDownloaded.Count + this._videoDownloadError.Count == newPromos.Count)
				{
					bool flag2 = true;
					foreach (CrossGameItem crossGameItem3 in this._videoDownloaded)
					{
						if (!string.IsNullOrEmpty(crossGameItem3.videoFixedUrl) && string.IsNullOrEmpty(crossGameItem3.videoFixedCache))
						{
							flag2 = false;
						}
						if (!string.IsNullOrEmpty(crossGameItem3.videoFloatUrl) && string.IsNullOrEmpty(crossGameItem3.videoFloatCache))
						{
							flag2 = false;
						}
					}
					if (flag2)
					{
						downloadSuccess(this._videoDownloaded.ToArray<CrossGameItem>());
					}
				}
			}
			if (this._videoDownloadError.Count == newPromos.Count)
			{
				downloadError(this._videoDownloadError);
			}
			yield break;
		}

		private readonly HashSet<CrossGameItem> _videoDownloaded = new HashSet<CrossGameItem>();

		private readonly HashSet<CrossGameItem> _videoDownloadError = new HashSet<CrossGameItem>();
	}
}
