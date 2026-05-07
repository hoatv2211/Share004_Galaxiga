using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ParticleSystem : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem particleSystem = (ParticleSystem)value;
			writer.WriteProperty<ParticleSystem.CollisionModule>("collision", particleSystem.collision);
			writer.WriteProperty<ParticleSystem.ColorBySpeedModule>("colorBySpeed", particleSystem.colorBySpeed);
			writer.WriteProperty<ParticleSystem.ColorOverLifetimeModule>("colorOverLifetime", particleSystem.colorOverLifetime);
			writer.WriteProperty<ParticleSystem.CustomDataModule>("customData", particleSystem.customData);
			writer.WriteProperty<ParticleSystem.EmissionModule>("emission", particleSystem.emission);
			writer.WriteProperty<ParticleSystem.ExternalForcesModule>("externalForces", particleSystem.externalForces);
			writer.WriteProperty<ParticleSystem.ForceOverLifetimeModule>("forceOverLifetime", particleSystem.forceOverLifetime);
			writer.WriteProperty<ParticleSystem.InheritVelocityModule>("inheritVelocity", particleSystem.inheritVelocity);
			writer.WriteProperty<ParticleSystem.LightsModule>("lights", particleSystem.lights);
			writer.WriteProperty<ParticleSystem.LimitVelocityOverLifetimeModule>("limitVelocityOverLifetime", particleSystem.limitVelocityOverLifetime);
			writer.WriteProperty<ParticleSystem.MainModule>("main", particleSystem.main);
			writer.WriteProperty<ParticleSystem.NoiseModule>("noise", particleSystem.noise);
			writer.WriteProperty<ParticleSystem.RotationBySpeedModule>("rotationBySpeed", particleSystem.rotationBySpeed);
			writer.WriteProperty<ParticleSystem.RotationOverLifetimeModule>("rotationOverLifetime", particleSystem.rotationOverLifetime);
			writer.WriteProperty<ParticleSystem.ShapeModule>("shape", particleSystem.shape);
			writer.WriteProperty<ParticleSystem.SizeBySpeedModule>("sizeBySpeed", particleSystem.sizeBySpeed);
			writer.WriteProperty<ParticleSystem.SizeOverLifetimeModule>("sizeOverLifetime", particleSystem.sizeOverLifetime);
			writer.WriteProperty<ParticleSystem.SubEmittersModule>("subEmitters", particleSystem.subEmitters);
			writer.WriteProperty<ParticleSystem.TextureSheetAnimationModule>("textureSheetAnimation", particleSystem.textureSheetAnimation);
			writer.WriteProperty<ParticleSystem.TrailModule>("trails", particleSystem.trails);
			writer.WriteProperty<ParticleSystem.TriggerModule>("trigger", particleSystem.trigger);
			writer.WriteProperty<ParticleSystem.VelocityOverLifetimeModule>("velocityOverLifetime", particleSystem.velocityOverLifetime);
			writer.WriteProperty<float>("time", particleSystem.time);
			writer.WriteProperty<uint>("randomSeed", particleSystem.randomSeed);
			writer.WriteProperty<bool>("useAutoRandomSeed", particleSystem.useAutoRandomSeed);
			writer.WriteProperty<string>("tag", particleSystem.tag);
			writer.WriteProperty<string>("name", particleSystem.name);
			writer.WriteProperty<HideFlags>("hideFlags", particleSystem.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem particleSystem = SaveGameType.CreateComponent<ParticleSystem>();
			this.ReadInto(particleSystem, reader);
			return particleSystem;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			ParticleSystem particleSystem = (ParticleSystem)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "collision":
					reader.ReadIntoProperty<ParticleSystem.CollisionModule>(particleSystem.collision);
					break;
				case "colorBySpeed":
					reader.ReadIntoProperty<ParticleSystem.ColorBySpeedModule>(particleSystem.colorBySpeed);
					break;
				case "colorOverLifetime":
					reader.ReadIntoProperty<ParticleSystem.ColorOverLifetimeModule>(particleSystem.colorOverLifetime);
					break;
				case "customData":
					reader.ReadIntoProperty<ParticleSystem.CustomDataModule>(particleSystem.customData);
					break;
				case "emission":
					reader.ReadIntoProperty<ParticleSystem.EmissionModule>(particleSystem.emission);
					break;
				case "externalForces":
					reader.ReadIntoProperty<ParticleSystem.ExternalForcesModule>(particleSystem.externalForces);
					break;
				case "forceOverLifetime":
					reader.ReadIntoProperty<ParticleSystem.ForceOverLifetimeModule>(particleSystem.forceOverLifetime);
					break;
				case "inheritVelocity":
					reader.ReadIntoProperty<ParticleSystem.InheritVelocityModule>(particleSystem.inheritVelocity);
					break;
				case "lights":
					reader.ReadIntoProperty<ParticleSystem.LightsModule>(particleSystem.lights);
					break;
				case "limitVelocityOverLifetime":
					reader.ReadIntoProperty<ParticleSystem.LimitVelocityOverLifetimeModule>(particleSystem.limitVelocityOverLifetime);
					break;
				case "main":
					reader.ReadIntoProperty<ParticleSystem.MainModule>(particleSystem.main);
					break;
				case "noise":
					reader.ReadIntoProperty<ParticleSystem.NoiseModule>(particleSystem.noise);
					break;
				case "rotationBySpeed":
					reader.ReadIntoProperty<ParticleSystem.RotationBySpeedModule>(particleSystem.rotationBySpeed);
					break;
				case "rotationOverLifetime":
					reader.ReadIntoProperty<ParticleSystem.RotationOverLifetimeModule>(particleSystem.rotationOverLifetime);
					break;
				case "shape":
					reader.ReadIntoProperty<ParticleSystem.ShapeModule>(particleSystem.shape);
					break;
				case "sizeBySpeed":
					reader.ReadIntoProperty<ParticleSystem.SizeBySpeedModule>(particleSystem.sizeBySpeed);
					break;
				case "sizeOverLifetime":
					reader.ReadIntoProperty<ParticleSystem.SizeOverLifetimeModule>(particleSystem.sizeOverLifetime);
					break;
				case "subEmitters":
					reader.ReadIntoProperty<ParticleSystem.SubEmittersModule>(particleSystem.subEmitters);
					break;
				case "textureSheetAnimation":
					reader.ReadIntoProperty<ParticleSystem.TextureSheetAnimationModule>(particleSystem.textureSheetAnimation);
					break;
				case "trails":
					reader.ReadIntoProperty<ParticleSystem.TrailModule>(particleSystem.trails);
					break;
				case "trigger":
					reader.ReadIntoProperty<ParticleSystem.TriggerModule>(particleSystem.trigger);
					break;
				case "velocityOverLifetime":
					reader.ReadIntoProperty<ParticleSystem.VelocityOverLifetimeModule>(particleSystem.velocityOverLifetime);
					break;
				case "time":
					particleSystem.time = reader.ReadProperty<float>();
					break;
				case "randomSeed":
					particleSystem.randomSeed = reader.ReadProperty<uint>();
					break;
				case "useAutoRandomSeed":
					particleSystem.useAutoRandomSeed = reader.ReadProperty<bool>();
					break;
				case "tag":
					particleSystem.tag = reader.ReadProperty<string>();
					break;
				case "name":
					particleSystem.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					particleSystem.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
