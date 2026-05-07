using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Hellmade.Sound;
using SkyGameKit;
using Spine;
using Spine.Unity;
using SWS;
using UnityEngine;

public class BossMode_CuaDo : NewBoss
{
	[EnemyAction(displayName = "Tuan/StartAttack")]
	public void StartAttack()
	{
		this.RandomSkill();
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry")]
	public void ActiveStatusAngry()
	{
		if (this.idStatus == 0)
		{
			base.ShowRedScreen();
		}
		this.isTransforming = true;
		EazySoundManager.PlaySound(this.listSound[1], 1f);
	}

	[EnemyAction(displayName = "Tuan/Skill0_ShotByGuns")]
	public void Skill0_ShotByGuns(float timeDelay, float speedBulletStatus0, float speedBulletStatus1, float speedBulletStatus2)
	{
		this.skill0_timeDelay = timeDelay;
		this.skill0_UbhLinear[0].m_bulletSpeed = speedBulletStatus0;
		this.skill0_UbhLinear[1].m_bulletSpeed = speedBulletStatus0;
		this.skill0_UbhLinear[2].m_bulletSpeed = speedBulletStatus1;
		this.skill0_UbhLinear[3].m_bulletSpeed = speedBulletStatus1;
		this.skill0_speedRocket = speedBulletStatus2;
	}

	[EnemyAction(displayName = "Tuan/Skill1_ShotByMouth")]
	public void Skill1_ShotByMouth(float timeDelay, float speedBigRocket0, float speedBullet1, float speedBullet2)
	{
		this.skill1_timeDelay = timeDelay;
		this.skill1_speedBigBullet = speedBigRocket0;
		this.skill1_UbhLinear[0].m_bulletSpeed = speedBullet1;
		this.skill1_UbhLinear[1].m_bulletSpeed = speedBullet1;
		this.skill1_UbhLinear[2].m_bulletSpeed = speedBullet1;
		this.skill1_UbhHoleCircle[0].m_bulletSpeed = speedBullet2;
		this.skill1_UbhHoleCircle[1].m_bulletSpeed = speedBullet2;
	}

	[EnemyAction(displayName = "Tuan/Skill2_ThrowLeftHand")]
	public void Skill2_ThrowLeftHand(float timeDelay, float speed)
	{
		this.timeDelay_ThrowLeftHand = timeDelay;
		this.speed_ThrowLeftHand = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill3_ThrowRightHand")]
	public void Skill3_ThrowRightHand(float timeDelay, float speed)
	{
		this.timeDelay_ThrowRightHand = timeDelay;
		this.speed_ThrowRightHand = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill4_ThrowTwoHand")]
	public void Skill4_ThrowTwoHand(float timeDelay, float speed)
	{
		this.timeDelay_ThrowTwoHand = timeDelay;
		this.speed_ThrowTwoHand = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill5_Dash")]
	public void Skill5_Dash(float timeDelay, float speed)
	{
		this.timeDelay_Dash = timeDelay;
		this.speed_Dash = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill6_ShotByTail")]
	public void Skill6_ShotByTail(float timeDelay, int numberBullet, float timeFollow)
	{
		this.timeDelay_ShotByTail = timeDelay;
		this.numberBullet_ShoyByTail = numberBullet;
		this.timeFollow_Rocket = timeFollow;
	}

	public override void Restart()
	{
		base.Restart();
		if (this.BaseStatBoss != null)
		{
			this.BaseStatBoss();
		}
		base.ShowHealthBar();
		base.SetNameBoss("Hell Crab");
	}

	private void Update()
	{
		if (this.player != null && !this.isBlind)
		{
			Vector3 vector = this.player.position - base.transform.position;
			vector.Normalize();
			float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(0f, 0f, num + 90f), Time.deltaTime * 2f);
		}
	}

	private void OnEnable()
	{
		this.FindRandomTarget();
		EazySoundManager.PlaySound(this.listSound[0], 1f);
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
			if (this.idStatus == 0)
			{
				base.StartCoroutine(this.AttackShotByGuns());
			}
			else if (this.idStatus == 1)
			{
				base.StartCoroutine(this.AttackShotByGuns1());
			}
			else if (this.idStatus == 2)
			{
				base.StartCoroutine(this.AttackShotByGuns2());
			}
			break;
		case 1:
			if (this.idStatus == 0)
			{
				base.StartCoroutine(this.AttackShotByMouth());
			}
			else if (this.idStatus == 1)
			{
				base.StartCoroutine(this.AttackShotByMouth1());
			}
			else if (this.idStatus == 2)
			{
				base.StartCoroutine(this.AttackShotByMouth2());
			}
			break;
		case 2:
			base.StartCoroutine(this.AttackThrowLeftHand());
			break;
		case 3:
			base.StartCoroutine(this.AttackThrowRightHand());
			break;
		case 4:
			base.StartCoroutine(this.AttackThrowTwoHand());
			break;
		case 5:
			base.StartCoroutine(this.AttackDash());
			break;
		case 6:
			base.StartCoroutine(this.AttackShotByTail());
			break;
		}
	}

	private void ChangeAnim(string anim)
	{
		this.skeletonAnimation.AnimationState.SetAnimation(0, anim, false).Complete += delegate(TrackEntry A_1)
		{
			if (this.idStatus == 0)
			{
				this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle0, true);
			}
			else if (this.idStatus == 1)
			{
				this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle1, true);
			}
			else if (this.idStatus == 1)
			{
				this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle2, true);
			}
			if (this.isTransforming)
			{
				this.Upgrade();
			}
			else
			{
				this.RandomSkill();
			}
		};
	}

