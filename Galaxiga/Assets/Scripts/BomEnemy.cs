using System;
using SkyGameKit;
using UnityEngine;

public class BomEnemy : EnemyGeneral
{
	public override int CurrentHP
	{
		get
		{
			return base.CurrentHP;
		}
		set
		{
			base.CurrentHP = value;
			float num = (float)(this.startHP - this.CurrentHP) / (float)this.startHP;
			if (num < 0.3f)
			{
				num = 0.3f;
			}
			this.avatar.transform.localScale = new Vector3(num, num, 1f);
		}
	}

	public override void Restart()
	{
		base.Restart();
		this.avatar.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
	}

	[EnemyAction(displayName = "BomEnemy/Spaw Bullet(Die)")]
	public void spawBullet()
	{
		this.ubhShot.StartShotRoutine();
	}

	public UbhShotCtrl ubhShot;
}
