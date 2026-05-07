using System;
using UnityEngine;

namespace SkyGameKit
{
	public struct CatmullRomDecoder
	{
		public float PredictionLength { get; private set; }

		public void Register(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
		{
			this.p0 = p0;
			this.p1 = p1;
			this.p2 = p2;
			this.p3 = p3;
			this.PredictionLength = this.GetLength(0f, 1f, 10);
		}

		public Vector2 GetPoint(float t)
		{
			float num = 1f - t;
			float num2 = num * num * num;
			float num3 = 3f * t * num * num;
			float num4 = 3f * t * t * num;
			float num5 = t * t * t;
			float num6 = this.p0.x * num2;
			num6 += this.p1.x * num3;
			num6 += this.p2.x * num4;
			num6 += this.p3.x * num5;
			float num7 = this.p0.y * num2;
			num7 += this.p1.y * num3;
			num7 += this.p2.y * num4;
			num7 += this.p3.y * num5;
			return new Vector2(num6, num7);
		}

		public float GetStraightLength(float tStart, float tEnd)
		{
			return Vector2.Distance(this.GetPoint(tStart), this.GetPoint(tEnd));
		}

		public float GetLength(float tStart, float tEnd, int resolution = 10)
		{
			float num = (tEnd - tStart) / (float)resolution;
			float num2 = 0f;
			for (int i = 0; i < resolution; i++)
			{
				num2 += this.GetStraightLength(tStart + num * (float)i, tStart + num * (float)(i + 1));
			}
			return num2;
		}

		public float CalculateStep(float speed)
		{
			return speed / (this.PredictionLength * 10f);
		}

		private const int defaultResolution = 10;

		private Vector2 p0;

		private Vector2 p1;

		private Vector2 p2;

		private Vector2 p3;
	}
}
