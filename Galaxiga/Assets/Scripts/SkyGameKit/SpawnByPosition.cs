using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkyGameKit
{
	public class SpawnByPosition : TurnManager
	{
		protected override int Spawn()
		{
			if (this.position.Count > 0)
			{
				this.spawnStream = this.DoActionEveryTime(this.timeToNextEnemy, this.position.Count, delegate()
				{
					this.SpawnEnemy();
				}, true, false);
			}
			return this.position.Count;
		}

		protected override bool BeforeSetFieldAndAction(BaseEnemy enemy)
		{
			enemy.transform.position = base.transform.position + (Vector3)this.position[enemy.Index];
			return base.BeforeSetFieldAndAction(enemy);
		}

		[Tooltip("Thời gian cách nhau giữa các enemy")]
		public float timeToNextEnemy = 0.5f;

		[Tooltip("Vị trí enemy")]
		public List<Vector2> position = new List<Vector2>();
	}
}
