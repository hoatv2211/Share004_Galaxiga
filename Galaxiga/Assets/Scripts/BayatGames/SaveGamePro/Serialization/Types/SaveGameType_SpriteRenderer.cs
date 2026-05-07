using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SpriteRenderer : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SpriteRenderer);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SpriteRenderer spriteRenderer = (SpriteRenderer)value;
			writer.WriteProperty<Sprite>("sprite", spriteRenderer.sprite);
			writer.WriteProperty<SpriteDrawMode>("drawMode", spriteRenderer.drawMode);
			writer.WriteProperty<Vector2>("size", spriteRenderer.size);
			writer.WriteProperty<float>("adaptiveModeThreshold", spriteRenderer.adaptiveModeThreshold);
			writer.WriteProperty<SpriteTileMode>("tileMode", spriteRenderer.tileMode);
			writer.WriteProperty<Color>("color", spriteRenderer.color);
			writer.WriteProperty<bool>("flipX", spriteRenderer.flipX);
			writer.WriteProperty<bool>("flipY", spriteRenderer.flipY);
			writer.WriteProperty<SpriteMaskInteraction>("maskInteraction", spriteRenderer.maskInteraction);
			writer.WriteProperty<bool>("enabled", spriteRenderer.enabled);
			writer.WriteProperty<ShadowCastingMode>("shadowCastingMode", spriteRenderer.shadowCastingMode);
			writer.WriteProperty<bool>("receiveShadows", spriteRenderer.receiveShadows);
			writer.WriteProperty<Material>("material", spriteRenderer.material);
			writer.WriteProperty<Material>("sharedMaterial", spriteRenderer.sharedMaterial);
			writer.WriteProperty<Material[]>("materials", spriteRenderer.materials);
			writer.WriteProperty<Material[]>("sharedMaterials", spriteRenderer.sharedMaterials);
			writer.WriteProperty<int>("lightmapIndex", spriteRenderer.lightmapIndex);
			writer.WriteProperty<int>("realtimeLightmapIndex", spriteRenderer.realtimeLightmapIndex);
			writer.WriteProperty<Vector4>("lightmapScaleOffset", spriteRenderer.lightmapScaleOffset);
			writer.WriteProperty<MotionVectorGenerationMode>("motionVectorGenerationMode", spriteRenderer.motionVectorGenerationMode);
			writer.WriteProperty<Vector4>("realtimeLightmapScaleOffset", spriteRenderer.realtimeLightmapScaleOffset);
			writer.WriteProperty<LightProbeUsage>("lightProbeUsage", spriteRenderer.lightProbeUsage);
			writer.WriteProperty<GameObject>("lightProbeProxyVolumeOverride", spriteRenderer.lightProbeProxyVolumeOverride);
			writer.WriteProperty<Transform>("probeAnchor", spriteRenderer.probeAnchor);
			writer.WriteProperty<ReflectionProbeUsage>("reflectionProbeUsage", spriteRenderer.reflectionProbeUsage);
			writer.WriteProperty<string>("sortingLayerName", spriteRenderer.sortingLayerName);
			writer.WriteProperty<int>("sortingLayerID", spriteRenderer.sortingLayerID);
			writer.WriteProperty<int>("sortingOrder", spriteRenderer.sortingOrder);
			writer.WriteProperty<string>("tag", spriteRenderer.tag);
			writer.WriteProperty<string>("name", spriteRenderer.name);
			writer.WriteProperty<HideFlags>("hideFlags", spriteRenderer.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			SpriteRenderer spriteRenderer = SaveGameType.CreateComponent<SpriteRenderer>();
			this.ReadInto(spriteRenderer, reader);
			return spriteRenderer;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			SpriteRenderer spriteRenderer = (SpriteRenderer)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "sprite":
					if (spriteRenderer.sprite == null)
					{
						spriteRenderer.sprite = reader.ReadProperty<Sprite>();
					}
					else
					{
						reader.ReadIntoProperty<Sprite>(spriteRenderer.sprite);
					}
					break;
				case "drawMode":
					spriteRenderer.drawMode = reader.ReadProperty<SpriteDrawMode>();
					break;
				case "size":
					spriteRenderer.size = reader.ReadProperty<Vector2>();
					break;
				case "adaptiveModeThreshold":
					spriteRenderer.adaptiveModeThreshold = reader.ReadProperty<float>();
					break;
				case "tileMode":
					spriteRenderer.tileMode = reader.ReadProperty<SpriteTileMode>();
					break;
				case "color":
					spriteRenderer.color = reader.ReadProperty<Color>();
					break;
				case "flipX":
					spriteRenderer.flipX = reader.ReadProperty<bool>();
					break;
				case "flipY":
					spriteRenderer.flipY = reader.ReadProperty<bool>();
					break;
				case "maskInteraction":
					spriteRenderer.maskInteraction = reader.ReadProperty<SpriteMaskInteraction>();
					break;
				case "enabled":
					spriteRenderer.enabled = reader.ReadProperty<bool>();
					break;
				case "shadowCastingMode":
					spriteRenderer.shadowCastingMode = reader.ReadProperty<ShadowCastingMode>();
					break;
				case "receiveShadows":
					spriteRenderer.receiveShadows = reader.ReadProperty<bool>();
					break;
				case "material":
					if (spriteRenderer.material == null)
					{
						spriteRenderer.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(spriteRenderer.material);
					}
					break;
				case "sharedMaterial":
					if (spriteRenderer.sharedMaterial == null)
					{
						spriteRenderer.sharedMaterial = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(spriteRenderer.sharedMaterial);
					}
					break;
				case "materials":
					spriteRenderer.materials = reader.ReadProperty<Material[]>();
					break;
				case "sharedMaterials":
					spriteRenderer.sharedMaterials = reader.ReadProperty<Material[]>();
					break;
				case "lightmapIndex":
					spriteRenderer.lightmapIndex = reader.ReadProperty<int>();
					break;
				case "realtimeLightmapIndex":
					spriteRenderer.realtimeLightmapIndex = reader.ReadProperty<int>();
					break;
				case "lightmapScaleOffset":
					spriteRenderer.lightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "motionVectorGenerationMode":
					spriteRenderer.motionVectorGenerationMode = reader.ReadProperty<MotionVectorGenerationMode>();
					break;
				case "realtimeLightmapScaleOffset":
					spriteRenderer.realtimeLightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "lightProbeUsage":
					spriteRenderer.lightProbeUsage = reader.ReadProperty<LightProbeUsage>();
					break;
				case "lightProbeProxyVolumeOverride":
					if (spriteRenderer.lightProbeProxyVolumeOverride == null)
					{
						spriteRenderer.lightProbeProxyVolumeOverride = reader.ReadProperty<GameObject>();
					}
					else
					{
						reader.ReadIntoProperty<GameObject>(spriteRenderer.lightProbeProxyVolumeOverride);
					}
					break;
				case "probeAnchor":
					if (spriteRenderer.probeAnchor == null)
					{
						spriteRenderer.probeAnchor = reader.ReadProperty<Transform>();
					}
					else
					{
						reader.ReadIntoProperty<Transform>(spriteRenderer.probeAnchor);
					}
					break;
				case "reflectionProbeUsage":
					spriteRenderer.reflectionProbeUsage = reader.ReadProperty<ReflectionProbeUsage>();
					break;
				case "sortingLayerName":
					spriteRenderer.sortingLayerName = reader.ReadProperty<string>();
					break;
				case "sortingLayerID":
					spriteRenderer.sortingLayerID = reader.ReadProperty<int>();
					break;
				case "sortingOrder":
					spriteRenderer.sortingOrder = reader.ReadProperty<int>();
					break;
				case "tag":
					spriteRenderer.tag = reader.ReadProperty<string>();
					break;
				case "name":
					spriteRenderer.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					spriteRenderer.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
