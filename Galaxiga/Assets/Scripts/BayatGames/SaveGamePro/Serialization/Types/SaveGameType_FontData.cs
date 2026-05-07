using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_FontData : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(FontData);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			FontData fontData = (FontData)value;
			writer.WriteProperty<Font>("font", fontData.font);
			writer.WriteProperty<int>("fontSize", fontData.fontSize);
			writer.WriteProperty<FontStyle>("fontStyle", fontData.fontStyle);
			writer.WriteProperty<bool>("bestFit", fontData.bestFit);
			writer.WriteProperty<int>("minSize", fontData.minSize);
			writer.WriteProperty<int>("maxSize", fontData.maxSize);
			writer.WriteProperty<TextAnchor>("alignment", fontData.alignment);
			writer.WriteProperty<bool>("alignByGeometry", fontData.alignByGeometry);
			writer.WriteProperty<bool>("richText", fontData.richText);
			writer.WriteProperty<HorizontalWrapMode>("horizontalOverflow", fontData.horizontalOverflow);
			writer.WriteProperty<VerticalWrapMode>("verticalOverflow", fontData.verticalOverflow);
			writer.WriteProperty<float>("lineSpacing", fontData.lineSpacing);
		}

		public override object Read(ISaveGameReader reader)
		{
			FontData fontData = new FontData();
			this.ReadInto(fontData, reader);
			return fontData;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			FontData fontData = (FontData)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "font":
					if (fontData.font == null)
					{
						fontData.font = reader.ReadProperty<Font>();
					}
					else
					{
						reader.ReadIntoProperty<Font>(fontData.font);
					}
					break;
				case "fontSize":
					fontData.fontSize = reader.ReadProperty<int>();
					break;
				case "fontStyle":
					fontData.fontStyle = reader.ReadProperty<FontStyle>();
					break;
				case "bestFit":
					fontData.bestFit = reader.ReadProperty<bool>();
					break;
				case "minSize":
					fontData.minSize = reader.ReadProperty<int>();
					break;
				case "maxSize":
					fontData.maxSize = reader.ReadProperty<int>();
					break;
				case "alignment":
					fontData.alignment = reader.ReadProperty<TextAnchor>();
					break;
				case "alignByGeometry":
					fontData.alignByGeometry = reader.ReadProperty<bool>();
					break;
				case "richText":
					fontData.richText = reader.ReadProperty<bool>();
					break;
				case "horizontalOverflow":
					fontData.horizontalOverflow = reader.ReadProperty<HorizontalWrapMode>();
					break;
				case "verticalOverflow":
					fontData.verticalOverflow = reader.ReadProperty<VerticalWrapMode>();
					break;
				case "lineSpacing":
					fontData.lineSpacing = reader.ReadProperty<float>();
					break;
				}
			}
		}
	}
}
