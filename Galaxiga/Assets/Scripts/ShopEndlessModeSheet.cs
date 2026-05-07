using System;
using System.Collections.Generic;

[Serializable]
public class ShopEndlessModeSheet
{
	public static ShopEndlessModeSheet Get(int id)
	{
		return ShopEndlessModeSheet.dictionary[id];
	}

	public static Dictionary<int, ShopEndlessModeSheet> GetDictionary()
	{
		return ShopEndlessModeSheet.dictionary;
	}

	public int id;

	public int itemID;

	public int number;

	public int price;

	public int basePrice;

	private static Dictionary<int, ShopEndlessModeSheet> dictionary = new Dictionary<int, ShopEndlessModeSheet>();
}
