using System;
using System.Collections.Generic;

[Serializable]
public class DataEventNoelSheet
{
	public static DataEventNoelSheet Get(int idBox)
	{
		return DataEventNoelSheet.dictionary[idBox];
	}

	public static Dictionary<int, DataEventNoelSheet> GetDictionary()
	{
		return DataEventNoelSheet.dictionary;
	}

	public int idBox;

	public int numberGems;

	public int numberCoins;

	public string nameCard;

	public int numberCards;

	public int numberCardAllDrone;

	public int numberCandy;

	private static Dictionary<int, DataEventNoelSheet> dictionary = new Dictionary<int, DataEventNoelSheet>();
}
