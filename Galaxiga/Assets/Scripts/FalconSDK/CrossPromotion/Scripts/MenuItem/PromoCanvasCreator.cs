using System;
using FalconSDK.CrossPromotion.Scripts.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace FalconSDK.CrossPromotion.Scripts.MenuItem
{
	public static class PromoCanvasCreator
	{
		public static GameObject Initiate()
		{
			GameObject gameObject = GameObject.Find("OneSoftPromoCanvas");
			if (gameObject == null)
			{
				gameObject = new GameObject("OneSoftPromoCanvas", new Type[]
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
				gameObject.layer = 5;
				gameObject.GetComponent<Canvas>().sortingOrder = 10;
			}
			return gameObject;
		}
	}
}
