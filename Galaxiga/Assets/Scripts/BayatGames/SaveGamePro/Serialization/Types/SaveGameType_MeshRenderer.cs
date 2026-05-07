using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_MeshRenderer : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(MeshRenderer);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			MeshRenderer meshRenderer = (MeshRenderer)value;
			writer.WriteProperty<Mesh>("additionalVertexStreams", meshRenderer.additionalVertexStreams);
			writer.WriteProperty<bool>("enabled", meshRenderer.enabled);
			writer.WriteProperty<ShadowCastingMode>("shadowCastingMode", meshRenderer.shadowCastingMode);
			writer.WriteProperty<bool>("receiveShadows", meshRenderer.receiveShadows);
			writer.WriteProperty<Material>("material", meshRenderer.material);
			writer.WriteProperty<Material>("sharedMaterial", meshRenderer.sharedMaterial);
			writer.WriteProperty<Material[]>("materials", meshRenderer.materials);
			writer.WriteProperty<Material[]>("sharedMaterials", meshRenderer.sharedMaterials);
			writer.WriteProperty<int>("lightmapIndex", meshRenderer.lightmapIndex);
			writer.WriteProperty<int>("realtimeLightmapIndex", meshRenderer.realtimeLightmapIndex);
			writer.WriteProperty<Vector4>("lightmapScaleOffset", meshRenderer.lightmapScaleOffset);
			writer.WriteProperty<MotionVectorGenerationMode>("motionVectorGenerationMode", meshRenderer.motionVectorGenerationMode);
			writer.WriteProperty<Vector4>("realtimeLightmapScaleOffset", meshRenderer.realtimeLightmapScaleOffset);
			writer.WriteProperty<LightProbeUsage>("lightProbeUsage", meshRenderer.lightProbeUsage);
			writer.WriteProperty<GameObject>("lightProbeProxyVolumeOverride", meshRenderer.lightProbeProxyVolumeOverride);
			writer.WriteProperty<Transform>("probeAnchor", meshRenderer.probeAnchor);
			writer.WriteProperty<ReflectionProbeUsage>("reflectionProbeUsage", meshRenderer.reflectionProbeUsage);
			writer.WriteProperty<string>("sortingLayerName", meshRenderer.sortingLayerName);
			writer.WriteProperty<int>("sortingLayerID", meshRenderer.sortingLayerID);
			writer.WriteProperty<int>("sortingOrder", meshRenderer.sortingOrder);
			writer.WriteProperty<string>("tag", meshRenderer.tag);
			writer.WriteProperty<string>("name", meshRenderer.name);
			writer.WriteProperty<HideFlags>("hideFlags", meshRenderer.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			MeshRenderer meshRenderer = SaveGameType.CreateComponent<MeshRenderer>();
			this.ReadInto(meshRenderer, reader);
			return meshRenderer;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			MeshRenderer meshRenderer = (MeshRenderer)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "additionalVertexStreams":
					if (meshRenderer.additionalVertexStreams == null)
					{
						meshRenderer.additionalVertexStreams = reader.ReadProperty<Mesh>();
					}
					else
					{
						reader.ReadIntoProperty<Mesh>(meshRenderer.additionalVertexStreams);
					}
					break;
				case "enabled":
					meshRenderer.enabled = reader.ReadProperty<bool>();
					break;
				case "shadowCastingMode":
					meshRenderer.shadowCastingMode = reader.ReadProperty<ShadowCastingMode>();
					break;
				case "receiveShadows":
					meshRenderer.receiveShadows = reader.ReadProperty<bool>();
					break;
				case "material":
					if (meshRenderer.material == null)
					{
						meshRenderer.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(meshRenderer.material);
					}
					break;
				case "sharedMaterial":
					if (meshRenderer.sharedMaterial == null)
					{
						meshRenderer.sharedMaterial = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(meshRenderer.sharedMaterial);
					}
					break;
				case "materials":
					reader.ReadIntoProperty<Material[]>(meshRenderer.materials);
					break;
				case "sharedMaterials":
					reader.ReadIntoProperty<Material[]>(meshRenderer.sharedMaterials);
					break;
				case "lightmapIndex":
					meshRenderer.lightmapIndex = reader.ReadProperty<int>();
					break;
				case "realtimeLightmapIndex":
					meshRenderer.realtimeLightmapIndex = reader.ReadProperty<int>();
					break;
				case "lightmapScaleOffset":
					meshRenderer.lightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "motionVectorGenerationMode":
					meshRenderer.motionVectorGenerationMode = reader.ReadProperty<MotionVectorGenerationMode>();
					break;
				case "realtimeLightmapScaleOffset":
					meshRenderer.realtimeLightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "lightProbeUsage":
					meshRenderer.lightProbeUsage = reader.ReadProperty<LightProbeUsage>();
					break;
				case "lightProbeProxyVolumeOverride":
					if (meshRenderer.lightProbeProxyVolumeOverride == null)
					{
						meshRenderer.lightProbeProxyVolumeOverride = reader.ReadProperty<GameObject>();
					}
					else
					{
						reader.ReadIntoProperty<GameObject>(meshRenderer.lightProbeProxyVolumeOverride);
					}
					break;
				case "probeAnchor":
					if (meshRenderer.probeAnchor == null)
					{
						meshRenderer.probeAnchor = reader.ReadProperty<Transform>();
					}
					else
					{
						reader.ReadIntoProperty<Transform>(meshRenderer.probeAnchor);
					}
					break;
				case "reflectionProbeUsage":
					meshRenderer.reflectionProbeUsage = reader.ReadProperty<ReflectionProbeUsage>();
					break;
				case "sortingLayerName":
					meshRenderer.sortingLayerName = reader.ReadProperty<string>();
					break;
				case "sortingLayerID":
					meshRenderer.sortingLayerID = reader.ReadProperty<int>();
					break;
				case "sortingOrder":
					meshRenderer.sortingOrder = reader.ReadProperty<int>();
					break;
				case "tag":
					meshRenderer.tag = reader.ReadProperty<string>();
					break;
				case "name":
					meshRenderer.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					meshRenderer.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
