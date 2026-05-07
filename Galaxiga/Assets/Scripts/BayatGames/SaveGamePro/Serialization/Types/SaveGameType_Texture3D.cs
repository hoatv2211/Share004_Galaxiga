using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Texture3D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Texture3D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Texture3D texture3D = (Texture3D)value;
			writer.WriteProperty<int>("width", texture3D.width);
			writer.WriteProperty<int>("height", texture3D.height);
			writer.WriteProperty<int>("depth", texture3D.depth);
			writer.WriteProperty<TextureDimension>("dimension", texture3D.dimension);
			writer.WriteProperty<FilterMode>("filterMode", texture3D.filterMode);
			writer.WriteProperty<int>("anisoLevel", texture3D.anisoLevel);
			writer.WriteProperty<TextureWrapMode>("wrapMode", texture3D.wrapMode);
			writer.WriteProperty<TextureWrapMode>("wrapModeU", texture3D.wrapModeU);
			writer.WriteProperty<TextureWrapMode>("wrapModeV", texture3D.wrapModeV);
			writer.WriteProperty<TextureWrapMode>("wrapModeW", texture3D.wrapModeW);
			writer.WriteProperty<float>("mipMapBias", texture3D.mipMapBias);
			writer.WriteProperty<string>("name", texture3D.name);
			writer.WriteProperty<HideFlags>("hideFlags", texture3D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Texture3D texture3D = new Texture3D(reader.ReadProperty<int>(), reader.ReadProperty<int>(), reader.ReadProperty<int>(), TextureFormat.ARGB32, true);
			this.ReadInto(texture3D, reader);
			return texture3D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Texture3D texture3D = (Texture3D)value;
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
				case "depth":
					reader.ReadProperty<int>();
					break;
				case "dimension":
					reader.ReadProperty<TextureDimension>();
					break;
				case "filterMode":
					texture3D.filterMode = reader.ReadProperty<FilterMode>();
					break;
				case "anisoLevel":
					texture3D.anisoLevel = reader.ReadProperty<int>();
					break;
				case "wrapMode":
					texture3D.wrapMode = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeU":
					texture3D.wrapModeU = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeV":
					texture3D.wrapModeV = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeW":
					texture3D.wrapModeW = reader.ReadProperty<TextureWrapMode>();
					break;
				case "mipMapBias":
					texture3D.mipMapBias = reader.ReadProperty<float>();
					break;
				case "name":
					texture3D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					texture3D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
