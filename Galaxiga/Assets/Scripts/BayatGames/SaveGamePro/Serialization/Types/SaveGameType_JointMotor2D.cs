using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_JointMotor2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(JointMotor2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			JointMotor2D jointMotor2D = (JointMotor2D)value;
			writer.WriteProperty<float>("motorSpeed", jointMotor2D.motorSpeed);
			writer.WriteProperty<float>("maxMotorTorque", jointMotor2D.maxMotorTorque);
		}

		public override object Read(ISaveGameReader reader)
		{
			JointMotor2D jointMotor2D = default(JointMotor2D);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "motorSpeed"))
					{
						if (text == "maxMotorTorque")
						{
							jointMotor2D.maxMotorTorque = reader.ReadProperty<float>();
						}
					}
					else
					{
						jointMotor2D.motorSpeed = reader.ReadProperty<float>();
					}
				}
			}
			return jointMotor2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
