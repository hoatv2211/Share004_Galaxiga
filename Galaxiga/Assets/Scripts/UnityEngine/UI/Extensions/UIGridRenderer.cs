using System;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Extensions/Primitives/UIGridRenderer")]
	public class UIGridRenderer : UILineRenderer
	{
		public int GridColumns
		{
			get
			{
				return this.m_GridColumns;
			}
			set
			{
				if (this.m_GridColumns == value)
				{
					return;
				}
				this.m_GridColumns = value;
				this.SetAllDirty();
			}
		}

		public int GridRows
		{
			get
			{
				return this.m_GridRows;
			}
			set
			{
				if (this.m_GridRows == value)
				{
					return;
				}
				this.m_GridRows = value;
				this.SetAllDirty();
			}
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			this.relativeSize = true;
			int num = this.GridRows * 3 + 1;
			if (this.GridRows % 2 == 0)
			{
				num++;
			}
			num += this.GridColumns * 3 + 1;
			this.m_points = new Vector2[num];
			int num2 = 0;
			for (int i = 0; i < this.GridRows; i++)
			{
				float x = 1f;
				float x2 = 0f;
				if (i % 2 == 0)
				{
					x = 0f;
					x2 = 1f;
				}
				float y = (float)i / (float)this.GridRows;
				this.m_points[num2].x = x;
				this.m_points[num2].y = y;
				num2++;
				this.m_points[num2].x = x2;
				this.m_points[num2].y = y;
				num2++;
				this.m_points[num2].x = x2;
				this.m_points[num2].y = (float)(i + 1) / (float)this.GridRows;
				num2++;
			}
			if (this.GridRows % 2 == 0)
			{
				this.m_points[num2].x = 1f;
				this.m_points[num2].y = 1f;
				num2++;
			}
			this.m_points[num2].x = 0f;
			this.m_points[num2].y = 1f;
			num2++;
			for (int j = 0; j < this.GridColumns; j++)
			{
				float y2 = 1f;
				float y3 = 0f;
				if (j % 2 == 0)
				{
					y2 = 0f;
					y3 = 1f;
				}
				float x3 = (float)j / (float)this.GridColumns;
				this.m_points[num2].x = x3;
				this.m_points[num2].y = y2;
				num2++;
				this.m_points[num2].x = x3;
				this.m_points[num2].y = y3;
				num2++;
				this.m_points[num2].x = (float)(j + 1) / (float)this.GridColumns;
				this.m_points[num2].y = y3;
				num2++;
			}
			if (this.GridColumns % 2 == 0)
			{
				this.m_points[num2].x = 1f;
				this.m_points[num2].y = 1f;
			}
			else
			{
				this.m_points[num2].x = 1f;
				this.m_points[num2].y = 0f;
			}
			base.OnPopulateMesh(vh);
		}

		[SerializeField]
		private int m_GridColumns = 10;

		[SerializeField]
		private int m_GridRows = 10;
	}
}
