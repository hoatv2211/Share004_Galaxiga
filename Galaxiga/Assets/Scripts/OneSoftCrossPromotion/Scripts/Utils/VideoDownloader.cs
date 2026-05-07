using System;
using System.Collections;
using System.Collections.Generic;
using OneSoftCrossPromotion.Scripts.Controller;
using OneSoftCrossPromotion.Scripts.Structs;
using UnityEngine;
using UnityEngine.Networking;

namespace OneSoftCrossPromotion.Scripts.Utils
{
	public class VideoDownloader
	{
		public IEnumerator DownloadVideo(GamePromoInfo gamePromoInfo, List<GamePromoInfo> newPromos, Action<GamePromoInfo[]> downloadSuccess, Action<HashSet<GamePromoInfo>> downloadError, Action<GamePromoInfo> onNetworkError, Action<GamePromoInfo> onHttpError)
		{
			UnityWebRequest webRequest = UnityWebRequest.Get(gamePromoInfo.url);
			UnityEngine.Debug.Log("Video Start Downloading");
			webRequest.timeout = 10;
			yield return webRequest.SendWebRequest();
			if (webRequest.isNetworkError || webRequest.isHttpError)
			{
				if (webRequest.isHttpError)
				{
					onHttpError(gamePromoInfo);
				}
				if (webRequest.isNetworkError)
				{
					onNetworkError(gamePromoInfo);
				}
				UnityEngine.Debug.Log("Request Error: " + webRequest);
				this._videoDownloadError.Add(gamePromoInfo);
			}
			else
			{
				UnityEngine.Debug.Log("Download Video Success");
				byte[] data = webRequest.downloadHandler.data;
				string text = string.Format("{0}.mp4", gamePromoInfo.name);
				string text2 = CacheController.CachePath + text;
				if (CacheController.IsFileExist(text))
				{
					UnityEngine.Debug.Log("Video is downloaded: " + text2);
					yield break;
				}
				CacheController.WriteFile(text, data);
				gamePromoInfo.cachePath = text2;
				this._videoDownloaded.Add(gamePromoInfo);
				List<GamePromoInfo> cachePromoInfos = CacheController.GetCachePromoInfos();
				cachePromoInfos.Add(gamePromoInfo);
				CacheController.WriteJson("promo.json", JsonHelper.ToJson<GamePromoInfo>(cachePromoInfos.ToArray(), true));
				UnityEngine.Debug.Log("downloaded: " + this._videoDownloaded.Count);
				UnityEngine.Debug.Log("download error: " + this._videoDownloadError.Count);
				if (this._videoDownloaded.Count + this._videoDownloadError.Count == newPromos.Count)
				{
					downloadSuccess(this._videoDownloaded.ToArray());
				}
			}
			if (this._videoDownloadError.Count == newPromos.Count)
			{
				downloadError(this._videoDownloadError);
			}
			yield break;
		}

		private readonly List<GamePromoInfo> _videoDownloaded = new List<GamePromoInfo>();

		private readonly HashSet<GamePromoInfo> _videoDownloadError = new HashSet<GamePromoInfo>();
	}
}
