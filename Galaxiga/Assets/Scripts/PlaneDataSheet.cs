using System;
using System.Collections.Generic;

[Serializable]
public class PlaneDataSheet
{
	public static PlaneDataSheet Get(int planeID)
	{
		return PlaneDataSheet.dictionary[planeID];
	}

	public static Dictionary<int, PlaneDataSheet> GetDictionary()
	{
		return PlaneDataSheet.dictionary;
	}

	public int planeID;

	public string planeName;

	public int mainPower;

	public float durationSkill;

	public float multiplierSuperPower;

	public float multiplierSubPower;

	public float fireRateSuperPower;

	public float firerateSubPower;

	public int levelUnlock;

	public string modeLevelUnlock;

	public string rank;

	public int priceGem;

	public int priceCoin;

	public int vipToGet;

	private static Dictionary<int, PlaneDataSheet> dictionary = new Dictionary<int, PlaneDataSheet>();
}
