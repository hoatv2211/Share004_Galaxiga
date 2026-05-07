using System;

namespace UnityEngine.UI.Extensions
{
	public class Circle
	{
		public Circle(float radius)
		{
			this.xAxis = radius;
			this.yAxis = radius;
			this.steps = 1;
		}

		public Circle(float radius, int steps)
		{
			this.xAxis = radius;
			this.yAxis = radius;
			this.steps = steps;
		}

		public Circle(float xAxis, float yAxis)
		{
			this.xAxis = xAxis;
			this.yAxis = yAxis;
			this.steps = 10;
		}

		public Circle(float xAxis, float yAxis, int steps)
		{
			this.xAxis = xAxis;
			this.yAxis = yAxis;
			this.steps = steps;
		}

		public float X
		{
			get
			{
				return this.xAxis;
			}
			set
			{
				this.xAxis = value;
			}
		}

		public float Y
		{
			get
			{
				return this.yAxis;
			}
			set
			{
				this.yAxis = value;
			}
		}

		public int Steps
		{
			get
			{
				return this.steps;
			}
			set
			{
				this.steps = value;
			}
		}

		public Vector2 Evaluate(float t)
		{
			float num = 360f / (float)this.steps;
			float f = 0.0174532924f * num * t;
			float x = Mathf.Sin(f) * this.xAxis;
			float y = Mathf.Cos(f) * this.yAxis;
			return new Vector2(x, y);
		}

		public void Evaluate(float t, out Vector2 eval)
		{
			float num = 360f / (float)this.steps;
			float f = 0.0174532924f * num * t;
			eval.x = Mathf.Sin(f) * this.xAxis;
			eval.y = Mathf.Cos(f) * this.yAxis;
		}

		[SerializeField]
		private float xAxis;

		[SerializeField]
		private float yAxis;

		[SerializeField]
		private int steps;
	}
}
