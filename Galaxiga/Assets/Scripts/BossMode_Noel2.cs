using System;
using System.Collections;
using DG.Tweening;
using Hellmade.Sound;
using SkyGameKit;
using UnityEngine;

public class BossMode_Noel2 : NewBoss
{
	[EnemyAction(displayName = "Tuan/StartAttack")]
	public void StartAttack()
	{
		base.StartCoroutine(this.AttackBulletHell0());
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry")]
	public void ActiveStatusAngry()
	{
		base.ShowRedScreen();
		EazySoundManager.PlaySound(this.listSound[2], 1f);
	}

	[EnemyAction(displayName = "Tuan/Skill0_BulletSinWave")]
	public void Skill0_BulletSinWave(float timeDelay, float speed)
	{
		this.timeDelay_Bullet0 = timeDelay;
		this.ubhSinWave.m_bulletSpeed = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill1_Bullet3Circle")]
	public void Skill1_Bullet3Circle(float timeDelay, float speed)
	{
		this.timeDelay_Bullet1 = timeDelay;
		this.ubhHoleCircle[0].m_bulletSpeed = speed;
		this.ubhHoleCircle[1].m_bulletSpeed = speed;
		this.ubhHoleCircle[2].m_bulletSpeed = speed;
		this.ubhHoleCircle[3].m_bulletSpeed = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill2_Laser")]
	public void Skill2_Laser(float timeDelay, float timeAttack)
	{
		this.timeDelay_AttackLaser = timeDelay;
		this.time_AttackLaser = timeAttack;
	}

	[EnemyAction(displayName = "Tuan/Skill3_SnowBoom")]
	public void Skill3_SnowBoom(float timeDelay, int numberBullet, float speedBullet)
	{
		this.timeDelay_SnowBom = timeDelay;
		this.numberBullet_SnowBom = numberBullet;
		this.speedBullet_SnowBom = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill4_SnowHomming")]
	public void Skill4_SnowHomming(float timeDelay, int numberBullet, float speed)
	{
		float betweenDelay = 2.5f / (float)numberBullet;
		this.timeDelay_RocketHoming = timeDelay;
		this.ubhHoming[0].m_bulletSpeed = speed;
		this.ubhHoming[0].m_bulletNum = numberBullet;
		this.ubhHoming[0].m_betweenDelay = betweenDelay;
		this.ubhHoming[1].m_bulletSpeed = speed;
		this.ubhHoming[1].m_bulletNum = numberBullet;
		this.ubhHoming[1].m_betweenDelay = betweenDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill5_BulletWave")]
	public void Skill5_BulletWave(float timeDelay, int numberBullet)
	{
		this.timeDelay_Bullet2 = timeDelay;
		this.ubhWaving.m_bulletNum = numberBullet;
	}

	public override void Restart()
	{
		base.Restart();
		if (this.BaseStatBoss != null)
		{
			this.BaseStatBoss();
		}
		base.ShowHealthBar();
		base.SetNameBoss("Winter Storm");
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
		{
			base.StartCoroutine(this.AttackLaser());
		}
	}

	private void OnEnable()
	{
		EazySoundManager.PlaySound(this.listSound[0], 1f);
	}

	private void TranformStatus(int id)
	{
		EazySoundManager.PlaySound(this.listSound[1], 1f);
		if (id == 0)
		{
			this.anim.SetTrigger("transform");
		}
		else if (id == 1)
		{
			this.anim.SetTrigger("transform1");
		}
	}

	private IEnumerator AttackBulletHell0()
	{
		this.animWarning.SetTrigger("warning0");
		yield return new WaitForSeconds(this.timeDelay_Bullet0);
		this.animWarning.SetTrigger("attack0");
		this.objBullet0.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[5], 1f);
		yield return new WaitForSeconds(2f);
		this.animWarning.SetTrigger("idle");
		base.StartCoroutine(this.AttackBulletHell1());
		yield break;
	}

	private IEnumerator AttackBulletHell1()
	{
		this.animWarning.SetTrigger("warning0");
		yield return new WaitForSeconds(this.timeDelay_Bullet1);
		this.animWarning.SetTrigger("attack0");
		this.smoothFollow.isSmoothFollow = false;
		this.objBullet1.StartShotRoutine();
		for (int i = 0; i < 4; i++)
		{
			EazySoundManager.PlaySound(this.listSound[7], 1f);
			yield return new WaitForSeconds(1f);
		}
		this.animWarning.SetTrigger("idle");
		this.smoothFollow.isSmoothFollow = true;
		base.StartCoroutine(this.AttackLaser());
		yield break;
	}

	private IEnumerator AttackLaser()
	{
		this.animWarning.SetTrigger("warning0");
		this.warningLaser.SetActive(true);
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid();
		yield return new WaitForSeconds(this.timeDelay_AttackLaser);
		this.warningLaser.SetActive(false);
		this.animWarning.gameObject.SetActive(false);
		this.objLaser.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		this.objLaser.SetActive(true);
		this.objLaser.transform.DORotate(new Vector3(0f, 0f, 90f), this.time_AttackLaser, RotateMode.Fast);
		for (int i = 0; i < (int)this.time_AttackLaser / 2; i++)
		{
			EazySoundManager.PlaySound(this.listSound[9], 1f);
			yield return new WaitForSeconds(2f);
		}
		yield return new WaitForSeconds(1f);
		this.smoothFollow.isSmoothFollow = true;
		this.animWarning.gameObject.SetActive(true);
		this.objLaser.SetActive(false);
		this.animWarning.SetTrigger("idle");
		base.StartCoroutine(this.AttackSnowBoom());
		yield break;
	}

	private IEnumerator AttackSnowBoom()
	{
		this.TranformStatus(0);
		yield return new WaitForSeconds(2.4f);
		this.warningSnowBom.SetActive(true);
		EazySoundManager.PlaySound(this.listSound[10], 1f);
		yield return new WaitForSeconds(this.timeDelay_SnowBom);
		for (int i = 0; i < this.numberBullet_SnowBom; i++)
		{
			this.anim.SetTrigger("attack2horn");
			yield return new WaitForSeconds(0.2f);
			this.bfp_SnowBom.Attack(0, 0, this.speedBullet_SnowBom, null, false);
			this.bfp_SnowBom.Attack(1, 0, this.speedBullet_SnowBom, null, false);
			EazySoundManager.PlaySound(this.listSound[11], 1f);
			yield return new WaitForSeconds(0.4f);
			this.anim.SetTrigger("idleangry");
		}
		this.warningSnowBom.SetActive(false);
		yield return new WaitForSeconds(2f);
		base.StartCoroutine(this.AttackRocketHoming());
		yield break;
	}

	private IEnumerator AttackRocketHoming()
	{
		yield return new WaitForSeconds(this.timeDelay_RocketHoming);
		this.anim.SetTrigger("attack2guns");
		EazySoundManager.PlaySound(this.listSound[12], 1f);
		yield return new WaitForSeconds(0.9f);
		this.objRocketHoming.StartShotRoutine();
		for (int i = 0; i < 5; i++)
		{
			EazySoundManager.PlaySound(this.listSound[13], 1f);
			yield return new WaitForSeconds(0.5f);
		}
		this.anim.SetTrigger("idleangry");
		base.StartCoroutine(this.AttackBulletHell2());
		yield break;
	}

	private IEnumerator AttackBulletHell2()
	{
		this.animWarning.SetTrigger("warning0");
		yield return new WaitForSeconds(this.timeDelay_Bullet2);
		this.animWarning.SetTrigger("attack0");
		this.objBullet2.StartShotRoutine();
		for (int i = 0; i < 3; i++)
		{
			EazySoundManager.PlaySound(this.listSound[15], 1f);
			yield return new WaitForSeconds(1.67f);
		}
		this.animWarning.SetTrigger("idle");
		yield return new WaitForSeconds(2f);
		this.TranformStatus(1);
		yield return new WaitForSeconds(2f);
		base.StartCoroutine(this.AttackBulletHell0());
		yield break;
	}

	private void MoveMid()
	{
		this.pathMid = new Vector2[]
		{
			new Vector2(base.transform.position.x, base.transform.position.y),
			new Vector2(0f, 4f)
		};
		this.BaseMoveCurve(1.5f, Ease.Linear, PathMode.Ignore, this.pathMid);
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		EazySoundManager.PlaySound(this.listSound[2], 1f);
	}

	[SerializeField]
	private Animator anim;

	[SerializeField]
	private SmoothFollow smoothFollow;

	[SerializeField]
	private Animator animWarning;

	[SerializeField]
	private AudioClip[] listSound;

	private float timeDelay_Bullet0;

	[SerializeField]
	private UbhShotCtrl objBullet0;

	[SerializeField]
	private UbhSinWaveBulletNwayShot ubhSinWave;

	private float timeDelay_Bullet1;

	[SerializeField]
	private UbhShotCtrl objBullet1;

	[SerializeField]
	private UbhHoleCircleShot[] ubhHoleCircle;

	private float timeDelay_AttackLaser;

	private float time_AttackLaser = 10f;

	[SerializeField]
	private GameObject objLaser;

	[SerializeField]
	private GameObject warningLaser;

	private float timeDelay_BigRocket;

	private float numberBullet_BigRocket;

	private float timeDelay_RocketHoming;

	[SerializeField]
	private UbhShotCtrl objRocketHoming;

	[SerializeField]
	private UbhHomingShot[] ubhHoming;

	private float timeDelay_Bullet2;

	[SerializeField]
	private UbhShotCtrl objBullet2;

	[SerializeField]
	private UbhWavingNwayShot ubhWaving;

	private float timeDelay_SnowBom;

	private int numberBullet_SnowBom;

	private float speedBullet_SnowBom = 6f;

	[SerializeField]
	private GameObject warningSnowBom;

	[SerializeField]
	private BulletFollowPath bfp_SnowBom;

	[EnemyEventCustom(displayName = "BossTuan/BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

	private Vector2[] pathMid;
}
