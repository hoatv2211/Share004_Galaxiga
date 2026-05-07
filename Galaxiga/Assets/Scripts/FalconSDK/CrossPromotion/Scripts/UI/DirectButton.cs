using System;
using FalconSDK.CrossPromotion.cs_sc.item;
using FalconSDK.CrossPromotion.Scripts.Controller;
using FalconSDK.CrossPromotion.Scripts.Enum;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FalconSDK.CrossPromotion.Scripts.UI
{
	[RequireComponent(typeof(Button))]
	public class DirectButton : MonoBehaviour
	{
		private void Start()
		{
			Button component = base.GetComponent<Button>();
			component.onClick.AddListener(new UnityAction(this.OnClick));
		}

		private void OnClick()
		{
			CrossGameItem promoInfo = this.videoController.promoInfo;
			PromoType promoType = this.videoController.promoType;
			if (promoType == PromoType.Float)
			{
				UnityEngine.Debug.Log(promoInfo.name + " " + promoType);
				ClickAdsHandler.OnClickFloatAds(promoInfo.packageName);
			}
			else
			{
				UnityEngine.Debug.Log(promoInfo.name + " " + promoType);
				ClickAdsHandler.OnClickFixedAds(promoInfo.packageName);
			}
		}

		public VideoController videoController;
	}
}
