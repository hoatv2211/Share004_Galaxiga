using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioChorusFilter : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioChorusFilter);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioChorusFilter audioChorusFilter = (AudioChorusFilter)value;
			writer.WriteProperty<float>("dryMix", audioChorusFilter.dryMix);
			writer.WriteProperty<float>("wetMix1", audioChorusFilter.wetMix1);
			writer.WriteProperty<float>("wetMix2", audioChorusFilter.wetMix2);
			writer.WriteProperty<float>("wetMix3", audioChorusFilter.wetMix3);
			writer.WriteProperty<float>("delay", audioChorusFilter.delay);
			writer.WriteProperty<float>("rate", audioChorusFilter.rate);
			writer.WriteProperty<float>("depth", audioChorusFilter.depth);
			writer.WriteProperty<bool>("enabled", audioChorusFilter.enabled);
			writer.WriteProperty<string>("tag", audioChorusFilter.tag);
			writer.WriteProperty<string>("name", audioChorusFilter.name);
			writer.WriteProperty<HideFlags>("hideFlags", audioChorusFilter.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AudioChorusFilter audioChorusFilter = SaveGameType.CreateComponent<AudioChorusFilter>();
			this.ReadInto(audioChorusFilter, reader);
			return audioChorusFilter;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AudioChorusFilter audioChorusFilter = (AudioChorusFilter)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "dryMix":
					audioChorusFilter.dryMix = reader.ReadProperty<float>();
					break;
				case "wetMix1":
					audioChorusFilter.wetMix1 = reader.ReadProperty<float>();
					break;
				case "wetMix2":
					audioChorusFilter.wetMix2 = reader.ReadProperty<float>();
					break;
				case "wetMix3":
					audioChorusFilter.wetMix3 = reader.ReadProperty<float>();
					break;
				case "delay":
					audioChorusFilter.delay = reader.ReadProperty<float>();
					break;
				case "rate":
					audioChorusFilter.rate = reader.ReadProperty<float>();
					break;
				case "depth":
					audioChorusFilter.depth = reader.ReadProperty<float>();
					break;
				case "enabled":
					audioChorusFilter.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					audioChorusFilter.tag = reader.ReadProperty<string>();
					break;
				case "name":
					audioChorusFilter.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					audioChorusFilter.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
