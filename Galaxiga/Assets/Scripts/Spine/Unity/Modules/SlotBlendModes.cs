using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Modules
{
	[DisallowMultipleComponent]
	public class SlotBlendModes : MonoBehaviour
	{
		internal static Dictionary<SlotBlendModes.MaterialTexturePair, Material> MaterialTable
		{
			get
			{
				if (SlotBlendModes.materialTable == null)
				{
					SlotBlendModes.materialTable = new Dictionary<SlotBlendModes.MaterialTexturePair, Material>();
				}
				return SlotBlendModes.materialTable;
			}
		}

		internal static Material GetMaterialFor(Material materialSource, Texture2D texture)
		{
			if (materialSource == null || texture == null)
			{
				return null;
			}
			Dictionary<SlotBlendModes.MaterialTexturePair, Material> dictionary = SlotBlendModes.MaterialTable;
			SlotBlendModes.MaterialTexturePair key = new SlotBlendModes.MaterialTexturePair
			{
				material = materialSource,
				texture2D = texture
			};
			Material material;
			if (!dictionary.TryGetValue(key, out material))
			{
				material = new Material(materialSource);
				material.name = "(Clone)" + texture.name + "-" + materialSource.name;
				material.mainTexture = texture;
				dictionary[key] = material;
			}
			return material;
		}

		public bool Applied { get; private set; }

		private void Start()
		{
			if (!this.Applied)
			{
				this.Apply();
			}
		}

		private void OnDestroy()
		{
			if (this.Applied)
			{
				this.Remove();
			}
		}

		public void Apply()
		{
			this.GetTexture();
			if (this.texture == null)
			{
				return;
			}
			SkeletonRenderer component = base.GetComponent<SkeletonRenderer>();
			if (component == null)
			{
				return;
			}
			Dictionary<Slot, Material> customSlotMaterials = component.CustomSlotMaterials;
			foreach (Slot slot in component.Skeleton.Slots)
			{
				BlendMode blendMode = slot.data.blendMode;
				if (blendMode != BlendMode.Multiply)
				{
					if (blendMode == BlendMode.Screen)
					{
						if (this.screenMaterialSource != null)
						{
							customSlotMaterials[slot] = SlotBlendModes.GetMaterialFor(this.screenMaterialSource, this.texture);
						}
					}
				}
				else if (this.multiplyMaterialSource != null)
				{
					customSlotMaterials[slot] = SlotBlendModes.GetMaterialFor(this.multiplyMaterialSource, this.texture);
				}
			}
			this.Applied = true;
			component.LateUpdate();
		}

		public void Remove()
		{
			this.GetTexture();
			if (this.texture == null)
			{
				return;
			}
			SkeletonRenderer component = base.GetComponent<SkeletonRenderer>();
			if (component == null)
			{
				return;
			}
			Dictionary<Slot, Material> customSlotMaterials = component.CustomSlotMaterials;
			foreach (Slot slot in component.Skeleton.Slots)
			{
				Material objA = null;
				BlendMode blendMode = slot.data.blendMode;
				if (blendMode != BlendMode.Multiply)
				{
					if (blendMode == BlendMode.Screen)
					{
						if (customSlotMaterials.TryGetValue(slot, out objA) && object.ReferenceEquals(objA, SlotBlendModes.GetMaterialFor(this.screenMaterialSource, this.texture)))
						{
							customSlotMaterials.Remove(slot);
						}
					}
				}
				else if (customSlotMaterials.TryGetValue(slot, out objA) && object.ReferenceEquals(objA, SlotBlendModes.GetMaterialFor(this.multiplyMaterialSource, this.texture)))
				{
					customSlotMaterials.Remove(slot);
				}
			}
			this.Applied = false;
			if (component.valid)
			{
				component.LateUpdate();
			}
		}

		public void GetTexture()
		{
			if (this.texture == null)
			{
				SkeletonRenderer component = base.GetComponent<SkeletonRenderer>();
				if (component == null)
				{
					return;
				}
				SkeletonDataAsset skeletonDataAsset = component.skeletonDataAsset;
				if (skeletonDataAsset == null)
				{
					return;
				}
				AtlasAsset atlasAsset = skeletonDataAsset.atlasAssets[0];
				if (atlasAsset == null)
				{
					return;
				}
				Material material = atlasAsset.materials[0];
				if (material == null)
				{
					return;
				}
				this.texture = (material.mainTexture as Texture2D);
			}
		}

		private static Dictionary<SlotBlendModes.MaterialTexturePair, Material> materialTable;

		public Material multiplyMaterialSource;

		public Material screenMaterialSource;

		private Texture2D texture;

		public struct MaterialTexturePair
		{
			public Texture2D texture2D;

			public Material material;
		}
	}
}
