using System;
using System.IO;

namespace BayatGames.SaveGamePro.Serialization
{
	public interface ISaveGameFormatter
	{
		void Serialize(Stream output, object value, SaveGameSettings settings);

		object Deserialize(Stream input, Type type, SaveGameSettings settings);

		void DeserializeInto(Stream input, object value, SaveGameSettings settings);

		bool IsTypeSupported(Type type);
	}
}
