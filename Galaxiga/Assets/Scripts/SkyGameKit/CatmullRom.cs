using System;
using System.Collections.Generic;
using SWS;
using UnityEngine;

namespace SkyGameKit
{
	public class CatmullRom
	{
		public CatmullRom(BezierPathManager bezierPath)
		{
			this.bezierPath = bezierPath;
			this.points = null;
			this.Restart();
		}

		public CatmullRom(List<Vector2> points)
		{
			this.points = points;
			points.Add(points[points.Count - 1]);
			points.Insert(0, points[0]);
			this.bezierPath = null;
			this.Restart();
		}

		public float CurrentPercent { get; private set; }

		public void Restart()
		{
			this.CurrentPercent = 0f;
			this.currentPoint = 0;
			this.GoToWaypoint(this.currentPoint);
		}

		public bool GoToWaypoint(int index)
		{
			if (this.points != null)
			{
				if (this.points.Count < index + 4)
				{
					return false;
				}
				this.crd.Register(this.points[index], this.points[index + 1], this.points[index + 2], this.points[index + 3]);
			}
			else
			{
				if (this.bezierPath.bPoints.Count < index + 2)
				{
					return false;
				}
				this.bp0 = this.bezierPath.bPoints[index];
				this.bp1 = this.bezierPath.bPoints[index + 1];
				this.crd.Register(this.bp0.wp.position, this.bp0.cp[1].position, this.bp1.cp[0].position, this.bp1.wp.position);
			}
			this.currentPoint = index;
			if (this.OnWaypointReached != null)
			{
				this.OnWaypointReached(index);
			}
			return true;
		}

		public MoveNextResult MoveNext(float speed)
		{
			Vector2 point = this.crd.GetPoint(this.CurrentPercent);
			float num = this.crd.CalculateStep(speed);
			float num2 = 0f;
			while (num2 < speed)
			{
				float tStart = this.CurrentPercent;
				float num3 = this.CurrentPercent + num;
				if (num3 > 1f)
				{
					num2 += this.crd.GetStraightLength(tStart, 1f);
					if (!this.GoToWaypoint(this.currentPoint + 1))
					{
						if (this.OnReachedEnd != null)
						{
							this.OnReachedEnd();
						}
						if (this.OnWaypointReached != null)
						{
							this.OnWaypointReached(this.currentPoint + 1);
						}
						this.CurrentPercent = 1f;
						break;
					}
					num = this.crd.CalculateStep(speed);
					tStart = 0f;
					num3 -= 1f;
				}
				num2 += this.crd.GetStraightLength(tStart, num3);
				this.CurrentPercent = num3;
			}
			return new MoveNextResult(point, this.crd.GetPoint(this.CurrentPercent));
		}

		public Action<int> OnWaypointReached;

		public Action OnReachedEnd;

		private CatmullRomDecoder crd;

		private BezierPoint bp0;

		private BezierPoint bp1;

		private BezierPathManager bezierPath;

		private List<Vector2> points;

		private int currentPoint;
	}
}
