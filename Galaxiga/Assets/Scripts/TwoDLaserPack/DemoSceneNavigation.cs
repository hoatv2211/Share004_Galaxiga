using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TwoDLaserPack
{
	public class DemoSceneNavigation : MonoBehaviour
	{
		private void Start()
		{
			this.buttonNextDemo.onClick.AddListener(new UnityAction(this.OnButtonNextDemoClick));
		}

		private void OnButtonNextDemoClick()
		{
			int buildIndex = SceneManager.GetActiveScene().buildIndex;
			if (buildIndex < SceneManager.sceneCount - 1)
			{
				SceneManager.LoadScene(buildIndex + 1);
			}
			else
			{
				SceneManager.LoadScene(0);
			}
		}

		private void Update()
		{
		}

		public Button buttonNextDemo;
	}
}
