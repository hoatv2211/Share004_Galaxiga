using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_EventSystem : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(EventSystem);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			EventSystem eventSystem = (EventSystem)value;
			writer.WriteProperty<bool>("sendNavigationEvents", eventSystem.sendNavigationEvents);
			writer.WriteProperty<int>("pixelDragThreshold", eventSystem.pixelDragThreshold);
			writer.WriteProperty<GameObject>("firstSelectedGameObject", eventSystem.firstSelectedGameObject);
			writer.WriteProperty<bool>("useGUILayout", eventSystem.useGUILayout);
			writer.WriteProperty<bool>("enabled", eventSystem.enabled);
			writer.WriteProperty<string>("tag", eventSystem.tag);
			writer.WriteProperty<string>("name", eventSystem.name);
			writer.WriteProperty<HideFlags>("hideFlags", eventSystem.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			EventSystem eventSystem = SaveGameType.CreateComponent<EventSystem>();
			this.ReadInto(eventSystem, reader);
			return eventSystem;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			EventSystem eventSystem = (EventSystem)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "sendNavigationEvents":
					eventSystem.sendNavigationEvents = reader.ReadProperty<bool>();
					break;
				case "pixelDragThreshold":
					eventSystem.pixelDragThreshold = reader.ReadProperty<int>();
					break;
				case "firstSelectedGameObject":
					if (eventSystem.firstSelectedGameObject == null)
					{
						eventSystem.firstSelectedGameObject = reader.ReadProperty<GameObject>();
					}
					else
					{
						reader.ReadIntoProperty<GameObject>(eventSystem.firstSelectedGameObject);
					}
					break;
				case "useGUILayout":
					eventSystem.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					eventSystem.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					eventSystem.tag = reader.ReadProperty<string>();
					break;
				case "name":
					eventSystem.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					eventSystem.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
