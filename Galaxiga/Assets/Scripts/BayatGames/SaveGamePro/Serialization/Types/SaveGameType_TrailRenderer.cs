using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_TrailRenderer : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(TrailRenderer);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			TrailRenderer trailRenderer = (TrailRenderer)value;
			writer.WriteProperty<float>("time", trailRenderer.time);
			writer.WriteProperty<float>("startWidth", trailRenderer.startWidth);
			writer.WriteProperty<float>("endWidth", trailRenderer.endWidth);
			writer.WriteProperty<AnimationCurve>("widthCurve", trailRenderer.widthCurve);
			writer.WriteProperty<float>("widthMultiplier", trailRenderer.widthMultiplier);
			writer.WriteProperty<Color>("startColor", trailRenderer.startColor);
			writer.WriteProperty<Color>("endColor", trailRenderer.endColor);
			writer.WriteProperty<Gradient>("colorGradient", trailRenderer.colorGradient);
			writer.WriteProperty<bool>("autodestruct", trailRenderer.autodestruct);
			writer.WriteProperty<int>("numCornerVertices", trailRenderer.numCornerVertices);
			writer.WriteProperty<int>("numCapVertices", trailRenderer.numCapVertices);
			writer.WriteProperty<float>("minVertexDistance", trailRenderer.minVertexDistance);
			writer.WriteProperty<LineTextureMode>("textureMode", trailRenderer.textureMode);
			writer.WriteProperty<LineAlignment>("alignment", trailRenderer.alignment);
			writer.WriteProperty<bool>("generateLightingData", trailRenderer.generateLightingData);
			writer.WriteProperty<bool>("enabled", trailRenderer.enabled);
			writer.WriteProperty<ShadowCastingMode>("shadowCastingMode", trailRenderer.shadowCastingMode);
			writer.WriteProperty<bool>("receiveShadows", trailRenderer.receiveShadows);
			writer.WriteProperty<Material>("material", trailRenderer.material);
			writer.WriteProperty<Material>("sharedMaterial", trailRenderer.sharedMaterial);
			writer.WriteProperty<Material[]>("materials", trailRenderer.materials);
			writer.WriteProperty<Material[]>("sharedMaterials", trailRenderer.sharedMaterials);
			writer.WriteProperty<int>("lightmapIndex", trailRenderer.lightmapIndex);
			writer.WriteProperty<int>("realtimeLightmapIndex", trailRenderer.realtimeLightmapIndex);
			writer.WriteProperty<Vector4>("lightmapScaleOffset", trailRenderer.lightmapScaleOffset);
			writer.WriteProperty<MotionVectorGenerationMode>("motionVectorGenerationMode", trailRenderer.motionVectorGenerationMode);
			writer.WriteProperty<Vector4>("realtimeLightmapScaleOffset", trailRenderer.realtimeLightmapScaleOffset);
			writer.WriteProperty<LightProbeUsage>("lightProbeUsage", trailRenderer.lightProbeUsage);
			writer.WriteProperty<GameObject>("lightProbeProxyVolumeOverride", trailRenderer.lightProbeProxyVolumeOverride);
			writer.WriteProperty<Transform>("probeAnchor", trailRenderer.probeAnchor);
			writer.WriteProperty<ReflectionProbeUsage>("reflectionProbeUsage", trailRenderer.reflectionProbeUsage);
			writer.WriteProperty<string>("sortingLayerName", trailRenderer.sortingLayerName);
			writer.WriteProperty<int>("sortingLayerID", trailRenderer.sortingLayerID);
			writer.WriteProperty<int>("sortingOrder", trailRenderer.sortingOrder);
			writer.WriteProperty<string>("tag", trailRenderer.tag);
			writer.WriteProperty<string>("name", trailRenderer.name);
			writer.WriteProperty<HideFlags>("hideFlags", trailRenderer.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			TrailRenderer trailRenderer = SaveGameType.CreateComponent<TrailRenderer>();
			this.ReadInto(trailRenderer, reader);
			return trailRenderer;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			TrailRenderer trailRenderer = (TrailRenderer)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "time":
					trailRenderer.time = reader.ReadProperty<float>();
					break;
				case "startWidth":
					trailRenderer.startWidth = reader.ReadProperty<float>();
					break;
				case "endWidth":
					trailRenderer.endWidth = reader.ReadProperty<float>();
					break;
				case "widthCurve":
					trailRenderer.widthCurve = reader.ReadProperty<AnimationCurve>();
					break;
				case "widthMultiplier":
					trailRenderer.widthMultiplier = reader.ReadProperty<float>();
					break;
				case "startColor":
					trailRenderer.startColor = reader.ReadProperty<Color>();
					break;
				case "endColor":
					trailRenderer.endColor = reader.ReadProperty<Color>();
					break;
				case "colorGradient":
					trailRenderer.colorGradient = reader.ReadProperty<Gradient>();
					break;
				case "autodestruct":
					trailRenderer.autodestruct = reader.ReadProperty<bool>();
					break;
				case "numCornerVertices":
					trailRenderer.numCornerVertices = reader.ReadProperty<int>();
					break;
				case "numCapVertices":
					trailRenderer.numCapVertices = reader.ReadProperty<int>();
					break;
				case "minVertexDistance":
					trailRenderer.minVertexDistance = reader.ReadProperty<float>();
					break;
				case "textureMode":
					trailRenderer.textureMode = reader.ReadProperty<LineTextureMode>();
					break;
				case "alignment":
					trailRenderer.alignment = reader.ReadProperty<LineAlignment>();
					break;
				case "generateLightingData":
					trailRenderer.generateLightingData = reader.ReadProperty<bool>();
					break;
				case "enabled":
					trailRenderer.enabled = reader.ReadProperty<bool>();
					break;
				case "shadowCastingMode":
					trailRenderer.shadowCastingMode = reader.ReadProperty<ShadowCastingMode>();
					break;
				case "receiveShadows":
					trailRenderer.receiveShadows = reader.ReadProperty<bool>();
					break;
				case "material":
					if (trailRenderer.material == null)
					{
						trailRenderer.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(trailRenderer.material);
					}
					break;
				case "sharedMaterial":
					if (trailRenderer.sharedMaterial == null)
					{
						trailRenderer.sharedMaterial = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(trailRenderer.sharedMaterial);
					}
					break;
				case "materials":
					trailRenderer.materials = reader.ReadProperty<Material[]>();
					break;
				case "sharedMaterials":
					trailRenderer.sharedMaterials = reader.ReadProperty<Material[]>();
					break;
				case "lightmapIndex":
					trailRenderer.lightmapIndex = reader.ReadProperty<int>();
					break;
				case "realtimeLightmapIndex":
					trailRenderer.realtimeLightmapIndex = reader.ReadProperty<int>();
					break;
				case "lightmapScaleOffset":
					trailRenderer.lightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "motionVectorGenerationMode":
					trailRenderer.motionVectorGenerationMode = reader.ReadProperty<MotionVectorGenerationMode>();
					break;
				case "realtimeLightmapScaleOffset":
					trailRenderer.realtimeLightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "lightProbeUsage":
					trailRenderer.lightProbeUsage = reader.ReadProperty<LightProbeUsage>();
					break;
				case "lightProbeProxyVolumeOverride":
					if (trailRenderer.lightProbeProxyVolumeOverride == null)
					{
						trailRenderer.lightProbeProxyVolumeOverride = reader.ReadProperty<GameObject>();
					}
					else
					{
						reader.ReadIntoProperty<GameObject>(trailRenderer.lightProbeProxyVolumeOverride);
					}
					break;
				case "probeAnchor":
					if (trailRenderer.probeAnchor == null)
					{
						trailRenderer.probeAnchor = reader.ReadProperty<Transform>();
					}
					else
					{
						reader.ReadIntoProperty<Transform>(trailRenderer.probeAnchor);
					}
					break;
				case "reflectionProbeUsage":
					trailRenderer.reflectionProbeUsage = reader.ReadProperty<ReflectionProbeUsage>();
					break;
				case "sortingLayerName":
					trailRenderer.sortingLayerName = reader.ReadProperty<string>();
					break;
				case "sortingLayerID":
					trailRenderer.sortingLayerID = reader.ReadProperty<int>();
					break;
				case "sortingOrder":
					trailRenderer.sortingOrder = reader.ReadProperty<int>();
					break;
				case "tag":
					trailRenderer.tag = reader.ReadProperty<string>();
					break;
				case "name":
					trailRenderer.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					trailRenderer.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
