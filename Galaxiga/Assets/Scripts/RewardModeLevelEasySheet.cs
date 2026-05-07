using System;
using System.Collections.Generic;

[Serializable]
public class RewardModeLevelEasySheet
{
	public static RewardModeLevelEasySheet Get(int level)
	{
		return RewardModeLevelEasySheet.dictionary[level];
	}

	public static Dictionary<int, RewardModeLevelEasySheet> GetDictionary()
	{
		return RewardModeLevelEasySheet.dictionary;
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

	private static Dictionary<int, RewardModeLevelEasySheet> dictionary = new Dictionary<int, RewardModeLevelEasySheet>();
}
