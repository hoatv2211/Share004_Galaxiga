using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneSoftCrossPromotion.Scripts.Controller
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
			OneSoftPromo.ShowFixedPromo();
		}

		public void HideAllPromo()
		{
			OneSoftPromo.HideAllPromo();
		}

		public void HidePromoAtIndex(int index)
		{
			OneSoftPromo.HidePromoAtIndex(index);
		}

		public void ShowAllPromo()
		{
			OneSoftPromo.ShowAllPromo();
		}

		public void ShowPromoAtIndex(int index)
		{
			OneSoftPromo.ShowPromoAtIndex(index);
		}

		public void ChangePromoAtIndex(int index)
		{
			OneSoftPromo.ChangePromoAtIndex(index, null);
		}
	}
}
