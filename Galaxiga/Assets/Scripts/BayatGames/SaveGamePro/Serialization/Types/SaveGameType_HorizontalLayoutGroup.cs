using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_HorizontalLayoutGroup : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(HorizontalLayoutGroup);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			HorizontalLayoutGroup horizontalLayoutGroup = (HorizontalLayoutGroup)value;
			writer.WriteProperty<float>("spacing", horizontalLayoutGroup.spacing);
			writer.WriteProperty<bool>("childForceExpandWidth", horizontalLayoutGroup.childForceExpandWidth);
			writer.WriteProperty<bool>("childForceExpandHeight", horizontalLayoutGroup.childForceExpandHeight);
			writer.WriteProperty<bool>("childControlWidth", horizontalLayoutGroup.childControlWidth);
			writer.WriteProperty<bool>("childControlHeight", horizontalLayoutGroup.childControlHeight);
			writer.WriteProperty<RectOffset>("padding", horizontalLayoutGroup.padding);
			writer.WriteProperty<TextAnchor>("childAlignment", horizontalLayoutGroup.childAlignment);
			writer.WriteProperty<bool>("useGUILayout", horizontalLayoutGroup.useGUILayout);
			writer.WriteProperty<bool>("enabled", horizontalLayoutGroup.enabled);
			writer.WriteProperty<string>("tag", horizontalLayoutGroup.tag);
			writer.WriteProperty<string>("name", horizontalLayoutGroup.name);
			writer.WriteProperty<HideFlags>("hideFlags", horizontalLayoutGroup.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			HorizontalLayoutGroup horizontalLayoutGroup = SaveGameType.CreateComponent<HorizontalLayoutGroup>();
			this.ReadInto(horizontalLayoutGroup, reader);
			return horizontalLayoutGroup;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			HorizontalLayoutGroup horizontalLayoutGroup = (HorizontalLayoutGroup)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "spacing":
					horizontalLayoutGroup.spacing = reader.ReadProperty<float>();
					break;
				case "childForceExpandWidth":
					horizontalLayoutGroup.childForceExpandWidth = reader.ReadProperty<bool>();
					break;
				case "childForceExpandHeight":
					horizontalLayoutGroup.childForceExpandHeight = reader.ReadProperty<bool>();
					break;
				case "childControlWidth":
					horizontalLayoutGroup.childControlWidth = reader.ReadProperty<bool>();
					break;
				case "childControlHeight":
					horizontalLayoutGroup.childControlHeight = reader.ReadProperty<bool>();
					break;
				case "padding":
					horizontalLayoutGroup.padding = reader.ReadProperty<RectOffset>();
					break;
				case "childAlignment":
					horizontalLayoutGroup.childAlignment = reader.ReadProperty<TextAnchor>();
					break;
				case "useGUILayout":
					horizontalLayoutGroup.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					horizontalLayoutGroup.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					horizontalLayoutGroup.tag = reader.ReadProperty<string>();
					break;
				case "name":
					horizontalLayoutGroup.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					horizontalLayoutGroup.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
