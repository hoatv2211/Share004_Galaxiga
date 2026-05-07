using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Cloth : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Cloth);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Cloth cloth = (Cloth)value;
			writer.WriteProperty<float>("sleepThreshold", cloth.sleepThreshold);
			writer.WriteProperty<float>("bendingStiffness", cloth.bendingStiffness);
			writer.WriteProperty<float>("stretchingStiffness", cloth.stretchingStiffness);
			writer.WriteProperty<float>("damping", cloth.damping);
			writer.WriteProperty<Vector3>("externalAcceleration", cloth.externalAcceleration);
			writer.WriteProperty<Vector3>("randomAcceleration", cloth.randomAcceleration);
			writer.WriteProperty<bool>("useGravity", cloth.useGravity);
			writer.WriteProperty<bool>("enabled", cloth.enabled);
			writer.WriteProperty<float>("friction", cloth.friction);
			writer.WriteProperty<float>("collisionMassScale", cloth.collisionMassScale);
			writer.WriteProperty<bool>("enableContinuousCollision", cloth.enableContinuousCollision);
			writer.WriteProperty<float>("useVirtualParticles", cloth.useVirtualParticles);
			writer.WriteProperty<ClothSkinningCoefficient[]>("coefficients", cloth.coefficients);
			writer.WriteProperty<float>("worldVelocityScale", cloth.worldVelocityScale);
			writer.WriteProperty<float>("worldAccelerationScale", cloth.worldAccelerationScale);
			writer.WriteProperty<float>("clothSolverFrequency", cloth.clothSolverFrequency);
			writer.WriteProperty<CapsuleCollider[]>("capsuleColliders", cloth.capsuleColliders);
			writer.WriteProperty<ClothSphereColliderPair[]>("sphereColliders", cloth.sphereColliders);
			writer.WriteProperty<string>("tag", cloth.tag);
			writer.WriteProperty<string>("name", cloth.name);
			writer.WriteProperty<HideFlags>("hideFlags", cloth.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Cloth cloth = SaveGameType.CreateComponent<Cloth>();
			this.ReadInto(cloth, reader);
			return cloth;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Cloth cloth = (Cloth)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "sleepThreshold":
					cloth.sleepThreshold = reader.ReadProperty<float>();
					break;
				case "bendingStiffness":
					cloth.bendingStiffness = reader.ReadProperty<float>();
					break;
				case "stretchingStiffness":
					cloth.stretchingStiffness = reader.ReadProperty<float>();
					break;
				case "damping":
					cloth.damping = reader.ReadProperty<float>();
					break;
				case "externalAcceleration":
					cloth.externalAcceleration = reader.ReadProperty<Vector3>();
					break;
				case "randomAcceleration":
					cloth.randomAcceleration = reader.ReadProperty<Vector3>();
					break;
				case "useGravity":
					cloth.useGravity = reader.ReadProperty<bool>();
					break;
				case "enabled":
					cloth.enabled = reader.ReadProperty<bool>();
					break;
				case "friction":
					cloth.friction = reader.ReadProperty<float>();
					break;
				case "collisionMassScale":
					cloth.collisionMassScale = reader.ReadProperty<float>();
					break;
				case "enableContinuousCollision":
					cloth.enableContinuousCollision = reader.ReadProperty<bool>();
					break;
				case "useVirtualParticles":
					cloth.useVirtualParticles = reader.ReadProperty<float>();
					break;
				case "coefficients":
					cloth.coefficients = reader.ReadProperty<ClothSkinningCoefficient[]>();
					break;
				case "worldVelocityScale":
					cloth.worldVelocityScale = reader.ReadProperty<float>();
					break;
				case "worldAccelerationScale":
					cloth.worldAccelerationScale = reader.ReadProperty<float>();
					break;
				case "clothSolverFrequency":
					cloth.clothSolverFrequency = reader.ReadProperty<float>();
					break;
				case "capsuleColliders":
					cloth.capsuleColliders = reader.ReadProperty<CapsuleCollider[]>();
					break;
				case "sphereColliders":
					cloth.sphereColliders = reader.ReadProperty<ClothSphereColliderPair[]>();
					break;
				case "tag":
					cloth.tag = reader.ReadProperty<string>();
					break;
				case "name":
					cloth.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					cloth.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
