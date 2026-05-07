using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Color : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Color);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Color color = (Color)value;
			writer.WriteProperty<float>("r", color.r);
			writer.WriteProperty<float>("g", color.g);
			writer.WriteProperty<float>("b", color.b);
			writer.WriteProperty<float>("a", color.a);
		}

		public override object Read(ISaveGameReader reader)
		{
			Color color = default(Color);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "r"))
					{
						if (!(text == "g"))
						{
							if (!(text == "b"))
							{
								if (text == "a")
								{
									color.a = reader.ReadProperty<float>();
								}
							}
							else
							{
								color.b = reader.ReadProperty<float>();
							}
						}
						else
						{
							color.g = reader.ReadProperty<float>();
						}
					}
					else
					{
						color.r = reader.ReadProperty<float>();
					}
				}
			}
			return color;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
