using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_LineRenderer : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(LineRenderer);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			LineRenderer lineRenderer = (LineRenderer)value;
			writer.WriteProperty<float>("startWidth", lineRenderer.startWidth);
			writer.WriteProperty<float>("endWidth", lineRenderer.endWidth);
			writer.WriteProperty<AnimationCurve>("widthCurve", lineRenderer.widthCurve);
			writer.WriteProperty<float>("widthMultiplier", lineRenderer.widthMultiplier);
			writer.WriteProperty<Color>("startColor", lineRenderer.startColor);
			writer.WriteProperty<Color>("endColor", lineRenderer.endColor);
			writer.WriteProperty<Gradient>("colorGradient", lineRenderer.colorGradient);
			writer.WriteProperty<int>("positionCount", lineRenderer.positionCount);
			writer.WriteProperty<bool>("useWorldSpace", lineRenderer.useWorldSpace);
			writer.WriteProperty<bool>("loop", lineRenderer.loop);
			writer.WriteProperty<int>("numCornerVertices", lineRenderer.numCornerVertices);
			writer.WriteProperty<int>("numCapVertices", lineRenderer.numCapVertices);
			writer.WriteProperty<LineTextureMode>("textureMode", lineRenderer.textureMode);
			writer.WriteProperty<LineAlignment>("alignment", lineRenderer.alignment);
			writer.WriteProperty<bool>("generateLightingData", lineRenderer.generateLightingData);
			writer.WriteProperty<bool>("enabled", lineRenderer.enabled);
			writer.WriteProperty<ShadowCastingMode>("shadowCastingMode", lineRenderer.shadowCastingMode);
			writer.WriteProperty<bool>("receiveShadows", lineRenderer.receiveShadows);
			writer.WriteProperty<Material>("material", lineRenderer.material);
			writer.WriteProperty<Material>("sharedMaterial", lineRenderer.sharedMaterial);
			writer.WriteProperty<Material[]>("materials", lineRenderer.materials);
			writer.WriteProperty<Material[]>("sharedMaterials", lineRenderer.sharedMaterials);
			writer.WriteProperty<int>("lightmapIndex", lineRenderer.lightmapIndex);
			writer.WriteProperty<int>("realtimeLightmapIndex", lineRenderer.realtimeLightmapIndex);
			writer.WriteProperty<Vector4>("lightmapScaleOffset", lineRenderer.lightmapScaleOffset);
			writer.WriteProperty<MotionVectorGenerationMode>("motionVectorGenerationMode", lineRenderer.motionVectorGenerationMode);
			writer.WriteProperty<Vector4>("realtimeLightmapScaleOffset", lineRenderer.realtimeLightmapScaleOffset);
			writer.WriteProperty<LightProbeUsage>("lightProbeUsage", lineRenderer.lightProbeUsage);
			writer.WriteProperty<GameObject>("lightProbeProxyVolumeOverride", lineRenderer.lightProbeProxyVolumeOverride);
			writer.WriteProperty<Transform>("probeAnchor", lineRenderer.probeAnchor);
			writer.WriteProperty<ReflectionProbeUsage>("reflectionProbeUsage", lineRenderer.reflectionProbeUsage);
			writer.WriteProperty<string>("sortingLayerName", lineRenderer.sortingLayerName);
			writer.WriteProperty<int>("sortingLayerID", lineRenderer.sortingLayerID);
			writer.WriteProperty<int>("sortingOrder", lineRenderer.sortingOrder);
			writer.WriteProperty<string>("tag", lineRenderer.tag);
			writer.WriteProperty<string>("name", lineRenderer.name);
			writer.WriteProperty<HideFlags>("hideFlags", lineRenderer.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			LineRenderer lineRenderer = SaveGameType.CreateComponent<LineRenderer>();
			this.ReadInto(lineRenderer, reader);
			return lineRenderer;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			LineRenderer lineRenderer = (LineRenderer)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "startWidth":
					lineRenderer.startWidth = reader.ReadProperty<float>();
					break;
				case "endWidth":
					lineRenderer.endWidth = reader.ReadProperty<float>();
					break;
				case "widthCurve":
					lineRenderer.widthCurve = reader.ReadProperty<AnimationCurve>();
					break;
				case "widthMultiplier":
					lineRenderer.widthMultiplier = reader.ReadProperty<float>();
					break;
				case "startColor":
					lineRenderer.startColor = reader.ReadProperty<Color>();
					break;
				case "endColor":
					lineRenderer.endColor = reader.ReadProperty<Color>();
					break;
				case "colorGradient":
					lineRenderer.colorGradient = reader.ReadProperty<Gradient>();
					break;
				case "positionCount":
					lineRenderer.positionCount = reader.ReadProperty<int>();
					break;
				case "useWorldSpace":
					lineRenderer.useWorldSpace = reader.ReadProperty<bool>();
					break;
				case "loop":
					lineRenderer.loop = reader.ReadProperty<bool>();
					break;
				case "numCornerVertices":
					lineRenderer.numCornerVertices = reader.ReadProperty<int>();
					break;
				case "numCapVertices":
					lineRenderer.numCapVertices = reader.ReadProperty<int>();
					break;
				case "textureMode":
					lineRenderer.textureMode = reader.ReadProperty<LineTextureMode>();
					break;
				case "alignment":
					lineRenderer.alignment = reader.ReadProperty<LineAlignment>();
					break;
				case "generateLightingData":
					lineRenderer.generateLightingData = reader.ReadProperty<bool>();
					break;
				case "enabled":
					lineRenderer.enabled = reader.ReadProperty<bool>();
					break;
				case "shadowCastingMode":
					lineRenderer.shadowCastingMode = reader.ReadProperty<ShadowCastingMode>();
					break;
				case "receiveShadows":
					lineRenderer.receiveShadows = reader.ReadProperty<bool>();
					break;
				case "material":
					if (lineRenderer.material == null)
					{
						lineRenderer.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(lineRenderer.material);
					}
					break;
				case "sharedMaterial":
					if (lineRenderer.sharedMaterial == null)
					{
						lineRenderer.sharedMaterial = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(lineRenderer.sharedMaterial);
					}
					break;
				case "materials":
					lineRenderer.materials = reader.ReadProperty<Material[]>();
					break;
				case "sharedMaterials":
					lineRenderer.sharedMaterials = reader.ReadProperty<Material[]>();
					break;
				case "lightmapIndex":
					lineRenderer.lightmapIndex = reader.ReadProperty<int>();
					break;
				case "realtimeLightmapIndex":
					lineRenderer.realtimeLightmapIndex = reader.ReadProperty<int>();
					break;
				case "lightmapScaleOffset":
					lineRenderer.lightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "motionVectorGenerationMode":
					lineRenderer.motionVectorGenerationMode = reader.ReadProperty<MotionVectorGenerationMode>();
					break;
				case "realtimeLightmapScaleOffset":
					lineRenderer.realtimeLightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "lightProbeUsage":
					lineRenderer.lightProbeUsage = reader.ReadProperty<LightProbeUsage>();
					break;
				case "lightProbeProxyVolumeOverride":
					if (lineRenderer.lightProbeProxyVolumeOverride == null)
					{
						lineRenderer.lightProbeProxyVolumeOverride = reader.ReadProperty<GameObject>();
					}
					else
					{
						reader.ReadIntoProperty<GameObject>(lineRenderer.lightProbeProxyVolumeOverride);
					}
					break;
				case "probeAnchor":
					if (lineRenderer.probeAnchor == null)
					{
						lineRenderer.probeAnchor = reader.ReadProperty<Transform>();
					}
					else
					{
						reader.ReadIntoProperty<Transform>(lineRenderer.probeAnchor);
					}
					break;
				case "reflectionProbeUsage":
					lineRenderer.reflectionProbeUsage = reader.ReadProperty<ReflectionProbeUsage>();
					break;
				case "sortingLayerName":
					lineRenderer.sortingLayerName = reader.ReadProperty<string>();
					break;
				case "sortingLayerID":
					lineRenderer.sortingLayerID = reader.ReadProperty<int>();
					break;
				case "sortingOrder":
					lineRenderer.sortingOrder = reader.ReadProperty<int>();
					break;
				case "tag":
					lineRenderer.tag = reader.ReadProperty<string>();
					break;
				case "name":
					lineRenderer.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					lineRenderer.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
