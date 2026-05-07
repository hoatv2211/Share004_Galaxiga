using System;
using FalconSDK.CrossPromotion.Scripts.Controller;
using FalconSDK.CrossPromotion.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace FalconSDK.CrossPromotion.Scripts.MenuItem
{
	public static class FixedPromoCreator
	{
		public static GameObject Initiate(Transform parent)
		{
			GameObject gameObject = new GameObject("FixedPromo", new Type[]
			{
				typeof(RectTransform),
				typeof(VideoController),
				typeof(VideoPlayer),
				typeof(Canvas),
				typeof(GraphicRaycaster),
				typeof(CloseButton),
				typeof(Image)
			});
			gameObject.transform.SetParent(parent);
			gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.6f);
			gameObject.GetComponent<CloseButton>().parent = gameObject;
			Canvas component = gameObject.GetComponent<Canvas>();
			component.overrideSorting = true;
			component.sortingOrder = 10;
			SortingLayer[] layers = SortingLayer.layers;
			component.sortingLayerName = layers[layers.Length - 1].name;
			gameObject.gameObject.SetActive(false);
			RectTransform component2 = gameObject.GetComponent<RectTransform>();
			component2.anchorMin = new Vector2(0f, 0f);
			component2.anchorMax = new Vector2(1f, 1f);
			component2.offsetMin = new Vector2(0f, 0f);
			component2.offsetMax = new Vector2(0f, 0f);
			GameObject gameObject2 = new GameObject("Content", new Type[]
			{
				typeof(Image),
				typeof(DirectButton)
			});
			gameObject2.GetComponent<DirectButton>().videoController = gameObject.GetComponent<VideoController>();
			gameObject2.transform.SetParent(gameObject.transform, false);
			RectTransform component3 = gameObject2.GetComponent<RectTransform>();
			component3.offsetMin = new Vector2(0f, 0f);
			component3.offsetMax = new Vector2(0f, 0f);
			gameObject2.layer = 5;
			gameObject2.SetActive(false);
			GameObject gameObject3 = FixedPromoCreator.CreateVideoPlayer(gameObject2.transform);
			GameObject gameObject4 = FixedPromoCreator.CreateCloseButton(gameObject2.transform);
			gameObject4.GetComponent<CloseButton>().parent = gameObject;
			gameObject.GetComponent<VideoController>().rawImage = gameObject3.GetComponent<RawImage>();
			return gameObject;
		}

		private static GameObject CreateVideoPlayer(Transform parent)
		{
			GameObject gameObject = new GameObject("VideoPlayer", new Type[]
			{
				typeof(RawImage)
			});
			gameObject.GetComponent<RawImage>().color = Color.white;
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = new Vector2(0.5f, 0.5f);
			component.anchorMax = new Vector2(0.5f, 0.5f);
			component.offsetMin = new Vector2(0f, 0f);
			component.offsetMax = new Vector2(0f, 0f);
			gameObject.layer = 5;
			gameObject.transform.SetParent(parent, false);
			return gameObject;
		}

		private static GameObject CreateCloseButton(Transform parent)
		{
			GameObject gameObject = new GameObject("CloseButton", new Type[]
			{
				typeof(Image),
				typeof(Button),
				typeof(CloseButton)
			});
			gameObject.transform.SetParent(parent);
			gameObject.layer = 5;
			float num = (float)Screen.width / (float)Screen.height;
			float num2 = 80f;
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = new Vector2(1f, 1f);
			component.anchorMax = new Vector2(1f, 1f);
			component.offsetMax = new Vector2(-num2 / 2f, -num2 / 2f);
			component.offsetMin = new Vector2(-num2 / 2f, -num2 / 2f);
			component.sizeDelta = new Vector2(num2, num2);
			Image component2 = gameObject.GetComponent<Image>();
			component2.GetComponent<Image>().sprite = Resources.Load<Sprite>("ico_close");
			component2.type = Image.Type.Sliced;
			component2.fillCenter = true;
			return gameObject;
		}
	}
}
