using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Color32 : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Color32);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Color32 color = (Color32)value;
			writer.WriteProperty<byte>("r", color.r);
			writer.WriteProperty<byte>("g", color.g);
			writer.WriteProperty<byte>("b", color.b);
			writer.WriteProperty<byte>("a", color.a);
		}

		public override object Read(ISaveGameReader reader)
		{
			Color32 color = default(Color32);
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
									color.a = reader.ReadProperty<byte>();
								}
							}
							else
							{
								color.b = reader.ReadProperty<byte>();
							}
						}
						else
						{
							color.g = reader.ReadProperty<byte>();
						}
					}
					else
					{
						color.r = reader.ReadProperty<byte>();
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
