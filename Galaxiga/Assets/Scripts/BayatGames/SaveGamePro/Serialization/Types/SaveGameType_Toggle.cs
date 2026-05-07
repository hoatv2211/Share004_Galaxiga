using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Toggle : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Toggle);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Toggle toggle = (Toggle)value;
			writer.WriteProperty<Toggle.ToggleTransition>("toggleTransition", toggle.toggleTransition);
			writer.WriteProperty<string>("graphicType", toggle.graphic.GetType().AssemblyQualifiedName);
			writer.WriteProperty<Graphic>("graphic", toggle.graphic);
			writer.WriteProperty<ToggleGroup>("group", toggle.group);
			writer.WriteProperty<bool>("isOn", toggle.isOn);
			writer.WriteProperty<Navigation>("navigation", toggle.navigation);
			writer.WriteProperty<Selectable.Transition>("transition", toggle.transition);
			writer.WriteProperty<ColorBlock>("colors", toggle.colors);
			writer.WriteProperty<SpriteState>("spriteState", toggle.spriteState);
			writer.WriteProperty<AnimationTriggers>("animationTriggers", toggle.animationTriggers);
			writer.WriteProperty<string>("targetGraphicType", toggle.targetGraphic.GetType().AssemblyQualifiedName);
			writer.WriteProperty<Graphic>("targetGraphic", toggle.targetGraphic);
			writer.WriteProperty<bool>("interactable", toggle.interactable);
			writer.WriteProperty<Image>("image", toggle.image);
			writer.WriteProperty<bool>("useGUILayout", toggle.useGUILayout);
			writer.WriteProperty<bool>("enabled", toggle.enabled);
			writer.WriteProperty<string>("tag", toggle.tag);
			writer.WriteProperty<string>("name", toggle.name);
			writer.WriteProperty<HideFlags>("hideFlags", toggle.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Toggle toggle = SaveGameType.CreateComponent<Toggle>();
			this.ReadInto(toggle, reader);
			return toggle;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Toggle toggle = (Toggle)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "toggleTransition":
					toggle.toggleTransition = reader.ReadProperty<Toggle.ToggleTransition>();
					break;
				case "graphic":
				{
					Type type = Type.GetType(reader.ReadProperty<string>());
					if (toggle.graphic == null)
					{
						toggle.graphic = (Graphic)reader.ReadProperty(type);
					}
					else
					{
						reader.ReadIntoProperty<Graphic>(toggle.graphic);
					}
					break;
				}
				case "group":
					if (toggle.group == null)
					{
						toggle.group = reader.ReadProperty<ToggleGroup>();
					}
					else
					{
						reader.ReadIntoProperty<ToggleGroup>(toggle.group);
					}
					break;
				case "isOn":
					toggle.isOn = reader.ReadProperty<bool>();
					break;
				case "navigation":
					toggle.navigation = reader.ReadProperty<Navigation>();
					break;
				case "transition":
					toggle.transition = reader.ReadProperty<Selectable.Transition>();
					break;
				case "colors":
					toggle.colors = reader.ReadProperty<ColorBlock>();
					break;
				case "spriteState":
					toggle.spriteState = reader.ReadProperty<SpriteState>();
					break;
				case "animationTriggers":
					toggle.animationTriggers = reader.ReadProperty<AnimationTriggers>();
					break;
				case "targetGraphic":
				{
					Type type2 = Type.GetType(reader.ReadProperty<string>());
					if (toggle.targetGraphic == null)
					{
						toggle.targetGraphic = (Graphic)reader.ReadProperty(type2);
					}
					else
					{
						reader.ReadIntoProperty<Graphic>(toggle.targetGraphic);
					}
					break;
				}
				case "interactable":
					toggle.interactable = reader.ReadProperty<bool>();
					break;
				case "image":
					if (toggle.image == null)
					{
						toggle.image = reader.ReadProperty<Image>();
					}
					else
					{
						reader.ReadIntoProperty<Image>(toggle.image);
					}
					break;
				case "useGUILayout":
					toggle.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					toggle.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					toggle.tag = reader.ReadProperty<string>();
					break;
				case "name":
					toggle.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					toggle.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
