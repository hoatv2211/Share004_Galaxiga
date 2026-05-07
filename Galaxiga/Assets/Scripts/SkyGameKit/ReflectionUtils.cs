using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace SkyGameKit
{
	public static class ReflectionUtils
	{
		public static string Encode(object obj)
		{
			if (obj == null)
			{
				return string.Empty;
			}
			Type type = obj.GetType();
			if (type == typeof(Vector2))
			{
				Vector2 vector = (Vector2)obj;
				return vector.x + ";" + vector.y;
			}
			if (type == typeof(Vector3))
			{
				Vector3 vector2 = (Vector3)obj;
				return string.Concat(new object[]
				{
					vector2.x,
					";",
					vector2.y,
					";",
					vector2.z
				});
			}
			if (type == typeof(Vector4))
			{
				Vector4 vector3 = (Vector4)obj;
				return string.Concat(new object[]
				{
					vector3.x,
					";",
					vector3.y,
					";",
					vector3.z,
					";",
					vector3.w
				});
			}
			if (type == typeof(Quaternion))
			{
				Quaternion quaternion = (Quaternion)obj;
				return string.Concat(new object[]
				{
					quaternion.x,
					";",
					quaternion.y,
					";",
					quaternion.z,
					";",
					quaternion.w
				});
			}
			if (type == typeof(Color))
			{
				Color color = (Color)obj;
				return string.Concat(new object[]
				{
					color.r,
					";",
					color.g,
					";",
					color.b,
					";",
					color.a
				});
			}
			if (type.IsSupportedList() || type.IsArray)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (obj is IEnumerable)
				{
					IEnumerator enumerator = (obj as IEnumerable).GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							stringBuilder.Append(ReflectionUtils.Encode(obj2)).Append('~');
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
				return stringBuilder.ToString();
			}
			return obj.ToString();
		}

		public static object Decode(Type type, string value)
		{
			object result;
			try
			{
				if (string.IsNullOrEmpty(value))
				{
					result = ReflectionUtils.GetDefault(type);
				}
				else if (type == typeof(string))
				{
					result = value;
				}
				else if (type.IsEnum)
				{
					result = Enum.Parse(type, value);
				}
				else if (type == typeof(int))
				{
					result = int.Parse(value);
				}
				else if (type == typeof(bool))
				{
					result = bool.Parse(value);
				}
				else if (type == typeof(float))
				{
					result = float.Parse(value);
				}
				else if (type == typeof(Vector2))
				{
					string[] array = value.Split(new char[]
					{
						';'
					});
					result = new Vector2(float.Parse(array[0]), float.Parse(array[1]));
				}
				else if (type == typeof(Vector3))
				{
					string[] array2 = value.Split(new char[]
					{
						';'
					});
					result = new Vector3(float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2]));
				}
				else if (type == typeof(Vector4))
				{
					string[] array3 = value.Split(new char[]
					{
						';'
					});
					result = new Vector4(float.Parse(array3[0]), float.Parse(array3[1]), float.Parse(array3[2]), float.Parse(array3[3]));
				}
				else if (type == typeof(Quaternion))
				{
					string[] array4 = value.Split(new char[]
					{
						';'
					});
					result = new Quaternion(float.Parse(array4[0]), float.Parse(array4[1]), float.Parse(array4[2]), float.Parse(array4[3]));
				}
				else if (type == typeof(Color))
				{
					string[] array5 = value.Split(new char[]
					{
						';'
					});
					result = new Color(float.Parse(array5[0]), float.Parse(array5[1]), float.Parse(array5[2]), float.Parse(array5[3]));
				}
				else if (type.IsArray)
				{
					Type itemType = type.GetElementType();
					string[] source = value.Split(new char[]
					{
						'~'
					}, StringSplitOptions.RemoveEmptyEntries);
					object[] array6 = (from x in source
					select ReflectionUtils.Decode(itemType, x)).ToArray<object>();
					Array array7 = Array.CreateInstance(itemType, array6.Length);
					Array.Copy(array6, array7, array6.Length);
					result = array7;
				}
				else if (type.IsSupportedList())
				{
					Type type2 = type.GetGenericArguments()[0];
					string[] array8 = value.Split(new char[]
					{
						'~'
					}, StringSplitOptions.RemoveEmptyEntries);
					IList list = (IList)Activator.CreateInstance(type);
					foreach (string value2 in array8)
					{
						list.Add(ReflectionUtils.Decode(type2, value2));
					}
					result = list;
				}
				else
				{
					result = ReflectionUtils.GetDefault(type);
				}
			}
			catch (Exception ex)
			{
				SgkLog.ReflectionLogError(string.Concat(new object[]
				{
					"Error when try to parse ",
					value,
					" to ",
					type,
					" \nMessage: ",
					ex.Message
				}));
				result = ReflectionUtils.GetDefault(type);
			}
			return result;
		}

		public static T Decode<T>(string stringValue, UnityEngine.Object objectValue = null)
		{
			object obj;
			if (typeof(T).IsUnityObject())
			{
				obj = objectValue;
			}
			else
			{
				obj = ReflectionUtils.Decode(typeof(T), stringValue);
			}
			if (obj is T)
			{
				return (T)((object)obj);
			}
			T result;
			try
			{
				result = (T)((object)Convert.ChangeType(obj, typeof(T)));
			}
			catch (InvalidCastException)
			{
				result = default(T);
			}
			return result;
		}

		public static string GetDefaultValue(Type type)
		{
			string result;
			try
			{
				result = ReflectionUtils.Encode(Activator.CreateInstance(type));
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		public static object GetDefault(Type type)
		{
			object result;
			try
			{
				result = Activator.CreateInstance(type);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public static bool IsUnityObject(this Type type)
		{
			return type.IsSubclassOf(typeof(UnityEngine.Object));
		}

		public static bool IsSupportedList(this Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
		}

		public static Type GetEventParamType(FieldInfo fieldInfo)
		{
			if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(EnemyEvent<>))
			{
				return fieldInfo.FieldType.GetGenericArguments()[0];
			}
			return null;
		}

		public static string GetEventParamName(FieldInfo fieldInfo)
		{
			foreach (object obj in fieldInfo.GetCustomAttributes(true))
			{
				EnemyEventCustom enemyEventCustom = obj as EnemyEventCustom;
				if (enemyEventCustom != null)
				{
					return enemyEventCustom.paramName;
				}
			}
			return null;
		}

		public static string GetMethodDisplayName(MethodInfo methodInfo)
		{
			foreach (object obj in methodInfo.GetCustomAttributes(true))
			{
				EnemyAction enemyAction = obj as EnemyAction;
				if (enemyAction != null && !string.IsNullOrEmpty(enemyAction.displayName))
				{
					return enemyAction.displayName;
				}
			}
			return methodInfo.Name;
		}

		public static string GetEventDisplayName(FieldInfo eventInfo)
		{
			foreach (object obj in eventInfo.GetCustomAttributes(true))
			{
				EnemyEventCustom enemyEventCustom = obj as EnemyEventCustom;
				if (enemyEventCustom != null && !string.IsNullOrEmpty(enemyEventCustom.displayName))
				{
					return enemyEventCustom.displayName;
				}
			}
			return eventInfo.Name;
		}
	}
}
