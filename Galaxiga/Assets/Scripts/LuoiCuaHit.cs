using System;
using SkyGameKit;
using UnityEngine;

public class LuoiCuaHit : BoxItemEnemy
{
	public override void Restart()
	{
		base.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		base.Restart();
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
	}

	public Transform avatar;
}
