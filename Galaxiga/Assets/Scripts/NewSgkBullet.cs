using System;
using SkyGameKit;
using UnityEngine;

public class NewSgkBullet : SgkBullet
{
	public override void Explosion()
	{
		if (!this.isDontDestroy)
		{
			base.Explosion();
		}
		else
		{
			Fu.SpawnExplosion(this.explosionPrefab, base.transform.position, Quaternion.identity);
		}
	}

	protected override void OnDisable()
	{
		if (!this.isDontDestroy)
		{
			base.OnDisable();
		}
	}

	public bool isDontDestroy;
}
