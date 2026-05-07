using System;
using SkyGameKit;
using UnityEngine;

public class WallBullet : EnemyHit
{
	protected override void OnTriggerEnter2D(Collider2D col)
	{
		string tag = col.tag;
		if (tag != null)
		{
			if (tag == "PlayerBullet")
			{
				SgkBullet component = col.GetComponent<SgkBullet>();
				component.Explosion();
			}
		}
	}
}
