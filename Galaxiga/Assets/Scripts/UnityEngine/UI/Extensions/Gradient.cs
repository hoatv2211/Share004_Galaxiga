using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Effects/Extensions/Gradient")]
	public class Gradient : BaseMeshEffect
	{
		public GradientMode GradientMode
		{
			get
			{
				return this._gradientMode;
			}
			set
			{
				this._gradientMode = value;
				base.graphic.SetVerticesDirty();
			}
		}

		public GradientDir GradientDir
		{
			get
			{
				return this._gradientDir;
			}
			set
			{
				this._gradientDir = value;
				base.graphic.SetVerticesDirty();
			}
		}

		public bool OverwriteAllColor
		{
			get
			{
				return this._overwriteAllColor;
			}
			set
			{
				this._overwriteAllColor = value;
				base.graphic.SetVerticesDirty();
			}
		}

		public Color Vertex1
		{
			get
			{
				return this._vertex1;
			}
			set
			{
				this._vertex1 = value;
				base.graphic.SetAllDirty();
			}
		}

		public Color Vertex2
		{
			get
			{
				return this._vertex2;
			}
			set
			{
				this._vertex2 = value;
				base.graphic.SetAllDirty();
			}
		}

		protected override void Awake()
		{
			this.targetGraphic = base.GetComponent<Graphic>();
		}

		public override void ModifyMesh(VertexHelper vh)
		{
			int currentVertCount = vh.currentVertCount;
			if (!this.IsActive() || currentVertCount == 0)
			{
				return;
			}
			List<UIVertex> list = new List<UIVertex>();
			vh.GetUIVertexStream(list);
			UIVertex vertex = default(UIVertex);
			if (this._gradientMode == GradientMode.Global)
			{
				if (this._gradientDir == GradientDir.DiagonalLeftToRight || this._gradientDir == GradientDir.DiagonalRightToLeft)
				{
					this._gradientDir = GradientDir.Vertical;
				}
				float num = (this._gradientDir != GradientDir.Vertical) ? list[list.Count - 1].position.x : list[list.Count - 1].position.y;
				float num2 = (this._gradientDir != GradientDir.Vertical) ? list[0].position.x : list[0].position.y;
				float num3 = num2 - num;
				for (int i = 0; i < currentVertCount; i++)
				{
					vh.PopulateUIVertex(ref vertex, i);
					if (this._overwriteAllColor || !(vertex.color != this.targetGraphic.color))
					{
						vertex.color *= Color.Lerp(this._vertex2, this._vertex1, (((this._gradientDir != GradientDir.Vertical) ? vertex.position.x : vertex.position.y) - num) / num3);
						vh.SetUIVertex(vertex, i);
					}
				}
			}
			else
			{
				for (int j = 0; j < currentVertCount; j++)
				{
					vh.PopulateUIVertex(ref vertex, j);
					if (this._overwriteAllColor || this.CompareCarefully(vertex.color, this.targetGraphic.color))
					{
						switch (this._gradientDir)
						{
						case GradientDir.Vertical:
							vertex.color *= ((j % 4 != 0 && (j - 1) % 4 != 0) ? this._vertex2 : this._vertex1);
							break;
						case GradientDir.Horizontal:
							vertex.color *= ((j % 4 != 0 && (j - 3) % 4 != 0) ? this._vertex2 : this._vertex1);
							break;
						case GradientDir.DiagonalLeftToRight:
							vertex.color *= ((j % 4 != 0) ? (((j - 2) % 4 != 0) ? Color.Lerp(this._vertex2, this._vertex1, 0.5f) : this._vertex2) : this._vertex1);
							break;
						case GradientDir.DiagonalRightToLeft:
							vertex.color *= (((j - 1) % 4 != 0) ? (((j - 3) % 4 != 0) ? Color.Lerp(this._vertex2, this._vertex1, 0.5f) : this._vertex2) : this._vertex1);
							break;
						}
						vh.SetUIVertex(vertex, j);
					}
				}
			}
		}

		private bool CompareCarefully(Color col1, Color col2)
		{
			return Mathf.Abs(col1.r - col2.r) < 0.003f && Mathf.Abs(col1.g - col2.g) < 0.003f && Mathf.Abs(col1.b - col2.b) < 0.003f && Mathf.Abs(col1.a - col2.a) < 0.003f;
		}

		[SerializeField]
		private GradientMode _gradientMode;

		[SerializeField]
		private GradientDir _gradientDir;

		[SerializeField]
		private bool _overwriteAllColor;

		[SerializeField]
		private Color _vertex1 = Color.white;

		[SerializeField]
		private Color _vertex2 = Color.black;

		private Graphic targetGraphic;
	}
}
