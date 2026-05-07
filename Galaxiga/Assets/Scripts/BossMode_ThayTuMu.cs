using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Hellmade.Sound;
using SkyGameKit;
using Spine;
using Spine.Unity;
using UnityEngine;

public class BossMode_ThayTuMu : NewBoss
{
	[EnemyAction(displayName = "Tuan/StartAttack")]
	public void StartAttack()
	{
		this.FindRandomTarget();
		this.RandomSkill();
	}

	[EnemyAction(displayName = "Tuan/StatAngry")]
	public void ChangeStatAngry(float statAngry)
	{
		this.angryStat = statAngry;
		this.skeletonAnimation.timeScale = 1f + (1f - this.angryStat);
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry")]
	public void ActiveStatusAngry()
	{
		this.isActiveAngry = true;
		base.ShowRedScreen();
		EazySoundManager.PlaySound(this.listSound[1], 1f);
	}

	[EnemyAction(displayName = "Tuan/Skill0_ShotByStomach")]
	public void Skill0_ShotByStomach(float timeDelay, int numberBullet, float speedBullet, float timeFollow)
	{
		this.timedelay_Skill0 = timeDelay;
		this.numberBullet_Skill0 = numberBullet;
		this.speedBullet_Skill0 = speedBullet;
		this.timeFollow_Skill0 = timeFollow;
	}

	[EnemyAction(displayName = "Tuan/Skill1_Throw3Chain")]
	public void Skill1_Throw3Chain(float timeDelay, int numberBullet, float speedBullet)
	{
		this.timedelay_Skill1 = timeDelay;
		for (int i = 0; i < this.ubhRandomShot.Length; i++)
		{
			this.ubhRandomShot[i].m_bulletNum = numberBullet;
			this.ubhRandomShot[i].m_minSpeed = speedBullet;
			this.ubhRandomShot[i].m_maxSpeed = speedBullet + 1f;
		}
	}

	[EnemyAction(displayName = "Tuan/Skill2_Laser3Chain")]
	public void Skill2_Laser3Chain(float timeDelay, int numberBullet, float speedBullet)
	{
		this.timedelay_Skill2 = timeDelay;
		for (int i = 0; i < this.ubhHoleCircleShots.Length; i++)
		{
			this.ubhHoleCircleShots[i].m_bulletNum = numberBullet;
			this.ubhHoleCircleShots[i].m_bulletSpeed = speedBullet;
		}
	}

	[EnemyAction(displayName = "Tuan/Skill3_Attack3Chain")]
	public void Skill3_Attack3Chain(float timeDelay)
	{
		this.timedelay_Skill3 = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill4_Spin6Chain")]
	public void Skill4_Spin6Chain(float timeDelay)
	{
		this.timedelay_Skill4 = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill5_Throw6Chain")]
	public void Skill5_Throw6Chain(float timeDelay)
	{
		this.timedelay_Skill5 = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill6_Laser6Chain")]
	public void Skill6_Laser6Chain(float timeDelay)
	{
		this.timedelay_Skill6 = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill7_Attack6Chain")]
	public void Skill7_Attack6Chain(float timeDelay)
	{
		this.timedelay_Skill7 = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill8_Drop6Chain")]
	public void Skill8_Drop6Chain(float timeDelay)
	{
		this.timedelay_Skill8 = timeDelay;
	}

	public override void Restart()
	{
		base.Restart();
		if (this.BaseStatBoss != null)
		{
			this.BaseStatBoss();
		}
		base.ShowHealthBar();
		base.SetNameBoss("Devil Monk");
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
		{
			base.StartCoroutine(this.AttackSkill0());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha2))
		{
			base.StartCoroutine(this.AttackSkill1());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha3))
		{
			base.StartCoroutine(this.AttackSkill2());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha4))
		{
			base.StartCoroutine(this.AttackSkill3());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha5))
		{
			base.StartCoroutine(this.AttackSkill4());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha6))
		{
			base.StartCoroutine(this.AttackSkill5());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha7))
		{
			base.StartCoroutine(this.AttackSkill6());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha8))
		{
			base.StartCoroutine(this.AttackSkill7());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha9))
		{
			base.StartCoroutine(this.AttackSkill8());
		}
	}

	private void OnEnable()
	{
		this.trans = base.gameObject.transform;
		EazySoundManager.PlaySound(this.listSound[0], 0.3f);
	}

	public void FindRandomTarget()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, this.rangeAttack, this.layerPlayer);
		if (array.Length > 0)
		{
			GameObject gameObject = array[UnityEngine.Random.Range(0, array.Length)].gameObject;
			this.player = gameObject.transform;
		}
	}

	private void RandomListID()
	{
		this.listID.Clear();
		if (!this.isActiveAngry)
		{
			while (this.listID.Count < 4)
			{
				int item = UnityEngine.Random.Range(0, 4);
				if (!this.listID.Contains(item))
				{
					this.listID.Add(item);
				}
			}
		}
		else
		{
			while (this.listID.Count < 9)
			{
				int item2 = UnityEngine.Random.Range(0, 9);
				if (!this.listID.Contains(item2))
				{
					this.listID.Add(item2);
				}
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
		case 7:
			base.StartCoroutine(this.AttackSkill7());
			break;
		case 8:
			base.StartCoroutine(this.AttackSkill8());
			break;
		}
	}

	private IEnumerator ChangeAnim(string anim)
	{
		this.skeletonAnimation.AnimationState.SetAnimation(0, anim, false).Complete += delegate(TrackEntry A_1)
		{
			if (!this.isActiveAngry)
			{
				this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle0, true);
			}
			else
			{
				this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle1, true);
			}
			this.RandomSkill();
		};
		yield return null;
		yield break;
	}

	private IEnumerator AttackSkill0()
	{
		yield return new WaitForSeconds(this.timedelay_Skill0);
		base.StartCoroutine(this.ChangeAnim(this.animSkill0));
		yield return new WaitForSeconds(1.3f * this.angryStat);
		for (int i = 0; i < this.numberBullet_Skill0; i++)
		{
			EazySoundManager.PlaySound(this.listSound[3], 1f);
			UbhBullet objBullett = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.bullet_Skill0, new Vector3(this.trans.position.x, this.trans.position.y + 2f, this.trans.position.z), false);
			UbhBullet objBullett2 = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.bullet_Skill0, new Vector3(this.trans.position.x - 1.5f, this.trans.position.y + 1.5f, this.trans.position.z), false);
			UbhBullet objBullett3 = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.bullet_Skill0, new Vector3(this.trans.position.x + 1.5f, this.trans.position.y + 1.5f, this.trans.position.z), false);
			NewBoss_BulletHomming homingMissile = objBullett.GetComponent<NewBoss_BulletHomming>();
			NewBoss_BulletHomming homingMissile2 = objBullett2.GetComponent<NewBoss_BulletHomming>();
			NewBoss_BulletHomming homingMissile3 = objBullett3.GetComponent<NewBoss_BulletHomming>();
			homingMissile.SetFollowTargetX(this.timeFollow_Skill0);
			homingMissile2.SetFollowTargetX(this.timeFollow_Skill0);
			homingMissile3.SetFollowTargetX(this.timeFollow_Skill0);
			homingMissile.speedMove = this.speedBullet_Skill0;
			homingMissile2.speedMove = this.speedBullet_Skill0;
			homingMissile3.speedMove = this.speedBullet_Skill0;
			yield return new WaitForSeconds(0.3f * this.angryStat);
		}
		yield break;
	}

	private IEnumerator AttackSkill1()
	{
		this.ubh_Skill1.m_shotList[0].m_afterDelay = 2f * this.angryStat;
		this.ubh_Skill1.m_shotList[1].m_afterDelay = 1.7f * this.angryStat;
		yield return new WaitForSeconds(this.timedelay_Skill1);
		EazySoundManager.PlaySound(this.listSound[5], 1f);
		this.warning_Skill1.SetActive(true);
		base.StartCoroutine(this.ChangeAnim(this.animSkill1));
		yield return new WaitForSeconds(1.2f * this.angryStat);
		this.ubh_Skill1.StartShotRoutine();
		this.warning_Skill1.SetActive(false);
		for (int i = 0; i < 3; i++)
		{
			EazySoundManager.PlaySound(this.listSound[4], 1f);
			yield return new WaitForSeconds(2f * this.angryStat);
		}
		yield break;
	}

	private IEnumerator AttackSkill2()
	{
		for (int j = 0; j < 5; j++)
		{
			this.ubh_Skill2.m_shotList[j].m_afterDelay = 1f * this.angryStat;
		}
		yield return new WaitForSeconds(this.timedelay_Skill2);
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid(new Vector2(0f, 0f));
		yield return new WaitForSeconds(1f * this.angryStat);
		EazySoundManager.PlaySound(this.listSound[7], 1f);
		base.StartCoroutine(this.ChangeAnim(this.animSkill2));
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.warning_Skill2.SetActive(true);
		yield return new WaitForSeconds(1f * this.angryStat);
		this.ubh_Skill2.StartShotRoutine();
		this.warning_Skill2.SetActive(false);
		this.obj_LaserSkill2.SetActive(true);
		for (int i = 0; i < 5; i++)
		{
			EazySoundManager.PlaySound(this.listSound[6], 1f);
			yield return new WaitForSeconds(1.4f * this.angryStat);
		}
		this.obj_LaserSkill2.SetActive(false);
		this.smoothFollow.isSmoothFollow = true;
		yield break;
	}

	private IEnumerator AttackSkill3()
	{
		EazySoundManager.PlaySound(this.listSound[9], 1f);
		yield return new WaitForSeconds(this.timedelay_Skill3);
		this.warning_Skill3.SetActive(true);
		base.StartCoroutine(this.ChangeAnim(this.animSkill3));
		yield return new WaitForSeconds(1.6f * this.angryStat);
		EazySoundManager.PlaySound(this.listSound[8], 1f);
		for (int i = 0; i < 3; i++)
		{
			this.box_IronNails0[i].SetActive(false);
		}
		yield return new WaitForSeconds(0.1f * this.angryStat);
		for (int j = 0; j < 3; j++)
		{
			this.box_IronNails1[j].SetActive(true);
		}
		this.warning_Skill3.SetActive(false);
		CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
		Fu.SpawnExplosion(this.fx_ExplosiveSkill3, new Vector3(this.trans.position.x, this.trans.position.y - 7.5f, this.trans.position.z), Quaternion.identity);
		yield return new WaitForSeconds(0.6f * this.angryStat);
		for (int k = 0; k < 3; k++)
		{
			this.box_IronNails1[k].SetActive(false);
			this.box_IronNails0[k].SetActive(true);
		}
		yield break;
	}

	private IEnumerator AttackSkill4()
	{
		yield return new WaitForSeconds(this.timedelay_Skill4);
		this.warning_Skill4.SetActive(true);
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid(new Vector2(0f, 1f));
		yield return new WaitForSeconds(1f * this.angryStat);
		EazySoundManager.PlaySound(this.listSound[11], 1f);
		base.StartCoroutine(this.ChangeAnim(this.animSkill4));
		yield return new WaitForSeconds(1f * this.angryStat);
		this.warning_Skill4.SetActive(false);
		for (int i = 0; i < 2; i++)
		{
			EazySoundManager.PlaySound(this.listSound[10], 1f);
			yield return new WaitForSeconds(1.5f * this.angryStat);
		}
		yield return new WaitForSeconds(3f * this.angryStat);
		this.smoothFollow.isSmoothFollow = true;
		yield break;
	}

	private IEnumerator AttackSkill5()
	{
		yield return new WaitForSeconds(this.timedelay_Skill5);
		base.StartCoroutine(this.ChangeAnim(this.animSkill5));
		this.warning_Skill5[0].SetActive(true);
		this.warning_Skill5[1].SetActive(true);
		this.warning_Skill5[2].SetActive(true);
		EazySoundManager.PlaySound(this.listSound[13], 1f);
		yield return new WaitForSeconds(1f * this.angryStat);
		EazySoundManager.PlaySound(this.listSound[12], 1f);
		this.warning_Skill5[0].SetActive(false);
		this.warning_Skill5[1].SetActive(false);
		this.warning_Skill5[2].SetActive(false);
		this.warning_Skill5[3].SetActive(true);
		this.warning_Skill5[4].SetActive(true);
		this.warning_Skill5[5].SetActive(true);
		yield return new WaitForSeconds(1f * this.angryStat);
		EazySoundManager.PlaySound(this.listSound[12], 1f);
		this.warning_Skill5[3].SetActive(false);
		this.warning_Skill5[4].SetActive(false);
		this.warning_Skill5[5].SetActive(false);
		for (int i = 0; i < 3; i++)
		{
			yield return new WaitForSeconds(0.9f * this.angryStat);
			EazySoundManager.PlaySound(this.listSound[12], 1f);
		}
		yield break;
	}

	private IEnumerator AttackSkill6()
	{
		yield return new WaitForSeconds(this.timedelay_Skill6);
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid(new Vector2(0f, 1f));
		yield return new WaitForSeconds(1f * this.angryStat);
		EazySoundManager.PlaySound(this.listSound[15], 1f);
		base.StartCoroutine(this.ChangeAnim(this.animSkill6));
		yield return new WaitForSeconds(0.8f * this.angryStat);
		for (int i = 0; i < 6; i++)
		{
			this.warning_Skill6[i].SetActive(true);
			yield return new WaitForSeconds(0.1f * this.angryStat);
		}
		for (int j = 0; j < 6; j++)
		{
			this.warning_Skill6[j].SetActive(false);
			this.obj_LaserSkill6[j].SetActive(true);
			EazySoundManager.PlaySound(this.listSound[14], 1f);
			if (j > 0)
			{
				this.obj_LaserSkill6[j - 1].SetActive(false);
			}
			yield return new WaitForSeconds(0.2f * this.angryStat);
			if (j == 5)
			{
				this.obj_LaserSkill6[j].SetActive(false);
			}
		}
		EazySoundManager.PlaySound(this.listSound[15], 1f);
		yield return new WaitForSeconds(0.3f * this.angryStat);
		for (int k = 5; k >= 0; k--)
		{
			yield return new WaitForSeconds(0.1f * this.angryStat);
			this.warning_Skill6[k].SetActive(true);
		}
		for (int l = 5; l >= 0; l--)
		{
			this.warning_Skill6[l].SetActive(false);
			if (l > 0)
			{
				this.obj_LaserSkill6[l].SetActive(true);
				EazySoundManager.PlaySound(this.listSound[14], 1f);
			}
			else
			{
				yield return new WaitForSeconds(0.2f * this.angryStat);
				this.obj_LaserSkill6[l].SetActive(true);
				EazySoundManager.PlaySound(this.listSound[14], 1f);
			}
			if (l < 5)
			{
				this.obj_LaserSkill6[l + 1].SetActive(false);
			}
			yield return new WaitForSeconds(0.2f * this.angryStat);
			if (l == 0)
			{
				this.obj_LaserSkill6[l].SetActive(false);
			}
		}
		yield return new WaitForSeconds(2f * this.angryStat);
		this.smoothFollow.isSmoothFollow = true;
		yield break;
	}

	private IEnumerator AttackSkill7()
	{
		yield return new WaitForSeconds(this.timedelay_Skill7);
		EazySoundManager.PlaySound(this.listSound[17], 1f);
		base.StartCoroutine(this.ChangeAnim(this.animSkill7));
		yield return new WaitForSeconds(0.4f * this.angryStat);
		this.warning_Skill7[0].SetActive(true);
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.warning_Skill7[1].SetActive(true);
		this.warning_Skill7[2].SetActive(true);
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.warning_Skill7[3].SetActive(true);
		this.warning_Skill7[4].SetActive(true);
		for (int i = 0; i < this.box_IronNails0.Length; i++)
		{
			this.box_IronNails0[i].SetActive(false);
		}
		yield return new WaitForSeconds(0.5f * this.angryStat);
		for (int j = 0; j < this.box_IronNails1.Length; j++)
		{
			this.box_IronNails1[j].SetActive(true);
		}
		EazySoundManager.PlaySound(this.listSound[16], 1f);
		this.warning_Skill7[0].SetActive(false);
		Fu.SpawnExplosion(this.fx_ExplosiveSkill6, this.listPosIron[2].position, Quaternion.identity);
		EazySoundManager.PlaySound(this.listSound[18], 1f);
		yield return new WaitForSeconds(0.2f * this.angryStat);
		this.warning_Skill7[1].SetActive(false);
		this.warning_Skill7[2].SetActive(false);
		Fu.SpawnExplosion(this.fx_ExplosiveSkill6, this.listPosIron[1].position, Quaternion.identity);
		Fu.SpawnExplosion(this.fx_ExplosiveSkill6, this.listPosIron[3].position, Quaternion.identity);
		EazySoundManager.PlaySound(this.listSound[18], 1f);
		yield return new WaitForSeconds(0.2f * this.angryStat);
		this.warning_Skill7[3].SetActive(false);
		this.warning_Skill7[4].SetActive(false);
		Fu.SpawnExplosion(this.fx_ExplosiveSkill6, this.listPosIron[0].position, Quaternion.identity);
		Fu.SpawnExplosion(this.fx_ExplosiveSkill6, this.listPosIron[4].position, Quaternion.identity);
		EazySoundManager.PlaySound(this.listSound[18], 1f);
		yield return new WaitForSeconds(1f * this.angryStat);
		for (int k = 0; k < this.box_IronNails0.Length; k++)
		{
			this.box_IronNails0[k].SetActive(true);
			this.box_IronNails1[k].SetActive(false);
		}
		yield break;
	}

	private IEnumerator AttackSkill8()
	{
		yield return new WaitForSeconds(this.timedelay_Skill8);
		this.warning_Skill8.SetActive(true);
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid(new Vector2(0f, 1f));
		EazySoundManager.PlaySound(this.listSound[20], 1f);
		base.StartCoroutine(this.ChangeAnim(this.animSkill8));
		for (int k = 0; k < this.box_IronNails0.Length; k++)
		{
			this.box_IronNails0[k].SetActive(false);
			this.box_IronNails2[k].SetActive(true);
		}
		yield return new WaitForSeconds(2f * this.angryStat);
		for (int l = 0; l < this.fx_RocketSkill8.Length; l++)
		{
			this.fx_RocketSkill8[l].SetActive(true);
		}
		yield return new WaitForSeconds(1f * this.angryStat);
		this.warning_Skill8.SetActive(false);
		for (int i = 0; i < 3; i++)
		{
			EazySoundManager.PlaySound(this.listSound[19], 1f);
			yield return new WaitForSeconds(0.2f * this.angryStat);
		}
		yield return new WaitForSeconds(0.8f * this.angryStat);
		for (int j = 0; j < 3; j++)
		{
			EazySoundManager.PlaySound(this.listSound[19], 1f);
			yield return new WaitForSeconds(0.2f * this.angryStat);
		}
		this.smoothFollow.isSmoothFollow = true;
		yield return new WaitForSeconds(0.5f * this.angryStat);
		for (int m = 0; m < this.fx_RocketSkill8.Length; m++)
		{
			this.fx_RocketSkill8[m].SetActive(false);
			this.box_IronNails0[m].SetActive(false);
			this.box_IronNails2[m].SetActive(false);
		}
		yield return new WaitForSeconds(1f * this.angryStat);
		for (int n = 0; n < this.fx_RocketSkill8.Length; n++)
		{
			this.box_IronNails0[n].SetActive(true);
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
		this.BaseMoveCurve(1.5f, Ease.Linear, PathMode.Ignore, this.pathMid);
	}

	private int GetRandomValue(params BossMode_ThayTuMu.RandomSelection[] selections)
	{
		float value = UnityEngine.Random.value;
		float num = 0f;
		foreach (BossMode_ThayTuMu.RandomSelection randomSelection in selections)
		{
			num += randomSelection.probability;
			if (value <= num)
			{
				return randomSelection.GetValue();
			}
		}
		return -1;
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		EazySoundManager.PlaySound(this.listSound[2], 1f);
	}

	private bool isActiveAngry;

	private float angryStat = 1f;

	private Transform trans;

	[SerializeField]
	private SmoothFollow smoothFollow;

	[SerializeField]
	private PolygonCollider2D polygonCollider2D;

	[SerializeField]
	private Transform[] listPosIron;

	[SerializeField]
	private GameObject[] box_IronNails0;

	[SerializeField]
	private GameObject[] box_IronNails1;

	[SerializeField]
	private GameObject[] box_IronNails2;

	[SerializeField]
	private AudioClip[] listSound;

	[SerializeField]
	private SkeletonAnimation skeletonAnimation;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animIdle0;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animIdle1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animTransform;

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
	private string animSkill6;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill7;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill8;

	private float timedelay_Skill0;

	private int numberBullet_Skill0 = 3;

	private float speedBullet_Skill0 = 6f;

	private float timeFollow_Skill0 = 2f;

	[SerializeField]
	private GameObject bullet_Skill0;

	private float timedelay_Skill1;

	[SerializeField]
	private UbhShotCtrl ubh_Skill1;

	[SerializeField]
	private UbhRandomShot[] ubhRandomShot;

	[SerializeField]
	private GameObject warning_Skill1;

	private float timedelay_Skill2;

	[SerializeField]
	private GameObject obj_LaserSkill2;

	[SerializeField]
	private GameObject warning_Skill2;

	[SerializeField]
	private UbhShotCtrl ubh_Skill2;

	[SerializeField]
	private UbhHoleCircleShot[] ubhHoleCircleShots;

	private float timedelay_Skill3;

	[SerializeField]
	private GameObject fx_ExplosiveSkill3;

	[SerializeField]
	private GameObject warning_Skill3;

	private float timedelay_Skill4;

	[SerializeField]
	private GameObject warning_Skill4;

	private float timedelay_Skill5;

	[SerializeField]
	private GameObject[] warning_Skill5;

	private float timedelay_Skill6;

	[SerializeField]
	private GameObject[] obj_LaserSkill6;

	[SerializeField]
	private GameObject[] warning_Skill6;

	[SerializeField]
	private GameObject fx_ExplosiveSkill6;

	private float timedelay_Skill7;

	[SerializeField]
	private GameObject[] warning_Skill7;

	private float timedelay_Skill8;

	[SerializeField]
	private GameObject[] fx_RocketSkill8;

	[SerializeField]
	private GameObject warning_Skill8;

	[EnemyEventCustom(displayName = "BossTuan/BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

	private float rangeAttack = 30f;

	public LayerMask layerPlayer;

	public Transform player;

	public List<int> listID = new List<int>();

	private int currentTurnSkill;

	private int lastIdSkill = -1;

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
