using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_RenderTextureDescriptor : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(RenderTextureDescriptor);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			RenderTextureDescriptor renderTextureDescriptor = (RenderTextureDescriptor)value;
			writer.WriteProperty<int>("width", renderTextureDescriptor.width);
			writer.WriteProperty<int>("height", renderTextureDescriptor.height);
			writer.WriteProperty<int>("msaaSamples", renderTextureDescriptor.msaaSamples);
			writer.WriteProperty<int>("volumeDepth", renderTextureDescriptor.volumeDepth);
			writer.WriteProperty<RenderTextureFormat>("colorFormat", renderTextureDescriptor.colorFormat);
			writer.WriteProperty<int>("depthBufferBits", renderTextureDescriptor.depthBufferBits);
			writer.WriteProperty<TextureDimension>("dimension", renderTextureDescriptor.dimension);
			writer.WriteProperty<ShadowSamplingMode>("shadowSamplingMode", renderTextureDescriptor.shadowSamplingMode);
			writer.WriteProperty<VRTextureUsage>("vrUsage", renderTextureDescriptor.vrUsage);
			writer.WriteProperty<RenderTextureMemoryless>("memoryless", renderTextureDescriptor.memoryless);
			writer.WriteProperty<bool>("sRGB", renderTextureDescriptor.sRGB);
			writer.WriteProperty<bool>("useMipMap", renderTextureDescriptor.useMipMap);
			writer.WriteProperty<bool>("autoGenerateMips", renderTextureDescriptor.autoGenerateMips);
			writer.WriteProperty<bool>("enableRandomWrite", renderTextureDescriptor.enableRandomWrite);
		}

		public override object Read(ISaveGameReader reader)
		{
			RenderTextureDescriptor renderTextureDescriptor = default(RenderTextureDescriptor);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "width":
					renderTextureDescriptor.width = reader.ReadProperty<int>();
					break;
				case "height":
					renderTextureDescriptor.height = reader.ReadProperty<int>();
					break;
				case "msaaSamples":
					renderTextureDescriptor.msaaSamples = reader.ReadProperty<int>();
					break;
				case "volumeDepth":
					renderTextureDescriptor.volumeDepth = reader.ReadProperty<int>();
					break;
				case "colorFormat":
					renderTextureDescriptor.colorFormat = reader.ReadProperty<RenderTextureFormat>();
					break;
				case "depthBufferBits":
					renderTextureDescriptor.depthBufferBits = reader.ReadProperty<int>();
					break;
				case "dimension":
					renderTextureDescriptor.dimension = reader.ReadProperty<TextureDimension>();
					break;
				case "shadowSamplingMode":
					renderTextureDescriptor.shadowSamplingMode = reader.ReadProperty<ShadowSamplingMode>();
					break;
				case "vrUsage":
					renderTextureDescriptor.vrUsage = reader.ReadProperty<VRTextureUsage>();
					break;
				case "memoryless":
					renderTextureDescriptor.memoryless = reader.ReadProperty<RenderTextureMemoryless>();
					break;
				case "sRGB":
					renderTextureDescriptor.sRGB = reader.ReadProperty<bool>();
					break;
				case "useMipMap":
					renderTextureDescriptor.useMipMap = reader.ReadProperty<bool>();
					break;
				case "autoGenerateMips":
					renderTextureDescriptor.autoGenerateMips = reader.ReadProperty<bool>();
					break;
				case "enableRandomWrite":
					renderTextureDescriptor.enableRandomWrite = reader.ReadProperty<bool>();
					break;
				}
			}
			return renderTextureDescriptor;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
