using System;
using System.Collections.Generic;

[Serializable]
public class LevelEnemySheet
{
	public static LevelEnemySheet Get(int levelPlanet)
	{
		return LevelEnemySheet.dictionary[levelPlanet];
	}

	public static Dictionary<int, LevelEnemySheet> GetDictionary()
	{
		return LevelEnemySheet.dictionary;
	}

	public int levelPlanet;

	public float percentHealth;

	public float percentSpeed;

	private static Dictionary<int, LevelEnemySheet> dictionary = new Dictionary<int, LevelEnemySheet>();
}
