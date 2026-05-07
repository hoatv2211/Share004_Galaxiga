using System;
using SkyGameKit;
using UnityEngine;

public class SnakeTurn : TurnManager
{
	protected override int Spawn()
	{
		APartOfSnake x = this.enemy as APartOfSnake;
		if (x == null)
		{
			SgkLog.LogError("Enemy không phải APartOfSnake");
			return 0;
		}
		if (this.number < 3)
		{
			SgkLog.LogError("Rắn ngắn quá không chạy được");
			return 0;
		}
		this.isSpawning = true;
		this.lastSpawnPart = null;
		this.DoActionEveryTime(this.timeToNextEnemy, this.number, delegate()
		{
			APartOfSnake apartOfSnake = (APartOfSnake)this.SpawnEnemy();
			if (this.lastSpawnPart != null)
			{
				apartOfSnake.beforeMe = this.lastSpawnPart;
				this.lastSpawnPart.afterMe = apartOfSnake;
			}
			else
			{
				apartOfSnake.OpenEye();
			}
			this.lastSpawnPart = apartOfSnake;
		}, delegate()
		{
			this.isSpawning = false;
			this.lastSpawnPart.GrowTail();
			foreach (BaseEnemy baseEnemy in base.EnemyList)
			{
				APartOfSnake apartOfSnake = (APartOfSnake)baseEnemy;
				apartOfSnake.canTakeDamage = true;
			}
		}, true, false);
		return this.number;
	}

	private void Update()
	{
		for (int i = 0; i < base.EnemyList.Count; i++)
		{
			if (base.EnemyList[i].State != EnemyState.InPool)
			{
				((APartOfSnake)base.EnemyList[i]).UpdateTransform();
			}
		}
	}

	[Tooltip("Thời gian cách nhau giữa các enemy")]
	public float timeToNextEnemy;

	[Tooltip("Số lượng enemy")]
	public int number = 1;

	private APartOfSnake lastSpawnPart;

	[HideInInspector]
	public bool isSpawning;
}
