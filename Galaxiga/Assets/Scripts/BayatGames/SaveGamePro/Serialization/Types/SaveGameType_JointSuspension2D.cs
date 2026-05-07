using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_JointSuspension2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(JointSuspension2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			JointSuspension2D jointSuspension2D = (JointSuspension2D)value;
			writer.WriteProperty<float>("dampingRatio", jointSuspension2D.dampingRatio);
			writer.WriteProperty<float>("frequency", jointSuspension2D.frequency);
			writer.WriteProperty<float>("angle", jointSuspension2D.angle);
		}

		public override object Read(ISaveGameReader reader)
		{
			JointSuspension2D jointSuspension2D = default(JointSuspension2D);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "dampingRatio"))
					{
						if (!(text == "frequency"))
						{
							if (text == "angle")
							{
								jointSuspension2D.angle = reader.ReadProperty<float>();
							}
						}
						else
						{
							jointSuspension2D.frequency = reader.ReadProperty<float>();
						}
					}
					else
					{
						jointSuspension2D.dampingRatio = reader.ReadProperty<float>();
					}
				}
			}
			return jointSuspension2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
