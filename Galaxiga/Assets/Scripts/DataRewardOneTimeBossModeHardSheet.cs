using System;
using System.Collections.Generic;

[Serializable]
public class DataRewardOneTimeBossModeHardSheet
{
	public static DataRewardOneTimeBossModeHardSheet Get(int bossID)
	{
		return DataRewardOneTimeBossModeHardSheet.dictionary[bossID];
	}

	public static Dictionary<int, DataRewardOneTimeBossModeHardSheet> GetDictionary()
	{
		return DataRewardOneTimeBossModeHardSheet.dictionary;
	}

	public int bossID;

	public int coin;

	public int gem;

	public int cardAllPlane;

	public int cardAllDrone;

	private static Dictionary<int, DataRewardOneTimeBossModeHardSheet> dictionary = new Dictionary<int, DataRewardOneTimeBossModeHardSheet>();
}
