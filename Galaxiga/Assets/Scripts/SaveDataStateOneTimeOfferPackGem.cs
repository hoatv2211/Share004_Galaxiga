using System;
using UnityEngine;

public class SaveDataStateOneTimeOfferPackGem
{
	public static void LoadData()
	{
		string stateOneTimeOfferPackGem = CacheGame.StateOneTimeOfferPackGem;
		if (stateOneTimeOfferPackGem == string.Empty)
		{
			SaveDataStateOneTimeOfferPackGem.InitData();
		}
		else
		{
			SaveDataStateOneTimeOfferPackGem.shopGemContainer = JsonUtility.FromJson<ShopGemContainer>(stateOneTimeOfferPackGem);
		}
	}

	public static void SaveData()
	{
		string text = JsonUtility.ToJson(SaveDataStateOneTimeOfferPackGem.shopGemContainer, true);
		CacheGame.StateOneTimeOfferPackGem = text;
		UnityEngine.Debug.Log(text);
	}

	public static void SetData(int indexItem)
	{
		SaveDataStateOneTimeOfferPackGem.shopGemContainer.listBool[indexItem] = false;
		SaveDataStateOneTimeOfferPackGem.SaveData();
	}

	public static void InitData()
	{
		SaveDataStateOneTimeOfferPackGem.shopGemContainer = new ShopGemContainer();
		for (int i = 0; i < 6; i++)
		{
			SaveDataStateOneTimeOfferPackGem.shopGemContainer.listBool.Add(true);
		}
		SaveDataStateOneTimeOfferPackGem.SaveData();
	}

	public static void AddData()
	{
		SaveDataStateOneTimeOfferPackGem.shopGemContainer.listBool.Add(true);
		SaveDataStateOneTimeOfferPackGem.SaveData();
	}

	public static bool IsOfferPack(int indexItem)
	{
		return SaveDataStateOneTimeOfferPackGem.shopGemContainer.listBool[indexItem];
	}

	public static ShopGemContainer shopGemContainer;
}
