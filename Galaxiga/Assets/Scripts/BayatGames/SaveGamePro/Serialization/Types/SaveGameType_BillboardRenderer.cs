using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_BillboardRenderer : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(BillboardRenderer);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			BillboardRenderer billboardRenderer = (BillboardRenderer)value;
			writer.WriteProperty<BillboardAsset>("billboard", billboardRenderer.billboard);
			writer.WriteProperty<bool>("enabled", billboardRenderer.enabled);
			writer.WriteProperty<ShadowCastingMode>("shadowCastingMode", billboardRenderer.shadowCastingMode);
			writer.WriteProperty<bool>("receiveShadows", billboardRenderer.receiveShadows);
			writer.WriteProperty<Material>("material", billboardRenderer.material);
			writer.WriteProperty<Material>("sharedMaterial", billboardRenderer.sharedMaterial);
			writer.WriteProperty<Material[]>("materials", billboardRenderer.materials);
			writer.WriteProperty<Material[]>("sharedMaterials", billboardRenderer.sharedMaterials);
			writer.WriteProperty<int>("lightmapIndex", billboardRenderer.lightmapIndex);
			writer.WriteProperty<int>("realtimeLightmapIndex", billboardRenderer.realtimeLightmapIndex);
			writer.WriteProperty<Vector4>("lightmapScaleOffset", billboardRenderer.lightmapScaleOffset);
			writer.WriteProperty<MotionVectorGenerationMode>("motionVectorGenerationMode", billboardRenderer.motionVectorGenerationMode);
			writer.WriteProperty<Vector4>("realtimeLightmapScaleOffset", billboardRenderer.realtimeLightmapScaleOffset);
			writer.WriteProperty<LightProbeUsage>("lightProbeUsage", billboardRenderer.lightProbeUsage);
			writer.WriteProperty<GameObject>("lightProbeProxyVolumeOverride", billboardRenderer.lightProbeProxyVolumeOverride);
			writer.WriteProperty<Transform>("probeAnchor", billboardRenderer.probeAnchor);
			writer.WriteProperty<ReflectionProbeUsage>("reflectionProbeUsage", billboardRenderer.reflectionProbeUsage);
			writer.WriteProperty<string>("sortingLayerName", billboardRenderer.sortingLayerName);
			writer.WriteProperty<int>("sortingLayerID", billboardRenderer.sortingLayerID);
			writer.WriteProperty<int>("sortingOrder", billboardRenderer.sortingOrder);
			writer.WriteProperty<string>("tag", billboardRenderer.tag);
			writer.WriteProperty<string>("name", billboardRenderer.name);
			writer.WriteProperty<HideFlags>("hideFlags", billboardRenderer.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			BillboardRenderer billboardRenderer = SaveGameType.CreateComponent<BillboardRenderer>();
			this.ReadInto(billboardRenderer, reader);
			return billboardRenderer;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			BillboardRenderer billboardRenderer = (BillboardRenderer)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "billboard":
					if (billboardRenderer.billboard == null)
					{
						billboardRenderer.billboard = reader.ReadProperty<BillboardAsset>();
					}
					else
					{
						reader.ReadIntoProperty<BillboardAsset>(billboardRenderer.billboard);
					}
					break;
				case "enabled":
					billboardRenderer.enabled = reader.ReadProperty<bool>();
					break;
				case "shadowCastingMode":
					billboardRenderer.shadowCastingMode = reader.ReadProperty<ShadowCastingMode>();
					break;
				case "receiveShadows":
					billboardRenderer.receiveShadows = reader.ReadProperty<bool>();
					break;
				case "material":
					if (billboardRenderer.material == null)
					{
						billboardRenderer.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(billboardRenderer.material);
					}
					break;
				case "sharedMaterial":
					if (billboardRenderer.sharedMaterial == null)
					{
						billboardRenderer.sharedMaterial = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(billboardRenderer.sharedMaterial);
					}
					break;
				case "materials":
					billboardRenderer.materials = reader.ReadProperty<Material[]>();
					break;
				case "sharedMaterials":
					billboardRenderer.sharedMaterials = reader.ReadProperty<Material[]>();
					break;
				case "lightmapIndex":
					billboardRenderer.lightmapIndex = reader.ReadProperty<int>();
					break;
				case "realtimeLightmapIndex":
					billboardRenderer.realtimeLightmapIndex = reader.ReadProperty<int>();
					break;
				case "lightmapScaleOffset":
					billboardRenderer.lightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "motionVectorGenerationMode":
					billboardRenderer.motionVectorGenerationMode = reader.ReadProperty<MotionVectorGenerationMode>();
					break;
				case "realtimeLightmapScaleOffset":
					billboardRenderer.realtimeLightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "lightProbeUsage":
					billboardRenderer.lightProbeUsage = reader.ReadProperty<LightProbeUsage>();
					break;
				case "lightProbeProxyVolumeOverride":
					if (billboardRenderer.lightProbeProxyVolumeOverride == null)
					{
						billboardRenderer.lightProbeProxyVolumeOverride = reader.ReadProperty<GameObject>();
					}
					else
					{
						reader.ReadIntoProperty<GameObject>(billboardRenderer.lightProbeProxyVolumeOverride);
					}
					break;
				case "probeAnchor":
					if (billboardRenderer.probeAnchor == null)
					{
						billboardRenderer.probeAnchor = reader.ReadProperty<Transform>();
					}
					else
					{
						reader.ReadIntoProperty<Transform>(billboardRenderer.probeAnchor);
					}
					break;
				case "reflectionProbeUsage":
					billboardRenderer.reflectionProbeUsage = reader.ReadProperty<ReflectionProbeUsage>();
					break;
				case "sortingLayerName":
					billboardRenderer.sortingLayerName = reader.ReadProperty<string>();
					break;
				case "sortingLayerID":
					billboardRenderer.sortingLayerID = reader.ReadProperty<int>();
					break;
				case "sortingOrder":
					billboardRenderer.sortingOrder = reader.ReadProperty<int>();
					break;
				case "tag":
					billboardRenderer.tag = reader.ReadProperty<string>();
					break;
				case "name":
					billboardRenderer.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					billboardRenderer.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
