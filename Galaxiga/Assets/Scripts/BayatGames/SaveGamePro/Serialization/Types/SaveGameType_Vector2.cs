using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Vector2 : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Vector2);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Vector2 vector = (Vector2)value;
			writer.WriteProperty<float>("x", vector.x);
			writer.WriteProperty<float>("y", vector.y);
		}

		public override object Read(ISaveGameReader reader)
		{
			Vector2 vector = default(Vector2);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "x"))
					{
						if (text == "y")
						{
							vector.y = reader.ReadProperty<float>();
						}
					}
					else
					{
						vector.x = reader.ReadProperty<float>();
					}
				}
			}
			return vector;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
