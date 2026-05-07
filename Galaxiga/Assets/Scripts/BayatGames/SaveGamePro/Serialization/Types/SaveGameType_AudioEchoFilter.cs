using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioEchoFilter : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioEchoFilter);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioEchoFilter audioEchoFilter = (AudioEchoFilter)value;
			writer.WriteProperty<float>("delay", audioEchoFilter.delay);
			writer.WriteProperty<float>("decayRatio", audioEchoFilter.decayRatio);
			writer.WriteProperty<float>("dryMix", audioEchoFilter.dryMix);
			writer.WriteProperty<float>("wetMix", audioEchoFilter.wetMix);
			writer.WriteProperty<bool>("enabled", audioEchoFilter.enabled);
			writer.WriteProperty<string>("tag", audioEchoFilter.tag);
			writer.WriteProperty<string>("name", audioEchoFilter.name);
			writer.WriteProperty<HideFlags>("hideFlags", audioEchoFilter.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AudioEchoFilter audioEchoFilter = SaveGameType.CreateComponent<AudioEchoFilter>();
			this.ReadInto(audioEchoFilter, reader);
			return audioEchoFilter;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AudioEchoFilter audioEchoFilter = (AudioEchoFilter)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "delay":
					audioEchoFilter.delay = reader.ReadProperty<float>();
					break;
				case "decayRatio":
					audioEchoFilter.decayRatio = reader.ReadProperty<float>();
					break;
				case "dryMix":
					audioEchoFilter.dryMix = reader.ReadProperty<float>();
					break;
				case "wetMix":
					audioEchoFilter.wetMix = reader.ReadProperty<float>();
					break;
				case "enabled":
					audioEchoFilter.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					audioEchoFilter.tag = reader.ReadProperty<string>();
					break;
				case "name":
					audioEchoFilter.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					audioEchoFilter.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
