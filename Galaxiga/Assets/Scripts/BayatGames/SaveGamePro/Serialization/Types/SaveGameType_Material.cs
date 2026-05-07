using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Material : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Material);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Material material = (Material)value;
			writer.WriteProperty<Shader>("shader", material.shader);
			writer.WriteProperty<Color>("color", material.color);
			writer.WriteProperty<Texture>("mainTexture", material.mainTexture);
			writer.WriteProperty<Vector2>("mainTextureOffset", material.mainTextureOffset);
			writer.WriteProperty<Vector2>("mainTextureScale", material.mainTextureScale);
			writer.WriteProperty<int>("renderQueue", material.renderQueue);
			writer.WriteProperty<string[]>("shaderKeywords", material.shaderKeywords);
			writer.WriteProperty<MaterialGlobalIlluminationFlags>("globalIlluminationFlags", material.globalIlluminationFlags);
			writer.WriteProperty<bool>("enableInstancing", material.enableInstancing);
			writer.WriteProperty<bool>("doubleSidedGI", material.doubleSidedGI);
			writer.WriteProperty<string>("name", material.name);
			writer.WriteProperty<HideFlags>("hideFlags", material.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Material material = new Material(reader.ReadProperty<Shader>());
			this.ReadInto(material, reader);
			return material;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Material material = (Material)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "shader":
					if (material.shader == null)
					{
						material.shader = reader.ReadProperty<Shader>();
					}
					else
					{
						reader.ReadIntoProperty<Shader>(material.shader);
					}
					break;
				case "color":
					material.color = reader.ReadProperty<Color>();
					break;
				case "mainTexture":
					if (material.mainTexture == null)
					{
						material.mainTexture = reader.ReadProperty<Texture2D>();
					}
					else
					{
						reader.ReadIntoProperty<Texture2D>(material.mainTexture as Texture2D);
					}
					break;
				case "mainTextureOffset":
					material.mainTextureOffset = reader.ReadProperty<Vector2>();
					break;
				case "mainTextureScale":
					material.mainTextureScale = reader.ReadProperty<Vector2>();
					break;
				case "renderQueue":
					material.renderQueue = reader.ReadProperty<int>();
					break;
				case "shaderKeywords":
					material.shaderKeywords = reader.ReadProperty<string[]>();
					break;
				case "globalIlluminationFlags":
					material.globalIlluminationFlags = reader.ReadProperty<MaterialGlobalIlluminationFlags>();
					break;
				case "enableInstancing":
					material.enableInstancing = reader.ReadProperty<bool>();
					break;
				case "doubleSidedGI":
					material.doubleSidedGI = reader.ReadProperty<bool>();
					break;
				case "name":
					material.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					material.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
