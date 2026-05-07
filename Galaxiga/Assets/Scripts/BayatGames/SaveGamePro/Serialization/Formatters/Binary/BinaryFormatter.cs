using System;
using System.IO;

namespace BayatGames.SaveGamePro.Serialization.Formatters.Binary
{
	public class BinaryFormatter : ISaveGameFormatter
	{
		public BinaryFormatter() : this(SaveGame.DefaultSettings)
		{
		}

		public BinaryFormatter(SaveGameSettings settings)
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

		public virtual byte[] Serialize(object value)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.Serialize(memoryStream, value, this.Settings);
				result = memoryStream.ToArray();
			}
			return result;
		}

		public virtual void Serialize(Stream output, object value)
		{
			this.Serialize(output, value, this.Settings);
		}

		public virtual void Serialize(Stream output, object value, SaveGameSettings settings)
		{
			using (BinaryObjectWriter binaryObjectWriter = new BinaryObjectWriter(output, settings))
			{
				binaryObjectWriter.Write(value);
			}
		}

		public virtual T Deserialize<T>(Stream input)
		{
			return (T)((object)this.Deserialize(input, typeof(T), this.Settings));
		}

		public virtual T Deserialize<T>(Stream input, SaveGameSettings settings)
		{
			return (T)((object)this.Deserialize(input, typeof(T), settings));
		}

		public virtual T Deserialize<T>(byte[] buffer)
		{
			return (T)((object)this.Deserialize(buffer, typeof(T)));
		}

		public virtual object Deserialize(byte[] buffer, Type type)
		{
			object result;
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				result = this.Deserialize(memoryStream, type, this.Settings);
			}
			return result;
		}

		public virtual object Deserialize(Stream input, Type type)
		{
			return this.Deserialize(input, type, this.Settings);
		}

		public virtual object Deserialize(Stream input, Type type, SaveGameSettings settings)
		{
			input.Position = 0L;
			object result;
			using (BinaryObjectReader binaryObjectReader = new BinaryObjectReader(input, settings))
			{
				result = binaryObjectReader.Read(type);
			}
			return result;
		}

		public virtual void DeserializeInto(Stream input, object value)
		{
			this.DeserializeInto(input, value, this.Settings);
		}

		public virtual void DeserializeInto(Stream input, object value, SaveGameSettings settings)
		{
			input.Position = 0L;
			using (BinaryObjectReader binaryObjectReader = new BinaryObjectReader(input, settings))
			{
				((ISaveGameReader)binaryObjectReader).ReadInto(value);
			}
		}

		public virtual bool IsTypeSupported(Type type)
		{
			return true;
		}

		public static byte[] SerializeObject(object value)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter(SaveGame.DefaultSettings);
			return binaryFormatter.Serialize(value);
		}

		public static void SerializeObject(Stream output, object value)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter(SaveGame.DefaultSettings);
			binaryFormatter.Serialize(output, value);
		}

		public static object DeserializeObject(byte[] buffer, Type type)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter(SaveGame.DefaultSettings);
			return binaryFormatter.Deserialize(buffer, type);
		}

		public static object DeserializeObject(Stream input, Type type)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter(SaveGame.DefaultSettings);
			return binaryFormatter.Deserialize(input, type);
		}

		public const byte PropertyStart = 100;

		public const byte PropertyEnd = 101;

		public const byte SaveGameTypeStart = 102;

		public const byte SaveGameTypeEnd = 103;

		public const byte Terminator = 104;

		protected SaveGameSettings m_Settings;
	}
}
