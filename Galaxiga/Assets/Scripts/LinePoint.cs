using System;
using UnityEngine;

[Serializable]
public struct LinePoint
{
	public LinePoint(Vector3 p)
	{
		this.point = p;
		this.isNextCurve = false;
		this.isPrvCurve = false;
		this.nextCurveOffset = Vector3.zero;
		this.prvCurveOffset = Vector3.zero;
		this.nextCurveDivideCount = 10;
		this.width = 10f;
		this.angle = 0f;
	}

	public Vector2 nextCurvePoint
	{
		get
		{
			return this.nextCurveOffset + this.point;
		}
		set
		{
			this.nextCurveOffset = value - this.point;
		}
	}

	public Vector2 prvCurvePoint
	{
		get
		{
			return this.prvCurveOffset + this.point;
		}
		set
		{
			this.prvCurveOffset = value - this.point;
		}
	}

	public Vector2 point;

	public bool isNextCurve;

	public Vector2 nextCurveOffset;

	public bool isPrvCurve;

	public Vector2 prvCurveOffset;

	[Range(1f, 100f)]
	public int nextCurveDivideCount;

	[Range(0f, 200f)]
	public float width;

	public float angle;
}
