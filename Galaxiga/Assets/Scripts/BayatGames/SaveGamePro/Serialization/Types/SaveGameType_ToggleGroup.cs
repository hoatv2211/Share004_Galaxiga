using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ToggleGroup : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ToggleGroup);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ToggleGroup toggleGroup = (ToggleGroup)value;
			writer.WriteProperty<bool>("allowSwitchOff", toggleGroup.allowSwitchOff);
			writer.WriteProperty<bool>("useGUILayout", toggleGroup.useGUILayout);
			writer.WriteProperty<bool>("enabled", toggleGroup.enabled);
			writer.WriteProperty<string>("tag", toggleGroup.tag);
			writer.WriteProperty<string>("name", toggleGroup.name);
			writer.WriteProperty<HideFlags>("hideFlags", toggleGroup.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			ToggleGroup toggleGroup = SaveGameType.CreateComponent<ToggleGroup>();
			this.ReadInto(toggleGroup, reader);
			return toggleGroup;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			ToggleGroup toggleGroup = (ToggleGroup)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "allowSwitchOff"))
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
											toggleGroup.hideFlags = reader.ReadProperty<HideFlags>();
										}
									}
									else
									{
										toggleGroup.name = reader.ReadProperty<string>();
									}
								}
								else
								{
									toggleGroup.tag = reader.ReadProperty<string>();
								}
							}
							else
							{
								toggleGroup.enabled = reader.ReadProperty<bool>();
							}
						}
						else
						{
							toggleGroup.useGUILayout = reader.ReadProperty<bool>();
						}
					}
					else
					{
						toggleGroup.allowSwitchOff = reader.ReadProperty<bool>();
					}
				}
			}
		}
	}
}
