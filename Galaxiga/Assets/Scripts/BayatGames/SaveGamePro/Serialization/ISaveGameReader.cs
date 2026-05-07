using System;
using System.Collections.Generic;

namespace BayatGames.SaveGamePro.Serialization
{
	public interface ISaveGameReader
	{
		IEnumerable<string> Properties { get; }

		T Read<T>();

		object Read(Type type);

		void ReadInto<T>(T value);

		void ReadInto(object value);

		T ReadProperty<T>();

		object ReadProperty(Type type);

		void ReadIntoProperty<T>(T value);

		void ReadIntoProperty(object value);

		void ReadSavableMembers(object obj, Type type);
	}
}
