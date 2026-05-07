using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SkinnedMeshRenderer : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SkinnedMeshRenderer);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)value;
			List<float> list = new List<float>();
			for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++)
			{
				list.Add(skinnedMeshRenderer.GetBlendShapeWeight(i));
			}
			writer.WriteProperty<List<float>>("blendShapeWeights", list);
			writer.WriteProperty<Transform[]>("bones", skinnedMeshRenderer.bones);
			writer.WriteProperty<Transform>("rootBone", skinnedMeshRenderer.rootBone);
			writer.WriteProperty<SkinQuality>("quality", skinnedMeshRenderer.quality);
			writer.WriteProperty<Mesh>("sharedMesh", skinnedMeshRenderer.sharedMesh);
			writer.WriteProperty<bool>("updateWhenOffscreen", skinnedMeshRenderer.updateWhenOffscreen);
			writer.WriteProperty<bool>("skinnedMotionVectors", skinnedMeshRenderer.skinnedMotionVectors);
			writer.WriteProperty<Bounds>("localBounds", skinnedMeshRenderer.localBounds);
			writer.WriteProperty<bool>("enabled", skinnedMeshRenderer.enabled);
			writer.WriteProperty<ShadowCastingMode>("shadowCastingMode", skinnedMeshRenderer.shadowCastingMode);
			writer.WriteProperty<bool>("receiveShadows", skinnedMeshRenderer.receiveShadows);
			writer.WriteProperty<Material>("material", skinnedMeshRenderer.material);
			writer.WriteProperty<Material>("sharedMaterial", skinnedMeshRenderer.sharedMaterial);
			writer.WriteProperty<Material[]>("materials", skinnedMeshRenderer.materials);
			writer.WriteProperty<Material[]>("sharedMaterials", skinnedMeshRenderer.sharedMaterials);
			writer.WriteProperty<int>("lightmapIndex", skinnedMeshRenderer.lightmapIndex);
			writer.WriteProperty<int>("realtimeLightmapIndex", skinnedMeshRenderer.realtimeLightmapIndex);
			writer.WriteProperty<Vector4>("lightmapScaleOffset", skinnedMeshRenderer.lightmapScaleOffset);
			writer.WriteProperty<MotionVectorGenerationMode>("motionVectorGenerationMode", skinnedMeshRenderer.motionVectorGenerationMode);
			writer.WriteProperty<Vector4>("realtimeLightmapScaleOffset", skinnedMeshRenderer.realtimeLightmapScaleOffset);
			writer.WriteProperty<LightProbeUsage>("lightProbeUsage", skinnedMeshRenderer.lightProbeUsage);
			writer.WriteProperty<GameObject>("lightProbeProxyVolumeOverride", skinnedMeshRenderer.lightProbeProxyVolumeOverride);
			writer.WriteProperty<Transform>("probeAnchor", skinnedMeshRenderer.probeAnchor);
			writer.WriteProperty<ReflectionProbeUsage>("reflectionProbeUsage", skinnedMeshRenderer.reflectionProbeUsage);
			writer.WriteProperty<string>("sortingLayerName", skinnedMeshRenderer.sortingLayerName);
			writer.WriteProperty<int>("sortingLayerID", skinnedMeshRenderer.sortingLayerID);
			writer.WriteProperty<int>("sortingOrder", skinnedMeshRenderer.sortingOrder);
			writer.WriteProperty<string>("tag", skinnedMeshRenderer.tag);
			writer.WriteProperty<string>("name", skinnedMeshRenderer.name);
			writer.WriteProperty<HideFlags>("hideFlags", skinnedMeshRenderer.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = SaveGameType.CreateComponent<SkinnedMeshRenderer>();
			this.ReadInto(skinnedMeshRenderer, reader);
			return skinnedMeshRenderer;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "blendShapeWeights":
				{
					List<float> list = reader.ReadProperty<List<float>>();
					for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++)
					{
						skinnedMeshRenderer.SetBlendShapeWeight(i, list[i]);
					}
					break;
				}
				case "bones":
					skinnedMeshRenderer.bones = reader.ReadProperty<Transform[]>();
					break;
				case "rootBone":
					if (skinnedMeshRenderer.rootBone == null)
					{
						skinnedMeshRenderer.rootBone = reader.ReadProperty<Transform>();
					}
					else
					{
						reader.ReadIntoProperty<Transform>(skinnedMeshRenderer.rootBone);
					}
					break;
				case "quality":
					skinnedMeshRenderer.quality = reader.ReadProperty<SkinQuality>();
					break;
				case "sharedMesh":
					if (skinnedMeshRenderer.sharedMesh == null)
					{
						skinnedMeshRenderer.sharedMesh = reader.ReadProperty<Mesh>();
					}
					else
					{
						reader.ReadIntoProperty<Mesh>(skinnedMeshRenderer.sharedMesh);
					}
					break;
				case "updateWhenOffscreen":
					skinnedMeshRenderer.updateWhenOffscreen = reader.ReadProperty<bool>();
					break;
				case "skinnedMotionVectors":
					skinnedMeshRenderer.skinnedMotionVectors = reader.ReadProperty<bool>();
					break;
				case "localBounds":
					skinnedMeshRenderer.localBounds = reader.ReadProperty<Bounds>();
					break;
				case "enabled":
					skinnedMeshRenderer.enabled = reader.ReadProperty<bool>();
					break;
				case "shadowCastingMode":
					skinnedMeshRenderer.shadowCastingMode = reader.ReadProperty<ShadowCastingMode>();
					break;
				case "receiveShadows":
					skinnedMeshRenderer.receiveShadows = reader.ReadProperty<bool>();
					break;
				case "material":
					if (skinnedMeshRenderer.material == null)
					{
						skinnedMeshRenderer.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(skinnedMeshRenderer.material);
					}
					break;
				case "sharedMaterial":
					if (skinnedMeshRenderer.sharedMaterial == null)
					{
						skinnedMeshRenderer.sharedMaterial = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(skinnedMeshRenderer.sharedMaterial);
					}
					break;
				case "materials":
					skinnedMeshRenderer.materials = reader.ReadProperty<Material[]>();
					break;
				case "sharedMaterials":
					skinnedMeshRenderer.sharedMaterials = reader.ReadProperty<Material[]>();
					break;
				case "lightmapIndex":
					skinnedMeshRenderer.lightmapIndex = reader.ReadProperty<int>();
					break;
				case "realtimeLightmapIndex":
					skinnedMeshRenderer.realtimeLightmapIndex = reader.ReadProperty<int>();
					break;
				case "lightmapScaleOffset":
					skinnedMeshRenderer.lightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "motionVectorGenerationMode":
					skinnedMeshRenderer.motionVectorGenerationMode = reader.ReadProperty<MotionVectorGenerationMode>();
					break;
				case "realtimeLightmapScaleOffset":
					skinnedMeshRenderer.realtimeLightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "lightProbeUsage":
					skinnedMeshRenderer.lightProbeUsage = reader.ReadProperty<LightProbeUsage>();
					break;
				case "lightProbeProxyVolumeOverride":
					if (skinnedMeshRenderer.lightProbeProxyVolumeOverride == null)
					{
						skinnedMeshRenderer.lightProbeProxyVolumeOverride = reader.ReadProperty<GameObject>();
					}
					else
					{
						reader.ReadIntoProperty<GameObject>(skinnedMeshRenderer.lightProbeProxyVolumeOverride);
					}
					break;
				case "probeAnchor":
					if (skinnedMeshRenderer.probeAnchor == null)
					{
						skinnedMeshRenderer.probeAnchor = reader.ReadProperty<Transform>();
					}
					else
					{
						reader.ReadIntoProperty<Transform>(skinnedMeshRenderer.probeAnchor);
					}
					break;
				case "reflectionProbeUsage":
					skinnedMeshRenderer.reflectionProbeUsage = reader.ReadProperty<ReflectionProbeUsage>();
					break;
				case "sortingLayerName":
					skinnedMeshRenderer.sortingLayerName = reader.ReadProperty<string>();
					break;
				case "sortingLayerID":
					skinnedMeshRenderer.sortingLayerID = reader.ReadProperty<int>();
					break;
				case "sortingOrder":
					skinnedMeshRenderer.sortingOrder = reader.ReadProperty<int>();
					break;
				case "tag":
					skinnedMeshRenderer.tag = reader.ReadProperty<string>();
					break;
				case "name":
					skinnedMeshRenderer.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					skinnedMeshRenderer.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
