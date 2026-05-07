using System;
using System.Collections;
using System.Collections.Generic;
using SkyGameKit;
using UnityEngine;

public class TurnPathLoopExtend : SpawnByNumberExtend
{
	protected override IEnumerator SpawnListEnemy()
	{
		for (int i = 0; i < this.extendEnemies.Length; i++)
		{
			for (int j = 0; j < this.extendEnemies[i].number; j++)
			{
				BaseEnemy ene = this.SpawnEnemy(this.extendEnemies[i].enemy);
				this.SetPathEnemy(ene);
				yield return new WaitForSeconds(this.timeToNextEnemy);
			}
		}
		yield break;
	}

	private void SetPathEnemy(BaseEnemy enemy)
	{
		for (int i = 0; i < this.ListPathLoop.Count; i++)
		{
			(enemy as EnemyGeneral).SetPathLoopCurveExtend(this.ListPathLoop, 0, this.stloop);
		}
	}

	public List<PathLoopInfo> ListPathLoop = new List<PathLoopInfo>();

	public bool stloop;
}
