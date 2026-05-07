using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Dropdown : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Dropdown);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Dropdown dropdown = (Dropdown)value;
			writer.WriteProperty<RectTransform>("template", dropdown.template);
			writer.WriteProperty<Text>("captionText", dropdown.captionText);
			writer.WriteProperty<Image>("captionImage", dropdown.captionImage);
			writer.WriteProperty<Text>("itemText", dropdown.itemText);
			writer.WriteProperty<Image>("itemImage", dropdown.itemImage);
			writer.WriteProperty<List<Dropdown.OptionData>>("options", dropdown.options);
			writer.WriteProperty<int>("value", dropdown.value);
			writer.WriteProperty<Navigation>("navigation", dropdown.navigation);
			writer.WriteProperty<Selectable.Transition>("transition", dropdown.transition);
			writer.WriteProperty<ColorBlock>("colors", dropdown.colors);
			writer.WriteProperty<SpriteState>("spriteState", dropdown.spriteState);
			writer.WriteProperty<AnimationTriggers>("animationTriggers", dropdown.animationTriggers);
			writer.WriteProperty<string>("targetGraphicType", dropdown.targetGraphic.GetType().AssemblyQualifiedName);
			writer.WriteProperty<Graphic>("targetGraphic", dropdown.targetGraphic);
			writer.WriteProperty<bool>("interactable", dropdown.interactable);
			writer.WriteProperty<Image>("image", dropdown.image);
			writer.WriteProperty<bool>("useGUILayout", dropdown.useGUILayout);
			writer.WriteProperty<bool>("enabled", dropdown.enabled);
			writer.WriteProperty<string>("tag", dropdown.tag);
			writer.WriteProperty<string>("name", dropdown.name);
			writer.WriteProperty<HideFlags>("hideFlags", dropdown.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Dropdown dropdown = SaveGameType.CreateComponent<Dropdown>();
			this.ReadInto(dropdown, reader);
			return dropdown;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Dropdown dropdown = (Dropdown)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "template":
					if (dropdown.template == null)
					{
						dropdown.template = reader.ReadProperty<RectTransform>();
					}
					else
					{
						reader.ReadIntoProperty<RectTransform>(dropdown.template);
					}
					break;
				case "captionText":
					if (dropdown.captionText == null)
					{
						dropdown.captionText = reader.ReadProperty<Text>();
					}
					else
					{
						reader.ReadIntoProperty<Text>(dropdown.captionText);
					}
					break;
				case "captionImage":
					if (dropdown.captionImage == null)
					{
						dropdown.captionImage = reader.ReadProperty<Image>();
					}
					else
					{
						reader.ReadIntoProperty<Image>(dropdown.captionImage);
					}
					break;
				case "itemText":
					if (dropdown.itemText == null)
					{
						dropdown.itemText = reader.ReadProperty<Text>();
					}
					else
					{
						reader.ReadIntoProperty<Text>(dropdown.itemText);
					}
					break;
				case "itemImage":
					if (dropdown.itemImage == null)
					{
						dropdown.itemImage = reader.ReadProperty<Image>();
					}
					else
					{
						reader.ReadIntoProperty<Image>(dropdown.itemImage);
					}
					break;
				case "options":
					dropdown.options = reader.ReadProperty<List<Dropdown.OptionData>>();
					break;
				case "value":
					dropdown.value = reader.ReadProperty<int>();
					break;
				case "navigation":
					dropdown.navigation = reader.ReadProperty<Navigation>();
					break;
				case "transition":
					dropdown.transition = reader.ReadProperty<Selectable.Transition>();
					break;
				case "colors":
					dropdown.colors = reader.ReadProperty<ColorBlock>();
					break;
				case "spriteState":
					dropdown.spriteState = reader.ReadProperty<SpriteState>();
					break;
				case "animationTriggers":
					dropdown.animationTriggers = reader.ReadProperty<AnimationTriggers>();
					break;
				case "targetGraphic":
				{
					Type type = Type.GetType(reader.ReadProperty<string>());
					if (dropdown.targetGraphic == null)
					{
						dropdown.targetGraphic = (Graphic)reader.ReadProperty(type);
					}
					else
					{
						reader.ReadIntoProperty<Graphic>(dropdown.targetGraphic);
					}
					break;
				}
				case "interactable":
					dropdown.interactable = reader.ReadProperty<bool>();
					break;
				case "image":
					if (dropdown.image == null)
					{
						dropdown.image = reader.ReadProperty<Image>();
					}
					else
					{
						reader.ReadIntoProperty<Image>(dropdown.image);
					}
					break;
				case "useGUILayout":
					dropdown.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					dropdown.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					dropdown.tag = reader.ReadProperty<string>();
					break;
				case "name":
					dropdown.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					dropdown.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
