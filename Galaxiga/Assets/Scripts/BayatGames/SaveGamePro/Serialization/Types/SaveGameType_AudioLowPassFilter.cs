using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioLowPassFilter : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioLowPassFilter);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioLowPassFilter audioLowPassFilter = (AudioLowPassFilter)value;
			writer.WriteProperty<float>("cutoffFrequency", audioLowPassFilter.cutoffFrequency);
			writer.WriteProperty<AnimationCurve>("customCutoffCurve", audioLowPassFilter.customCutoffCurve);
			writer.WriteProperty<float>("lowpassResonanceQ", audioLowPassFilter.lowpassResonanceQ);
			writer.WriteProperty<bool>("enabled", audioLowPassFilter.enabled);
			writer.WriteProperty<string>("tag", audioLowPassFilter.tag);
			writer.WriteProperty<string>("name", audioLowPassFilter.name);
			writer.WriteProperty<HideFlags>("hideFlags", audioLowPassFilter.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AudioLowPassFilter audioLowPassFilter = SaveGameType.CreateComponent<AudioLowPassFilter>();
			this.ReadInto(audioLowPassFilter, reader);
			return audioLowPassFilter;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AudioLowPassFilter audioLowPassFilter = (AudioLowPassFilter)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "cutoffFrequency":
					audioLowPassFilter.cutoffFrequency = reader.ReadProperty<float>();
					break;
				case "customCutoffCurve":
					audioLowPassFilter.customCutoffCurve = reader.ReadProperty<AnimationCurve>();
					break;
				case "lowpassResonanceQ":
					audioLowPassFilter.lowpassResonanceQ = reader.ReadProperty<float>();
					break;
				case "enabled":
					audioLowPassFilter.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					audioLowPassFilter.tag = reader.ReadProperty<string>();
					break;
				case "name":
					audioLowPassFilter.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					audioLowPassFilter.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
