using System;
using OneSoftCrossPromotion.Scripts.Controller;
using OneSoftCrossPromotion.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace OneSoftCrossPromotion.Scripts.MenuItem
{
	public static class FixedPromoCreator
	{
		public static GameObject Initiate(Transform parent)
		{
			string str = (parent.childCount != 0) ? (" (" + parent.childCount + ") ") : string.Empty;
			GameObject gameObject = new GameObject("FixedPromo" + str, new Type[]
			{
				typeof(RectTransform),
				typeof(VideoController),
				typeof(VideoPlayer),
				typeof(Canvas),
				typeof(GraphicRaycaster),
				typeof(DirectButton)
			});
			gameObject.transform.SetParent(parent);
			gameObject.GetComponent<DirectButton>().videoController = gameObject.GetComponent<VideoController>();
			Canvas component = gameObject.GetComponent<Canvas>();
			component.overrideSorting = true;
			component.sortingOrder = 11;
			gameObject.gameObject.SetActive(false);
			RectTransform component2 = gameObject.GetComponent<RectTransform>();
			component2.anchorMin = new Vector2(0f, 0f);
			component2.anchorMax = new Vector2(1f, 1f);
			component2.offsetMin = new Vector2(0f, 0f);
			component2.offsetMax = new Vector2(0f, 0f);
			component2.sizeDelta = new Vector2(0f, 0f);
			GameObject gameObject2 = new GameObject("Content", new Type[]
			{
				typeof(Image),
				typeof(FixedPromo)
			});
			gameObject2.transform.SetParent(gameObject.transform, false);
			RectTransform component3 = gameObject2.GetComponent<RectTransform>();
			component3.anchorMin = new Vector2(0f, 0f);
			component3.anchorMax = new Vector2(1f, 1f);
			component3.offsetMin = new Vector2(150f, 350f);
			component3.offsetMax = new Vector2(-150f, -350f);
			gameObject2.layer = 5;
			gameObject2.SetActive(false);
			GameObject gameObject3 = FixedPromoCreator.CreateVideoPlayer(gameObject2.transform);
			GameObject gameObject4 = FixedPromoCreator.CreateCloseButton(gameObject2.transform);
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
			component.anchorMin = new Vector2(0f, 0f);
			component.anchorMax = new Vector2(1f, 1f);
			component.offsetMin = new Vector2(6f, 6f);
			component.offsetMax = new Vector2(-6f, -6f);
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
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = new Vector2(1f, 1f);
			component.anchorMax = new Vector2(1f, 1f);
			component.offsetMax = new Vector2(-40f, -40f);
			component.offsetMin = new Vector2(-40f, -40f);
			component.sizeDelta = new Vector2(60f, 60f);
			Image component2 = gameObject.GetComponent<Image>();
			component2.GetComponent<Image>().sprite = Resources.Load<Sprite>("ico_close");
			component2.type = Image.Type.Sliced;
			component2.fillCenter = true;
			return gameObject;
		}
	}
}
