using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Examples
{
	public class SaveGameObject : MonoBehaviour
	{
		public void UpdateColor()
		{
			if (this.target == null)
			{
				UnityEngine.Debug.LogWarning("Target object is destroyed.");
				return;
			}
			if (this.targetRenderer == null)
			{
				this.targetRenderer = this.target.GetComponent<Renderer>();
			}
			this.targetRenderer.material.color = new Color(this.redSlider.value, this.greenSlider.value, this.blueSlider.value, this.alphaSlider.value);
		}

		public void DestroyTarget()
		{
			UnityEngine.Object.Destroy(this.target);
		}

		public void Save()
		{
			SaveGame.Save<GameObject>("gameObject.txt", this.target);
		}

		public void Load()
		{
			if (this.target == null)
			{
				this.target = SaveGame.Load<GameObject>("gameObject.txt");
			}
			else
			{
				SaveGame.LoadInto<GameObject>("gameObject.txt", this.target);
			}
			this.targetRenderer = this.target.GetComponent<Renderer>();
			this.redSlider.value = this.targetRenderer.material.color.r;
			this.greenSlider.value = this.targetRenderer.material.color.g;
			this.blueSlider.value = this.targetRenderer.material.color.b;
			this.alphaSlider.value = this.targetRenderer.material.color.a;
		}

		public GameObject target;

		public Renderer targetRenderer;

		public Slider redSlider;

		public Slider greenSlider;

		public Slider blueSlider;

		public Slider alphaSlider;
	}
}
