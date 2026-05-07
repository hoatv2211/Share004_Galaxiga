using System;
using System.Collections;
using SkyGameKit;
using UnityEngine;

public class BossShield : BossGeneral
{
	private void Start()
	{
		this._anim = base.GetComponent<Animator>();
		for (int i = 0; i < SgkSingleton<LevelManager>.Instance.AliveEnemy.Count; i++)
		{
			if (this == SgkSingleton<LevelManager>.Instance.AliveEnemy[i])
			{
				SgkSingleton<LevelManager>.Instance.AliveEnemy.Remove(SgkSingleton<LevelManager>.Instance.AliveEnemy[i]);
				break;
			}
		}
		base.StartCoroutine(this.Attack1());
	}

	private IEnumerator Attack1()
	{
		yield return new WaitForSeconds(this.timeAttack1);
		if (!this.stun)
		{
			this.objAttackLeft.StartShotRoutine();
			this.objAttackRight.StartShotRoutine();
			if ((float)this.CurrentHP < (float)this.startHP / 1.5f)
			{
				this.objAttack4Way.StartShotRoutine();
			}
		}
		yield return null;
		if (!this.stun)
		{
			base.StartCoroutine(this.Attack2());
		}
		else
		{
			base.StartCoroutine(this.SetIdle());
		}
		yield break;
	}

	private IEnumerator Attack2()
	{
		yield return new WaitForSeconds(this.timeAttack2);
		if (!this.stun)
		{
			this.objAttack2Left.StartShotRoutine();
			this.objAttack2Right.StartShotRoutine();
			if ((float)this.CurrentHP < (float)this.startHP / 3f)
			{
				this.objAttack4Way.StartShotRoutine();
			}
		}
		yield return null;
		if (!this.stun)
		{
			base.StartCoroutine(this.Attack3());
		}
		else
		{
			base.StartCoroutine(this.SetIdle());
		}
		yield break;
	}

	private IEnumerator Attack3()
	{
		if ((float)this.CurrentHP > (float)this.startHP / 2f)
		{
			yield return new WaitForSeconds(this.timeAttack3);
		}
		else
		{
			yield return new WaitForSeconds(this.timeAttack3 / 2f);
		}
		if (!this.stun)
		{
			this.objAttack3.StartShotRoutine();
		}
		yield return null;
		if (!this.stun)
		{
			base.StartCoroutine(this.Attack1());
		}
		else
		{
			base.StartCoroutine(this.SetIdle());
		}
		yield break;
	}

	public void Stun()
	{
		this.stun = true;
		SgkSingleton<LevelManager>.Instance.AliveEnemy.Add(this);
		this._anim.SetBool("Stun", true);
	}

	private IEnumerator SetIdle()
	{
		yield return new WaitForSeconds(this.timeStun);
		this.shield.SetActive(true);
		this.stun = false;
		this._anim.SetBool("Stun", false);
		for (int i = 0; i < SgkSingleton<LevelManager>.Instance.AliveEnemy.Count; i++)
		{
			if (this == SgkSingleton<LevelManager>.Instance.AliveEnemy[i])
			{
				SgkSingleton<LevelManager>.Instance.AliveEnemy.Remove(SgkSingleton<LevelManager>.Instance.AliveEnemy[i]);
				break;
			}
		}
		this.shield.SendMessage("ResetListLock");
		yield return null;
		base.StartCoroutine(this.Attack1());
		yield break;
	}

	[Header("ATTACK_BOSS")]
	public float timeAttack1;

	public float timeAttack2;

	public float timeAttack3;

	public float timeAttack4;

	public UbhShotCtrl objAttackLeft;

	public UbhShotCtrl objAttackRight;

	public UbhShotCtrl objAttack2Left;

	public UbhShotCtrl objAttack2Right;

	public UbhShotCtrl objAttack3;

	public UbhShotCtrl objAttack4Way;

	public GameObject shield;

	public bool stun;

	public float timeStun = 3f;

	private Animator _anim;
}
