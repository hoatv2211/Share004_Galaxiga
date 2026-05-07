using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioDistortionFilter : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioDistortionFilter);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioDistortionFilter audioDistortionFilter = (AudioDistortionFilter)value;
			writer.WriteProperty<float>("distortionLevel", audioDistortionFilter.distortionLevel);
			writer.WriteProperty<bool>("enabled", audioDistortionFilter.enabled);
			writer.WriteProperty<string>("tag", audioDistortionFilter.tag);
			writer.WriteProperty<string>("name", audioDistortionFilter.name);
			writer.WriteProperty<HideFlags>("hideFlags", audioDistortionFilter.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AudioDistortionFilter audioDistortionFilter = SaveGameType.CreateComponent<AudioDistortionFilter>();
			this.ReadInto(audioDistortionFilter, reader);
			return audioDistortionFilter;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AudioDistortionFilter audioDistortionFilter = (AudioDistortionFilter)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "distortionLevel"))
					{
						if (!(text == "enabled"))
						{
							if (!(text == "tag"))
							{
								if (!(text == "name"))
								{
									if (text == "hideFlags")
									{
										audioDistortionFilter.hideFlags = reader.ReadProperty<HideFlags>();
									}
								}
								else
								{
									audioDistortionFilter.name = reader.ReadProperty<string>();
								}
							}
							else
							{
								audioDistortionFilter.tag = reader.ReadProperty<string>();
							}
						}
						else
						{
							audioDistortionFilter.enabled = reader.ReadProperty<bool>();
						}
					}
					else
					{
						audioDistortionFilter.distortionLevel = reader.ReadProperty<float>();
					}
				}
			}
		}
	}
}
