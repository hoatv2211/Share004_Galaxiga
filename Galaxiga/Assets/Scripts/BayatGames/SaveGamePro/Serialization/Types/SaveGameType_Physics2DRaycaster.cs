using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Physics2DRaycaster : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Physics2DRaycaster);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Physics2DRaycaster physics2DRaycaster = (Physics2DRaycaster)value;
			writer.WriteProperty<LayerMask>("eventMask", physics2DRaycaster.eventMask);
			writer.WriteProperty<bool>("useGUILayout", physics2DRaycaster.useGUILayout);
			writer.WriteProperty<bool>("enabled", physics2DRaycaster.enabled);
			writer.WriteProperty<string>("tag", physics2DRaycaster.tag);
			writer.WriteProperty<string>("name", physics2DRaycaster.name);
			writer.WriteProperty<HideFlags>("hideFlags", physics2DRaycaster.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Physics2DRaycaster physics2DRaycaster = SaveGameType.CreateComponent<Physics2DRaycaster>();
			this.ReadInto(physics2DRaycaster, reader);
			return physics2DRaycaster;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Physics2DRaycaster physics2DRaycaster = (Physics2DRaycaster)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "eventMask"))
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
											physics2DRaycaster.hideFlags = reader.ReadProperty<HideFlags>();
										}
									}
									else
									{
										physics2DRaycaster.name = reader.ReadProperty<string>();
									}
								}
								else
								{
									physics2DRaycaster.tag = reader.ReadProperty<string>();
								}
							}
							else
							{
								physics2DRaycaster.enabled = reader.ReadProperty<bool>();
							}
						}
						else
						{
							physics2DRaycaster.useGUILayout = reader.ReadProperty<bool>();
						}
					}
					else
					{
						physics2DRaycaster.eventMask = reader.ReadProperty<LayerMask>();
					}
				}
			}
		}
	}
}
