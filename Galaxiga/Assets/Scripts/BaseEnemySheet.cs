using System;
using System.Collections.Generic;

[Serializable]
public class BaseEnemySheet
{
	public static BaseEnemySheet Get(string id)
	{
		return BaseEnemySheet.dictionary[id];
	}

	public static Dictionary<string, BaseEnemySheet> GetDictionary()
	{
		return BaseEnemySheet.dictionary;
	}

	public string id;

	public int healthEnemy;

	public float speedBullet;

	public int scoreEnemy;

	private static Dictionary<string, BaseEnemySheet> dictionary = new Dictionary<string, BaseEnemySheet>();
}
