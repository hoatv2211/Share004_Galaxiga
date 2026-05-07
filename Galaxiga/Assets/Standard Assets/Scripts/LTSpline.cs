using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LTSpline
{
	public LTSpline(Vector3[] pts)
	{
		this.init(pts, true);
	}

	public LTSpline(Vector3[] pts, bool constantSpeed)
	{
		this.constantSpeed = constantSpeed;
		this.init(pts, constantSpeed);
	}

	private void init(Vector3[] pts, bool constantSpeed)
	{
		if (pts.Length < 4)
		{
			LeanTween.logError("LeanTween - When passing values for a spline path, you must pass four or more values!");
			return;
		}
		this.pts = new Vector3[pts.Length];
		Array.Copy(pts, this.pts, pts.Length);
		this.numSections = pts.Length - 3;
		float num = float.PositiveInfinity;
		Vector3 vector = this.pts[1];
		float num2 = 0f;
		for (int i = 1; i < this.pts.Length - 1; i++)
		{
			float num3 = Vector3.Distance(this.pts[i], vector);
			if (num3 < num)
			{
				num = num3;
			}
			num2 += num3;
		}
		if (constantSpeed)
		{
			num = num2 / (float)(this.numSections * LTSpline.SUBLINE_COUNT);
			float num4 = num / (float)LTSpline.SUBLINE_COUNT;
			int num5 = (int)Mathf.Ceil(num2 / num4) * LTSpline.DISTANCE_COUNT;
			if (num5 <= 1)
			{
				num5 = 2;
			}
			this.ptsAdj = new Vector3[num5];
			vector = this.interp(0f);
			int num6 = 1;
			this.ptsAdj[0] = vector;
			this.distance = 0f;
			for (int j = 0; j < num5 + 1; j++)
			{
				float num7 = (float)j / (float)num5;
				Vector3 vector2 = this.interp(num7);
				float num8 = Vector3.Distance(vector2, vector);
				if (num8 >= num4 || num7 >= 1f)
				{
					this.ptsAdj[num6] = vector2;
					this.distance += num8;
					vector = vector2;
					num6++;
				}
			}
			this.ptsAdjLength = num6;
		}
	}

	public Vector3 map(float u)
	{
		if (u >= 1f)
		{
			return this.pts[this.pts.Length - 2];
		}
		float num = u * (float)(this.ptsAdjLength - 1);
		int num2 = (int)Mathf.Floor(num);
		int num3 = (int)Mathf.Ceil(num);
		if (num2 < 0)
		{
			num2 = 0;
		}
		Vector3 vector = this.ptsAdj[num2];
		Vector3 a = this.ptsAdj[num3];
		float d = num - (float)num2;
		return vector + (a - vector) * d;
	}

	public Vector3 interp(float t)
	{
		this.currPt = Mathf.Min(Mathf.FloorToInt(t * (float)this.numSections), this.numSections - 1);
		float num = t * (float)this.numSections - (float)this.currPt;
		Vector3 a = this.pts[this.currPt];
		Vector3 a2 = this.pts[this.currPt + 1];
		Vector3 vector = this.pts[this.currPt + 2];
		Vector3 b = this.pts[this.currPt + 3];
		return 0.5f * ((-a + 3f * a2 - 3f * vector + b) * (num * num * num) + (2f * a - 5f * a2 + 4f * vector - b) * (num * num) + (-a + vector) * num + 2f * a2);
	}

	public float ratioAtPoint(Vector3 pt)
	{
		float num = float.MaxValue;
		int num2 = 0;
		for (int i = 0; i < this.ptsAdjLength; i++)
		{
			float num3 = Vector3.Distance(pt, this.ptsAdj[i]);
			if (num3 < num)
			{
				num = num3;
				num2 = i;
			}
		}
		return (float)num2 / (float)(this.ptsAdjLength - 1);
	}

	public Vector3 point(float ratio)
	{
		float num = (ratio <= 1f) ? ratio : 1f;
		return (!this.constantSpeed) ? this.interp(num) : this.map(num);
	}

	public void place2d(Transform transform, float ratio)
	{
		transform.position = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			Vector3 vector = this.point(ratio) - transform.position;
			float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.eulerAngles = new Vector3(0f, 0f, z);
		}
	}

	public void placeLocal2d(Transform transform, float ratio)
	{
		Transform parent = transform.parent;
		if (parent == null)
		{
			this.place2d(transform, ratio);
			return;
		}
		transform.localPosition = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			Vector3 a = this.point(ratio);
			Vector3 vector = a - transform.localPosition;
			float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			transform.localEulerAngles = new Vector3(0f, 0f, z);
		}
	}

	public void place(Transform transform, float ratio)
	{
		this.place(transform, ratio, Vector3.up);
	}

	public void place(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.position = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(this.point(ratio), worldUp);
		}
	}

	public void placeLocal(Transform transform, float ratio)
	{
		this.placeLocal(transform, ratio, Vector3.up);
	}

	public void placeLocal(Transform transform, float ratio, Vector3 worldUp)
	{
		transform.localPosition = this.point(ratio);
		ratio += 0.001f;
		if (ratio <= 1f)
		{
			transform.LookAt(transform.parent.TransformPoint(this.point(ratio)), worldUp);
		}
	}

	public void gizmoDraw(float t = -1f)
	{
		if (this.ptsAdj == null || this.ptsAdj.Length <= 0)
		{
			return;
		}
		Vector3 from = this.ptsAdj[0];
		for (int i = 0; i < this.ptsAdjLength; i++)
		{
			Vector3 vector = this.ptsAdj[i];
			Gizmos.DrawLine(from, vector);
			from = vector;
		}
	}

	public void drawGizmo(Color color)
	{
		if (this.ptsAdjLength >= 4)
		{
			Vector3 from = this.ptsAdj[0];
			Color color2 = Gizmos.color;
			Gizmos.color = color;
			for (int i = 0; i < this.ptsAdjLength; i++)
			{
				Vector3 vector = this.ptsAdj[i];
				Gizmos.DrawLine(from, vector);
				from = vector;
			}
			Gizmos.color = color2;
		}
	}

	public static void drawGizmo(Transform[] arr, Color color)
	{
		if (arr.Length >= 4)
		{
			Vector3[] array = new Vector3[arr.Length];
			for (int i = 0; i < arr.Length; i++)
			{
				array[i] = arr[i].position;
			}
			LTSpline ltspline = new LTSpline(array);
			Vector3 from = ltspline.ptsAdj[0];
			Color color2 = Gizmos.color;
			Gizmos.color = color;
			for (int j = 0; j < ltspline.ptsAdjLength; j++)
			{
				Vector3 vector = ltspline.ptsAdj[j];
				Gizmos.DrawLine(from, vector);
				from = vector;
			}
			Gizmos.color = color2;
		}
	}

	public static void drawLine(Transform[] arr, float width, Color color)
	{
		if (arr.Length >= 4)
		{
		}
	}

	public void drawLinesGLLines(Material outlineMaterial, Color color, float width)
	{
		GL.PushMatrix();
		outlineMaterial.SetPass(0);
		GL.LoadPixelMatrix();
		GL.Begin(1);
		GL.Color(color);
		if (this.constantSpeed)
		{
			if (this.ptsAdjLength >= 4)
			{
				Vector3 v = this.ptsAdj[0];
				for (int i = 0; i < this.ptsAdjLength; i++)
				{
					Vector3 vector = this.ptsAdj[i];
					GL.Vertex(v);
					GL.Vertex(vector);
					v = vector;
				}
			}
		}
		else if (this.pts.Length >= 4)
		{
			Vector3 v2 = this.pts[0];
			float num = 1f / ((float)this.pts.Length * 10f);
			for (float num2 = 0f; num2 < 1f; num2 += num)
			{
				float t = num2 / 1f;
				Vector3 vector2 = this.interp(t);
				GL.Vertex(v2);
				GL.Vertex(vector2);
				v2 = vector2;
			}
		}
		GL.End();
		GL.PopMatrix();
	}

	public Vector3[] generateVectors()
	{
		if (this.pts.Length >= 4)
		{
			List<Vector3> list = new List<Vector3>();
			Vector3 item = this.pts[0];
			list.Add(item);
			float num = 1f / ((float)this.pts.Length * 10f);
			for (float num2 = 0f; num2 < 1f; num2 += num)
			{
				float t = num2 / 1f;
				Vector3 item2 = this.interp(t);
				list.Add(item2);
			}
			list.ToArray();
		}
		return null;
	}

	public static int DISTANCE_COUNT = 3;

	public static int SUBLINE_COUNT = 20;

	public float distance;

	public bool constantSpeed = true;

	public Vector3[] pts;

	[NonSerialized]
	public Vector3[] ptsAdj;

	public int ptsAdjLength;

	public bool orientToPath;

	public bool orientToPath2d;

	private int numSections;

	private int currPt;
}
