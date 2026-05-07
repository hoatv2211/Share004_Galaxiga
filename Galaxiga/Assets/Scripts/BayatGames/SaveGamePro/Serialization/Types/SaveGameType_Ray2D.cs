using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Ray2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Ray2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Ray2D ray2D = (Ray2D)value;
			writer.WriteProperty<Vector2>("origin", ray2D.origin);
			writer.WriteProperty<Vector2>("direction", ray2D.direction);
		}

		public override object Read(ISaveGameReader reader)
		{
			Ray2D ray2D = default(Ray2D);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "origin"))
					{
						if (text == "direction")
						{
							ray2D.direction = reader.ReadProperty<Vector2>();
						}
					}
					else
					{
						ray2D.origin = reader.ReadProperty<Vector2>();
					}
				}
			}
			return ray2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
