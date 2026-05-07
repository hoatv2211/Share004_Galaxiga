using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataLevelBossMode
{
	public static void LoadData()
	{
		string text = CacheGame.DataLevelBossMode;
		if (text == string.Empty)
		{
			SaveDataLevelBossMode.InitData();
		}
		else
		{
			SaveDataLevelBossMode.dataLevelBossMode = JsonUtility.FromJson<DataLevelBossModeContainer>(text);
		}
	}

	public static void SaveData()
	{
		string text = JsonUtility.ToJson(SaveDataLevelBossMode.dataLevelBossMode, true);
		CacheGame.DataLevelBossMode = text;
	}

	public static void InitData()
	{
		SaveDataLevelBossMode.dataLevelBossMode = new DataLevelBossModeContainer();
		for (int i = 1; i <= 30; i++)
		{
			DataLevelBossMode item = new DataLevelBossMode(i, 0, false, false, false, false, false, false, 0, 0, 0);
			SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes.Add(item);
		}
		SaveDataLevelBossMode.SaveData();
	}

	public static void ResetData()
	{
		for (int i = 0; i < 30; i++)
		{
			DataLevelBossMode dataLevelBossMode = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[i];
			dataLevelBossMode.numberStars = 0;
			SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[i] = dataLevelBossMode;
		}
		SaveDataLevelBossMode.SaveData();
	}

	public static void SetDataLevel(DataLevelBossMode data, int level)
	{
		SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[level - 1] = data;
		SaveDataLevelBossMode.SaveData();
	}

	public static int NumberStars(int bossID)
	{
		return SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[bossID - 1].numberStars;
	}

	public static bool WasPassedBoss(int bossID, GameContext.ModeLevel modeLevel)
	{
		bool result = false;
		switch (modeLevel)
		{
		case GameContext.ModeLevel.Easy:
			result = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[bossID - 1].wasPassedBossEasy;
			break;
		case GameContext.ModeLevel.Normal:
			result = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[bossID - 1].wasPassedBossNormal;
			break;
		case GameContext.ModeLevel.Hard:
			result = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[bossID - 1].wasPassedBossHard;
			break;
		}
		return result;
	}

	public static bool WasCollectedRewardOneTime(int bossID, GameContext.ModeLevel modeLevel)
	{
		bool result = false;
		switch (modeLevel)
		{
		case GameContext.ModeLevel.Easy:
			result = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[bossID - 1].wasCollectedRewardOneTimeBossEasy;
			break;
		case GameContext.ModeLevel.Normal:
			result = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[bossID - 1].wasCollectedRewardOneTimeBossNormal;
			break;
		case GameContext.ModeLevel.Hard:
			result = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[bossID - 1].wasCollectedRewardOneTimeBossHard;
			break;
		}
		return result;
	}

	public static void SetPassLevel(int level, GameContext.ModeLevel modeLevel)
	{
		DataLevelBossMode dataLevelBossMode = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[level - 1];
		int num = dataLevelBossMode.numberStars;
		if (num < 3)
		{
			num++;
			dataLevelBossMode.numberStars = num;
		}
		if (modeLevel != GameContext.ModeLevel.Easy)
		{
			if (modeLevel != GameContext.ModeLevel.Normal)
			{
				if (modeLevel == GameContext.ModeLevel.Hard)
				{
					if (!dataLevelBossMode.wasPassedBossHard)
					{
						dataLevelBossMode.wasPassedBossHard = true;
					}
				}
			}
			else if (!dataLevelBossMode.wasPassedBossNormal)
			{
				dataLevelBossMode.wasPassedBossNormal = true;
			}
		}
		else if (!dataLevelBossMode.wasPassedBossEasy)
		{
			dataLevelBossMode.wasPassedBossEasy = true;
		}
		SaveDataLevelBossMode.SetDataLevel(dataLevelBossMode, level);
	}

	public static void SetCollectRewardOneTime(int level, GameContext.ModeLevel modeLevel)
	{
		DataLevelBossMode dataLevelBossMode = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[level - 1];
		switch (modeLevel)
		{
		case GameContext.ModeLevel.Easy:
			dataLevelBossMode.wasCollectedRewardOneTimeBossEasy = true;
			break;
		case GameContext.ModeLevel.Normal:
			dataLevelBossMode.wasCollectedRewardOneTimeBossNormal = true;
			break;
		case GameContext.ModeLevel.Hard:
			dataLevelBossMode.wasCollectedRewardOneTimeBossHard = true;
			break;
		}
		SaveDataLevelBossMode.SetDataLevel(dataLevelBossMode, level);
	}

	public static int ScoreLevel(int level, GameContext.ModeLevel modeLevel)
	{
		int result = 0;
		if (modeLevel != GameContext.ModeLevel.Easy)
		{
			if (modeLevel != GameContext.ModeLevel.Normal)
			{
				if (modeLevel == GameContext.ModeLevel.Hard)
				{
					result = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[level - 1].scoreModeHard;
				}
			}
			else
			{
				result = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[level - 1].scoreModeNormal;
			}
		}
		else
		{
			result = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[level - 1].scoreModeEasy;
		}
		return result;
	}

	public static void SetScoreLevel(GameContext.ModeLevel modeLevel, int level, int score)
	{
		DataLevelBossMode dataLevelBossMode = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[level - 1];
		if (modeLevel != GameContext.ModeLevel.Easy)
		{
			if (modeLevel != GameContext.ModeLevel.Normal)
			{
				if (modeLevel == GameContext.ModeLevel.Hard)
				{
					dataLevelBossMode.scoreModeHard = score;
				}
			}
			else
			{
				dataLevelBossMode.scoreModeNormal = score;
			}
		}
		else
		{
			dataLevelBossMode.scoreModeEasy = score;
		}
		SaveDataLevelBossMode.SetDataLevel(dataLevelBossMode, level);
	}

	public static void SetData(List<DataLevelBossMode> dataLevels)
	{
		try
		{
			int count = dataLevels.Count;
			for (int i = 0; i < dataLevels.Count; i++)
			{
				SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[i] = dataLevels[i];
			}
			SaveDataLevelBossMode.SaveData();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Player " + ex.Message);
		}
	}

	public static DataLevelBossModeContainer dataLevelBossMode;
}
