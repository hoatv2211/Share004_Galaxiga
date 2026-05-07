using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace I2.Loc
{
	public static class I2Utils
	{
		public static string ReverseText(string source)
		{
			int length = source.Length;
			char[] array = new char[length];
			for (int i = 0; i < length; i++)
			{
				array[length - 1 - i] = source[i];
			}
			return new string(array);
		}

		public static string RemoveNonASCII(string text, bool allowCategory = false)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			return new string((from c in text.ToCharArray()
			where "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_".IndexOf(c) >= 0 || c == ' ' || (c == '\\' && allowCategory) || (c == '/' && allowCategory)
			select c).ToArray<char>());
		}

		public static string GetValidTermName(string text, bool allowCategory = false)
		{
			text = I2Utils.RemoveTags(text);
			return I2Utils.RemoveNonASCII(text, allowCategory);
		}

		public static string SplitLine(string line, int maxCharacters)
		{
			if (maxCharacters <= 0 || line.Length < maxCharacters)
			{
				return line;
			}
			char[] array = line.ToCharArray();
			bool flag = true;
			bool flag2 = false;
			int i = 0;
			int num = 0;
			while (i < array.Length)
			{
				if (flag)
				{
					num++;
					if (array[i] == '\n')
					{
						num = 0;
					}
					if (num >= maxCharacters && char.IsWhiteSpace(array[i]))
					{
						array[i] = '\n';
						flag = false;
						flag2 = false;
					}
				}
				else if (!char.IsWhiteSpace(array[i]))
				{
					flag = true;
					num = 0;
				}
				else if (array[i] != '\n')
				{
					array[i] = '\0';
				}
				else
				{
					if (!flag2)
					{
						array[i] = '\0';
					}
					flag2 = true;
				}
				i++;
			}
			return new string((from c in array
			where c != '\0'
			select c).ToArray<char>());
		}

		public static bool FindNextTag(string line, int iStart, out int tagStart, out int tagEnd)
		{
			tagStart = -1;
			tagEnd = -1;
			int length = line.Length;
			for (tagStart = iStart; tagStart < length; tagStart++)
			{
				if (line[tagStart] == '[' || line[tagStart] == '(' || line[tagStart] == '{')
				{
					break;
				}
			}
			if (tagStart == length)
			{
				return false;
			}
			bool flag = false;
			for (tagEnd = tagStart + 1; tagEnd < length; tagEnd++)
			{
				char c = line[tagEnd];
				if (c == ']' || c == ')' || c == '}')
				{
					return !flag || I2Utils.FindNextTag(line, tagEnd + 1, out tagStart, out tagEnd);
				}
				if (c > 'ÿ')
				{
					flag = true;
				}
			}
			return false;
		}

		public static string RemoveTags(string text)
		{
			return Regex.Replace(text, "\\{\\[(.*?)]}|\\[(.*?)]|\\<(.*?)>", string.Empty);
		}

		public static bool IsPlaying()
		{
			return Application.isPlaying;
		}

		public static string GetPath(this Transform tr)
		{
			Transform parent = tr.parent;
			if (tr == null)
			{
				return tr.name;
			}
			return parent.GetPath() + "/" + tr.name;
		}

		public static Transform FindObject(string objectPath)
		{
			return I2Utils.FindObject(SceneManager.GetActiveScene(), objectPath);
		}

		public static Transform FindObject(Scene scene, string objectPath)
		{
			GameObject[] rootGameObjects = scene.GetRootGameObjects();
			for (int i = 0; i < rootGameObjects.Length; i++)
			{
				Transform transform = rootGameObjects[i].transform;
				if (transform.name == objectPath)
				{
					return transform;
				}
				if (objectPath.StartsWith(transform.name + "/"))
				{
					return I2Utils.FindObject(transform, objectPath.Substring(transform.name.Length + 1));
				}
			}
			return null;
		}

		public static Transform FindObject(Transform root, string objectPath)
		{
			for (int i = 0; i < root.childCount; i++)
			{
				Transform child = root.GetChild(i);
				if (child.name == objectPath)
				{
					return child;
				}
				if (objectPath.StartsWith(child.name + "/"))
				{
					return I2Utils.FindObject(child, objectPath.Substring(child.name.Length + 1));
				}
			}
			return null;
		}

		public static H FindInParents<H>(Transform tr) where H : Component
		{
			if (!tr)
			{
				return (H)((object)null);
			}
			H component = tr.GetComponent<H>();
			while (!component && tr)
			{
				component = tr.GetComponent<H>();
				tr = tr.parent;
			}
			return component;
		}

		public static string GetCaptureMatch(Match match)
		{
			for (int i = match.Groups.Count - 1; i >= 0; i--)
			{
				if (match.Groups[i].Success)
				{
					return match.Groups[i].ToString();
				}
			}
			return match.ToString();
		}

		public static void SendWebRequest(UnityWebRequest www)
		{
			www.SendWebRequest();
		}

		public const string ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";

		public const string NumberChars = "0123456789";
	}
}
