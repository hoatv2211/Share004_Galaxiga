using System;
using System.Collections;
using System.Collections.Generic;
using OneSoftCrossPromotion.Scripts.MenuItem;
using OneSoftCrossPromotion.Scripts.Singleton;
using OneSoftCrossPromotion.Scripts.Structs;
using OneSoftCrossPromotion.Scripts.Utils;
using UnityEngine;

namespace OneSoftCrossPromotion.Scripts.Controller
{
	public class PromoController : Singleton<PromoController>
	{
		protected override void Awake()
		{
			base.Awake();
			if (this.userPremium)
			{
				base.gameObject.SetActive(false);
			}
			this.CheckToDownloadVideo();
		}

		private void ActiveChilds(bool active)
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					transform.gameObject.SetActive(active);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		private void CheckToDownloadVideo()
		{
			if (this.isDownloading)
			{
				return;
			}
			this.ActiveChilds(false);
			List<GamePromoInfo> list = CacheController.CompareToGetNewPromo();
			UnityEngine.Debug.Log("New Promo Count: " + list.Count);
			if (list.Count == 0)
			{
				UnityEngine.Debug.Log("Have not new video");
				this.readyToShow = true;
				this.ActiveChilds(true);
			}
			else
			{
				this.StartDownloadVideos(list);
			}
		}

		private void StartDownloadVideos(List<GamePromoInfo> newPromos)
		{
			this.isDownloading = true;
			VideoDownloader videoDownloader = new VideoDownloader();
			foreach (GamePromoInfo gamePromoInfo in newPromos)
			{
				base.StartCoroutine(videoDownloader.DownloadVideo(gamePromoInfo, newPromos, new Action<GamePromoInfo[]>(this.OnDownloadAllVideoSuccess), new Action<HashSet<GamePromoInfo>>(this.OnDownloadAllVideoError), new Action<GamePromoInfo>(this.OnNetworkError), new Action<GamePromoInfo>(this.OnHttpError)));
			}
		}

		private void OnNetworkError(GamePromoInfo gamePromoInfo)
		{
			this.isDownloading = false;
			List<GamePromoInfo> cachePromoInfos = CacheController.GetCachePromoInfos();
			if (cachePromoInfos.Count == 0)
			{
				this.readyToShow = false;
			}
			else
			{
				this.readyToShow = true;
				this.ActiveChilds(true);
			}
		}

		private void OnHttpError(GamePromoInfo gamePromoInfo)
		{
			UnityEngine.Debug.Log("Http Error, please check url and try again");
			this.isDownloading = false;
		}

		private void OnDownloadAllVideoSuccess(GamePromoInfo[] videoDownloaded)
		{
			UnityEngine.Debug.Log("Downloaded All Video Success");
			this.isDownloading = false;
			this.readyToShow = true;
			this.ActiveChilds(true);
		}

		private void OnDownloadAllVideoError(HashSet<GamePromoInfo> videoDownloadError)
		{
			UnityEngine.Debug.Log("Downloaded Video Error");
			this.isDownloading = false;
		}

		public void ShowFixedPromo()
		{
			if (this._tryToShowFixed)
			{
				return;
			}
			base.StartCoroutine(this.TryToShowFixedPromo());
		}

		private IEnumerator TryToShowFixedPromo()
		{
			this._tryToShowFixed = true;
			this.CheckToDownloadVideo();
			GameObject fixedPromo = FixedPromoCreator.Initiate(base.transform);
			UnityEngine.Debug.Log("Downloading: " + this.isDownloading);
			while (this.isDownloading)
			{
				yield return null;
			}
			this._tryToShowFixed = false;
			if (this.noInternet)
			{
				yield break;
			}
			if (!this.readyToShow)
			{
				yield break;
			}
			fixedPromo.SetActive(true);
			yield break;
		}

		public void HideAllChild()
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					transform.gameObject.SetActive(false);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		public void HideChildAtIndex(int index)
		{
			if (index > base.transform.childCount - 1)
			{
				throw new Exception("Index out of bound");
			}
			base.transform.GetChild(index).gameObject.SetActive(false);
		}

		public void ShowAllChild()
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					transform.gameObject.SetActive(true);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		public void ShowChildAtIndex(int index)
		{
			if (index > base.transform.childCount - 1)
			{
				throw new Exception("Index out of bound");
			}
			base.transform.GetChild(index).gameObject.SetActive(true);
		}

		public void ChangePromoAtIndex(int index, GamePromoInfo? promoInfo)
		{
			if (index > base.transform.childCount - 1)
			{
				throw new Exception("Index out of bound");
			}
			VideoController component = base.transform.GetChild(index).GetComponent<VideoController>();
			component.ChoseVideo(promoInfo);
		}

		public void DestroyAllChild()
		{
			while (base.transform.childCount > 0)
			{
				UnityEngine.Object.Destroy(base.transform.GetChild(0));
			}
		}

		public void DestroyChildAtIndex(int index)
		{
			if (index > base.transform.childCount - 1)
			{
				throw new Exception("Index out of bound");
			}
			UnityEngine.Object.Destroy(base.transform.GetChild(index));
		}

		[SerializeField]
		private bool userPremium;

		[HideInInspector]
		public bool isDownloading;

		[HideInInspector]
		public bool noInternet;

		[HideInInspector]
		public bool readyToShow;

		private bool _tryToShowFixed;
	}
}
