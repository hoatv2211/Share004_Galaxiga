using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_LightmapData : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(LightmapData);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			LightmapData lightmapData = (LightmapData)value;
			writer.WriteProperty<Texture2D>("lightmapColor", lightmapData.lightmapColor);
			writer.WriteProperty<Texture2D>("lightmapDir", lightmapData.lightmapDir);
			writer.WriteProperty<Texture2D>("shadowMask", lightmapData.shadowMask);
		}

		public override object Read(ISaveGameReader reader)
		{
			LightmapData lightmapData = new LightmapData();
			this.ReadInto(lightmapData, reader);
			return lightmapData;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			LightmapData lightmapData = (LightmapData)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "lightmapColor"))
					{
						if (!(text == "lightmapDir"))
						{
							if (text == "shadowMask")
							{
								if (lightmapData.shadowMask == null)
								{
									lightmapData.shadowMask = reader.ReadProperty<Texture2D>();
								}
								else
								{
									reader.ReadIntoProperty<Texture2D>(lightmapData.shadowMask);
								}
							}
						}
						else if (lightmapData.lightmapDir == null)
						{
							lightmapData.lightmapDir = reader.ReadProperty<Texture2D>();
						}
						else
						{
							reader.ReadIntoProperty<Texture2D>(lightmapData.lightmapDir);
						}
					}
					else if (lightmapData.lightmapColor == null)
					{
						lightmapData.lightmapColor = reader.ReadProperty<Texture2D>();
					}
					else
					{
						reader.ReadIntoProperty<Texture2D>(lightmapData.lightmapColor);
					}
				}
			}
		}
	}
}
