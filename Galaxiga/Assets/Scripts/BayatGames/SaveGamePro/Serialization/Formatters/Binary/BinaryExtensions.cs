using System;

namespace BayatGames.SaveGamePro.Serialization.Formatters.Binary
{
	public static class BinaryExtensions
	{
		public static byte[] ToBinary(this object value)
		{
			return BinaryFormatter.SerializeObject(value);
		}
	}
}
