using System;
using System.Collections.Generic;
using System.Text;

namespace BayatGames.SaveGamePro.Serialization.Formatters.Json
{
	public static class JsonExtensions
	{
		public static string ToJson(this object value)
		{
			return JsonFormatter.SerializeObject(value);
		}

		public static void AppendUntilStringEnd(this StringBuilder builder, string json, ref int index)
		{
			if (json.Length <= index)
			{
				return;
			}
			builder.Append(json[index]);
			index++;
			while (json[index] != '"')
			{
				builder.Append(json[index]);
				index++;
			}
			builder.Append(json[index]);
		}

		public static string RemoveWhitespaceJson(this string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				return json;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < json.Length; i++)
			{
				if (json[i] == '"')
				{
					stringBuilder.AppendUntilStringEnd(json, ref i);
				}
				else if (!char.IsWhiteSpace(json[i]))
				{
					stringBuilder.Append(json[i]);
				}
			}
			return stringBuilder.ToString();
		}

		public static string[] SplitJson(this string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				return new string[0];
			}
			List<string> list = new List<string>();
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			int i = 1;
			while (i < json.Length - 1)
			{
				char c = json[i];
				switch (c)
				{
				case '[':
					goto IL_7C;
				default:
					switch (c)
					{
					case '{':
						goto IL_7C;
					default:
						if (c != '"')
						{
							if (c != ',' && c != ':')
							{
								goto IL_BF;
							}
							if (num != 0)
							{
								goto IL_BF;
							}
							list.Add(stringBuilder.ToString());
							stringBuilder.Length = 0;
						}
						else
						{
							stringBuilder.AppendUntilStringEnd(json, ref i);
						}
						break;
					case '}':
						goto IL_85;
					}
					break;
				case ']':
					goto IL_85;
				}
				IL_CD:
				i++;
				continue;
				IL_BF:
				stringBuilder.Append(json[i]);
				goto IL_CD;
				IL_85:
				num--;
				goto IL_BF;
				IL_7C:
				num++;
				goto IL_BF;
			}
			list.Add(stringBuilder.ToString());
			return list.ToArray();
		}

		public static string EscapeStringJson(this string str)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in str)
			{
				string value = string.Empty;
				switch (c)
				{
				case '\b':
					value = "\\b";
					break;
				case '\t':
					value = "\\t";
					break;
				case '\n':
					value = "\\n";
					break;
				default:
					if (c != '"')
					{
						if (c != '\'')
						{
							if (c == '\\')
							{
								value = "\\\\";
							}
						}
						else
						{
							value = "\\'";
						}
					}
					else
					{
						value = "\\\"";
					}
					break;
				case '\f':
					value = "\\f";
					break;
				case '\r':
					value = "\\r";
					break;
				}
				if (string.IsNullOrEmpty(value))
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append(value);
				}
			}
			return stringBuilder.ToString();
		}

		public static string UnEscapeStringJson(this string str)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in str)
			{
				if (c != '\\')
				{
					stringBuilder.Append(c);
				}
				else
				{
					switch (c)
					{
					case 'r':
						stringBuilder.Append('\r');
						break;
					default:
						if (c != '"' && c != '\'' && c != '/')
						{
							if (c != '\\')
							{
								if (c != 'b')
								{
									if (c != 'f')
									{
										if (c == 'n')
										{
											stringBuilder.Append('\n');
										}
									}
									else
									{
										stringBuilder.Append('\f');
									}
								}
								else
								{
									stringBuilder.Append('\b');
								}
							}
							else
							{
								stringBuilder.Append('\\');
							}
						}
						else
						{
							stringBuilder.Append(c);
						}
						break;
					case 't':
						stringBuilder.Append('\t');
						break;
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
