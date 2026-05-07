using System;
using SkyGameKit;
using UnityEngine;

public class BossChildHit : BaseEnemy
{
	private void OnEnable()
	{
		this.baseBoss = this.bossParent.GetComponent<BossGeneral>();
	}

	private void OnBecameVisible()
	{
		this.CurrentHP = this.startHP;
		this.startSetHP = true;
	}

	public override int CurrentHP
	{
		get
		{
			return base.CurrentHP;
		}
		set
		{
			if (this.startSetHP)
			{
				this.baseBoss.CurrentHP -= this.CurrentHP - value;
			}
			else if (this.startHP > 0)
			{
				base.CurrentHP = this.startHP;
			}
		}
	}

	public GameObject bossParent;

	private BaseEnemy baseBoss;

	private bool startSetHP;
}
