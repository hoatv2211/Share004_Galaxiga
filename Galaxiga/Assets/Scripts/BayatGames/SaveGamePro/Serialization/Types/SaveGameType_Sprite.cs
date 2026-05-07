using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Sprite : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Sprite);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Sprite sprite = (Sprite)value;
			writer.WriteProperty<Texture2D>("texture", sprite.texture);
			writer.WriteProperty<Rect>("rect", sprite.rect);
			writer.WriteProperty<Vector2>("pivot", new Vector2(sprite.pivot.x / sprite.rect.width + sprite.rect.x, sprite.pivot.y / sprite.rect.height));
			writer.WriteProperty<float>("pixelsPerUnit", sprite.pixelsPerUnit);
			writer.WriteProperty<string>("name", sprite.name);
			writer.WriteProperty<HideFlags>("hideFlags", sprite.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Sprite sprite = Sprite.Create(reader.ReadProperty<Texture2D>(), reader.ReadProperty<Rect>(), reader.ReadProperty<Vector2>(), reader.ReadProperty<float>());
			this.ReadInto(sprite, reader);
			return sprite;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Sprite sprite = (Sprite)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "texture"))
					{
						if (!(text == "rect"))
						{
							if (!(text == "pivot"))
							{
								if (!(text == "pixelsPerUnit"))
								{
									if (!(text == "name"))
									{
										if (text == "hideFlags")
										{
											sprite.hideFlags = reader.ReadProperty<HideFlags>();
										}
									}
									else
									{
										sprite.name = reader.ReadProperty<string>();
									}
								}
								else
								{
									reader.ReadProperty<float>();
								}
							}
							else
							{
								reader.ReadProperty<Vector2>();
							}
						}
						else
						{
							reader.ReadProperty<Rect>();
						}
					}
					else
					{
						Texture2D texture2D = reader.ReadProperty<Texture2D>();
						sprite.texture.LoadRawTextureData(texture2D.GetRawTextureData());
					}
				}
			}
		}
	}
}
