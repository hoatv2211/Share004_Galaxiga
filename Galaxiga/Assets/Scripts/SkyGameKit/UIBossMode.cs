using System;
using UnityEngine;
using UnityEngine.UI;

namespace SkyGameKit
{
	public class UIBossMode : MonoBehaviour
	{
		private void Awake()
		{
			UIBossMode.inst = this;
		}

		public void Show()
		{
			this.redScreen.SetActive(true);
		}

		public void Hide()
		{
			this.redScreen.SetActive(false);
		}

		public void ShowText()
		{
			this.particalText.Play();
		}

		public static UIBossMode inst;

		public GameObject parentHealthBar;

		public Image[] listHealthBar;

		public Text txtName;

		[SerializeField]
		private GameObject redScreen;

		[SerializeField]
		private ParticleSystem particalText;

		public int numberHealthBar;
	}
}
