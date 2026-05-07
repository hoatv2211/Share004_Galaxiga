using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Quaternion : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Quaternion);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Quaternion quaternion = (Quaternion)value;
			writer.WriteProperty<float>("x", quaternion.x);
			writer.WriteProperty<float>("y", quaternion.y);
			writer.WriteProperty<float>("z", quaternion.z);
			writer.WriteProperty<float>("w", quaternion.w);
			writer.WriteProperty<Vector3>("eulerAngles", quaternion.eulerAngles);
		}

		public override object Read(ISaveGameReader reader)
		{
			Quaternion quaternion = default(Quaternion);
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
								if (!(text == "w"))
								{
									if (text == "eulerAngles")
									{
										quaternion.eulerAngles = reader.ReadProperty<Vector3>();
									}
								}
								else
								{
									quaternion.w = reader.ReadProperty<float>();
								}
							}
							else
							{
								quaternion.z = reader.ReadProperty<float>();
							}
						}
						else
						{
							quaternion.y = reader.ReadProperty<float>();
						}
					}
					else
					{
						quaternion.x = reader.ReadProperty<float>();
					}
				}
			}
			return quaternion;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
