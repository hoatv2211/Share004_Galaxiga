using System;
using UnityEngine;

namespace OneSoftCrossPromotion.Scripts.Utils
{
	public static class JsonHelper
	{
		public static T[] FromJson<T>(string json)
		{
			JsonHelper.Wrapper<T> wrapper = JsonUtility.FromJson<JsonHelper.Wrapper<T>>(json);
			return wrapper.Videos;
		}

		public static string ToJson<T>(T[] array)
		{
			return JsonUtility.ToJson(new JsonHelper.Wrapper<T>
			{
				Videos = array
			});
		}

		public static string ToJson<T>(T[] array, bool prettyPrint)
		{
			return JsonUtility.ToJson(new JsonHelper.Wrapper<T>
			{
				Videos = array
			}, prettyPrint);
		}

		[Serializable]
		private class Wrapper<T>
		{
			public T[] Videos;
		}
	}
}
