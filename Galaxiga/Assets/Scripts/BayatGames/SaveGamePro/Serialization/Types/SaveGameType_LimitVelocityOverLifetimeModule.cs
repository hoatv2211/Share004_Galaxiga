using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_LimitVelocityOverLifetimeModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.LimitVelocityOverLifetimeModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetimeModule = (ParticleSystem.LimitVelocityOverLifetimeModule)value;
			writer.WriteProperty<bool>("enabled", limitVelocityOverLifetimeModule.enabled);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("limitX", limitVelocityOverLifetimeModule.limitX);
			writer.WriteProperty<float>("limitXMultiplier", limitVelocityOverLifetimeModule.limitXMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("limitY", limitVelocityOverLifetimeModule.limitY);
			writer.WriteProperty<float>("limitYMultiplier", limitVelocityOverLifetimeModule.limitYMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("limitZ", limitVelocityOverLifetimeModule.limitZ);
			writer.WriteProperty<float>("limitZMultiplier", limitVelocityOverLifetimeModule.limitZMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("limit", limitVelocityOverLifetimeModule.limit);
			writer.WriteProperty<float>("limitMultiplier", limitVelocityOverLifetimeModule.limitMultiplier);
			writer.WriteProperty<float>("dampen", limitVelocityOverLifetimeModule.dampen);
			writer.WriteProperty<bool>("separateAxes", limitVelocityOverLifetimeModule.separateAxes);
			writer.WriteProperty<ParticleSystemSimulationSpace>("space", limitVelocityOverLifetimeModule.space);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetimeModule = default(ParticleSystem.LimitVelocityOverLifetimeModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					limitVelocityOverLifetimeModule.enabled = reader.ReadProperty<bool>();
					break;
				case "limitX":
					limitVelocityOverLifetimeModule.limitX = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "limitXMultiplier":
					limitVelocityOverLifetimeModule.limitXMultiplier = reader.ReadProperty<float>();
					break;
				case "limitY":
					limitVelocityOverLifetimeModule.limitY = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "limitYMultiplier":
					limitVelocityOverLifetimeModule.limitYMultiplier = reader.ReadProperty<float>();
					break;
				case "limitZ":
					limitVelocityOverLifetimeModule.limitZ = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "limitZMultiplier":
					limitVelocityOverLifetimeModule.limitZMultiplier = reader.ReadProperty<float>();
					break;
				case "limit":
					limitVelocityOverLifetimeModule.limit = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "limitMultiplier":
					limitVelocityOverLifetimeModule.limitMultiplier = reader.ReadProperty<float>();
					break;
				case "dampen":
					limitVelocityOverLifetimeModule.dampen = reader.ReadProperty<float>();
					break;
				case "separateAxes":
					limitVelocityOverLifetimeModule.separateAxes = reader.ReadProperty<bool>();
					break;
				case "space":
					limitVelocityOverLifetimeModule.space = reader.ReadProperty<ParticleSystemSimulationSpace>();
					break;
				}
			}
			return limitVelocityOverLifetimeModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
