using System;
using System.Collections.Generic;

[Serializable]
public class DailyGiftFirstMonthSheet
{
	public static DailyGiftFirstMonthSheet Get(int day)
	{
		return DailyGiftFirstMonthSheet.dictionary[day];
	}

	public static Dictionary<int, DailyGiftFirstMonthSheet> GetDictionary()
	{
		return DailyGiftFirstMonthSheet.dictionary;
	}

	public int day;

	public string gift;

	public int id;

	public int value;

	public int vip;

	private static Dictionary<int, DailyGiftFirstMonthSheet> dictionary = new Dictionary<int, DailyGiftFirstMonthSheet>();
}
