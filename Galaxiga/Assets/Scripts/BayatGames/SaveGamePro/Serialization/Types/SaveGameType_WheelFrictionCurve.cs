using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_WheelFrictionCurve : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(WheelFrictionCurve);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			WheelFrictionCurve wheelFrictionCurve = (WheelFrictionCurve)value;
			writer.WriteProperty<float>("extremumSlip", wheelFrictionCurve.extremumSlip);
			writer.WriteProperty<float>("extremumValue", wheelFrictionCurve.extremumValue);
			writer.WriteProperty<float>("asymptoteSlip", wheelFrictionCurve.asymptoteSlip);
			writer.WriteProperty<float>("asymptoteValue", wheelFrictionCurve.asymptoteValue);
			writer.WriteProperty<float>("stiffness", wheelFrictionCurve.stiffness);
		}

		public override object Read(ISaveGameReader reader)
		{
			WheelFrictionCurve wheelFrictionCurve = default(WheelFrictionCurve);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "extremumSlip"))
					{
						if (!(text == "extremumValue"))
						{
							if (!(text == "asymptoteSlip"))
							{
								if (!(text == "asymptoteValue"))
								{
									if (text == "stiffness")
									{
										wheelFrictionCurve.stiffness = reader.ReadProperty<float>();
									}
								}
								else
								{
									wheelFrictionCurve.asymptoteValue = reader.ReadProperty<float>();
								}
							}
							else
							{
								wheelFrictionCurve.asymptoteSlip = reader.ReadProperty<float>();
							}
						}
						else
						{
							wheelFrictionCurve.extremumValue = reader.ReadProperty<float>();
						}
					}
					else
					{
						wheelFrictionCurve.extremumSlip = reader.ReadProperty<float>();
					}
				}
			}
			return wheelFrictionCurve;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
