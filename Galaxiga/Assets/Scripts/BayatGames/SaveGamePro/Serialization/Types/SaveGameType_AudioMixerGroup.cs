using System;
using UnityEngine;
using UnityEngine.Audio;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioMixerGroup : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioMixerGroup);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioMixerGroup audioMixerGroup = (AudioMixerGroup)value;
			writer.WriteProperty<string>("name", audioMixerGroup.name);
			writer.WriteProperty<HideFlags>("hideFlags", audioMixerGroup.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			return base.Read(reader);
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AudioMixerGroup audioMixerGroup = (AudioMixerGroup)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "name"))
					{
						if (text == "hideFlags")
						{
							audioMixerGroup.hideFlags = reader.ReadProperty<HideFlags>();
						}
					}
					else
					{
						audioMixerGroup.name = reader.ReadProperty<string>();
					}
				}
			}
		}
	}
}
