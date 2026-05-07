using System;
using UnityEngine;

namespace FalconSDK.CrossPromotion.Scripts.Utils
{
	public static class TagHelper
	{
		public static void AddTag(string tagName)
		{
		}

		public static bool IsTagExisted(string tagName)
		{
			return GameObject.FindGameObjectsWithTag(tagName).Length != 0;
		}
	}
}
