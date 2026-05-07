using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_CollisionModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.CollisionModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.CollisionModule collisionModule = (ParticleSystem.CollisionModule)value;
			writer.WriteProperty<bool>("enabled", collisionModule.enabled);
			writer.WriteProperty<ParticleSystemCollisionType>("type", collisionModule.type);
			writer.WriteProperty<ParticleSystemCollisionMode>("mode", collisionModule.mode);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("dampen", collisionModule.dampen);
			writer.WriteProperty<float>("dampenMultiplier", collisionModule.dampenMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("bounce", collisionModule.bounce);
			writer.WriteProperty<float>("bounceMultiplier", collisionModule.bounceMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("lifetimeLoss", collisionModule.lifetimeLoss);
			writer.WriteProperty<float>("lifetimeLossMultiplier", collisionModule.lifetimeLossMultiplier);
			writer.WriteProperty<float>("minKillSpeed", collisionModule.minKillSpeed);
			writer.WriteProperty<float>("maxKillSpeed", collisionModule.maxKillSpeed);
			writer.WriteProperty<LayerMask>("collidesWith", collisionModule.collidesWith);
			writer.WriteProperty<bool>("enableDynamicColliders", collisionModule.enableDynamicColliders);
			writer.WriteProperty<int>("maxCollisionShapes", collisionModule.maxCollisionShapes);
			writer.WriteProperty<ParticleSystemCollisionQuality>("quality", collisionModule.quality);
			writer.WriteProperty<float>("voxelSize", collisionModule.voxelSize);
			writer.WriteProperty<float>("radiusScale", collisionModule.radiusScale);
			writer.WriteProperty<bool>("sendCollisionMessages", collisionModule.sendCollisionMessages);
			writer.WriteProperty<float>("colliderForce", collisionModule.colliderForce);
			writer.WriteProperty<bool>("multiplyColliderForceByCollisionAngle", collisionModule.multiplyColliderForceByCollisionAngle);
			writer.WriteProperty<bool>("multiplyColliderForceByParticleSpeed", collisionModule.multiplyColliderForceByParticleSpeed);
			writer.WriteProperty<bool>("multiplyColliderForceByParticleSize", collisionModule.multiplyColliderForceByParticleSize);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.CollisionModule collisionModule = default(ParticleSystem.CollisionModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					collisionModule.enabled = reader.ReadProperty<bool>();
					break;
				case "type":
					collisionModule.type = reader.ReadProperty<ParticleSystemCollisionType>();
					break;
				case "mode":
					collisionModule.mode = reader.ReadProperty<ParticleSystemCollisionMode>();
					break;
				case "dampen":
					collisionModule.dampen = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "dampenMultiplier":
					collisionModule.dampenMultiplier = reader.ReadProperty<float>();
					break;
				case "bounce":
					collisionModule.bounce = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "bounceMultiplier":
					collisionModule.bounceMultiplier = reader.ReadProperty<float>();
					break;
				case "lifetimeLoss":
					collisionModule.lifetimeLoss = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "lifetimeLossMultiplier":
					collisionModule.lifetimeLossMultiplier = reader.ReadProperty<float>();
					break;
				case "minKillSpeed":
					collisionModule.minKillSpeed = reader.ReadProperty<float>();
					break;
				case "maxKillSpeed":
					collisionModule.maxKillSpeed = reader.ReadProperty<float>();
					break;
				case "collidesWith":
					collisionModule.collidesWith = reader.ReadProperty<LayerMask>();
					break;
				case "enableDynamicColliders":
					collisionModule.enableDynamicColliders = reader.ReadProperty<bool>();
					break;
				case "maxCollisionShapes":
					collisionModule.maxCollisionShapes = reader.ReadProperty<int>();
					break;
				case "quality":
					collisionModule.quality = reader.ReadProperty<ParticleSystemCollisionQuality>();
					break;
				case "voxelSize":
					collisionModule.voxelSize = reader.ReadProperty<float>();
					break;
				case "radiusScale":
					collisionModule.radiusScale = reader.ReadProperty<float>();
					break;
				case "sendCollisionMessages":
					collisionModule.sendCollisionMessages = reader.ReadProperty<bool>();
					break;
				case "colliderForce":
					collisionModule.colliderForce = reader.ReadProperty<float>();
					break;
				case "multiplyColliderForceByCollisionAngle":
					collisionModule.multiplyColliderForceByCollisionAngle = reader.ReadProperty<bool>();
					break;
				case "multiplyColliderForceByParticleSpeed":
					collisionModule.multiplyColliderForceByParticleSpeed = reader.ReadProperty<bool>();
					break;
				case "multiplyColliderForceByParticleSize":
					collisionModule.multiplyColliderForceByParticleSize = reader.ReadProperty<bool>();
					break;
				}
			}
			return collisionModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
