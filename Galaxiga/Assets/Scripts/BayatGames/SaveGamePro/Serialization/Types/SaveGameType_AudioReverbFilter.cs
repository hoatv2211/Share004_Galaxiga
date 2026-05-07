using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioReverbFilter : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioReverbFilter);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioReverbFilter audioReverbFilter = (AudioReverbFilter)value;
			writer.WriteProperty<AudioReverbPreset>("reverbPreset", audioReverbFilter.reverbPreset);
			writer.WriteProperty<float>("dryLevel", audioReverbFilter.dryLevel);
			writer.WriteProperty<float>("room", audioReverbFilter.room);
			writer.WriteProperty<float>("roomHF", audioReverbFilter.roomHF);
			writer.WriteProperty<float>("decayTime", audioReverbFilter.decayTime);
			writer.WriteProperty<float>("decayHFRatio", audioReverbFilter.decayHFRatio);
			writer.WriteProperty<float>("reflectionsLevel", audioReverbFilter.reflectionsLevel);
			writer.WriteProperty<float>("reflectionsDelay", audioReverbFilter.reflectionsDelay);
			writer.WriteProperty<float>("reverbLevel", audioReverbFilter.reverbLevel);
			writer.WriteProperty<float>("reverbDelay", audioReverbFilter.reverbDelay);
			writer.WriteProperty<float>("diffusion", audioReverbFilter.diffusion);
			writer.WriteProperty<float>("density", audioReverbFilter.density);
			writer.WriteProperty<float>("hfReference", audioReverbFilter.hfReference);
			writer.WriteProperty<float>("roomLF", audioReverbFilter.roomLF);
			writer.WriteProperty<float>("lfReference", audioReverbFilter.lfReference);
			writer.WriteProperty<bool>("enabled", audioReverbFilter.enabled);
			writer.WriteProperty<string>("tag", audioReverbFilter.tag);
			writer.WriteProperty<string>("name", audioReverbFilter.name);
			writer.WriteProperty<HideFlags>("hideFlags", audioReverbFilter.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AudioReverbFilter audioReverbFilter = SaveGameType.CreateComponent<AudioReverbFilter>();
			this.ReadInto(audioReverbFilter, reader);
			return audioReverbFilter;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AudioReverbFilter audioReverbFilter = (AudioReverbFilter)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "reverbPreset":
					audioReverbFilter.reverbPreset = reader.ReadProperty<AudioReverbPreset>();
					break;
				case "dryLevel":
					audioReverbFilter.dryLevel = reader.ReadProperty<float>();
					break;
				case "room":
					audioReverbFilter.room = reader.ReadProperty<float>();
					break;
				case "roomHF":
					audioReverbFilter.roomHF = reader.ReadProperty<float>();
					break;
				case "decayTime":
					audioReverbFilter.decayTime = reader.ReadProperty<float>();
					break;
				case "decayHFRatio":
					audioReverbFilter.decayHFRatio = reader.ReadProperty<float>();
					break;
				case "reflectionsLevel":
					audioReverbFilter.reflectionsLevel = reader.ReadProperty<float>();
					break;
				case "reflectionsDelay":
					audioReverbFilter.reflectionsDelay = reader.ReadProperty<float>();
					break;
				case "reverbLevel":
					audioReverbFilter.reverbLevel = reader.ReadProperty<float>();
					break;
				case "reverbDelay":
					audioReverbFilter.reverbDelay = reader.ReadProperty<float>();
					break;
				case "diffusion":
					audioReverbFilter.diffusion = reader.ReadProperty<float>();
					break;
				case "density":
					audioReverbFilter.density = reader.ReadProperty<float>();
					break;
				case "hfReference":
					audioReverbFilter.hfReference = reader.ReadProperty<float>();
					break;
				case "roomLF":
					audioReverbFilter.roomLF = reader.ReadProperty<float>();
					break;
				case "lfReference":
					audioReverbFilter.lfReference = reader.ReadProperty<float>();
					break;
				case "enabled":
					audioReverbFilter.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					audioReverbFilter.tag = reader.ReadProperty<string>();
					break;
				case "name":
					audioReverbFilter.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					audioReverbFilter.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
