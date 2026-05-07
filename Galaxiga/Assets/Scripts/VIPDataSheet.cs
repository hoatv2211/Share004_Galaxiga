using System;
using System.Collections.Generic;

[Serializable]
public class VIPDataSheet
{
	public static VIPDataSheet Get(int vipID)
	{
		return VIPDataSheet.dictionary[vipID];
	}

	public static Dictionary<int, VIPDataSheet> GetDictionary()
	{
		return VIPDataSheet.dictionary;
	}

	public int vipID;

	public int vipPoint;

	public string descriptionVip;

	private static Dictionary<int, VIPDataSheet> dictionary = new Dictionary<int, VIPDataSheet>();
}
