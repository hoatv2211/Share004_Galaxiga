using System;
using SkyGameKit;

public class EnemyLaser : ReflectiveLaser
{
	protected override void OnHit(LaserHitInfo target)
	{
		base.OnHit(target);
		PlaneIngameManager.current.TakeDamagePlayer();
	}
}
