using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Ray : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Ray);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Ray ray = (Ray)value;
			writer.WriteProperty<Vector3>("origin", ray.origin);
			writer.WriteProperty<Vector3>("direction", ray.direction);
		}

		public override object Read(ISaveGameReader reader)
		{
			Ray ray = default(Ray);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "origin"))
					{
						if (text == "direction")
						{
							ray.direction = reader.ReadProperty<Vector3>();
						}
					}
					else
					{
						ray.origin = reader.ReadProperty<Vector3>();
					}
				}
			}
			return ray;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
