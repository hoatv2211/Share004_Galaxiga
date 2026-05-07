using System;
using UnityEngine;

namespace SkyGameKit
{
	[Serializable]
	public class PointOfShape
	{
		public PointOfShape(Vector3 position, int enemyType, bool hasAction)
		{
			GameObject gameObject = new GameObject("Point");
			this.transformPoint = gameObject.transform;
			this.transformPoint.position = position;
			this.enemyType = enemyType;
			this.hasAction = hasAction;
		}

		public Vector2 Point
		{
			get
			{
				return this.transformPoint.position;
			}
		}

		public Transform transformPoint;

		public int enemyType;

		public bool hasAction;
	}
}
