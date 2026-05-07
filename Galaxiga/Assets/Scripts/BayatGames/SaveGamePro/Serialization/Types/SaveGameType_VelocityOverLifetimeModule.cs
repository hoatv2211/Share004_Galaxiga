using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_VelocityOverLifetimeModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.VelocityOverLifetimeModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = (ParticleSystem.VelocityOverLifetimeModule)value;
			writer.WriteProperty<bool>("enabled", velocityOverLifetimeModule.enabled);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("x", velocityOverLifetimeModule.x);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("y", velocityOverLifetimeModule.y);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("z", velocityOverLifetimeModule.z);
			writer.WriteProperty<float>("xMultiplier", velocityOverLifetimeModule.xMultiplier);
			writer.WriteProperty<float>("yMultiplier", velocityOverLifetimeModule.yMultiplier);
			writer.WriteProperty<float>("zMultiplier", velocityOverLifetimeModule.zMultiplier);
			writer.WriteProperty<ParticleSystemSimulationSpace>("space", velocityOverLifetimeModule.space);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = default(ParticleSystem.VelocityOverLifetimeModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					velocityOverLifetimeModule.enabled = reader.ReadProperty<bool>();
					break;
				case "x":
					velocityOverLifetimeModule.x = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "y":
					velocityOverLifetimeModule.y = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "z":
					velocityOverLifetimeModule.z = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "xMultiplier":
					velocityOverLifetimeModule.xMultiplier = reader.ReadProperty<float>();
					break;
				case "yMultiplier":
					velocityOverLifetimeModule.yMultiplier = reader.ReadProperty<float>();
					break;
				case "zMultiplier":
					velocityOverLifetimeModule.zMultiplier = reader.ReadProperty<float>();
					break;
				case "space":
					velocityOverLifetimeModule.space = reader.ReadProperty<ParticleSystemSimulationSpace>();
					break;
				}
			}
			return velocityOverLifetimeModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
