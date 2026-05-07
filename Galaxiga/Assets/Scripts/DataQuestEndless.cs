using System;

[Serializable]
public class DataQuestEndless
{
	public DataQuestEndless()
	{
	}

	public DataQuestEndless(int _questID, int _process, int _levelQuest, bool _isCollect)
	{
		this.questID = _questID;
		this.processQuest = _process;
		this.levelQuest = _levelQuest;
		this.isCollectReward = _isCollect;
	}

	public int questID;

	public int processQuest;

	public int levelQuest;

	public bool isCollectReward;
}
