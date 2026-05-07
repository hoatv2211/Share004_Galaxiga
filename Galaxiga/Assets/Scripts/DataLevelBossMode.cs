using System;

[Serializable]
public class DataLevelBossMode
{
	public DataLevelBossMode()
	{
	}

	public DataLevelBossMode(int _bossID, int _stars, bool _passedBossEasy, bool _passedBossNormal, bool _passedBossHard, bool _collectedRewardBossEasy, bool _collectedRewardBossNormal, bool _collectedRewardBossHard, int _scoreModeEasy, int _scoreModeNormal, int _scoreModeHard)
	{
		this.bossID = _bossID;
		this.numberStars = _stars;
		this.wasPassedBossEasy = _passedBossEasy;
		this.wasPassedBossNormal = _passedBossNormal;
		this.wasPassedBossHard = _passedBossHard;
		this.wasCollectedRewardOneTimeBossEasy = _collectedRewardBossEasy;
		this.wasCollectedRewardOneTimeBossNormal = _collectedRewardBossNormal;
		this.wasCollectedRewardOneTimeBossHard = _collectedRewardBossHard;
		this.scoreModeEasy = _scoreModeEasy;
		this.scoreModeNormal = _scoreModeNormal;
		this.scoreModeHard = _scoreModeHard;
	}

	public int bossID;

	public int numberStars;

	public bool wasPassedBossEasy;

	public bool wasPassedBossNormal;

	public bool wasPassedBossHard;

	public bool wasCollectedRewardOneTimeBossEasy;

	public bool wasCollectedRewardOneTimeBossNormal;

	public bool wasCollectedRewardOneTimeBossHard;

	public int scoreModeEasy;

	public int scoreModeNormal;

	public int scoreModeHard;
}
