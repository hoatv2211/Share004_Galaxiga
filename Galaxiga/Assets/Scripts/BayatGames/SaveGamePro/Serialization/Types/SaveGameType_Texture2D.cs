using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Texture2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Texture2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Texture2D texture2D = (Texture2D)value;
			writer.WriteProperty<int>("width", texture2D.width);
			writer.WriteProperty<int>("height", texture2D.height);
			writer.WriteProperty<byte[]>("rawTextureData", texture2D.EncodeToPNG());
			writer.WriteProperty<TextureDimension>("dimension", texture2D.dimension);
			writer.WriteProperty<FilterMode>("filterMode", texture2D.filterMode);
			writer.WriteProperty<int>("anisoLevel", texture2D.anisoLevel);
			writer.WriteProperty<TextureWrapMode>("wrapMode", texture2D.wrapMode);
			writer.WriteProperty<TextureWrapMode>("wrapModeU", texture2D.wrapModeU);
			writer.WriteProperty<TextureWrapMode>("wrapModeV", texture2D.wrapModeV);
			writer.WriteProperty<TextureWrapMode>("wrapModeW", texture2D.wrapModeW);
			writer.WriteProperty<float>("mipMapBias", texture2D.mipMapBias);
			writer.WriteProperty<string>("name", texture2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", texture2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Texture2D texture2D = new Texture2D(0, 0);
			this.ReadInto(texture2D, reader);
			return texture2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Texture2D texture2D = (Texture2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "width":
					reader.ReadProperty<int>();
					break;
				case "height":
					reader.ReadProperty<int>();
					break;
				case "rawTextureData":
					texture2D.LoadImage(reader.ReadProperty<byte[]>());
					texture2D.Apply();
					break;
				case "dimension":
					reader.ReadProperty<TextureDimension>();
					break;
				case "filterMode":
					texture2D.filterMode = reader.ReadProperty<FilterMode>();
					break;
				case "anisoLevel":
					texture2D.anisoLevel = reader.ReadProperty<int>();
					break;
				case "wrapMode":
					texture2D.wrapMode = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeU":
					texture2D.wrapModeU = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeV":
					texture2D.wrapModeV = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeW":
					texture2D.wrapModeW = reader.ReadProperty<TextureWrapMode>();
					break;
				case "mipMapBias":
					texture2D.mipMapBias = reader.ReadProperty<float>();
					break;
				case "name":
					texture2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					texture2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
