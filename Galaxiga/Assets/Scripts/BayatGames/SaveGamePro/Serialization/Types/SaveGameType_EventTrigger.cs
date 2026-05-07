using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_EventTrigger : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(EventTrigger);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			EventTrigger eventTrigger = (EventTrigger)value;
			writer.WriteProperty<List<EventTrigger.Entry>>("triggers", eventTrigger.triggers);
			writer.WriteProperty<bool>("useGUILayout", eventTrigger.useGUILayout);
			writer.WriteProperty<bool>("enabled", eventTrigger.enabled);
			writer.WriteProperty<string>("tag", eventTrigger.tag);
			writer.WriteProperty<string>("name", eventTrigger.name);
			writer.WriteProperty<HideFlags>("hideFlags", eventTrigger.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			EventTrigger eventTrigger = SaveGameType.CreateComponent<EventTrigger>();
			this.ReadInto(eventTrigger, reader);
			return eventTrigger;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			EventTrigger eventTrigger = (EventTrigger)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "triggers"))
					{
						if (!(text == "useGUILayout"))
						{
							if (!(text == "enabled"))
							{
								if (!(text == "tag"))
								{
									if (!(text == "name"))
									{
										if (text == "hideFlags")
										{
											eventTrigger.hideFlags = reader.ReadProperty<HideFlags>();
										}
									}
									else
									{
										eventTrigger.name = reader.ReadProperty<string>();
									}
								}
								else
								{
									eventTrigger.tag = reader.ReadProperty<string>();
								}
							}
							else
							{
								eventTrigger.enabled = reader.ReadProperty<bool>();
							}
						}
						else
						{
							eventTrigger.useGUILayout = reader.ReadProperty<bool>();
						}
					}
					else
					{
						eventTrigger.triggers = reader.ReadProperty<List<EventTrigger.Entry>>();
					}
				}
			}
		}
	}
}
