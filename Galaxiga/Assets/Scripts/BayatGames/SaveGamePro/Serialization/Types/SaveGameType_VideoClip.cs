using System;
using UnityEngine;
using UnityEngine.Video;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_VideoClip : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(VideoClip);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			VideoClip videoClip = (VideoClip)value;
			writer.WriteProperty<string>("name", videoClip.name);
			writer.WriteProperty<HideFlags>("hideFlags", videoClip.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			return base.Read(reader);
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			VideoClip videoClip = (VideoClip)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "name"))
					{
						if (text == "hideFlags")
						{
							videoClip.hideFlags = reader.ReadProperty<HideFlags>();
						}
					}
					else
					{
						videoClip.name = reader.ReadProperty<string>();
					}
				}
			}
		}
	}
}
