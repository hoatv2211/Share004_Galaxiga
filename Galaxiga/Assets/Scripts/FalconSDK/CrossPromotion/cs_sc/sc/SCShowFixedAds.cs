using System;
using OSNet;
using UnityEngine;

namespace FalconSDK.CrossPromotion.cs_sc.sc
{
	public class SCShowFixedAds : SCMessage
	{
		public override string GetEvent()
		{
			return "sc_show_fixed_ads";
		}

		public override void OnData()
		{
			UnityEngine.Debug.Log("SCShowFixedAds");
			FSDK.CrossPromotion.ShowFixedPromo();
		}
	}
}
