using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Utilities
{
	public static class PathUtils
	{
		public static string GetAssetsRelativePath(string path)
		{
			if (path.StartsWith(Application.dataPath))
			{
				return "Assets" + path.Substring(Application.dataPath.Length);
			}
			return path;
		}
	}
}
