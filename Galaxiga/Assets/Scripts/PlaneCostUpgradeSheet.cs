using System;
using System.Collections.Generic;

[Serializable]
public class PlaneCostUpgradeSheet
{
	public static PlaneCostUpgradeSheet Get(int level)
	{
		return PlaneCostUpgradeSheet.dictionary[level];
	}

	public static Dictionary<int, PlaneCostUpgradeSheet> GetDictionary()
	{
		return PlaneCostUpgradeSheet.dictionary;
	}

	public int level;

	public int costCoin;

	public int costGem;

	private static Dictionary<int, PlaneCostUpgradeSheet> dictionary = new Dictionary<int, PlaneCostUpgradeSheet>();
}
