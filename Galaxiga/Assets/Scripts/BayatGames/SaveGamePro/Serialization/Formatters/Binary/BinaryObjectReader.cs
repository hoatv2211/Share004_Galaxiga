using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using BayatGames.SaveGamePro.Reflection;
using BayatGames.SaveGamePro.Serialization.Types;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Formatters.Binary
{
	public class BinaryObjectReader : IDisposable, ISaveGameReader
	{
		public BinaryObjectReader(Stream stream, SaveGameSettings settings) : this(new BinaryReader(stream, settings.Encoding), settings)
		{
		}

		public BinaryObjectReader(BinaryReader reader, SaveGameSettings settings)
		{
			this.m_Reader = reader;
			this.m_Settings = settings;
		}

		public virtual BinaryReader Reader
		{
			get
			{
				return this.m_Reader;
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

		public virtual IEnumerable<string> Properties
		{
			get
			{
				return this.GetProperties();
			}
		}

		public virtual T Read<T>()
		{
			return (T)((object)this.Read(typeof(T)));
		}

		public virtual object Read(Type type)
		{
			Type left = null;
			object obj = null;
			if (type == null || !this.m_Reader.ReadBoolean())
			{
				obj = null;
			}
			else
			{
				if (Nullable.GetUnderlyingType(type) != null)
				{
					left = type;
					type = Nullable.GetUnderlyingType(type);
				}
				bool isPrimitive = type.IsPrimitive;
				bool isEnum = type.IsEnum;
				bool isSerializable = type.IsSerializable;
				bool isGenericType = type.IsGenericType;
				if (type == typeof(GameObject))
				{
					this.m_Reader.ReadByte();
					this.m_Reader.ReadInt64();
					int layer = this.ReadProperty<int>();
					bool isStatic = this.ReadProperty<bool>();
					string tag = this.ReadProperty<string>();
					string name = this.ReadProperty<string>();
					HideFlags hideFlags = this.ReadProperty<HideFlags>();
					this.m_Reader.ReadByte();
					GameObject gameObject = new GameObject(name)
					{
						layer = layer,
						isStatic = isStatic,
						tag = tag,
						hideFlags = hideFlags
					};
					this.m_Reader.ReadString();
					int num = this.m_Reader.ReadInt32();
					for (int i = 0; i < num; i++)
					{
						string typeName = this.m_Reader.ReadString();
						Type type2 = Type.GetType(typeName);
						Component value = gameObject.GetComponent(type2);
						if (type2 != typeof(Transform) && type2.BaseType != typeof(Transform))
						{
							Component component = gameObject.AddComponent(type2);
							if (component != null)
							{
								value = component;
							}
						}
						this.ReadInto<Component>(value);
					}
					this.m_Reader.ReadString();
					num = this.m_Reader.ReadInt32();
					for (int j = 0; j < num; j++)
					{
						this.ReadChild(gameObject);
					}
					obj = gameObject;
				}
				else if (isPrimitive || type == typeof(string) || type == typeof(decimal))
				{
					if (type == typeof(string))
					{
						obj = this.m_Reader.ReadString();
					}
					else if (type == typeof(decimal))
					{
						obj = this.m_Reader.ReadDecimal();
					}
					else if (type == typeof(short))
					{
						obj = this.m_Reader.ReadInt16();
					}
					else if (type == typeof(int))
					{
						obj = this.m_Reader.ReadInt32();
					}
					else if (type == typeof(long))
					{
						obj = this.m_Reader.ReadInt64();
					}
					else if (type == typeof(ushort))
					{
						obj = this.m_Reader.ReadUInt16();
					}
					else if (type == typeof(uint))
					{
						obj = this.m_Reader.ReadUInt32();
					}
					else if (type == typeof(ulong))
					{
						obj = this.m_Reader.ReadUInt64();
					}
					else if (type == typeof(double))
					{
						obj = this.m_Reader.ReadDouble();
					}
					else if (type == typeof(float))
					{
						obj = this.m_Reader.ReadSingle();
					}
					else if (type == typeof(byte))
					{
						obj = this.m_Reader.ReadByte();
					}
					else if (type == typeof(sbyte))
					{
						obj = this.m_Reader.ReadSByte();
					}
					else if (type == typeof(char))
					{
						obj = this.m_Reader.ReadChar();
					}
					else if (type == typeof(bool))
					{
						obj = this.m_Reader.ReadBoolean();
					}
				}
				else if (isEnum)
				{
					obj = Enum.Parse(type, this.m_Reader.ReadString());
				}
				else if (type == typeof(DateTime))
				{
					obj = DateTime.FromBinary(this.m_Reader.ReadInt64());
				}
				else if (type == typeof(TimeSpan))
				{
					obj = TimeSpan.Parse(this.m_Reader.ReadString());
				}
				else if (type.IsArray)
				{
					Type elementType = type.GetElementType();
					int num2 = this.m_Reader.ReadInt32();
					int[] array = new int[num2];
					for (int k = 0; k < num2; k++)
					{
						array[k] = this.m_Reader.ReadInt32();
					}
					Array array2 = Array.CreateInstance(elementType, array);
					int[] array3 = new int[array2.Rank];
					for (int l = 0; l < array2.Rank; l++)
					{
						array3[l] = array2.GetLowerBound(l);
					}
					array3[array2.Rank - 1]--;
					bool flag = false;
					while (!flag)
					{
						array3[array2.Rank - 1]++;
						for (int m = array2.Rank - 1; m >= 0; m--)
						{
							if (array3[m] > array2.GetUpperBound(m))
							{
								if (m == 0)
								{
									flag = true;
									break;
								}
								for (int n = m; n < array2.Rank; n++)
								{
									array3[n] = array2.GetLowerBound(n);
								}
								array3[m - 1]++;
							}
						}
						if (!flag)
						{
							array2.SetValue(this.Read(elementType), array3);
						}
					}
					obj = array2;
				}
				else if (type == typeof(DictionaryEntry))
				{
					DictionaryEntry dictionaryEntry = default(DictionaryEntry);
					Type type3 = Type.GetType(this.m_Reader.ReadString());
					dictionaryEntry.Key = this.Read(type3);
					Type type4 = Type.GetType(this.m_Reader.ReadString());
					dictionaryEntry.Value = this.Read(type4);
					obj = dictionaryEntry;
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<, >))
				{
					Type[] genericArguments = type.GetGenericArguments();
					obj = Activator.CreateInstance(type, new object[]
					{
						this.Read(genericArguments[0]),
						this.Read(genericArguments[1])
					});
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
				{
					Type[] genericArguments2 = type.GetGenericArguments();
					IList list = (IList)Activator.CreateInstance(type);
					int num3 = this.m_Reader.ReadInt32();
					for (int num4 = 0; num4 < num3; num4++)
					{
						list.Add(this.Read(genericArguments2[0]));
					}
					obj = list;
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(LinkedList<>))
				{
					Type[] genericArguments3 = type.GetGenericArguments();
					object obj2 = Activator.CreateInstance(type);
					MethodInfo method = type.GetMethod("AddLast", genericArguments3);
					int num5 = this.m_Reader.ReadInt32();
					for (int num6 = 0; num6 < num5; num6++)
					{
						method.Invoke(obj2, new object[]
						{
							this.Read(genericArguments3[0])
						});
					}
					obj = obj2;
				}
				else if (isGenericType && (type.GetGenericTypeDefinition() == typeof(Dictionary<, >) || type.GetGenericTypeDefinition() == typeof(SortedDictionary<, >) || type.GetGenericTypeDefinition() == typeof(SortedList<, >)))
				{
					Type[] genericArguments4 = type.GetGenericArguments();
					IDictionary dictionary = (IDictionary)Activator.CreateInstance(type);
					int num7 = this.m_Reader.ReadInt32();
					Type type5 = typeof(KeyValuePair<, >).MakeGenericType(genericArguments4);
					PropertyInfo property = type5.GetProperty("Key", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					PropertyInfo property2 = type5.GetProperty("Value", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					for (int num8 = 0; num8 < num7; num8++)
					{
						object obj3 = this.Read(type5);
						dictionary.Add(property.GetValue(obj3, null), property2.GetValue(obj3, null));
					}
					obj = dictionary;
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(Stack<>))
				{
					Type[] genericArguments5 = type.GetGenericArguments();
					object obj4 = Activator.CreateInstance(type);
					MethodInfo method2 = type.GetMethod("Push");
					int num9 = this.m_Reader.ReadInt32();
					for (int num10 = 0; num10 < num9; num10++)
					{
						method2.Invoke(obj4, new object[]
						{
							this.Read(genericArguments5[0])
						});
					}
					obj = obj4;
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(Queue<>))
				{
					Type[] genericArguments6 = type.GetGenericArguments();
					object obj5 = Activator.CreateInstance(type);
					MethodInfo method3 = type.GetMethod("Enqueue");
					int num11 = this.m_Reader.ReadInt32();
					for (int num12 = 0; num12 < num11; num12++)
					{
						method3.Invoke(obj5, new object[]
						{
							this.Read(genericArguments6[0])
						});
					}
					obj = obj5;
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(HashSet<>))
				{
					Type[] genericArguments7 = type.GetGenericArguments();
					object obj6 = Activator.CreateInstance(type);
					MethodInfo method4 = type.GetMethod("Add");
					int num13 = this.m_Reader.ReadInt32();
					for (int num14 = 0; num14 < num13; num14++)
					{
						method4.Invoke(obj6, new object[]
						{
							this.Read(genericArguments7[0])
						});
					}
					obj = obj6;
				}
				else if (type == typeof(Hashtable))
				{
					Hashtable hashtable = new Hashtable();
					int num15 = this.m_Reader.ReadInt32();
					for (int num16 = 0; num16 < num15; num16++)
					{
						DictionaryEntry dictionaryEntry2 = this.Read<DictionaryEntry>();
						hashtable.Add(dictionaryEntry2.Key, dictionaryEntry2.Value);
					}
					obj = hashtable;
				}
				else if (SaveGameTypeManager.HasType(type))
				{
					this.m_Reader.ReadByte();
					this.m_Reader.ReadInt64();
					SaveGameType type6 = SaveGameTypeManager.GetType(type);
					obj = type6.Read(this);
					this.m_Reader.ReadByte();
				}
				else
				{
					obj = this.ReadObject(type);
				}
			}
			if (obj is IDeserializationCallback)
			{
				(obj as IDeserializationCallback).OnDeserialization(this);
			}
			if (left != null)
			{
				Type nullableType = type.GetNullableType();
				obj = Activator.CreateInstance(nullableType, new object[]
				{
					obj
				});
			}
			return obj;
		}

		public virtual void ReadInto<T>(T value)
		{
			this.ReadInto(value);
		}

		public virtual void ReadInto(object value)
		{
			if (value == null || !this.m_Reader.ReadBoolean())
			{
				return;
			}
			Type type = value.GetType();
			bool isGenericType = type.IsGenericType;
			if (type == typeof(GameObject))
			{
				this.m_Reader.ReadByte();
				this.m_Reader.ReadInt64();
				int layer = this.ReadProperty<int>();
				bool isStatic = this.ReadProperty<bool>();
				string tag = this.ReadProperty<string>();
				string name = this.ReadProperty<string>();
				HideFlags hideFlags = this.ReadProperty<HideFlags>();
				this.m_Reader.ReadByte();
				GameObject gameObject = value as GameObject;
				gameObject.layer = layer;
				gameObject.isStatic = isStatic;
				gameObject.tag = tag;
				gameObject.name = name;
				gameObject.hideFlags = hideFlags;
				this.m_Reader.ReadString();
				int num = this.m_Reader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					string typeName = this.m_Reader.ReadString();
					Type type2 = Type.GetType(typeName);
					Component component = gameObject.GetComponent(type2);
					if (component == null)
					{
						component = gameObject.AddComponent(type2);
					}
					this.ReadInto<Component>(component);
				}
				this.m_Reader.ReadString();
				num = this.m_Reader.ReadInt32();
				for (int j = 0; j < num; j++)
				{
					if (gameObject.transform.childCount > j)
					{
						Transform child = gameObject.transform.GetChild(j);
						this.ReadInto<GameObject>(child.gameObject);
					}
					else
					{
						this.ReadChild(gameObject);
					}
				}
			}
			else if (type.IsArray)
			{
				Type elementType = type.GetElementType();
				int num2 = this.m_Reader.ReadInt32();
				int[] array = new int[num2];
				for (int k = 0; k < num2; k++)
				{
					array[k] = this.m_Reader.ReadInt32();
				}
				Array array2 = Array.CreateInstance(elementType, array);
				int[] array3 = new int[array2.Rank];
				for (int l = 0; l < array2.Rank; l++)
				{
					array3[l] = array2.GetLowerBound(l);
				}
				array3[array2.Rank - 1]--;
				bool flag = false;
				while (!flag)
				{
					array3[array2.Rank - 1]++;
					for (int m = array2.Rank - 1; m >= 0; m--)
					{
						if (array3[m] > array2.GetUpperBound(m))
						{
							if (m == 0)
							{
								flag = true;
								break;
							}
							for (int n = m; n < array2.Rank; n++)
							{
								array3[n] = array2.GetLowerBound(n);
							}
							array3[m - 1]++;
						}
					}
					if (!flag)
					{
						if (array2.GetValue(array3) == null)
						{
							array2.SetValue(this.Read(elementType), array3);
						}
						else
						{
							this.ReadInto(array2.GetValue(array3));
						}
					}
				}
			}
			else if (isGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
			{
				Type[] genericArguments = type.GetGenericArguments();
				IList list = value as IList;
				int num3 = this.m_Reader.ReadInt32();
				for (int num4 = 0; num4 < num3; num4++)
				{
					if (list.Count > num4 && list[num4] != null)
					{
						this.ReadInto(list[num4]);
					}
					else
					{
						list.Add(this.Read(genericArguments[0]));
					}
				}
			}
			else if (value is ICollection && value is IEnumerable)
			{
				this.m_Reader.ReadInt32();
				IEnumerable enumerable = value as IEnumerable;
				IEnumerator enumerator = enumerable.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object value2 = enumerator.Current;
						this.ReadInto(value2);
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
			}
			else if (SaveGameTypeManager.HasType(type))
			{
				this.m_Reader.ReadByte();
				this.m_Reader.ReadInt64();
				SaveGameType type3 = SaveGameTypeManager.GetType(type);
				type3.ReadInto(value, this);
				this.m_Reader.ReadByte();
			}
			else
			{
				this.ReadIntoObject(type, value);
			}
		}

		public virtual GameObject ReadChild(GameObject parent)
		{
			if (parent == null || !this.m_Reader.ReadBoolean())
			{
				return null;
			}
			this.m_Reader.ReadByte();
			this.m_Reader.ReadInt64();
			int layer = this.ReadProperty<int>();
			bool isStatic = this.ReadProperty<bool>();
			string tag = this.ReadProperty<string>();
			string name = this.ReadProperty<string>();
			HideFlags hideFlags = this.ReadProperty<HideFlags>();
			this.m_Reader.ReadByte();
			GameObject gameObject = new GameObject(name)
			{
				layer = layer,
				isStatic = isStatic,
				tag = tag,
				hideFlags = hideFlags
			};
			gameObject.transform.SetParent(parent.transform);
			this.m_Reader.ReadString();
			int num = this.m_Reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string typeName = this.m_Reader.ReadString();
				Type type = Type.GetType(typeName);
				Component value = gameObject.GetComponent(type);
				if (type != typeof(Transform) && type.BaseType != typeof(Transform))
				{
					Component component = gameObject.AddComponent(type);
					if (component != null)
					{
						value = component;
					}
				}
				this.ReadInto<Component>(value);
			}
			this.m_Reader.ReadString();
			num = this.m_Reader.ReadInt32();
			for (int j = 0; j < num; j++)
			{
				this.ReadChild(gameObject);
			}
			return gameObject;
		}

		public virtual T ReadProperty<T>()
		{
			return (T)((object)this.ReadProperty(typeof(T)));
		}

		public virtual object ReadProperty(Type type)
		{
			this.m_Reader.ReadByte();
			this.m_Reader.ReadString();
			this.m_Reader.ReadInt64();
			object result = this.Read(type);
			this.m_Reader.ReadByte();
			return result;
		}

		public virtual void ReadIntoProperty<T>(T value)
		{
			this.ReadIntoProperty(value);
		}

		public virtual void ReadIntoProperty(object value)
		{
			this.m_Reader.ReadByte();
			this.m_Reader.ReadString();
			this.m_Reader.ReadInt64();
			this.ReadInto(value);
			this.m_Reader.ReadByte();
		}

		protected virtual string[] GetProperties()
		{
            long position = m_Reader.BaseStream.Position;
            int num = 0;
            List<string> list = new List<string>();
            while (m_Reader.BaseStream.Position < m_Reader.BaseStream.Length)
            {
                switch (m_Reader.ReadByte())
                {
                    default:
                        continue;
                    case 100:
                        list.Add(SkipProperty());
                        continue;
                    case 102:
                        if (num > 0)
                        {
                            m_Reader.BaseStream.Position = m_Reader.ReadInt64();
                        }
                        num++;
                        continue;
                    case 103:
                        break;
                }
                if (num == 0)
                {
                    break;
                }
                num--;
            }
            m_Reader.BaseStream.Position = position;
            return list.ToArray();
        }

		protected virtual string SkipProperty()
		{
			string result = this.m_Reader.ReadString();
			this.m_Reader.BaseStream.Position = this.m_Reader.ReadInt64();
			this.m_Reader.ReadByte();
			return result;
		}

		protected virtual object ReadObject(Type type)
		{
			object result;
			if (type.IsSubclassOf<ScriptableObject>())
			{
				result = ScriptableObject.CreateInstance(type);
			}
			else if (type.IsValueType())
			{
				result = Activator.CreateInstance(type);
			}
			else
			{
				result = type.CreateInstance();
			}
			this.ReadObject(type, result);
			return result;
		}

		protected virtual void ReadObject(Type type, object result)
		{
			if (result != null)
			{
				if (result is ISavable)
				{
					ISavable savable = result as ISavable;
					savable.OnRead(this);
				}
				else if (result is ISerializable)
				{
					int num = this.m_Reader.ReadInt32();
					for (int i = 0; i < num; i++)
					{
						string name = this.m_Reader.ReadString();
						FieldInfo savableField = type.GetSavableField(name);
						if (savableField != null)
						{
							savableField.SetValue(result, this.Read(savableField.FieldType));
						}
						else
						{
							PropertyInfo savableProperty = type.GetSavableProperty(name);
							if (savableProperty != null)
							{
								savableProperty.SetValue(result, this.Read(savableProperty.PropertyType), null);
							}
						}
					}
				}
				else
				{
					this.ReadSavableMembers(result, type);
				}
			}
		}

		protected virtual void ReadIntoObject(Type type, object result)
		{
			if (result != null)
			{
				if (result is ISavable)
				{
					ISavable savable = result as ISavable;
					savable.OnRead(this);
				}
				else if (result is ISerializable)
				{
					int num = this.m_Reader.ReadInt32();
					for (int i = 0; i < num; i++)
					{
						string name = this.m_Reader.ReadString();
						FieldInfo savableField = type.GetSavableField(name);
						if (savableField != null)
						{
							object value = savableField.GetValue(result);
							this.ReadInto(value);
						}
						else
						{
							PropertyInfo savableProperty = type.GetSavableProperty(name);
							if (savableProperty != null)
							{
								object value2 = savableProperty.GetValue(result, null);
								this.ReadInto(value2);
							}
						}
					}
				}
				else
				{
					this.ReadIntoSavableMembers(result, type);
				}
			}
		}

		public virtual void ReadSavableMembers(object obj, Type type)
		{
			int num = this.m_Reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string name = this.m_Reader.ReadString();
				FieldInfo savableField = type.GetSavableField(name);
				if (savableField != null)
				{
					savableField.SetValue(obj, this.Read(savableField.FieldType));
				}
				else
				{
					PropertyInfo savableProperty = type.GetSavableProperty(name);
					if (savableProperty != null)
					{
						savableProperty.SetValue(obj, this.Read(savableProperty.PropertyType), null);
					}
				}
			}
			num = this.m_Reader.ReadInt32();
			for (int j = 0; j < num; j++)
			{
				string name2 = this.m_Reader.ReadString();
				FieldInfo savableField2 = type.GetSavableField(name2);
				if (savableField2 != null)
				{
					savableField2.SetValue(obj, this.Read(savableField2.FieldType));
				}
				else
				{
					PropertyInfo savableProperty2 = type.GetSavableProperty(name2);
					if (savableProperty2 != null)
					{
						savableProperty2.SetValue(obj, this.Read(savableProperty2.PropertyType), null);
					}
				}
			}
		}

		public virtual void ReadIntoSavableMembers(object obj, Type type)
		{
			int num = this.m_Reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string name = this.m_Reader.ReadString();
				FieldInfo savableField = type.GetSavableField(name);
				if (savableField != null)
				{
					object value = savableField.GetValue(obj);
					if (value == null)
					{
						savableField.SetValue(obj, this.Read(savableField.FieldType));
					}
					else
					{
						this.ReadInto(value);
					}
				}
				else
				{
					PropertyInfo savableProperty = type.GetSavableProperty(name);
					if (savableProperty != null)
					{
						object value2 = savableProperty.GetValue(obj, null);
						if (value2 == null)
						{
							savableProperty.SetValue(obj, this.Read(savableProperty.PropertyType), null);
						}
						else
						{
							this.ReadInto(value2);
						}
					}
				}
			}
			num = this.m_Reader.ReadInt32();
			for (int j = 0; j < num; j++)
			{
				string name2 = this.m_Reader.ReadString();
				FieldInfo savableField2 = type.GetSavableField(name2);
				if (savableField2 != null)
				{
					object value3 = savableField2.GetValue(obj);
					if (value3 == null)
					{
						savableField2.SetValue(obj, this.Read(savableField2.FieldType));
					}
					else
					{
						this.ReadInto(value3);
					}
				}
				else
				{
					PropertyInfo savableProperty2 = type.GetSavableProperty(name2);
					if (savableProperty2 != null)
					{
						object value4 = savableProperty2.GetValue(obj, null);
						if (value4 == null)
						{
							savableProperty2.SetValue(obj, this.Read(savableProperty2.PropertyType), null);
						}
						else
						{
							this.ReadInto(value4);
						}
					}
				}
			}
		}

		public virtual void Dispose()
		{
			if (this.m_Reader != null)
			{
				((IDisposable)this.m_Reader).Dispose();
			}
		}

		protected BinaryReader m_Reader;

		protected SaveGameSettings m_Settings;
	}
}
