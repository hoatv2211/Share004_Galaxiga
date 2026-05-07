using System;
using System.Collections.Generic;

[Serializable]
public class DataEnemy1Sheet
{
	public static DataEnemy1Sheet Get(string id)
	{
		return DataEnemy1Sheet.dictionary[id];
	}

	public static Dictionary<string, DataEnemy1Sheet> GetDictionary()
	{
		return DataEnemy1Sheet.dictionary;
	}

	public string id;

	public string nameEnemy;

	public int healthEnemy;

	public float speedBullet;

	public int scoreEnemy;

	private static Dictionary<string, DataEnemy1Sheet> dictionary = new Dictionary<string, DataEnemy1Sheet>();
}
