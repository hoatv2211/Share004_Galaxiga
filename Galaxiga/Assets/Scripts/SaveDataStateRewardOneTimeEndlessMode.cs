using System;
using UnityEngine;

public class SaveDataStateRewardOneTimeEndlessMode
{
	public static void LoadData()
	{
		string stateClaimRewardOneTimeEndlessMode = CacheGame.StateClaimRewardOneTimeEndlessMode;
		if (stateClaimRewardOneTimeEndlessMode == string.Empty)
		{
			SaveDataStateRewardOneTimeEndlessMode.InitData();
		}
		else
		{
			SaveDataStateRewardOneTimeEndlessMode.arrStateClaimRewards = JsonUtility.FromJson<EndlessModeStateRewardOneTimeContainer>(stateClaimRewardOneTimeEndlessMode);
		}
	}

	public static void SaveData()
	{
		string stateClaimRewardOneTimeEndlessMode = JsonUtility.ToJson(SaveDataStateRewardOneTimeEndlessMode.arrStateClaimRewards, true);
		CacheGame.StateClaimRewardOneTimeEndlessMode = stateClaimRewardOneTimeEndlessMode;
	}

	public static void SetData(int indexItem)
	{
		SaveDataStateRewardOneTimeEndlessMode.arrStateClaimRewards.arrStateReward[indexItem] = true;
		SaveDataStateRewardOneTimeEndlessMode.SaveData();
	}

	public static void InitData()
	{
		SaveDataStateRewardOneTimeEndlessMode.arrStateClaimRewards = new EndlessModeStateRewardOneTimeContainer();
		for (int i = 0; i < 30; i++)
		{
			SaveDataStateRewardOneTimeEndlessMode.arrStateClaimRewards.arrStateReward.Add(false);
		}
		SaveDataStateRewardOneTimeEndlessMode.SaveData();
	}

	public static bool StateItem(int indexItem)
	{
		return SaveDataStateRewardOneTimeEndlessMode.arrStateClaimRewards.arrStateReward[indexItem];
	}

	public static EndlessModeStateRewardOneTimeContainer arrStateClaimRewards;
}
