using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Mask : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Mask);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Mask mask = (Mask)value;
			writer.WriteProperty<bool>("showMaskGraphic", mask.showMaskGraphic);
			writer.WriteProperty<bool>("useGUILayout", mask.useGUILayout);
			writer.WriteProperty<bool>("enabled", mask.enabled);
			writer.WriteProperty<string>("tag", mask.tag);
			writer.WriteProperty<string>("name", mask.name);
			writer.WriteProperty<HideFlags>("hideFlags", mask.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Mask mask = SaveGameType.CreateComponent<Mask>();
			this.ReadInto(mask, reader);
			return mask;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Mask mask = (Mask)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "showMaskGraphic"))
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
											mask.hideFlags = reader.ReadProperty<HideFlags>();
										}
									}
									else
									{
										mask.name = reader.ReadProperty<string>();
									}
								}
								else
								{
									mask.tag = reader.ReadProperty<string>();
								}
							}
							else
							{
								mask.enabled = reader.ReadProperty<bool>();
							}
						}
						else
						{
							mask.useGUILayout = reader.ReadProperty<bool>();
						}
					}
					else
					{
						mask.showMaskGraphic = reader.ReadProperty<bool>();
					}
				}
			}
		}
	}
}
