using System;
using System.Collections;
using FalconSDK.CrossPromotion.Scripts.MenuItem;
using FalconSDK.CrossPromotion.Scripts.Singleton;
using FalconSDK.CrossPromotion.Scripts.Structs;
using UnityEngine;

namespace FalconSDK.CrossPromotion.Scripts.Controller
{
	public class WaitToShowPromo : PersistentSingleton<WaitToShowPromo>
	{
		private void Start()
		{
			base.StartCoroutine(this.WaitShowFixed());
		}

		private IEnumerator WaitShowFixed()
		{
			while (!CrossPromoFlag.ShowFixedPromo)
			{
				yield return null;
			}
			GameObject canvas = PromoCanvasCreator.Initiate();
			PromoController promoController = canvas.GetComponent<PromoController>();
			promoController.ShowFixedPromo();
			UnityEngine.Object.Destroy(base.gameObject);
			yield break;
		}
	}
}
