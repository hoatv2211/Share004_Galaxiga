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

namespace BayatGames.SaveGamePro.Serialization.Formatters.Binary
{
	public class BinaryObjectWriter : IDisposable, ISaveGameWriter
	{
		public BinaryObjectWriter(Stream stream, SaveGameSettings settings) : this(new BinaryWriter(stream, settings.Encoding), settings)
		{
		}

		public BinaryObjectWriter(BinaryWriter writer, SaveGameSettings settings)
		{
			this.m_Writer = writer;
			this.m_Settings = settings;
		}

		public virtual BinaryWriter Writer
		{
			get
			{
				return this.m_Writer;
			}
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

		public virtual void Write(object value)
		{
			this.m_Writer.Write(value != null);
			if (value == null)
			{
				return;
			}
			Type type = value.GetType();
			bool isPrimitive = type.IsPrimitive;
			bool isEnum = type.IsEnum;
			bool isSerializable = type.IsSerializable;
			bool isGenericType = type.IsGenericType;
			bool flag = type.GetInterfaces().Contains(typeof(ICollection<>));
			if (type == typeof(GameObject))
			{
				GameObject gameObject = value as GameObject;
				this.m_Writer.BaseStream.WriteByte(102);
				long position = this.m_Writer.BaseStream.Position;
				this.m_Writer.Write(0L);
				this.WriteProperty<int>("layer", gameObject.layer);
				this.WriteProperty<bool>("isStatic", gameObject.isStatic);
				this.WriteProperty<string>("tag", gameObject.tag);
				this.WriteProperty<string>("name", gameObject.name);
				this.WriteProperty<HideFlags>("hideFlags", gameObject.hideFlags);
				long position2 = this.m_Writer.BaseStream.Position;
				this.m_Writer.BaseStream.Position = position;
				this.m_Writer.Write(position2);
				this.m_Writer.BaseStream.Position = position2;
				this.m_Writer.BaseStream.WriteByte(103);
				Component[] components = gameObject.GetComponents<Component>();
				this.m_Writer.Write("components");
				this.m_Writer.Write(components.Length);
				for (int i = 0; i < components.Length; i++)
				{
					this.m_Writer.Write(components[i].GetType().AssemblyQualifiedName);
					this.Write<Component>(components[i]);
				}
				List<GameObject> list = new List<GameObject>();
				for (int j = 0; j < gameObject.transform.childCount; j++)
				{
					list.Add(gameObject.transform.GetChild(j).gameObject);
				}
				this.m_Writer.Write("childs");
				this.m_Writer.Write(list.Count);
				for (int k = 0; k < list.Count; k++)
				{
					this.Write<GameObject>(list[k]);
				}
			}
			else if (isPrimitive || type == typeof(string) || type == typeof(decimal))
			{
				if (type == typeof(string))
				{
					this.m_Writer.Write((string)value);
				}
				else if (type == typeof(decimal))
				{
					this.m_Writer.Write((decimal)value);
				}
				else if (type == typeof(short))
				{
					this.m_Writer.Write((short)value);
				}
				else if (type == typeof(int))
				{
					this.m_Writer.Write((int)value);
				}
				else if (type == typeof(long))
				{
					this.m_Writer.Write((long)value);
				}
				else if (type == typeof(ushort))
				{
					this.m_Writer.Write((ushort)value);
				}
				else if (type == typeof(uint))
				{
					this.m_Writer.Write((uint)value);
				}
				else if (type == typeof(ulong))
				{
					this.m_Writer.Write((ulong)value);
				}
				else if (type == typeof(double))
				{
					this.m_Writer.Write((double)value);
				}
				else if (type == typeof(float))
				{
					this.m_Writer.Write((float)value);
				}
				else if (type == typeof(byte))
				{
					this.m_Writer.Write((byte)value);
				}
				else if (type == typeof(sbyte))
				{
					this.m_Writer.Write((sbyte)value);
				}
				else if (type == typeof(char))
				{
					this.m_Writer.Write((char)value);
				}
				else if (type == typeof(bool))
				{
					this.m_Writer.Write((bool)value);
				}
			}
			else if (isEnum)
			{
				this.m_Writer.Write(value.ToString());
			}
			else if (type == typeof(DateTime))
			{
				this.m_Writer.Write(((DateTime)value).ToBinary());
			}
			else if (type == typeof(TimeSpan))
			{
				this.m_Writer.Write(((TimeSpan)value).ToString());
			}
			else if (type.IsArray)
			{
				Array array = value as Array;
				int[] array2 = new int[array.Rank];
				this.m_Writer.Write(array.Rank);
				for (int l = 0; l < array.Rank; l++)
				{
					this.m_Writer.Write(array.GetLength(l));
					array2[l] = array.GetLowerBound(l);
				}
				array2[array.Rank - 1]--;
				bool flag2 = false;
				while (!flag2)
				{
					array2[array.Rank - 1]++;
					for (int m = array.Rank - 1; m >= 0; m--)
					{
						if (array2[m] > array.GetUpperBound(m))
						{
							if (m == 0)
							{
								flag2 = true;
								break;
							}
							for (int n = m; n < array.Rank; n++)
							{
								array2[n] = array.GetLowerBound(n);
							}
							array2[m - 1]++;
						}
					}
					if (!flag2)
					{
						this.Write(array.GetValue(array2));
					}
				}
			}
			else if (value is IEnumerable && (value is ICollection || flag))
			{
				IEnumerable enumerable = value as IEnumerable;
				int value2 = (int)type.GetProperty("Count").GetValue(value, null);
				IEnumerator enumerator = enumerable.GetEnumerator();
				this.m_Writer.Write(value2);
				while (enumerator.MoveNext())
				{
					object value3 = enumerator.Current;
					this.Write(value3);
				}
			}
			else if (type == typeof(DictionaryEntry))
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)value;
				this.m_Writer.Write(dictionaryEntry.Key.GetType().AssemblyQualifiedName);
				this.Write(dictionaryEntry.Key);
				this.m_Writer.Write(dictionaryEntry.Value.GetType().AssemblyQualifiedName);
				this.Write(dictionaryEntry.Value);
			}
			else if (isGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<, >))
			{
				this.Write(type.GetProperty("Key").GetValue(value, null));
				this.Write(type.GetProperty("Value").GetValue(value, null));
			}
			else
			{
				this.WriteObject(value, type);
			}
		}

		public virtual void WriteProperty<T>(string identifier, T value)
		{
			this.WriteProperty(identifier, value);
		}

		public virtual void WriteProperty(string identifier, object value)
		{
			this.m_Writer.BaseStream.WriteByte(100);
			this.m_Writer.Write(identifier);
			long position = this.m_Writer.BaseStream.Position;
			this.m_Writer.Write(0L);
			this.Write(value);
			long position2 = this.m_Writer.BaseStream.Position;
			this.m_Writer.BaseStream.Position = position;
			this.m_Writer.Write(position2);
			this.m_Writer.BaseStream.Position = position2;
			this.m_Writer.BaseStream.WriteByte(101);
		}

		protected virtual void WriteObject(object value, Type type)
		{
			if (value is ISavable)
			{
				ISavable savable = value as ISavable;
				savable.OnWrite(this);
			}
			else if (SaveGameTypeManager.HasType(type))
			{
				this.m_Writer.BaseStream.WriteByte(102);
				long position = this.m_Writer.BaseStream.Position;
				this.m_Writer.Write(0L);
				SaveGameType type2 = SaveGameTypeManager.GetType(type);
				type2.Write(value, this);
				long position2 = this.m_Writer.BaseStream.Position;
				this.m_Writer.BaseStream.Position = position;
				this.m_Writer.Write(position2);
				this.m_Writer.BaseStream.Position = position2;
				this.m_Writer.BaseStream.WriteByte(103);
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

		public virtual void WriteSavableMembers(object value, Type type)
		{
			this.WriteSavableFields(value, type);
			this.WriteSavableProperties(value, type);
		}

		public virtual void WriteSavableFields(object value, Type type)
		{
			List<FieldInfo> savableFields = type.GetSavableFields();
			this.m_Writer.Write(savableFields.Count);
			for (int i = 0; i < savableFields.Count; i++)
			{
				this.m_Writer.Write(savableFields[i].Name);
				this.Write(savableFields[i].GetValue(value));
			}
		}

		public virtual void WriteSavableProperties(object value, Type type)
		{
			List<PropertyInfo> savableProperties = type.GetSavableProperties();
			this.m_Writer.Write(savableProperties.Count);
			for (int i = 0; i < savableProperties.Count; i++)
			{
				this.m_Writer.Write(savableProperties[i].Name);
				this.Write(savableProperties[i].GetValue(value, null));
			}
		}

		protected virtual void WriteSerializationInfo(SerializationInfo info)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			this.m_Writer.Write(info.MemberCount);
			while (enumerator.MoveNext())
			{
				this.m_Writer.Write(enumerator.Name);
				this.Write(enumerator.Value);
			}
		}

		public virtual void Dispose()
		{
			if (this.m_Writer != null)
			{
				((IDisposable)this.m_Writer).Dispose();
			}
		}

		protected BinaryWriter m_Writer;

		protected SaveGameSettings m_Settings;
	}
}
