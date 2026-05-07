using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_JointAngleLimits2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(JointAngleLimits2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			JointAngleLimits2D jointAngleLimits2D = (JointAngleLimits2D)value;
			writer.WriteProperty<float>("min", jointAngleLimits2D.min);
			writer.WriteProperty<float>("max", jointAngleLimits2D.max);
		}

		public override object Read(ISaveGameReader reader)
		{
			JointAngleLimits2D jointAngleLimits2D = default(JointAngleLimits2D);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "min"))
					{
						if (text == "max")
						{
							jointAngleLimits2D.max = reader.ReadProperty<float>();
						}
					}
					else
					{
						jointAngleLimits2D.min = reader.ReadProperty<float>();
					}
				}
			}
			return jointAngleLimits2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
