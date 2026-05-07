using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SoftJointLimit : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SoftJointLimit);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SoftJointLimit softJointLimit = (SoftJointLimit)value;
			writer.WriteProperty<float>("limit", softJointLimit.limit);
			writer.WriteProperty<float>("bounciness", softJointLimit.bounciness);
			writer.WriteProperty<float>("contactDistance", softJointLimit.contactDistance);
		}

		public override object Read(ISaveGameReader reader)
		{
			SoftJointLimit softJointLimit = default(SoftJointLimit);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "limit"))
					{
						if (!(text == "bounciness"))
						{
							if (text == "contactDistance")
							{
								softJointLimit.contactDistance = reader.ReadProperty<float>();
							}
						}
						else
						{
							softJointLimit.bounciness = reader.ReadProperty<float>();
						}
					}
					else
					{
						softJointLimit.limit = reader.ReadProperty<float>();
					}
				}
			}
			return softJointLimit;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