	private void Upgrade()
	{
		this.isBlind = true;
		string empty = string.Empty;
		if (this.idStatus == 0)
		{
			empty = this.animUpgrade0;
		}
		else if (this.idStatus == 1)
		{
			empty = this.animUpgrade1;
		}
		else if (this.idStatus == 2)
		{
			empty = this.animUpgrade2;
		}
		this.skeletonAnimation.AnimationState.SetAnimation(0, empty, false).Complete += delegate(TrackEntry A_1)
		{
			this.isBlind = false;
			this.isTransforming = false;
			this.idStatus++;
			if (this.idStatus == 0)
			{
				this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle0, true);
			}
			else if (this.idStatus == 1)
			{
				this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle1, true);
			}
			else if (this.idStatus == 2)
			{
				this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle2, true);
			}
			this.RandomSkill();
		};
	}

	private IEnumerator AttackShotByGuns()
	{
		yield return new WaitForSeconds(this.skill0_timeDelay * this.angryStat);
		this.skill0_Warning[0].SetActive(true);
		this.skill0_Warning[1].SetActive(true);
		this.skeletonAnimation.AnimationName = this.animShotByGuns;
		this.ChangeAnim(this.animShotByGuns);
		EazySoundManager.PlaySound(this.listSound[3], 1f);
		yield return new WaitForSeconds(1.5f * this.angryStat);
		this.skill0_UbhLinear[0].m_angle = 170f + base.gameObject.transform.eulerAngles.z;
		this.skill0_UbhLinear[1].m_angle = 170f + base.gameObject.transform.eulerAngles.z;
		EazySoundManager.PlaySound(this.listSound[4], 1f);
		this.skill0_Ubh0.StartShotRoutine();
		this.skill0_FxShot[0].SetActive(true);
		this.skill0_FxShot[1].SetActive(true);
		yield return new WaitForSeconds(0.3f * this.angryStat);
		this.skill0_FxShot[0].SetActive(false);
		this.skill0_FxShot[1].SetActive(false);
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.skill0_Warning[0].SetActive(false);
		this.skill0_Warning[1].SetActive(false);
		this.skill0_UbhLinear[0].m_angle = 220f + base.gameObject.transform.eulerAngles.z;
		this.skill0_UbhLinear[1].m_angle = 220f + base.gameObject.transform.eulerAngles.z;
		EazySoundManager.PlaySound(this.listSound[4], 1f);
		this.skill0_Ubh0.StartShotRoutine();
		this.skill0_FxShot[0].SetActive(true);
		this.skill0_FxShot[1].SetActive(true);
		yield return new WaitForSeconds(0.3f * this.angryStat);
		this.skill0_FxShot[0].SetActive(false);
		this.skill0_FxShot[1].SetActive(false);
		yield break;
	}

	private IEnumerator AttackShotByGuns1()
	{
		yield return new WaitForSeconds(this.skill0_timeDelay * this.angryStat);
		this.ChangeAnim(this.animShotByGuns1);
		yield return new WaitForSeconds(0.5f * this.angryStat);
		for (int i = 0; i < 4; i++)
		{
			this.skill0_UbhLinear[2].m_angle = base.gameObject.transform.eulerAngles.z + 150f;
			this.skill0_UbhLinear[3].m_angle = base.gameObject.transform.eulerAngles.z + 210f;
			this.skill0_Ubh1.StartShotRoutine();
			yield return new WaitForSeconds(0.1f * this.angryStat);
		}
		yield return new WaitForSeconds(0.5f * this.angryStat);
		for (int j = 0; j < 4; j++)
		{
			this.skill0_UbhLinear[2].m_angle = base.gameObject.transform.eulerAngles.z + 200f;
			this.skill0_UbhLinear[3].m_angle = base.gameObject.transform.eulerAngles.z + 160f;
			this.skill0_Ubh1.StartShotRoutine();
			yield return new WaitForSeconds(0.1f * this.angryStat);
		}
		yield break;
	}

	private IEnumerator AttackShotByGuns2()
	{
		yield return new WaitForSeconds(this.skill0_timeDelay * this.angryStat);
		this.ChangeAnim(this.animShotByGuns2);
		yield return new WaitForSeconds(0.6f * this.angryStat);
		for (int i = 0; i < 4; i++)
		{
			this.bfp_ThrowHand.Attack(6 + i, 7, this.skill0_speedRocket, this.player.transform, true);
			this.bfp_ThrowHand.Attack(10 + i, 7, this.skill0_speedRocket, this.player.transform, true);
			this.bfp_ThrowHand.Attack(14 + i, 7, this.skill0_speedRocket, this.player.transform, true);
			this.bfp_ThrowHand.Attack(18 + i, 7, this.skill0_speedRocket, this.player.transform, true);
			this.skill0_FxShot2[0].SetActive(true);
			this.skill0_FxShot2[1].SetActive(true);
			this.skill0_FxShot2[2].SetActive(true);
			this.skill0_FxShot2[3].SetActive(true);
			yield return new WaitForSeconds(0.3f * this.angryStat);
			this.skill0_FxShot2[0].SetActive(false);
			this.skill0_FxShot2[1].SetActive(false);
			this.skill0_FxShot2[2].SetActive(false);
			this.skill0_FxShot2[3].SetActive(false);
		}
		yield return new WaitForSeconds(0.5f * this.angryStat);
		yield break;
	}

	private IEnumerator AttackShotByMouth()
	{
		yield return new WaitForSeconds(this.skill1_timeDelay * this.angryStat);
		this.skill1_Warning[0].SetActive(true);
		this.skill1_Warning[1].SetActive(true);
		this.skill1_Warning[2].SetActive(true);
		this.ChangeAnim(this.animShotByMouth);
		EazySoundManager.PlaySound(this.listSound[5], 1f);
		yield return new WaitForSeconds(1.4f * this.angryStat);
		this.skill1_UbhRandomSpriral0.m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill1_Ubh0.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[6], 1f);
		this.bfp_ThrowHand.Attack(0, 2, this.skill1_speedBigBullet, null, false);
		this.bfp_ThrowHand.Attack(1, 2, this.skill1_speedBigBullet, null, false);
		this.skill1_Warning[0].SetActive(false);
		this.skill1_Warning[1].SetActive(false);
		this.skill1_Warning[2].SetActive(false);
		yield break;
	}

	private IEnumerator AttackShotByMouth1()
	{
		yield return new WaitForSeconds(this.skill1_timeDelay * this.angryStat);
		this.skill1_Warning[0].SetActive(true);
		this.ChangeAnim(this.animShotByMouth1);
		EazySoundManager.PlaySound(this.listSound[5], 1f);
		yield return new WaitForSeconds(1.4f * this.angryStat);
		for (int i = 0; i < 10; i++)
		{
			this.skill1_UbhLinear[0].m_angle = base.gameObject.transform.eulerAngles.z + 180f;
			this.skill1_UbhLinear[1].m_angle = base.gameObject.transform.eulerAngles.z + 90f;
			this.skill1_UbhLinear[2].m_angle = base.gameObject.transform.eulerAngles.z + 270f;
			this.skill1_Ubh1.StartShotRoutine();
			yield return new WaitForSeconds(0.05f);
		}
		EazySoundManager.PlaySound(this.listSound[6], 1f);
		this.skill1_Warning[0].SetActive(false);
		yield break;
	}

	private IEnumerator AttackShotByMouth2()
	{
		yield return new WaitForSeconds(this.skill1_timeDelay * this.angryStat);
		this.skill1_Warning[0].SetActive(true);
		this.ChangeAnim(this.animShotByMouth2);
		EazySoundManager.PlaySound(this.listSound[5], 1f);
		yield return new WaitForSeconds(1.4f * this.angryStat);
		this.skill1_UbhRandomSpriral2.m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill1_Ubh2.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[6], 1f);
		this.skill1_Warning[0].SetActive(false);
		yield break;
	}

	private IEnumerator AttackThrowLeftHand()
	{
		yield return new WaitForSeconds(this.timeDelay_ThrowLeftHand * this.angryStat);
		this.warning_ThrowLeftHand.SetActive(true);
		if (this.idStatus == 0)
		{
			this.ChangeAnim(this.animThrowLeftHand);
		}
		else if (this.idStatus == 1)
		{
			this.ChangeAnim(this.animThrowLeftHand1);
		}
		else if (this.idStatus == 2)
		{
			this.ChangeAnim(this.animThrowLeftHand2);
		}
		EazySoundManager.PlaySound(this.listSound[7], 1f);
		yield return new WaitForSeconds(0.7f * this.angryStat);
		this.isBlind = true;
		yield return new WaitForSeconds(0.3f * this.angryStat);
		this.isBlind = false;
		if (this.idStatus == 0)
		{
			this.bfp_ThrowHand.Attack(2, 0, this.speed_ThrowLeftHand, this.player.transform, true);
		}
		else if (this.idStatus == 1)
		{
			this.bfp_ThrowHand.Attack(2, 3, this.speed_ThrowLeftHand, this.player.transform, true);
		}
		else if (this.idStatus == 2)
		{
			this.bfp_ThrowHand.Attack(2, 5, this.speed_ThrowLeftHand, this.player.transform, true);
		}
		EazySoundManager.PlaySound(this.listSound[8], 1f);
		this.warning_ThrowLeftHand.SetActive(false);
		yield break;
	}

	private IEnumerator AttackThrowRightHand()
	{
		yield return new WaitForSeconds(this.timeDelay_ThrowRightHand * this.angryStat);
		this.warning_ThrowRightHand.SetActive(true);
		if (this.idStatus == 0)
		{
			this.ChangeAnim(this.animThrowRightHand);
		}
		else if (this.idStatus == 1)
		{
			this.ChangeAnim(this.animThrowRightHand1);
		}
		else if (this.idStatus == 2)
		{
			this.ChangeAnim(this.animThrowRightHand2);
		}
		EazySoundManager.PlaySound(this.listSound[9], 1f);
		yield return new WaitForSeconds(0.7f * this.angryStat);
		this.isBlind = true;
		yield return new WaitForSeconds(0.3f * this.angryStat);
		this.isBlind = false;
		if (this.idStatus == 0)
		{
			this.bfp_ThrowHand.Attack(3, 1, this.speed_ThrowLeftHand, this.player.transform, true);
		}
		else if (this.idStatus == 1)
		{
			this.bfp_ThrowHand.Attack(3, 4, this.speed_ThrowLeftHand, this.player.transform, true);
		}
		else if (this.idStatus == 2)
		{
			this.bfp_ThrowHand.Attack(3, 6, this.speed_ThrowLeftHand, this.player.transform, true);
		}
		EazySoundManager.PlaySound(this.listSound[10], 1f);
		this.warning_ThrowRightHand.SetActive(false);
		yield break;
	}

	private IEnumerator AttackThrowTwoHand()
	{
		yield return new WaitForSeconds(this.timeDelay_ThrowTwoHand * this.angryStat);
		this.warning_ThrowLeftHand.SetActive(true);
		this.warning_ThrowRightHand.SetActive(true);
		if (this.idStatus == 0)
		{
			this.ChangeAnim(this.animThrowTwoHand);
		}
		else if (this.idStatus == 1)
		{
			this.ChangeAnim(this.animThrowTwoHand1);
		}
		else if (this.idStatus == 2)
		{
			this.ChangeAnim(this.animThrowTwoHand2);
		}
		EazySoundManager.PlaySound(this.listSound[13], 1f);
		yield return new WaitForSeconds(0.6f * this.angryStat);
		this.isBlind = true;
		yield return new WaitForSeconds(0.3f * this.angryStat);
		this.isBlind = false;
		if (this.idStatus == 0)
		{
			this.bfp_ThrowHand.Attack(4, 0, this.speed_ThrowTwoHand, this.player.transform, true);
			this.bfp_ThrowHand.Attack(5, 1, this.speed_ThrowTwoHand, this.player.transform, true);
		}
		else if (this.idStatus == 1)
		{
			this.bfp_ThrowHand.Attack(4, 3, this.speed_ThrowTwoHand, this.player.transform, true);
			this.bfp_ThrowHand.Attack(5, 4, this.speed_ThrowTwoHand, this.player.transform, true);
		}
		else if (this.idStatus == 2)
		{
			this.bfp_ThrowHand.Attack(4, 5, this.speed_ThrowTwoHand, this.player.transform, true);
			this.bfp_ThrowHand.Attack(5, 6, this.speed_ThrowTwoHand, this.player.transform, true);
		}
		EazySoundManager.PlaySound(this.listSound[14], 1f);
		this.warning_ThrowRightHand.SetActive(false);
		this.warning_ThrowLeftHand.SetActive(false);
		yield break;
	}

	private IEnumerator AttackDash()
	{
		yield return new WaitForSeconds(this.timeDelay_Dash * this.angryStat);
		this.isBlind = true;
		this.smoothFollow.isSmoothFollow = false;
		this.warning_dash.SetActive(true);
		float distance = Vector3.Distance(base.gameObject.transform.position, this.player.position);
		float speed = distance * this.speed_Dash;
		if (this.pathMoveDash.GetComponent<BezierPathManager>() != null)
		{
			this.pathMoveDash.GetComponent<BezierPathManager>().CalculatePath();
		}
		if (this.player.transform)
		{
			this.pathMoveDash.waypoints[this.pathMoveDash.waypoints.Length - 1].transform.position = new Vector3(this.player.transform.position.x, this.player.transform.position.y + 0.5f, 0f);
		}
		this.splineMove.speed = speed;
		EazySoundManager.PlaySound(this.listSound[11], 0.8f);
		yield return new WaitForSeconds(0.5f);
		this.warning_dash.SetActive(false);
		this.splineMove.SetPath(this.pathMoveDash);
		this.skeletonAnimation.timeScale = this.speed_Dash + 0.5f;
		yield return new WaitForSeconds(0.5f * this.angryStat);
		if (this.idStatus == 0)
		{
			this.ChangeAnim(this.animDash);
		}
		else if (this.idStatus == 1)
		{
			this.ChangeAnim(this.animDash1);
		}
		else if (this.idStatus == 2)
		{
			this.ChangeAnim(this.animDash2);
		}
		EazySoundManager.PlaySound(this.listSound[12], 1f);
		yield return new WaitForSeconds(0.64f / this.speed_Dash * this.angryStat);
		CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
		yield return new WaitForSeconds(0.44f / this.speed_Dash * this.angryStat);
		this.skeletonAnimation.timeScale = 2f;
		yield return new WaitForSeconds(0.54f / this.speed_Dash * this.angryStat);
		this.skeletonAnimation.timeScale = 1f;
		this.isBlind = false;
		this.smoothFollow.isSmoothFollow = true;
		yield break;
	}

	private IEnumerator AttackShotByTail()
	{
		yield return new WaitForSeconds(this.timeDelay_ShotByTail * this.angryStat);
		this.MoveMid();
		yield return new WaitForSeconds(1f);
		if (this.idStatus == 0)
		{
			this.ChangeAnim(this.animShotByTail);
		}
		else if (this.idStatus == 1)
		{
			this.ChangeAnim(this.animShotByTail1);
		}
		else if (this.idStatus == 2)
		{
			this.ChangeAnim(this.animShotByTail2);
		}
		EazySoundManager.PlaySound(this.listSound[15], 1f);
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.fx_ShotByTail.SetActive(true);
		yield return new WaitForSeconds(1f * this.angryStat);
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < this.numberBullet_ShoyByTail; j++)
			{
				UbhBullet objBullett = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.prefabsBullet[this.idStatus], this.posSpawBullet.position, false);
				NewBoss_BulletHomming homingMissile = objBullett.GetComponent<NewBoss_BulletHomming>();
				homingMissile.SetFollowTargetX(this.timeFollow_Rocket);
				EazySoundManager.PlaySound(this.listSound[16], 1f);
				yield return new WaitForSeconds(0.4f / (float)this.numberBullet_ShoyByTail);
			}
		}
		this.fx_ShotByTail.SetActive(false);
		yield return new WaitForSeconds(2f * this.angryStat);
		this.smoothFollow.isSmoothFollow = true;
		yield break;
	}

	private void MoveMid()
	{
		this.smoothFollow.isSmoothFollow = false;
		this.pathMid = new Vector2[]
		{
			new Vector2(base.transform.position.x, base.transform.position.y),
			new Vector2(0f, 3f)
		};
		this.BaseMoveCurve(1.5f, Ease.Linear, PathMode.Ignore, this.pathMid);
	}

	private void MoveUp()
	{
		this.smoothFollow.isSmoothFollow = false;
		this.pathMid = new Vector2[]
		{
			new Vector2(base.transform.position.x, base.transform.position.y),
			new Vector2(0f, 5f)
		};
		this.BaseMoveCurve(1.5f, Ease.Linear, PathMode.Ignore, this.pathMid);
	}

	private void MoveDown()
	{
		this.smoothFollow.isSmoothFollow = false;
		this.pathMid = new Vector2[]
		{
			new Vector2(base.transform.position.x, base.transform.position.y),
			new Vector2(0f, -5f)
		};
		this.BaseMoveCurve(10f, Ease.Linear, PathMode.Ignore, this.pathMid);
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		EazySoundManager.PlaySound(this.listSound[2], 1f);
	}

	private int GetRandomValue(params BossMode_CuaDo.RandomSelection[] selections)
	{
		float value = UnityEngine.Random.value;
		float num = 0f;
		foreach (BossMode_CuaDo.RandomSelection randomSelection in selections)
		{
			num += randomSelection.probability;
			if (value <= num)
			{
				return randomSelection.GetValue();
			}
		}
		return -1;
	}

	private int idStatus;

	private bool isActiveAngry;

	private float angryStat = 1f;

	private bool isBlind;

	public bool isTransforming;

	[SerializeField]
	private AudioClip[] listSound;

	[SerializeField]
	private SmoothFollow smoothFollow;

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
	private string animIdle2;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animUpgrade0;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animUpgrade1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animUpgrade2;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByGuns;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByGuns1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByGuns2;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByMouth;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByMouth1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByMouth2;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animThrowLeftHand;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animThrowLeftHand1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animThrowLeftHand2;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animThrowRightHand;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animThrowRightHand1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animThrowRightHand2;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animThrowTwoHand;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animThrowTwoHand1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animThrowTwoHand2;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animDash;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animDash1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animDash2;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByTail;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByTail1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByTail2;

	private float skill0_timeDelay;

	[SerializeField]
	private UbhShotCtrl skill0_Ubh0;

	[SerializeField]
	private UbhShotCtrl skill0_Ubh1;

	[SerializeField]
	private UbhShotCtrl skill0_Ubh2;

	[SerializeField]
	private GameObject[] skill0_Warning;

	[SerializeField]
	private GameObject[] skill0_FxShot;

	[SerializeField]
	private GameObject[] skill0_FxShot2;

	[SerializeField]
	private UbhLinearShot[] skill0_UbhLinear;

	[SerializeField]
	private UbhRandomSpiralShot[] skill0_UbhRandom;

	[SerializeField]
	private float skill0_speedRocket = 10f;

	private float skill1_timeDelay;

	private float skill1_speedBigBullet = 7f;

	[SerializeField]
	private UbhShotCtrl skill1_Ubh0;

	[SerializeField]
	private UbhRandomSpiralShot skill1_UbhRandomSpriral0;

	[SerializeField]
	private GameObject[] skill1_Warning;

	[SerializeField]
	private UbhShotCtrl skill1_Ubh1;

	[SerializeField]
	private UbhLinearShot[] skill1_UbhLinear;

	[SerializeField]
	private UbhShotCtrl skill1_Ubh2;

	[SerializeField]
	private UbhRandomSpiralShot skill1_UbhRandomSpriral2;

	[SerializeField]
	private UbhHoleCircleShot[] skill1_UbhHoleCircle;

	private float timeDelay_ThrowLeftHand;

	private float speed_ThrowLeftHand = 15f;

	[SerializeField]
	private GameObject warning_ThrowLeftHand;

	private float timeDelay_ThrowRightHand;

	private float speed_ThrowRightHand = 15f;

	[SerializeField]
	private GameObject warning_ThrowRightHand;

	private float timeDelay_ThrowTwoHand;

	private float speed_ThrowTwoHand = 15f;

	[SerializeField]
	private BulletFollowPath bfp_ThrowHand;

	private float timeDelay_Dash;

	private float speed_Dash = 1.2f;

	[SerializeField]
	private GameObject warning_dash;

	[SerializeField]
	private splineMove splineMove;

	[SerializeField]
	private PathManager pathMoveDash;

	[SerializeField]
	private UbhShotCtrl ubh_Dash;

	[SerializeField]
	private GameObject fx_Dash;

	private float timeDelay_ShotByTail;

	private int numberBullet_ShoyByTail = 2;

	private float timeFollow_Rocket = 3f;

	[SerializeField]
	private GameObject fx_ShotByTail;

	[SerializeField]
	private Transform posSpawBullet;

	[SerializeField]
	private GameObject[] prefabsBullet;

	[EnemyEventCustom(displayName = "BossTuan/CuaXanh_BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

	private float rangeAttack = 30f;

	public LayerMask layerPlayer;

	public Transform player;

	public List<int> listID = new List<int>();

	private int currentTurnSkill;

	private int lastIdSkill = -1;

	private Vector2[] pathMid;

	private Vector2[] pathUp;

	private Vector2[] pathDown;

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
