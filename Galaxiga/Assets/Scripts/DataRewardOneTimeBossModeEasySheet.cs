using System;
using System.Collections.Generic;

[Serializable]
public class DataRewardOneTimeBossModeEasySheet
{
	public static DataRewardOneTimeBossModeEasySheet Get(int bossID)
	{
		return DataRewardOneTimeBossModeEasySheet.dictionary[bossID];
	}

	public static Dictionary<int, DataRewardOneTimeBossModeEasySheet> GetDictionary()
	{
		return DataRewardOneTimeBossModeEasySheet.dictionary;
	}

	public int bossID;

	public int coin;

	public int gem;

	public int cardAllPlane;

	public int cardAllDrone;

	private static Dictionary<int, DataRewardOneTimeBossModeEasySheet> dictionary = new Dictionary<int, DataRewardOneTimeBossModeEasySheet>();
}
