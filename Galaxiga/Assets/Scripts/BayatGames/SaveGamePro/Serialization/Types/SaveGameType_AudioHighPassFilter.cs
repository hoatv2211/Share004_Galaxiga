using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioHighPassFilter : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioHighPassFilter);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioHighPassFilter audioHighPassFilter = (AudioHighPassFilter)value;
			writer.WriteProperty<float>("cutoffFrequency", audioHighPassFilter.cutoffFrequency);
			writer.WriteProperty<float>("highpassResonanceQ", audioHighPassFilter.highpassResonanceQ);
			writer.WriteProperty<bool>("enabled", audioHighPassFilter.enabled);
			writer.WriteProperty<string>("tag", audioHighPassFilter.tag);
			writer.WriteProperty<string>("name", audioHighPassFilter.name);
			writer.WriteProperty<HideFlags>("hideFlags", audioHighPassFilter.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AudioHighPassFilter audioHighPassFilter = SaveGameType.CreateComponent<AudioHighPassFilter>();
			this.ReadInto(audioHighPassFilter, reader);
			return audioHighPassFilter;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AudioHighPassFilter audioHighPassFilter = (AudioHighPassFilter)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "cutoffFrequency"))
					{
						if (!(text == "highpassResonanceQ"))
						{
							if (!(text == "enabled"))
							{
								if (!(text == "tag"))
								{
									if (!(text == "name"))
									{
										if (text == "hideFlags")
										{
											audioHighPassFilter.hideFlags = reader.ReadProperty<HideFlags>();
										}
									}
									else
									{
										audioHighPassFilter.name = reader.ReadProperty<string>();
									}
								}
								else
								{
									audioHighPassFilter.tag = reader.ReadProperty<string>();
								}
							}
							else
							{
								audioHighPassFilter.enabled = reader.ReadProperty<bool>();
							}
						}
						else
						{
							audioHighPassFilter.highpassResonanceQ = reader.ReadProperty<float>();
						}
					}
					else
					{
						audioHighPassFilter.cutoffFrequency = reader.ReadProperty<float>();
					}
				}
			}
		}
	}
}
