using System;
using System.Collections.Generic;

[Serializable]
public class DroneDataMaxSheet
{
	public static DroneDataMaxSheet Get(int rankID)
	{
		return DroneDataMaxSheet.dictionary[rankID];
	}

	public static Dictionary<int, DroneDataMaxSheet> GetDictionary()
	{
		return DroneDataMaxSheet.dictionary;
	}

	public int rankID;

	public string rank;

	public int maxLevel;

	public int cardCraft;

	public int costEvolve;

	public string nameAbility;

	public int percent;

	private static Dictionary<int, DroneDataMaxSheet> dictionary = new Dictionary<int, DroneDataMaxSheet>();
}
