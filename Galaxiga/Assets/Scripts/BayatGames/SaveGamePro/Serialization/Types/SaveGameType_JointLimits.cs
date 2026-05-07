using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_JointLimits : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(JointLimits);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			JointLimits jointLimits = (JointLimits)value;
			writer.WriteProperty<float>("min", jointLimits.min);
			writer.WriteProperty<float>("max", jointLimits.max);
			writer.WriteProperty<float>("bounciness", jointLimits.bounciness);
			writer.WriteProperty<float>("bounceMinVelocity", jointLimits.bounceMinVelocity);
			writer.WriteProperty<float>("contactDistance", jointLimits.contactDistance);
		}

		public override object Read(ISaveGameReader reader)
		{
			JointLimits jointLimits = default(JointLimits);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "min"))
					{
						if (!(text == "max"))
						{
							if (!(text == "bounciness"))
							{
								if (!(text == "bounceMinVelocity"))
								{
									if (text == "contactDistance")
									{
										jointLimits.contactDistance = reader.ReadProperty<float>();
									}
								}
								else
								{
									jointLimits.bounceMinVelocity = reader.ReadProperty<float>();
								}
							}
							else
							{
								jointLimits.bounciness = reader.ReadProperty<float>();
							}
						}
						else
						{
							jointLimits.max = reader.ReadProperty<float>();
						}
					}
					else
					{
						jointLimits.min = reader.ReadProperty<float>();
					}
				}
			}
			return jointLimits;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
