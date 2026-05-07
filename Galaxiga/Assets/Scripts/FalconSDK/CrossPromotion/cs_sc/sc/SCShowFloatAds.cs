using System;
using FalconSDK.CrossPromotion.Scripts.Controller;
using FalconSDK.CrossPromotion.Scripts.Singleton;
using FalconSDK.CrossPromotion.Scripts.Structs;
using OSNet;
using UnityEngine;

namespace FalconSDK.CrossPromotion.cs_sc.sc
{
	public class SCShowFloatAds : SCMessage
	{
		public override string GetEvent()
		{
			return "sc_show_float_ads";
		}

		public override void OnData()
		{
			UnityEngine.Debug.Log("SCShowFloatAds");
			CrossPromoFlag.ShowFloatPromo = true;
			if (Singleton<PromoController>.Instance != null)
			{
				FSDK.CrossPromotion.ShowFloatPromo();
			}
		}
	}
}
