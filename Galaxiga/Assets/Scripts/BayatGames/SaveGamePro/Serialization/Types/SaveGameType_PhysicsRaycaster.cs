using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_PhysicsRaycaster : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(PhysicsRaycaster);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			PhysicsRaycaster physicsRaycaster = (PhysicsRaycaster)value;
			writer.WriteProperty<LayerMask>("eventMask", physicsRaycaster.eventMask);
			writer.WriteProperty<bool>("useGUILayout", physicsRaycaster.useGUILayout);
			writer.WriteProperty<bool>("enabled", physicsRaycaster.enabled);
			writer.WriteProperty<string>("tag", physicsRaycaster.tag);
			writer.WriteProperty<string>("name", physicsRaycaster.name);
			writer.WriteProperty<HideFlags>("hideFlags", physicsRaycaster.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			PhysicsRaycaster physicsRaycaster = SaveGameType.CreateComponent<PhysicsRaycaster>();
			this.ReadInto(physicsRaycaster, reader);
			return physicsRaycaster;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			PhysicsRaycaster physicsRaycaster = (PhysicsRaycaster)value;
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
											physicsRaycaster.hideFlags = reader.ReadProperty<HideFlags>();
										}
									}
									else
									{
										physicsRaycaster.name = reader.ReadProperty<string>();
									}
								}
								else
								{
									physicsRaycaster.tag = reader.ReadProperty<string>();
								}
							}
							else
							{
								physicsRaycaster.enabled = reader.ReadProperty<bool>();
							}
						}
						else
						{
							physicsRaycaster.useGUILayout = reader.ReadProperty<bool>();
						}
					}
					else
					{
						physicsRaycaster.eventMask = reader.ReadProperty<LayerMask>();
					}
				}
			}
		}
	}
}
