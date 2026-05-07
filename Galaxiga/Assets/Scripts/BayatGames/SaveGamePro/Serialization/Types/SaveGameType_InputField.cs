using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_InputField : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(InputField);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			InputField inputField = (InputField)value;
			writer.WriteProperty<bool>("shouldHideMobileInput", inputField.shouldHideMobileInput);
			writer.WriteProperty<string>("text", inputField.text);
			writer.WriteProperty<float>("caretBlinkRate", inputField.caretBlinkRate);
			writer.WriteProperty<int>("caretWidth", inputField.caretWidth);
			writer.WriteProperty<Text>("textComponent", inputField.textComponent);
			writer.WriteProperty<string>("placeholderType", inputField.placeholder.GetType().AssemblyQualifiedName);
			writer.WriteProperty<Graphic>("placeholder", inputField.placeholder);
			writer.WriteProperty<Color>("caretColor", inputField.caretColor);
			writer.WriteProperty<bool>("customCaretColor", inputField.customCaretColor);
			writer.WriteProperty<Color>("selectionColor", inputField.selectionColor);
			writer.WriteProperty<int>("characterLimit", inputField.characterLimit);
			writer.WriteProperty<InputField.ContentType>("contentType", inputField.contentType);
			writer.WriteProperty<InputField.LineType>("lineType", inputField.lineType);
			writer.WriteProperty<InputField.InputType>("inputType", inputField.inputType);
			writer.WriteProperty<TouchScreenKeyboardType>("keyboardType", inputField.keyboardType);
			writer.WriteProperty<InputField.CharacterValidation>("characterValidation", inputField.characterValidation);
			writer.WriteProperty<bool>("readOnly", inputField.readOnly);
			writer.WriteProperty<char>("asteriskChar", inputField.asteriskChar);
			writer.WriteProperty<int>("caretPosition", inputField.caretPosition);
			writer.WriteProperty<int>("selectionAnchorPosition", inputField.selectionAnchorPosition);
			writer.WriteProperty<int>("selectionFocusPosition", inputField.selectionFocusPosition);
			writer.WriteProperty<Navigation>("navigation", inputField.navigation);
			writer.WriteProperty<Selectable.Transition>("transition", inputField.transition);
			writer.WriteProperty<ColorBlock>("colors", inputField.colors);
			writer.WriteProperty<SpriteState>("spriteState", inputField.spriteState);
			writer.WriteProperty<AnimationTriggers>("animationTriggers", inputField.animationTriggers);
			writer.WriteProperty<string>("targetGraphicType", inputField.targetGraphic.GetType().AssemblyQualifiedName);
			writer.WriteProperty<Graphic>("targetGraphic", inputField.targetGraphic);
			writer.WriteProperty<bool>("interactable", inputField.interactable);
			writer.WriteProperty<Image>("image", inputField.image);
			writer.WriteProperty<bool>("useGUILayout", inputField.useGUILayout);
			writer.WriteProperty<bool>("enabled", inputField.enabled);
			writer.WriteProperty<string>("tag", inputField.tag);
			writer.WriteProperty<string>("name", inputField.name);
			writer.WriteProperty<HideFlags>("hideFlags", inputField.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			InputField inputField = SaveGameType.CreateComponent<InputField>();
			this.ReadInto(inputField, reader);
			return inputField;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			InputField inputField = (InputField)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "shouldHideMobileInput":
					inputField.shouldHideMobileInput = reader.ReadProperty<bool>();
					break;
				case "text":
					inputField.text = reader.ReadProperty<string>();
					break;
				case "caretBlinkRate":
					inputField.caretBlinkRate = reader.ReadProperty<float>();
					break;
				case "caretWidth":
					inputField.caretWidth = reader.ReadProperty<int>();
					break;
				case "textComponent":
					if (inputField.textComponent == null)
					{
						inputField.textComponent = reader.ReadProperty<Text>();
					}
					else
					{
						reader.ReadIntoProperty<Text>(inputField.textComponent);
					}
					break;
				case "placeholder":
				{
					Type type = Type.GetType(reader.ReadProperty<string>());
					if (inputField.placeholder == null)
					{
						inputField.placeholder = (Graphic)reader.ReadProperty(type);
					}
					else
					{
						reader.ReadIntoProperty<Graphic>(inputField.placeholder);
					}
					break;
				}
				case "caretColor":
					inputField.caretColor = reader.ReadProperty<Color>();
					break;
				case "customCaretColor":
					inputField.customCaretColor = reader.ReadProperty<bool>();
					break;
				case "selectionColor":
					inputField.selectionColor = reader.ReadProperty<Color>();
					break;
				case "characterLimit":
					inputField.characterLimit = reader.ReadProperty<int>();
					break;
				case "contentType":
					inputField.contentType = reader.ReadProperty<InputField.ContentType>();
					break;
				case "lineType":
					inputField.lineType = reader.ReadProperty<InputField.LineType>();
					break;
				case "inputType":
					inputField.inputType = reader.ReadProperty<InputField.InputType>();
					break;
				case "keyboardType":
					inputField.keyboardType = reader.ReadProperty<TouchScreenKeyboardType>();
					break;
				case "characterValidation":
					inputField.characterValidation = reader.ReadProperty<InputField.CharacterValidation>();
					break;
				case "readOnly":
					inputField.readOnly = reader.ReadProperty<bool>();
					break;
				case "asteriskChar":
					inputField.asteriskChar = reader.ReadProperty<char>();
					break;
				case "caretPosition":
					inputField.caretPosition = reader.ReadProperty<int>();
					break;
				case "selectionAnchorPosition":
					inputField.selectionAnchorPosition = reader.ReadProperty<int>();
					break;
				case "selectionFocusPosition":
					inputField.selectionFocusPosition = reader.ReadProperty<int>();
					break;
				case "navigation":
					inputField.navigation = reader.ReadProperty<Navigation>();
					break;
				case "transition":
					inputField.transition = reader.ReadProperty<Selectable.Transition>();
					break;
				case "colors":
					inputField.colors = reader.ReadProperty<ColorBlock>();
					break;
				case "spriteState":
					inputField.spriteState = reader.ReadProperty<SpriteState>();
					break;
				case "animationTriggers":
					inputField.animationTriggers = reader.ReadProperty<AnimationTriggers>();
					break;
				case "targetGraphic":
				{
					Type type2 = Type.GetType(reader.ReadProperty<string>());
					if (inputField.targetGraphic == null)
					{
						inputField.targetGraphic = (Graphic)reader.ReadProperty(type2);
					}
					else
					{
						reader.ReadIntoProperty<Graphic>(inputField.targetGraphic);
					}
					break;
				}
				case "interactable":
					inputField.interactable = reader.ReadProperty<bool>();
					break;
				case "image":
					if (inputField.image == null)
					{
						inputField.image = reader.ReadProperty<Image>();
					}
					else
					{
						reader.ReadIntoProperty<Image>(inputField.image);
					}
					break;
				case "useGUILayout":
					inputField.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					inputField.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					inputField.tag = reader.ReadProperty<string>();
					break;
				case "name":
					inputField.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					inputField.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
