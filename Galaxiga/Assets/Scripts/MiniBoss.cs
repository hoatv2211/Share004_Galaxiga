using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

public class MiniBoss : BossGeneral
{
	private void OnBecameVisible()
	{
		this.startAttack = true;
	}

	private void Start()
	{
		this.avatarSpawnBullet = this.avatar.GetComponent<AvatarSpawnBullet>();
		this._anim = this.avatar.GetComponent<Animator>();
	}

	public override void Restart()
	{
		base.Restart();
		this.speedBullet = BaseEnemyDataSheet.Get(this.id).speedBullet * LevelHardEnemySheet.Get(SgkSingleton<LevelInfo>.Instance.numHard).percentSpeed * LevelEnemySheet.Get(SgkSingleton<LevelInfo>.Instance.numPlanet).percentSpeed;
		if (this.EveryXSEvent != null)
		{
			this.EveryXSEvent.ForEach(delegate(EnemyEventUnit<int> x)
			{
				this.DoActionEveryTime((float)x.param, new Action(x.Invoke), false, false);
			});
		}
	}

	[EnemyAction(displayName = "Attack/Bắn Đạn")]
	public void Attack(bool randomTime, int percentAttack, float timeDelay)
	{
		if (percentAttack >= UnityEngine.Random.Range(0, 100) && this.startAttack)
		{
			base.StartCoroutine(this.AttackIE(randomTime, timeDelay));
		}
	}

	private IEnumerator AttackIE(bool random, float timeDelay)
	{
		this.avatarSpawnBullet.ActiveEffect();
		if (timeDelay == 0f)
		{
			timeDelay = 2f;
		}
		if (random)
		{
			timeDelay = UnityEngine.Random.Range(1f, timeDelay);
		}
		yield return new WaitForSeconds(timeDelay);
		this._anim.SetTrigger("attack");
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "Attack/Bắn Nhiều Loại Đạn")]
	public void MultyAttack(int percentAttack, bool randomAttack, float timeDelay)
	{
		if (randomAttack)
		{
			this.add = UnityEngine.Random.Range(0, this.listUbh.Count);
		}
		if (percentAttack >= UnityEngine.Random.Range(0, 100) && this.startAttack)
		{
			this.avatarSpawnBullet.ActiveEffect();
			base.StartCoroutine(this.MultyAttackIE(this.add, timeDelay));
		}
	}

	[EnemyAction(displayName = "Attack/Giữ >> Tấn Công")]
	public virtual void Attack_Hold(float timeHold, bool random, float timeDelay)
	{
		base.StartCoroutine(this.Hold(timeHold, random, timeDelay));
	}

	private IEnumerator Hold(float timeHold, bool random, float timeDelay)
	{
		this.avatarSpawnBullet.ActiveEffect();
		if (timeDelay == 0f)
		{
			timeDelay = 2f;
		}
		this._anim.SetTrigger("hold");
		yield return new WaitForSeconds(timeHold);
		this.Attack(random, 100, timeDelay);
		yield return null;
		yield break;
	}

	private IEnumerator MultyAttackIE(int number, float timeDelay)
	{
		yield return new WaitForSeconds(timeDelay);
		this._anim.SetTrigger("attack");
		this.add++;
		if (this.add >= this.avatarSpawnBullet.objSpawnBullet.Count)
		{
			this.add = 0;
		}
		this.avatarSpawnBullet.numberAttack = this.add;
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "MiniBoss/Tăng Tốc Độ Đạn")]
	public void SetSpeed(float ratio)
	{
		this.speedBullet *= ratio;
	}

	private bool startAttack;

	public float speedBullet;

	public UbhBaseShot childObjAttack;

	public UbhBaseShot childObjAttack2;

	public bool multyAttack;

	[ShowIf("multyAttack", true)]
	public List<UbhShotCtrl> listUbh = new List<UbhShotCtrl>();

	[ShowIf("multyAttack", true)]
	public int add;

	private Animator _anim;

	public GameObject avatar;

	[EnemyEventCustom(paramName = "time")]
	public EnemyEvent<int> EveryXSEvent = new EnemyEvent<int>();

	private AvatarSpawnBullet avatarSpawnBullet;
}
