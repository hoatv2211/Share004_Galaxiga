using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_JointTranslationLimits2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(JointTranslationLimits2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			JointTranslationLimits2D jointTranslationLimits2D = (JointTranslationLimits2D)value;
			writer.WriteProperty<float>("min", jointTranslationLimits2D.min);
			writer.WriteProperty<float>("max", jointTranslationLimits2D.max);
		}

		public override object Read(ISaveGameReader reader)
		{
			JointTranslationLimits2D jointTranslationLimits2D = default(JointTranslationLimits2D);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "min"))
					{
						if (text == "max")
						{
							jointTranslationLimits2D.max = reader.ReadProperty<float>();
						}
					}
					else
					{
						jointTranslationLimits2D.min = reader.ReadProperty<float>();
					}
				}
			}
			return jointTranslationLimits2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
