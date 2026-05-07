using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_TrailModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.TrailModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.TrailModule trailModule = (ParticleSystem.TrailModule)value;
			writer.WriteProperty<bool>("enabled", trailModule.enabled);
			writer.WriteProperty<float>("ratio", trailModule.ratio);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("lifetime", trailModule.lifetime);
			writer.WriteProperty<float>("lifetimeMultiplier", trailModule.lifetimeMultiplier);
			writer.WriteProperty<float>("minVertexDistance", trailModule.minVertexDistance);
			writer.WriteProperty<ParticleSystemTrailTextureMode>("textureMode", trailModule.textureMode);
			writer.WriteProperty<bool>("worldSpace", trailModule.worldSpace);
			writer.WriteProperty<bool>("dieWithParticles", trailModule.dieWithParticles);
			writer.WriteProperty<bool>("sizeAffectsWidth", trailModule.sizeAffectsWidth);
			writer.WriteProperty<bool>("sizeAffectsLifetime", trailModule.sizeAffectsLifetime);
			writer.WriteProperty<bool>("inheritParticleColor", trailModule.inheritParticleColor);
			writer.WriteProperty<ParticleSystem.MinMaxGradient>("colorOverLifetime", trailModule.colorOverLifetime);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("widthOverTrail", trailModule.widthOverTrail);
			writer.WriteProperty<float>("widthOverTrailMultiplier", trailModule.widthOverTrailMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxGradient>("colorOverTrail", trailModule.colorOverTrail);
			writer.WriteProperty<bool>("generateLightingData", trailModule.generateLightingData);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.TrailModule trailModule = default(ParticleSystem.TrailModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					trailModule.enabled = reader.ReadProperty<bool>();
					break;
				case "ratio":
					trailModule.ratio = reader.ReadProperty<float>();
					break;
				case "lifetime":
					trailModule.lifetime = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "lifetimeMultiplier":
					trailModule.lifetimeMultiplier = reader.ReadProperty<float>();
					break;
				case "minVertexDistance":
					trailModule.minVertexDistance = reader.ReadProperty<float>();
					break;
				case "textureMode":
					trailModule.textureMode = reader.ReadProperty<ParticleSystemTrailTextureMode>();
					break;
				case "worldSpace":
					trailModule.worldSpace = reader.ReadProperty<bool>();
					break;
				case "dieWithParticles":
					trailModule.dieWithParticles = reader.ReadProperty<bool>();
					break;
				case "sizeAffectsWidth":
					trailModule.sizeAffectsWidth = reader.ReadProperty<bool>();
					break;
				case "sizeAffectsLifetime":
					trailModule.sizeAffectsLifetime = reader.ReadProperty<bool>();
					break;
				case "inheritParticleColor":
					trailModule.inheritParticleColor = reader.ReadProperty<bool>();
					break;
				case "colorOverLifetime":
					trailModule.colorOverLifetime = reader.ReadProperty<ParticleSystem.MinMaxGradient>();
					break;
				case "widthOverTrail":
					trailModule.widthOverTrail = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "widthOverTrailMultiplier":
					trailModule.widthOverTrailMultiplier = reader.ReadProperty<float>();
					break;
				case "colorOverTrail":
					trailModule.colorOverTrail = reader.ReadProperty<ParticleSystem.MinMaxGradient>();
					break;
				case "generateLightingData":
					trailModule.generateLightingData = reader.ReadProperty<bool>();
					break;
				}
			}
			return trailModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
