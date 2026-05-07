using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioReverbZone : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioReverbZone);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioReverbZone audioReverbZone = (AudioReverbZone)value;
			writer.WriteProperty<float>("minDistance", audioReverbZone.minDistance);
			writer.WriteProperty<float>("maxDistance", audioReverbZone.maxDistance);
			writer.WriteProperty<AudioReverbPreset>("reverbPreset", audioReverbZone.reverbPreset);
			writer.WriteProperty<int>("room", audioReverbZone.room);
			writer.WriteProperty<int>("roomHF", audioReverbZone.roomHF);
			writer.WriteProperty<int>("roomLF", audioReverbZone.roomLF);
			writer.WriteProperty<float>("decayTime", audioReverbZone.decayTime);
			writer.WriteProperty<float>("decayHFRatio", audioReverbZone.decayHFRatio);
			writer.WriteProperty<int>("reflections", audioReverbZone.reflections);
			writer.WriteProperty<float>("reflectionsDelay", audioReverbZone.reflectionsDelay);
			writer.WriteProperty<int>("reverb", audioReverbZone.reverb);
			writer.WriteProperty<float>("reverbDelay", audioReverbZone.reverbDelay);
			writer.WriteProperty<float>("HFReference", audioReverbZone.HFReference);
			writer.WriteProperty<float>("LFReference", audioReverbZone.LFReference);
			writer.WriteProperty<float>("diffusion", audioReverbZone.diffusion);
			writer.WriteProperty<float>("density", audioReverbZone.density);
			writer.WriteProperty<bool>("enabled", audioReverbZone.enabled);
			writer.WriteProperty<string>("tag", audioReverbZone.tag);
			writer.WriteProperty<string>("name", audioReverbZone.name);
			writer.WriteProperty<HideFlags>("hideFlags", audioReverbZone.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AudioReverbZone audioReverbZone = SaveGameType.CreateComponent<AudioReverbZone>();
			this.ReadInto(audioReverbZone, reader);
			return audioReverbZone;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AudioReverbZone audioReverbZone = (AudioReverbZone)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "minDistance":
					audioReverbZone.minDistance = reader.ReadProperty<float>();
					break;
				case "maxDistance":
					audioReverbZone.maxDistance = reader.ReadProperty<float>();
					break;
				case "reverbPreset":
					audioReverbZone.reverbPreset = reader.ReadProperty<AudioReverbPreset>();
					break;
				case "room":
					audioReverbZone.room = reader.ReadProperty<int>();
					break;
				case "roomHF":
					audioReverbZone.roomHF = reader.ReadProperty<int>();
					break;
				case "roomLF":
					audioReverbZone.roomLF = reader.ReadProperty<int>();
					break;
				case "decayTime":
					audioReverbZone.decayTime = reader.ReadProperty<float>();
					break;
				case "decayHFRatio":
					audioReverbZone.decayHFRatio = reader.ReadProperty<float>();
					break;
				case "reflections":
					audioReverbZone.reflections = reader.ReadProperty<int>();
					break;
				case "reflectionsDelay":
					audioReverbZone.reflectionsDelay = reader.ReadProperty<float>();
					break;
				case "reverb":
					audioReverbZone.reverb = reader.ReadProperty<int>();
					break;
				case "reverbDelay":
					audioReverbZone.reverbDelay = reader.ReadProperty<float>();
					break;
				case "HFReference":
					audioReverbZone.HFReference = reader.ReadProperty<float>();
					break;
				case "LFReference":
					audioReverbZone.LFReference = reader.ReadProperty<float>();
					break;
				case "diffusion":
					audioReverbZone.diffusion = reader.ReadProperty<float>();
					break;
				case "density":
					audioReverbZone.density = reader.ReadProperty<float>();
					break;
				case "enabled":
					audioReverbZone.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					audioReverbZone.tag = reader.ReadProperty<string>();
					break;
				case "name":
					audioReverbZone.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					audioReverbZone.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
