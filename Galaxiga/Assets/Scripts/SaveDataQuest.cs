using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataQuest
{
	public static void LoadData()
	{
		string questData = CacheGame.QuestData;
		if (questData == string.Empty)
		{
			SaveDataQuest.InitDataQuest();
		}
		else
		{
			SaveDataQuest.dataQuestContainer = JsonUtility.FromJson<DataQuestContainer>(questData);
			if (SaveDataQuest.dataQuestContainer.dataQuests.Count <= 0)
			{
				SaveDataQuest.InitDataQuest();
			}
		}
	}

	public static void SaveData(DataQuestContainer dataQuestContainer)
	{
		string questData = JsonUtility.ToJson(dataQuestContainer, true);
		CacheGame.QuestData = questData;
	}

	public static void AddDataQuest(DataQuest data)
	{
		if (!SaveDataQuest.dataQuestContainer.dataQuests.Contains(data))
		{
			SaveDataQuest.dataQuestContainer.dataQuests.Add(data);
		}
	}

	public static void SetDataQuest(DataQuest data, int idQuest)
	{
		SaveDataQuest.dataQuestContainer.dataQuests[idQuest] = data;
		SaveDataQuest.SaveData(SaveDataQuest.dataQuestContainer);
	}

	public static void InitDataQuest()
	{
		for (int i = 0; i < 20; i++)
		{
			DataQuest data = new DataQuest(i, 0, false);
			SaveDataQuest.AddDataQuest(data);
		}
		SaveDataQuest.SaveData(SaveDataQuest.dataQuestContainer);
	}

	public static void ResetDataQuest()
	{
		SaveDataQuest.dataQuestContainer.dataQuests.Clear();
		SaveDataQuest.InitDataQuest();
	}

	public static void SetProcessQuest(DailyQuestManager.Quest quest, int process)
	{
		int num = SaveDataQuest.ProgressQuest(quest);
		num += process;
		int targetQuest = DailyQuestSheet.Get((int)quest).targetQuest;
		if (DailyQuestManager.Current != null)
		{
			DailyQuestManager.Current.CheckQuest(quest, num);
		}
		if (num > targetQuest)
		{
			return;
		}
		DataQuest dataQuest = SaveDataQuest.dataQuestContainer.dataQuests[(int)quest];
		dataQuest.processQuest = num;
		SaveDataQuest.SetDataQuest(dataQuest, (int)quest);
	}

	public static void CollectReward(DailyQuestManager.Quest quest)
	{
		DataQuest dataQuest = SaveDataQuest.dataQuestContainer.dataQuests[(int)quest];
		dataQuest.isCollectReward = true;
		SaveDataQuest.SetDataQuest(dataQuest, (int)quest);
	}

	public static bool IsCollectReward(DailyQuestManager.Quest quest)
	{
		return SaveDataQuest.dataQuestContainer.dataQuests[(int)quest].isCollectReward;
	}

	public static int ProgressQuest(DailyQuestManager.Quest quest)
	{
		return SaveDataQuest.dataQuestContainer.dataQuests[(int)quest].processQuest;
	}

	public static void SetData(List<DataQuest> dataQuests)
	{
		SaveDataQuest.dataQuestContainer = new DataQuestContainer();
		SaveDataQuest.dataQuestContainer.dataQuests = dataQuests;
		SaveDataQuest.SaveData(SaveDataQuest.dataQuestContainer);
	}

	public static DataQuestContainer dataQuestContainer = new DataQuestContainer();
}
