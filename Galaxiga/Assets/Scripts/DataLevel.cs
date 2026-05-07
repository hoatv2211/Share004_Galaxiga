using System;

[Serializable]
public class DataLevel
{
	public DataLevel()
	{
	}

	public DataLevel(int _level, int _numberStar, int _scoreModeEasy, int _scoreModeNormal, int _scoreModeHard)
	{
		this.level = _level;
		this.numberStar = _numberStar;
		this.scoreModeEasy = _scoreModeEasy;
		this.scoreModeNormal = _scoreModeNormal;
		this.scoreModeHard = _scoreModeHard;
	}

	public int level = 1;

	public int numberStar;

	public int scoreModeEasy;

	public int scoreModeNormal;

	public int scoreModeHard;
}
