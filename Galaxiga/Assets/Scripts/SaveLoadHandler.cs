using System;
using BayatGames.SaveGamePro;

public class SaveLoadHandler
{
	public static void Save<T>(string key, T obj)
	{
		SaveGame.Save<T>(key, obj);
	}

	public static T Load<T>(string key)
	{
		T t = default(T);
		return SaveGame.Load<T>(key);
	}

	public static bool Exist(string key)
	{
		return SaveGame.Exists(key);
	}

	public static void DeleteKey(string key)
	{
		SaveGame.Delete(key);
	}
}
