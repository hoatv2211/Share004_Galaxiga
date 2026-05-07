using System;
using System.Collections.Generic;

[Serializable]
public class ShopCoinItemSheet
{
	public static ShopCoinItemSheet Get(int idItem)
	{
		return ShopCoinItemSheet.dictionary[idItem];
	}

	public static Dictionary<int, ShopCoinItemSheet> GetDictionary()
	{
		return ShopCoinItemSheet.dictionary;
	}

	public int idItem;

	public string nameItem;

	public int numberItem;

	public int priceItem;

	private static Dictionary<int, ShopCoinItemSheet> dictionary = new Dictionary<int, ShopCoinItemSheet>();
}
