using System;
using System.IO;

namespace BayatGames.SaveGamePro.Serialization.Formatters.Json
{
	public class JsonFormatter : ISaveGameFormatter
	{
		public JsonFormatter() : this(SaveGame.DefaultSettings)
		{
		}

		public JsonFormatter(SaveGameSettings settings)
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

		public string Serialize(object value)
		{
			StringWriter stringWriter = new StringWriter();
			this.Serialize(stringWriter, value, this.Settings);
			return stringWriter.ToString();
		}

		public void Serialize(Stream output, object value)
		{
			this.Serialize(output, value, this.Settings);
		}

		public void Serialize(Stream output, object value, SaveGameSettings settings)
		{
			this.Serialize(new StreamWriter(output, settings.Encoding), value, settings);
		}

		public void Serialize(TextWriter output, object value)
		{
			this.Serialize(output, value, this.Settings);
		}

		public void Serialize(TextWriter output, object value, SaveGameSettings settings)
		{
			using (JsonWriter jsonWriter = new JsonTextWriter(output, settings))
			{
				jsonWriter.Write(value);
			}
		}

		public T Deserialize<T>(string input)
		{
			return (T)((object)this.Deserialize(input, typeof(T)));
		}

		public T Deserialize<T>(Stream input)
		{
			return (T)((object)this.Deserialize(input, typeof(T), this.Settings));
		}

		public T Deserialize<T>(TextReader input)
		{
			return (T)((object)this.Deserialize(input, typeof(T), this.Settings));
		}

		public object Deserialize(string input, Type type)
		{
			return this.Deserialize(new StringReader(input), type, this.Settings);
		}

		public object Deserialize(Stream input, Type type)
		{
			return this.Deserialize(input, type, this.Settings);
		}

		public object Deserialize(Stream input, Type type, SaveGameSettings settings)
		{
			input.Position = 0L;
			return this.Deserialize(new StreamReader(input, settings.Encoding), type, settings);
		}

		public object Deserialize(TextReader input, Type type)
		{
			return this.Deserialize(input, type, this.Settings);
		}

		public object Deserialize(TextReader input, Type type, SaveGameSettings settings)
		{
			object result;
			using (JsonReader jsonReader = new JsonTextReader(input, settings))
			{
				result = jsonReader.Read(type);
			}
			return result;
		}

		public void DeserializeInto(Stream input, object value)
		{
			this.DeserializeInto(input, value, this.Settings);
		}

		public void DeserializeInto(Stream input, object value, SaveGameSettings settings)
		{
			input.Position = 0L;
			this.DeserializeInto(new StreamReader(input, settings.Encoding), value, settings);
		}

		public void DeserializeInto(TextReader input, object value)
		{
			this.DeserializeInto(input, value, this.Settings);
		}

		public void DeserializeInto(TextReader input, object value, SaveGameSettings settings)
		{
			using (JsonReader jsonReader = new JsonTextReader(input, settings))
			{
				jsonReader.ReadInto(value);
			}
		}

		public bool IsTypeSupported(Type type)
		{
			return !type.IsArray || type.GetArrayRank() <= 1;
		}

		public static string SerializeObject(object value)
		{
			JsonFormatter jsonFormatter = new JsonFormatter(SaveGame.DefaultSettings);
			return jsonFormatter.Serialize(value);
		}

		public static object DeserializeObject(string json, Type type)
		{
			JsonFormatter jsonFormatter = new JsonFormatter(SaveGame.DefaultSettings);
			return jsonFormatter.Deserialize(json, type);
		}

		protected SaveGameSettings m_Settings;
	}
}
