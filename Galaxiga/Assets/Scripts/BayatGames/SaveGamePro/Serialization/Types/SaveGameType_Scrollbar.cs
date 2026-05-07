using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Scrollbar : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Scrollbar);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Scrollbar scrollbar = (Scrollbar)value;
			writer.WriteProperty<RectTransform>("handleRect", scrollbar.handleRect);
			writer.WriteProperty<Scrollbar.Direction>("direction", scrollbar.direction);
			writer.WriteProperty<float>("value", scrollbar.value);
			writer.WriteProperty<float>("size", scrollbar.size);
			writer.WriteProperty<int>("numberOfSteps", scrollbar.numberOfSteps);
			writer.WriteProperty<Navigation>("navigation", scrollbar.navigation);
			writer.WriteProperty<Selectable.Transition>("transition", scrollbar.transition);
			writer.WriteProperty<ColorBlock>("colors", scrollbar.colors);
			writer.WriteProperty<SpriteState>("spriteState", scrollbar.spriteState);
			writer.WriteProperty<AnimationTriggers>("animationTriggers", scrollbar.animationTriggers);
			writer.WriteProperty<string>("targetGraphicType", scrollbar.targetGraphic.GetType().AssemblyQualifiedName);
			writer.WriteProperty<Graphic>("targetGraphic", scrollbar.targetGraphic);
			writer.WriteProperty<bool>("interactable", scrollbar.interactable);
			writer.WriteProperty<Image>("image", scrollbar.image);
			writer.WriteProperty<bool>("useGUILayout", scrollbar.useGUILayout);
			writer.WriteProperty<bool>("enabled", scrollbar.enabled);
			writer.WriteProperty<string>("tag", scrollbar.tag);
			writer.WriteProperty<string>("name", scrollbar.name);
			writer.WriteProperty<HideFlags>("hideFlags", scrollbar.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Scrollbar scrollbar = SaveGameType.CreateComponent<Scrollbar>();
			this.ReadInto(scrollbar, reader);
			return scrollbar;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Scrollbar scrollbar = (Scrollbar)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "handleRect":
					if (scrollbar.handleRect == null)
					{
						scrollbar.handleRect = reader.ReadProperty<RectTransform>();
					}
					else
					{
						reader.ReadIntoProperty<RectTransform>(scrollbar.handleRect);
					}
					break;
				case "direction":
					scrollbar.direction = reader.ReadProperty<Scrollbar.Direction>();
					break;
				case "value":
					scrollbar.value = reader.ReadProperty<float>();
					break;
				case "size":
					scrollbar.size = reader.ReadProperty<float>();
					break;
				case "numberOfSteps":
					scrollbar.numberOfSteps = reader.ReadProperty<int>();
					break;
				case "navigation":
					scrollbar.navigation = reader.ReadProperty<Navigation>();
					break;
				case "transition":
					scrollbar.transition = reader.ReadProperty<Selectable.Transition>();
					break;
				case "colors":
					scrollbar.colors = reader.ReadProperty<ColorBlock>();
					break;
				case "spriteState":
					scrollbar.spriteState = reader.ReadProperty<SpriteState>();
					break;
				case "animationTriggers":
					scrollbar.animationTriggers = reader.ReadProperty<AnimationTriggers>();
					break;
				case "targetGraphic":
				{
					Type type = Type.GetType(reader.ReadProperty<string>());
					if (scrollbar.targetGraphic == null)
					{
						scrollbar.targetGraphic = (Graphic)reader.ReadProperty(type);
					}
					else
					{
						reader.ReadIntoProperty<Graphic>(scrollbar.targetGraphic);
					}
					break;
				}
				case "interactable":
					scrollbar.interactable = reader.ReadProperty<bool>();
					break;
				case "image":
					if (scrollbar.image == null)
					{
						scrollbar.image = reader.ReadProperty<Image>();
					}
					else
					{
						reader.ReadIntoProperty<Image>(scrollbar.image);
					}
					break;
				case "useGUILayout":
					scrollbar.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					scrollbar.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					scrollbar.tag = reader.ReadProperty<string>();
					break;
				case "name":
					scrollbar.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					scrollbar.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
