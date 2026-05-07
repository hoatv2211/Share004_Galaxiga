using System;
using System.Collections.Generic;

[Serializable]
public class DailyQuestSheet
{
	public static DailyQuestSheet Get(int idQuest)
	{
		return DailyQuestSheet.dictionary[idQuest];
	}

	public static Dictionary<int, DailyQuestSheet> GetDictionary()
	{
		return DailyQuestSheet.dictionary;
	}

	public int idQuest;

	public string nameQuest;

	public string descriptionQuest;

	public int targetQuest;

	public string typeReward;

	public int valueRewardQuest;

	private static Dictionary<int, DailyQuestSheet> dictionary = new Dictionary<int, DailyQuestSheet>();
}
