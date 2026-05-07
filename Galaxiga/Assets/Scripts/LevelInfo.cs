using System;
using SkyGameKit;
using UnityEngine;

public class LevelInfo : SgkSingleton<LevelInfo>
{
	protected override void Awake()
	{
		switch (GameContext.currentUniverse)
		{
		case 0:
			this.numberWaveStart = 0;
			break;
		case 1:
			this.numberWaveStart = 4;
			break;
		case 2:
			this.numberWaveStart = 8;
			break;
		case 3:
			this.numberWaveStart = 12;
			break;
		}
		GalagaEndlessLevel.zigZagStartIndex = this.numberWaveStart;
		base.Awake();
		string[] array = base.name.Split(new char[]
		{
			'_'
		}, StringSplitOptions.RemoveEmptyEntries);
		int num = -1;
		if (array.Length > 2)
		{
			if (!int.TryParse(array[0], out num))
			{
				UnityEngine.Debug.LogError("Đặt tên level sai: " + base.name);
			}
			else
			{
				this.numPlanet = (num - 1) / 10 + 1;
			}
			if (!int.TryParse(array[1], out this.numHard))
			{
				UnityEngine.Debug.LogError("Đặt tên level sai: " + base.name);
			}
		}
		else
		{
			UnityEngine.Debug.LogError("Đặt tên level sai: " + base.name);
		}
	}

	public int numPlanet = 1;

	public int numHard = 1;

	public float percentHealthEnemy = 1f;

	public int totalcoin;

	public int totalcoinDefault = 1000;

	public int totalcoinadd;

	public float percentCoin;

	public LevelInfo.modeLevel mode;

	public int numberWaveStart;

	public int numberItemEvent;

	public enum modeLevel
	{
		Campain,
		EndLess,
		Boss
	}
}
