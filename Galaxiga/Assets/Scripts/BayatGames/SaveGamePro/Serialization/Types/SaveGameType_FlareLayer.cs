using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_FlareLayer : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(FlareLayer);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			FlareLayer flareLayer = (FlareLayer)value;
			writer.WriteProperty<bool>("enabled", flareLayer.enabled);
			writer.WriteProperty<string>("tag", flareLayer.tag);
			writer.WriteProperty<string>("name", flareLayer.name);
			writer.WriteProperty<HideFlags>("hideFlags", flareLayer.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			FlareLayer flareLayer = SaveGameType.CreateComponent<FlareLayer>();
			this.ReadInto(flareLayer, reader);
			return flareLayer;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			FlareLayer flareLayer = (FlareLayer)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "enabled"))
					{
						if (!(text == "tag"))
						{
							if (!(text == "name"))
							{
								if (text == "hideFlags")
								{
									flareLayer.hideFlags = reader.ReadProperty<HideFlags>();
								}
							}
							else
							{
								flareLayer.name = reader.ReadProperty<string>();
							}
						}
						else
						{
							flareLayer.tag = reader.ReadProperty<string>();
						}
					}
					else
					{
						flareLayer.enabled = reader.ReadProperty<bool>();
					}
				}
			}
		}
	}
}
