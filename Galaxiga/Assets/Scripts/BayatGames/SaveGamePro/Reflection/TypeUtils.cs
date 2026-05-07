using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BayatGames.SaveGamePro.Reflection
{
	public static class TypeUtils
	{
		public static bool IsSavable(this Type type)
		{
			List<FieldInfo> savableFields = type.GetSavableFields();
			List<PropertyInfo> savableProperties = type.GetSavableProperties();
			return !type.IsInterface && (savableFields.Count > 0 || savableProperties.Count > 0) && !Attribute.IsDefined(type, typeof(ObsoleteAttribute));
		}

		public static FieldInfo GetSavableField(this Type type, string name)
		{
			FieldInfo field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return (!(field != null) || !field.IsSavable()) ? null : field;
		}

		public static PropertyInfo GetSavableProperty(this Type type, string name)
		{
			PropertyInfo property = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return (!(property != null) || !property.IsSavable()) ? null : property;
		}

		public static List<FieldInfo> GetSavableFields(this Type type)
		{
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			List<FieldInfo> list = new List<FieldInfo>();
			foreach (FieldInfo fieldInfo in fields)
			{
				if (fieldInfo.IsSavable())
				{
					list.Add(fieldInfo);
				}
			}
			return list;
		}

		public static List<PropertyInfo> GetSavableProperties(this Type type)
		{
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			List<PropertyInfo> list = new List<PropertyInfo>();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.IsSavable())
				{
					list.Add(propertyInfo);
				}
			}
			return list;
		}

		public static string GetFriendlyName(this Type type)
		{
            string fullName = type.FullName;
            fullName = ((!type.IsGenericType) ? type.FullName : (type.FullName.Split('`')[0] + "<" + string.Join(", ", type.GetGenericArguments().Select(GetFriendlyName).ToArray()) + ">"));
            return fullName.Replace("+", ".");
        }

		public static object GetDefault(this Type type)
		{
			if (type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		public static bool IsSubclassOf<T>(this Type type)
		{
			return type.IsSubclassOf(typeof(T));
		}

		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		public static object CreateInstance(this Type type)
		{
			return FormatterServices.GetUninitializedObject(type);
		}

		public static Type GetNullableType(this Type type)
		{
			return typeof(Nullable<>).MakeGenericType(new Type[]
			{
				type
			});
		}

		public const BindingFlags SavableBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		[CompilerGenerated]
		private static Func<Type, string> _003C_003Ef__mg_0024cache0;
	}
}
