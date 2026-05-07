using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Rect : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Rect);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Rect rect = (Rect)value;
			writer.WriteProperty<float>("x", rect.x);
			writer.WriteProperty<float>("y", rect.y);
			writer.WriteProperty<Vector2>("position", rect.position);
			writer.WriteProperty<Vector2>("center", rect.center);
			writer.WriteProperty<Vector2>("min", rect.min);
			writer.WriteProperty<Vector2>("max", rect.max);
			writer.WriteProperty<float>("width", rect.width);
			writer.WriteProperty<float>("height", rect.height);
			writer.WriteProperty<Vector2>("size", rect.size);
			writer.WriteProperty<float>("xMin", rect.xMin);
			writer.WriteProperty<float>("yMin", rect.yMin);
			writer.WriteProperty<float>("xMax", rect.xMax);
			writer.WriteProperty<float>("yMax", rect.yMax);
		}

		public override object Read(ISaveGameReader reader)
		{
			Rect rect = default(Rect);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "x":
					rect.x = reader.ReadProperty<float>();
					break;
				case "y":
					rect.y = reader.ReadProperty<float>();
					break;
				case "position":
					rect.position = reader.ReadProperty<Vector2>();
					break;
				case "center":
					rect.center = reader.ReadProperty<Vector2>();
					break;
				case "min":
					rect.min = reader.ReadProperty<Vector2>();
					break;
				case "max":
					rect.max = reader.ReadProperty<Vector2>();
					break;
				case "width":
					rect.width = reader.ReadProperty<float>();
					break;
				case "height":
					rect.height = reader.ReadProperty<float>();
					break;
				case "size":
					rect.size = reader.ReadProperty<Vector2>();
					break;
				case "xMin":
					rect.xMin = reader.ReadProperty<float>();
					break;
				case "yMin":
					rect.yMin = reader.ReadProperty<float>();
					break;
				case "xMax":
					rect.xMax = reader.ReadProperty<float>();
					break;
				case "yMax":
					rect.yMax = reader.ReadProperty<float>();
					break;
				}
			}
			return rect;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
