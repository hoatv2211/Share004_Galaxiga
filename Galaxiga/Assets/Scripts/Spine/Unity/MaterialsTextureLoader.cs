using System;
using System.IO;
using UnityEngine;

namespace Spine.Unity
{
	public class MaterialsTextureLoader : TextureLoader
	{
		public MaterialsTextureLoader(AtlasAsset atlasAsset)
		{
			this.atlasAsset = atlasAsset;
		}

		public void Load(AtlasPage page, string path)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
			Material material = null;
			foreach (Material material2 in this.atlasAsset.materials)
			{
				if (material2.mainTexture == null)
				{
					UnityEngine.Debug.LogError("Material is missing texture: " + material2.name, material2);
					return;
				}
				if (material2.mainTexture.name == fileNameWithoutExtension)
				{
					material = material2;
					break;
				}
			}
			if (material == null)
			{
				UnityEngine.Debug.LogError("Material with texture name \"" + fileNameWithoutExtension + "\" not found for atlas asset: " + this.atlasAsset.name, this.atlasAsset);
				return;
			}
			page.rendererObject = material;
			if (page.width == 0 || page.height == 0)
			{
				page.width = material.mainTexture.width;
				page.height = material.mainTexture.height;
			}
		}

		public void Unload(object texture)
		{
		}

		private AtlasAsset atlasAsset;
	}
}
