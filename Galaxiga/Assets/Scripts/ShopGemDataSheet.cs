using System;
using System.Collections.Generic;

[Serializable]
public class ShopGemDataSheet
{
	public static ShopGemDataSheet Get(int packID)
	{
		return ShopGemDataSheet.dictionary[packID];
	}

	public static Dictionary<int, ShopGemDataSheet> GetDictionary()
	{
		return ShopGemDataSheet.dictionary;
	}

	public int packID;

	public string packName;

	public int numberGem;

	public int numberGemOneTime;

	public float price;

	private static Dictionary<int, ShopGemDataSheet> dictionary = new Dictionary<int, ShopGemDataSheet>();
}
