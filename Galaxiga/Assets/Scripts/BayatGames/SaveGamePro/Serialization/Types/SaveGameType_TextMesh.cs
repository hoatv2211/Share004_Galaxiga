using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_TextMesh : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(TextMesh);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			TextMesh textMesh = (TextMesh)value;
			writer.WriteProperty<string>("text", textMesh.text);
			writer.WriteProperty<Font>("font", textMesh.font);
			writer.WriteProperty<int>("fontSize", textMesh.fontSize);
			writer.WriteProperty<FontStyle>("fontStyle", textMesh.fontStyle);
			writer.WriteProperty<float>("offsetZ", textMesh.offsetZ);
			writer.WriteProperty<TextAlignment>("alignment", textMesh.alignment);
			writer.WriteProperty<TextAnchor>("anchor", textMesh.anchor);
			writer.WriteProperty<float>("characterSize", textMesh.characterSize);
			writer.WriteProperty<float>("lineSpacing", textMesh.lineSpacing);
			writer.WriteProperty<float>("tabSize", textMesh.tabSize);
			writer.WriteProperty<bool>("richText", textMesh.richText);
			writer.WriteProperty<Color>("color", textMesh.color);
			writer.WriteProperty<string>("tag", textMesh.tag);
			writer.WriteProperty<string>("name", textMesh.name);
			writer.WriteProperty<HideFlags>("hideFlags", textMesh.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			TextMesh textMesh = SaveGameType.CreateComponent<TextMesh>();
			this.ReadInto(textMesh, reader);
			return textMesh;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			TextMesh textMesh = (TextMesh)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "text":
					textMesh.text = reader.ReadProperty<string>();
					break;
				case "font":
					if (textMesh.font == null)
					{
						textMesh.font = reader.ReadProperty<Font>();
					}
					else
					{
						reader.ReadIntoProperty<Font>(textMesh.font);
					}
					break;
				case "fontSize":
					textMesh.fontSize = reader.ReadProperty<int>();
					break;
				case "fontStyle":
					textMesh.fontStyle = reader.ReadProperty<FontStyle>();
					break;
				case "offsetZ":
					textMesh.offsetZ = reader.ReadProperty<float>();
					break;
				case "alignment":
					textMesh.alignment = reader.ReadProperty<TextAlignment>();
					break;
				case "anchor":
					textMesh.anchor = reader.ReadProperty<TextAnchor>();
					break;
				case "characterSize":
					textMesh.characterSize = reader.ReadProperty<float>();
					break;
				case "lineSpacing":
					textMesh.lineSpacing = reader.ReadProperty<float>();
					break;
				case "tabSize":
					textMesh.tabSize = reader.ReadProperty<float>();
					break;
				case "richText":
					textMesh.richText = reader.ReadProperty<bool>();
					break;
				case "color":
					textMesh.color = reader.ReadProperty<Color>();
					break;
				case "tag":
					textMesh.tag = reader.ReadProperty<string>();
					break;
				case "name":
					textMesh.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					textMesh.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
