using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SkyGameKit;
using Spine;
using Spine.Unity;
using SWS;
using UnityEngine;

public class BossMode_ConCu : NewBoss
{
	[EnemyAction(displayName = "Tuan/StartAttack")]
	public void StartAttack()
	{
		this.RandomSkill();
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry")]
	public void ActiveStatusAngry()
	{
		this.isActiveAngry = true;
	}

	[EnemyAction(displayName = "Tuan/ActiveClone")]
	public void ActiveClone(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill1_timeDelay = timeDelay;
		this.skill0_UbhCircle[0].m_bulletNum = numberBullet;
		this.skill0_UbhCircle[1].m_bulletNum = numberBullet;
		this.skill0_UbhCircle[2].m_bulletNum = numberBullet;
		this.skill0_UbhCircle[3].m_bulletNum = numberBullet;
		this.skill0_UbhCircle[0].m_bulletSpeed = speedBullet;
		this.skill0_UbhCircle[1].m_bulletSpeed = speedBullet;
		this.skill0_UbhCircle[2].m_bulletSpeed = speedBullet;
		this.skill0_UbhCircle[3].m_bulletSpeed = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill1_ShotByMouth")]
	public void Skill1_ShotByMouth(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill1_timeDelay = timeDelay;
		for (int i = 0; i < this.skill1_UbhRandomSpiral.Length; i++)
		{
			this.skill1_UbhRandomSpiral[i].m_bulletNum = numberBullet;
			this.skill1_UbhRandomSpiral[i].m_maxSpeed = speedBullet;
			this.skill1_UbhRandomSpiral[i].m_minSpeed = speedBullet - 2f;
		}
	}

	[EnemyAction(displayName = "Tuan/Skill2_ShotRocket")]
	public void Skill2_ShotRocket(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill2_timeDelay = timeDelay;
		this.skill2_numberBullet = numberBullet;
		this.skill2_speedBullet = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill3_Dash")]
	public void Skill3_Dash(float timeDelay, float speedMove)
	{
		this.skill3_timeDelay = timeDelay;
		this.skill3_SpeedMove = speedMove;
	}

	[EnemyAction(displayName = "Tuan/Skill4_ShotByMidEye")]
	public void Skill4_ShotByMidEye(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill4_timeDelay = timeDelay;
		this.skill4_UbhCircle[0].m_bulletNum = numberBullet;
		this.skill4_UbhCircle[0].m_bulletSpeed = speedBullet;
		this.skill4_UbhCircle[1].m_bulletNum = numberBullet;
		this.skill4_UbhCircle[1].m_bulletSpeed = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill5_LaserEyes")]
	public void Skill5_LaserEyes(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill5_timeDelay = timeDelay;
		for (int i = 0; i < this.skill5_UbhSinWave.Length; i++)
		{
			this.skill5_UbhSinWave[i].m_bulletNum = numberBullet;
			this.skill5_UbhSinWave[i].m_bulletSpeed = speedBullet;
		}
	}

	[EnemyAction(displayName = "Tuan/Skill6_LaserEyes")]
	public void Skill6_LaserEyes(float timeDelay, float speedBullet)
	{
		this.skill6_timeDelay = timeDelay;
		for (int i = 0; i < this.skill6_UbhSinWave.Length; i++)
		{
			this.skill6_UbhSinWave[i].m_bulletSpeed = speedBullet;
		}
	}

	public override void Restart()
	{
		base.Restart();
		if (this.BaseStatBoss != null)
		{
			this.BaseStatBoss();
		}
		base.ShowHealthBar();
		base.SetNameBoss("Twin Owls");
		SgkSingleton<LevelManager>.Instance.AliveEnemy.Add(this.listBoss[0]);
		SgkSingleton<LevelManager>.Instance.AliveEnemy.Add(this.listBoss[1]);
	}

	private void Update()
	{
		if (!this.isActiveAngry)
		{
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
			{
				base.StartCoroutine(this.ActiveClone());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha2))
			{
				base.StartCoroutine(this.HideClone());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha3))
			{
				base.StartCoroutine(this.AttackSkill1());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha4))
			{
				base.StartCoroutine(this.AttackSkill2());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha5))
			{
				base.StartCoroutine(this.AttackSkill3());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha6))
			{
				base.StartCoroutine(this.AttackSkill4());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha7))
			{
				base.StartCoroutine(this.AttackSkill5());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha8))
			{
				base.StartCoroutine(this.AttackSkill6());
			}
		}
		else
		{
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
			{
				base.StartCoroutine(this.ActiveClone());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha2))
			{
				base.StartCoroutine(this.HideClone());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha3))
			{
				base.StartCoroutine(this.AttackSkill1());
				base.StartCoroutine(this.AttackSkill1_Clone());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha4))
			{
				base.StartCoroutine(this.AttackSkill2());
				base.StartCoroutine(this.AttackSkill2_Clone());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha5))
			{
				base.StartCoroutine(this.AttackSkill3());
				base.StartCoroutine(this.AttackSkill3_Clone());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha6))
			{
				base.StartCoroutine(this.AttackSkill4());
				base.StartCoroutine(this.AttackSkill4_Clone());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha7))
			{
				base.StartCoroutine(this.AttackSkill5());
				base.StartCoroutine(this.AttackSkill5_Clone());
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha8))
			{
				base.StartCoroutine(this.AttackSkill6());
				base.StartCoroutine(this.AttackSkill6_Clone());
			}
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha9))
		{
			this.isActiveAngry = true;
		}
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
		while (this.listID.Count < 6)
		{
			int item = UnityEngine.Random.Range(0, 6);
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
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill1());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill1());
				base.StartCoroutine(this.AttackSkill1_Clone());
			}
			break;
		case 1:
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill2());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill2());
				base.StartCoroutine(this.AttackSkill2_Clone());
			}
			break;
		case 2:
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill3());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill3_Clone());
			}
			break;
		case 3:
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill4());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill4());
				base.StartCoroutine(this.AttackSkill4_Clone());
			}
			break;
		case 4:
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill5());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill5());
				base.StartCoroutine(this.AttackSkill5_Clone());
			}
			break;
		case 5:
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill6());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill6());
				base.StartCoroutine(this.AttackSkill6_Clone());
			}
			break;
		}
	}

	private void ChangeAnimMain(string anim)
	{
		if (anim == this.animSkill6)
		{
			this.skeletonAnimation.timeScale = 0.5f;
		}
		this.skeletonAnimation.AnimationState.SetAnimation(0, anim, false).Complete += delegate(TrackEntry A_1)
		{
			this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle0, true);
			if (anim == this.animSkill6)
			{
				this.skeletonAnimation.timeScale = 1f;
			}
			if (this.isActiveAngry && !this.isUpgrade)
			{
				this.isUpgrade = true;
				this.StartCoroutine(this.ActiveClone());
			}
			else
			{
				this.RandomSkill();
			}
		};
	}

	private void ChangeAnimClone(string anim)
	{
		if (anim == this.animSkill6)
		{
			this.skeletonAnimation_Clone.timeScale = 0.5f;
		}
		this.skeletonAnimation_Clone.AnimationState.SetAnimation(0, anim, false).Complete += delegate(TrackEntry A_1)
		{
			this.skeletonAnimation_Clone.AnimationState.SetAnimation(0, this.animIdle0, true);
			if (anim == this.animSkill6)
			{
				this.skeletonAnimation_Clone.timeScale = 1f;
			}
		};
	}

	private IEnumerator ActiveClone()
	{
		base.ShowRedScreen();
		this.isBlind = true;
		this.smoothFollow.isSmoothFollow = true;
		this.MoveMid(new Vector3(0f, 4f, 0f));
		yield return new WaitForSeconds(1f);
		this.ChangeAnimMain(this.animSkill0);
		this.skill0_Ubh.StartShotRoutine();
		this.skeletonAnimation_Clone.gameObject.SetActive(true);
		this.skeletonAnimation_Clone.AnimationState.SetAnimation(0, this.animSkill0, false).Complete += delegate(TrackEntry A_1)
		{
			this.skeletonAnimation_Clone.AnimationState.SetAnimation(0, this.animIdle0, true);
		};
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.skeletonAnimation.gameObject.transform.DOLocalMove(new Vector3(2f, 0f, 0f), 1f, false).SetEase(Ease.InBounce);
		this.skeletonAnimation_Clone.gameObject.transform.DOLocalMove(new Vector3(-2f, 0f, 0f), 1f, false).SetEase(Ease.InBounce);
		yield return new WaitForSeconds(1f * this.angryStat);
		this.isActiveAngry = true;
		this.isBlind = false;
		yield break;
	}

	private IEnumerator HideClone()
	{
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.isBlind = true;
		this.smoothFollow.isSmoothFollow = true;
		this.ChangeAnimMain(this.animSkill0);
		this.skeletonAnimation_Clone.gameObject.SetActive(true);
		this.skeletonAnimation_Clone.AnimationState.SetAnimation(0, this.animSkill0, false).Complete += delegate(TrackEntry A_1)
		{
			this.skeletonAnimation_Clone.AnimationState.SetAnimation(0, this.animIdle0, true);
		};
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.skeletonAnimation.gameObject.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 1f, false).SetEase(Ease.InBounce);
		this.skeletonAnimation_Clone.gameObject.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 1f, false).SetEase(Ease.InBounce);
		yield return new WaitForSeconds(1f * this.angryStat);
		this.isActiveAngry = false;
		this.isBlind = false;
		this.smoothFollow.isSmoothFollow = false;
		this.skeletonAnimation_Clone.gameObject.SetActive(false);
		yield break;
	}

	private IEnumerator AttackSkill1()
	{
		yield return new WaitForSeconds(this.skill1_timeDelay * this.angryStat);
		this.ChangeAnimMain(this.animSkill1);
		yield return new WaitForSeconds(1f);
		this.skill1_UbhRandomSpiral[0].m_startAngle = base.gameObject.transform.eulerAngles.z + 140f;
		this.skill1_UbhRandomSpiral[1].m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill1_UbhRandomSpiral[2].m_startAngle = base.gameObject.transform.eulerAngles.z + 220f;
		this.skill1_Ubh.StartShotRoutine();
		yield break;
	}

	private IEnumerator AttackSkill1_Clone()
	{
		yield return new WaitForSeconds(this.skill1_timeDelay * this.angryStat);
		this.ChangeAnimClone(this.animSkill1);
		yield return new WaitForSeconds(1f);
		this.skill1_UbhRandomSpiral[3].m_startAngle = base.gameObject.transform.eulerAngles.z + 140f;
		this.skill1_UbhRandomSpiral[4].m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill1_UbhRandomSpiral[5].m_startAngle = base.gameObject.transform.eulerAngles.z + 220f;
		this.skill1_Ubh_Clone.StartShotRoutine();
		yield break;
	}

	private IEnumerator AttackSkill2()
	{
		yield return new WaitForSeconds(this.skill2_timeDelay * this.angryStat);
		this.isBlind = true;
		this.ChangeAnimMain(this.animSkill2);
		for (int i = 0; i < this.skill2_numberBullet; i++)
		{
			this.skill2_Bfp.Attack(i, 0, this.skill2_speedBullet, this.player, true);
			yield return new WaitForSeconds(0.04f * this.angryStat);
		}
		yield return new WaitForSeconds(0.04f * this.angryStat);
		for (int j = this.skill2_numberBullet - 1; j > 0; j--)
		{
			this.skill2_Bfp.Attack(j, 0, this.skill2_speedBullet, this.player, true);
			yield return new WaitForSeconds(0.04f * this.angryStat);
		}
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.isBlind = false;
		yield break;
	}

	private IEnumerator AttackSkill2_Clone()
	{
		yield return new WaitForSeconds(this.skill2_timeDelay * this.angryStat);
		this.isBlind = true;
		this.ChangeAnimClone(this.animSkill2);
		for (int i = 0; i < this.skill2_numberBullet; i++)
		{
			this.skill2_Bfp_Clone.Attack(i, 0, this.skill2_speedBullet, null, true);
			yield return new WaitForSeconds(0.04f * this.angryStat);
		}
		yield return new WaitForSeconds(0.04f * this.angryStat);
		for (int j = this.skill2_numberBullet - 1; j > 0; j--)
		{
			this.skill2_Bfp_Clone.Attack(j, 0, this.skill2_speedBullet, null, true);
			yield return new WaitForSeconds(0.04f * this.angryStat);
		}
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.isBlind = false;
		yield break;
	}

	private IEnumerator AttackSkill3()
	{
		SgkSingleton<LevelManager>.Instance.AliveEnemy.Remove(this);
		yield return new WaitForSeconds(this.skill3_timeDelay);
		this.smoothFollow.isSmoothFollow = false;
		this.isBlind = true;
		this.MoveMid(new Vector2(0f, 4f));
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 0f, DG.Tweening.RotateMode.Fast);
		this.boss[0].transform.DORotate(new Vector3(0f, 0f, 0f), 0f, DG.Tweening.RotateMode.Fast);
		int idPath = UnityEngine.Random.Range(0, 2);
		yield return new WaitForSeconds(1f);
		this.skeletonAnimation.gameObject.transform.DORotate(new Vector3(0f, 0f, 90f), 0f, DG.Tweening.RotateMode.Fast);
		this.skeletonAnimation.AnimationState.SetAnimation(0, this.animSkill3, true);
		this.skill3_SplineMove.speed = this.skill3_SpeedMove;
		this.skill3_SplineMove.easeType = Ease.Linear;
		this.skill3_SplineMove.pathMode = PathMode.TopDown2D;
		this.skill3_SplineMove.timeValue = splineMove.TimeValue.time;
		this.skill3_SplineMove.SetPath(this.skill3_PathMoveDash[idPath]);
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < this.skill3_NumberBulletAura; i++)
		{
			Fu.SpawnExplosion(this.skill3_BulletAura, this.skeletonAnimation.gameObject.transform.position, Quaternion.identity);
			yield return new WaitForSeconds((this.skill3_SplineMove.speed - 0.5f) / (float)this.skill3_NumberBulletAura);
		}
		this.skeletonAnimation.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 2f, DG.Tweening.RotateMode.Fast);
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 2f, DG.Tweening.RotateMode.Fast);
		this.boss[0].transform.DORotate(new Vector3(0f, 0f, 0f), 2f, DG.Tweening.RotateMode.Fast);
		yield return new WaitForSeconds(1f);
		this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle0, true);
		this.smoothFollow.isSmoothFollow = true;
		this.isBlind = false;
		this.RandomSkill();
		yield break;
	}

	private IEnumerator AttackSkill3_Clone()
	{
		yield return new WaitForSeconds(this.skill3_timeDelay);
		this.smoothFollow.isSmoothFollow = false;
		this.isBlind = true;
		this.MoveMid(new Vector2(0f, 0f));
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 0f, DG.Tweening.RotateMode.Fast);
		this.boss[0].transform.DOLocalRotate(new Vector3(2f, 0f, 0f), 0f, DG.Tweening.RotateMode.Fast);
		this.boss[1].transform.DOLocalRotate(new Vector3(-2f, 0f, 0f), 0f, DG.Tweening.RotateMode.Fast);
		this.boss[0].transform.DOLocalMove(new Vector3(2f, 0f, 0f), 0f, false);
		this.boss[1].transform.DOLocalMove(new Vector3(-2f, 0f, 0f), 0f, false);
		this.skeletonAnimation.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 0f, false);
		this.skeletonAnimation_Clone.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 0f, false);
		yield return new WaitForSeconds(1f);
		this.skeletonAnimation.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 90f), 0f, DG.Tweening.RotateMode.Fast);
		this.skeletonAnimation.AnimationState.SetAnimation(0, this.animSkill3, true);
		this.skeletonAnimation_Clone.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 90f), 0f, DG.Tweening.RotateMode.Fast);
		this.skeletonAnimation_Clone.AnimationState.SetAnimation(0, this.animSkill3, true);
		this.skill3_SplineMove.speed = this.skill3_SpeedMove;
		this.skill3_SplineMove.easeType = Ease.Linear;
		this.skill3_SplineMove.pathMode = PathMode.TopDown2D;
		this.skill3_SplineMove.timeValue = splineMove.TimeValue.time;
		this.skill3_SplineMove.SetPath(this.skill3_PathMoveDash_Clone[0]);
		this.skill3_SplineMove_Clone.speed = this.skill3_SpeedMove;
		this.skill3_SplineMove_Clone.easeType = Ease.Linear;
		this.skill3_SplineMove_Clone.pathMode = PathMode.TopDown2D;
		this.skill3_SplineMove_Clone.timeValue = splineMove.TimeValue.time;
		this.skill3_SplineMove_Clone.SetPath(this.skill3_PathMoveDash_Clone[1]);
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < this.skill3_NumberBulletAura; i++)
		{
			Fu.SpawnExplosion(this.skill3_BulletAura, this.skeletonAnimation.gameObject.transform.position, Quaternion.identity);
			Fu.SpawnExplosion(this.skill3_BulletAura, this.skeletonAnimation_Clone.gameObject.transform.position, Quaternion.identity);
			yield return new WaitForSeconds((this.skill3_SplineMove.speed - 0.5f) / (float)this.skill3_NumberBulletAura);
		}
		yield return new WaitForSeconds(1.5f);
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 2f, DG.Tweening.RotateMode.Fast);
		this.skeletonAnimation.transform.DOLocalMove(new Vector3(2f, 0f, 0f), 2f, false);
		this.skeletonAnimation_Clone.transform.DOLocalMove(new Vector3(-2f, 0f, 0f), 2f, false);
		this.skeletonAnimation.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 2f, DG.Tweening.RotateMode.Fast);
		this.skeletonAnimation_Clone.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 2f, DG.Tweening.RotateMode.Fast);
		this.boss[0].transform.DOLocalMove(new Vector3(0f, 0f, 0f), 2f, false);
		this.boss[1].transform.DOLocalMove(new Vector3(0f, 0f, 0f), 2f, false);
		this.boss[0].transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 2f, DG.Tweening.RotateMode.Fast);
		this.boss[1].transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 2f, DG.Tweening.RotateMode.Fast);
		yield return new WaitForSeconds(1f);
		this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle0, true);
		this.skeletonAnimation_Clone.AnimationState.SetAnimation(0, this.animIdle0, true);
		this.smoothFollow.isSmoothFollow = true;
		yield return new WaitForSeconds(2f);
		this.RandomSkill();
		yield break;
	}

	private IEnumerator AttackSkill4()
	{
		yield return new WaitForSeconds(this.skill3_timeDelay * this.angryStat);
		this.ChangeAnimMain(this.animSkill5);
		yield return new WaitForSeconds(0.4f);
		this.skill4_Ubh.StartShotRoutine();
		yield break;
	}

	private IEnumerator AttackSkill4_Clone()
	{
		yield return new WaitForSeconds(this.skill3_timeDelay * this.angryStat);
		this.ChangeAnimClone(this.animSkill5);
		yield return new WaitForSeconds(0.4f);
		this.skill4_Ubh_Clone.StartShotRoutine();
		yield break;
	}

	private IEnumerator AttackSkill5()
	{
		yield return new WaitForSeconds(this.skill5_timeDelay * this.angryStat);
		this.isBlind = true;
		this.skill5_Warning.SetActive(true);
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid(new Vector3(0f, 4f));
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 1f, DG.Tweening.RotateMode.Fast);
		yield return new WaitForSeconds(1f);
		this.skill5_Warning.SetActive(false);
		this.ChangeAnimMain(this.animSkill6);
		this.skill5_Laser[0].SetActive(true);
		this.skill5_Laser[1].SetActive(true);
		if (!this.isActiveAngry)
		{
			this.skill5_Ubh.StartShotRoutine();
		}
		yield return new WaitForSeconds(8f);
		this.skill5_Laser[0].SetActive(false);
		this.skill5_Laser[1].SetActive(false);
		this.isBlind = false;
		this.smoothFollow.isSmoothFollow = true;
		yield break;
	}

	private IEnumerator AttackSkill5_Clone()
	{
		yield return new WaitForSeconds(this.skill5_timeDelay * this.angryStat);
		this.skill5_Warning_Clone.SetActive(true);
		yield return new WaitForSeconds(1f);
		this.skill5_Warning_Clone.SetActive(false);
		this.ChangeAnimClone(this.animSkill6);
		this.skill5_Laser_Clone[0].SetActive(true);
		this.skill5_Laser_Clone[1].SetActive(true);
		yield return new WaitForSeconds(8f);
		this.skill5_Laser_Clone[0].SetActive(false);
		this.skill5_Laser_Clone[1].SetActive(false);
		yield break;
	}

	private IEnumerator AttackSkill6()
	{
		yield return new WaitForSeconds(this.skill6_timeDelay * this.angryStat);
		this.isBlind = true;
		if (this.isActiveAngry)
		{
			this.MoveMid(new Vector3(0f, 4f));
			this.smoothFollow.isSmoothFollow = false;
		}
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 1f, DG.Tweening.RotateMode.Fast);
		yield return new WaitForSeconds(1f);
		this.ChangeAnimMain(this.animSkill7);
		yield return new WaitForSeconds(0.5f);
		this.skill6_Ubh.StartShotRoutine();
		yield return new WaitForSeconds(1f);
		this.isBlind = false;
		yield break;
	}

	private IEnumerator AttackSkill6_Clone()
	{
		yield return new WaitForSeconds(this.skill6_timeDelay * this.angryStat);
		this.isBlind = true;
		yield return new WaitForSeconds(1f);
		this.ChangeAnimClone(this.animSkill7);
		yield return new WaitForSeconds(0.5f);
		this.skill6_Ubh_Clone.StartShotRoutine();
		yield return new WaitForSeconds(1f);
		this.isBlind = false;
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

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
	}

	private int GetRandomValue(params BossMode_ConCu.RandomSelection[] selections)
	{
		float value = UnityEngine.Random.value;
		float num = 0f;
		foreach (BossMode_ConCu.RandomSelection randomSelection in selections)
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

	private bool isUpgrade;

	private float angryStat = 1f;

	public bool isBlind;

	[SerializeField]
	private AudioClip[] listSound;

	[SerializeField]
	private SmoothFollow smoothFollow;

	[SerializeField]
	private SkeletonAnimation skeletonAnimation;

	[SerializeField]
	private SkeletonAnimation skeletonAnimation_Clone;

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
	private string animSkill6;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill7;

	private float skill0_timeDelay = 1f;

	[SerializeField]
	private UbhShotCtrl skill0_Ubh;

	[SerializeField]
	private UbhCircleShot[] skill0_UbhCircle;

	private float skill1_timeDelay = 1f;

	[SerializeField]
	private UbhShotCtrl skill1_Ubh;

	[SerializeField]
	private UbhShotCtrl skill1_Ubh_Clone;

	[SerializeField]
	private UbhRandomSpiralShot[] skill1_UbhRandomSpiral;

	private float skill2_timeDelay = 1f;

	private int skill2_numberBullet = 12;

	private float skill2_speedBullet = 6f;

	[SerializeField]
	private BulletFollowPath skill2_Bfp;

	[SerializeField]
	private BulletFollowPath skill2_Bfp_Clone;

	private float skill3_timeDelay = 1f;

	private float skill3_SpeedMove = 5f;

	private int skill3_NumberBulletAura = 10;

	[SerializeField]
	private GameObject[] boss;

	[SerializeField]
	private GameObject skill3_BulletAura;

	[SerializeField]
	private PathManager[] skill3_PathMoveDash;

	[SerializeField]
	private GameObject[] skill3_Warning;

	[SerializeField]
	private splineMove skill3_SplineMove;

	[SerializeField]
	private PathManager[] skill3_PathMoveDash_Clone;

	[SerializeField]
	private GameObject[] skill3_Warning_Clone;

	[SerializeField]
	private splineMove skill3_SplineMove_Clone;

	private float skill4_timeDelay = 1f;

	[SerializeField]
	private UbhShotCtrl skill4_Ubh;

	[SerializeField]
	private UbhShotCtrl skill4_Ubh_Clone;

	[SerializeField]
	private UbhHoleCircleShot[] skill4_UbhCircle;

	private float skill5_timeDelay = 1f;

	[SerializeField]
	private GameObject[] skill5_Laser;

	[SerializeField]
	private GameObject[] skill5_Laser_Clone;

	[SerializeField]
	private GameObject skill5_Warning;

	[SerializeField]
	private GameObject skill5_Warning_Clone;

	[SerializeField]
	private UbhShotCtrl skill5_Ubh;

	[SerializeField]
	private UbhShotCtrl skill5_Ubh_Clone;

	[SerializeField]
	private UbhSinWaveBulletNwayShot[] skill5_UbhSinWave;

	private float skill6_timeDelay;

	[SerializeField]
	private UbhShotCtrl skill6_Ubh;

	[SerializeField]
	private UbhShotCtrl skill6_Ubh_Clone;

	[SerializeField]
	private UbhSinWaveBulletNwayShot[] skill6_UbhSinWave;

	[EnemyEventCustom(displayName = "BossTuan/CuaCu_BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

	private float rangeAttack = 30f;

	public LayerMask layerPlayer;

	public Transform player;

	public List<int> listID = new List<int>();

	private int currentTurnSkill;

	private int lastIdSkill = -1;

	[SerializeField]
	private BaseEnemy[] listBoss;

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
