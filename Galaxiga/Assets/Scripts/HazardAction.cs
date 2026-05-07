using System;
using Hellmade.Sound;
using SkyGameKit;
using UnityEngine;

public class HazardAction : BoxItemEnemy
{
	[EnemyAction(displayName = "Hazard/KickThuoc Hazard")]
	public void SizeHazard(float minSize, float maxSize)
	{
		float num = UnityEngine.Random.Range(minSize, maxSize);
		base.transform.localScale = new Vector3(num, num, 1f);
		this.CurrentHP = (int)((float)this.CurrentHP * num);
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		if (type == EnemyKilledBy.Player)
		{
			EazySoundManager.PlaySound(AudioCache.Sound.enemy_die3);
		}
	}
}
