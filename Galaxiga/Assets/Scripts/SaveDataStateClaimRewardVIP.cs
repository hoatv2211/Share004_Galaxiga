using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataStateClaimRewardVIP
{
	public static void LoadData()
	{
		string text = CacheGame.StateClaimRewardVIP;
		if (text == string.Empty)
		{
			SaveDataStateClaimRewardVIP.InitData();
		}
		else
		{
			SaveDataStateClaimRewardVIP.stateClaimRewardVIP = JsonUtility.FromJson<StateClaimRewardVIPContainer>(text);
		}
	}

	public static void SaveData()
	{
		string text = JsonUtility.ToJson(SaveDataStateClaimRewardVIP.stateClaimRewardVIP, true);
		CacheGame.StateClaimRewardVIP = text;
	}

	public static void SetData(int indexItem)
	{
		SaveDataStateClaimRewardVIP.stateClaimRewardVIP.arrBool[indexItem] = false;
		SaveDataStateClaimRewardVIP.SaveData();
	}

	public static void InitData()
	{
		SaveDataStateClaimRewardVIP.stateClaimRewardVIP = new StateClaimRewardVIPContainer();
		for (int i = 0; i < 15; i++)
		{
			SaveDataStateClaimRewardVIP.stateClaimRewardVIP.arrBool.Add(true);
		}
		SaveDataStateClaimRewardVIP.SaveData();
	}

	public static bool StateItem(int indexItem)
	{
		UnityEngine.Debug.Log("Can Claim Reward VIP " + SaveDataStateClaimRewardVIP.stateClaimRewardVIP.arrBool[indexItem]);
		return SaveDataStateClaimRewardVIP.stateClaimRewardVIP.arrBool[indexItem];
	}

	public static void SetData(List<bool> arrBools)
	{
		SaveDataStateClaimRewardVIP.stateClaimRewardVIP = new StateClaimRewardVIPContainer();
		SaveDataStateClaimRewardVIP.stateClaimRewardVIP.arrBool = arrBools;
		SaveDataStateClaimRewardVIP.SaveData();
	}

	public static StateClaimRewardVIPContainer stateClaimRewardVIP;
}
