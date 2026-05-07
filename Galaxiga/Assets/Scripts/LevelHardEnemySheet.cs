using System;
using System.Collections.Generic;

[Serializable]
public class LevelHardEnemySheet
{
	public static LevelHardEnemySheet Get(int id)
	{
		return LevelHardEnemySheet.dictionary[id];
	}

	public static Dictionary<int, LevelHardEnemySheet> GetDictionary()
	{
		return LevelHardEnemySheet.dictionary;
	}

	public int id;

	public float percentHealth;

	public float percentSpeed;

	private static Dictionary<int, LevelHardEnemySheet> dictionary = new Dictionary<int, LevelHardEnemySheet>();
}
