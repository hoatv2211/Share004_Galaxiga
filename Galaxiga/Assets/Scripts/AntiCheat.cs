using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class AntiCheat
{
	public static void SetInt(string key, int value, int fakeNumber = -95)
	{
		int value2 = value * fakeNumber + fakeNumber;
		PlayerPrefs.SetInt(key, value2);
		PlayerPrefs.SetString("CHECKSUM" + key, AntiCheat.md5(key, value2));
	}

	public static int GetInt(string key, int defaultValue, int fakeNumber = -95)
	{
		if (!PlayerPrefs.HasKey(key))
		{
			return defaultValue;
		}
		int @int = PlayerPrefs.GetInt(key);
		if (PlayerPrefs.HasKey("CHECKSUM" + key) && PlayerPrefs.GetString("CHECKSUM" + key).CompareTo(AntiCheat.md5(key, @int)) != 0)
		{
			return 0;
		}
		int num = (@int - fakeNumber) / fakeNumber;
		if (num < 0)
		{
			return defaultValue;
		}
		return num;
	}

	public static int GetIntConverted(string oldKey, string newKey, int defaultValue, int fakeNumber = -95)
	{
		if (!PlayerPrefs.HasKey(newKey))
		{
			return PlayerPrefs.GetInt(oldKey, defaultValue);
		}
		int @int = PlayerPrefs.GetInt(newKey);
		if (PlayerPrefs.HasKey("CHECKSUM" + newKey) && PlayerPrefs.GetString("CHECKSUM" + newKey).CompareTo(AntiCheat.md5(newKey, @int)) != 0)
		{
			return 0;
		}
		return (@int - fakeNumber) / fakeNumber;
	}

	public static void SetIntConverted(string newKey, int value, int fakeNumber = -95)
	{
		int value2 = value * fakeNumber + fakeNumber;
		PlayerPrefs.SetInt(newKey, value2);
		PlayerPrefs.SetString("CHECKSUM" + newKey, AntiCheat.md5(newKey, value2));
	}

	public static string GetString3Difficult(string difficult)
	{
		if (difficult != null)
		{
			if (difficult == "Normal")
			{
				return "Che_Do_De";
			}
			if (difficult == "Hard")
			{
				return "Che_Do_BinhThuong";
			}
			if (difficult == "Hell")
			{
				return "Che_Do_Kho";
			}
		}
		return "Che_Do_De";
	}

	public static string md5(string key, int value)
	{
		string s = key + "galaxiga_2018_Lrwa2WQV" + value;
		ASCIIEncoding asciiencoding = new ASCIIEncoding();
		byte[] bytes = asciiencoding.GetBytes(s);
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		return BitConverter.ToString(md5CryptoServiceProvider.ComputeHash(bytes));
	}

	public static string md5(string key, float value)
	{
		string s = key + "galaxiga_2018_Lrwa2WQV" + value;
		ASCIIEncoding asciiencoding = new ASCIIEncoding();
		byte[] bytes = asciiencoding.GetBytes(s);
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		return BitConverter.ToString(md5CryptoServiceProvider.ComputeHash(bytes));
	}

	public const string PRIVATE_KEY = "galaxiga_2018_Lrwa2WQV";

	public const string CHECKSUM_KEY = "CHECKSUM";
}
