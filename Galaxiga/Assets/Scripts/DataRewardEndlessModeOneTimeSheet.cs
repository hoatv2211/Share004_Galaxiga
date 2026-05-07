using System;
using System.Collections.Generic;

[Serializable]
public class DataRewardEndlessModeOneTimeSheet
{
	public static DataRewardEndlessModeOneTimeSheet Get(int wave)
	{
		return DataRewardEndlessModeOneTimeSheet.dictionary[wave];
	}

	public static Dictionary<int, DataRewardEndlessModeOneTimeSheet> GetDictionary()
	{
		return DataRewardEndlessModeOneTimeSheet.dictionary;
	}

	public int wave;

	public int coin;

	public int gem;

	public int cardAllPlane;

	public int cardAllDrone;

	private static Dictionary<int, DataRewardEndlessModeOneTimeSheet> dictionary = new Dictionary<int, DataRewardEndlessModeOneTimeSheet>();
}
