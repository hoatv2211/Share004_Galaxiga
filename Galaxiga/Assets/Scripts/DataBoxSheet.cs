using System;
using System.Collections.Generic;

[Serializable]
public class DataBoxSheet
{
	public static DataBoxSheet Get(int idReward)
	{
		return DataBoxSheet.dictionary[idReward];
	}

	public static Dictionary<int, DataBoxSheet> GetDictionary()
	{
		return DataBoxSheet.dictionary;
	}

	public int idReward;

	public string nameReward;

	public float percent;

	public int value1;

	public float percentValue1;

	public int value2;

	public float percentValue2;

	public int value3;

	public float percentValue3;

	private static Dictionary<int, DataBoxSheet> dictionary = new Dictionary<int, DataBoxSheet>();
}
