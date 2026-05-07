using System;
using System.Collections.Generic;

[Serializable]
public class GemReviveDataSheet
{
	public static GemReviveDataSheet Get(int idPrice)
	{
		return GemReviveDataSheet.dictionary[idPrice];
	}

	public static Dictionary<int, GemReviveDataSheet> GetDictionary()
	{
		return GemReviveDataSheet.dictionary;
	}

	public int idPrice;

	public int level;

	public int price1;

	public int price2;

	public int price3;

	private static Dictionary<int, GemReviveDataSheet> dictionary = new Dictionary<int, GemReviveDataSheet>();
}
