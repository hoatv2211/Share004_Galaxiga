using System;
using PathologicalGames;
using SkyGameKit;
using UnityEngine;

public class EnemyNoel : EnemyGeneral
{
	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		float num = (float)UnityEngine.Random.Range(0, 100);
		if (num < this.percentSpawn && this.waveControl.addNumberDrop < this.waveControl.numberDrop && this.waveControl.itemSpawnDrop != null)
		{
			PoolManager.Pools["ItemPool"].Spawn(this.waveControl.itemSpawnDrop, base.transform.position, Quaternion.identity);
			this.waveControl.addNumberDrop = this.waveControl.addNumberDrop++;
		}
	}

	public GameObject itemEvent;

	public float percentSpawn = 100f;
}
