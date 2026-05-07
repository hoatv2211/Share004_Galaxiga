using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Avatar : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Avatar);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Avatar avatar = (Avatar)value;
			writer.WriteProperty<string>("name", avatar.name);
			writer.WriteProperty<HideFlags>("hideFlags", avatar.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			return base.Read(reader);
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Avatar avatar = (Avatar)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "name"))
					{
						if (text == "hideFlags")
						{
							avatar.hideFlags = reader.ReadProperty<HideFlags>();
						}
					}
					else
					{
						avatar.name = reader.ReadProperty<string>();
					}
				}
			}
		}
	}
}
