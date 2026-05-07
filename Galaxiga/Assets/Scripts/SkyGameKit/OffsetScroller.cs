using System;
using UnityEngine;

namespace SkyGameKit
{
	public class OffsetScroller : SgkSingleton<OffsetScroller>, ICanGetSpeed
	{
		protected virtual void Start()
		{
			PointWave.speedProvider = this;
			this.myRenderer = base.GetComponent<Renderer>();
			this.savedOffset = this.myRenderer.sharedMaterial.GetTextureOffset("_MainTex");
		}

		protected virtual void Update()
		{
			float y = Mathf.Repeat(Time.time * this.scrollSpeed, 1f);
			Vector2 value = new Vector2(this.savedOffset.x, y);
			this.myRenderer.sharedMaterial.SetTextureOffset("_MainTex", value);
		}

		protected virtual void OnDisable()
		{
			if (this.myRenderer != null)
			{
				this.myRenderer.sharedMaterial.SetTextureOffset("_MainTex", this.savedOffset);
			}
		}

		public float GetSpeed()
		{
			if (this.myRenderer == null)
			{
				this.myRenderer = base.GetComponent<Renderer>();
			}
			return base.transform.localScale.y * (1f / this.myRenderer.sharedMaterial.GetTextureScale("_MainTex").y) * this.scrollSpeed;
		}

		public float scrollSpeed = 0.05f;

		private Vector2 savedOffset;

		private Renderer myRenderer;
	}
}
