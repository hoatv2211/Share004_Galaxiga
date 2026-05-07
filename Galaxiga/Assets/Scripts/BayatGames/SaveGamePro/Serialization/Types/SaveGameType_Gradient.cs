using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Gradient : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Gradient);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Gradient gradient = (Gradient)value;
			writer.WriteProperty<GradientColorKey[]>("colorKeys", gradient.colorKeys);
			writer.WriteProperty<GradientAlphaKey[]>("alphaKeys", gradient.alphaKeys);
			writer.WriteProperty<GradientMode>("mode", gradient.mode);
		}

		public override object Read(ISaveGameReader reader)
		{
			Gradient gradient = new Gradient();
			this.ReadInto(gradient, reader);
			return gradient;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Gradient gradient = (Gradient)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "colorKeys"))
					{
						if (!(text == "alphaKeys"))
						{
							if (text == "mode")
							{
								gradient.mode = reader.ReadProperty<GradientMode>();
							}
						}
						else
						{
							gradient.alphaKeys = reader.ReadProperty<GradientAlphaKey[]>();
						}
					}
					else
					{
						gradient.colorKeys = reader.ReadProperty<GradientColorKey[]>();
					}
				}
			}
		}
	}
}
