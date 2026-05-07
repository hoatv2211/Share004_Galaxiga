using System;
using OneSoftCrossPromotion.Scripts.Controller;
using OneSoftCrossPromotion.Scripts.Structs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace OneSoftCrossPromotion.Scripts.UI
{
	[RequireComponent(typeof(Button))]
	public class DirectButton : MonoBehaviour
	{
		private void Awake()
		{
			Button component = base.GetComponent<Button>();
			component.onClick.AddListener(new UnityAction(this.OnClick));
		}

		private void OnClick()
		{
			GamePromoInfo promoInfo = this.videoController.promoInfo;
			UnityEngine.Debug.Log(promoInfo.name);
		}

		public VideoController videoController;
	}
}
