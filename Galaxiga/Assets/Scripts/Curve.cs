using System;
using UnityEngine;

public static class Curve
{
	public static Vector3 CalculateBezier(Vector3 p0, Vector3 p1, Vector3 cp0, Vector3 cp1, float t)
	{
		float num = 1f - t;
		return num * num * num * p0 + 3f * num * num * t * cp0 + 3f * num * t * t * cp1 + t * t * t * p1;
	}

	public static Vector3 CalculateBezier(Vector3 p0, Vector3 p1, Vector3 cp0, float t)
	{
		float num = 1f - t;
		return num * num * p0 + 2f * num * t * cp0 + t * t * p1;
	}

	public static Vector3 CalculateBezierDerivative(Vector3 p0, Vector3 p1, Vector3 cp0, Vector3 cp1, float t)
	{
		float num = 1f - t;
		return 3f * num * num * (cp0 - p0) + 6f * num * t * (cp1 - cp0) + 3f * t * t * (p1 - cp1);
	}
}
