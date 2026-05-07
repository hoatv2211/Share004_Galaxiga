using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Motion : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Motion);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Motion motion = (Motion)value;
			writer.WriteProperty<string>("name", motion.name);
			writer.WriteProperty<HideFlags>("hideFlags", motion.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			return base.Read(reader);
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Motion motion = (Motion)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "name"))
					{
						if (text == "hideFlags")
						{
							motion.hideFlags = reader.ReadProperty<HideFlags>();
						}
					}
					else
					{
						motion.name = reader.ReadProperty<string>();
					}
				}
			}
		}
	}
}
