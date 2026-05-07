using System;
using System.Collections.Generic;

[Serializable]
public class PlaneDataMaxSheet
{
	public static PlaneDataMaxSheet Get(int rankID)
	{
		return PlaneDataMaxSheet.dictionary[rankID];
	}

	public static Dictionary<int, PlaneDataMaxSheet> GetDictionary()
	{
		return PlaneDataMaxSheet.dictionary;
	}

	public int rankID;

	public string rank;

	public int maxLevel;

	public int cardEvovle;

	public int costEvolve;

	public float multiPlier;

	private static Dictionary<int, PlaneDataMaxSheet> dictionary = new Dictionary<int, PlaneDataMaxSheet>();
}
