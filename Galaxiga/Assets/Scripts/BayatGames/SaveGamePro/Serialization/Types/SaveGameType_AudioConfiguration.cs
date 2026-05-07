using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioConfiguration : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioConfiguration);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioConfiguration audioConfiguration = (AudioConfiguration)value;
			writer.WriteProperty<AudioSpeakerMode>("speakerMode", audioConfiguration.speakerMode);
			writer.WriteProperty<int>("dspBufferSize", audioConfiguration.dspBufferSize);
			writer.WriteProperty<int>("sampleRate", audioConfiguration.sampleRate);
			writer.WriteProperty<int>("numRealVoices", audioConfiguration.numRealVoices);
			writer.WriteProperty<int>("numVirtualVoices", audioConfiguration.numVirtualVoices);
		}

		public override object Read(ISaveGameReader reader)
		{
			AudioConfiguration audioConfiguration = default(AudioConfiguration);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "speakerMode"))
					{
						if (!(text == "dspBufferSize"))
						{
							if (!(text == "sampleRate"))
							{
								if (!(text == "numRealVoices"))
								{
									if (text == "numVirtualVoices")
									{
										audioConfiguration.numVirtualVoices = reader.ReadProperty<int>();
									}
								}
								else
								{
									audioConfiguration.numRealVoices = reader.ReadProperty<int>();
								}
							}
							else
							{
								audioConfiguration.sampleRate = reader.ReadProperty<int>();
							}
						}
						else
						{
							audioConfiguration.dspBufferSize = reader.ReadProperty<int>();
						}
					}
					else
					{
						audioConfiguration.speakerMode = reader.ReadProperty<AudioSpeakerMode>();
					}
				}
			}
			return audioConfiguration;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
