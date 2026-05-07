using System;
using System.Collections.Generic;

[Serializable]
public class LevelDetailSheet
{
	public static LevelDetailSheet Get(int levelPlanet)
	{
		return LevelDetailSheet.dictionary[levelPlanet];
	}

	public static Dictionary<int, LevelDetailSheet> GetDictionary()
	{
		return LevelDetailSheet.dictionary;
	}

	public int levelPlanet;

	public float percentHealth;

	private static Dictionary<int, LevelDetailSheet> dictionary = new Dictionary<int, LevelDetailSheet>();
}
