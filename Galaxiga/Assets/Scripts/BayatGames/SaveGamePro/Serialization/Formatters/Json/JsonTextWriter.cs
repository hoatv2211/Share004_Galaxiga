using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using BayatGames.SaveGamePro.Reflection;
using BayatGames.SaveGamePro.Serialization.Types;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Formatters.Json
{
	public class JsonTextWriter : JsonWriter
	{
		public JsonTextWriter(TextWriter writer, SaveGameSettings settings) : base(settings)
		{
			this.m_Writer = writer;
		}

		public virtual TextWriter Writer
		{
			get
			{
				return this.m_Writer;
			}
		}

		public override void Write(object value)
		{
			if (value == null)
			{
				this.m_Writer.Write("null");
			}
			else
			{
				Type type = value.GetType();
				bool isEnum = type.IsEnum;
				bool isSerializable = type.IsSerializable;
				bool isGenericType = type.IsGenericType;
				bool flag = type.GetInterfaces().Contains(typeof(ICollection<>));
				if (type == typeof(GameObject))
				{
					this.m_Writer.Write("{");
					GameObject gameObject = value as GameObject;
					this.m_IsFirstProperty = true;
					this.WriteProperty<int>("layer", gameObject.layer);
					this.WriteProperty<bool>("isStatic", gameObject.isStatic);
					this.WriteProperty<string>("tag", gameObject.tag);
					this.WriteProperty<string>("name", gameObject.name);
					this.WriteProperty<HideFlags>("hideFlags", gameObject.hideFlags);
					this.m_Writer.Write(",");
					this.Write<string>("components");
					this.m_Writer.Write(":");
					this.m_Writer.Write("[");
					Component[] components = gameObject.GetComponents<Component>();
					bool flag2 = true;
					for (int i = 0; i < components.Length; i++)
					{
						if (flag2)
						{
							flag2 = false;
						}
						else
						{
							this.m_Writer.Write(",");
						}
						this.m_Writer.Write("{");
						this.Write<string>("type");
						this.m_Writer.Write(":");
						this.Write<string>(components[i].GetType().AssemblyQualifiedName);
						this.m_Writer.Write(",");
						this.Write<string>("component");
						this.m_Writer.Write(":");
						this.Write<Component>(components[i]);
						this.m_Writer.Write("}");
					}
					this.m_Writer.Write("]");
					this.m_Writer.Write(",");
					this.Write<string>("childs");
					this.m_Writer.Write(":");
					List<GameObject> list = new List<GameObject>();
					for (int j = 0; j < gameObject.transform.childCount; j++)
					{
						list.Add(gameObject.transform.GetChild(j).gameObject);
					}
					this.Write<List<GameObject>>(list);
					this.m_Writer.Write("}");
				}
				else if (type == typeof(string) || isEnum)
				{
					this.m_Writer.Write("\"{0}\"", value.ToString().EscapeStringJson());
				}
				else if (type == typeof(bool))
				{
					this.m_Writer.Write(value.ToString().ToLower());
				}
				else if (type == typeof(short) || type == typeof(int) || type == typeof(long) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong) || type == typeof(byte) || type == typeof(sbyte) || type == typeof(decimal) || type == typeof(double) || type == typeof(float))
				{
					this.m_Writer.Write(Convert.ChangeType(Convert.ToDecimal(value), typeof(string)));
				}
				else if (type == typeof(DictionaryEntry))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)value;
					this.m_Writer.Write("{");
					this.Write<string>("KeyType");
					this.m_Writer.Write(":");
					this.Write<string>(dictionaryEntry.Key.GetType().AssemblyQualifiedName);
					this.m_Writer.Write(",");
					this.Write<string>("Key");
					this.m_Writer.Write(":");
					this.Write(dictionaryEntry.Key);
					this.m_Writer.Write(",");
					this.Write<string>("ValueType");
					this.m_Writer.Write(":");
					this.Write<string>(dictionaryEntry.Value.GetType().AssemblyQualifiedName);
					this.m_Writer.Write(",");
					this.Write<string>("Value");
					this.m_Writer.Write(":");
					this.Write(dictionaryEntry.Value);
					this.m_Writer.Write("}");
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<, >))
				{
					this.m_Writer.Write("{");
					this.Write<string>("Key");
					this.m_Writer.Write(":");
					this.Write(type.GetProperty("Key").GetValue(value, null));
					this.m_Writer.Write(",");
					this.Write<string>("Value");
					this.m_Writer.Write(":");
					this.Write(type.GetProperty("Value").GetValue(value, null));
					this.m_Writer.Write("}");
				}
				else if (value is Hashtable)
				{
					Hashtable hashtable = value as Hashtable;
					bool flag3 = true;
					this.m_Writer.Write("[");
					IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DictionaryEntry value2 = (DictionaryEntry)obj;
							if (flag3)
							{
								flag3 = false;
							}
							else
							{
								this.m_Writer.Write(",");
							}
							this.Write<DictionaryEntry>(value2);
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
					this.m_Writer.Write("]");
				}
				else if (value is IDictionary)
				{
					IDictionary dictionary = value as IDictionary;
					bool flag4 = true;
					this.m_Writer.Write("{");
					IEnumerator enumerator2 = dictionary.Keys.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							if (flag4)
							{
								flag4 = false;
							}
							else
							{
								this.m_Writer.Write(",");
							}
							this.Write(obj2);
							this.m_Writer.Write(":");
							this.Write(dictionary[obj2]);
						}
					}
					finally
					{
						IDisposable disposable2;
						if ((disposable2 = (enumerator2 as IDisposable)) != null)
						{
							disposable2.Dispose();
						}
					}
					this.m_Writer.Write("}");
				}
				else if (value is IEnumerable && (value is ICollection || flag))
				{
					IEnumerable enumerable = value as IEnumerable;
					IEnumerator enumerator3 = enumerable.GetEnumerator();
					bool flag5 = true;
					this.m_Writer.Write("[");
					while (enumerator3.MoveNext())
					{
						if (flag5)
						{
							flag5 = false;
						}
						else
						{
							this.m_Writer.Write(",");
						}
						this.Write(enumerator3.Current);
					}
					this.m_Writer.Write("]");
				}
				else
				{
					this.WriteObject(value, type);
				}
			}
		}

		public override void WriteProperty(string identifier, object value)
		{
			if (this.m_IsFirstProperty)
			{
				this.m_IsFirstProperty = false;
			}
			else
			{
				this.m_Writer.Write(",");
			}
			this.Write<string>(identifier);
			this.m_Writer.Write(":");
			this.Write(value);
		}

		protected virtual void WriteObject(object value, Type type)
		{
			if (value is ISavable)
			{
				this.m_Writer.Write("{");
				this.m_IsFirstProperty = true;
				ISavable savable = value as ISavable;
				savable.OnWrite(this);
				this.m_Writer.Write("}");
			}
			else if (SaveGameTypeManager.HasType(type))
			{
				this.m_Writer.Write("{");
				this.m_IsFirstProperty = true;
				SaveGameType type2 = SaveGameTypeManager.GetType(type);
				type2.Write(value, this);
				this.m_Writer.Write("}");
			}
			else if (value is ISerializable)
			{
				SerializationInfo info = new SerializationInfo(type, new FormatterConverter());
				ISerializable serializable = value as ISerializable;
				serializable.GetObjectData(info, new StreamingContext(StreamingContextStates.All));
				this.WriteSerializationInfo(info);
			}
			else
			{
				this.WriteSavableMembers(value, type);
			}
		}

		public override void WriteSavableMembers(object obj, Type type)
		{
			bool flag = true;
			this.m_Writer.Write("{");
			this.WriteSavableFields(obj, type, ref flag);
			this.WriteSavableProperties(obj, type, ref flag);
			this.m_Writer.Write("}");
		}

		public virtual void WriteSavableFields(object obj, Type type, ref bool isFirst)
		{
			List<FieldInfo> savableFields = type.GetSavableFields();
			for (int i = 0; i < savableFields.Count; i++)
			{
				if (isFirst)
				{
					isFirst = false;
				}
				else
				{
					this.m_Writer.Write(",");
				}
				this.Write<string>(savableFields[i].Name);
				this.m_Writer.Write(":");
				this.Write(savableFields[i].GetValue(obj));
			}
		}

		public virtual void WriteSavableProperties(object obj, Type type, ref bool isFirst)
		{
			List<PropertyInfo> savableProperties = type.GetSavableProperties();
			for (int i = 0; i < savableProperties.Count; i++)
			{
				if (isFirst)
				{
					isFirst = false;
				}
				else
				{
					this.m_Writer.Write(",");
				}
				this.Write<string>(savableProperties[i].Name);
				this.m_Writer.Write(":");
				this.Write(savableProperties[i].GetValue(obj, null));
			}
		}

		protected virtual void WriteSerializationInfo(SerializationInfo info)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			bool flag = true;
			this.m_Writer.Write("{");
			while (enumerator.MoveNext())
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.m_Writer.Write(",");
				}
				this.Write<string>(enumerator.Name);
				this.m_Writer.Write(":");
				this.Write(enumerator.Value);
			}
			this.m_Writer.Write("}");
		}

		public override void Dispose()
		{
			if (this.m_Writer != null)
			{
				this.m_Writer.Dispose();
			}
		}

		protected TextWriter m_Writer;

		protected bool m_IsFirstProperty;
	}
}
