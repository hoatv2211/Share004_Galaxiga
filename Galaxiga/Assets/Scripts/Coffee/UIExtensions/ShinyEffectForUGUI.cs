using System;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	public class ShinyEffectForUGUI : BaseMeshEffect
	{
		public new Graphic graphic
		{
			get
			{
				return base.graphic;
			}
		}

		public float location
		{
			get
			{
				return this.m_Location;
			}
			set
			{
				this.m_Location = Mathf.Clamp(value, 0f, 1f);
				this._SetDirty();
			}
		}

		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = Mathf.Clamp(value, 0f, 2f);
				this._SetDirty();
			}
		}

		public float alpha
		{
			get
			{
				return this.m_Alpha;
			}
			set
			{
				this.m_Alpha = Mathf.Clamp(value, 0f, 1f);
				this._SetDirty();
			}
		}

		public float rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				if (!Mathf.Approximately(this.m_Rotation, value))
				{
					this.m_Rotation = value;
					this._SetDirty();
				}
			}
		}

		public virtual Material effectMaterial
		{
			get
			{
				return this.m_EffectMaterial;
			}
		}

		protected override void OnEnable()
		{
			this.graphic.material = this.effectMaterial;
			base.OnEnable();
		}

		protected override void OnDisable()
		{
			this.graphic.material = null;
			base.OnDisable();
		}

		public void OnBeforeSerialize()
		{
		}

		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			Rect rect = this.graphic.rectTransform.rect;
			float f = this.rotation * 0.0174532924f;
			Vector2 normalized = new Vector2(Mathf.Cos(f), Mathf.Sin(f));
			normalized.x *= rect.height / rect.width;
			normalized = normalized.normalized;
			UIVertex vertex = default(UIVertex);
			ShinyEffectForUGUI.Matrix2x3 m = new ShinyEffectForUGUI.Matrix2x3(rect, normalized.x, normalized.y);
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				vertex.uv1 = new Vector2(ShinyEffectForUGUI._PackToFloat(Mathf.Clamp01((m * vertex.position).y), this.location, this.width, this.alpha), 0f);
				vh.SetUIVertex(vertex, i);
			}
		}

		private void _SetDirty()
		{
			if (this.graphic)
			{
				this.graphic.SetVerticesDirty();
			}
		}

		private static float _PackToFloat(float x, float y, float z, float w)
		{
			return (float)((Mathf.FloorToInt(w * 63f) << 18) + (Mathf.FloorToInt(z * 63f) << 12) + (Mathf.FloorToInt(y * 63f) << 6) + Mathf.FloorToInt(x * 63f));
		}

		public const string shaderName = "UI/Hidden/UI-Effect-Shiny";

		[SerializeField]
		[Range(0f, 1f)]
		private float m_Location;

		[SerializeField]
		[Range(0f, 1f)]
		private float m_Width = 0.25f;

		[SerializeField]
		[Range(0f, 1f)]
		private float m_Alpha = 1f;

		[SerializeField]
		[Range(-180f, 180f)]
		private float m_Rotation;

		[SerializeField]
		private Material m_EffectMaterial;

		private struct Matrix2x3
		{
			public Matrix2x3(Rect rect, float cos, float sin)
			{
				float num = -rect.xMin / rect.width - 0.5f;
				float num2 = -rect.yMin / rect.height - 0.5f;
				this.m00 = cos / rect.width;
				this.m01 = -sin / rect.height;
				this.m02 = num * cos - num2 * sin + 0.5f;
				this.m10 = sin / rect.width;
				this.m11 = cos / rect.height;
				this.m12 = num * sin + num2 * cos + 0.5f;
			}

			public static Vector2 operator *(ShinyEffectForUGUI.Matrix2x3 m, Vector2 v)
			{
				return new Vector2(m.m00 * v.x + m.m01 * v.y + m.m02, m.m10 * v.x + m.m11 * v.y + m.m12);
			}

			public float m00;

			public float m01;

			public float m02;

			public float m10;

			public float m11;

			public float m12;
		}
	}
}
