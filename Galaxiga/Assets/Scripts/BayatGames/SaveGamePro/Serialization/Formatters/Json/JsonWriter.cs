using System;

namespace BayatGames.SaveGamePro.Serialization.Formatters.Json
{
	public abstract class JsonWriter : IDisposable, ISaveGameWriter
	{
		public JsonWriter(SaveGameSettings settings)
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

		public virtual void Write<T>(T value)
		{
			this.Write(value);
		}

		public abstract void Write(object value);

		public virtual void WriteProperty<T>(string identifier, T value)
		{
			this.WriteProperty(identifier, value);
		}

		public abstract void WriteProperty(string identifier, object value);

		public abstract void WriteSavableMembers(object obj, Type type);

		public abstract void Dispose();

		protected SaveGameSettings m_Settings;
	}
}
