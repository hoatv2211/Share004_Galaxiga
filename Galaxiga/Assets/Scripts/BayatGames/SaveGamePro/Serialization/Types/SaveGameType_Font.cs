using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Font : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Font);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Font font = (Font)value;
			writer.WriteProperty<string[]>("fontNames", font.fontNames);
			writer.WriteProperty<CharacterInfo[]>("characterInfo", font.characterInfo);
			writer.WriteProperty<string>("name", font.name);
			writer.WriteProperty<HideFlags>("hideFlags", font.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Font font = new Font();
			this.ReadInto(font, reader);
			return font;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Font font = (Font)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "fontNames"))
					{
						if (!(text == "characterInfo"))
						{
							if (!(text == "name"))
							{
								if (text == "hideFlags")
								{
									font.hideFlags = reader.ReadProperty<HideFlags>();
								}
							}
							else
							{
								font.name = reader.ReadProperty<string>();
							}
						}
						else
						{
							font.characterInfo = reader.ReadProperty<CharacterInfo[]>();
						}
					}
					else
					{
						font.fontNames = reader.ReadProperty<string[]>();
					}
				}
			}
		}
	}
}
