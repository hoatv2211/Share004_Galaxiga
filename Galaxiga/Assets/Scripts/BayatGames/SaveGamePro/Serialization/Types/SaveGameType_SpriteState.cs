using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SpriteState : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SpriteState);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SpriteState spriteState = (SpriteState)value;
			writer.WriteProperty<Sprite>("highlightedSprite", spriteState.highlightedSprite);
			writer.WriteProperty<Sprite>("pressedSprite", spriteState.pressedSprite);
			writer.WriteProperty<Sprite>("disabledSprite", spriteState.disabledSprite);
		}

		public override object Read(ISaveGameReader reader)
		{
			SpriteState spriteState = default(SpriteState);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "highlightedSprite"))
					{
						if (!(text == "pressedSprite"))
						{
							if (text == "disabledSprite")
							{
								if (spriteState.disabledSprite == null)
								{
									spriteState.disabledSprite = reader.ReadProperty<Sprite>();
								}
								else
								{
									reader.ReadIntoProperty<Sprite>(spriteState.disabledSprite);
								}
							}
						}
						else if (spriteState.pressedSprite == null)
						{
							spriteState.pressedSprite = reader.ReadProperty<Sprite>();
						}
						else
						{
							reader.ReadIntoProperty<Sprite>(spriteState.pressedSprite);
						}
					}
					else if (spriteState.highlightedSprite == null)
					{
						spriteState.highlightedSprite = reader.ReadProperty<Sprite>();
					}
					else
					{
						reader.ReadIntoProperty<Sprite>(spriteState.highlightedSprite);
					}
				}
			}
			return spriteState;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
