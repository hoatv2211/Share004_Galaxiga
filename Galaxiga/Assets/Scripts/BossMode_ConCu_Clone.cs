using System;
using SkyGameKit;
using UnityEngine;

public class BossMode_ConCu_Clone : BaseEnemy
{
	private void OnEnable()
	{
		this.baseBoss = this.bossParent.GetComponent<BossGeneral>();
	}

	private void OnBecameVisible()
	{
		this.CurrentHP = this.baseBoss.startHP;
	}

	public override int CurrentHP
	{
		get
		{
			return base.CurrentHP;
		}
		set
		{
			this.baseBoss.CurrentHP -= this.CurrentHP - value;
		}
	}

	public GameObject bossParent;

	private BaseEnemy baseBoss;
}
