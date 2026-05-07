using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_GradientColorKey : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(GradientColorKey);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			GradientColorKey gradientColorKey = (GradientColorKey)value;
			writer.WriteProperty<Color>("color", gradientColorKey.color);
			writer.WriteProperty<float>("time", gradientColorKey.time);
		}

		public override object Read(ISaveGameReader reader)
		{
			GradientColorKey gradientColorKey = default(GradientColorKey);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "color"))
					{
						if (text == "time")
						{
							gradientColorKey.time = reader.ReadProperty<float>();
						}
					}
					else
					{
						gradientColorKey.color = reader.ReadProperty<Color>();
					}
				}
			}
			return gradientColorKey;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
