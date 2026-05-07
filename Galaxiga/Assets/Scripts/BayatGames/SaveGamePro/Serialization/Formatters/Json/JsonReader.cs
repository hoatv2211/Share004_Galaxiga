using System;
using System.Collections.Generic;

namespace BayatGames.SaveGamePro.Serialization.Formatters.Json
{
	public abstract class JsonReader : IDisposable, ISaveGameReader
	{
		public JsonReader(SaveGameSettings settings)
		{
			this.m_Settings = settings;
		}

		public virtual SaveGameSettings Settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
			}
		}

		public abstract IEnumerable<string> Properties { get; }

		public virtual T Read<T>()
		{
			return (T)((object)this.Read(typeof(T)));
		}

		public abstract object Read(Type type);

		public virtual void ReadInto<T>(T value)
		{
			this.ReadInto(value);
		}

		public abstract void ReadInto(object value);

		public virtual T ReadProperty<T>()
		{
			return (T)((object)this.ReadProperty(typeof(T)));
		}

		public abstract object ReadProperty(Type type);

		public virtual void ReadIntoProperty<T>(T value)
		{
			this.ReadIntoProperty(value);
		}

		public abstract void ReadIntoProperty(object value);

		public abstract void ReadSavableMembers(object obj, Type type);

		public abstract void ReadIntoSavableMembers(object obj, Type type);

		public abstract void Dispose();

		protected SaveGameSettings m_Settings;
	}
}
