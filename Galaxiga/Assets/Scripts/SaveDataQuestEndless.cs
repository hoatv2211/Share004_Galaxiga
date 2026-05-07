using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataQuestEndless : MonoBehaviour
{
	public static void LoadData()
	{
		string questEndlessData = CacheGame.QuestEndlessData;
		if (questEndlessData == string.Empty)
		{
			SaveDataQuestEndless.InitDataQuest();
		}
		else
		{
			SaveDataQuestEndless.dataQuestEndlessContainer = JsonUtility.FromJson<DataQuestEndlessContainer>(questEndlessData);
			if (SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless.Count <= 0)
			{
				SaveDataQuestEndless.InitDataQuest();
			}
			UnityEngine.Debug.Log(questEndlessData);
		}
	}

	public static void SaveData(DataQuestEndlessContainer dataQuestEndlessContainer)
	{
		string questEndlessData = JsonUtility.ToJson(dataQuestEndlessContainer, true);
		CacheGame.QuestEndlessData = questEndlessData;
	}

	public static void AddDataQuest(DataQuestEndless data)
	{
		if (!SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless.Contains(data))
		{
			SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless.Add(data);
		}
	}

	public static void SetDataQuest(DataQuestEndless data, int idQuest)
	{
		SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless[idQuest] = data;
		SaveDataQuestEndless.SaveData(SaveDataQuestEndless.dataQuestEndlessContainer);
	}

	public static void InitDataQuest()
	{
		UnityEngine.Debug.Log("InitDataQuestEndless");
		for (int i = 0; i < 6; i++)
		{
			DataQuestEndless data = new DataQuestEndless(i, 0, 0, false);
			SaveDataQuestEndless.AddDataQuest(data);
		}
		SaveDataQuestEndless.SaveData(SaveDataQuestEndless.dataQuestEndlessContainer);
	}

	public static void ResetDataQuest()
	{
		SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless.Clear();
		SaveDataQuestEndless.InitDataQuest();
	}

	public static void SetProcessQuest(EndlessDailyQuestManager.Quest quest, int process)
	{
		int num = SaveDataQuestEndless.ProgressQuest(quest);
		num += process;
		if (SaveDataQuestEndless.LevelQuest(quest) == 0)
		{
			int num2 = QuestEndlessModeSheet.Get((int)(quest + 1)).target1;
		}
		else if (SaveDataQuestEndless.LevelQuest(quest) == 1)
		{
			int num2 = QuestEndlessModeSheet.Get((int)(quest + 1)).target2;
		}
		else if (SaveDataQuestEndless.LevelQuest(quest) == 2)
		{
			int num2 = QuestEndlessModeSheet.Get((int)(quest + 1)).target3;
		}
		if (EndlessDailyQuestManager.current != null)
		{
			EndlessDailyQuestManager.current.CheckQuest(quest, num);
		}
		if (num > QuestEndlessModeSheet.Get((int)(quest + 1)).target3)
		{
			num = QuestEndlessModeSheet.Get((int)(quest + 1)).target3;
		}
		DataQuestEndless dataQuestEndless = SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless[(int)quest];
		dataQuestEndless.processQuest = num;
		SaveDataQuestEndless.SetDataQuest(dataQuestEndless, (int)quest);
	}

	public static void SetLevelQuest(EndlessDailyQuestManager.Quest quest, int level)
	{
		int num = SaveDataQuestEndless.LevelQuest(quest);
		num += level;
		DataQuestEndless dataQuestEndless = SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless[(int)quest];
		dataQuestEndless.levelQuest = num;
		SaveDataQuestEndless.SetDataQuest(dataQuestEndless, (int)quest);
	}

	public static void CollectReward(EndlessDailyQuestManager.Quest quest)
	{
		DataQuestEndless dataQuestEndless = SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless[(int)quest];
		dataQuestEndless.isCollectReward = true;
		SaveDataQuestEndless.SetDataQuest(dataQuestEndless, (int)quest);
	}

	public static bool IsCollectReward(EndlessDailyQuestManager.Quest quest)
	{
		return SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless[(int)quest].isCollectReward;
	}

	public static int ProgressQuest(EndlessDailyQuestManager.Quest quest)
	{
		return SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless[(int)quest].processQuest;
	}

	public static int LevelQuest(EndlessDailyQuestManager.Quest quest)
	{
		return SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless[(int)quest].levelQuest;
	}

	public static void SetData(List<DataQuestEndless> dataQuestEndless)
	{
		SaveDataQuestEndless.dataQuestEndlessContainer = new DataQuestEndlessContainer();
		SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless = dataQuestEndless;
		SaveDataQuestEndless.SaveData(SaveDataQuestEndless.dataQuestEndlessContainer);
	}

	public static DataQuestEndlessContainer dataQuestEndlessContainer = new DataQuestEndlessContainer();
}
