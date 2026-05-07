using System;
using FalconSDK.CrossPromotion.Scripts.Enum;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FalconSDK.Scripts
{
	public class TestController : MonoBehaviour
	{
		public void ChangeScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}

		public void TestClick()
		{
			UnityEngine.Debug.Log("Clicked");
		}

		public void ShowFixedPromo()
		{
			FSDK.CrossPromotion.ShowFixedPromo();
		}

		public void HideAllPromo()
		{
			FSDK.CrossPromotion.ShowAllPromo(false);
		}

		public void ShowPromoByType()
		{
			FSDK.CrossPromotion.ShowPromoByType(PromoType.Float, false);
		}

		public void HidePromoAtIndex(int index)
		{
			FSDK.CrossPromotion.ShowPromoAtIndex(index, false);
		}

		public void ShowAllPromo()
		{
			FSDK.CrossPromotion.ShowAllPromo(true);
		}

		public void ShowPromoAtIndex(int index)
		{
			FSDK.CrossPromotion.ShowPromoAtIndex(index, true);
		}

		public void ChangePromoAtIndex(int index)
		{
			FSDK.CrossPromotion.ChangePromoAtIndex(index, null);
		}
	}
}
