using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_CharacterInfo : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(CharacterInfo);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			CharacterInfo characterInfo = (CharacterInfo)value;
			writer.WriteProperty<int>("index", characterInfo.index);
			writer.WriteProperty<int>("size", characterInfo.size);
			writer.WriteProperty<FontStyle>("style", characterInfo.style);
			writer.WriteProperty<int>("advance", characterInfo.advance);
			writer.WriteProperty<int>("glyphWidth", characterInfo.glyphWidth);
			writer.WriteProperty<int>("glyphHeight", characterInfo.glyphHeight);
			writer.WriteProperty<int>("bearing", characterInfo.bearing);
			writer.WriteProperty<int>("minY", characterInfo.minY);
			writer.WriteProperty<int>("maxY", characterInfo.maxY);
			writer.WriteProperty<int>("minX", characterInfo.minX);
			writer.WriteProperty<int>("maxX", characterInfo.maxX);
			writer.WriteProperty<Vector2>("uvBottomLeft", characterInfo.uvBottomLeft);
			writer.WriteProperty<Vector2>("uvBottomRight", characterInfo.uvBottomRight);
			writer.WriteProperty<Vector2>("uvTopRight", characterInfo.uvTopRight);
			writer.WriteProperty<Vector2>("uvTopLeft", characterInfo.uvTopLeft);
		}

		public override object Read(ISaveGameReader reader)
		{
			CharacterInfo characterInfo = default(CharacterInfo);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "index":
					characterInfo.index = reader.ReadProperty<int>();
					break;
				case "size":
					characterInfo.size = reader.ReadProperty<int>();
					break;
				case "style":
					characterInfo.style = reader.ReadProperty<FontStyle>();
					break;
				case "advance":
					characterInfo.advance = reader.ReadProperty<int>();
					break;
				case "glyphWidth":
					characterInfo.glyphWidth = reader.ReadProperty<int>();
					break;
				case "glyphHeight":
					characterInfo.glyphHeight = reader.ReadProperty<int>();
					break;
				case "bearing":
					characterInfo.bearing = reader.ReadProperty<int>();
					break;
				case "minY":
					characterInfo.minY = reader.ReadProperty<int>();
					break;
				case "maxY":
					characterInfo.maxY = reader.ReadProperty<int>();
					break;
				case "minX":
					characterInfo.minX = reader.ReadProperty<int>();
					break;
				case "maxX":
					characterInfo.maxX = reader.ReadProperty<int>();
					break;
				case "uvBottomLeft":
					characterInfo.uvBottomLeft = reader.ReadProperty<Vector2>();
					break;
				case "uvBottomRight":
					characterInfo.uvBottomRight = reader.ReadProperty<Vector2>();
					break;
				case "uvTopRight":
					characterInfo.uvTopRight = reader.ReadProperty<Vector2>();
					break;
				case "uvTopLeft":
					characterInfo.uvTopLeft = reader.ReadProperty<Vector2>();
					break;
				}
			}
			return characterInfo;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
