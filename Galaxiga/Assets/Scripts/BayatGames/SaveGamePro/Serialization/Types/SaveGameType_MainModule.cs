using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_MainModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.MainModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.MainModule mainModule = (ParticleSystem.MainModule)value;
			writer.WriteProperty<float>("duration", mainModule.duration);
			writer.WriteProperty<bool>("loop", mainModule.loop);
			writer.WriteProperty<bool>("prewarm", mainModule.prewarm);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("startDelay", mainModule.startDelay);
			writer.WriteProperty<float>("startDelayMultiplier", mainModule.startDelayMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("startLifetime", mainModule.startLifetime);
			writer.WriteProperty<float>("startLifetimeMultiplier", mainModule.startLifetimeMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("startSpeed", mainModule.startSpeed);
			writer.WriteProperty<float>("startSpeedMultiplier", mainModule.startSpeedMultiplier);
			writer.WriteProperty<bool>("startSize3D", mainModule.startSize3D);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("startSize", mainModule.startSize);
			writer.WriteProperty<float>("startSizeMultiplier", mainModule.startSizeMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("startSizeX", mainModule.startSizeX);
			writer.WriteProperty<float>("startSizeXMultiplier", mainModule.startSizeXMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("startSizeY", mainModule.startSizeY);
			writer.WriteProperty<float>("startSizeYMultiplier", mainModule.startSizeYMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("startSizeZ", mainModule.startSizeZ);
			writer.WriteProperty<float>("startSizeZMultiplier", mainModule.startSizeZMultiplier);
			writer.WriteProperty<bool>("startRotation3D", mainModule.startRotation3D);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("startRotation", mainModule.startRotation);
			writer.WriteProperty<float>("startRotationMultiplier", mainModule.startRotationMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("startRotationX", mainModule.startRotationX);
			writer.WriteProperty<float>("startRotationXMultiplier", mainModule.startRotationXMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("startRotationY", mainModule.startRotationY);
			writer.WriteProperty<float>("startRotationYMultiplier", mainModule.startRotationYMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("startRotationZ", mainModule.startRotationZ);
			writer.WriteProperty<float>("startRotationZMultiplier", mainModule.startRotationZMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxGradient>("startColor", mainModule.startColor);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("gravityModifier", mainModule.gravityModifier);
			writer.WriteProperty<float>("gravityModifierMultiplier", mainModule.gravityModifierMultiplier);
			writer.WriteProperty<ParticleSystemSimulationSpace>("simulationSpace", mainModule.simulationSpace);
			writer.WriteProperty<Transform>("customSimulationSpace", mainModule.customSimulationSpace);
			writer.WriteProperty<float>("simulationSpeed", mainModule.simulationSpeed);
			writer.WriteProperty<bool>("useUnscaledTime", mainModule.useUnscaledTime);
			writer.WriteProperty<ParticleSystemScalingMode>("scalingMode", mainModule.scalingMode);
			writer.WriteProperty<bool>("playOnAwake", mainModule.playOnAwake);
			writer.WriteProperty<int>("maxParticles", mainModule.maxParticles);
			writer.WriteProperty<ParticleSystemEmitterVelocityMode>("emitterVelocityMode", mainModule.emitterVelocityMode);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.MainModule mainModule = default(ParticleSystem.MainModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "duration":
					mainModule.duration = reader.ReadProperty<float>();
					break;
				case "loop":
					mainModule.loop = reader.ReadProperty<bool>();
					break;
				case "prewarm":
					mainModule.prewarm = reader.ReadProperty<bool>();
					break;
				case "startDelay":
					mainModule.startDelay = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "startDelayMultiplier":
					mainModule.startDelayMultiplier = reader.ReadProperty<float>();
					break;
				case "startLifetime":
					mainModule.startLifetime = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "startLifetimeMultiplier":
					mainModule.startLifetimeMultiplier = reader.ReadProperty<float>();
					break;
				case "startSpeed":
					mainModule.startSpeed = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "startSpeedMultiplier":
					mainModule.startSpeedMultiplier = reader.ReadProperty<float>();
					break;
				case "startSize3D":
					mainModule.startSize3D = reader.ReadProperty<bool>();
					break;
				case "startSize":
					mainModule.startSize = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "startSizeMultiplier":
					mainModule.startSizeMultiplier = reader.ReadProperty<float>();
					break;
				case "startSizeX":
					mainModule.startSizeX = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "startSizeXMultiplier":
					mainModule.startSizeXMultiplier = reader.ReadProperty<float>();
					break;
				case "startSizeY":
					mainModule.startSizeY = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "startSizeYMultiplier":
					mainModule.startSizeYMultiplier = reader.ReadProperty<float>();
					break;
				case "startSizeZ":
					mainModule.startSizeZ = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "startSizeZMultiplier":
					mainModule.startSizeZMultiplier = reader.ReadProperty<float>();
					break;
				case "startRotation3D":
					mainModule.startRotation3D = reader.ReadProperty<bool>();
					break;
				case "startRotation":
					mainModule.startRotation = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "startRotationMultiplier":
					mainModule.startRotationMultiplier = reader.ReadProperty<float>();
					break;
				case "startRotationX":
					mainModule.startRotationX = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "startRotationXMultiplier":
					mainModule.startRotationXMultiplier = reader.ReadProperty<float>();
					break;
				case "startRotationY":
					mainModule.startRotationY = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "startRotationYMultiplier":
					mainModule.startRotationYMultiplier = reader.ReadProperty<float>();
					break;
				case "startRotationZ":
					mainModule.startRotationZ = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "startRotationZMultiplier":
					mainModule.startRotationZMultiplier = reader.ReadProperty<float>();
					break;
				case "randomizeRotationDirection":
					reader.ReadProperty<float>();
					break;
				case "startColor":
					mainModule.startColor = reader.ReadProperty<ParticleSystem.MinMaxGradient>();
					break;
				case "gravityModifier":
					mainModule.gravityModifier = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "gravityModifierMultiplier":
					mainModule.gravityModifierMultiplier = reader.ReadProperty<float>();
					break;
				case "simulationSpace":
					mainModule.simulationSpace = reader.ReadProperty<ParticleSystemSimulationSpace>();
					break;
				case "customSimulationSpace":
					if (mainModule.customSimulationSpace == null)
					{
						mainModule.customSimulationSpace = reader.ReadProperty<Transform>();
					}
					else
					{
						reader.ReadIntoProperty<Transform>(mainModule.customSimulationSpace);
					}
					break;
				case "simulationSpeed":
					mainModule.simulationSpeed = reader.ReadProperty<float>();
					break;
				case "useUnscaledTime":
					mainModule.useUnscaledTime = reader.ReadProperty<bool>();
					break;
				case "scalingMode":
					mainModule.scalingMode = reader.ReadProperty<ParticleSystemScalingMode>();
					break;
				case "playOnAwake":
					mainModule.playOnAwake = reader.ReadProperty<bool>();
					break;
				case "maxParticles":
					mainModule.maxParticles = reader.ReadProperty<int>();
					break;
				case "emitterVelocityMode":
					mainModule.emitterVelocityMode = reader.ReadProperty<ParticleSystemEmitterVelocityMode>();
					break;
				}
			}
			return mainModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
