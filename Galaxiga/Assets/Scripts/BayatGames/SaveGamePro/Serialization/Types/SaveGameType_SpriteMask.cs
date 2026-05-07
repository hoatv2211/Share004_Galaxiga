using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SpriteMask : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SpriteMask);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SpriteMask spriteMask = (SpriteMask)value;
			writer.WriteProperty<Sprite>("sprite", spriteMask.sprite);
			writer.WriteProperty<float>("alphaCutoff", spriteMask.alphaCutoff);
			writer.WriteProperty<bool>("isCustomRangeActive", spriteMask.isCustomRangeActive);
			writer.WriteProperty<int>("frontSortingLayerID", spriteMask.frontSortingLayerID);
			writer.WriteProperty<int>("frontSortingOrder", spriteMask.frontSortingOrder);
			writer.WriteProperty<int>("backSortingLayerID", spriteMask.backSortingLayerID);
			writer.WriteProperty<int>("backSortingOrder", spriteMask.backSortingOrder);
			writer.WriteProperty<bool>("enabled", spriteMask.enabled);
			writer.WriteProperty<ShadowCastingMode>("shadowCastingMode", spriteMask.shadowCastingMode);
			writer.WriteProperty<bool>("receiveShadows", spriteMask.receiveShadows);
			writer.WriteProperty<Material>("material", spriteMask.material);
			writer.WriteProperty<Material>("sharedMaterial", spriteMask.sharedMaterial);
			writer.WriteProperty<Material[]>("materials", spriteMask.materials);
			writer.WriteProperty<Material[]>("sharedMaterials", spriteMask.sharedMaterials);
			writer.WriteProperty<int>("lightmapIndex", spriteMask.lightmapIndex);
			writer.WriteProperty<int>("realtimeLightmapIndex", spriteMask.realtimeLightmapIndex);
			writer.WriteProperty<Vector4>("lightmapScaleOffset", spriteMask.lightmapScaleOffset);
			writer.WriteProperty<MotionVectorGenerationMode>("motionVectorGenerationMode", spriteMask.motionVectorGenerationMode);
			writer.WriteProperty<Vector4>("realtimeLightmapScaleOffset", spriteMask.realtimeLightmapScaleOffset);
			writer.WriteProperty<LightProbeUsage>("lightProbeUsage", spriteMask.lightProbeUsage);
			writer.WriteProperty<GameObject>("lightProbeProxyVolumeOverride", spriteMask.lightProbeProxyVolumeOverride);
			writer.WriteProperty<Transform>("probeAnchor", spriteMask.probeAnchor);
			writer.WriteProperty<ReflectionProbeUsage>("reflectionProbeUsage", spriteMask.reflectionProbeUsage);
			writer.WriteProperty<string>("sortingLayerName", spriteMask.sortingLayerName);
			writer.WriteProperty<int>("sortingLayerID", spriteMask.sortingLayerID);
			writer.WriteProperty<int>("sortingOrder", spriteMask.sortingOrder);
			writer.WriteProperty<string>("tag", spriteMask.tag);
			writer.WriteProperty<string>("name", spriteMask.name);
			writer.WriteProperty<HideFlags>("hideFlags", spriteMask.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			SpriteMask spriteMask = SaveGameType.CreateComponent<SpriteMask>();
			this.ReadInto(spriteMask, reader);
			return spriteMask;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			SpriteMask spriteMask = (SpriteMask)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "sprite":
					if (spriteMask.sprite == null)
					{
						spriteMask.sprite = reader.ReadProperty<Sprite>();
					}
					else
					{
						reader.ReadIntoProperty<Sprite>(spriteMask.sprite);
					}
					break;
				case "alphaCutoff":
					spriteMask.alphaCutoff = reader.ReadProperty<float>();
					break;
				case "isCustomRangeActive":
					spriteMask.isCustomRangeActive = reader.ReadProperty<bool>();
					break;
				case "frontSortingLayerID":
					spriteMask.frontSortingLayerID = reader.ReadProperty<int>();
					break;
				case "frontSortingOrder":
					spriteMask.frontSortingOrder = reader.ReadProperty<int>();
					break;
				case "backSortingLayerID":
					spriteMask.backSortingLayerID = reader.ReadProperty<int>();
					break;
				case "backSortingOrder":
					spriteMask.backSortingOrder = reader.ReadProperty<int>();
					break;
				case "enabled":
					spriteMask.enabled = reader.ReadProperty<bool>();
					break;
				case "shadowCastingMode":
					spriteMask.shadowCastingMode = reader.ReadProperty<ShadowCastingMode>();
					break;
				case "receiveShadows":
					spriteMask.receiveShadows = reader.ReadProperty<bool>();
					break;
				case "material":
					if (spriteMask.material == null)
					{
						spriteMask.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(spriteMask.material);
					}
					break;
				case "sharedMaterial":
					if (spriteMask.sharedMaterial == null)
					{
						spriteMask.sharedMaterial = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(spriteMask.sharedMaterial);
					}
					break;
				case "materials":
					spriteMask.materials = reader.ReadProperty<Material[]>();
					break;
				case "sharedMaterials":
					spriteMask.sharedMaterials = reader.ReadProperty<Material[]>();
					break;
				case "lightmapIndex":
					spriteMask.lightmapIndex = reader.ReadProperty<int>();
					break;
				case "realtimeLightmapIndex":
					spriteMask.realtimeLightmapIndex = reader.ReadProperty<int>();
					break;
				case "lightmapScaleOffset":
					spriteMask.lightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "motionVectorGenerationMode":
					spriteMask.motionVectorGenerationMode = reader.ReadProperty<MotionVectorGenerationMode>();
					break;
				case "realtimeLightmapScaleOffset":
					spriteMask.realtimeLightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "lightProbeUsage":
					spriteMask.lightProbeUsage = reader.ReadProperty<LightProbeUsage>();
					break;
				case "lightProbeProxyVolumeOverride":
					if (spriteMask.lightProbeProxyVolumeOverride == null)
					{
						spriteMask.lightProbeProxyVolumeOverride = reader.ReadProperty<GameObject>();
					}
					else
					{
						reader.ReadIntoProperty<GameObject>(spriteMask.lightProbeProxyVolumeOverride);
					}
					break;
				case "probeAnchor":
					if (spriteMask.probeAnchor == null)
					{
						spriteMask.probeAnchor = reader.ReadProperty<Transform>();
					}
					else
					{
						reader.ReadIntoProperty<Transform>(spriteMask.probeAnchor);
					}
					break;
				case "reflectionProbeUsage":
					spriteMask.reflectionProbeUsage = reader.ReadProperty<ReflectionProbeUsage>();
					break;
				case "sortingLayerName":
					spriteMask.sortingLayerName = reader.ReadProperty<string>();
					break;
				case "sortingLayerID":
					spriteMask.sortingLayerID = reader.ReadProperty<int>();
					break;
				case "sortingOrder":
					spriteMask.sortingOrder = reader.ReadProperty<int>();
					break;
				case "tag":
					spriteMask.tag = reader.ReadProperty<string>();
					break;
				case "name":
					spriteMask.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					spriteMask.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
