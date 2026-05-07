using System;
using UnityEngine;

namespace SkyGameKit
{
	public static class SgkLog
	{
		public static void ReflectionLog(string msg)
		{
			UnityEngine.Debug.Log("<color=#03A9F4>SkyGameKit(Reflection)  </color>" + msg);
		}

		public static void ReflectionLogError(string msg)
		{
			UnityEngine.Debug.LogError("<color=#03A9F4>SkyGameKit(Reflection)  </color>" + msg);
		}

		public static void Log(string msg)
		{
			UnityEngine.Debug.Log("<color=#03A9F4>SkyGameKit  </color>" + msg);
		}

		public static void LogWarning(string msg)
		{
			UnityEngine.Debug.LogWarning("<color=#03A9F4>SkyGameKit  </color>" + msg);
		}

		public static void LogError(string msg)
		{
			UnityEngine.Debug.LogError("<color=#03A9F4>SkyGameKit  </color>" + msg);
		}

		public const string textColor = "#03A9F4";
	}
}
