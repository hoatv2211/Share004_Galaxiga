using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Hellmade.Sound;
using SkyGameKit;
using Spine;
using Spine.Unity;
using UnityEngine;

public class BossMode_ChimVang : NewBoss
{
	[EnemyAction(displayName = "Tuan/StartAttack")]
	public void StartAttack()
	{
		this.RandomSkill();
	}

	[EnemyAction(displayName = "Tuan/ChangeStatAngry")]
	public void ChangeStatAngry(float statAngry)
	{
		this.angryStat = statAngry;
		this.animationBoss.timeScale = 1f + (1f - this.angryStat);
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry")]
	public void ActiveStatusAngry()
	{
		this.isActiveAngry = true;
		base.ShowRedScreen();
		EazySoundManager.PlaySound(this.listSound[1], 1f);
	}

	[EnemyAction(displayName = "Tuan/Skill0_ShotCurvy")]
	public void Skill0_ShotCurvy(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill0_TimeDelay = timeDelay;
		for (int i = 0; i < this.skill0_UbhRandom.Length; i++)
		{
			if (i == 1 || i == 4)
			{
				this.skill0_UbhRandom[i].m_bulletNum = numberBullet + 1;
			}
			else
			{
				this.skill0_UbhRandom[i].m_bulletNum = numberBullet;
			}
			this.skill0_UbhRandom[i].m_minSpeed = speedBullet - 3f;
			this.skill0_UbhRandom[i].m_maxSpeed = speedBullet;
		}
	}

	[EnemyAction(displayName = "Tuan/Skill1_ShotFeathers")]
	public void Skill1_ShotFeathers(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill1_TimeDelay = timeDelay;
		for (int i = 0; i < this.skill1_UbhRandom.Length; i++)
		{
			this.skill1_UbhRandom[i].m_bulletNum = numberBullet;
			this.skill1_UbhRandom[i].m_minSpeed = speedBullet - 5f;
			this.skill1_UbhRandom[i].m_maxSpeed = speedBullet;
		}
	}

	[EnemyAction(displayName = "Tuan/Skill2_ShotThunder")]
	public void Skill2_ShotThunder(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill2_TimeDelay = timeDelay;
		this.skill2_UbhSpiral.m_bulletNum = numberBullet;
		this.skill2_UbhSpiral.m_bulletSpeed = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill3_ShotPhoenix")]
	public void Skill3_ShotPhoenix(float timeDelay, int numberBullet, float speedBullet, float timeFollow)
	{
		this.skill3_TimeDelay = timeDelay;
		this.skill3_NumberBullet = numberBullet;
		this.skill3_SpeedBullet = speedBullet;
		this.skill3_TimeFollow = timeFollow;
	}

	[EnemyAction(displayName = "Tuan/Skill4_ShotFail")]
	public void Skill4_ShotFail(float timeDelay, int speedBullet)
	{
		this.skill4_TimeDelay = timeDelay;
		this.skill4_Linear.m_bulletSpeed = (float)speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill5_Flame")]
	public void Skill5_Flame(float timeDelay)
	{
		this.skill5_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill6_Blink")]
	public void Skill6_Blink(float timeDelay)
	{
		this.skill6_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/FakeDie")]
	public void FakeDie()
	{
		this.isDie = true;
	}

	public override void Restart()
	{
		base.Restart();
		if (this.StatAngry != null)
		{
			this.StatAngry();
		}
		if (this.BaseStatBoss != null)
		{
			this.BaseStatBoss();
		}
		base.ShowHealthBar();
		base.SetNameBoss("Son of The Sun");
	}

	private void Update()
	{
		this.MoveStart();
	}

	private void OnEnable()
	{
		EazySoundManager.PlaySound(this.listSound[0], 0.3f);
	}

	private void RandomListID()
	{
		this.listID.Clear();
		while (this.listID.Count < 7)
		{
			int item = UnityEngine.Random.Range(0, 7);
			if (!this.listID.Contains(item))
			{
				this.listID.Add(item);
			}
		}
		if (this.lastIdSkill != this.listID[0])
		{
			this.lastIdSkill = this.listID[this.listID.Count - 1];
			this.RandomSkill();
		}
		else
		{
			this.RandomListID();
		}
	}

	private void RandomSkill()
	{
		if (this.currentTurnSkill < this.listID.Count)
		{
			this.Attack();
			this.currentTurnSkill++;
		}
		else
		{
			this.currentTurnSkill = 0;
			this.RandomListID();
		}
	}

	private void Attack()
	{
		switch (this.listID[this.currentTurnSkill])
		{
		case 0:
			base.StartCoroutine(this.AttackSkill0());
			break;
		case 1:
			base.StartCoroutine(this.AttackSkill1());
			break;
		case 2:
			base.StartCoroutine(this.AttackSkill2());
			break;
		case 3:
			base.StartCoroutine(this.AttackSkill3());
			break;
		case 4:
			base.StartCoroutine(this.AttackSkill4());
			break;
		case 5:
			base.StartCoroutine(this.AttackSkill5());
			break;
		case 6:
			base.StartCoroutine(this.AttackSkill6());
			break;
		}
	}

	private void ChangeAnim(string anim)
	{
		this.animationBoss.AnimationState.SetAnimation(0, anim, false).Complete += delegate(TrackEntry A_1)
		{
			if (!this.isActiveAngry)
			{
				this.animationBoss.AnimationState.SetAnimation(0, this.animIdle0, true);
			}
			else
			{
				this.animationBoss.AnimationState.SetAnimation(0, this.animIdle1, true);
			}
		};
	}

	private IEnumerator ChangeAnimDie()
	{
		this.smoothFollow.isSmoothFollow = false;
		if (this.SpawnCoin != null)
		{
			this.SpawnCoin();
		}
		EazySoundManager.PlaySound(this.listSound[2], 1f);
		CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
		this.animationBoss.AnimationState.SetAnimation(0, this.animDie, false).Complete += delegate(TrackEntry A_1)
		{
			this.Die(EnemyKilledBy.Player);
		};
		yield return new WaitForSeconds(1f);
		Fu.SpawnExplosion(this.dieExplosion, base.transform.position, Quaternion.identity);
		yield break;
	}

	private IEnumerator AttackSkill0()
	{
		yield return new WaitForSeconds(this.skill0_TimeDelay);
		this.ChangeAnim(this.animSkill0);
		yield return new WaitForSeconds(1.1f);
		this.skill0_Ubh.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[3], 1f);
		yield return new WaitForSeconds(0.6f);
		EazySoundManager.PlaySound(this.listSound[3], 1f);
		yield return new WaitForSeconds(1.4f);
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill1()
	{
		yield return new WaitForSeconds(this.skill1_TimeDelay);
		this.ChangeAnim(this.animSkill1);
		EazySoundManager.PlaySound(this.listSound[4], 1f);
		yield return new WaitForSeconds(0.3f);
		this.skill1_Ubh.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[5], 1f);
		yield return new WaitForSeconds(2f);
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill2()
	{
		yield return new WaitForSeconds(this.skill2_TimeDelay);
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid(new Vector3(0f, 4f));
		yield return new WaitForSeconds(1f);
		this.skill2_Ubh.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[6], 1f);
		yield return new WaitForSeconds(4.2f);
		EazySoundManager.PlaySound(this.listSound[7], 1f);
		this.ChangeAnim(this.animSkill1);
		yield return new WaitForSeconds(1f);
		this.smoothFollow.isSmoothFollow = true;
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill3()
	{
		yield return new WaitForSeconds(this.skill3_TimeDelay);
		EazySoundManager.PlaySound(this.listSound[0], 1f);
		EazySoundManager.PlaySound(this.listSound[8], 1f);
		for (int i = 0; i < this.skill3_NumberBullet; i++)
		{
			float[] listMoreX = new float[]
			{
				-0.2f,
				-0.1f,
				0f,
				0.1f,
				0.2f
			};
			int x = UnityEngine.Random.Range(0, 5);
			UbhBullet objBullett = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.skill3_BulletPhoenix, new Vector3(base.gameObject.transform.position.x + listMoreX[x], base.gameObject.transform.position.y + 0.5f, base.gameObject.transform.position.z), false);
			NewBoss_BulletHomming homingMissile = objBullett.GetComponent<NewBoss_BulletHomming>();
			homingMissile.speedMove = UnityEngine.Random.Range((float)((int)(this.skill3_SpeedBullet / 2f)), this.skill3_SpeedBullet);
			homingMissile.SetFollowTargetX(this.skill3_TimeFollow);
			yield return new WaitForSeconds(0.2f);
		}
		yield return new WaitForSeconds(2f);
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill4()
	{
		yield return new WaitForSeconds(this.skill4_TimeDelay);
		this.ChangeAnim(this.animSkill4);
		this.skill4_Warning.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		EazySoundManager.PlaySound(this.listSound[9], 1f);
		this.skill4_Ubh.StartShotRoutine();
		this.skill4_Warning.SetActive(false);
		yield return new WaitForSeconds(2f);
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill5()
	{
		yield return new WaitForSeconds(this.skill5_TimeDelay);
		this.ChangeAnim(this.animSkill5);
		this.skill4_Warning.SetActive(true);
		yield return new WaitForSeconds(1.5f);
		this.skill4_Warning.SetActive(false);
		this.skill5_Flame.SetActive(true);
		EazySoundManager.PlaySound(this.listSound[10], 1f);
		yield return new WaitForSeconds(1f);
		this.skill5_BoxFlame.enabled = true;
		this.skill1_Ubh.StartShotRoutine();
		yield return new WaitForSeconds(0.5f);
		EazySoundManager.PlaySound(this.listSound[10], 1f);
		yield return new WaitForSeconds(0.5f);
		this.skill1_Ubh.StartShotRoutine();
		yield return new WaitForSeconds(1f);
		EazySoundManager.PlaySound(this.listSound[10], 1f);
		this.skill1_Ubh.StartShotRoutine();
		yield return new WaitForSeconds(1f);
		this.skill1_Ubh.StartShotRoutine();
		yield return new WaitForSeconds(0.5f);
		this.skill5_BoxFlame.enabled = false;
		yield return new WaitForSeconds(1f);
		this.skill5_Flame.SetActive(false);
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill6()
	{
		yield return new WaitForSeconds(this.skill6_TimeDelay);
		this.ChangeAnim(this.animSkill6a);
		this.polyBox.enabled = false;
		base.StartCoroutine(this.Blink());
		EazySoundManager.PlaySound(this.listSound[11], 1f);
		yield return new WaitForSeconds(1f);
		this.ChangeAnim(this.animSkill6b);
		yield return new WaitForSeconds(1f);
		this.polyBox.enabled = true;
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private void MoveMid(Vector2 vt2)
	{
		this.pathMid = new Vector2[]
		{
			new Vector2(base.transform.position.x, base.transform.position.y),
			vt2
		};
		this.BaseMoveCurve(2f, Ease.Linear, PathMode.Ignore, this.pathMid);
	}

	private IEnumerator Blink()
	{
		float[] array = new float[3];
		array[0] = -2f;
		array[1] = 2f;
		float[] xx = array;
		float[] yy = new float[]
		{
			0f,
			2f,
			4f
		};
		for (int i = 0; i < 3; i++)
		{
			float x = xx[UnityEngine.Random.Range(0, 3)];
			float y = yy[UnityEngine.Random.Range(0, 3)];
			base.gameObject.transform.position = new Vector3(x, y, 0f);
			Fu.SpawnExplosion(this.skill6_FxBlink, base.transform.position, Quaternion.identity);
			this.skill0_Ubh.StartShotRoutine();
			yield return new WaitForSeconds(0.5f);
		}
		yield break;
	}

	private void MoveStart()
	{
		if (!this.isMoveStart)
		{
			base.gameObject.transform.position = Vector3.Lerp(base.gameObject.transform.position, new Vector3(0f, 5f, 0f), 0.01f);
			if (base.gameObject.transform.position.y >= 4f)
			{
				this.shield.SetActive(false);
				Fu.SpawnExplosion(this.skill6_FxBlink, base.transform.position, Quaternion.identity);
				this.isMoveStart = true;
				this.smoothFollow.enabled = true;
				this.polyBox.enabled = true;
			}
		}
	}

	private int GetRandomValue(params BossMode_ChimVang.RandomSelection[] selections)
	{
		float value = UnityEngine.Random.value;
		float num = 0f;
		foreach (BossMode_ChimVang.RandomSelection randomSelection in selections)
		{
			num += randomSelection.probability;
			if (value <= num)
			{
				return randomSelection.GetValue();
			}
		}
		return -1;
	}

	private bool isActiveAngry;

	private bool isDie;

	private float angryStat = 1f;

	[SerializeField]
	private SmoothFollow smoothFollow;

	[SerializeField]
	private PolygonCollider2D polyBox;

	[SerializeField]
	private GameObject shield;

	[SerializeField]
	private AudioClip[] listSound;

	[SerializeField]
	private SkeletonAnimation animationBoss;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animIdle0;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animIdle1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animDie;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill0;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill2;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill3;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill4;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill5;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill6a;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill6b;

	private float skill0_TimeDelay;

	[SerializeField]
	private UbhShotCtrl skill0_Ubh;

	[SerializeField]
	private UbhRandomSpiralShot[] skill0_UbhRandom;

	private float skill1_TimeDelay;

	[SerializeField]
	private UbhShotCtrl skill1_Ubh;

	[SerializeField]
	private UbhRandomShot[] skill1_UbhRandom;

	private float skill2_TimeDelay;

	[SerializeField]
	private UbhShotCtrl skill2_Ubh;

	[SerializeField]
	private UbhSpiralShot skill2_UbhSpiral;

	private float skill3_TimeDelay;

	private int skill3_NumberBullet = 5;

	private float skill3_TimeFollow = 3f;

	private float skill3_SpeedBullet = 6f;

	[SerializeField]
	private GameObject skill3_BulletPhoenix;

	private float skill4_TimeDelay;

	[SerializeField]
	private GameObject skill4_Warning;

	[SerializeField]
	private UbhShotCtrl skill4_Ubh;

	[SerializeField]
	private UbhLinearShot skill4_Linear;

	private float skill5_TimeDelay;

	[SerializeField]
	private GameObject skill5_Flame;

	[SerializeField]
	private BoxCollider2D skill5_BoxFlame;

	private float skill6_TimeDelay;

	[SerializeField]
	private GameObject skill6_FxBlink;

	[EnemyEventCustom(displayName = "BossTuan/BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

	[EnemyEventCustom(displayName = "BossTuan/StatAngry")]
	public EnemyEvent StatAngry;

	[EnemyEventCustom(displayName = "BossTuan/SpawnCoin")]
	public EnemyEvent SpawnCoin;

	public List<int> listID = new List<int>();

	private int currentTurnSkill;

	private int lastIdSkill = -1;

	private Vector2[] pathMid;

	private bool isMoveStart;

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
