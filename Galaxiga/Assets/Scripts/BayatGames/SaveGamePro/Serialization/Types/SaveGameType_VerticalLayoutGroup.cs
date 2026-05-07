using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_VerticalLayoutGroup : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(VerticalLayoutGroup);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			VerticalLayoutGroup verticalLayoutGroup = (VerticalLayoutGroup)value;
			writer.WriteProperty<float>("spacing", verticalLayoutGroup.spacing);
			writer.WriteProperty<bool>("childForceExpandWidth", verticalLayoutGroup.childForceExpandWidth);
			writer.WriteProperty<bool>("childForceExpandHeight", verticalLayoutGroup.childForceExpandHeight);
			writer.WriteProperty<bool>("childControlWidth", verticalLayoutGroup.childControlWidth);
			writer.WriteProperty<bool>("childControlHeight", verticalLayoutGroup.childControlHeight);
			writer.WriteProperty<RectOffset>("padding", verticalLayoutGroup.padding);
			writer.WriteProperty<TextAnchor>("childAlignment", verticalLayoutGroup.childAlignment);
			writer.WriteProperty<bool>("useGUILayout", verticalLayoutGroup.useGUILayout);
			writer.WriteProperty<bool>("enabled", verticalLayoutGroup.enabled);
			writer.WriteProperty<string>("tag", verticalLayoutGroup.tag);
			writer.WriteProperty<string>("name", verticalLayoutGroup.name);
			writer.WriteProperty<HideFlags>("hideFlags", verticalLayoutGroup.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			VerticalLayoutGroup verticalLayoutGroup = SaveGameType.CreateComponent<VerticalLayoutGroup>();
			this.ReadInto(verticalLayoutGroup, reader);
			return verticalLayoutGroup;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			VerticalLayoutGroup verticalLayoutGroup = (VerticalLayoutGroup)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "spacing":
					verticalLayoutGroup.spacing = reader.ReadProperty<float>();
					break;
				case "childForceExpandWidth":
					verticalLayoutGroup.childForceExpandWidth = reader.ReadProperty<bool>();
					break;
				case "childForceExpandHeight":
					verticalLayoutGroup.childForceExpandHeight = reader.ReadProperty<bool>();
					break;
				case "childControlWidth":
					verticalLayoutGroup.childControlWidth = reader.ReadProperty<bool>();
					break;
				case "childControlHeight":
					verticalLayoutGroup.childControlHeight = reader.ReadProperty<bool>();
					break;
				case "padding":
					verticalLayoutGroup.padding = reader.ReadProperty<RectOffset>();
					break;
				case "childAlignment":
					verticalLayoutGroup.childAlignment = reader.ReadProperty<TextAnchor>();
					break;
				case "useGUILayout":
					verticalLayoutGroup.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					verticalLayoutGroup.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					verticalLayoutGroup.tag = reader.ReadProperty<string>();
					break;
				case "name":
					verticalLayoutGroup.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					verticalLayoutGroup.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
