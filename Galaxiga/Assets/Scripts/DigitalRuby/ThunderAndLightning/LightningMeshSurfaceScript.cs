using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRuby.ThunderAndLightning
{
	public class LightningMeshSurfaceScript : LightningBoltPrefabScriptBase
	{
		private void CheckMesh()
		{
			if (this.MeshFilter == null || this.MeshFilter.sharedMesh == null)
			{
				this.meshHelper = null;
			}
			else if (this.MeshFilter.sharedMesh != this.previousMesh)
			{
				this.previousMesh = this.MeshFilter.sharedMesh;
				this.meshHelper = new MeshHelper(this.previousMesh);
			}
		}

		protected override LightningBoltParameters OnCreateParameters()
		{
			LightningBoltParameters lightningBoltParameters = base.OnCreateParameters();
			lightningBoltParameters.Generator = LightningGeneratorPath.PathGeneratorInstance;
			return lightningBoltParameters;
		}

		protected virtual void PopulateSourcePoints(List<Vector3> points)
		{
			if (this.meshHelper != null)
			{
				this.CreateRandomLightningPath(this.sourcePoints);
			}
		}

		public void CreateRandomLightningPath(List<Vector3> points)
		{
			if (this.meshHelper == null)
			{
				return;
			}
			RaycastHit raycastHit = default(RaycastHit);
			this.maximumPathDistanceSquared = this.MaximumPathDistance * this.MaximumPathDistance;
			int num;
			this.meshHelper.GenerateRandomPoint(ref raycastHit, out num);
			raycastHit.distance = UnityEngine.Random.Range(this.MeshOffsetRange.Minimum, this.MeshOffsetRange.Maximum);
			Vector3 vector = raycastHit.point + raycastHit.normal * raycastHit.distance;
			float num2 = UnityEngine.Random.Range(this.MinimumPathDistanceRange.Minimum, this.MinimumPathDistanceRange.Maximum);
			num2 *= num2;
			this.sourcePoints.Add(this.MeshFilter.transform.TransformPoint(vector));
			int num3 = (UnityEngine.Random.Range(0, 1) != 1) ? -3 : 3;
			int num4 = UnityEngine.Random.Range(this.PathLengthCount.Minimum, this.PathLengthCount.Maximum);
			while (num4 != 0)
			{
				num += num3;
				if (num >= 0 && num < this.meshHelper.Triangles.Length)
				{
					this.meshHelper.GetRaycastFromTriangleIndex(num, ref raycastHit);
					raycastHit.distance = UnityEngine.Random.Range(this.MeshOffsetRange.Minimum, this.MeshOffsetRange.Maximum);
					Vector3 vector2 = raycastHit.point + raycastHit.normal * raycastHit.distance;
					float sqrMagnitude = (vector2 - vector).sqrMagnitude;
					if (sqrMagnitude > this.maximumPathDistanceSquared)
					{
						break;
					}
					if (sqrMagnitude >= num2)
					{
						vector = vector2;
						this.sourcePoints.Add(this.MeshFilter.transform.TransformPoint(vector2));
						num4--;
						num2 = UnityEngine.Random.Range(this.MinimumPathDistanceRange.Minimum, this.MinimumPathDistanceRange.Maximum);
						num2 *= num2;
					}
				}
				else
				{
					num3 = -num3;
					num += num3;
					num4--;
				}
			}
		}

		protected override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			this.CheckMesh();
			base.Update();
		}

		public override void CreateLightningBolt(LightningBoltParameters parameters)
		{
			if (this.meshHelper == null)
			{
				return;
			}
			int generations = Mathf.Clamp(this.Generations, 1, 5);
			parameters.Generations = generations;
			this.Generations = generations;
			this.sourcePoints.Clear();
			this.PopulateSourcePoints(this.sourcePoints);
			if (this.sourcePoints.Count > 1)
			{
				if (this.Spline && this.sourcePoints.Count > 3)
				{
					parameters.Points = new List<Vector3>(this.sourcePoints.Count * this.Generations);
					LightningSplineScript.PopulateSpline(parameters.Points, this.sourcePoints, this.Generations, this.DistancePerSegmentHint, this.Camera);
					parameters.SmoothingFactor = (parameters.Points.Count - 1) / this.sourcePoints.Count;
				}
				else
				{
					parameters.Points = new List<Vector3>(this.sourcePoints);
					parameters.SmoothingFactor = 1;
				}
				base.CreateLightningBolt(parameters);
			}
		}

		[Header("Lightning Mesh Properties")]
		[Tooltip("The mesh filter. You must assign a mesh filter in order to create lightning on the mesh.")]
		public MeshFilter MeshFilter;

		[Tooltip("The mesh collider. This is used to get random points on the mesh.")]
		public Collider MeshCollider;

		[SingleLine("Random range that the point will offset from the mesh, using the normal of the chosen point to offset")]
		public RangeOfFloats MeshOffsetRange = new RangeOfFloats
		{
			Minimum = 0.5f,
			Maximum = 1f
		};

		[Header("Lightning Path Properties")]
		[SingleLine("Range for points in the lightning path")]
		public RangeOfIntegers PathLengthCount = new RangeOfIntegers
		{
			Minimum = 3,
			Maximum = 6
		};

		[SingleLine("Range for minimum distance between points in the lightning path")]
		public RangeOfFloats MinimumPathDistanceRange = new RangeOfFloats
		{
			Minimum = 0.5f,
			Maximum = 1f
		};

		[Tooltip("The maximum distance between mesh points. When walking the mesh, if a point is greater than this, the path direction is reversed. This tries to avoid paths crossing between mesh points that are not actually physically touching.")]
		public float MaximumPathDistance = 2f;

		private float maximumPathDistanceSquared;

		[Tooltip("Whether to use spline interpolation between the path points. Paths must be at least 4 points long to be splined.")]
		public bool Spline;

		[Tooltip("For spline. the distance hint for each spline segment. Set to <= 0 to use the generations to determine how many spline segments to use. If > 0, it will be divided by Generations before being applied. This value is a guideline and is approximate, and not uniform on the spline.")]
		public float DistancePerSegmentHint;

		private readonly List<Vector3> sourcePoints = new List<Vector3>();

		private Mesh previousMesh;

		private MeshHelper meshHelper;
	}
}
