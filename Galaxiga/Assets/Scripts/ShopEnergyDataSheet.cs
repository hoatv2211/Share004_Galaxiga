using System;
using System.Collections.Generic;

[Serializable]
public class ShopEnergyDataSheet
{
	public static ShopEnergyDataSheet Get(int energyID)
	{
		return ShopEnergyDataSheet.dictionary[energyID];
	}

	public static Dictionary<int, ShopEnergyDataSheet> GetDictionary()
	{
		return ShopEnergyDataSheet.dictionary;
	}

	public int energyID;

	public int numberEnergy;

	public int numberAvailbale;

	public int price0;

	public int price1;

	public int price2;

	public int price3;

	private static Dictionary<int, ShopEnergyDataSheet> dictionary = new Dictionary<int, ShopEnergyDataSheet>();
}
