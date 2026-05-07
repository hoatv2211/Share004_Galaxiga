using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_LightsModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.LightsModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.LightsModule lightsModule = (ParticleSystem.LightsModule)value;
			writer.WriteProperty<bool>("enabled", lightsModule.enabled);
			writer.WriteProperty<float>("ratio", lightsModule.ratio);
			writer.WriteProperty<bool>("useRandomDistribution", lightsModule.useRandomDistribution);
			writer.WriteProperty<Light>("light", lightsModule.light);
			writer.WriteProperty<bool>("useParticleColor", lightsModule.useParticleColor);
			writer.WriteProperty<bool>("sizeAffectsRange", lightsModule.sizeAffectsRange);
			writer.WriteProperty<bool>("alphaAffectsIntensity", lightsModule.alphaAffectsIntensity);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("range", lightsModule.range);
			writer.WriteProperty<float>("rangeMultiplier", lightsModule.rangeMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("intensity", lightsModule.intensity);
			writer.WriteProperty<float>("intensityMultiplier", lightsModule.intensityMultiplier);
			writer.WriteProperty<int>("maxLights", lightsModule.maxLights);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.LightsModule lightsModule = default(ParticleSystem.LightsModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					lightsModule.enabled = reader.ReadProperty<bool>();
					break;
				case "ratio":
					lightsModule.ratio = reader.ReadProperty<float>();
					break;
				case "useRandomDistribution":
					lightsModule.useRandomDistribution = reader.ReadProperty<bool>();
					break;
				case "light":
					if (lightsModule.light == null)
					{
						lightsModule.light = reader.ReadProperty<Light>();
					}
					else
					{
						reader.ReadIntoProperty<Light>(lightsModule.light);
					}
					break;
				case "useParticleColor":
					lightsModule.useParticleColor = reader.ReadProperty<bool>();
					break;
				case "sizeAffectsRange":
					lightsModule.sizeAffectsRange = reader.ReadProperty<bool>();
					break;
				case "alphaAffectsIntensity":
					lightsModule.alphaAffectsIntensity = reader.ReadProperty<bool>();
					break;
				case "range":
					lightsModule.range = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "rangeMultiplier":
					lightsModule.rangeMultiplier = reader.ReadProperty<float>();
					break;
				case "intensity":
					lightsModule.intensity = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "intensityMultiplier":
					lightsModule.intensityMultiplier = reader.ReadProperty<float>();
					break;
				case "maxLights":
					lightsModule.maxLights = reader.ReadProperty<int>();
					break;
				}
			}
			return lightsModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
