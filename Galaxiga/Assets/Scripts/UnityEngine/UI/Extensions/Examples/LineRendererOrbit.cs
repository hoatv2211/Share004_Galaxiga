using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions.Examples
{
	[RequireComponent(typeof(UILineRenderer))]
	public class LineRendererOrbit : MonoBehaviour
	{
		public float xAxis
		{
			get
			{
				return this._xAxis;
			}
			set
			{
				this._xAxis = value;
				this.GenerateOrbit();
			}
		}

		public float yAxis
		{
			get
			{
				return this._yAxis;
			}
			set
			{
				this._yAxis = value;
				this.GenerateOrbit();
			}
		}

		public int Steps
		{
			get
			{
				return this._steps;
			}
			set
			{
				this._steps = value;
				this.GenerateOrbit();
			}
		}

		private void Awake()
		{
			this.lr = base.GetComponent<UILineRenderer>();
			this.orbitGOrt = this.OrbitGO.GetComponent<RectTransform>();
			this.GenerateOrbit();
		}

		private void Update()
		{
			this.orbitTime = ((this.orbitTime <= (float)this._steps) ? (this.orbitTime + Time.deltaTime) : (this.orbitTime = 0f));
			this.orbitGOrt.localPosition = this.circle.Evaluate(this.orbitTime);
		}

		private void GenerateOrbit()
		{
			this.circle = new Circle(this._xAxis, this._yAxis, this._steps);
			List<Vector2> list = new List<Vector2>();
			for (int i = 0; i < this._steps; i++)
			{
				list.Add(this.circle.Evaluate((float)i));
			}
			list.Add(this.circle.Evaluate(0f));
			this.lr.Points = list.ToArray();
		}

		private void OnValidate()
		{
			if (this.lr != null)
			{
				this.GenerateOrbit();
			}
		}

		private UILineRenderer lr;

		private Circle circle;

		public GameObject OrbitGO;

		private RectTransform orbitGOrt;

		private float orbitTime;

		[SerializeField]
		private float _xAxis = 3f;

		[SerializeField]
		private float _yAxis = 3f;

		[SerializeField]
		private int _steps = 10;
	}
}
