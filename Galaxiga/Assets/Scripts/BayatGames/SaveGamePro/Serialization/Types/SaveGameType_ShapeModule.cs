using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ShapeModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.ShapeModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.ShapeModule shapeModule = (ParticleSystem.ShapeModule)value;
			writer.WriteProperty<bool>("enabled", shapeModule.enabled);
			writer.WriteProperty<ParticleSystemShapeType>("shapeType", shapeModule.shapeType);
			writer.WriteProperty<float>("randomDirectionAmount", shapeModule.randomDirectionAmount);
			writer.WriteProperty<float>("sphericalDirectionAmount", shapeModule.sphericalDirectionAmount);
			writer.WriteProperty<float>("randomPositionAmount", shapeModule.randomPositionAmount);
			writer.WriteProperty<bool>("alignToDirection", shapeModule.alignToDirection);
			writer.WriteProperty<float>("radius", shapeModule.radius);
			writer.WriteProperty<ParticleSystemShapeMultiModeValue>("radiusMode", shapeModule.radiusMode);
			writer.WriteProperty<float>("radiusSpread", shapeModule.radiusSpread);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("radiusSpeed", shapeModule.radiusSpeed);
			writer.WriteProperty<float>("radiusSpeedMultiplier", shapeModule.radiusSpeedMultiplier);
			writer.WriteProperty<float>("radiusThickness", shapeModule.radiusThickness);
			writer.WriteProperty<float>("angle", shapeModule.angle);
			writer.WriteProperty<float>("length", shapeModule.length);
			writer.WriteProperty<Vector3>("boxThickness", shapeModule.boxThickness);
			writer.WriteProperty<ParticleSystemMeshShapeType>("meshShapeType", shapeModule.meshShapeType);
			writer.WriteProperty<Mesh>("mesh", shapeModule.mesh);
			writer.WriteProperty<MeshRenderer>("meshRenderer", shapeModule.meshRenderer);
			writer.WriteProperty<SkinnedMeshRenderer>("skinnedMeshRenderer", shapeModule.skinnedMeshRenderer);
			writer.WriteProperty<bool>("useMeshMaterialIndex", shapeModule.useMeshMaterialIndex);
			writer.WriteProperty<int>("meshMaterialIndex", shapeModule.meshMaterialIndex);
			writer.WriteProperty<bool>("useMeshColors", shapeModule.useMeshColors);
			writer.WriteProperty<float>("normalOffset", shapeModule.normalOffset);
			writer.WriteProperty<float>("arc", shapeModule.arc);
			writer.WriteProperty<ParticleSystemShapeMultiModeValue>("arcMode", shapeModule.arcMode);
			writer.WriteProperty<float>("arcSpread", shapeModule.arcSpread);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("arcSpeed", shapeModule.arcSpeed);
			writer.WriteProperty<float>("arcSpeedMultiplier", shapeModule.arcSpeedMultiplier);
			writer.WriteProperty<float>("donutRadius", shapeModule.donutRadius);
			writer.WriteProperty<Vector3>("position", shapeModule.position);
			writer.WriteProperty<Vector3>("rotation", shapeModule.rotation);
			writer.WriteProperty<Vector3>("scale", shapeModule.scale);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.ShapeModule shapeModule = default(ParticleSystem.ShapeModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					shapeModule.enabled = reader.ReadProperty<bool>();
					break;
				case "shapeType":
					shapeModule.shapeType = reader.ReadProperty<ParticleSystemShapeType>();
					break;
				case "randomDirectionAmount":
					shapeModule.randomDirectionAmount = reader.ReadProperty<float>();
					break;
				case "sphericalDirectionAmount":
					shapeModule.sphericalDirectionAmount = reader.ReadProperty<float>();
					break;
				case "randomPositionAmount":
					shapeModule.randomPositionAmount = reader.ReadProperty<float>();
					break;
				case "alignToDirection":
					shapeModule.alignToDirection = reader.ReadProperty<bool>();
					break;
				case "radius":
					shapeModule.radius = reader.ReadProperty<float>();
					break;
				case "radiusMode":
					shapeModule.radiusMode = reader.ReadProperty<ParticleSystemShapeMultiModeValue>();
					break;
				case "radiusSpread":
					shapeModule.radiusSpread = reader.ReadProperty<float>();
					break;
				case "radiusSpeed":
					shapeModule.radiusSpeed = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "radiusSpeedMultiplier":
					shapeModule.radiusSpeedMultiplier = reader.ReadProperty<float>();
					break;
				case "radiusThickness":
					shapeModule.radiusThickness = reader.ReadProperty<float>();
					break;
				case "angle":
					shapeModule.angle = reader.ReadProperty<float>();
					break;
				case "length":
					shapeModule.length = reader.ReadProperty<float>();
					break;
				case "boxThickness":
					shapeModule.boxThickness = reader.ReadProperty<Vector3>();
					break;
				case "meshShapeType":
					shapeModule.meshShapeType = reader.ReadProperty<ParticleSystemMeshShapeType>();
					break;
				case "mesh":
					if (shapeModule.mesh == null)
					{
						shapeModule.mesh = reader.ReadProperty<Mesh>();
					}
					else
					{
						reader.ReadIntoProperty<Mesh>(shapeModule.mesh);
					}
					break;
				case "meshRenderer":
					if (shapeModule.meshRenderer == null)
					{
						shapeModule.meshRenderer = reader.ReadProperty<MeshRenderer>();
					}
					else
					{
						reader.ReadIntoProperty<MeshRenderer>(shapeModule.meshRenderer);
					}
					break;
				case "skinnedMeshRenderer":
					if (shapeModule.skinnedMeshRenderer == null)
					{
						shapeModule.skinnedMeshRenderer = reader.ReadProperty<SkinnedMeshRenderer>();
					}
					else
					{
						reader.ReadIntoProperty<SkinnedMeshRenderer>(shapeModule.skinnedMeshRenderer);
					}
					break;
				case "useMeshMaterialIndex":
					shapeModule.useMeshMaterialIndex = reader.ReadProperty<bool>();
					break;
				case "meshMaterialIndex":
					shapeModule.meshMaterialIndex = reader.ReadProperty<int>();
					break;
				case "useMeshColors":
					shapeModule.useMeshColors = reader.ReadProperty<bool>();
					break;
				case "normalOffset":
					shapeModule.normalOffset = reader.ReadProperty<float>();
					break;
				case "arc":
					shapeModule.arc = reader.ReadProperty<float>();
					break;
				case "arcMode":
					shapeModule.arcMode = reader.ReadProperty<ParticleSystemShapeMultiModeValue>();
					break;
				case "arcSpread":
					shapeModule.arcSpread = reader.ReadProperty<float>();
					break;
				case "arcSpeed":
					shapeModule.arcSpeed = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "arcSpeedMultiplier":
					shapeModule.arcSpeedMultiplier = reader.ReadProperty<float>();
					break;
				case "donutRadius":
					shapeModule.donutRadius = reader.ReadProperty<float>();
					break;
				case "position":
					shapeModule.position = reader.ReadProperty<Vector3>();
					break;
				case "rotation":
					shapeModule.rotation = reader.ReadProperty<Vector3>();
					break;
				case "scale":
					shapeModule.scale = reader.ReadProperty<Vector3>();
					break;
				}
			}
			return shapeModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
