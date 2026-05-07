using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using BayatGames.SaveGamePro.Reflection;
using BayatGames.SaveGamePro.Serialization.Types;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Formatters.Json
{
	public class JsonTextReader : JsonReader
	{
		public JsonTextReader(TextReader reader, SaveGameSettings settings) : base(settings)
		{
			this.m_Reader = reader;
			this.m_Position = 0;
			this.m_Json = this.m_Reader.ReadToEnd().RemoveWhitespaceJson();
		}

		public virtual TextReader Reader
		{
			get
			{
				return this.m_Reader;
			}
		}

		public override IEnumerable<string> Properties
		{
			get
			{
				return this.GetProperties();
			}
		}

		public override object Read(Type type)
		{
			Type left = null;
			object obj;
			if (type == null || string.IsNullOrEmpty(this.m_Json))
			{
				obj = null;
			}
			else if (this.m_Json[this.m_Position] == 'n' && this.PeekString() == "null")
			{
				this.ReadString();
				obj = null;
			}
			else
			{
				if (Nullable.GetUnderlyingType(type) != null)
				{
					left = type;
					type = Nullable.GetUnderlyingType(type);
				}
				bool isEnum = type.IsEnum;
				bool isSerializable = type.IsSerializable;
				bool isGenericType = type.IsGenericType;
				if (type == typeof(GameObject))
				{
					this.m_Position++;
					this.m_IsFirstProperty = true;
					int layer = 0;
					bool isStatic = false;
					string tag = string.Empty;
					string name = string.Empty;
					HideFlags hideFlags = HideFlags.None;
					foreach (string text in this.Properties)
					{
						if (text != null)
						{
							if (!(text == "layer"))
							{
								if (!(text == "isStatic"))
								{
									if (!(text == "tag"))
									{
										if (!(text == "name"))
										{
											if (text == "hideFlags")
											{
												hideFlags = this.ReadProperty<HideFlags>();
											}
										}
										else
										{
											name = this.ReadProperty<string>();
										}
									}
									else
									{
										tag = this.ReadProperty<string>();
									}
								}
								else
								{
									isStatic = this.ReadProperty<bool>();
								}
							}
							else
							{
								layer = this.ReadProperty<int>();
							}
						}
					}
					GameObject gameObject = new GameObject(name);
					gameObject.layer = layer;
					gameObject.isStatic = isStatic;
					gameObject.tag = tag;
					gameObject.name = name;
					gameObject.hideFlags = hideFlags;
					this.m_Position++;
					this.ReadQoutedString();
					this.m_Position += 2;
					int arrayLength = this.GetArrayLength();
					bool flag = true;
					for (int i = 0; i < arrayLength; i++)
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							this.m_Position++;
						}
						this.m_Position++;
						this.ReadQoutedString();
						this.m_Position++;
						string typeName = this.ReadQoutedString();
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
						this.m_Position++;
						this.ReadQoutedString();
						this.m_Position++;
						this.ReadInto<Component>(value);
						this.m_Position++;
					}
					this.m_Position += 2;
					this.ReadQoutedString();
					this.m_Position += 2;
					arrayLength = this.GetArrayLength();
					flag = true;
					for (int j = 0; j < arrayLength; j++)
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							this.m_Position++;
						}
						this.ReadChild(gameObject);
					}
					obj = gameObject;
					this.m_Position++;
				}
				else if (type == typeof(string))
				{
					obj = this.ReadQoutedString().UnEscapeStringJson();
				}
				else if (isEnum)
				{
					obj = Enum.Parse(type, this.ReadQoutedString().UnEscapeStringJson());
				}
				else if (type == typeof(bool))
				{
					obj = bool.Parse(this.ReadString());
				}
				else if (type == typeof(short) || type == typeof(int) || type == typeof(long) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong) || type == typeof(byte) || type == typeof(sbyte) || type == typeof(decimal) || type == typeof(double) || type == typeof(float))
				{
					obj = Convert.ChangeType(this.ReadString(), type);
				}
				else if (type.IsArray)
				{
					this.m_Position++;
					Type elementType = type.GetElementType();
					int arrayLength2 = this.GetArrayLength();
					Array array = Array.CreateInstance(elementType, arrayLength2);
					bool flag2 = true;
					for (int k = 0; k < arrayLength2; k++)
					{
						if (flag2)
						{
							flag2 = false;
						}
						else
						{
							this.m_Position++;
						}
						array.SetValue(this.Read(elementType), k);
					}
					obj = array;
					this.m_Position++;
				}
				else if (type == typeof(DictionaryEntry))
				{
					this.m_Position++;
					DictionaryEntry dictionaryEntry = default(DictionaryEntry);
					bool flag3 = true;
					Type type3 = null;
					Type type4 = null;
					for (int l = 0; l < 4; l++)
					{
						if (flag3)
						{
							flag3 = false;
						}
						else
						{
							this.m_Position++;
						}
						string a = this.ReadQoutedString();
						this.m_Position++;
						if (a == "KeyType")
						{
							type3 = Type.GetType(this.ReadQoutedString());
						}
						else if (a == "Key")
						{
							dictionaryEntry.Key = this.Read(type3);
						}
						else if (a == "ValueType")
						{
							type4 = Type.GetType(this.ReadQoutedString());
						}
						else if (a == "Value")
						{
							dictionaryEntry.Value = this.Read(type4);
						}
					}
					obj = dictionaryEntry;
					this.m_Position++;
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<, >))
				{
					this.m_Position++;
					Type[] genericArguments = type.GetGenericArguments();
					object obj2 = null;
					object obj3 = null;
					string a2 = this.ReadQoutedString();
					this.m_Position++;
					if (a2 == "Key")
					{
						obj2 = this.Read(genericArguments[0]);
					}
					else
					{
						obj3 = this.Read(genericArguments[1]);
					}
					this.m_Position++;
					a2 = this.ReadQoutedString();
					this.m_Position++;
					if (a2 == "Key")
					{
						obj2 = this.Read(genericArguments[0]);
					}
					else
					{
						obj3 = this.Read(genericArguments[1]);
					}
					obj = Activator.CreateInstance(type, new object[]
					{
						obj2,
						obj3
					});
					this.m_Position++;
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
				{
					this.m_Position++;
					Type[] genericArguments2 = type.GetGenericArguments();
					int arrayLength3 = this.GetArrayLength();
					IList list = (IList)Activator.CreateInstance(type);
					bool flag4 = true;
					for (int m = 0; m < arrayLength3; m++)
					{
						if (flag4)
						{
							flag4 = false;
						}
						else
						{
							this.m_Position++;
						}
						list.Add(this.Read(genericArguments2[0]));
					}
					obj = list;
					this.m_Position++;
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(LinkedList<>))
				{
					this.m_Position++;
					Type[] genericArguments3 = type.GetGenericArguments();
					int arrayLength4 = this.GetArrayLength();
					object obj4 = Activator.CreateInstance(type);
					MethodInfo method = type.GetMethod("AddLast", genericArguments3);
					bool flag5 = true;
					for (int n = 0; n < arrayLength4; n++)
					{
						if (flag5)
						{
							flag5 = false;
						}
						else
						{
							this.m_Position++;
						}
						method.Invoke(obj4, new object[]
						{
							this.Read(genericArguments3[0])
						});
					}
					obj = obj4;
					this.m_Position++;
				}
				else if (isGenericType && (type.GetGenericTypeDefinition() == typeof(Dictionary<, >) || type.GetGenericTypeDefinition() == typeof(SortedDictionary<, >) || type.GetGenericTypeDefinition() == typeof(SortedList<, >)))
				{
					this.m_Position++;
					Type[] genericArguments4 = type.GetGenericArguments();
					int objectLength = this.GetObjectLength();
					bool flag6 = true;
					IDictionary dictionary = (IDictionary)Activator.CreateInstance(type);
					for (int num = 0; num < objectLength; num++)
					{
						if (flag6)
						{
							flag6 = false;
						}
						else
						{
							this.m_Position++;
						}
						object key = this.Read(genericArguments4[0]);
						this.m_Position++;
						object value2 = this.Read(genericArguments4[1]);
						dictionary.Add(key, value2);
					}
					obj = dictionary;
					this.m_Position++;
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(Stack<>))
				{
					this.m_Position++;
					Type[] genericArguments5 = type.GetGenericArguments();
					int arrayLength5 = this.GetArrayLength();
					object obj5 = Activator.CreateInstance(type);
					MethodInfo method2 = type.GetMethod("Push");
					bool flag7 = true;
					for (int num2 = 0; num2 < arrayLength5; num2++)
					{
						if (flag7)
						{
							flag7 = false;
						}
						else
						{
							this.m_Position++;
						}
						method2.Invoke(obj5, new object[]
						{
							this.Read(genericArguments5[0])
						});
					}
					obj = obj5;
					this.m_Position++;
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(Queue<>))
				{
					this.m_Position++;
					Type[] genericArguments6 = type.GetGenericArguments();
					int arrayLength6 = this.GetArrayLength();
					object obj6 = Activator.CreateInstance(type);
					MethodInfo method3 = type.GetMethod("Enqueue");
					bool flag8 = true;
					for (int num3 = 0; num3 < arrayLength6; num3++)
					{
						if (flag8)
						{
							flag8 = false;
						}
						else
						{
							this.m_Position++;
						}
						method3.Invoke(obj6, new object[]
						{
							this.Read(genericArguments6[0])
						});
					}
					obj = obj6;
					this.m_Position++;
				}
				else if (isGenericType && type.GetGenericTypeDefinition() == typeof(HashSet<>))
				{
					this.m_Position++;
					Type[] genericArguments7 = type.GetGenericArguments();
					int arrayLength7 = this.GetArrayLength();
					object obj7 = Activator.CreateInstance(type);
					MethodInfo method4 = type.GetMethod("Add");
					bool flag9 = true;
					for (int num4 = 0; num4 < arrayLength7; num4++)
					{
						if (flag9)
						{
							flag9 = false;
						}
						else
						{
							this.m_Position++;
						}
						method4.Invoke(obj7, new object[]
						{
							this.Read(genericArguments7[0])
						});
					}
					obj = obj7;
					this.m_Position++;
				}
				else if (type == typeof(Hashtable))
				{
					this.m_Position++;
					bool flag10 = true;
					Hashtable hashtable = new Hashtable();
					int arrayLength8 = this.GetArrayLength();
					for (int num5 = 0; num5 < arrayLength8; num5++)
					{
						if (flag10)
						{
							flag10 = false;
						}
						else
						{
							this.m_Position++;
						}
						DictionaryEntry dictionaryEntry2 = this.Read<DictionaryEntry>();
						hashtable.Add(dictionaryEntry2.Key, dictionaryEntry2.Value);
					}
					obj = hashtable;
					this.m_Position++;
				}
				else if (SaveGameTypeManager.HasType(type))
				{
					this.m_Position++;
					this.m_IsFirstProperty = true;
					SaveGameType type5 = SaveGameTypeManager.GetType(type);
					obj = type5.Read(this);
					this.m_Position++;
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

		public override void ReadInto(object value)
		{
			if (value == null || string.IsNullOrEmpty(this.m_Json))
			{
				return;
			}
			if (this.m_Json[this.m_Position] == 'n' && this.PeekString() == "null")
			{
				this.ReadString();
				return;
			}
			Type type = value.GetType();
			bool isGenericType = type.IsGenericType;
			if (type == typeof(GameObject))
			{
				this.m_Position++;
				this.m_IsFirstProperty = true;
				int layer = 0;
				bool isStatic = false;
				string tag = string.Empty;
				string name = string.Empty;
				HideFlags hideFlags = HideFlags.None;
				foreach (string text in this.Properties)
				{
					if (text != null)
					{
						if (!(text == "layer"))
						{
							if (!(text == "isStatic"))
							{
								if (!(text == "tag"))
								{
									if (!(text == "name"))
									{
										if (text == "hideFlags")
										{
											hideFlags = this.ReadProperty<HideFlags>();
										}
									}
									else
									{
										name = this.ReadProperty<string>();
									}
								}
								else
								{
									tag = this.ReadProperty<string>();
								}
							}
							else
							{
								isStatic = this.ReadProperty<bool>();
							}
						}
						else
						{
							layer = this.ReadProperty<int>();
						}
					}
				}
				GameObject gameObject = value as GameObject;
				gameObject.layer = layer;
				gameObject.isStatic = isStatic;
				gameObject.tag = tag;
				gameObject.name = name;
				gameObject.hideFlags = hideFlags;
				this.m_Position++;
				this.ReadQoutedString();
				this.m_Position += 2;
				int arrayLength = this.GetArrayLength();
				bool flag = true;
				for (int i = 0; i < arrayLength; i++)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						this.m_Position++;
					}
					this.m_Position++;
					this.ReadQoutedString();
					this.m_Position++;
					string typeName = this.ReadQoutedString();
					Type type2 = Type.GetType(typeName);
					Component component = gameObject.GetComponent(type2);
					if (component == null)
					{
						component = gameObject.AddComponent(type2);
					}
					this.m_Position++;
					this.ReadQoutedString();
					this.m_Position++;
					this.ReadInto<Component>(component);
					this.m_Position++;
				}
				this.m_Position += 2;
				this.ReadQoutedString();
				this.m_Position += 2;
				arrayLength = this.GetArrayLength();
				flag = true;
				for (int j = 0; j < arrayLength; j++)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						this.m_Position++;
					}
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
				this.m_Position++;
			}
			else if (type.IsArray)
			{
				this.m_Position++;
				Type elementType = type.GetElementType();
				int arrayLength2 = this.GetArrayLength();
				Array array = value as Array;
				if (array.Length < arrayLength2)
				{
					array = Array.CreateInstance(elementType, arrayLength2);
				}
				bool flag2 = true;
				for (int k = 0; k < array.Length; k++)
				{
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						this.m_Position++;
					}
					if (array.GetValue(k) == null)
					{
						array.SetValue(this.Read(elementType), k);
					}
					else
					{
						this.ReadInto(array.GetValue(k));
					}
				}
				this.m_Position++;
			}
			else if (isGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
			{
				this.m_Position++;
				Type[] genericArguments = type.GetGenericArguments();
				int arrayLength3 = this.GetArrayLength();
				IList list = value as IList;
				bool flag3 = true;
				for (int l = 0; l < arrayLength3; l++)
				{
					if (flag3)
					{
						flag3 = false;
					}
					else
					{
						this.m_Position++;
					}
					if (list.Count > l && list[l] != null)
					{
						this.ReadInto(list[l]);
					}
					else
					{
						list.Add(this.Read(genericArguments[0]));
					}
				}
				this.m_Position++;
			}
			else if (value is ICollection && value is IEnumerable)
			{
				this.m_Position++;
				IEnumerable enumerable = value as IEnumerable;
				bool flag4 = true;
				IEnumerator enumerator2 = enumerable.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						object value2 = enumerator2.Current;
						if (flag4)
						{
							flag4 = false;
						}
						else
						{
							this.m_Position++;
						}
						this.ReadInto(value2);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator2 as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				this.m_Position++;
			}
			else if (SaveGameTypeManager.HasType(type))
			{
				this.m_Position++;
				this.m_IsFirstProperty = true;
				SaveGameType type3 = SaveGameTypeManager.GetType(type);
				type3.ReadInto(value, this);
				this.m_Position++;
			}
			else
			{
				this.ReadIntoObject(type, value);
			}
		}

		public virtual GameObject ReadChild(GameObject parent)
		{
			if (parent == null || string.IsNullOrEmpty(this.m_Json))
			{
				return null;
			}
			if (this.m_Json[this.m_Position] == 'n' && this.PeekString() == "null")
			{
				this.ReadString();
				return null;
			}
			this.m_Position++;
			this.m_IsFirstProperty = true;
			int layer = 0;
			bool isStatic = false;
			string tag = string.Empty;
			string name = string.Empty;
			HideFlags hideFlags = HideFlags.None;
			foreach (string text in this.Properties)
			{
				if (text != null)
				{
					if (!(text == "layer"))
					{
						if (!(text == "isStatic"))
						{
							if (!(text == "tag"))
							{
								if (!(text == "name"))
								{
									if (text == "hideFlags")
									{
										hideFlags = this.ReadProperty<HideFlags>();
									}
								}
								else
								{
									name = this.ReadProperty<string>();
								}
							}
							else
							{
								tag = this.ReadProperty<string>();
							}
						}
						else
						{
							isStatic = this.ReadProperty<bool>();
						}
					}
					else
					{
						layer = this.ReadProperty<int>();
					}
				}
			}
			GameObject gameObject = new GameObject(name);
			gameObject.layer = layer;
			gameObject.isStatic = isStatic;
			gameObject.tag = tag;
			gameObject.name = name;
			gameObject.hideFlags = hideFlags;
			gameObject.transform.SetParent(parent.transform);
			this.m_Position++;
			this.ReadQoutedString();
			this.m_Position += 2;
			int arrayLength = this.GetArrayLength();
			bool flag = true;
			for (int i = 0; i < arrayLength; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.m_Position++;
				}
				this.m_Position++;
				this.ReadQoutedString();
				this.m_Position++;
				string typeName = this.ReadQoutedString();
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
				this.m_Position++;
				this.ReadQoutedString();
				this.m_Position++;
				this.ReadInto<Component>(value);
				this.m_Position++;
			}
			this.m_Position += 2;
			this.ReadQoutedString();
			this.m_Position += 2;
			arrayLength = this.GetArrayLength();
			flag = true;
			for (int j = 0; j < arrayLength; j++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.m_Position++;
				}
				this.ReadChild(gameObject);
			}
			this.m_Position++;
			return gameObject;
		}

		public override object ReadProperty(Type type)
		{
			if (this.m_IsFirstProperty)
			{
				this.m_IsFirstProperty = false;
			}
			else
			{
				this.m_Position++;
			}
			this.ReadQoutedString();
			this.m_Position++;
			return this.Read(type);
		}

		public override void ReadIntoProperty(object value)
		{
			if (this.m_IsFirstProperty)
			{
				this.m_IsFirstProperty = false;
			}
			else
			{
				this.m_Position++;
			}
			this.ReadQoutedString();
			this.m_Position++;
			this.ReadInto(value);
		}

		protected virtual string[] GetProperties()
		{
			if (this.m_Json[this.m_Position] == '{' || this.m_Json[this.m_Position] == ',')
			{
				this.m_Position++;
			}
			List<string> list = new List<string>();
			if (this.m_Json[this.m_Position] == '}')
			{
				return list.ToArray();
			}
			int position = this.m_Position;
			int num = 0;
			list.Add(this.ReadQoutedString());
			if (this.m_Json[this.m_Position] != ']')
			{
				while (this.m_Json.Length > this.m_Position)
				{
					char c = this.m_Json[this.m_Position];
					switch (c)
					{
					case '[':
						goto IL_DE;
					default:
						switch (c)
						{
						case '{':
							goto IL_DE;
						default:
							if (c == ',')
							{
								if (num == 0)
								{
									this.m_Position++;
									list.Add(this.ReadQoutedString());
									continue;
								}
							}
							break;
						case '}':
							goto IL_E7;
						}
						break;
					case ']':
						goto IL_E7;
					}
					IL_12E:
					this.m_Position++;
					continue;
					IL_DE:
					num++;
					goto IL_12E;
					IL_E7:
					if (num == 0)
					{
						this.m_Position = position;
						return list.ToArray();
					}
					num--;
					goto IL_12E;
				}
				this.m_Position = position;
			}
			return list.ToArray();
		}

		protected virtual int GetObjectLength()
		{
			if (this.m_Json[this.m_Position] == '{')
			{
				this.m_Position++;
			}
			int num = 0;
			if (this.m_Json[this.m_Position] == '}')
			{
				return num;
			}
			int num2 = 0;
			int num3 = this.m_Position;
			if (this.m_Json[this.m_Position] != ']')
			{
				while (this.m_Json.Length > num3)
				{
					char c = this.m_Json[num3];
					switch (c)
					{
					case '[':
						goto IL_AC;
					default:
						switch (c)
						{
						case '{':
							goto IL_AC;
						default:
							if (c == ':')
							{
								if (num2 == 0)
								{
									num++;
								}
							}
							break;
						case '}':
							goto IL_B5;
						}
						break;
					case ']':
						goto IL_B5;
					}
					IL_D5:
					num3++;
					continue;
					IL_AC:
					num2++;
					goto IL_D5;
					IL_B5:
					if (num2 == 0)
					{
						return num;
					}
					num2--;
					goto IL_D5;
				}
			}
			return num;
		}

		protected virtual int GetArrayLength()
		{
			if (this.m_Json[this.m_Position] == '[')
			{
				this.m_Position++;
			}
			int num = 0;
			if (this.m_Json[this.m_Position] == ']')
			{
				return num;
			}
			int num2 = 0;
			int num3 = this.m_Position;
			if (this.m_Json[this.m_Position] != ']')
			{
				while (this.m_Json.Length > num3)
				{
					char c = this.m_Json[num3];
					switch (c)
					{
					case '[':
						goto IL_AC;
					default:
						switch (c)
						{
						case '{':
							goto IL_AC;
						default:
							if (c == ',')
							{
								if (num2 == 0)
								{
									num++;
								}
							}
							break;
						case '}':
							goto IL_B5;
						}
						break;
					case ']':
						goto IL_B5;
					}
					IL_D9:
					num3++;
					continue;
					IL_AC:
					num2++;
					goto IL_D9;
					IL_B5:
					if (num2 == 0)
					{
						return num + 1;
					}
					num2--;
					goto IL_D9;
				}
			}
			return num;
		}

		protected virtual string PeekString()
		{
			int position = this.m_Position;
			string result = this.ReadString();
			this.m_Position = position;
			return result;
		}

		protected virtual string ReadString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (this.m_Json.Length > this.m_Position)
			{
				char c = this.m_Json[this.m_Position];
				if (c == ',' || c == ']' || c == '}')
				{
					return stringBuilder.ToString();
				}
				stringBuilder.Append(this.m_Json[this.m_Position]);
				this.m_Position++;
			}
			return stringBuilder.ToString();
		}

		protected virtual string ReadQoutedString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.m_Position++;
			while (this.m_Json.Length > this.m_Position)
			{
				char c = this.m_Json[this.m_Position];
				if (c == '"')
				{
					this.m_Position++;
					return stringBuilder.ToString();
				}
				stringBuilder.Append(this.m_Json[this.m_Position]);
				this.m_Position++;
			}
			return stringBuilder.ToString();
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
					this.m_Position++;
					this.m_IsFirstProperty = true;
					ISavable savable = result as ISavable;
					savable.OnRead(this);
					this.m_Position++;
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
					this.m_Position++;
					this.m_IsFirstProperty = true;
					ISavable savable = result as ISavable;
					savable.OnRead(this);
					this.m_Position++;
				}
				else
				{
					this.ReadIntoSavableMembers(result, type);
				}
			}
		}

		public override void ReadSavableMembers(object obj, Type type)
		{
			this.m_Position++;
			bool flag = true;
			int objectLength = this.GetObjectLength();
			for (int i = 0; i < objectLength; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.m_Position++;
				}
				string name = this.ReadQoutedString();
				FieldInfo savableField = type.GetSavableField(name);
				if (savableField != null)
				{
					this.m_Position++;
					object value = this.Read(savableField.FieldType);
					savableField.SetValue(obj, value);
				}
				else
				{
					PropertyInfo savableProperty = type.GetSavableProperty(name);
					if (savableProperty != null)
					{
						this.m_Position++;
						object value2 = this.Read(savableProperty.PropertyType);
						savableProperty.SetValue(obj, value2, null);
					}
				}
			}
			this.m_Position++;
		}

		public override void ReadIntoSavableMembers(object obj, Type type)
		{
			this.m_Position++;
			bool flag = true;
			int objectLength = this.GetObjectLength();
			for (int i = 0; i < objectLength; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.m_Position++;
				}
				string name = this.ReadQoutedString();
				FieldInfo savableField = type.GetSavableField(name);
				if (savableField != null)
				{
					this.m_Position++;
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
						this.m_Position++;
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
			this.m_Position++;
		}

		public override void Dispose()
		{
			if (this.m_Reader != null)
			{
				this.m_Reader.Dispose();
			}
		}

		protected TextReader m_Reader;

		protected string m_Json;

		protected int m_Position;

		protected bool m_IsFirstProperty = true;
	}
}
