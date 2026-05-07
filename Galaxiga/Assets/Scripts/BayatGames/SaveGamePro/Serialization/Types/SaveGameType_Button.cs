using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Button : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Button);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Button button = (Button)value;
			writer.WriteProperty<Navigation>("navigation", button.navigation);
			writer.WriteProperty<Selectable.Transition>("transition", button.transition);
			writer.WriteProperty<ColorBlock>("colors", button.colors);
			writer.WriteProperty<SpriteState>("spriteState", button.spriteState);
			writer.WriteProperty<AnimationTriggers>("animationTriggers", button.animationTriggers);
			writer.WriteProperty<string>("targetGraphicType", button.targetGraphic.GetType().AssemblyQualifiedName);
			writer.WriteProperty<Graphic>("targetGraphic", button.targetGraphic);
			writer.WriteProperty<bool>("interactable", button.interactable);
			writer.WriteProperty<Image>("image", button.image);
			writer.WriteProperty<bool>("useGUILayout", button.useGUILayout);
			writer.WriteProperty<bool>("enabled", button.enabled);
			writer.WriteProperty<string>("tag", button.tag);
			writer.WriteProperty<string>("name", button.name);
			writer.WriteProperty<HideFlags>("hideFlags", button.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Button button = SaveGameType.CreateComponent<Button>();
			this.ReadInto(button, reader);
			return button;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Button button = (Button)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "navigation":
					button.navigation = reader.ReadProperty<Navigation>();
					break;
				case "transition":
					button.transition = reader.ReadProperty<Selectable.Transition>();
					break;
				case "colors":
					button.colors = reader.ReadProperty<ColorBlock>();
					break;
				case "spriteState":
					button.spriteState = reader.ReadProperty<SpriteState>();
					break;
				case "animationTriggers":
					button.animationTriggers = reader.ReadProperty<AnimationTriggers>();
					break;
				case "targetGraphic":
				{
					Type type = Type.GetType(reader.ReadProperty<string>());
					if (button.targetGraphic == null)
					{
						button.targetGraphic = (Graphic)reader.ReadProperty(type);
					}
					else
					{
						reader.ReadIntoProperty<Graphic>(button.targetGraphic);
					}
					break;
				}
				case "interactable":
					button.interactable = reader.ReadProperty<bool>();
					break;
				case "image":
					if (button.image == null)
					{
						button.image = reader.ReadProperty<Image>();
					}
					else
					{
						reader.ReadIntoProperty<Image>(button.image);
					}
					break;
				case "useGUILayout":
					button.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					button.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					button.tag = reader.ReadProperty<string>();
					break;
				case "name":
					button.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					button.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
