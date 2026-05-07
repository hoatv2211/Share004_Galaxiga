using System;
using System.Collections.Generic;

[Serializable]
public class DataRewardEndlessModeSheet
{
	public static DataRewardEndlessModeSheet Get(int wave)
	{
		return DataRewardEndlessModeSheet.dictionary[wave];
	}

	public static Dictionary<int, DataRewardEndlessModeSheet> GetDictionary()
	{
		return DataRewardEndlessModeSheet.dictionary;
	}

	public int wave;

	public int coin;

	public int gem;

	public int cardAllPlane;

	public int cardAllDrone;

	public int cardRandomPlane;

	public int cardRandomDrone;

	private static Dictionary<int, DataRewardEndlessModeSheet> dictionary = new Dictionary<int, DataRewardEndlessModeSheet>();
}
