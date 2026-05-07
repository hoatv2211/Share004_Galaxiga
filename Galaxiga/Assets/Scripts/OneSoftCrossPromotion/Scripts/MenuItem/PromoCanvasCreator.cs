using System;
using OneSoftCrossPromotion.Scripts.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace OneSoftCrossPromotion.Scripts.MenuItem
{
	public static class PromoCanvasCreator
	{
		public static GameObject Initiate()
		{
			TagHelper.AddTag("OneSoftPromoAds");
			GameObject gameObject = GameObject.FindWithTag("OneSoftPromoAds");
			if (gameObject == null)
			{
				gameObject = new GameObject("OneSoftPromoAdsCanvas", new Type[]
				{
					typeof(Canvas),
					typeof(CanvasScaler),
					typeof(PromoController)
				});
				gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
				CanvasScaler component = gameObject.GetComponent<CanvasScaler>();
				component.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
				component.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
				component.referenceResolution = ((Screen.width / Screen.height >= 1) ? new Vector2(1920f, 1080f) : new Vector2(1080f, 1920f));
				gameObject.tag = "OneSoftPromoAds";
				gameObject.layer = 5;
				gameObject.GetComponent<Canvas>().sortingOrder = 10;
			}
			return gameObject;
		}
	}
}
