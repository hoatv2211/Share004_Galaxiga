using System;
using System.Collections.Generic;

[Serializable]
public class RewardModeLevelNormalSheet
{
	public static RewardModeLevelNormalSheet Get(int level)
	{
		return RewardModeLevelNormalSheet.dictionary[level];
	}

	public static Dictionary<int, RewardModeLevelNormalSheet> GetDictionary()
	{
		return RewardModeLevelNormalSheet.dictionary;
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

	private static Dictionary<int, RewardModeLevelNormalSheet> dictionary = new Dictionary<int, RewardModeLevelNormalSheet>();
}
