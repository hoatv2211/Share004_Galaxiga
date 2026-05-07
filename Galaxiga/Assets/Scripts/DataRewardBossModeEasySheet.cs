using System;
using System.Collections.Generic;

[Serializable]
public class DataRewardBossModeEasySheet
{
	public static DataRewardBossModeEasySheet Get(int bossID)
	{
		return DataRewardBossModeEasySheet.dictionary[bossID];
	}

	public static Dictionary<int, DataRewardBossModeEasySheet> GetDictionary()
	{
		return DataRewardBossModeEasySheet.dictionary;
	}

	public int bossID;

	public int coin;

	public int cardRandomPlane;

	public int cardRandomDrone;

	public string nameBoss;

	private static Dictionary<int, DataRewardBossModeEasySheet> dictionary = new Dictionary<int, DataRewardBossModeEasySheet>();
}
