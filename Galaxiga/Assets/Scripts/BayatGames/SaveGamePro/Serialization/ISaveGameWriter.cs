using System;

namespace BayatGames.SaveGamePro.Serialization
{
	public interface ISaveGameWriter
	{
		void Write(object value);

		void WriteProperty<T>(string identifier, T value);

		void WriteProperty(string identifier, object value);

		void WriteSavableMembers(object obj, Type type);
	}
}
