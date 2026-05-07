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

public class BossMode_BoGai : NewBoss
{
	[EnemyAction(displayName = "Tuan/StartAttack")]
	public void StartAttack()
	{
		this.FindRandomTarget();
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

	[EnemyAction(displayName = "Tuan/Skill0_ShotQuills")]
	public void Skill0_ShotQuills(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill0_TimeDelay = timeDelay;
		for (int i = 0; i < this.skill0_UbhRandom.Length; i++)
		{
			this.skill0_UbhRandom[i].m_bulletNum = numberBullet;
			this.skill0_UbhRandom[i].m_minSpeed = speedBullet - 5f;
			this.skill0_UbhRandom[i].m_maxSpeed = speedBullet;
		}
	}

	[EnemyAction(displayName = "Tuan/Skill1_Rolling")]
	public void Skill1_Rolling(float timeDelay, float speedMove, int numberBulletAura, int numberBullet, int numberWayBullet, float speedBullet)
	{
		this.skill1_TimeDelay = timeDelay;
		this.skill1_speedMove = speedMove;
		this.skill1_numberBulletAura = numberBulletAura;
		for (int i = 0; i < this.skill1a_UbhNwayShot.Length; i++)
		{
			this.skill1a_UbhNwayShot[i].m_bulletNum = numberBullet;
			this.skill1a_UbhNwayShot[i].m_wayNum = numberWayBullet;
			this.skill1a_UbhNwayShot[i].m_bulletSpeed = speedBullet;
			this.skill1b_UbhNwayShot[i].m_bulletNum = numberBullet;
			this.skill1b_UbhNwayShot[i].m_wayNum = numberWayBullet;
			this.skill1b_UbhNwayShot[i].m_bulletSpeed = speedBullet;
		}
	}

	[EnemyAction(displayName = "Tuan/Skill2_SpiritsIO")]
	public void Skill2_SpiritsIO(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill2_TimeDelay = timeDelay;
		this.skill2_UbhSpiralShot.m_bulletNum = numberBullet;
		this.skill2_UbhSpiralShot.m_bulletSpeed = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill3_ShotByMouth")]
	public void Skill3_ShotByMouth(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill3_TimeDelay = timeDelay;
		this.skill3_UbhNway.m_bulletNum = numberBullet;
		this.skill3_UbhNway.m_bulletSpeed = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill4_SummonEarthLeft")]
	public void Skill4_SummonEarthLeft(float timeDelay)
	{
		this.skill4_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill5_SummonEarthRight")]
	public void Skill5_SummonEarthRight(float timeDelay)
	{
		this.skill5_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill6_Dash")]
	public void Skill6_Dash(float timeDelay, float speedMove)
	{
		this.skill6_TimeDelay = timeDelay;
		this.skill6_moveSpeed = speedMove;
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
		base.SetNameBoss("Monster Bugs");
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

	private void ChangeAnimAngry()
	{
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.animationBoss.AnimationState.SetAnimation(0, this.animUpgrade, false).Complete += delegate(TrackEntry A_1)
			{
				this.listBoxCheckBullet[0].SetActive(false);
				this.listBoxCheckBullet[1].SetActive(true);
				this.isUpgrade = true;
				this.animationBoss.AnimationState.SetAnimation(0, this.animIdle1, true);
				base.StartCoroutine(this.AttackSkill3());
			};
		}
	}

	private IEnumerator ChangeAnimDie()
	{
		this.smoothFollow.isSmoothFollow = false;
		this.isBlind = true;
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
		yield return new WaitForSeconds(1.7f);
		this.isBlind = true;
		for (int i = 0; i < this.skill0_UbhRandom.Length; i++)
		{
			this.skill0_UbhRandom[i].m_randomCenterAngle = (float)this.listPosUbh[i];
			this.skill0_UbhRandom[i].m_randomCenterAngle = base.gameObject.transform.eulerAngles.z + this.skill0_UbhRandom[i].m_randomCenterAngle;
		}
		this.skill0_Ubh.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[3], 1f);
		yield return new WaitForSeconds(2.3f);
		this.isBlind = false;
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else if (this.isActiveAngry && !this.isUpgrade)
		{
			this.ChangeAnimAngry();
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
		this.smoothFollow.isSmoothFollow = false;
		this.isBlind = true;
		this.MoveMid(new Vector2(0f, 4f));
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 0f, DG.Tweening.RotateMode.Fast);
		yield return new WaitForSeconds(2f);
		this.animationBoss.gameObject.transform.DORotate(new Vector3(0f, 0f, 90f), 0f, DG.Tweening.RotateMode.Fast);
		this.animationBoss.AnimationState.SetAnimation(0, this.animSkill1, true);
		int idPath = UnityEngine.Random.Range(0, 2);
		this.skill6_SplineMove.speed = this.skill1_speedMove;
		this.skill6_SplineMove.easeType = Ease.Linear;
		this.skill6_SplineMove.pathMode = PathMode.TopDown2D;
		this.skill6_SplineMove.timeValue = splineMove.TimeValue.time;
		this.skill6_SplineMove.SetPath(this.skill1_PathMoveDash[idPath]);
		this.skill1_Warning.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		this.skill1_Ubh[idPath].StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[5], 1f);
		for (int i = 0; i < this.skill1_numberBulletAura; i++)
		{
			EazySoundManager.PlaySound(this.listSound[4], 1f);
			Fu.SpawnExplosion(this.skill1_bulletAura, base.gameObject.transform.position, Quaternion.identity);
			yield return new WaitForSeconds((this.skill6_SplineMove.speed - 0.5f) / (float)this.skill1_numberBulletAura);
		}
		this.animationBoss.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 2f, DG.Tweening.RotateMode.Fast);
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 2f, DG.Tweening.RotateMode.Fast);
		yield return new WaitForSeconds(1.5f);
		this.skill1_Warning.SetActive(false);
		yield return new WaitForSeconds(1f);
		this.animationBoss.AnimationState.SetAnimation(0, this.animIdle0, true);
		this.smoothFollow.isSmoothFollow = true;
		this.isBlind = false;
		yield return new WaitForSeconds(2f);
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else if (this.isActiveAngry && !this.isUpgrade)
		{
			this.ChangeAnimAngry();
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
		this.isBlind = true;
		this.skill2_UbhSpiralShot.m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill2_Ubh[0].StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[6], 1f);
		yield return new WaitForSeconds(2f);
		this.isBlind = false;
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else if (this.isActiveAngry && !this.isUpgrade)
		{
			this.ChangeAnimAngry();
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
		this.ChangeAnim(this.animSkill3);
		yield return new WaitForSeconds(1f);
		this.isBlind = true;
		this.skill3_UbhNway.m_centerAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill3_Ubh[0].StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[7], 1f);
		this.skill3_Warning.SetActive(true);
		yield return new WaitForSeconds(1.5f);
		this.skill3_Warning.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		this.isBlind = false;
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else if (this.isActiveAngry && !this.isUpgrade)
		{
			this.ChangeAnimAngry();
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
		this.smoothFollow.isSmoothFollow = false;
		this.isBlind = true;
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 0f, DG.Tweening.RotateMode.Fast);
		this.MoveMid(new Vector2(0f, 5f));
		yield return new WaitForSeconds(1f);
		this.ChangeAnim(this.animSkill4);
		this.skill4_Warning.SetActive(true);
		yield return new WaitForSeconds(1.7f);
		EazySoundManager.PlaySound(this.listSound[8], 1f);
		CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
		this.skill4_Warning.SetActive(false);
		Fu.SpawnExplosion(this.skill4_fxEarthQuake, new Vector3(0f, 0f, 0f), Quaternion.identity);
		yield return new WaitForSeconds(3.3f);
		this.smoothFollow.isSmoothFollow = true;
		this.isBlind = false;
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else if (this.isActiveAngry && !this.isUpgrade)
		{
			this.ChangeAnimAngry();
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
		this.smoothFollow.isSmoothFollow = false;
		this.isBlind = true;
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 0f, DG.Tweening.RotateMode.Fast);
		this.MoveMid(new Vector2(0f, 5f));
		yield return new WaitForSeconds(1f);
		this.ChangeAnim(this.animSkill5);
		this.skill5_Warning.SetActive(true);
		yield return new WaitForSeconds(1.7f);
		CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
		this.skill5_Warning.SetActive(false);
		Fu.SpawnExplosion(this.skill5_fxEarthQuake, new Vector3(0f, 0f, 0f), Quaternion.identity);
		EazySoundManager.PlaySound(this.listSound[8], 1f);
		yield return new WaitForSeconds(3.3f);
		this.smoothFollow.isSmoothFollow = true;
		this.isBlind = false;
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else if (this.isActiveAngry && !this.isUpgrade)
		{
			this.ChangeAnimAngry();
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
		this.isBlind = true;
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid(new Vector2(0f, 4f));
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 1f, DG.Tweening.RotateMode.Fast);
		yield return new WaitForSeconds(1f);
		this.skill6_Warning[0].SetActive(true);
		yield return new WaitForSeconds(1f);
		this.skill6_Warning[0].SetActive(false);
		this.skill6_SplineMove.speed = this.skill6_moveSpeed;
		this.skill6_PathMoveDash.waypoints[2].position = new Vector3(0f, SgkCamera.bottomLeft.y + 2f, 0f);
		this.skill6_SplineMove.pathMode = PathMode.Ignore;
		this.skill6_SplineMove.easeType = Ease.OutBounce;
		this.skill6_SplineMove.SetPath(this.skill6_PathMoveDash);
		this.skill1_Warning.SetActive(true);
		EazySoundManager.PlaySound(this.listSound[9], 1f);
		yield return new WaitForSeconds(this.skill6_SplineMove.speed - 1.3f);
		EazySoundManager.PlaySound(this.listSound[11], 1f);
		yield return new WaitForSeconds(1.3f);
		this.skill1_Warning.SetActive(false);
		if (!this.isActiveAngry)
		{
			this.ChangeAnim(this.animSkill6a);
			this.skill6_Warning[1].SetActive(true);
			yield return new WaitForSeconds(1.4f);
			this.skill6_Warning[1].SetActive(false);
			this.listBoxCheckBullet[2].SetActive(true);
			EazySoundManager.PlaySound(this.listSound[10], 1f);
			CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
			yield return new WaitForSeconds(1.3f);
			this.listBoxCheckBullet[2].SetActive(false);
		}
		else
		{
			this.ChangeAnim(this.animSkill6b);
			this.skill6_Warning[2].SetActive(true);
			yield return new WaitForSeconds(1.4f);
			this.skill6_Warning[2].SetActive(false);
			this.listBoxCheckBullet[3].SetActive(true);
			EazySoundManager.PlaySound(this.listSound[10], 1f);
			CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
			yield return new WaitForSeconds(1.3f);
			this.listBoxCheckBullet[3].SetActive(false);
		}
		this.smoothFollow.isSmoothFollow = true;
		this.isBlind = false;
		yield return new WaitForSeconds(2f);
		if (this.isDie)
		{
			base.StartCoroutine(this.ChangeAnimDie());
		}
		else if (this.isActiveAngry && !this.isUpgrade)
		{
			this.ChangeAnimAngry();
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

	private bool isBlind;

	private bool isActiveAngry;

	private bool isUpgrade;

	private bool isDie;

	private float angryStat = 1f;

	[SerializeField]
	private SmoothFollow smoothFollow;

	[SerializeField]
	private GameObject[] listBoxCheckBullet;

	[SerializeField]
	private AudioClip[] listSound;

	[SerializeField]
	private SkeletonAnimation animationBoss;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animStart;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animIdle0;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animUpgrade;

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
	private string animSkill2a;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill2b;

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

	private int[] listPosUbh = new int[]
	{
		120,
		140,
		160,
		240,
		220,
		200
	};

	[SerializeField]
	private UbhShotCtrl skill0_Ubh;

	[SerializeField]
	private UbhRandomShot[] skill0_UbhRandom;

	private float skill1_TimeDelay;

	private float skill1_speedMove = 10f;

	private int skill1_numberBulletAura = 10;

	[SerializeField]
	private GameObject skill1_bulletAura;

	[SerializeField]
	private UbhShotCtrl[] skill1_Ubh;

	[SerializeField]
	private PathManager[] skill1_PathMoveDash;

	[SerializeField]
	private UbhNwayShot[] skill1a_UbhNwayShot;

	[SerializeField]
	private UbhNwayShot[] skill1b_UbhNwayShot;

	[SerializeField]
	private GameObject skill1_Warning;

	private float skill2_TimeDelay;

	[SerializeField]
	private UbhShotCtrl[] skill2_Ubh;

	[SerializeField]
	private UbhSpiralShot skill2_UbhSpiralShot;

	private float skill3_TimeDelay;

	[SerializeField]
	private UbhShotCtrl[] skill3_Ubh;

	[SerializeField]
	private UbhNwayShot skill3_UbhNway;

	[SerializeField]
	private GameObject skill3_Warning;

	private float skill4_TimeDelay;

	[SerializeField]
	private GameObject skill4_Warning;

	[SerializeField]
	private GameObject skill4_fxEarthQuake;

	private float skill5_TimeDelay;

	[SerializeField]
	private GameObject skill5_Warning;

	[SerializeField]
	private GameObject skill5_fxEarthQuake;

	private float skill6_TimeDelay;

	private float skill6_moveSpeed = 1f;

	[SerializeField]
	private GameObject[] skill6_Warning;

	[SerializeField]
	private splineMove skill6_SplineMove;

	[SerializeField]
	private PathManager skill6_PathMoveDash;

	[EnemyEventCustom(displayName = "BossTuan/BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

	[EnemyEventCustom(displayName = "BossTuan/StatAngry")]
	public EnemyEvent StatAngry;

	[EnemyEventCustom(displayName = "BossTuan/SpawnCoin")]
	public EnemyEvent SpawnCoin;

	private float rangeAttack = 30f;

	public LayerMask layerPlayer;

	public Transform player;

	public List<int> listID = new List<int>();

	private int currentTurnSkill;

	private int lastIdSkill = -1;

	private Vector2[] pathMid;
}
