using System;
using System.Collections.Generic;

[Serializable]
public class DataRewardBossModeNormalSheet
{
	public static DataRewardBossModeNormalSheet Get(int bossID)
	{
		return DataRewardBossModeNormalSheet.dictionary[bossID];
	}

	public static Dictionary<int, DataRewardBossModeNormalSheet> GetDictionary()
	{
		return DataRewardBossModeNormalSheet.dictionary;
	}

	public int bossID;

	public int coin;

	public int cardRandomPlane;

	public int cardRandomDrone;

	private static Dictionary<int, DataRewardBossModeNormalSheet> dictionary = new Dictionary<int, DataRewardBossModeNormalSheet>();
}
