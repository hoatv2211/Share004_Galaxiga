using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SoftJointLimitSpring : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SoftJointLimitSpring);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SoftJointLimitSpring softJointLimitSpring = (SoftJointLimitSpring)value;
			writer.WriteProperty<float>("spring", softJointLimitSpring.spring);
			writer.WriteProperty<float>("damper", softJointLimitSpring.damper);
		}

		public override object Read(ISaveGameReader reader)
		{
			SoftJointLimitSpring softJointLimitSpring = default(SoftJointLimitSpring);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "spring"))
					{
						if (text == "damper")
						{
							softJointLimitSpring.damper = reader.ReadProperty<float>();
						}
					}
					else
					{
						softJointLimitSpring.spring = reader.ReadProperty<float>();
					}
				}
			}
			return softJointLimitSpring;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
