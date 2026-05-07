using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRuby.ThunderAndLightning
{
	public class LightningSplineScript : LightningBoltPathScriptBase
	{
		private bool SourceChanged()
		{
			if (this.sourcePoints.Count != this.prevSourcePoints.Count)
			{
				return true;
			}
			for (int i = 0; i < this.sourcePoints.Count; i++)
			{
				if (this.sourcePoints[i] != this.prevSourcePoints[i])
				{
					return true;
				}
			}
			return false;
		}

		protected override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			base.Update();
		}

		public override void CreateLightningBolt(LightningBoltParameters parameters)
		{
			if (this.LightningPath == null)
			{
				return;
			}
			this.sourcePoints.Clear();
			try
			{
				foreach (GameObject gameObject in this.LightningPath)
				{
					if (gameObject != null)
					{
						this.sourcePoints.Add(gameObject.transform.position);
					}
				}
			}
			catch (NullReferenceException)
			{
				return;
			}
			if (this.sourcePoints.Count < 4)
			{
				UnityEngine.Debug.LogError("To create spline lightning, you need a lightning path with at least " + 4 + " points.");
			}
			else
			{
				int generations = Mathf.Clamp(this.Generations, 1, 5);
				parameters.Generations = generations;
				this.Generations = generations;
				if (this.previousGenerations != this.Generations || this.previousDistancePerSegment != this.DistancePerSegmentHint || this.SourceChanged())
				{
					this.previousGenerations = this.Generations;
					this.previousDistancePerSegment = this.DistancePerSegmentHint;
					parameters.Points = new List<Vector3>(this.sourcePoints.Count * this.Generations);
					LightningSplineScript.PopulateSpline(parameters.Points, this.sourcePoints, this.Generations, this.DistancePerSegmentHint, this.Camera);
					this.prevSourcePoints.Clear();
					this.prevSourcePoints.AddRange(this.sourcePoints);
					this.savedSplinePoints = parameters.Points;
				}
				else
				{
					parameters.Points = this.savedSplinePoints;
				}
				parameters.SmoothingFactor = (parameters.Points.Count - 1) / this.sourcePoints.Count;
				base.CreateLightningBolt(parameters);
			}
		}

		protected override LightningBoltParameters OnCreateParameters()
		{
			LightningBoltParameters orCreateParameters = LightningBoltParameters.GetOrCreateParameters();
			orCreateParameters.Generator = LightningGeneratorPath.PathGeneratorInstance;
			return orCreateParameters;
		}

		public void Trigger(List<Vector3> points, bool spline)
		{
			if (points.Count < 2)
			{
				return;
			}
			this.Generations = Mathf.Clamp(this.Generations, 1, 5);
			LightningBoltParameters lightningBoltParameters = base.CreateParameters();
			if (spline && points.Count > 3)
			{
				lightningBoltParameters.Points = new List<Vector3>(points.Count * this.Generations);
				LightningSplineScript.PopulateSpline(lightningBoltParameters.Points, points, this.Generations, this.DistancePerSegmentHint, this.Camera);
				lightningBoltParameters.SmoothingFactor = (lightningBoltParameters.Points.Count - 1) / points.Count;
			}
			else
			{
				lightningBoltParameters.Points = new List<Vector3>(points);
				lightningBoltParameters.SmoothingFactor = 1;
			}
			base.CreateLightningBolt(lightningBoltParameters);
			base.CreateLightningBoltsNow();
		}

		public static void PopulateSpline(List<Vector3> splinePoints, List<Vector3> sourcePoints, int generations, float distancePerSegmentHit, Camera camera)
		{
			splinePoints.Clear();
			PathGenerator.Is2D = (camera != null && camera.orthographic);
			if (distancePerSegmentHit > 0f)
			{
				PathGenerator.CreateSplineWithSegmentDistance(splinePoints, sourcePoints, distancePerSegmentHit / (float)generations, false);
			}
			else
			{
				PathGenerator.CreateSpline(splinePoints, sourcePoints, sourcePoints.Count * generations * generations, false);
			}
		}

		public const int MaxSplineGenerations = 5;

		[Header("Lightning Spline Properties")]
		[Tooltip("The distance hint for each spline segment. Set to <= 0 to use the generations to determine how many spline segments to use. If > 0, it will be divided by Generations before being applied. This value is a guideline and is approximate, and not uniform on the spline.")]
		public float DistancePerSegmentHint;

		private readonly List<Vector3> prevSourcePoints = new List<Vector3>(new Vector3[]
		{
			Vector3.zero
		});

		private readonly List<Vector3> sourcePoints = new List<Vector3>();

		private List<Vector3> savedSplinePoints;

		private int previousGenerations = -1;

		private float previousDistancePerSegment = -1f;
	}
}
