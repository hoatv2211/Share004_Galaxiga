using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataStateItemShopCoin
{
	public static void LoadData()
	{
		string text = CacheGame.StateItemShopCoin;
		if (text == string.Empty)
		{
			SaveDataStateItemShopCoin.InitData();
		}
		else
		{
			SaveDataStateItemShopCoin.stateItemShopCoin = JsonUtility.FromJson<ShopItemCoinContainer>(text);
		}
	}

	public static void SaveData()
	{
		string text = JsonUtility.ToJson(SaveDataStateItemShopCoin.stateItemShopCoin, true);
		CacheGame.StateItemShopCoin = text;
	}

	public static void SetData(int indexItem)
	{
		SaveDataStateItemShopCoin.stateItemShopCoin.canBuyItem[indexItem] = false;
		SaveDataStateItemShopCoin.SaveData();
	}

	public static void InitData()
	{
		SaveDataStateItemShopCoin.stateItemShopCoin = new ShopItemCoinContainer();
		for (int i = 0; i < 15; i++)
		{
			SaveDataStateItemShopCoin.stateItemShopCoin.canBuyItem.Add(true);
		}
		SaveDataStateItemShopCoin.SaveData();
	}

	public static void ResetData()
	{
		for (int i = 0; i < 15; i++)
		{
			SaveDataStateItemShopCoin.stateItemShopCoin.canBuyItem[i] = true;
		}
		SaveDataStateItemShopCoin.SaveData();
	}

	public static bool StateItem(int indexItem)
	{
		return SaveDataStateItemShopCoin.stateItemShopCoin.canBuyItem[indexItem];
	}

	public static void SetData(List<bool> listBool)
	{
		SaveDataStateItemShopCoin.stateItemShopCoin = new ShopItemCoinContainer();
		SaveDataStateItemShopCoin.stateItemShopCoin.canBuyItem = listBool;
		SaveDataStateItemShopCoin.SaveData();
	}

	public static ShopItemCoinContainer stateItemShopCoin;
}
