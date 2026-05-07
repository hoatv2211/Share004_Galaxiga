using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_JointDrive : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(JointDrive);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			JointDrive jointDrive = (JointDrive)value;
			writer.WriteProperty<float>("positionSpring", jointDrive.positionSpring);
			writer.WriteProperty<float>("positionDamper", jointDrive.positionDamper);
			writer.WriteProperty<float>("maximumForce", jointDrive.maximumForce);
		}

		public override object Read(ISaveGameReader reader)
		{
			JointDrive jointDrive = default(JointDrive);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "positionSpring"))
					{
						if (!(text == "positionDamper"))
						{
							if (text == "maximumForce")
							{
								jointDrive.maximumForce = reader.ReadProperty<float>();
							}
						}
						else
						{
							jointDrive.positionDamper = reader.ReadProperty<float>();
						}
					}
					else
					{
						jointDrive.positionSpring = reader.ReadProperty<float>();
					}
				}
			}
			return jointDrive;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
