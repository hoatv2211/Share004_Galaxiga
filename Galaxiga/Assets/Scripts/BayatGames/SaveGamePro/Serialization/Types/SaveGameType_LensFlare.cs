using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_LensFlare : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(LensFlare);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			LensFlare lensFlare = (LensFlare)value;
			writer.WriteProperty<Flare>("flare", lensFlare.flare);
			writer.WriteProperty<float>("brightness", lensFlare.brightness);
			writer.WriteProperty<float>("fadeSpeed", lensFlare.fadeSpeed);
			writer.WriteProperty<Color>("color", lensFlare.color);
			writer.WriteProperty<bool>("enabled", lensFlare.enabled);
			writer.WriteProperty<string>("tag", lensFlare.tag);
			writer.WriteProperty<string>("name", lensFlare.name);
			writer.WriteProperty<HideFlags>("hideFlags", lensFlare.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			LensFlare lensFlare = SaveGameType.CreateComponent<LensFlare>();
			this.ReadInto(lensFlare, reader);
			return lensFlare;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			LensFlare lensFlare = (LensFlare)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "flare":
					if (lensFlare.flare == null)
					{
						lensFlare.flare = reader.ReadProperty<Flare>();
					}
					else
					{
						reader.ReadIntoProperty<Flare>(lensFlare.flare);
					}
					break;
				case "brightness":
					lensFlare.brightness = reader.ReadProperty<float>();
					break;
				case "fadeSpeed":
					lensFlare.fadeSpeed = reader.ReadProperty<float>();
					break;
				case "color":
					lensFlare.color = reader.ReadProperty<Color>();
					break;
				case "enabled":
					lensFlare.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					lensFlare.tag = reader.ReadProperty<string>();
					break;
				case "name":
					lensFlare.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					lensFlare.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
