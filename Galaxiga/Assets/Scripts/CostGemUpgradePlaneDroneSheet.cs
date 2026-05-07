using System;
using System.Collections.Generic;

[Serializable]
public class CostGemUpgradePlaneDroneSheet
{
	public static CostGemUpgradePlaneDroneSheet Get(int level)
	{
		return CostGemUpgradePlaneDroneSheet.dictionary[level];
	}

	public static Dictionary<int, CostGemUpgradePlaneDroneSheet> GetDictionary()
	{
		return CostGemUpgradePlaneDroneSheet.dictionary;
	}

	public int level;

	public int plane;

	public int drone;

	private static Dictionary<int, CostGemUpgradePlaneDroneSheet> dictionary = new Dictionary<int, CostGemUpgradePlaneDroneSheet>();
}
