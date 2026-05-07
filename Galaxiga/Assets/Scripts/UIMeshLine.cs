using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMeshLine : MaskableGraphic, IMeshModifier, ICanvasRaycastFilter
{
	public float lineLength
	{
		get
		{
			float num = 0f;
			for (int i = 0; i < this.m_points.Count - 1; i++)
			{
				num += Vector2.Distance(this.m_points[i].point, this.m_points[i + 1].point);
			}
			return num;
		}
	}

	public float lengthRatio
	{
		get
		{
			return this.m_lengthRatio;
		}
		set
		{
			this.m_lengthRatio = value;
			this.UpdateGeometry();
		}
	}

	public float startRatio
	{
		get
		{
			return this.m_startRatio;
		}
		set
		{
			this.m_startRatio = value;
			this.UpdateGeometry();
		}
	}

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
		UIVertex[] prvLineVert = null;
		for (int i = 0; i < this.m_points.Count - 1; i++)
		{
			if (this.GetLength(i + 1) / this.lineLength > this.m_startRatio)
			{
				if (this.GetLength(i) / this.lineLength > this.m_lengthRatio)
				{
					break;
				}
				prvLineVert = this.DrawLine(i, vh, prvLineVert);
			}
		}
	}

	private UIVertex[] DrawLine(int index, VertexHelper vh, UIVertex[] prvLineVert = null)
	{
		UIVertex[] array = null;
		float lineLength = this.lineLength;
		float num = this.GetLength(index) / lineLength;
		float num2 = this.GetLength(index + 1) / lineLength;
		float num3 = 0f;
		float num4 = num;
		int nextCurveDivideCount = this.m_points[index].nextCurveDivideCount;
		for (int i = 0; i < nextCurveDivideCount; i++)
		{
			Vector3 v = this.EvaluatePoint(index, 1f / (float)nextCurveDivideCount * (float)i);
			Vector3 v2 = this.EvaluatePoint(index, 1f / (float)nextCurveDivideCount * (float)(i + 1));
			num3 += Vector2.Distance(v, v2);
		}
		float num5 = 0f;
		float num6 = 0f;
		bool flag = false;
		if (this.startRatio > num && this.startRatio < num2)
		{
			num6 = (this.startRatio - num) / (num2 - num);
			num5 = num6 / (1f / (float)nextCurveDivideCount);
			flag = true;
		}
		for (int j = (int)num5; j < nextCurveDivideCount; j++)
		{
			float t = 1f / (float)nextCurveDivideCount * (float)j;
			if (flag)
			{
				flag = false;
				t = num6;
			}
			float t2 = 1f / (float)nextCurveDivideCount * (float)(j + 1);
			Vector3 vector = this.EvaluatePoint(index, t);
			Vector3 vector2 = this.EvaluatePoint(index, t2);
			float num7 = (!this.eachWidth) ? this.m_width : this.EvaluateWidth(index, t);
			float num8 = (!this.eachWidth) ? this.m_width : this.EvaluateWidth(index, t2);
			float a = (!this.useAngle) ? 0f : Mathf.Lerp(this.m_points[index].angle, this.m_points[index + 1].angle, t);
			float a2 = (!this.useAngle) ? 0f : Mathf.Lerp(this.m_points[index].angle, this.m_points[index + 1].angle, t2);
			Color color = (!this.useGradient) ? this.color : this.gradient.Evaluate(num4);
			float num9 = Vector2.Distance(vector, vector2) / num3 * (num2 - num);
			num4 += num9;
			Color color2 = (!this.useGradient) ? this.color : this.gradient.Evaluate(num4);
			bool flag2 = false;
			if (num4 > this.m_lengthRatio)
			{
				num4 -= num9;
				float num10 = lineLength * this.m_lengthRatio;
				vector2 = vector + (vector2 - vector).normalized * (num10 - lineLength * num4);
				flag2 = true;
			}
			if (this.roundEdge && index == 0 && j == 0)
			{
				this.DrawRoundEdge(vh, vector, vector2, color, num7);
			}
			if (this.roundEdge && ((index == this.m_points.Count - 2 && j == nextCurveDivideCount - 1) || flag2))
			{
				this.DrawRoundEdge(vh, vector2, vector, color2, num8);
			}
			UIVertex[] array2 = this.MakeQuad(vh, vector, vector2, color, color2, a, a2, array, num7, num8);
			if (this.fillLineJoint && prvLineVert != null)
			{
				this.FillJoint(vh, array2[0], array2[1], prvLineVert, color, -1f);
				prvLineVert = null;
			}
			if (flag2)
			{
				break;
			}
			if (array == null)
			{
				array = new UIVertex[2];
			}
			array[0] = array2[3];
			array[1] = array2[2];
		}
		return array;
	}

	private void FillJoint(VertexHelper vh, UIVertex vp0, UIVertex vp1, UIVertex[] prvLineVert, Color color, float width = -1f)
	{
		Vector3 rhs = vp1.position - vp0.position;
		Vector3 lhs = prvLineVert[1].position - prvLineVert[0].position;
		Vector3 lhs2 = Vector3.Cross(lhs, new Vector3(0f, 0f, 1f));
		Vector3 vector = (vp0.position + vp1.position) / 2f;
		Vector3 position;
		Vector3 position2;
		if (Vector3.Dot(lhs2, rhs) > 0f)
		{
			position = vp1.position;
			position2 = prvLineVert[1].position;
		}
		else
		{
			position = vp0.position;
			position2 = prvLineVert[0].position;
		}
		if (width < 0f)
		{
			width = this.m_width;
		}
		Vector3 cp = (position + position2 - vector * 2f).normalized * width * this.fillRatio + vector;
		float num = Vector3.Angle(position - vector, position2 - vector);
		int currentVertCount = vh.currentVertCount;
		int num2 = (int)(num / this.fillDivideAngle);
		if (num2 == 0)
		{
			num2 = 1;
		}
		float num3 = 1f / (float)num2;
		vh.AddVert(vector, color, Vector2.one * 0.5f);
		vh.AddVert(position, color, Vector2.zero);
		for (int i = 0; i < num2; i++)
		{
			vh.AddVert(Curve.CalculateBezier(position, position2, cp, num3 * (float)(i + 1)), color, Vector2.zero);
			vh.AddTriangle(currentVertCount + 2 + i, currentVertCount, currentVertCount + 1 + i);
		}
	}

	private UIVertex[] MakeQuad(VertexHelper vh, Vector3 p0, Vector3 p1, Color c0, Color c1, float a0, float a1, UIVertex[] prvVert = null, float w0 = -1f, float w1 = -1f)
	{
		Vector3 lhs = p1 - p0;
		Vector3 vector = Vector3.Cross(lhs, new Vector3(0f, 0f, 1f));
		vector.Normalize();
		Vector3 a2 = (!this.useAngle) ? vector : (Quaternion.Euler(0f, 0f, a0) * vector);
		Vector3 a3 = (!this.useAngle) ? vector : (Quaternion.Euler(0f, 0f, a1) * vector);
		UIVertex[] array = new UIVertex[4];
		if (w0 < 0f)
		{
			w0 = this.m_width;
		}
		if (w1 < 0f)
		{
			w1 = this.m_width;
		}
		if (prvVert != null)
		{
			array[0] = prvVert[0];
			array[1] = prvVert[1];
		}
		else
		{
			array[0].position = p0 + a2 * w0 * 0.5f;
			array[1].position = p0 - a2 * w0 * 0.5f;
		}
		array[2].position = p1 - a3 * w1 * 0.5f;
		array[3].position = p1 + a3 * w1 * 0.5f;
		array[0].uv0 = new Vector2(0f, 0f);
		array[1].uv0 = new Vector2(1f, 0f);
		array[2].uv0 = new Vector2(1f, 1f);
		array[3].uv0 = new Vector2(0f, 1f);
		array[0].color = c0;
		array[1].color = c0;
		array[2].color = c1;
		array[3].color = c1;
		vh.AddUIVertexQuad(array);
		return array;
	}

	private Vector2 EvaluatePoint(LinePoint p0, LinePoint p1, float t)
	{
		if (p0.isNextCurve && !p1.isPrvCurve)
		{
			return Curve.CalculateBezier(p0.point, p1.point, p0.nextCurvePoint, t);
		}
		if (!p0.isNextCurve && p1.isPrvCurve)
		{
			return Curve.CalculateBezier(p0.point, p1.point, p1.prvCurvePoint, t);
		}
		if (p0.isNextCurve && p1.isPrvCurve)
		{
			return Curve.CalculateBezier(p0.point, p1.point, p0.nextCurvePoint, p1.prvCurvePoint, t);
		}
		return Vector2.Lerp(p0.point, p1.point, t);
	}

	private Vector2 EvaluatePoint(int index, float t)
	{
		return this.EvaluatePoint(this.m_points[index], this.m_points[index + 1], t);
	}

	private float EvaluateWidth(int index, float t)
	{
		return Mathf.Lerp(this.m_points[index].width, this.m_points[index + 1].width, t);
	}

	private Vector2 GetDerivative(LinePoint p0, LinePoint p1, float t)
	{
		if (p0.isNextCurve || p1.isPrvCurve)
		{
			return Curve.CalculateBezierDerivative(p0.point, p1.point, p0.nextCurvePoint, p1.prvCurvePoint, t);
		}
		return (p1.point - p0.point).normalized;
	}

	private float GetLength(int index)
	{
		if (index <= 0)
		{
			return 0f;
		}
		float num = 0f;
		for (int i = 0; i < index; i++)
		{
			num += Vector2.Distance(this.m_points[i].point, this.m_points[i + 1].point);
		}
		return num;
	}

	public LinePoint GetPointInfo(int index)
	{
		return this.m_points[index];
	}

	public void SetPointInfo(int index, LinePoint data)
	{
		this.m_points[index] = data;
		this.SetVerticesDirty();
	}

	public void SetPointPosition(int index, Vector2 position)
	{
		LinePoint value = this.m_points[index];
		value.point = position;
		this.m_points[index] = value;
		this.SetVerticesDirty();
	}

	public void AddPoint(LinePoint data)
	{
		this.m_points.Add(data);
		this.SetVerticesDirty();
	}

	public int GetPointCount()
	{
		return this.m_points.Count;
	}

	public Vector2 GetCurvePosition(int index, int curveIndex)
	{
		if (curveIndex >= this.m_points[index].nextCurveDivideCount)
		{
			throw new Exception(string.Concat(new object[]
			{
				"index Error index : ",
				curveIndex,
				" maxValue : ",
				this.m_points[index].nextCurveDivideCount
			}));
		}
		return base.transform.TransformPoint(this.EvaluatePoint(this.m_points[index], this.m_points[index + 1], 1f / (float)this.m_points[index].nextCurveDivideCount * (float)curveIndex));
	}

	public bool IsCurve(int index)
	{
		if (this.m_points.Count - 1 <= index)
		{
			throw new Exception(string.Concat(new object[]
			{
				"인덱스가 작음 index:",
				index,
				" maxValue : ",
				this.m_points.Count - 1
			}));
		}
		return this.m_points[index].isNextCurve || this.m_points[index + 1].isPrvCurve;
	}

	public void DrawRoundEdge(VertexHelper vh, Vector2 p0, Vector2 p1, Color color, float width = -1f)
	{
		if (width < 0f)
		{
			width = this.m_width;
		}
		Vector2 vector = Vector3.Cross(p0 - p1, new Vector3(0f, 0f, 1f));
		vector.Normalize();
		vector = vector * width / 2f;
		Vector2 a = (p0 - p1).normalized * width / 2f;
		int num = this.roundEdgePolygonCount;
		int currentVertCount = vh.currentVertCount;
		float num2 = 3.14159274f / (float)(num - 1);
		vh.AddVert(p0, color, Vector2.one * 0.5f);
		vh.AddVert(p0 + vector, color, Vector2.zero);
		for (int i = 0; i < num; i++)
		{
			vh.AddVert(p0 + Mathf.Cos(num2 * (float)i) * vector + Mathf.Sin(num2 * (float)i) * a, color, Vector2.zero);
			vh.AddTriangle(currentVertCount, currentVertCount + 2 + i, currentVertCount + 1 + i);
		}
	}

	bool ICanvasRaycastFilter.IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
	{
		if (!this.useRayCastFilter)
		{
			return true;
		}
		if (base.GetComponentInParent<Canvas>().renderMode != RenderMode.ScreenSpaceOverlay)
		{
			UnityEngine.Debug.LogWarning("this filter only implement at overlaymode.");
			return true;
		}
		for (int i = 0; i < this.GetPointCount() - 1; i++)
		{
			if (this.CheckPointOnLine(this.GetPointInfo(i), this.GetPointInfo(i + 1), sp))
			{
				return true;
			}
		}
		return false;
	}

	private bool CheckPointOnLine(LinePoint linePoint1, LinePoint linePoint2, Vector2 sp)
	{
		Vector3 p = base.transform.TransformPoint(linePoint1.point);
		Vector3 p2 = base.transform.TransformPoint(linePoint2.point);
		Vector3 c = base.transform.TransformPoint(linePoint1.nextCurvePoint);
		Vector3 c2 = base.transform.TransformPoint(linePoint2.prvCurvePoint);
		if (!linePoint1.isNextCurve && !linePoint2.isPrvCurve)
		{
			return this.CheckPointOnStraightLine(p, p2, sp);
		}
		return this.CheckPointOnBezierCurve(p, c, c2, p2, sp);
	}

	private bool CheckPointOnBezierCurve(Vector3 p0, Vector3 c0, Vector3 c1, Vector3 p1, Vector2 sp)
	{
		float t = this.CalculateMinT(p0, c0, c1, p1, sp);
		return Vector3.Distance(sp, Curve.CalculateBezier(p0, p1, c0, c1, t)) < this.m_width / 2f;
	}

	private float CalculateMinT(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 sp)
	{
		float result = 0.5f;
		float num = float.MaxValue;
		for (float num2 = 0f; num2 < 1f; num2 += 0.02f)
		{
			Vector3 vector = Curve.CalculateBezier(p0, p3, p1, p2, num2);
			float num3 = (vector.x - sp.x) * (vector.x - sp.x) + (vector.y - sp.y) * (vector.y - sp.y);
			if (num3 < num)
			{
				num = num3;
				result = num2;
			}
		}
		return result;
	}

	private bool CheckPointOnStraightLine(Vector3 p0, Vector3 p1, Vector3 sp)
	{
		Vector3 onNormal = p1 - p0;
		Vector3 vector = sp - p0;
		Vector3 b = Vector3.Project(vector, onNormal);
		if (b.normalized != onNormal.normalized || b.magnitude > onNormal.magnitude)
		{
			return false;
		}
		float num = Vector3.Distance(p0 + b, sp);
		return num < this.m_width / 2f;
	}

	[SerializeField]
	private List<LinePoint> m_points = new List<LinePoint>();

	[SerializeField]
	private float m_width = 10f;

	public bool eachWidth;

	public bool useRayCastFilter;

	public bool useAngle;

	public bool useGradient;

	public Gradient gradient;

	public bool fillLineJoint;

	public float fillDivideAngle = 25f;

	public float fillRatio = 1f;

	public bool roundEdge;

	public int roundEdgePolygonCount = 5;

	[Range(0f, 1f)]
	[Header("0일땐 안그림 1일때 전부그림")]
	[SerializeField]
	private float m_lengthRatio = 1f;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_startRatio;
}
