using System;
using UnityEngine;

public class SaveDataEventNoel
{
	public static void LoadData()
	{
		string stateClaimRewardEventNoel = CacheGame.StateClaimRewardEventNoel;
		if (stateClaimRewardEventNoel == string.Empty)
		{
			SaveDataEventNoel.InitData();
		}
		else
		{
			SaveDataEventNoel.noelContainer = JsonUtility.FromJson<EventNoelContainer>(stateClaimRewardEventNoel);
			UnityEngine.Debug.Log(stateClaimRewardEventNoel);
		}
	}

	public static void SaveData()
	{
		string text = JsonUtility.ToJson(SaveDataEventNoel.noelContainer, true);
		CacheGame.StateClaimRewardEventNoel = text;
		UnityEngine.Debug.Log(text);
	}

	public static void SetData(int indexItem)
	{
		SaveDataEventNoel.noelContainer.listBoolStateReward[indexItem] = true;
		SaveDataEventNoel.SaveData();
	}

	public static void InitData()
	{
		SaveDataEventNoel.noelContainer = new EventNoelContainer();
		for (int i = 0; i < 13; i++)
		{
			SaveDataEventNoel.noelContainer.listBoolStateReward.Add(false);
		}
		SaveDataEventNoel.SaveData();
	}

	public static bool StateItem(int indexItem)
	{
		return SaveDataEventNoel.noelContainer.listBoolStateReward[indexItem];
	}

	public static EventNoelContainer noelContainer;
}
