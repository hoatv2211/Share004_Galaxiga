using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPolygon : Image, IMeshModifier
{
	public void ModifyMesh(VertexHelper vh)
	{
		this.EditMesh(vh);
	}

	public void ModifyMesh(Mesh mesh)
	{
		using (VertexHelper vertexHelper = new VertexHelper(mesh))
		{
			this.EditMesh(vertexHelper);
			vertexHelper.FillMesh(mesh);
		}
	}

	private void EditMesh(VertexHelper vh)
	{
		vh.Clear();
		int count = this.vertexInfoList.Count;
		if (count < 3)
		{
			return;
		}
		for (int i = 0; i < this.vertexInfoList.Count; i++)
		{
			float num = 6.28318548f / (float)this.vertexInfoList.Count;
			Vector3 v = new Vector3(Mathf.Cos(num * (float)i + this.offset) * 0.5f + 0.5f, Mathf.Sin(num * (float)i + this.offset) * 0.5f + 0.5f);
			vh.AddVert(this.getRadiusPosition(this.vertexInfoList[i], i, 1f), this.checkVertexColor(this.vertexInfoList[i].color), v);
		}
		if (!this.innerPolygon)
		{
			int[] array = new int[]
			{
				0,
				1,
				count - 1
			};
			for (int j = 0; j < count - 2; j++)
			{
				if (j % 2 == 1)
				{
					vh.AddTriangle(array[0], array[1], array[2]);
				}
				else
				{
					vh.AddTriangle(array[0], array[2], array[1]);
				}
				int num2 = (array[j % 3] != 0) ? ((count - 2 - j) * ((j % 2 != 1) ? -1 : 1)) : 2;
				array[j % 3] += num2;
			}
		}
		else
		{
			for (int k = 0; k < count; k++)
			{
				float num3 = 6.28318548f / (float)this.vertexInfoList.Count;
				Vector3 v2 = new Vector3(Mathf.Cos(num3 * (float)k + this.offset) * 0.5f * (1f - this.innerWidth) + 0.5f, Mathf.Sin(num3 * (float)k + this.offset) * 0.5f * (1f - this.innerWidth) + 0.5f);
				vh.AddVert(this.getRadiusPosition(this.vertexInfoList[k], k, 1f - this.innerWidth), this.checkVertexColor(this.vertexInfoList[k].color), v2);
			}
			for (int l = 0; l < count; l++)
			{
				vh.AddTriangle(l, count + (1 + l) % count, (l + 1) % count);
				vh.AddTriangle(l, l + count, count + (1 + l) % count);
			}
		}
	}

	private Vector3 getRadiusPosition(UIPolygon.PolygonVertexInfo info, int index, float scale = 1f)
	{
		if (this.vertexInfoList.Count < 3)
		{
			return Vector3.zero;
		}
		float num = base.rectTransform.rect.width / 2f * info.length;
		float num2 = base.rectTransform.rect.height / 2f * info.length;
		float num3 = 6.28318548f / (float)this.vertexInfoList.Count;
		float num4 = this.offset / 360f * 3.14159274f * 2f;
		Vector3 a = new Vector3(num * Mathf.Cos(num3 * (float)index + num4), num2 * Mathf.Sin(num3 * (float)index + num4));
		return a * scale;
	}

	private Color checkVertexColor(Color vertexColor)
	{
		if (this.vertexColorFlag)
		{
			return vertexColor;
		}
		return this.color;
	}

	public List<UIPolygon.PolygonVertexInfo> vertexInfoList = new List<UIPolygon.PolygonVertexInfo>();

	[Range(0f, 360f)]
	public float offset;

	public bool innerPolygon;

	[Header("innerPolygon 옵션에서 제로점과 가까울시 width의 예외처리가 되어있지 않음. ")]
	[Range(0f, 1f)]
	public float innerWidth = 1f;

	public bool vertexColorFlag;

	[Serializable]
	public struct PolygonVertexInfo
	{
		public PolygonVertexInfo(float length)
		{
			this.color = Color.white;
			this.length = 1f;
		}

		public Color color;

		[Range(0f, 1f)]
		public float length;
	}
}
