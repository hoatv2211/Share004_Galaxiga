using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_LayoutElement : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(LayoutElement);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			LayoutElement layoutElement = (LayoutElement)value;
			writer.WriteProperty<bool>("ignoreLayout", layoutElement.ignoreLayout);
			writer.WriteProperty<float>("minWidth", layoutElement.minWidth);
			writer.WriteProperty<float>("minHeight", layoutElement.minHeight);
			writer.WriteProperty<float>("preferredWidth", layoutElement.preferredWidth);
			writer.WriteProperty<float>("preferredHeight", layoutElement.preferredHeight);
			writer.WriteProperty<float>("flexibleWidth", layoutElement.flexibleWidth);
			writer.WriteProperty<float>("flexibleHeight", layoutElement.flexibleHeight);
			writer.WriteProperty<int>("layoutPriority", layoutElement.layoutPriority);
			writer.WriteProperty<bool>("useGUILayout", layoutElement.useGUILayout);
			writer.WriteProperty<bool>("enabled", layoutElement.enabled);
			writer.WriteProperty<string>("tag", layoutElement.tag);
			writer.WriteProperty<string>("name", layoutElement.name);
			writer.WriteProperty<HideFlags>("hideFlags", layoutElement.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			LayoutElement layoutElement = SaveGameType.CreateComponent<LayoutElement>();
			this.ReadInto(layoutElement, reader);
			return layoutElement;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			LayoutElement layoutElement = (LayoutElement)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "ignoreLayout":
					layoutElement.ignoreLayout = reader.ReadProperty<bool>();
					break;
				case "minWidth":
					layoutElement.minWidth = reader.ReadProperty<float>();
					break;
				case "minHeight":
					layoutElement.minHeight = reader.ReadProperty<float>();
					break;
				case "preferredWidth":
					layoutElement.preferredWidth = reader.ReadProperty<float>();
					break;
				case "preferredHeight":
					layoutElement.preferredHeight = reader.ReadProperty<float>();
					break;
				case "flexibleWidth":
					layoutElement.flexibleWidth = reader.ReadProperty<float>();
					break;
				case "flexibleHeight":
					layoutElement.flexibleHeight = reader.ReadProperty<float>();
					break;
				case "layoutPriority":
					layoutElement.layoutPriority = reader.ReadProperty<int>();
					break;
				case "useGUILayout":
					layoutElement.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					layoutElement.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					layoutElement.tag = reader.ReadProperty<string>();
					break;
				case "name":
					layoutElement.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					layoutElement.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
