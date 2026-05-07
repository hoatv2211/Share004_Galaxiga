using System;
using System.Collections.Generic;

[Serializable]
public class DailyGiftMonthAfterSheet
{
	public static DailyGiftMonthAfterSheet Get(int day)
	{
		return DailyGiftMonthAfterSheet.dictionary[day];
	}

	public static Dictionary<int, DailyGiftMonthAfterSheet> GetDictionary()
	{
		return DailyGiftMonthAfterSheet.dictionary;
	}

	public int day;

	public string gift;

	public int id;

	public int value;

	public int vip;

	private static Dictionary<int, DailyGiftMonthAfterSheet> dictionary = new Dictionary<int, DailyGiftMonthAfterSheet>();
}
