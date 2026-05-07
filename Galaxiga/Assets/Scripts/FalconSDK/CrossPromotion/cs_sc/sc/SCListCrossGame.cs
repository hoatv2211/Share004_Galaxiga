using System;
using System.Collections.Generic;
using FalconSDK.CrossPromotion.cs_sc.item;
using FalconSDK.CrossPromotion.Scripts.Controller;
using OSNet;
using UnityEngine;

namespace FalconSDK.CrossPromotion.cs_sc.sc
{
	public class SCListCrossGame : SCMessage
	{
		public override string GetEvent()
		{
			return "sc_list_cross_game";
		}

		public override void OnData()
		{
			FSDK.CrossPromotion.CrossGameItems = this.datas;
			GameObject gameObject = new GameObject("[PromoDownloader]", new Type[]
			{
				typeof(PromoDownloadController)
			});
		}

		public List<CrossGameItem> datas;
	}
}
