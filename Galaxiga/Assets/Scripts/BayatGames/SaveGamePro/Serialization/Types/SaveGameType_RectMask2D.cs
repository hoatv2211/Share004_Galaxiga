using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_RectMask2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(RectMask2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			RectMask2D rectMask2D = (RectMask2D)value;
			writer.WriteProperty<bool>("useGUILayout", rectMask2D.useGUILayout);
			writer.WriteProperty<bool>("enabled", rectMask2D.enabled);
			writer.WriteProperty<string>("tag", rectMask2D.tag);
			writer.WriteProperty<string>("name", rectMask2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", rectMask2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			RectMask2D rectMask2D = SaveGameType.CreateComponent<RectMask2D>();
			this.ReadInto(rectMask2D, reader);
			return rectMask2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			RectMask2D rectMask2D = (RectMask2D)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
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
										rectMask2D.hideFlags = reader.ReadProperty<HideFlags>();
									}
								}
								else
								{
									rectMask2D.name = reader.ReadProperty<string>();
								}
							}
							else
							{
								rectMask2D.tag = reader.ReadProperty<string>();
							}
						}
						else
						{
							rectMask2D.enabled = reader.ReadProperty<bool>();
						}
					}
					else
					{
						rectMask2D.useGUILayout = reader.ReadProperty<bool>();
					}
				}
			}
		}
	}
}
