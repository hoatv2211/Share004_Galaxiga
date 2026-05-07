using System;
using System.Collections.Generic;
using FalconSDK.CrossPromotion.cs_sc.item;
using FalconSDK.CrossPromotion.Scripts.Controller;
using FalconSDK.CrossPromotion.Scripts.Enum;
using FalconSDK.CrossPromotion.Scripts.Structs;
using UnityEngine;

namespace FalconSDK
{
	public static class FSDK
	{
		public static class CrossPromotion
		{
			public static void ShowFixedPromo()
			{
				GameObject gameObject = new GameObject("[WaitToShowPromo]", new Type[]
				{
					typeof(WaitToShowPromo)
				});
			}

			public static void CanShowFixedPromo(bool show)
			{
				CrossPromoFlag.ShowFixedPromo = show;
			}

			public static void CheckDownloadPromo()
			{
				GameObject gameObject = new GameObject("[PromoDownloader]", new Type[]
				{
					typeof(PromoDownloadController)
				});
			}

			private static PromoController GetPromoController()
			{
				GameObject gameObject = GameObject.Find("OneSoftPromoCanvas");
				if (gameObject == null)
				{
					UnityEngine.Debug.LogWarning("Promo Canvas not initial");
					return null;
				}
				return gameObject.GetComponent<PromoController>();
			}

			public static void ShowFloatPromo()
			{
				PromoController promoController = FSDK.CrossPromotion.GetPromoController();
				if (promoController == null)
				{
					return;
				}
				promoController.ShowFloatPromo();
			}

			public static void ShowAllPromo(bool show)
			{
				PromoController promoController = FSDK.CrossPromotion.GetPromoController();
				if (promoController == null)
				{
					return;
				}
				promoController.ShowAllChild(show);
			}

			public static void ShowPromoByType(PromoType promoType, bool show)
			{
				PromoController promoController = FSDK.CrossPromotion.GetPromoController();
				if (promoController == null)
				{
					return;
				}
				promoController.ShowChildByType(promoType, show);
			}

			public static void ShowPromoAtIndex(int index, bool show)
			{
				PromoController promoController = FSDK.CrossPromotion.GetPromoController();
				if (promoController == null)
				{
					return;
				}
				promoController.ShowChildAtIndex(index, show);
			}

			public static void ChangePromoAtIndex(int index, CrossGameItem promoInfo)
			{
				PromoController promoController = FSDK.CrossPromotion.GetPromoController();
				if (promoController == null)
				{
					return;
				}
				promoController.ChangePromoAtIndex(index, promoInfo);
			}

			public static List<CrossGameItem> CrossGameItems;
		}
	}
}
