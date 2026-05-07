using System;
using UnityEngine;
using UnityEngine.Audio;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioMixer : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioMixer);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioMixer audioMixer = (AudioMixer)value;
			writer.WriteProperty<AudioMixerGroup>("outputAudioMixerGroup", audioMixer.outputAudioMixerGroup);
			writer.WriteProperty<AudioMixerUpdateMode>("updateMode", audioMixer.updateMode);
			writer.WriteProperty<string>("name", audioMixer.name);
			writer.WriteProperty<HideFlags>("hideFlags", audioMixer.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			return base.Read(reader);
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AudioMixer audioMixer = (AudioMixer)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "outputAudioMixerGroup"))
					{
						if (!(text == "updateMode"))
						{
							if (!(text == "name"))
							{
								if (text == "hideFlags")
								{
									audioMixer.hideFlags = reader.ReadProperty<HideFlags>();
								}
							}
							else
							{
								audioMixer.name = reader.ReadProperty<string>();
							}
						}
						else
						{
							audioMixer.updateMode = reader.ReadProperty<AudioMixerUpdateMode>();
						}
					}
					else if (audioMixer.outputAudioMixerGroup == null)
					{
						audioMixer.outputAudioMixerGroup = reader.ReadProperty<AudioMixerGroup>();
					}
					else
					{
						reader.ReadIntoProperty<AudioMixerGroup>(audioMixer.outputAudioMixerGroup);
					}
				}
			}
		}
	}
}
