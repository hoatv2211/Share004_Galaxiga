using System;
using UnityEngine;

namespace SkyGameKit
{
	public struct LaserHitInfo
	{
		public void SetValue(float deltaTime, Vector2 point, Collider2D collider)
		{
			this.deltaTime = deltaTime;
			this.point = point;
			this.collider = collider;
		}

		public float deltaTime;

		public Vector2 point;

		public Collider2D collider;
	}
}
