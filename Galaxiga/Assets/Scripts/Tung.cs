using System;
using UnityEngine;

public class Tung
{
	public static void Log(string mess)
	{
		UnityEngine.Debug.Log(string.Format("<color=#03A9F4>-------------TUNG-------------</color>{0}", mess));
	}

	public static void LogError(string mess)
	{
		UnityEngine.Debug.LogError(string.Format("<color=#03A9F4>-------------TUNG-------------</color>{0}", mess));
	}
}
