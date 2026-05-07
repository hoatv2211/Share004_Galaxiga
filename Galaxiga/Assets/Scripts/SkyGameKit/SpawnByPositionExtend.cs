using System;
using UnityEngine;
namespace SkyGameKit
{
	public class SpawnByPositionExtend : TurnManager
	{
		protected override int Spawn()
		{
			if (Fu.IsNullOrEmpty(this.extendEnemies))
			{
				SgkLog.LogError("Danh sánh enemy trống Turn: " + base.name);
				return 0;
			}
			foreach (PointOfShape pointOfShape in this.shape.points)
			{
				this.SpawnEnemy(this.extendEnemies[pointOfShape.enemyType % this.extendEnemies.Length]);
			}
			return this.shape.points.Count;
		}

		protected override bool BeforeSetFieldAndAction(BaseEnemy enemy)
		{
			enemy.transform.position = base.transform.position + (Vector3)this.shape.points[enemy.Index].Point;
			return base.BeforeSetFieldAndAction(enemy);
		}

		public BaseEnemy[] extendEnemies;

		public ShapeManager shape;
	}
}
