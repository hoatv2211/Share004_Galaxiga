using System;
using System.Collections.Generic;
using OneSoftCrossPromotion.Scripts.Structs;

namespace OneSoftCrossPromotion.Scripts.SampleData
{
	public static class GamePromos
	{
		public static List<GamePromoInfo> List = new List<GamePromoInfo>
		{
			new GamePromoInfo
			{
				url = VideoUrls.Sample[3],
				version = "1.0",
				name = "Game 1",
				bundleId = "com.ads.onesoft"
			},
			new GamePromoInfo
			{
				url = VideoUrls.Sample[1],
				version = "1.0",
				name = "Game 2",
				bundleId = "com.ads.onesoft111"
			},
			new GamePromoInfo
			{
				url = VideoUrls.Sample[2],
				version = "1.0",
				name = "Game 3",
				bundleId = "com.ads.onesoft222"
			},
			new GamePromoInfo
			{
				url = VideoUrls.Sample[0],
				version = "1.0",
				name = "Game 4",
				bundleId = "com.ads.onesoft222"
			}
		};
	}
}
