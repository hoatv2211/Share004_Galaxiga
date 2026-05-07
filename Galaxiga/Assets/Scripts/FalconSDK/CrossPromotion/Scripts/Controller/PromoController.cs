using System;
using System.Collections;
using System.Collections.Generic;
using FalconSDK.CrossPromotion.cs_sc.item;
using FalconSDK.CrossPromotion.Scripts.Enum;
using FalconSDK.CrossPromotion.Scripts.MenuItem;
using FalconSDK.CrossPromotion.Scripts.Singleton;
using FalconSDK.CrossPromotion.Scripts.Structs;
using UnityEngine;

namespace FalconSDK.CrossPromotion.Scripts.Controller
{
	public class PromoController : Singleton<PromoController>
	{
		private void Start()
		{
			if (this.userPremium || CrossPromoFlag.NoInternet)
			{
				base.gameObject.SetActive(false);
			}
			if (CrossPromoFlag.ShowFloatPromo)
			{
				this.ShowFloatPromo();
			}
		}

		private bool IsPromoTypeExisted(PromoType promoType)
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				if (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					return transform.GetComponent<VideoController>().promoType == promoType;
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
			return false;
		}

		public void ShowFixedPromo()
		{
			if (this._tryToShowFixed)
			{
				return;
			}
			if (!CrossPromoFlag.IsDownloading)
			{
				GameObject gameObject = new GameObject("[PromoDownloader]", new Type[]
				{
					typeof(PromoDownloadController)
				});
			}
			base.StartCoroutine(this.TryToShowFixedPromo());
		}

		private IEnumerator TryToShowFixedPromo()
		{
			this._tryToShowFixed = true;
			while (CrossPromoFlag.IsDownloading)
			{
				yield return null;
			}
			this._tryToShowFixed = false;
			if (CrossPromoFlag.NoInternet || !CrossPromoFlag.ReadyToShowPromo)
			{
				yield break;
			}
			this.DestroyChildByType(PromoType.Fixed);
			GameObject fixedPromo = FixedPromoCreator.Initiate(base.transform);
			fixedPromo.SetActive(true);
			yield break;
		}

		public void ShowFloatPromo()
		{
			if (!CrossPromoFlag.IsDownloading)
			{
				GameObject gameObject = new GameObject("[PromoDownloader]", new Type[]
				{
					typeof(PromoDownloadController)
				});
			}
			base.StartCoroutine(this.TryToShowFloatPromo());
		}

		private IEnumerator TryToShowFloatPromo()
		{
			while (CrossPromoFlag.IsDownloading)
			{
				yield return null;
			}
			bool isTypeExisted = this.IsPromoTypeExisted(PromoType.Float);
			if (CrossPromoFlag.NoInternet || !CrossPromoFlag.ReadyToShowPromo || !isTypeExisted)
			{
				yield break;
			}
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (transform.GetComponent<VideoController>().promoType == PromoType.Float)
					{
						transform.gameObject.SetActive(true);
					}
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
			yield break;
		}

		public void ShowAllChild(bool show)
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					transform.gameObject.SetActive(show);
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

		public void ShowChildByType(PromoType promoType, bool show)
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					VideoController component = transform.GetComponent<VideoController>();
					if (component.promoType == promoType)
					{
						transform.gameObject.SetActive(show);
					}
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

		public void ShowChildAtIndex(int index, bool show)
		{
			if (index > base.transform.childCount - 1)
			{
				UnityEngine.Debug.LogWarning("Index of promo out of bound");
				return;
			}
			base.transform.GetChild(index).gameObject.SetActive(show);
		}

		public void ChangePromoAtIndex(int index, CrossGameItem promoInfo)
		{
			if (index > base.transform.childCount - 1)
			{
				UnityEngine.Debug.LogWarning("Index of promo out of bound");
				return;
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

		public void DestroyChildByType(PromoType promoType)
		{
			List<Transform> list = new List<Transform>();
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (transform.GetComponent<VideoController>().promoType == promoType)
					{
						list.Add(transform);
					}
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
			list.ForEach(delegate(Transform t)
			{
				UnityEngine.Object.Destroy(t.gameObject);
			});
		}

		public void DestroyChildAtIndex(int index)
		{
			if (index > base.transform.childCount - 1)
			{
				UnityEngine.Debug.LogWarning("Index of promo out of bound");
				return;
			}
			UnityEngine.Object.Destroy(base.transform.GetChild(index));
		}

		[SerializeField]
		private bool userPremium;

		private bool _tryToShowFixed;
	}
}
