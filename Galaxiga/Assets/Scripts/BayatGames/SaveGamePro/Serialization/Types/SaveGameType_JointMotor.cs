using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_JointMotor : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(JointMotor);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			JointMotor jointMotor = (JointMotor)value;
			writer.WriteProperty<float>("targetVelocity", jointMotor.targetVelocity);
			writer.WriteProperty<float>("force", jointMotor.force);
			writer.WriteProperty<bool>("freeSpin", jointMotor.freeSpin);
		}

		public override object Read(ISaveGameReader reader)
		{
			JointMotor jointMotor = default(JointMotor);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "targetVelocity"))
					{
						if (!(text == "force"))
						{
							if (text == "freeSpin")
							{
								jointMotor.freeSpin = reader.ReadProperty<bool>();
							}
						}
						else
						{
							jointMotor.force = reader.ReadProperty<float>();
						}
					}
					else
					{
						jointMotor.targetVelocity = reader.ReadProperty<float>();
					}
				}
			}
			return jointMotor;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
