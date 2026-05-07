using System;
using System.Collections.Generic;

[Serializable]
public class BaseEnemyDataSheet
{
	public static BaseEnemyDataSheet Get(string id)
	{
		return BaseEnemyDataSheet.dictionary[id];
	}

	public static Dictionary<string, BaseEnemyDataSheet> GetDictionary()
	{
		return BaseEnemyDataSheet.dictionary;
	}

	public string id;

	public int healthEnemy;

	public float speedBullet;

	public int scoreEnemy;

	private static Dictionary<string, BaseEnemyDataSheet> dictionary = new Dictionary<string, BaseEnemyDataSheet>();
}
