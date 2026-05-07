using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_NoiseModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.NoiseModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.NoiseModule noiseModule = (ParticleSystem.NoiseModule)value;
			writer.WriteProperty<bool>("enabled", noiseModule.enabled);
			writer.WriteProperty<bool>("separateAxes", noiseModule.separateAxes);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("strength", noiseModule.strength);
			writer.WriteProperty<float>("strengthMultiplier", noiseModule.strengthMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("strengthX", noiseModule.strengthX);
			writer.WriteProperty<float>("strengthXMultiplier", noiseModule.strengthXMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("strengthY", noiseModule.strengthY);
			writer.WriteProperty<float>("strengthYMultiplier", noiseModule.strengthYMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("strengthZ", noiseModule.strengthZ);
			writer.WriteProperty<float>("strengthZMultiplier", noiseModule.strengthZMultiplier);
			writer.WriteProperty<float>("frequency", noiseModule.frequency);
			writer.WriteProperty<bool>("damping", noiseModule.damping);
			writer.WriteProperty<int>("octaveCount", noiseModule.octaveCount);
			writer.WriteProperty<float>("octaveMultiplier", noiseModule.octaveMultiplier);
			writer.WriteProperty<float>("octaveScale", noiseModule.octaveScale);
			writer.WriteProperty<ParticleSystemNoiseQuality>("quality", noiseModule.quality);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("scrollSpeed", noiseModule.scrollSpeed);
			writer.WriteProperty<float>("scrollSpeedMultiplier", noiseModule.scrollSpeedMultiplier);
			writer.WriteProperty<bool>("remapEnabled", noiseModule.remapEnabled);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("remap", noiseModule.remap);
			writer.WriteProperty<float>("remapMultiplier", noiseModule.remapMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("remapX", noiseModule.remapX);
			writer.WriteProperty<float>("remapXMultiplier", noiseModule.remapXMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("remapY", noiseModule.remapY);
			writer.WriteProperty<float>("remapYMultiplier", noiseModule.remapYMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("remapZ", noiseModule.remapZ);
			writer.WriteProperty<float>("remapZMultiplier", noiseModule.remapZMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("positionAmount", noiseModule.positionAmount);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("rotationAmount", noiseModule.rotationAmount);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("sizeAmount", noiseModule.sizeAmount);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.NoiseModule noiseModule = default(ParticleSystem.NoiseModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					noiseModule.enabled = reader.ReadProperty<bool>();
					break;
				case "separateAxes":
					noiseModule.separateAxes = reader.ReadProperty<bool>();
					break;
				case "strength":
					noiseModule.strength = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "strengthMultiplier":
					noiseModule.strengthMultiplier = reader.ReadProperty<float>();
					break;
				case "strengthX":
					noiseModule.strengthX = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "strengthXMultiplier":
					noiseModule.strengthXMultiplier = reader.ReadProperty<float>();
					break;
				case "strengthY":
					noiseModule.strengthY = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "strengthYMultiplier":
					noiseModule.strengthYMultiplier = reader.ReadProperty<float>();
					break;
				case "strengthZ":
					noiseModule.strengthZ = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "strengthZMultiplier":
					noiseModule.strengthZMultiplier = reader.ReadProperty<float>();
					break;
				case "frequency":
					noiseModule.frequency = reader.ReadProperty<float>();
					break;
				case "damping":
					noiseModule.damping = reader.ReadProperty<bool>();
					break;
				case "octaveCount":
					noiseModule.octaveCount = reader.ReadProperty<int>();
					break;
				case "octaveMultiplier":
					noiseModule.octaveMultiplier = reader.ReadProperty<float>();
					break;
				case "octaveScale":
					noiseModule.octaveScale = reader.ReadProperty<float>();
					break;
				case "quality":
					noiseModule.quality = reader.ReadProperty<ParticleSystemNoiseQuality>();
					break;
				case "scrollSpeed":
					noiseModule.scrollSpeed = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "scrollSpeedMultiplier":
					noiseModule.scrollSpeedMultiplier = reader.ReadProperty<float>();
					break;
				case "remapEnabled":
					noiseModule.remapEnabled = reader.ReadProperty<bool>();
					break;
				case "remap":
					noiseModule.remap = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "remapMultiplier":
					noiseModule.remapMultiplier = reader.ReadProperty<float>();
					break;
				case "remapX":
					noiseModule.remapX = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "remapXMultiplier":
					noiseModule.remapXMultiplier = reader.ReadProperty<float>();
					break;
				case "remapY":
					noiseModule.remapY = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "remapYMultiplier":
					noiseModule.remapYMultiplier = reader.ReadProperty<float>();
					break;
				case "remapZ":
					noiseModule.remapZ = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "remapZMultiplier":
					noiseModule.remapZMultiplier = reader.ReadProperty<float>();
					break;
				case "positionAmount":
					noiseModule.positionAmount = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "rotationAmount":
					noiseModule.rotationAmount = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "sizeAmount":
					noiseModule.sizeAmount = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				}
			}
			return noiseModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
