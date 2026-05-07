using System;
using UnityEngine;

namespace SkyGameKit
{
	public class SpawnByNumber : TurnManager
	{
		protected override int Spawn()
		{
			this.spawnStream = this.DoActionEveryTime(this.timeToNextEnemy, this.number, delegate()
			{
				this.SpawnEnemy();
			}, true, false);
			return this.number;
		}

		[Tooltip("Thời gian cách nhau giữa các enemy")]
		public float timeToNextEnemy;

		[Tooltip("Số lượng enemy")]
		public int number = 1;
	}
}
