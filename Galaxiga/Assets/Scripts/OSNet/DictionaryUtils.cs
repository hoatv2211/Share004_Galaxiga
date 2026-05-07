using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OSNet
{
	public class DictionaryUtils
	{
		public static object Decode(object ret, Dictionary<string, object> dictionary)
		{
            Type type = ret.GetType();
            FieldInfo[] fields = type.GetFields();
            if (fields != null)
            {
                FieldInfo[] array = fields;
                foreach (FieldInfo fieldInfo in array)
                {
                    string name = fieldInfo.Name;
                    Type fieldType = fieldInfo.FieldType;
                    if (!dictionary.ContainsKey(name))
                    {
                        continue;
                    }
                    if (fieldType.IsPrimitive || fieldType.Equals(typeof(string)))
                    {
                        fieldInfo.SetValue(ret, Convert.ChangeType(dictionary[name], fieldType));
                    }
                    else if (fieldType.IsArray)
                    {
                        Type elementType = fieldType.GetElementType();
                        if (elementType.IsPrimitive || fieldType.Equals(typeof(string)))
                        {
                            fieldInfo.SetValue(ret, dictionary[name]);
                            continue;
                        }
                        Dictionary<string, object>[] array2 = (Dictionary<string, object>[])dictionary[name];
                        Array array3 = Array.CreateInstance(elementType, array2.Length);
                        for (int j = 0; j < array2.Length; j++)
                        {
                            object obj = Activator.CreateInstance(elementType);
                            Decode(obj, array2[j]);
                            array3.SetValue(obj, j);
                        }
                        fieldInfo.SetValue(ret, array3);
                    }
                    else if (fieldType.IsGenericList())
                    {
                        Type type2 = fieldType.GetGenericArguments().Single();
                        IList list = (IList)Activator.CreateInstance(fieldType);
                        if (type2.IsPrimitive || fieldType.Equals(typeof(string)) || type2.Equals(typeof(string)))
                        {
                            IList list2 = (IList)dictionary[name];
                            for (int k = 0; k < list2.Count; k++)
                            {
                                list.Add(Convert.ChangeType(list2[k], type2));
                            }
                        }
                        else
                        {
                            IList list3 = (IList)dictionary[name];
                            for (int l = 0; l < list3.Count; l++)
                            {
                                object obj2 = Activator.CreateInstance(type2);
                                Decode(obj2, (Dictionary<string, object>)list3[l]);
                                list.Add(obj2);
                            }
                        }
                        fieldInfo.SetValue(ret, list);
                    }
                    else
                    {
                        Dictionary<string, object> dictionary2 = (Dictionary<string, object>)dictionary[name];
                        fieldInfo.SetValue(ret, Decode(Activator.CreateInstance(fieldType), dictionary2));
                    }
                }
            }
            return ret;
        }

		public static Dictionary<string, object> encode(object obj)
		{
            if (obj == null)
            {
                return null;
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            if (fields != null)
            {
                FieldInfo[] array = fields;
                foreach (FieldInfo fieldInfo in array)
                {
                    string name = fieldInfo.Name;
                    Type fieldType = fieldInfo.FieldType;
                    if (fieldType.IsPrimitive || fieldType.Equals(typeof(string)))
                    {
                        dictionary.Add(name, fieldInfo.GetValue(obj));
                    }
                    else if (fieldType.IsArray)
                    {
                        Array array2 = (Array)fieldInfo.GetValue(obj);
                        if (array2 == null)
                        {
                            continue;
                        }
                        Type elementType = fieldType.GetElementType();
                        if (elementType.IsPrimitive || fieldType.Equals(typeof(string)) || elementType.Equals(typeof(string)))
                        {
                            dictionary.Add(name, array2);
                            continue;
                        }
                        Dictionary<string, object>[] array3 = new Dictionary<string, object>[array2.Length];
                        for (int j = 0; j < array2.Length; j++)
                        {
                            array3[j] = encode(array2.GetValue(j));
                        }
                        dictionary.Add(name, array3);
                    }
                    else if (fieldType.IsGenericList())
                    {
                        Type type2 = fieldType.GetGenericArguments().Single();
                        IList list = (IList)fieldInfo.GetValue(obj);
                        if (list == null)
                        {
                            continue;
                        }
                        if (type2.IsPrimitive || fieldType.Equals(typeof(string)) || type2.Equals(typeof(string)))
                        {
                            dictionary.Add(name, list);
                            continue;
                        }
                        List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
                        for (int k = 0; k < list.Count; k++)
                        {
                            list2.Add(encode(list[k]));
                        }
                        dictionary.Add(name, list2);
                    }
                    else
                    {
                        object value = fieldInfo.GetValue(obj);
                        if (value != null)
                        {
                            dictionary.Add(name, encode(value));
                        }
                    }
                }
            }
            return dictionary;
        }
	}
}
