using System;
using SkyGameKit;
using Spine;
using Spine.Unity;
using UnityEngine;

public class BossMode_Shield_PartOfBoss : BaseEnemy
{
	public void SetStat(int _startHp)
	{
		this.startHP = _startHp;
		this.CurrentHP = this.startHP;
		this.isStart = true;
		SgkSingleton<LevelManager>.Instance.AliveEnemy.Add(this);
	}

	private void Update()
	{
		if (this.isStart)
		{
			if (this.CurrentHP <= 0)
			{
				this.Destroy();
			}
			else
			{
				this.healthBar.transform.localScale = new Vector3((float)this.CurrentHP * 1f / (float)this.startHP, this.healthBar.transform.localScale.y, 0f);
			}
		}
	}

	private void Destroy()
	{
		if (!this.isDestroy)
		{
			SgkSingleton<LevelManager>.Instance.AliveEnemy.Remove(this);
			this.isDestroy = true;
			if (this.idPart == 0)
			{
				this.boss.isDestroyRightHand = true;
			}
			if (this.idPart == 1)
			{
				this.boss.isDestroyLeftHand = true;
			}
			if (this.idPart == 2)
			{
				this.boss.isDestroyTail = true;
				base.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			}
			this.parenthealthBar.SetActive(false);
			Fu.SpawnExplosion(this.fxDestroy, base.transform.GetChild(0).transform.position, Quaternion.identity);
			this.anim.AnimationState.SetAnimation(0, this.animDie, false).Complete += delegate(TrackEntry A_1)
			{
				base.gameObject.transform.GetChild(0).gameObject.SetActive(false);
			};
		}
	}

	public int idPart;

	[Header("HEALTH_BOSS")]
	[SerializeField]
	private GameObject parenthealthBar;

	[SerializeField]
	private GameObject healthBar;

	[SerializeField]
	private SkeletonAnimation anim;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animDie;

	[SerializeField]
	private BossMode_Shield boss;

	[SerializeField]
	private GameObject fxDestroy;

	private bool isStart;

	private bool isDestroy;
}
