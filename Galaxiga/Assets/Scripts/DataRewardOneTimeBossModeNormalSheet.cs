using System;
using System.Collections.Generic;

[Serializable]
public class DataRewardOneTimeBossModeNormalSheet
{
	public static DataRewardOneTimeBossModeNormalSheet Get(int bossID)
	{
		return DataRewardOneTimeBossModeNormalSheet.dictionary[bossID];
	}

	public static Dictionary<int, DataRewardOneTimeBossModeNormalSheet> GetDictionary()
	{
		return DataRewardOneTimeBossModeNormalSheet.dictionary;
	}

	public int bossID;

	public int coin;

	public int gem;

	public int cardAllPlane;

	public int cardAllDrone;

	private static Dictionary<int, DataRewardOneTimeBossModeNormalSheet> dictionary = new Dictionary<int, DataRewardOneTimeBossModeNormalSheet>();
}
