using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Text : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Text);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Text text = (Text)value;
			writer.WriteProperty<Font>("font", text.font);
			writer.WriteProperty<string>("text", text.text);
			writer.WriteProperty<bool>("supportRichText", text.supportRichText);
			writer.WriteProperty<bool>("resizeTextForBestFit", text.resizeTextForBestFit);
			writer.WriteProperty<int>("resizeTextMinSize", text.resizeTextMinSize);
			writer.WriteProperty<int>("resizeTextMaxSize", text.resizeTextMaxSize);
			writer.WriteProperty<TextAnchor>("alignment", text.alignment);
			writer.WriteProperty<bool>("alignByGeometry", text.alignByGeometry);
			writer.WriteProperty<int>("fontSize", text.fontSize);
			writer.WriteProperty<HorizontalWrapMode>("horizontalOverflow", text.horizontalOverflow);
			writer.WriteProperty<VerticalWrapMode>("verticalOverflow", text.verticalOverflow);
			writer.WriteProperty<float>("lineSpacing", text.lineSpacing);
			writer.WriteProperty<FontStyle>("fontStyle", text.fontStyle);
			writer.WriteProperty<bool>("maskable", text.maskable);
			writer.WriteProperty<Color>("color", text.color);
			writer.WriteProperty<bool>("raycastTarget", text.raycastTarget);
			writer.WriteProperty<Material>("material", text.material);
			writer.WriteProperty<bool>("useGUILayout", text.useGUILayout);
			writer.WriteProperty<bool>("enabled", text.enabled);
			writer.WriteProperty<string>("tag", text.tag);
			writer.WriteProperty<string>("name", text.name);
			writer.WriteProperty<HideFlags>("hideFlags", text.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Text text = SaveGameType.CreateComponent<Text>();
			this.ReadInto(text, reader);
			return text;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Text text = (Text)value;
			foreach (string text2 in reader.Properties)
			{
				switch (text2)
				{
				case "font":
					if (text.font == null)
					{
						text.font = reader.ReadProperty<Font>();
					}
					else
					{
						reader.ReadIntoProperty<Font>(text.font);
					}
					break;
				case "text":
					text.text = reader.ReadProperty<string>();
					break;
				case "supportRichText":
					text.supportRichText = reader.ReadProperty<bool>();
					break;
				case "resizeTextForBestFit":
					text.resizeTextForBestFit = reader.ReadProperty<bool>();
					break;
				case "resizeTextMinSize":
					text.resizeTextMinSize = reader.ReadProperty<int>();
					break;
				case "resizeTextMaxSize":
					text.resizeTextMaxSize = reader.ReadProperty<int>();
					break;
				case "alignment":
					text.alignment = reader.ReadProperty<TextAnchor>();
					break;
				case "alignByGeometry":
					text.alignByGeometry = reader.ReadProperty<bool>();
					break;
				case "fontSize":
					text.fontSize = reader.ReadProperty<int>();
					break;
				case "horizontalOverflow":
					text.horizontalOverflow = reader.ReadProperty<HorizontalWrapMode>();
					break;
				case "verticalOverflow":
					text.verticalOverflow = reader.ReadProperty<VerticalWrapMode>();
					break;
				case "lineSpacing":
					text.lineSpacing = reader.ReadProperty<float>();
					break;
				case "fontStyle":
					text.fontStyle = reader.ReadProperty<FontStyle>();
					break;
				case "maskable":
					text.maskable = reader.ReadProperty<bool>();
					break;
				case "color":
					text.color = reader.ReadProperty<Color>();
					break;
				case "raycastTarget":
					text.raycastTarget = reader.ReadProperty<bool>();
					break;
				case "material":
					if (text.material == null)
					{
						text.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(text.material);
					}
					break;
				case "useGUILayout":
					text.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					text.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					text.tag = reader.ReadProperty<string>();
					break;
				case "name":
					text.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					text.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
