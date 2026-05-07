using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Texture : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Texture);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Texture texture = (Texture)value;
			writer.WriteProperty<int>("width", texture.width);
			writer.WriteProperty<int>("height", texture.height);
			writer.WriteProperty<TextureDimension>("dimension", texture.dimension);
			writer.WriteProperty<FilterMode>("filterMode", texture.filterMode);
			writer.WriteProperty<int>("anisoLevel", texture.anisoLevel);
			writer.WriteProperty<TextureWrapMode>("wrapMode", texture.wrapMode);
			writer.WriteProperty<TextureWrapMode>("wrapModeU", texture.wrapModeU);
			writer.WriteProperty<TextureWrapMode>("wrapModeV", texture.wrapModeV);
			writer.WriteProperty<TextureWrapMode>("wrapModeW", texture.wrapModeW);
			writer.WriteProperty<float>("mipMapBias", texture.mipMapBias);
			writer.WriteProperty<string>("name", texture.name);
			writer.WriteProperty<HideFlags>("hideFlags", texture.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Texture texture = new Texture2D(0, 0);
			this.ReadInto(texture, reader);
			return texture;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Texture texture = (Texture)value;
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
				case "dimension":
					reader.ReadProperty<TextureDimension>();
					break;
				case "filterMode":
					texture.filterMode = reader.ReadProperty<FilterMode>();
					break;
				case "anisoLevel":
					texture.anisoLevel = reader.ReadProperty<int>();
					break;
				case "wrapMode":
					texture.wrapMode = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeU":
					texture.wrapModeU = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeV":
					texture.wrapModeV = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeW":
					texture.wrapModeW = reader.ReadProperty<TextureWrapMode>();
					break;
				case "mipMapBias":
					texture.mipMapBias = reader.ReadProperty<float>();
					break;
				case "name":
					texture.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					texture.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
