using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SkyGameKit
{
	public class SpawnByBoss : TurnManager
	{
		protected override int Spawn()
		{
			this.spawnStream = this.DoActionEveryTime(this.timeToNextEnemy, this.number, delegate()
			{
				this.SpawnEnemy();
			}, true, false);
			return this.number;
		}

		protected override bool BeforeSetFieldAndAction(BaseEnemy enemy)
		{
			enemy.transform.position = this.boss.position + (Vector3)this.offset;
			return base.BeforeSetFieldAndAction(enemy);
		}

		[Tooltip("Thời gian cách nhau giữa các enemy")]
		public float timeToNextEnemy;

		[Tooltip("Số lượng enemy")]
		public int number = 1;

		[DisplayAsString]
		public Transform boss;

		[DisplayAsString]
		public Vector2 offset;
	}
}
