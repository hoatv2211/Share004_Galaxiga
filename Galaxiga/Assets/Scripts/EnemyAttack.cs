using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

public class EnemyAttack : BoxItemEnemy
{
	private void Awake()
	{
		this._anim = this.avatar.GetComponent<Animator>();
		if (this.avatar != null)
		{
			this.avatarSpawBullet = this.avatar.GetComponent<AvatarSpawnBullet>();
		}
	}

	private void OnBecameVisible()
	{
		this.startAttack = true;
	}

	[EnemyAction(displayName = "Attack/Bắn Đạn")]
	public virtual void Attack(bool randomTime, int percentAttack)
	{
		this.effectHold.SetActive(true);
		if (percentAttack >= UnityEngine.Random.Range(0, 100) && this.startAttack)
		{
			base.StartCoroutine(this.AttackIE(randomTime));
		}
	}

	[EnemyAction(displayName = "Attack/Bắn Đạn Theo ID")]
	public void AttackID(bool randomTime, int percentAttack, int[] listIndex)
	{
		bool flag = false;
		for (int i = 0; i < listIndex.Length; i++)
		{
			if (this.Index == listIndex[i])
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.effectHold.SetActive(true);
			this.Attack(randomTime, percentAttack);
		}
	}

	private IEnumerator AttackIE(bool random)
	{
		float timeramdom = 0.1f;
		if (random)
		{
			timeramdom = (float)UnityEngine.Random.Range(1, 3);
		}
		yield return new WaitForSeconds(timeramdom);
		this._anim.SetTrigger("attack");
		this.childObjAttack.m_bulletSpeed = this.speedBullet;
		if (this.childObjAttack2 != null)
		{
			this.childObjAttack2.m_bulletSpeed = this.speedBullet;
		}
		this.avatarSpawBullet.numberAttack = 0;
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "Enemy/Tăng Tốc Độ Đạn")]
	public void SetSpeed(float ratio)
	{
		this.speedBullet *= ratio;
	}

	[EnemyAction(displayName = "Attack/Bắn Nhiều Loại Đạn")]
	public void MultyAttack(int percentAttack, bool randomAttack)
	{
		if (this.add == this.listUbh.Count)
		{
			this.add = 0;
		}
		if (randomAttack)
		{
			this.add = UnityEngine.Random.Range(0, this.listUbh.Count);
		}
		if (percentAttack >= UnityEngine.Random.Range(0, 100) && this.startAttack)
		{
			this.effectHold.SetActive(true);
			base.StartCoroutine(this.MultyAttackIE(this.add));
		}
	}

	private IEnumerator MultyAttackIE(int number)
	{
		yield return new WaitForSeconds(2f);
		this._anim.SetTrigger("attack");
		this.add++;
		this.avatarSpawBullet.numberAttack = this.add;
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "Attack/Giữ >> Tấn Công")]
	public virtual void Attack_Hold(float timeHold, bool random)
	{
		base.StartCoroutine(this.Hold(timeHold, random));
	}

	private IEnumerator Hold(float timeHold, bool random)
	{
		this._anim.SetTrigger("hold");
		yield return new WaitForSeconds(timeHold);
		this.Attack(random, 100);
		yield return null;
		yield break;
	}

	private bool startAttack;

	[Header("ANIMATION")]
	public GameObject avatar;

	private Animator _anim;

	[Header("ATTACK")]
	public bool stHold = true;

	public float timeAttack;

	public UbhShotCtrl objAttack;

	public UbhBaseShot childObjAttack;

	public UbhBaseShot childObjAttack2;

	public bool stattackPath;

	public float speedBullet = 10f;

	public GameObject effectHold;

	[Header("MULTY_ATTACK")]
	public bool multiAttack;

	[ShowIf("multiAttack", true)]
	public List<UbhShotCtrl> listUbh = new List<UbhShotCtrl>();

	[ShowIf("multiAttack", true)]
	public List<float> listTimeAttack = new List<float>();

	[ShowIf("multiAttack", true)]
	public int add;

	private AvatarSpawnBullet avatarSpawBullet;
}
