using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Examples
{
	public class ItemBase : ScriptableObject
	{
		[NonSavable]
		public GameObject prefab;
	}
}
