using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Image : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Image);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Image image = (Image)value;
			writer.WriteProperty<Sprite>("sprite", image.sprite);
			writer.WriteProperty<Sprite>("overrideSprite", image.overrideSprite);
			writer.WriteProperty<Image.Type>("type", image.type);
			writer.WriteProperty<bool>("preserveAspect", image.preserveAspect);
			writer.WriteProperty<bool>("fillCenter", image.fillCenter);
			writer.WriteProperty<Image.FillMethod>("fillMethod", image.fillMethod);
			writer.WriteProperty<float>("fillAmount", image.fillAmount);
			writer.WriteProperty<bool>("fillClockwise", image.fillClockwise);
			writer.WriteProperty<int>("fillOrigin", image.fillOrigin);
			writer.WriteProperty<float>("alphaHitTestMinimumThreshold", image.alphaHitTestMinimumThreshold);
			writer.WriteProperty<Material>("material", image.material);
			writer.WriteProperty<bool>("maskable", image.maskable);
			writer.WriteProperty<Color>("color", image.color);
			writer.WriteProperty<bool>("raycastTarget", image.raycastTarget);
			writer.WriteProperty<bool>("useGUILayout", image.useGUILayout);
			writer.WriteProperty<bool>("enabled", image.enabled);
			writer.WriteProperty<string>("tag", image.tag);
			writer.WriteProperty<string>("name", image.name);
			writer.WriteProperty<HideFlags>("hideFlags", image.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Image image = SaveGameType.CreateComponent<Image>();
			this.ReadInto(image, reader);
			return image;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Image image = (Image)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "sprite":
					if (image.sprite == null)
					{
						image.sprite = reader.ReadProperty<Sprite>();
					}
					else
					{
						reader.ReadIntoProperty<Sprite>(image.sprite);
					}
					break;
				case "overrideSprite":
					if (image.overrideSprite == null)
					{
						image.overrideSprite = reader.ReadProperty<Sprite>();
					}
					else
					{
						reader.ReadIntoProperty<Sprite>(image.overrideSprite);
					}
					break;
				case "type":
					image.type = reader.ReadProperty<Image.Type>();
					break;
				case "preserveAspect":
					image.preserveAspect = reader.ReadProperty<bool>();
					break;
				case "fillCenter":
					image.fillCenter = reader.ReadProperty<bool>();
					break;
				case "fillMethod":
					image.fillMethod = reader.ReadProperty<Image.FillMethod>();
					break;
				case "fillAmount":
					image.fillAmount = reader.ReadProperty<float>();
					break;
				case "fillClockwise":
					image.fillClockwise = reader.ReadProperty<bool>();
					break;
				case "fillOrigin":
					image.fillOrigin = reader.ReadProperty<int>();
					break;
				case "alphaHitTestMinimumThreshold":
					image.alphaHitTestMinimumThreshold = reader.ReadProperty<float>();
					break;
				case "material":
					if (image.material == null)
					{
						image.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(image.material);
					}
					break;
				case "maskable":
					image.maskable = reader.ReadProperty<bool>();
					break;
				case "color":
					image.color = reader.ReadProperty<Color>();
					break;
				case "raycastTarget":
					image.raycastTarget = reader.ReadProperty<bool>();
					break;
				case "useGUILayout":
					image.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					image.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					image.tag = reader.ReadProperty<string>();
					break;
				case "name":
					image.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					image.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
