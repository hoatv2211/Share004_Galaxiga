using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AvatarMask : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AvatarMask);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AvatarMask avatarMask = (AvatarMask)value;
			writer.WriteProperty<int>("transformCount", avatarMask.transformCount);
			writer.WriteProperty<string>("name", avatarMask.name);
			writer.WriteProperty<HideFlags>("hideFlags", avatarMask.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AvatarMask avatarMask = new AvatarMask();
			this.ReadInto(avatarMask, reader);
			return avatarMask;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AvatarMask avatarMask = (AvatarMask)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "transformCount"))
					{
						if (!(text == "name"))
						{
							if (text == "hideFlags")
							{
								avatarMask.hideFlags = reader.ReadProperty<HideFlags>();
							}
						}
						else
						{
							avatarMask.name = reader.ReadProperty<string>();
						}
					}
					else
					{
						avatarMask.transformCount = reader.ReadProperty<int>();
					}
				}
			}
		}
	}
}
