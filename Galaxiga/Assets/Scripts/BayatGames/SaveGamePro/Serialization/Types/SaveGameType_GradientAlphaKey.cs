using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_GradientAlphaKey : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(GradientAlphaKey);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			GradientAlphaKey gradientAlphaKey = (GradientAlphaKey)value;
			writer.WriteProperty<float>("alpha", gradientAlphaKey.alpha);
			writer.WriteProperty<float>("time", gradientAlphaKey.time);
		}

		public override object Read(ISaveGameReader reader)
		{
			GradientAlphaKey gradientAlphaKey = default(GradientAlphaKey);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "alpha"))
					{
						if (text == "time")
						{
							gradientAlphaKey.time = reader.ReadProperty<float>();
						}
					}
					else
					{
						gradientAlphaKey.alpha = reader.ReadProperty<float>();
					}
				}
			}
			return gradientAlphaKey;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
