using System;

[Serializable]
public class DataQuest
{
	public DataQuest()
	{
	}

	public DataQuest(int _questID, int _process, bool _isCollect)
	{
		this.questID = _questID;
		this.processQuest = _process;
		this.isCollectReward = _isCollect;
	}

	public int questID;

	public int processQuest;

	public bool isCollectReward;
}
