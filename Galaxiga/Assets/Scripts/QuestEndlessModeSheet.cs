using System;
using System.Collections.Generic;

[Serializable]
public class QuestEndlessModeSheet
{
	public static QuestEndlessModeSheet Get(int idQuest)
	{
		return QuestEndlessModeSheet.dictionary[idQuest];
	}

	public static Dictionary<int, QuestEndlessModeSheet> GetDictionary()
	{
		return QuestEndlessModeSheet.dictionary;
	}

	public int idQuest;

	public string inforQuest;

	public int target1;

	public int target2;

	public int target3;

	public string reward1;

	public string reward2;

	public string reward3;

	public string idReward;

	private static Dictionary<int, QuestEndlessModeSheet> dictionary = new Dictionary<int, QuestEndlessModeSheet>();
}
