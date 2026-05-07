using System;
using System.Collections.Generic;

public static class DictExt
{
	public static void RemoveExt(this Dictionary<int, string> dic, int key)
	{
		dic.Remove(key);
		SaveLoadHandler.Save<Dictionary<int, string>>("key_what_changed", dic);
	}

	public static void AddExt(this Dictionary<int, string> dic, int key, string value)
	{
		dic.Add(key, value);
		SaveLoadHandler.Save<Dictionary<int, string>>("key_what_changed", dic);
	}

	public static void ClearExt(this Dictionary<int, string> dic)
	{
		dic.Clear();
		SaveLoadHandler.Save<Dictionary<int, string>>("key_what_changed", dic);
	}
}
