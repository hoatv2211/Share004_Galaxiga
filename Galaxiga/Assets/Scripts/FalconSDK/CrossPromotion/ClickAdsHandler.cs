using System;
using System.Collections.Generic;

namespace FalconSDK.CrossPromotion
{
	public static class ClickAdsHandler
	{
		public static void OnClickFixedAds(string promotedAppId)
		{
			ClickAdsHandler.OnClickAds(promotedAppId, "cross_promotion_fixed_ads");
		}

		public static void OnClickFloatAds(string promotedAppId)
		{
			ClickAdsHandler.OnClickAds(promotedAppId, "cross_promotion_float_ads");
		}

		public static void OnClickAds(string promotedAppId, string campaign)
		{
			if (!string.IsNullOrEmpty(promotedAppId))
			{
				Dictionary<string, string> customParams = new Dictionary<string, string>();
			}
		}
	}
}
