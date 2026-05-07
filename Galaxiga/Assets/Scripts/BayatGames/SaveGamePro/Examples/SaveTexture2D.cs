using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Examples
{
	public class SaveTexture2D : MonoBehaviour
	{
		private void Awake()
		{
			this.image.sprite = Sprite.Create(this.texture, new Rect(0f, 0f, (float)this.texture.width, (float)this.texture.height), new Vector2(0.5f, 0.5f));
		}

		public void ClearImage()
		{
			this.image.sprite = null;
		}

		public void Save()
		{
			SaveGame.Save<Texture2D>("texture", this.texture);
		}

		public void Load()
		{
			this.texture = SaveGame.Load<Texture2D>("texture");
			this.image.sprite = Sprite.Create(this.texture, new Rect(0f, 0f, (float)this.texture.width, (float)this.texture.height), new Vector2(0.5f, 0.5f));
		}

		public Texture2D texture;

		public Image image;
	}
}
