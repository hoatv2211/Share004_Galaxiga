using System;
using System.Collections.Generic;

[Serializable]
public class DataRewardBossModeHardSheet
{
	public static DataRewardBossModeHardSheet Get(int bossID)
	{
		return DataRewardBossModeHardSheet.dictionary[bossID];
	}

	public static Dictionary<int, DataRewardBossModeHardSheet> GetDictionary()
	{
		return DataRewardBossModeHardSheet.dictionary;
	}

	public int bossID;

	public int coin;

	public int cardRandomPlane;

	public int cardRandomDrone;

	private static Dictionary<int, DataRewardBossModeHardSheet> dictionary = new Dictionary<int, DataRewardBossModeHardSheet>();
}
