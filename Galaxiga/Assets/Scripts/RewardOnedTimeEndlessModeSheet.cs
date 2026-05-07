using System;
using System.Collections.Generic;

[Serializable]
public class RewardOnedTimeEndlessModeSheet
{
	public static RewardOnedTimeEndlessModeSheet Get(int wave)
	{
		return RewardOnedTimeEndlessModeSheet.dictionary[wave];
	}

	public static Dictionary<int, RewardOnedTimeEndlessModeSheet> GetDictionary()
	{
		return RewardOnedTimeEndlessModeSheet.dictionary;
	}

	public int wave;

	public int coins;

	public int blueStars;

	private static Dictionary<int, RewardOnedTimeEndlessModeSheet> dictionary = new Dictionary<int, RewardOnedTimeEndlessModeSheet>();
}
