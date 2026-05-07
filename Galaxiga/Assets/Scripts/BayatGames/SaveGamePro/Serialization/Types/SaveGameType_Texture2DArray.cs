using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Texture2DArray : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Texture2DArray);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Texture2DArray texture2DArray = (Texture2DArray)value;
			writer.WriteProperty<int>("width", texture2DArray.width);
			writer.WriteProperty<int>("height", texture2DArray.height);
			writer.WriteProperty<int>("depth", texture2DArray.depth);
			writer.WriteProperty<TextureDimension>("dimension", texture2DArray.dimension);
			writer.WriteProperty<FilterMode>("filterMode", texture2DArray.filterMode);
			writer.WriteProperty<int>("anisoLevel", texture2DArray.anisoLevel);
			writer.WriteProperty<TextureWrapMode>("wrapMode", texture2DArray.wrapMode);
			writer.WriteProperty<TextureWrapMode>("wrapModeU", texture2DArray.wrapModeU);
			writer.WriteProperty<TextureWrapMode>("wrapModeV", texture2DArray.wrapModeV);
			writer.WriteProperty<TextureWrapMode>("wrapModeW", texture2DArray.wrapModeW);
			writer.WriteProperty<float>("mipMapBias", texture2DArray.mipMapBias);
			writer.WriteProperty<string>("name", texture2DArray.name);
			writer.WriteProperty<HideFlags>("hideFlags", texture2DArray.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Texture2DArray texture2DArray = new Texture2DArray(reader.ReadProperty<int>(), reader.ReadProperty<int>(), reader.ReadProperty<int>(), TextureFormat.ARGB32, true);
			this.ReadInto(texture2DArray, reader);
			return texture2DArray;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Texture2DArray texture2DArray = (Texture2DArray)value;
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
					texture2DArray.filterMode = reader.ReadProperty<FilterMode>();
					break;
				case "anisoLevel":
					texture2DArray.anisoLevel = reader.ReadProperty<int>();
					break;
				case "wrapMode":
					texture2DArray.wrapMode = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeU":
					texture2DArray.wrapModeU = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeV":
					texture2DArray.wrapModeV = reader.ReadProperty<TextureWrapMode>();
					break;
				case "wrapModeW":
					texture2DArray.wrapModeW = reader.ReadProperty<TextureWrapMode>();
					break;
				case "mipMapBias":
					texture2DArray.mipMapBias = reader.ReadProperty<float>();
					break;
				case "name":
					texture2DArray.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					texture2DArray.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
