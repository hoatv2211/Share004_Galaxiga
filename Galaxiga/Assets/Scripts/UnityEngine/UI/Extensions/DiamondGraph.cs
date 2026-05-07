using System;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Extensions/Primitives/Diamond Graph")]
	public class DiamondGraph : UIPrimitiveBase
	{
		public float A
		{
			get
			{
				return this.m_a;
			}
			set
			{
				this.m_a = value;
			}
		}

		public float B
		{
			get
			{
				return this.m_b;
			}
			set
			{
				this.m_b = value;
			}
		}

		public float C
		{
			get
			{
				return this.m_c;
			}
			set
			{
				this.m_c = value;
			}
		}

		public float D
		{
			get
			{
				return this.m_d;
			}
			set
			{
				this.m_d = value;
			}
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			float num = base.rectTransform.rect.width / 2f;
			this.m_a = Math.Min(1f, Math.Max(0f, this.m_a));
			this.m_b = Math.Min(1f, Math.Max(0f, this.m_b));
			this.m_c = Math.Min(1f, Math.Max(0f, this.m_c));
			this.m_d = Math.Min(1f, Math.Max(0f, this.m_d));
			Color32 color = this.color;
			vh.AddVert(new Vector3(-num * this.m_a, 0f), color, new Vector2(0f, 0f));
			vh.AddVert(new Vector3(0f, num * this.m_b), color, new Vector2(0f, 1f));
			vh.AddVert(new Vector3(num * this.m_c, 0f), color, new Vector2(1f, 1f));
			vh.AddVert(new Vector3(0f, -num * this.m_d), color, new Vector2(1f, 0f));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}

		[SerializeField]
		private float m_a = 1f;

		[SerializeField]
		private float m_b = 1f;

		[SerializeField]
		private float m_c = 1f;

		[SerializeField]
		private float m_d = 1f;
	}
}
