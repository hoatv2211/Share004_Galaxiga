using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_JointSpring : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(JointSpring);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			JointSpring jointSpring = (JointSpring)value;
			writer.WriteProperty<float>("spring", jointSpring.spring);
			writer.WriteProperty<float>("damper", jointSpring.damper);
			writer.WriteProperty<float>("targetPosition", jointSpring.targetPosition);
		}

		public override object Read(ISaveGameReader reader)
		{
			JointSpring jointSpring = default(JointSpring);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "spring"))
					{
						if (!(text == "damper"))
						{
							if (text == "targetPosition")
							{
								jointSpring.targetPosition = reader.ReadProperty<float>();
							}
						}
						else
						{
							jointSpring.damper = reader.ReadProperty<float>();
						}
					}
					else
					{
						jointSpring.spring = reader.ReadProperty<float>();
					}
				}
			}
			return jointSpring;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
