using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ForceOverLifetimeModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.ForceOverLifetimeModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.ForceOverLifetimeModule forceOverLifetimeModule = (ParticleSystem.ForceOverLifetimeModule)value;
			writer.WriteProperty<bool>("enabled", forceOverLifetimeModule.enabled);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("x", forceOverLifetimeModule.x);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("y", forceOverLifetimeModule.y);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("z", forceOverLifetimeModule.z);
			writer.WriteProperty<float>("xMultiplier", forceOverLifetimeModule.xMultiplier);
			writer.WriteProperty<float>("yMultiplier", forceOverLifetimeModule.yMultiplier);
			writer.WriteProperty<float>("zMultiplier", forceOverLifetimeModule.zMultiplier);
			writer.WriteProperty<ParticleSystemSimulationSpace>("space", forceOverLifetimeModule.space);
			writer.WriteProperty<bool>("randomized", forceOverLifetimeModule.randomized);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.ForceOverLifetimeModule forceOverLifetimeModule = default(ParticleSystem.ForceOverLifetimeModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					forceOverLifetimeModule.enabled = reader.ReadProperty<bool>();
					break;
				case "x":
					forceOverLifetimeModule.x = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "y":
					forceOverLifetimeModule.y = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "z":
					forceOverLifetimeModule.z = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "xMultiplier":
					forceOverLifetimeModule.xMultiplier = reader.ReadProperty<float>();
					break;
				case "yMultiplier":
					forceOverLifetimeModule.yMultiplier = reader.ReadProperty<float>();
					break;
				case "zMultiplier":
					forceOverLifetimeModule.zMultiplier = reader.ReadProperty<float>();
					break;
				case "space":
					forceOverLifetimeModule.space = reader.ReadProperty<ParticleSystemSimulationSpace>();
					break;
				case "randomized":
					forceOverLifetimeModule.randomized = reader.ReadProperty<bool>();
					break;
				}
			}
			return forceOverLifetimeModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
