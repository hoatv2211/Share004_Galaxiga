using System;
using System.Collections.Generic;

[Serializable]
public class ShopCoinDataSheet
{
	public static ShopCoinDataSheet Get(int packID)
	{
		return ShopCoinDataSheet.dictionary[packID];
	}

	public static Dictionary<int, ShopCoinDataSheet> GetDictionary()
	{
		return ShopCoinDataSheet.dictionary;
	}

	public int packID;

	public string packName;

	public int numberCoin;

	public int priceGem;

	private static Dictionary<int, ShopCoinDataSheet> dictionary = new Dictionary<int, ShopCoinDataSheet>();
}
