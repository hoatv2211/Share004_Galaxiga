using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Spine.Unity
{
	public class AtlasAsset : ScriptableObject
	{
		public bool IsLoaded
		{
			get
			{
				return this.atlas != null;
			}
		}

		public static AtlasAsset CreateRuntimeInstance(TextAsset atlasText, Material[] materials, bool initialize)
		{
			AtlasAsset atlasAsset = ScriptableObject.CreateInstance<AtlasAsset>();
			atlasAsset.Reset();
			atlasAsset.atlasFile = atlasText;
			atlasAsset.materials = materials;
			if (initialize)
			{
				atlasAsset.GetAtlas();
			}
			return atlasAsset;
		}

		public static AtlasAsset CreateRuntimeInstance(TextAsset atlasText, Texture2D[] textures, Material materialPropertySource, bool initialize)
		{
			string text = atlasText.text;
			text = text.Replace("\r", string.Empty);
			string[] array = text.Split(new char[]
			{
				'\n'
			});
			List<string> list = new List<string>();
			for (int i = 0; i < array.Length - 1; i++)
			{
				if (array[i].Trim().Length == 0)
				{
					list.Add(array[i + 1].Trim().Replace(".png", string.Empty));
				}
			}
			Material[] array2 = new Material[list.Count];
			int j = 0;
			int count = list.Count;
			while (j < count)
			{
				Material material = null;
				string a = list[j];
				int k = 0;
				int num = textures.Length;
				while (k < num)
				{
					if (string.Equals(a, textures[k].name, StringComparison.OrdinalIgnoreCase))
					{
						material = new Material(materialPropertySource);
						material.mainTexture = textures[k];
						break;
					}
					k++;
				}
				if (!(material != null))
				{
					throw new ArgumentException("Could not find matching atlas page in the texture array.");
				}
				array2[j] = material;
				j++;
			}
			return AtlasAsset.CreateRuntimeInstance(atlasText, array2, initialize);
		}

		public static AtlasAsset CreateRuntimeInstance(TextAsset atlasText, Texture2D[] textures, Shader shader, bool initialize)
		{
			if (shader == null)
			{
				shader = Shader.Find("Spine/Skeleton");
			}
			Material materialPropertySource = new Material(shader);
			return AtlasAsset.CreateRuntimeInstance(atlasText, textures, materialPropertySource, initialize);
		}

		private void Reset()
		{
			this.Clear();
		}

		public virtual void Clear()
		{
			this.atlas = null;
		}

		public virtual Atlas GetAtlas()
		{
			if (this.atlasFile == null)
			{
				UnityEngine.Debug.LogError("Atlas file not set for atlas asset: " + base.name, this);
				this.Clear();
				return null;
			}
			if (this.materials == null || this.materials.Length == 0)
			{
				UnityEngine.Debug.LogError("Materials not set for atlas asset: " + base.name, this);
				this.Clear();
				return null;
			}
			if (this.atlas != null)
			{
				return this.atlas;
			}
			Atlas result;
			try
			{
				this.atlas = new Atlas(new StringReader(this.atlasFile.text), string.Empty, new MaterialsTextureLoader(this));
				this.atlas.FlipV();
				result = this.atlas;
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogError(string.Concat(new string[]
				{
					"Error reading atlas file for atlas asset: ",
					base.name,
					"\n",
					ex.Message,
					"\n",
					ex.StackTrace
				}), this);
				result = null;
			}
			return result;
		}

		public Mesh GenerateMesh(string name, Mesh mesh, out Material material, float scale = 0.01f)
		{
			AtlasRegion atlasRegion = this.atlas.FindRegion(name);
			material = null;
			if (atlasRegion != null)
			{
				if (mesh == null)
				{
					mesh = new Mesh();
					mesh.name = name;
				}
				Vector3[] array = new Vector3[4];
				Vector2[] array2 = new Vector2[4];
				Color[] colors = new Color[]
				{
					Color.white,
					Color.white,
					Color.white,
					Color.white
				};
				int[] triangles = new int[]
				{
					0,
					1,
					2,
					2,
					3,
					0
				};
				float num = (float)atlasRegion.width / -2f;
				float x = num * -1f;
				float num2 = (float)atlasRegion.height / 2f;
				float y = num2 * -1f;
				array[0] = new Vector3(num, y, 0f) * scale;
				array[1] = new Vector3(num, num2, 0f) * scale;
				array[2] = new Vector3(x, num2, 0f) * scale;
				array[3] = new Vector3(x, y, 0f) * scale;
				float u = atlasRegion.u;
				float v = atlasRegion.v;
				float u2 = atlasRegion.u2;
				float v2 = atlasRegion.v2;
				if (!atlasRegion.rotate)
				{
					array2[0] = new Vector2(u, v2);
					array2[1] = new Vector2(u, v);
					array2[2] = new Vector2(u2, v);
					array2[3] = new Vector2(u2, v2);
				}
				else
				{
					array2[0] = new Vector2(u2, v2);
					array2[1] = new Vector2(u, v2);
					array2[2] = new Vector2(u, v);
					array2[3] = new Vector2(u2, v);
				}
				mesh.triangles = new int[0];
				mesh.vertices = array;
				mesh.uv = array2;
				mesh.colors = colors;
				mesh.triangles = triangles;
				mesh.RecalculateNormals();
				mesh.RecalculateBounds();
				material = (Material)atlasRegion.page.rendererObject;
			}
			else
			{
				mesh = null;
			}
			return mesh;
		}

		public TextAsset atlasFile;

		public Material[] materials;

		protected Atlas atlas;
	}
}
