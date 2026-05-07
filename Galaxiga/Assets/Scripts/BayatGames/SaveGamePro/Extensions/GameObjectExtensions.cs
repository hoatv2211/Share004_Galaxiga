using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Extensions
{
	public static class GameObjectExtensions
	{
		public static void DestroyChild(this GameObject gameObject, int index)
		{
			gameObject.transform.DestroyChild(index);
		}

		public static void DestroyChilds(this GameObject gameObject)
		{
			gameObject.transform.DestroyChilds();
		}
	}
}
