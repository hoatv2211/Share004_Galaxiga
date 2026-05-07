using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Vector4 : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Vector4);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Vector4 vector = (Vector4)value;
			writer.WriteProperty<float>("x", vector.x);
			writer.WriteProperty<float>("y", vector.y);
			writer.WriteProperty<float>("z", vector.z);
			writer.WriteProperty<float>("w", vector.w);
		}

		public override object Read(ISaveGameReader reader)
		{
			Vector4 vector = default(Vector4);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "x"))
					{
						if (!(text == "y"))
						{
							if (!(text == "z"))
							{
								if (text == "w")
								{
									vector.w = reader.ReadProperty<float>();
								}
							}
							else
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
