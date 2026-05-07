using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_WindZone : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(WindZone);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			WindZone windZone = (WindZone)value;
			writer.WriteProperty<WindZoneMode>("mode", windZone.mode);
			writer.WriteProperty<float>("radius", windZone.radius);
			writer.WriteProperty<float>("windMain", windZone.windMain);
			writer.WriteProperty<float>("windTurbulence", windZone.windTurbulence);
			writer.WriteProperty<float>("windPulseMagnitude", windZone.windPulseMagnitude);
			writer.WriteProperty<float>("windPulseFrequency", windZone.windPulseFrequency);
			writer.WriteProperty<string>("tag", windZone.tag);
			writer.WriteProperty<string>("name", windZone.name);
			writer.WriteProperty<HideFlags>("hideFlags", windZone.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			WindZone windZone = SaveGameType.CreateComponent<WindZone>();
			this.ReadInto(windZone, reader);
			return windZone;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			WindZone windZone = (WindZone)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "mode":
					windZone.mode = reader.ReadProperty<WindZoneMode>();
					break;
				case "radius":
					windZone.radius = reader.ReadProperty<float>();
					break;
				case "windMain":
					windZone.windMain = reader.ReadProperty<float>();
					break;
				case "windTurbulence":
					windZone.windTurbulence = reader.ReadProperty<float>();
					break;
				case "windPulseMagnitude":
					windZone.windPulseMagnitude = reader.ReadProperty<float>();
					break;
				case "windPulseFrequency":
					windZone.windPulseFrequency = reader.ReadProperty<float>();
					break;
				case "tag":
					windZone.tag = reader.ReadProperty<string>();
					break;
				case "name":
					windZone.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					windZone.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
