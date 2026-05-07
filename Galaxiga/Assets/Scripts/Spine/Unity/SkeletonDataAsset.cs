using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Spine.Unity
{
	public class SkeletonDataAsset : ScriptableObject
	{
		public bool IsLoaded
		{
			get
			{
				return this.skeletonData != null;
			}
		}

		private void Reset()
		{
			this.Clear();
		}

		public static SkeletonDataAsset CreateRuntimeInstance(TextAsset skeletonDataFile, AtlasAsset atlasAsset, bool initialize, float scale = 0.01f)
		{
			return SkeletonDataAsset.CreateRuntimeInstance(skeletonDataFile, new AtlasAsset[]
			{
				atlasAsset
			}, initialize, scale);
		}

		public static SkeletonDataAsset CreateRuntimeInstance(TextAsset skeletonDataFile, AtlasAsset[] atlasAssets, bool initialize, float scale = 0.01f)
		{
			SkeletonDataAsset skeletonDataAsset = ScriptableObject.CreateInstance<SkeletonDataAsset>();
			skeletonDataAsset.Clear();
			skeletonDataAsset.skeletonJSON = skeletonDataFile;
			skeletonDataAsset.atlasAssets = atlasAssets;
			skeletonDataAsset.scale = scale;
			if (initialize)
			{
				skeletonDataAsset.GetSkeletonData(true);
			}
			return skeletonDataAsset;
		}

		public void Clear()
		{
			this.skeletonData = null;
			this.stateData = null;
		}

		public SkeletonData GetSkeletonData(bool quiet)
		{
			if (this.skeletonJSON == null)
			{
				if (!quiet)
				{
					UnityEngine.Debug.LogError("Skeleton JSON file not set for SkeletonData asset: " + base.name, this);
				}
				this.Clear();
				return null;
			}
			if (this.skeletonData != null)
			{
				return this.skeletonData;
			}
			Atlas[] atlasArray = this.GetAtlasArray();
			AttachmentLoader attachmentLoader2;
			if (atlasArray.Length == 0)
			{
				AttachmentLoader attachmentLoader = new RegionlessAttachmentLoader();
				attachmentLoader2 = attachmentLoader;
			}
			else
			{
				attachmentLoader2 = new AtlasAttachmentLoader(atlasArray);
			}
			AttachmentLoader attachmentLoader3 = attachmentLoader2;
			float num = this.scale;
			bool flag = this.skeletonJSON.name.ToLower().Contains(".skel");
			SkeletonData sd;
			try
			{
				if (flag)
				{
					sd = SkeletonDataAsset.ReadSkeletonData(this.skeletonJSON.bytes, attachmentLoader3, num);
				}
				else
				{
					sd = SkeletonDataAsset.ReadSkeletonData(this.skeletonJSON.text, attachmentLoader3, num);
				}
			}
			catch (Exception ex)
			{
				if (!quiet)
				{
					UnityEngine.Debug.LogError(string.Concat(new string[]
					{
						"Error reading skeleton JSON file for SkeletonData asset: ",
						base.name,
						"\n",
						ex.Message,
						"\n",
						ex.StackTrace
					}), this);
				}
				return null;
			}
			this.InitializeWithData(sd);
			return this.skeletonData;
		}

		internal void InitializeWithData(SkeletonData sd)
		{
			this.skeletonData = sd;
			this.stateData = new AnimationStateData(this.skeletonData);
			this.FillStateData();
		}

		internal Atlas[] GetAtlasArray()
		{
			List<Atlas> list = new List<Atlas>(this.atlasAssets.Length);
			for (int i = 0; i < this.atlasAssets.Length; i++)
			{
				AtlasAsset atlasAsset = this.atlasAssets[i];
				if (!(atlasAsset == null))
				{
					Atlas atlas = atlasAsset.GetAtlas();
					if (atlas != null)
					{
						list.Add(atlas);
					}
				}
			}
			return list.ToArray();
		}

		internal static SkeletonData ReadSkeletonData(byte[] bytes, AttachmentLoader attachmentLoader, float scale)
		{
			MemoryStream input = new MemoryStream(bytes);
			SkeletonBinary skeletonBinary = new SkeletonBinary(attachmentLoader)
			{
				Scale = scale
			};
			return skeletonBinary.ReadSkeletonData(input);
		}

		internal static SkeletonData ReadSkeletonData(string text, AttachmentLoader attachmentLoader, float scale)
		{
			StringReader reader = new StringReader(text);
			SkeletonJson skeletonJson = new SkeletonJson(attachmentLoader)
			{
				Scale = scale
			};
			return skeletonJson.ReadSkeletonData(reader);
		}

		public void FillStateData()
		{
			if (this.stateData != null)
			{
				this.stateData.defaultMix = this.defaultMix;
				int i = 0;
				int num = this.fromAnimation.Length;
				while (i < num)
				{
					if (this.fromAnimation[i].Length != 0 && this.toAnimation[i].Length != 0)
					{
						this.stateData.SetMix(this.fromAnimation[i], this.toAnimation[i], this.duration[i]);
					}
					i++;
				}
			}
		}

		public AnimationStateData GetAnimationStateData()
		{
			if (this.stateData != null)
			{
				return this.stateData;
			}
			this.GetSkeletonData(false);
			return this.stateData;
		}

		public AtlasAsset[] atlasAssets = new AtlasAsset[0];

		public float scale = 0.01f;

		public TextAsset skeletonJSON;

		[SpineAnimation("", "", false, false)]
		public string[] fromAnimation = new string[0];

		[SpineAnimation("", "", false, false)]
		public string[] toAnimation = new string[0];

		public float[] duration = new float[0];

		public float defaultMix;

		public RuntimeAnimatorController controller;

		private SkeletonData skeletonData;

		private AnimationStateData stateData;
	}
}
