using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Slider : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Slider);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Slider slider = (Slider)value;
			writer.WriteProperty<RectTransform>("fillRect", slider.fillRect);
			writer.WriteProperty<RectTransform>("handleRect", slider.handleRect);
			writer.WriteProperty<Slider.Direction>("direction", slider.direction);
			writer.WriteProperty<float>("minValue", slider.minValue);
			writer.WriteProperty<float>("maxValue", slider.maxValue);
			writer.WriteProperty<bool>("wholeNumbers", slider.wholeNumbers);
			writer.WriteProperty<float>("value", slider.value);
			writer.WriteProperty<float>("normalizedValue", slider.normalizedValue);
			writer.WriteProperty<Navigation>("navigation", slider.navigation);
			writer.WriteProperty<Selectable.Transition>("transition", slider.transition);
			writer.WriteProperty<ColorBlock>("colors", slider.colors);
			writer.WriteProperty<SpriteState>("spriteState", slider.spriteState);
			writer.WriteProperty<AnimationTriggers>("animationTriggers", slider.animationTriggers);
			writer.WriteProperty<string>("targetGraphicType", slider.targetGraphic.GetType().AssemblyQualifiedName);
			writer.WriteProperty<Graphic>("targetGraphic", slider.targetGraphic);
			writer.WriteProperty<bool>("interactable", slider.interactable);
			writer.WriteProperty<Image>("image", slider.image);
			writer.WriteProperty<bool>("useGUILayout", slider.useGUILayout);
			writer.WriteProperty<bool>("enabled", slider.enabled);
			writer.WriteProperty<string>("tag", slider.tag);
			writer.WriteProperty<string>("name", slider.name);
			writer.WriteProperty<HideFlags>("hideFlags", slider.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Slider slider = SaveGameType.CreateComponent<Slider>();
			this.ReadInto(slider, reader);
			return slider;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Slider slider = (Slider)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "fillRect":
					if (slider.fillRect == null)
					{
						slider.fillRect = reader.ReadProperty<RectTransform>();
					}
					else
					{
						reader.ReadIntoProperty<RectTransform>(slider.fillRect);
					}
					break;
				case "handleRect":
					if (slider.handleRect == null)
					{
						slider.handleRect = reader.ReadProperty<RectTransform>();
					}
					else
					{
						reader.ReadIntoProperty<RectTransform>(slider.handleRect);
					}
					break;
				case "direction":
					slider.direction = reader.ReadProperty<Slider.Direction>();
					break;
				case "minValue":
					slider.minValue = reader.ReadProperty<float>();
					break;
				case "maxValue":
					slider.maxValue = reader.ReadProperty<float>();
					break;
				case "wholeNumbers":
					slider.wholeNumbers = reader.ReadProperty<bool>();
					break;
				case "value":
					slider.value = reader.ReadProperty<float>();
					break;
				case "normalizedValue":
					slider.normalizedValue = reader.ReadProperty<float>();
					break;
				case "navigation":
					slider.navigation = reader.ReadProperty<Navigation>();
					break;
				case "transition":
					slider.transition = reader.ReadProperty<Selectable.Transition>();
					break;
				case "colors":
					slider.colors = reader.ReadProperty<ColorBlock>();
					break;
				case "spriteState":
					slider.spriteState = reader.ReadProperty<SpriteState>();
					break;
				case "animationTriggers":
					slider.animationTriggers = reader.ReadProperty<AnimationTriggers>();
					break;
				case "targetGraphic":
				{
					Type type = Type.GetType(reader.ReadProperty<string>());
					if (slider.targetGraphic == null)
					{
						slider.targetGraphic = (Graphic)reader.ReadProperty(type);
					}
					else
					{
						reader.ReadIntoProperty<Graphic>(slider.targetGraphic);
					}
					break;
				}
				case "interactable":
					slider.interactable = reader.ReadProperty<bool>();
					break;
				case "image":
					if (slider.image == null)
					{
						slider.image = reader.ReadProperty<Image>();
					}
					else
					{
						reader.ReadIntoProperty<Image>(slider.image);
					}
					break;
				case "useGUILayout":
					slider.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					slider.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					slider.tag = reader.ReadProperty<string>();
					break;
				case "name":
					slider.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					slider.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
