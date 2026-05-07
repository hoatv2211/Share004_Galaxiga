using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ColorBlock : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ColorBlock);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ColorBlock colorBlock = (ColorBlock)value;
			writer.WriteProperty<Color>("normalColor", colorBlock.normalColor);
			writer.WriteProperty<Color>("highlightedColor", colorBlock.highlightedColor);
			writer.WriteProperty<Color>("pressedColor", colorBlock.pressedColor);
			writer.WriteProperty<Color>("disabledColor", colorBlock.disabledColor);
			writer.WriteProperty<float>("colorMultiplier", colorBlock.colorMultiplier);
			writer.WriteProperty<float>("fadeDuration", colorBlock.fadeDuration);
		}

		public override object Read(ISaveGameReader reader)
		{
			ColorBlock colorBlock = default(ColorBlock);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "normalColor"))
					{
						if (!(text == "highlightedColor"))
						{
							if (!(text == "pressedColor"))
							{
								if (!(text == "disabledColor"))
								{
									if (!(text == "colorMultiplier"))
									{
										if (text == "fadeDuration")
										{
											colorBlock.fadeDuration = reader.ReadProperty<float>();
										}
									}
									else
									{
										colorBlock.colorMultiplier = reader.ReadProperty<float>();
									}
								}
								else
								{
									colorBlock.disabledColor = reader.ReadProperty<Color>();
								}
							}
							else
							{
								colorBlock.pressedColor = reader.ReadProperty<Color>();
							}
						}
						else
						{
							colorBlock.highlightedColor = reader.ReadProperty<Color>();
						}
					}
					else
					{
						colorBlock.normalColor = reader.ReadProperty<Color>();
					}
				}
			}
			return colorBlock;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
