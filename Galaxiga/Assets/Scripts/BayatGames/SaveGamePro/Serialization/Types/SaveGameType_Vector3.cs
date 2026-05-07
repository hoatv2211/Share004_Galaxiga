using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Vector3 : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Vector3);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Vector3 vector = (Vector3)value;
			writer.WriteProperty<float>("x", vector.x);
			writer.WriteProperty<float>("y", vector.y);
			writer.WriteProperty<float>("z", vector.z);
		}

		public override object Read(ISaveGameReader reader)
		{
			Vector3 vector = default(Vector3);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "x"))
					{
						if (!(text == "y"))
						{
							if (text == "z")
							{
								vector.z = reader.ReadProperty<float>();
							}
						}
						else
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
