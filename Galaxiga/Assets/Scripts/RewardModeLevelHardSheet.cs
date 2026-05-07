using System;
using System.Collections.Generic;

[Serializable]
public class RewardModeLevelHardSheet
{
	public static RewardModeLevelHardSheet Get(int level)
	{
		return RewardModeLevelHardSheet.dictionary[level];
	}

	public static Dictionary<int, RewardModeLevelHardSheet> GetDictionary()
	{
		return RewardModeLevelHardSheet.dictionary;
	}

	public int level;

	public string cardPlane;

	public int numberCardPlane;

	public string cardDrone;

	public int numberCardDrone;

	public string unlockPlane;

	public int numberCoins;

	public int numberGems;

	public int numberBox;

	private static Dictionary<int, RewardModeLevelHardSheet> dictionary = new Dictionary<int, RewardModeLevelHardSheet>();
}
