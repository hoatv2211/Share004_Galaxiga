using System;
using System.Collections;
using UnityEngine;

namespace SkyGameKit
{
	public class SpawnByNumberExtend : TurnManager
	{
		protected override int Spawn()
		{
			int num = 0;
			if (Fu.IsNullOrEmpty(this.extendEnemies))
			{
				SgkLog.LogError("Danh sánh enemy trống Turn: " + base.name);
				return 0;
			}
			for (int i = 0; i < this.extendEnemies.Length; i++)
			{
				num += this.extendEnemies[i].number;
			}
			base.StartCoroutine(this.SpawnListEnemy());
			return num;
		}

		protected virtual IEnumerator SpawnListEnemy()
		{
			for (int i = 0; i < this.extendEnemies.Length; i++)
			{
				for (int j = 0; j < this.extendEnemies[i].number; j++)
				{
					this.SpawnEnemy(this.extendEnemies[i].enemy);
					yield return new WaitForSeconds(this.timeToNextEnemy);
				}
			}
			yield break;
		}

		[Tooltip("Thời gian cách nhau giữa các enemy")]
		public float timeToNextEnemy = 0.5f;

		public SpawnByNumberExtend.EnemyAndNumberInformation[] extendEnemies;

		[Serializable]
		public struct EnemyAndNumberInformation
		{
			public EnemyAndNumberInformation(BaseEnemy enemy, int number)
			{
				this.enemy = enemy;
				this.number = number;
			}

			public BaseEnemy enemy;

			public int number;
		}
	}
}
