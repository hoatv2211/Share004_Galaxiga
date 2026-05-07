using System;
using System.Linq;

namespace BayatGames.SaveGamePro.Utilities
{
	public static class StringUtils
	{
		public static string ToCamelCase(string titleCase)
		{
			return char.ToLowerInvariant(titleCase[0]) + titleCase.Substring(1);
		}

		public static string RandomString(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
			select s[StringUtils.random.Next(s.Length)]).ToArray<char>());
		}

		private static Random random = new Random();
	}
}
