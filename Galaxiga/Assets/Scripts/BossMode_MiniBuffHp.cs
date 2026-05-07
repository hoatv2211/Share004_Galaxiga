using System;
using System.Collections;
using SkyGameKit;
using UnityEngine;

public class BossMode_MiniBuffHp : BaseEnemy
{
	private void Start()
	{
		this.timeDelayActive = 15f;
		this.numberHP = 800000;
		base.StartCoroutine(this.SetStat(this.numberHP));
	}

	private void Update()
	{
		if (this.isActive)
		{
			if (this.CurrentHP <= 0)
			{
				base.StartCoroutine(this.InactiveBuff());
			}
			else
			{
				this.healthBar.transform.localScale = new Vector3((float)this.CurrentHP * 1f / (float)this.startHP, this.healthBar.transform.localScale.y, 0f);
			}
		}
	}

	public IEnumerator SetStat(int _hpLock)
	{
		yield return new WaitForSeconds(this.timeDelayActive);
		if (!this.isActive)
		{
			this.startHP = _hpLock;
			this.CurrentHP = this.startHP;
			this.isActive = true;
			base.StartCoroutine(this.ActiveBuff());
		}
		yield break;
	}

	private IEnumerator ActiveBuff()
	{
		this.anim.SetTrigger("transform");
		yield return new WaitForSeconds(2.85f);
		this.anim.SetTrigger("idle1");
		this.fx_BossBuff.SetActive(true);
		this.fx_MiniBuff.SetActive(true);
		this.circleBox.enabled = true;
		this.parenthealthBar.SetActive(true);
		this.boss.isRegen = true;
		SgkSingleton<LevelManager>.Instance.AliveEnemy.Add(this);
		yield break;
	}

	private IEnumerator InactiveBuff()
	{
		if (this.isActive)
		{
			this.isActive = false;
			this.anim.SetTrigger("transform");
			this.fx_BossBuff.SetActive(false);
			this.fx_MiniBuff.SetActive(false);
			this.circleBox.enabled = false;
			this.parenthealthBar.SetActive(false);
			this.boss.isRegen = false;
			SgkSingleton<LevelManager>.Instance.AliveEnemy.Remove(this);
			Fu.SpawnExplosion(this.fx_Explosive, base.gameObject.transform.position, Quaternion.identity);
			yield return new WaitForSeconds(2.85f);
			this.anim.SetTrigger("idle0");
			base.StartCoroutine(this.SetStat(this.numberHP));
		}
		yield break;
	}

	private bool isActive;

	public float timeDelayActive;

	public int numberHP;

	[SerializeField]
	private GameObject parenthealthBar;

	[SerializeField]
	private GameObject healthBar;

	[SerializeField]
	private Animator anim;

	[SerializeField]
	private GameObject fx_BossBuff;

	[SerializeField]
	private GameObject fx_MiniBuff;

	[SerializeField]
	private CircleCollider2D circleBox;

	[SerializeField]
	private BossMode_LaserSpecial boss;

	[SerializeField]
	private GameObject fx_Explosive;
}
