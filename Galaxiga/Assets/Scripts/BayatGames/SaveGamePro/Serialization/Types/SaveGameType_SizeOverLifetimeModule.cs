using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SizeOverLifetimeModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.SizeOverLifetimeModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.SizeOverLifetimeModule sizeOverLifetimeModule = (ParticleSystem.SizeOverLifetimeModule)value;
			writer.WriteProperty<bool>("enabled", sizeOverLifetimeModule.enabled);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("size", sizeOverLifetimeModule.size);
			writer.WriteProperty<float>("sizeMultiplier", sizeOverLifetimeModule.sizeMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("x", sizeOverLifetimeModule.x);
			writer.WriteProperty<float>("xMultiplier", sizeOverLifetimeModule.xMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("y", sizeOverLifetimeModule.y);
			writer.WriteProperty<float>("yMultiplier", sizeOverLifetimeModule.yMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("z", sizeOverLifetimeModule.z);
			writer.WriteProperty<float>("zMultiplier", sizeOverLifetimeModule.zMultiplier);
			writer.WriteProperty<bool>("separateAxes", sizeOverLifetimeModule.separateAxes);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.SizeOverLifetimeModule sizeOverLifetimeModule = default(ParticleSystem.SizeOverLifetimeModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					sizeOverLifetimeModule.enabled = reader.ReadProperty<bool>();
					break;
				case "size":
					sizeOverLifetimeModule.size = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "sizeMultiplier":
					sizeOverLifetimeModule.sizeMultiplier = reader.ReadProperty<float>();
					break;
				case "x":
					sizeOverLifetimeModule.x = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "xMultiplier":
					sizeOverLifetimeModule.xMultiplier = reader.ReadProperty<float>();
					break;
				case "y":
					sizeOverLifetimeModule.y = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "yMultiplier":
					sizeOverLifetimeModule.yMultiplier = reader.ReadProperty<float>();
					break;
				case "z":
					sizeOverLifetimeModule.z = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "zMultiplier":
					sizeOverLifetimeModule.zMultiplier = reader.ReadProperty<float>();
					break;
				case "separateAxes":
					sizeOverLifetimeModule.separateAxes = reader.ReadProperty<bool>();
					break;
				}
			}
			return sizeOverLifetimeModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
