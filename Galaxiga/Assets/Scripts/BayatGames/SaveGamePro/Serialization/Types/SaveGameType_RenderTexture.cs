using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_RenderTexture : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(RenderTexture);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			RenderTexture renderTexture = (RenderTexture)value;
			writer.WriteProperty<int>("width", renderTexture.width);
			writer.WriteProperty<int>("height", renderTexture.height);
			writer.WriteProperty<int>("depth", renderTexture.depth);
			writer.WriteProperty<bool>("isPowerOfTwo", renderTexture.isPowerOfTwo);
			writer.WriteProperty<RenderTextureFormat>("format", renderTexture.format);
			writer.WriteProperty<bool>("useMipMap", renderTexture.useMipMap);
			writer.WriteProperty<bool>("autoGenerateMips", renderTexture.autoGenerateMips);
			writer.WriteProperty<TextureDimension>("dimension", renderTexture.dimension);
			writer.WriteProperty<int>("volumeDepth", renderTexture.volumeDepth);
			writer.WriteProperty<RenderTextureMemoryless>("memorylessMode", renderTexture.memorylessMode);
			writer.WriteProperty<int>("antiAliasing", renderTexture.antiAliasing);
			writer.WriteProperty<bool>("enableRandomWrite", renderTexture.enableRandomWrite);
			writer.WriteProperty<RenderTextureDescriptor>("descriptor", renderTexture.descriptor);
			writer.WriteProperty<FilterMode>("filterMode", renderTexture.filterMode);
			writer.WriteProperty<int>("anisoLevel", renderTexture.anisoLevel);
			writer.WriteProperty<TextureWrapMode>("wrapMode", renderTexture.wrapMode);
			writer.WriteProperty<TextureWrapMode>("wrapModeU", renderTexture.wrapModeU);
			writer.WriteProperty<TextureWrapMode>("wrapModeV", renderTexture.wrapModeV);
			writer.WriteProperty<TextureWrapMode>("wrapModeW", renderTexture.wrapModeW);
			writer.WriteProperty<float>("mipMapBias", renderTexture.mipMapBias);
			writer.WriteProperty<string>("name", renderTexture.name);
			writer.WriteProperty<HideFlags>("hideFlags", renderTexture.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			return base.Read(reader);
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			RenderTexture renderTexture = (RenderTexture)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "width":
					renderTexture.width = reader.ReadProperty<int>();
					break;
				case "height":
					renderTexture.height = reader.ReadProperty<int>();
					break;
				case "depth":
					renderTexture.depth = reader.ReadProperty<int>();
					break;
				case "isPowerOfTwo":
					renderTexture.isPowerOfTwo = reader.ReadProperty<bool>();
					break;
				case "format":
					renderTexture.format = reader.ReadProperty<RenderTextureFormat>();
					break;
				case "useMipMap":
					renderTexture.useMipMap = reader.ReadProperty<bool>();
					break;
				case "autoGenerateMips":
					renderTexture.autoGenerateMips = reader.ReadProperty<bool>();
					break;
				case "dimension":
					renderTexture.dimension = reader.ReadProperty<TextureDimension>();
					break;
				case "volumeDepth":
					renderTexture.volumeDepth = reader.ReadProperty<int>();
					break;
				case "memorylessMode":
					renderTexture.memorylessMode = reader.ReadProperty<RenderTextureMemoryless>();
					break;
				case "antiAliasing":
					renderTexture.antiAliasing = reader.ReadProperty<int>();
					break;
				case "enableRandomWrite":
					renderTexture.enableRandomWrite = reader.ReadProperty<bool>();
					break;
				case "descriptor":
					renderTexture.descriptor = reader.ReadProperty<RenderTextureDescriptor>();
					break;
				case "filterMode":
					renderTexture.filterMode = reader.ReadProperty<FilterMode>();
					break;
				case "anisoLevel":
					renderTexture.anisoLevel = reader.ReadProperty<int>();
					break;
				case "wrapMode":
					renderTexture.wrapMode = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeU":
					renderTexture.wrapModeU = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeV":
					renderTexture.wrapModeV = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeW":
					renderTexture.wrapModeW = reader.ReadProperty<TextureWrapMode>();
					break;
				case "mipMapBias":
					renderTexture.mipMapBias = reader.ReadProperty<float>();
					break;
				case "name":
					renderTexture.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					renderTexture.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
