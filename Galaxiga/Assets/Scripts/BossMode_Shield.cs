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

public class BossMode_Shield : NewBoss
{
	[EnemyAction(displayName = "Tuan/StartAttack")]
	public void StartAttack()
	{
		this.RandomSkill();
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry")]
	public void ActiveStatusAngry()
	{
		base.ShowRedScreen();
	}

	[EnemyAction(displayName = "Tuan/Skill0_ShotByGuns0")]
	public void Skill0_ShotByGuns0(float timeDelay, float speedBullet)
	{
		this.skill0_TimeDelay = timeDelay;
		this.skill0_ubhLinear[0].m_bulletSpeed = speedBullet;
		this.skill0_ubhLinear[1].m_bulletSpeed = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill1_ShotByGuns1")]
	public void Skill1_ShotByGuns1(float timeDelay, float speedBullet)
	{
		this.skill1_TimeDelay = timeDelay;
		this.skill1_ubhLinear[0].m_bulletSpeed = speedBullet;
		this.skill1_ubhLinear[1].m_bulletSpeed = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill2_ShotByTail")]
	public void Skill2_ShotByTail(float timeDelay, float speedBullet)
	{
		this.skill2_TimeDelay = timeDelay;
		this.skill2_UbhSinWave.m_bulletSpeed = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill3_LaserEyes")]
	public void Skill3_LaserEyes(float timeDelay)
	{
		this.skill3_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill4_ShotByStomach")]
	public void Skill4_ShotByStomach(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill4_TimeDelay = timeDelay;
		this.skill4_numberBullet = numberBullet;
		this.skill4_speedBullet = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill5_Dash")]
	public void Skill5_Dash(float timeDelay)
	{
		this.skill5_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/FakeDie")]
	public void FakeDie()
	{
		this.isDie = true;
	}

	[EnemyAction(displayName = "Tuan/SetStatShield")]
	public void SetStatShield(int _hpLock, int _timeReactive)
	{
		this.hpLock = _hpLock;
		this.timeReactiveProtect = (float)_timeReactive;
	}

	[EnemyAction(displayName = "Tuan/SetStatPathOfBoss")]
	public void SetStatPathOfBoss(int _hpHandRight, int _hpLeftHand, int _hpTail)
	{
		this.hpRightHand = _hpHandRight;
		this.hpLeftHand = _hpLeftHand;
		this.hpTail = _hpTail;
	}

	public override void Restart()
	{
		base.Restart();
		if (this.BaseStatBoss != null)
		{
			this.BaseStatBoss();
		}
		base.ShowHealthBar();
		base.SetNameBoss("Killer Bee");
		this.objProtect.SetStat(this.timeReactiveProtect);
		this.objLock[0].SetStat(this.hpLock);
		this.objLock[1].SetStat(this.hpLock);
		this.objLock[2].SetStat(this.hpLock);
		this.objRightHand.SetStat(this.hpRightHand);
		this.objLeftHand.SetStat(this.hpLeftHand);
		this.objTail.SetStat(this.hpTail);
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
		if (this.player != null && !this.isBlind)
		{
			Vector3 vector = this.player.position - base.transform.position;
			vector.Normalize();
			float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(0f, 0f, num + 90f), Time.deltaTime * 5f);
		}
	}

	private void OnEnable()
	{
		this.isBlind = true;
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
			if (this.isDestroyTail && this.listID[this.currentTurnSkill] == 5)
			{
				this.currentTurnSkill++;
				this.RandomSkill();
			}
			else if (this.isDestroyLeftHand && this.isDestroyRightHand && this.listID[this.currentTurnSkill] == 0)
			{
				this.currentTurnSkill++;
				this.RandomSkill();
			}
			else if (this.isDestroyLeftHand && this.isDestroyRightHand && this.listID[this.currentTurnSkill] == 1)
			{
				this.currentTurnSkill++;
				this.RandomSkill();
			}
			else
			{
				this.Attack();
				this.currentTurnSkill++;
			}
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
		}
	}

	private void ChangeAnim(string anim)
	{
		this.animBoss[0].AnimationState.SetAnimation(0, anim, false).Complete += delegate(TrackEntry A_1)
		{
			this.animBoss[0].AnimationState.SetAnimation(0, this.animIdle0, true);
		};
		this.animBoss[1].AnimationState.SetAnimation(0, anim, false).Complete += delegate(TrackEntry A_1)
		{
			this.animBoss[1].AnimationState.SetAnimation(0, this.animIdle0, true);
		};
		if (!this.isDestroyTail)
		{
			this.animBoss[2].AnimationState.SetAnimation(0, anim, false).Complete += delegate(TrackEntry A_1)
			{
				this.animBoss[2].AnimationState.SetAnimation(0, this.animIdle0, true);
			};
		}
		if (!this.isDestroyLeftHand)
		{
			this.animBoss[3].AnimationState.SetAnimation(0, anim, false).Complete += delegate(TrackEntry A_1)
			{
				this.animBoss[3].AnimationState.SetAnimation(0, this.animIdle0, true);
			};
		}
		if (!this.isDestroyRightHand)
		{
			this.animBoss[4].AnimationState.SetAnimation(0, anim, false).Complete += delegate(TrackEntry A_1)
			{
				this.animBoss[4].AnimationState.SetAnimation(0, this.animIdle0, true);
			};
		}
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
		for (int i = 0; i < this.animBoss.Length; i++)
		{
			this.animBoss[i].AnimationState.SetAnimation(0, this.animDie, false).Complete += delegate(TrackEntry A_1)
			{
				this.Die(EnemyKilledBy.Player);
			};
		}
		yield return new WaitForSeconds(1f);
		Fu.SpawnExplosion(this.dieExplosion, base.transform.position, Quaternion.identity);
		yield break;
	}

	private IEnumerator AttackSkill0()
	{
		yield return new WaitForSeconds(this.skill0_TimeDelay);
		this.ChangeAnim(this.animSkill0);
		yield return new WaitForSeconds(0.7f);
		this.fx_GunsShot[0].SetActive(true);
		this.fx_GunsShot[1].SetActive(true);
		for (int i = 0; i < 12; i++)
		{
			this.skill0_ubhLinear[0].m_angle = this.skill0_hand[0].transform.eulerAngles.z + 180f;
			this.skill0_ubhLinear[1].m_angle = this.skill0_hand[1].transform.eulerAngles.z + 180f;
			if (!this.isDestroyRightHand)
			{
				this.skill0_Ubh[0].StartShotRoutine();
			}
			if (!this.isDestroyLeftHand)
			{
				this.skill0_Ubh[1].StartShotRoutine();
			}
			yield return new WaitForSeconds(0.2f);
		}
		this.fx_GunsShot[0].SetActive(false);
		this.fx_GunsShot[1].SetActive(false);
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
		yield return new WaitForSeconds(0.7f);
		for (int i = 0; i < 3; i++)
		{
			this.skill1_ubhLinear[0].m_angle = this.skill0_hand[0].transform.eulerAngles.z + 180f;
			this.skill1_ubhLinear[1].m_angle = this.skill0_hand[1].transform.eulerAngles.z + 180f;
			if (!this.isDestroyRightHand)
			{
				this.skill1_Ubh[0].StartShotRoutine();
			}
			this.fx_GunsShot[0].SetActive(true);
			yield return new WaitForSeconds(0.1f);
			if (!this.isDestroyLeftHand)
			{
				this.skill1_Ubh[1].StartShotRoutine();
			}
			this.fx_GunsShot[1].SetActive(true);
			yield return new WaitForSeconds(0.1f);
			this.fx_GunsShot[0].SetActive(false);
			yield return new WaitForSeconds(0.1f);
			this.fx_GunsShot[1].SetActive(false);
			yield return new WaitForSeconds(0.3f);
		}
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
		this.MoveMid(new Vector2(0f, 4f));
		yield return new WaitForSeconds(1f);
		this.ChangeAnim(this.animSkill2);
		yield return new WaitForSeconds(0.8f);
		this.skill2_Ubh.StartShotRoutine();
		yield return new WaitForSeconds(2f);
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
		this.ChangeAnim(this.animSkill3);
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < 7; i += 2)
		{
			this.skill3_Warning[i].SetActive(true);
		}
		yield return new WaitForSeconds(0.5f);
		for (int j = 0; j < 7; j += 2)
		{
			this.skill3_Warning[j].SetActive(false);
		}
		for (int k = 0; k < 7; k += 2)
		{
			this.skill3_Laser[k].SetActive(true);
		}
		yield return new WaitForSeconds(1.5f);
		for (int l = 1; l < 6; l += 2)
		{
			this.skill3_Warning[l].SetActive(true);
		}
		yield return new WaitForSeconds(0.5f);
		for (int m = 0; m < 7; m += 2)
		{
			this.skill3_Laser[m].SetActive(false);
		}
		for (int n = 1; n < 6; n += 2)
		{
			this.skill3_Warning[n].SetActive(false);
		}
		for (int num = 1; num < 6; num += 2)
		{
			this.skill3_Laser[num].SetActive(true);
		}
		yield return new WaitForSeconds(1.5f);
		for (int num2 = 0; num2 < 7; num2 += 2)
		{
			this.skill3_Warning[num2].SetActive(true);
		}
		yield return new WaitForSeconds(0.5f);
		for (int num3 = 1; num3 < 6; num3 += 2)
		{
			this.skill3_Laser[num3].SetActive(false);
		}
		for (int num4 = 0; num4 < 7; num4 += 2)
		{
			this.skill3_Warning[num4].SetActive(false);
		}
		for (int num5 = 0; num5 < 7; num5 += 2)
		{
			this.skill3_Laser[num5].SetActive(true);
		}
		yield return new WaitForSeconds(1.5f);
		for (int num6 = 1; num6 < 6; num6 += 2)
		{
			this.skill3_Warning[num6].SetActive(true);
		}
		yield return new WaitForSeconds(0.5f);
		for (int num7 = 0; num7 < 7; num7 += 2)
		{
			this.skill3_Laser[num7].SetActive(false);
		}
		for (int num8 = 1; num8 < 6; num8 += 2)
		{
			this.skill3_Warning[num8].SetActive(false);
		}
		for (int num9 = 1; num9 < 6; num9 += 2)
		{
			this.skill3_Laser[num9].SetActive(true);
		}
		yield return new WaitForSeconds(1.5f);
		for (int num10 = 1; num10 < 6; num10 += 2)
		{
			this.skill3_Laser[num10].SetActive(false);
		}
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
		yield return new WaitForSeconds(1f);
		this.skill4_FxAttack.SetActive(true);
		this.skill4_Warning.SetActive(false);
		for (int i = 0; i < this.skill4_numberBullet; i += 2)
		{
			this.skill4_Bfp.Attack(i, 0, this.skill4_speedBullet, null, false);
			this.skill4_Bfp.Attack(i + 1, 0, this.skill4_speedBullet, null, false);
			yield return new WaitForSeconds(0.15f);
		}
		this.skill4_FxAttack.SetActive(false);
		yield return new WaitForSeconds(1f);
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
		this.smoothFollow.isSmoothFollow = false;
		this.isBlind = false;
		yield return new WaitForSeconds(1f);
		this.ChangeAnim(this.animSkill5);
		this.skill5_FxAttack.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		float time = 0.8f;
		float distance = Vector3.Distance(base.gameObject.transform.position, this.player.position);
		float speed = distance / time;
		if (this.skill5_PathMoveDash.GetComponent<BezierPathManager>() != null)
		{
			this.skill5_PathMoveDash.GetComponent<BezierPathManager>().CalculatePath();
		}
		if (this.player.transform)
		{
			this.skill5_PathMoveDash.waypoints[this.skill5_PathMoveDash.waypoints.Length - 2].transform.position = new Vector3(this.player.transform.position.x, this.player.transform.position.y, 0f);
		}
		this.skill5_SplineMove.speed = speed;
		this.skill5_Warning.SetActive(true);
		this.skill5_SplineMove.SetPath(this.skill5_PathMoveDash);
		this.isBlind = true;
		yield return new WaitForSeconds(2f);
		base.transform.DORotate(new Vector3(0f, 0f, 0f), 1f, DG.Tweening.RotateMode.Fast);
		yield return new WaitForSeconds(1f);
		this.skill5_Warning.SetActive(false);
		this.skill5_FxAttack.SetActive(false);
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

	private void MoveMid(Vector2 vt2)
	{
		this.pathMid = new Vector2[]
		{
			new Vector2(base.transform.position.x, base.transform.position.y),
			vt2
		};
		this.BaseMoveCurve(2f, Ease.Linear, PathMode.Ignore, this.pathMid);
	}

	private int GetRandomValue(params BossMode_Shield.RandomSelection[] selections)
	{
		float value = UnityEngine.Random.value;
		float num = 0f;
		foreach (BossMode_Shield.RandomSelection randomSelection in selections)
		{
			num += randomSelection.probability;
			if (value <= num)
			{
				return randomSelection.GetValue();
			}
		}
		return -1;
	}

	private bool isBlind;

	private bool isDie;

	public bool isDestroyRightHand;

	public bool isDestroyLeftHand;

	public bool isDestroyTail;

	[SerializeField]
	private SmoothFollow smoothFollow;

	[SerializeField]
	private AudioClip[] listSound;

	[SerializeField]
	private SkeletonAnimation[] animBoss;

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
	private string animIdle3;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animTransform0;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animTransform1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animTransform2;

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

	private float skill0_TimeDelay = 2f;

	[SerializeField]
	private UbhShotCtrl[] skill0_Ubh;

	[SerializeField]
	private GameObject[] fx_GunsShot;

	[SerializeField]
	private UbhLinearShot[] skill0_ubhLinear;

	[SerializeField]
	private GameObject[] skill0_hand;

	private float skill1_TimeDelay = 2f;

	[SerializeField]
	private UbhShotCtrl[] skill1_Ubh;

	[SerializeField]
	private UbhLinearShot[] skill1_ubhLinear;

	private float skill2_TimeDelay = 2f;

	[SerializeField]
	private UbhShotCtrl skill2_Ubh;

	[SerializeField]
	private UbhSinWaveBulletNwayShot skill2_UbhSinWave;

	private float skill3_TimeDelay = 2f;

	[SerializeField]
	private GameObject[] skill3_Warning;

	[SerializeField]
	private GameObject[] skill3_Laser;

	private float skill4_TimeDelay = 2f;

	private int skill4_numberBullet = 8;

	private float skill4_speedBullet = 6f;

	[SerializeField]
	private GameObject skill4_Warning;

	[SerializeField]
	private GameObject skill4_FxAttack;

	[SerializeField]
	private BulletFollowPath skill4_Bfp;

	private float skill5_TimeDelay = 2f;

	private float skill5_SpeedDash = 1.2f;

	[SerializeField]
	private splineMove skill5_SplineMove;

	[SerializeField]
	private PathManager skill5_PathMoveDash;

	[SerializeField]
	private GameObject skill5_Warning;

	[SerializeField]
	private GameObject skill5_FxAttack;

	private int hpProtect;

	private int hpLock;

	private float timeReactiveProtect;

	[SerializeField]
	private BossMode_Lock[] objLock;

	[SerializeField]
	private BossMode_Protect objProtect;

	private int hpRightHand;

	private int hpLeftHand;

	private int hpTail;

	[SerializeField]
	private BossMode_Shield_PartOfBoss objRightHand;

	[SerializeField]
	private BossMode_Shield_PartOfBoss objLeftHand;

	[SerializeField]
	private BossMode_Shield_PartOfBoss objTail;

	[EnemyEventCustom(displayName = "BossTuan/BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

	[EnemyEventCustom(displayName = "BossTuan/SpawnCoin")]
	public EnemyEvent SpawnCoin;

	private float rangeAttack = 30f;

	public LayerMask layerPlayer;

	public Transform player;

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
