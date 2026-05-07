using System;
using OneSoftCrossPromotion.Scripts.Controller;
using OneSoftCrossPromotion.Scripts.MenuItem;
using OneSoftCrossPromotion.Scripts.Structs;
using UnityEngine;

namespace OneSoftCrossPromotion.Scripts
{
	public static class OneSoftPromo
	{
		public static void ShowFixedPromo()
		{
			GameObject gameObject = PromoCanvasCreator.Initiate();
			PromoController component = gameObject.GetComponent<PromoController>();
			component.ShowFixedPromo();
		}

		public static void HideAllPromo()
		{
			GameObject gameObject = GameObject.FindWithTag("OneSoftPromoAds");
			PromoController component = gameObject.GetComponent<PromoController>();
			component.HideAllChild();
		}

		public static void HidePromoAtIndex(int index)
		{
			GameObject gameObject = GameObject.FindWithTag("OneSoftPromoAds");
			PromoController component = gameObject.GetComponent<PromoController>();
			component.HideChildAtIndex(index);
		}

		public static void ShowAllPromo()
		{
			GameObject gameObject = GameObject.FindWithTag("OneSoftPromoAds");
			PromoController component = gameObject.GetComponent<PromoController>();
			component.ShowAllChild();
		}

		public static void ShowPromoAtIndex(int index)
		{
			GameObject gameObject = GameObject.FindWithTag("OneSoftPromoAds");
			PromoController component = gameObject.GetComponent<PromoController>();
			component.ShowChildAtIndex(index);
		}

		public static void ChangePromoAtIndex(int index, GamePromoInfo? promoInfo)
		{
			GameObject gameObject = GameObject.FindWithTag("OneSoftPromoAds");
			PromoController component = gameObject.GetComponent<PromoController>();
			component.ChangePromoAtIndex(index, promoInfo);
		}

		public static void DestroyAllPromo()
		{
			GameObject gameObject = GameObject.FindWithTag("OneSoftPromoAds");
			PromoController component = gameObject.GetComponent<PromoController>();
		}

		public static void DestroyPromoAtIndex(int index)
		{
			GameObject gameObject = GameObject.FindWithTag("OneSoftPromoAds");
			PromoController component = gameObject.GetComponent<PromoController>();
		}
	}
}
