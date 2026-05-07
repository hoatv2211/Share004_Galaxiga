using System;
using System.Collections.Generic;
using UnityEngine;

namespace BayatGames.SaveGamePro.Extensions
{
	public static class TransformExtensions
	{
		public static void DestroyChild(this Transform transform, int index)
		{
			UnityEngine.Object.Destroy(transform.GetChild(index).gameObject);
		}

		public static void DestroyChilds(this Transform transform)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < transform.childCount; i++)
			{
				list.Add(transform.GetChild(i).gameObject);
			}
			for (int j = 0; j < list.Count; j++)
			{
				UnityEngine.Object.Destroy(list[j]);
			}
		}
	}
}
