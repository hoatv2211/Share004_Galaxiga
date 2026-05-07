using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_RawImage : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(RawImage);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			RawImage rawImage = (RawImage)value;
			writer.WriteProperty<Texture>("texture", rawImage.texture);
			writer.WriteProperty<Rect>("uvRect", rawImage.uvRect);
			writer.WriteProperty<bool>("maskable", rawImage.maskable);
			writer.WriteProperty<Color>("color", rawImage.color);
			writer.WriteProperty<bool>("raycastTarget", rawImage.raycastTarget);
			writer.WriteProperty<Material>("material", rawImage.material);
			writer.WriteProperty<bool>("useGUILayout", rawImage.useGUILayout);
			writer.WriteProperty<bool>("enabled", rawImage.enabled);
			writer.WriteProperty<string>("tag", rawImage.tag);
			writer.WriteProperty<string>("name", rawImage.name);
			writer.WriteProperty<HideFlags>("hideFlags", rawImage.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			RawImage rawImage = SaveGameType.CreateComponent<RawImage>();
			this.ReadInto(rawImage, reader);
			return rawImage;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			RawImage rawImage = (RawImage)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "texture":
					if (rawImage.texture == null)
					{
						rawImage.texture = reader.ReadProperty<Texture>();
					}
					else
					{
						reader.ReadIntoProperty<Texture>(rawImage.texture);
					}
					break;
				case "uvRect":
					rawImage.uvRect = reader.ReadProperty<Rect>();
					break;
				case "maskable":
					rawImage.maskable = reader.ReadProperty<bool>();
					break;
				case "color":
					rawImage.color = reader.ReadProperty<Color>();
					break;
				case "raycastTarget":
					rawImage.raycastTarget = reader.ReadProperty<bool>();
					break;
				case "material":
					if (rawImage.material == null)
					{
						rawImage.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(rawImage.material);
					}
					break;
				case "useGUILayout":
					rawImage.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					rawImage.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					rawImage.tag = reader.ReadProperty<string>();
					break;
				case "name":
					rawImage.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					rawImage.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
