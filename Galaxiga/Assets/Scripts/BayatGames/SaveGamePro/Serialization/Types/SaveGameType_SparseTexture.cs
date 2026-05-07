using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SparseTexture : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SparseTexture);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SparseTexture sparseTexture = (SparseTexture)value;
			writer.WriteProperty<int>("width", sparseTexture.width);
			writer.WriteProperty<int>("height", sparseTexture.height);
			writer.WriteProperty<TextureDimension>("dimension", sparseTexture.dimension);
			writer.WriteProperty<FilterMode>("filterMode", sparseTexture.filterMode);
			writer.WriteProperty<int>("anisoLevel", sparseTexture.anisoLevel);
			writer.WriteProperty<TextureWrapMode>("wrapMode", sparseTexture.wrapMode);
			writer.WriteProperty<TextureWrapMode>("wrapModeU", sparseTexture.wrapModeU);
			writer.WriteProperty<TextureWrapMode>("wrapModeV", sparseTexture.wrapModeV);
			writer.WriteProperty<TextureWrapMode>("wrapModeW", sparseTexture.wrapModeW);
			writer.WriteProperty<float>("mipMapBias", sparseTexture.mipMapBias);
			writer.WriteProperty<string>("name", sparseTexture.name);
			writer.WriteProperty<HideFlags>("hideFlags", sparseTexture.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			return base.Read(reader);
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			SparseTexture sparseTexture = (SparseTexture)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "width":
					sparseTexture.width = reader.ReadProperty<int>();
					break;
				case "height":
					sparseTexture.height = reader.ReadProperty<int>();
					break;
				case "dimension":
					sparseTexture.dimension = reader.ReadProperty<TextureDimension>();
					break;
				case "filterMode":
					sparseTexture.filterMode = reader.ReadProperty<FilterMode>();
					break;
				case "anisoLevel":
					sparseTexture.anisoLevel = reader.ReadProperty<int>();
					break;
				case "wrapMode":
					sparseTexture.wrapMode = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeU":
					sparseTexture.wrapModeU = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeV":
					sparseTexture.wrapModeV = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeW":
					sparseTexture.wrapModeW = reader.ReadProperty<TextureWrapMode>();
					break;
				case "mipMapBias":
					sparseTexture.mipMapBias = reader.ReadProperty<float>();
					break;
				case "name":
					sparseTexture.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					sparseTexture.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
