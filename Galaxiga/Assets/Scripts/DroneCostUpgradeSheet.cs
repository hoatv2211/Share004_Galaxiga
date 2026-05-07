using System;
using System.Collections.Generic;

[Serializable]
public class DroneCostUpgradeSheet
{
	public static DroneCostUpgradeSheet Get(int level)
	{
		return DroneCostUpgradeSheet.dictionary[level];
	}

	public static Dictionary<int, DroneCostUpgradeSheet> GetDictionary()
	{
		return DroneCostUpgradeSheet.dictionary;
	}

	public int level;

	public int priceCoin;

	public int priceGem;

	private static Dictionary<int, DroneCostUpgradeSheet> dictionary = new Dictionary<int, DroneCostUpgradeSheet>();
}
