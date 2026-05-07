using System;
using System.Collections;
using DG.Tweening;
using Hellmade.Sound;
using SkyGameKit;
using UnityEngine;

public class BossMode_DomDom : NewBoss
{
	[EnemyAction(displayName = "Tuan/StartAttack")]
	public void StartAttack()
	{
		this.isAttack = true;
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry0")]
	public void ActiveStatusAngry0()
	{
		this.isTranform = true;
		this.isActiveWing = true;
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry1")]
	public void ActiveStatusAngry1()
	{
		this.isTranform = true;
		this.isActiveTail = true;
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry2")]
	public void ActiveStatusAngry2()
	{
		this.isTranform = true;
		this.isActiveHead = true;
	}

	[EnemyAction(displayName = "Tuan/Skill0_ShotByGun0")]
	public void Skill0_ShotByGun0(int timeDelay, float speed)
	{
		this.timeDelay_BulletGuns0 = (float)timeDelay;
		this.ubh_BulletGuns0[0].m_bulletSpeed = speed;
		this.ubh_BulletGuns0[1].m_bulletSpeed = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill1_ShotByGun1")]
	public void Skill1_ShotByGun1(int timeDelay, float speed)
	{
		this.timeDelay_BulletGuns1 = (float)timeDelay;
		this.ubh_BulletGuns1[0].m_bulletSpeed = speed;
		this.ubh_BulletGuns1[1].m_bulletSpeed = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill2_ShotByStomach")]
	public void Skill2_ShotByStomach(int timeDelay, float speed)
	{
		this.timeDelay_BulletStomach = (float)timeDelay;
		this.ubh_BulletStomach.m_bulletSpeed = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill3_ShotRandomByWing")]
	public void Skill3_ShotRandomByWing(int timeDelay, float speed)
	{
		this.timeDelay_BulletWing = (float)timeDelay;
		this.ubh_BulletWing[0].m_maxSpeed = speed;
		this.ubh_BulletWing[1].m_maxSpeed = speed;
		this.ubh_BulletWing[0].m_minSpeed = speed - 1f;
		this.ubh_BulletWing[1].m_minSpeed = speed - 1f;
	}

	[EnemyAction(displayName = "Tuan/Skill4_LaserBall")]
	public void Skill4_LaserBall(int timeDelay)
	{
		this.timeDelay_LaserBall = (float)timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill5_BombTail")]
	public void Skill5_BombTail(int timeDelay, float speed)
	{
		this.timeDelay_BulletTail = (float)timeDelay;
		this.ubh_BulletTail.m_bulletSpeed = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill6_LaserTail")]
	public void Skill6_LaserTail(int timeDelay)
	{
		this.timeDelay_LaserTail = (float)timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill7_LaserEyes")]
	public void Skill7_LaserEyes(int timeDelay)
	{
		this.timeDelay_LaserEyes = (float)timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill8_Summon5Ball")]
	public void Skill8_Summon5Ball(int timeDelay, float speed)
	{
		this.timeDelay_Ultimate = (float)timeDelay;
		this.ubh_Ultimate[0].m_bulletSpeed = speed;
		this.ubh_Ultimate[1].m_bulletSpeed = speed;
		this.ubh_Ultimate[2].m_bulletSpeed = speed;
		this.ubh_Ultimate[3].m_bulletSpeed = speed;
		this.ubh_Ultimate[4].m_bulletSpeed = speed;
	}

	public override void Restart()
	{
		base.Restart();
		if (this.BaseStatBoss != null)
		{
			this.BaseStatBoss();
		}
		base.ShowHealthBar();
		base.SetNameBoss("Green Lantern");
	}

	private void OnEnable()
	{
		EazySoundManager.PlaySound(this.listSound[0], 1f);
	}

	private void Update()
	{
		if (this.isAttack && !this.isTranform)
		{
			this.RandomSkill();
		}
	}

	private void RandomSkill()
	{
		int randomValue;
		if (this.isActiveHead)
		{
			randomValue = this.GetRandomValue(new BossMode_DomDom.RandomSelection[]
			{
				new BossMode_DomDom.RandomSelection(0, 0, 0.05f),
				new BossMode_DomDom.RandomSelection(1, 1, 0.05f),
				new BossMode_DomDom.RandomSelection(2, 2, 0.1f),
				new BossMode_DomDom.RandomSelection(3, 3, 0.1f),
				new BossMode_DomDom.RandomSelection(4, 4, 0.1f),
				new BossMode_DomDom.RandomSelection(5, 5, 0.1f),
				new BossMode_DomDom.RandomSelection(6, 6, 0.15f),
				new BossMode_DomDom.RandomSelection(7, 7, 0.2f),
				new BossMode_DomDom.RandomSelection(8, 8, 0.15f)
			});
		}
		else if (this.isActiveTail)
		{
			randomValue = this.GetRandomValue(new BossMode_DomDom.RandomSelection[]
			{
				new BossMode_DomDom.RandomSelection(0, 0, 0.05f),
				new BossMode_DomDom.RandomSelection(1, 1, 0.05f),
				new BossMode_DomDom.RandomSelection(2, 2, 0.1f),
				new BossMode_DomDom.RandomSelection(3, 3, 0.1f),
				new BossMode_DomDom.RandomSelection(4, 4, 0.1f),
				new BossMode_DomDom.RandomSelection(5, 5, 0.25f),
				new BossMode_DomDom.RandomSelection(6, 6, 0.25f),
				new BossMode_DomDom.RandomSelection(8, 8, 0.1f)
			});
		}
		else if (this.isActiveWing)
		{
			randomValue = this.GetRandomValue(new BossMode_DomDom.RandomSelection[]
			{
				new BossMode_DomDom.RandomSelection(0, 0, 0.1f),
				new BossMode_DomDom.RandomSelection(1, 1, 0.1f),
				new BossMode_DomDom.RandomSelection(2, 2, 0.1f),
				new BossMode_DomDom.RandomSelection(3, 3, 0.35f),
				new BossMode_DomDom.RandomSelection(4, 4, 0.25f),
				new BossMode_DomDom.RandomSelection(8, 8, 0.1f)
			});
		}
		else
		{
			randomValue = this.GetRandomValue(new BossMode_DomDom.RandomSelection[]
			{
				new BossMode_DomDom.RandomSelection(0, 0, 0.3f),
				new BossMode_DomDom.RandomSelection(1, 1, 0.35f),
				new BossMode_DomDom.RandomSelection(2, 2, 0.2f),
				new BossMode_DomDom.RandomSelection(8, 8, 0.15f)
			});
		}
		if (randomValue == this.currentIdSkill)
		{
			this.RandomSkill();
		}
		else
		{
			this.currentIdSkill = randomValue;
			this.Attack();
		}
	}

	private void Attack()
	{
		switch (this.currentIdSkill)
		{
		case 0:
			base.StartCoroutine(this.AttackBulletGuns0());
			break;
		case 1:
			base.StartCoroutine(this.AttackBulletGuns1());
			break;
		case 2:
			base.StartCoroutine(this.AttackBulletStomach());
			break;
		case 3:
			base.StartCoroutine(this.AttackBulletWing());
			break;
		case 4:
			base.StartCoroutine(this.AttackLaserBall());
			break;
		case 5:
			base.StartCoroutine(this.AttackLaserTail());
			break;
		case 6:
			base.StartCoroutine(this.AttackBulletTail());
			break;
		case 7:
			base.StartCoroutine(this.AttackLaserEyes());
			break;
		case 8:
			base.StartCoroutine(this.AttackUltimate());
			break;
		}
	}

	private IEnumerator TranformStatus()
	{
		if (this.isTranform)
		{
			if (this.isActiveHead)
			{
				base.ShowTextScreen();
				this.listEffectTransform[0].SetActive(true);
				yield return new WaitForSeconds(1f);
				this.listBodyPart[0].SetActive(true);
				this.listBodyPart[11].SetActive(true);
				this.anim.SetTrigger("transform2");
				EazySoundManager.PlaySound(this.listSound[1], 1f);
				this.isActiveHead = true;
				yield return new WaitForSeconds(2f);
				this.listEffectTransform[0].SetActive(false);
				this.listEffectTransform[1].SetActive(true);
				base.StartCoroutine(this.AttackLaserEyes());
				this.currentIdSkill = 7;
				this.isTranform = false;
			}
			else if (this.isActiveTail)
			{
				base.ShowTextScreen();
				yield return new WaitForSeconds(1f);
				this.listBodyPart[10].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
				this.listBodyPart[10].SetActive(true);
				this.anim.SetTrigger("transform1");
				EazySoundManager.PlaySound(this.listSound[1], 1f);
				this.isActiveTail = true;
				yield return new WaitForSeconds(2f);
				base.StartCoroutine(this.AttackLaserTail());
				this.currentIdSkill = 5;
				this.isTranform = false;
			}
			else if (this.isActiveWing)
			{
				base.ShowRedScreen();
				yield return new WaitForSeconds(1f);
				this.listBodyPart[6].SetActive(true);
				this.listBodyPart[7].SetActive(true);
				this.listBodyPart[8].SetActive(true);
				this.listBodyPart[9].SetActive(true);
				this.anim.SetTrigger("transform0");
				EazySoundManager.PlaySound(this.listSound[1], 1f);
				this.isActiveWing = true;
				yield return new WaitForSeconds(1f);
				base.StartCoroutine(this.AttackLaserBall());
				this.currentIdSkill = 3;
				this.isTranform = false;
			}
		}
		else
		{
			this.isAttack = true;
		}
		yield break;
	}

	private IEnumerator AttackBulletGuns0()
	{
		this.isAttack = false;
		yield return new WaitForSeconds(this.timeDelay_BulletGuns0);
		this.smoothFollow.isSmoothFollow = false;
		this.MoveCross();
		this.anim.SetTrigger("attack_guns0");
		yield return new WaitForSeconds(0.6f);
		this.obj_BulletGuns0.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[3], 1f);
		yield return new WaitForSeconds(0.7f);
		EazySoundManager.PlaySound(this.listSound[3], 1f);
		yield return new WaitForSeconds(1.3f);
		this.anim.SetTrigger("attack_guns0");
		yield return new WaitForSeconds(0.6f);
		this.obj_BulletGuns0.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[3], 1f);
		yield return new WaitForSeconds(0.7f);
		EazySoundManager.PlaySound(this.listSound[3], 1f);
		yield return new WaitForSeconds(1.3f);
		this.anim.SetTrigger("attack_guns0");
		yield return new WaitForSeconds(0.6f);
		this.obj_BulletGuns0.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[3], 1f);
		yield return new WaitForSeconds(0.7f);
		EazySoundManager.PlaySound(this.listSound[3], 1f);
		yield return new WaitForSeconds(1.3f);
		this.smoothFollow.isSmoothFollow = true;
		base.StartCoroutine(this.TranformStatus());
		yield break;
	}

	private IEnumerator AttackBulletGuns1()
	{
		this.isAttack = false;
		yield return new WaitForSeconds(this.timeDelay_BulletGuns1);
		this.smoothFollow.isSmoothFollow = false;
		this.MoveUp();
		this.anim.SetTrigger("attack_guns1");
		yield return new WaitForSeconds(0.6f);
		this.obj_BulletGuns1.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[4], 1f);
		yield return new WaitForSeconds(0.2f);
		EazySoundManager.PlaySound(this.listSound[4], 1f);
		yield return new WaitForSeconds(0.2f);
		EazySoundManager.PlaySound(this.listSound[4], 1f);
		yield return new WaitForSeconds(0.2f);
		EazySoundManager.PlaySound(this.listSound[4], 1f);
		yield return new WaitForSeconds(0.2f);
		this.smoothFollow.isSmoothFollow = true;
		base.StartCoroutine(this.TranformStatus());
		yield break;
	}

	private IEnumerator AttackBulletStomach()
	{
		this.isAttack = false;
		yield return new WaitForSeconds(this.timeDelay_BulletStomach);
		this.smoothFollow.isSmoothFollow = false;
		this.anim.SetTrigger("attack_stomach");
		EazySoundManager.PlaySound(this.listSound[5], 1f);
		yield return new WaitForSeconds(1.5f);
		EazySoundManager.PlaySound(this.listSound[5], 1f);
		this.obj_BulletStomach.StartShotRoutine();
		yield return new WaitForSeconds(2.5f);
		EazySoundManager.PlaySound(this.listSound[6], 1f);
		yield return new WaitForSeconds(1f);
		this.smoothFollow.isSmoothFollow = true;
		yield return new WaitForSeconds(1f);
		base.StartCoroutine(this.TranformStatus());
		yield break;
	}

	private IEnumerator AttackBulletWing()
	{
		this.isAttack = false;
		yield return new WaitForSeconds(this.timeDelay_BulletWing);
		this.smoothFollow.isSmoothFollow = false;
		this.MoveDown();
		this.anim.SetTrigger("attack_bulletwing");
		yield return new WaitForSeconds(0.8f);
		this.obj_BulletWing.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[7], 1f);
		yield return new WaitForSeconds(2f);
		base.StartCoroutine(this.TranformStatus());
		yield break;
	}

	private IEnumerator AttackLaserBall()
	{
		this.isAttack = false;
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid();
		yield return new WaitForSeconds(this.timeDelay_LaserBall);
		int[] angle = new int[]
		{
			-90,
			90
		};
		int id = UnityEngine.Random.Range(0, 2);
		this.anim.SetTrigger("attack_rocket");
		this.obj_LaserBall.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		this.warning_LaserBall.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		yield return new WaitForSeconds(0.75f);
		this.warning_LaserBall.SetActive(true);
		this.warning_LaserBall.transform.DORotate(new Vector3(0f, 0f, (float)angle[id]), 0.75f, RotateMode.Fast);
		EazySoundManager.PlaySound(this.listSound[8], 1f);
		yield return new WaitForSeconds(1f);
		EazySoundManager.PlaySound(this.listSound[9], 1f);
		this.warning_LaserBall.SetActive(false);
		this.obj_LaserBall.SetActive(true);
		this.obj_LaserBall.transform.DORotate(new Vector3(0f, 0f, (float)(-(float)angle[id])), 2f, RotateMode.Fast);
		yield return new WaitForSeconds(2f);
		this.smoothFollow.isSmoothFollow = true;
		this.obj_LaserBall.SetActive(false);
		base.StartCoroutine(this.TranformStatus());
		yield break;
	}

	private IEnumerator AttackBulletTail()
	{
		this.isAttack = false;
		yield return new WaitForSeconds(this.timeDelay_BulletTail);
		this.anim.SetTrigger("attack_bullettail");
		yield return new WaitForSeconds(2f);
		this.obj_BulletTail.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[10], 1f);
		yield return new WaitForSeconds(2f);
		this.listBodyPart[10].SetActive(true);
		base.StartCoroutine(this.TranformStatus());
		yield break;
	}

	private IEnumerator AttackLaserTail()
	{
		this.isAttack = false;
		yield return new WaitForSeconds(this.timeDelay_LaserTail);
		this.warning_LaserTail.SetActive(true);
		EazySoundManager.PlaySound(this.listSound[8], 1f);
		yield return new WaitForSeconds(1.25f);
		this.anim.SetTrigger("attack_lasertail");
		yield return new WaitForSeconds(0.75f);
		EazySoundManager.PlaySound(this.listSound[11], 1f);
		this.warning_LaserTail.SetActive(false);
		this.obj_LaserTail.SetActive(true);
		yield return new WaitForSeconds(this.time_AttackLaserTail);
		this.obj_LaserTail.SetActive(false);
		base.StartCoroutine(this.TranformStatus());
		yield break;
	}

	private IEnumerator AttackLaserEyes()
	{
		this.isAttack = false;
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid();
		yield return new WaitForSeconds(this.timeDelay_LaserEyes);
		this.anim.SetTrigger("attack_lasereyes");
		this.warning_LaserEyes[0].SetActive(true);
		this.warning_LaserEyes[1].SetActive(true);
		yield return new WaitForSeconds(1f);
		EazySoundManager.PlaySound(this.listSound[12], 1f);
		this.warning_LaserEyes[0].SetActive(false);
		this.warning_LaserEyes[1].SetActive(false);
		this.obj_LaserEyes[0].SetActive(true);
		this.obj_LaserEyes[1].SetActive(true);
		yield return new WaitForSeconds(3f);
		this.smoothFollow.isSmoothFollow = true;
		this.obj_LaserEyes[0].SetActive(false);
		this.obj_LaserEyes[1].SetActive(false);
		base.StartCoroutine(this.TranformStatus());
		yield break;
	}

	private IEnumerator AttackUltimate()
	{
		this.isAttack = false;
		yield return new WaitForSeconds(this.timeDelay_Ultimate);
		this.smoothFollow.isSmoothFollow = false;
		this.anim.SetTrigger("attack_ultimate");
		EazySoundManager.PlaySound(this.listSound[13], 1f);
		yield return new WaitForSeconds(2f);
		EazySoundManager.PlaySound(this.listSound[10], 1f);
		yield return new WaitForSeconds(1.5f);
		this.obj_Ultimate.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[14], 1f);
		yield return new WaitForSeconds(0.3f);
		EazySoundManager.PlaySound(this.listSound[14], 1f);
		yield return new WaitForSeconds(0.3f);
		EazySoundManager.PlaySound(this.listSound[14], 1f);
		yield return new WaitForSeconds(0.3f);
		EazySoundManager.PlaySound(this.listSound[14], 1f);
		yield return new WaitForSeconds(0.3f);
		EazySoundManager.PlaySound(this.listSound[14], 1f);
		yield return new WaitForSeconds(0.3f);
		this.smoothFollow.isSmoothFollow = true;
		base.StartCoroutine(this.TranformStatus());
		yield break;
	}

	private void MoveCross()
	{
		this.pathCross = new Vector2[]
		{
			new Vector2(0f, 3f),
			new Vector2(-2.5f, 3f),
			new Vector2(0f, 3f),
			new Vector2(2.5f, 3f),
			new Vector2(0f, 3f)
		};
		this.BaseMoveCurve(1.5f, Ease.Linear, PathMode.Ignore, this.pathCross);
	}

	private void MoveUp()
	{
		this.pathUp = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(0f, 5f),
			new Vector2(0f, 3f)
		};
		this.BaseMoveCurve(1.5f, Ease.Linear, PathMode.Ignore, this.pathUp);
	}

	private void MoveDown()
	{
		this.pathDown = new Vector2[]
		{
			new Vector2(0f, 3f),
			new Vector2(0f, -3f),
			new Vector2(0f, 3f)
		};
		this.BaseMoveCurve(3f, Ease.Linear, PathMode.Ignore, this.pathDown);
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

	private int GetRandomValue(params BossMode_DomDom.RandomSelection[] selections)
	{
		float value = UnityEngine.Random.value;
		float num = 0f;
		foreach (BossMode_DomDom.RandomSelection randomSelection in selections)
		{
			num += randomSelection.probability;
			if (value <= num)
			{
				return randomSelection.GetValue();
			}
		}
		return -1;
	}

	private bool isTranform;

	private bool isAttack;

	private bool isActiveTail;

	private bool isActiveWing;

	private bool isActiveHead;

	[SerializeField]
	private SmoothFollow smoothFollow;

	[SerializeField]
	private Animator anim;

	[SerializeField]
	private GameObject[] listBodyPart;

	[SerializeField]
	private GameObject[] listEffectTransform;

	[SerializeField]
	private AudioClip[] listSound;

	private float timeDelay_BulletGuns0;

	[SerializeField]
	private UbhShotCtrl obj_BulletGuns0;

	[SerializeField]
	private UbhNwayShot[] ubh_BulletGuns0;

	private float timeDelay_BulletGuns1;

	[SerializeField]
	private UbhShotCtrl obj_BulletGuns1;

	[SerializeField]
	private UbhSpiralShot[] ubh_BulletGuns1;

	private float timeDelay_BulletStomach;

	[SerializeField]
	private UbhShotCtrl obj_BulletStomach;

	[SerializeField]
	private UbhSpiralShot ubh_BulletStomach;

	private float timeDelay_BulletWing;

	[SerializeField]
	private UbhShotCtrl obj_BulletWing;

	[SerializeField]
	private UbhRandomShot[] ubh_BulletWing;

	private float timeDelay_BulletTail;

	[SerializeField]
	private UbhShotCtrl obj_BulletTail;

	[SerializeField]
	private UbhLinearShot ubh_BulletTail;

	private float timeDelay_Ultimate;

	[SerializeField]
	private UbhShotCtrl obj_Ultimate;

	[SerializeField]
	private UbhHomingShot[] ubh_Ultimate;

	private float timeDelay_LaserBall;

	[SerializeField]
	private GameObject warning_LaserBall;

	[SerializeField]
	private GameObject obj_LaserBall;

	private float timeDelay_LaserTail;

	private float time_AttackLaserTail = 1f;

	[SerializeField]
	private GameObject obj_LaserTail;

	[SerializeField]
	private GameObject warning_LaserTail;

	private float timeDelay_LaserEyes;

	[SerializeField]
	private GameObject[] obj_LaserEyes;

	[SerializeField]
	private GameObject[] warning_LaserEyes;

	[EnemyEventCustom(displayName = "BossTuan/BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

	public int currentIdSkill = -1;

	private Vector2[] pathCross;

	private Vector2[] pathUp;

	private Vector2[] pathDown;

	private Vector2[] pathMid;

	private struct RandomSelection
	{
		public RandomSelection(int minValue, int maxValue, float probability)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.probability = probability;
		}

		public int GetValue()
		{
			return UnityEngine.Random.Range(this.minValue, this.maxValue + 1);
		}

		private int minValue;

		private int maxValue;

		public float probability;
	}
}
