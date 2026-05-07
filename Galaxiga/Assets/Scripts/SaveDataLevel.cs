using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataLevel
{
	public static void LoadData()
	{
		string dataLevel = CacheGame.DataLevel;
		if (dataLevel == string.Empty)
		{
			SaveDataLevel.InitDataLevel();
		}
		else
		{
			SaveDataLevel.dataLevelContainer = JsonUtility.FromJson<DataLevelContainer>(dataLevel);
		}
	}

	public static void SaveData(DataLevelContainer dataLevelContainer)
	{
		string text = JsonUtility.ToJson(dataLevelContainer, true);
		CacheGame.DataLevel = text;
		UnityEngine.Debug.Log(text);
	}

	public static void AddLevelData(DataLevel data)
	{
		if (!SaveDataLevel.dataLevelContainer.dataLevels.Contains(data))
		{
			SaveDataLevel.dataLevelContainer.dataLevels.Add(data);
		}
		else
		{
			UnityEngine.Debug.Log("exits " + data.level);
		}
	}

	public static void SetDataLevel(DataLevel data, int level)
	{
		if (level > SaveDataLevel.dataLevelContainer.dataLevels.Count)
		{
			SaveDataLevel.dataLevelContainer.dataLevels.Add(data);
		}
		else
		{
			SaveDataLevel.dataLevelContainer.dataLevels[level - 1] = data;
		}
		SaveDataLevel.SaveData(SaveDataLevel.dataLevelContainer);
	}

	public static int NumberStar(int level)
	{
		if (level > SaveDataLevel.dataLevelContainer.dataLevels.Count)
		{
			return 0;
		}
		return SaveDataLevel.dataLevelContainer.dataLevels[level - 1].numberStar;
	}

	public static int ScoreLevel(int level, GameContext.ModeLevel modeLevel)
	{
		int result = 0;
		if (level <= SaveDataLevel.dataLevelContainer.dataLevels.Count)
		{
			if (modeLevel != GameContext.ModeLevel.Easy)
			{
				if (modeLevel != GameContext.ModeLevel.Normal)
				{
					if (modeLevel == GameContext.ModeLevel.Hard)
					{
						result = SaveDataLevel.dataLevelContainer.dataLevels[level - 1].scoreModeHard;
					}
				}
				else
				{
					result = SaveDataLevel.dataLevelContainer.dataLevels[level - 1].scoreModeNormal;
				}
			}
			else
			{
				result = SaveDataLevel.dataLevelContainer.dataLevels[level - 1].scoreModeEasy;
			}
		}
		return result;
	}

	public static void SetScoreLevel(GameContext.ModeLevel modeLevel, int level, int score)
	{
		DataLevel dataLevel;
		if (level > SaveDataLevel.dataLevelContainer.dataLevels.Count)
		{
			dataLevel = new DataLevel(level, 0, 0, 0, 0);
		}
		else
		{
			dataLevel = SaveDataLevel.dataLevelContainer.dataLevels[level - 1];
		}
		if (modeLevel != GameContext.ModeLevel.Easy)
		{
			if (modeLevel != GameContext.ModeLevel.Normal)
			{
				if (modeLevel == GameContext.ModeLevel.Hard)
				{
					dataLevel.scoreModeHard = score;
				}
			}
			else
			{
				dataLevel.scoreModeNormal = score;
			}
		}
		else
		{
			dataLevel.scoreModeEasy = score;
		}
		UnityEngine.Debug.Log(dataLevel);
		SaveDataLevel.SetDataLevel(dataLevel, level);
	}

	public static void InitDataLevel()
	{
		DataLevel data = new DataLevel(1, 0, 0, 0, 0);
		SaveDataLevel.dataLevelContainer.dataLevels = new List<DataLevel>();
		SaveDataLevel.AddLevelData(data);
		SaveDataLevel.SaveData(SaveDataLevel.dataLevelContainer);
	}

	public static int TotalScoreAllLevel()
	{
		int num = 0;
		if (GameContext.maxLevelUnlocked > 1)
		{
			num = SaveDataLevel.dataLevelContainer.dataLevels[0].scoreModeEasy + SaveDataLevel.dataLevelContainer.dataLevels[0].scoreModeNormal + SaveDataLevel.dataLevelContainer.dataLevels[0].scoreModeHard;
		}
		else
		{
			for (int i = 0; i < GameContext.maxLevelUnlocked - 1; i++)
			{
				int num2 = SaveDataLevel.dataLevelContainer.dataLevels[i].scoreModeEasy + SaveDataLevel.dataLevelContainer.dataLevels[i].scoreModeNormal + SaveDataLevel.dataLevelContainer.dataLevels[i].scoreModeHard;
				num += num2;
			}
		}
		UnityEngine.Debug.Log("total score: " + num);
		return num;
	}

	public static int TotalStarAllLevel()
	{
		int num = 0;
		if (GameContext.maxLevelUnlocked == 1)
		{
			num = SaveDataLevel.dataLevelContainer.dataLevels[0].numberStar;
		}
		else
		{
			for (int i = 0; i < GameContext.maxLevelUnlocked - 1; i++)
			{
				num += SaveDataLevel.dataLevelContainer.dataLevels[i].numberStar;
			}
		}
		UnityEngine.Debug.Log("total score: " + num);
		return num;
	}

	public static void SetData(List<DataLevel> dataLevels)
	{
		try
		{
			int count = dataLevels.Count;
			SaveDataLevel.dataLevelContainer = new DataLevelContainer();
			for (int i = 0; i < count; i++)
			{
				SaveDataLevel.dataLevelContainer.dataLevels.Add(dataLevels[i]);
			}
			SaveDataLevel.SaveData(SaveDataLevel.dataLevelContainer);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Exception Player " + ex.Message);
		}
	}

	public static List<DataLevel> dataLevelsToSaved
	{
		get
		{
			if (SaveDataLevel.dataLevelContainer == null && SaveDataLevel.dataLevelContainer.dataLevels.Count <= 0)
			{
				SaveDataLevel.LoadData();
			}
			UnityEngine.Debug.Log("total data lelve to saved " + SaveDataLevel.dataLevelContainer.dataLevels.Count);
			return SaveDataLevel.dataLevelContainer.dataLevels;
		}
	}

	public static int GetStarInRange(int min, int max)
	{
		int num = 0;
		if (max < GameContext.maxLevelUnlocked && min > 0 && max > min)
		{
			for (int i = min - 1; i <= max - 1; i++)
			{
				num += SaveDataLevel.dataLevelContainer.dataLevels[i].numberStar;
			}
		}
		return num;
	}

	public static int MinLevelNotMaxStar()
	{
		for (int i = SaveDataLevel.minLevelNotMaxStar - 1; i < GameContext.maxLevelUnlocked; i++)
		{
			int numberStar = SaveDataLevel.dataLevelContainer.dataLevels[i].numberStar;
			if (numberStar < 3)
			{
				SaveDataLevel.minLevelNotMaxStar = i + 1;
				break;
			}
		}
		return SaveDataLevel.minLevelNotMaxStar;
	}

	public static DataLevelContainer dataLevelContainer = new DataLevelContainer();

	public static int stars = 0;

	public static int score = 0;

	private static int minLevelNotMaxStar = 1;
}
