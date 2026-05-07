using System;
using System.Collections.Generic;

[Serializable]
public class WaveEnemySheet
{
	public static WaveEnemySheet Get(int numWave)
	{
		return WaveEnemySheet.dictionary[numWave];
	}

	public static Dictionary<int, WaveEnemySheet> GetDictionary()
	{
		return WaveEnemySheet.dictionary;
	}

	public int numWave;

	public float percentHeath;

	private static Dictionary<int, WaveEnemySheet> dictionary = new Dictionary<int, WaveEnemySheet>();
}
