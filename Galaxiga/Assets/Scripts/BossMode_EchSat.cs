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

public class BossMode_EchSat : NewBoss
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
		this.skeletonAnimation.timeScale = 1f + (1f - this.angryStat);
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry")]
	public void ActiveStatusAngry()
	{
		this.isActiveAngry = true;
		base.ShowRedScreen();
		EazySoundManager.PlaySound(this.listSound[1], 1f);
	}

	[EnemyAction(displayName = "Tuan/Skill0_ShotByHead")]
	public void Skill0_ShotByHead(float timeDelay, int numberTurn, int numberBulletPerTurn, float speedBullet)
	{
		this.skill0_TimeDelay = timeDelay;
		this.skill0_NumberBullet = numberTurn;
		for (int i = 0; i < numberTurn; i++)
		{
			this.skill0_UbhSin[i].m_bulletNum = numberBulletPerTurn;
			this.skill0_UbhSin[i].m_bulletSpeed = speedBullet;
		}
	}

	[EnemyAction(displayName = "Tuan/Skill1_Loll")]
	public void Skill1_Loll(float timeDelay)
	{
		this.skill1_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill2_ShotByTinyGuns")]
	public void Skill2_ShotByTinyGuns(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill2_TimeDelay = timeDelay;
		for (int i = 0; i < 2; i++)
		{
			this.skill2_UbhLinearShot[i].m_bulletNum = numberBullet;
			this.skill2_UbhLinearShot[i].m_bulletSpeed = speedBullet;
			this.skill2_UbhLinearShot[i].m_betweenDelay = 0.1f * this.angryStat;
			this.skill2_UbhNwayShot[i].m_bulletNum = numberBullet * 3;
			this.skill2_UbhNwayShot[i].m_bulletSpeed = speedBullet;
			this.skill2_UbhNwayShot[i].m_nextLineDelay = 0.1f * this.angryStat;
		}
	}

	[EnemyAction(displayName = "Tuan/Skill3_ShotByBigGuns")]
	public void Skill3_ShotByBigGuns(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill3_TimeDelay = timeDelay;
		this.skill3_numberBullet = numberBullet;
		this.skill3_UbhHommingShot[0].m_bulletNum = numberBullet;
		this.skill3_UbhHommingShot[0].m_bulletSpeed = speedBullet;
		this.skill3_UbhHommingShot[0].m_betweenDelay = 0.3f * this.angryStat;
		this.skill3_UbhHommingShot[1].m_bulletNum = numberBullet;
		this.skill3_UbhHommingShot[1].m_bulletSpeed = speedBullet;
		this.skill3_UbhHommingShot[1].m_betweenDelay = 0.3f * this.angryStat;
	}

	[EnemyAction(displayName = "Tuan/Skill4_SpawnMeteos")]
	public void Skill4_SpawnMeteos(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill4_TimeDelay = timeDelay;
		this.skill4_numberBullet = numberBullet;
		this.skill4_UbhRandom.m_bulletNum = numberBullet;
		this.skill4_UbhRandom.m_minSpeed = speedBullet;
		this.skill4_UbhRandom.m_maxSpeed = speedBullet + 1f;
	}

	[EnemyAction(displayName = "Tuan/Skill5_Dash")]
	public void Skill5_Dash(float timeDelay, float speedDash)
	{
		this.skill5_TimeDelay = timeDelay;
		this.skill5_SpeedDash = speedDash;
	}

	[EnemyAction(displayName = "Tuan/Skill6_MultiLoll")]
	public void Skill6_MultiLoll(float timeDelay)
	{
		this.skill6_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/FakeDie")]
	public void FakeDie()
	{
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
		base.SetNameBoss("Terminator Frog");
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
		if (!this.isActiveAngry)
		{
			while (this.listID.Count < 5)
			{
				int num = UnityEngine.Random.Range(0, 6);
				if (!this.listID.Contains(num) && num != 3)
				{
					this.listID.Add(num);
				}
			}
		}
		else
		{
			while (this.listID.Count < 7)
			{
				int item = UnityEngine.Random.Range(0, 7);
				if (!this.listID.Contains(item))
				{
					this.listID.Add(item);
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
		this.isShotting = true;
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
        if (anim == animSkill3)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, animTransform1, false).Complete += delegate
            {
                skeletonAnimation.AnimationState.SetAnimation(0, anim, false).Complete += delegate
                {
                    skeletonAnimation.AnimationState.SetAnimation(0, animTransform, false).Complete += delegate
                    {
                        skeletonAnimation.AnimationState.SetAnimation(0, animIdle0, true);
                    };
                };
            };
        }
        else
        {
            skeletonAnimation.AnimationState.SetAnimation(0, anim, false).Complete += delegate
            {
                skeletonAnimation.AnimationState.SetAnimation(0, animIdle0, true);
            };
        }
    }

	private void ChangeAnimAngry()
	{
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.skeletonAnimation.AnimationState.SetAnimation(0, this.animUpgrade, false).Complete += delegate(TrackEntry A_1)
			{
				this.isUpgrade = true;
				this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle1, true);
				base.StartCoroutine(this.AttackSkill3());
			};
		}
	}

	private void NextAttack()
	{
		this.isShotting = false;
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.ChangeAnimAngry();
		}
		else
		{
			this.RandomSkill();
		}
	}

	private IEnumerator AttackSkill0()
	{
		yield return new WaitForSeconds(this.skill0_TimeDelay);
		this.ChangeAnim(this.animSkill0);
		yield return new WaitForSeconds(1f * this.angryStat);
		this.isBlind = true;
		for (int i = 0; i < this.skill0_NumberBullet; i++)
		{
			this.skill0_UbhSin[i].m_centerAngle = (float)this.listPosUbh[i];
			this.skill0_UbhSin[i].m_centerAngle = base.gameObject.transform.eulerAngles.z + 180f + (this.skill0_UbhSin[i].m_centerAngle - 180f);
			this.skill0_Ubh[i].StartShotRoutine();
			EazySoundManager.PlaySound(this.listSound[3], 0.8f);
			yield return new WaitForSeconds(1.5f / (float)this.skill0_NumberBullet * this.angryStat);
		}
		this.isBlind = false;
		yield return new WaitForSeconds(0.7f * this.angryStat);
		yield return new WaitForSeconds(1f * this.angryStat);
		this.NextAttack();
		yield break;
	}

	private IEnumerator AttackSkill1()
	{
		yield return new WaitForSeconds(this.skill1_TimeDelay);
		this.skill1_Warning.SetActive(true);
		yield return new WaitForSeconds(1f);
		this.ChangeAnim(this.animSkill1);
		yield return new WaitForSeconds(0.75f * this.angryStat);
		this.isBlind = true;
		EazySoundManager.PlaySound(this.listSound[4], 1f);
		yield return new WaitForSeconds(0.75f * this.angryStat);
		CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
		Fu.SpawnExplosion(this.fx_Water, this.skill1_Warning.transform.GetChild(0).transform.position, Quaternion.identity);
		this.skill1_Warning.SetActive(false);
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.isBlind = false;
		yield return new WaitForSeconds(1f * this.angryStat);
		this.NextAttack();
		yield break;
	}

	private IEnumerator AttackSkill2()
	{
		yield return new WaitForSeconds(this.skill2_TimeDelay);
		this.ChangeAnim(this.animSkill2);
		yield return new WaitForSeconds(1.5f * this.angryStat);
		this.isBlind = true;
		this.skill2_UbhLinearShot[0].m_angle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill2_UbhLinearShot[1].m_angle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill2_Ubh[0].StartShotRoutine();
		this.fx_TinyGuns[0].SetActive(true);
		this.fx_TinyGuns[1].SetActive(true);
		for (int i = 0; i < 3; i++)
		{
			EazySoundManager.PlaySound(this.listSound[5], 1f);
			yield return new WaitForSeconds(0.3f * this.angryStat);
		}
		this.fx_TinyGuns[0].SetActive(false);
		this.fx_TinyGuns[1].SetActive(false);
		yield return new WaitForSeconds(0.9f * this.angryStat);
		this.fx_TinyGuns[0].SetActive(true);
		this.fx_TinyGuns[1].SetActive(true);
		this.skill2_UbhNwayShot[0].m_centerAngle = base.gameObject.transform.eulerAngles.z + 220f;
		this.skill2_UbhNwayShot[1].m_centerAngle = base.gameObject.transform.eulerAngles.z + 140f;
		this.skill2_Ubh[1].StartShotRoutine();
		for (int j = 0; j < 3; j++)
		{
			EazySoundManager.PlaySound(this.listSound[5], 1f);
			yield return new WaitForSeconds(0.27f * this.angryStat);
		}
		this.fx_TinyGuns[0].SetActive(false);
		this.fx_TinyGuns[1].SetActive(false);
		yield return new WaitForSeconds(1.23f * this.angryStat);
		this.isBlind = false;
		yield return new WaitForSeconds(1f * this.angryStat);
		this.NextAttack();
		yield break;
	}

	private IEnumerator AttackSkill3()
	{
		yield return new WaitForSeconds(this.skill3_TimeDelay);
		this.ChangeAnim(this.animSkill3);
		yield return new WaitForSeconds(2.8f * this.angryStat);
		this.skill3_Ubh[0].StartShotRoutine();
		for (int i = 0; i < this.skill3_numberBullet; i++)
		{
			EazySoundManager.PlaySound(this.listSound[6], 1f);
			yield return new WaitForSeconds(1.8f / (float)this.skill3_numberBullet * this.angryStat);
		}
		yield return new WaitForSeconds(0.7f * this.angryStat);
		this.skill3_Ubh[1].StartShotRoutine();
		for (int j = 0; j < this.skill3_numberBullet; j++)
		{
			EazySoundManager.PlaySound(this.listSound[6], 1f);
			yield return new WaitForSeconds(1.8f / (float)this.skill3_numberBullet * this.angryStat);
		}
		yield return new WaitForSeconds(1f * this.angryStat);
		this.NextAttack();
		yield break;
	}

	private IEnumerator AttackSkill4()
	{
		yield return new WaitForSeconds(this.skill4_TimeDelay);
		this.ChangeAnim(this.animSkill4);
		yield return new WaitForSeconds(2f * this.angryStat);
		this.isBlind = true;
		this.skill4_UbhRandom.m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill4_Ubh.StartShotRoutine();
		this.skill4_Warning.SetActive(true);
		EazySoundManager.PlaySound(this.listSound[7], 0.8f);
		yield return new WaitForSeconds(1f);
		this.skill4_Warning.SetActive(false);
		this.isBlind = false;
		yield return new WaitForSeconds(0.5f * this.angryStat);
		yield return new WaitForSeconds(1f * this.angryStat);
		this.NextAttack();
		yield break;
	}

	private IEnumerator AttackSkill5()
	{
		this.smoothFollow.isSmoothFollow = false;
		this.ChangeAnim(this.animSkill5);
		yield return new WaitForSeconds(1.5f * this.angryStat);
		this.isBlind = true;
		float distance = Vector3.Distance(base.gameObject.transform.position, this.player.position);
		float speed = distance / (1f * this.angryStat);
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
		EazySoundManager.PlaySound(this.listSound[8], 1f);
		yield return new WaitForSeconds(1f * this.angryStat);
		this.skill5_Warning.SetActive(false);
		this.smoothFollow.isSmoothFollow = true;
		yield return new WaitForSeconds(2.3f * this.angryStat);
		this.isBlind = false;
		yield return new WaitForSeconds(1f * this.angryStat);
		this.NextAttack();
		yield break;
	}

	private IEnumerator AttackSkill6()
	{
		yield return new WaitForSeconds(this.skill6_TimeDelay);
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid(new Vector2(0f, 2f));
		for (int i = 0; i < this.skill6_Warning.Length; i++)
		{
			this.skill6_Warning[i].SetActive(true);
			yield return new WaitForSeconds(0.8f * this.angryStat);
		}
		this.ChangeAnim(this.animSkill6);
		this.isBlind = true;
		yield return new WaitForSeconds(1f * this.angryStat);
		for (int j = 0; j < this.skill6_Warning.Length; j++)
		{
			EazySoundManager.PlaySound(this.listSound[4], 0.8f);
			yield return new WaitForSeconds(0.65f * this.angryStat);
			this.skill6_Warning[j].SetActive(false);
			CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
			Fu.SpawnExplosion(this.fx_Water, this.skill6_Warning[j].transform.position, Quaternion.identity);
			yield return new WaitForSeconds(0.15f * this.angryStat);
		}
		this.smoothFollow.isSmoothFollow = true;
		this.isBlind = false;
		yield return new WaitForSeconds(1.5f * this.angryStat);
		yield return new WaitForSeconds(1f * this.angryStat);
		this.NextAttack();
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

	private int GetRandomValue(params BossMode_EchSat.RandomSelection[] selections)
	{
		float value = UnityEngine.Random.value;
		float num = 0f;
		foreach (BossMode_EchSat.RandomSelection randomSelection in selections)
		{
			num += randomSelection.probability;
			if (value <= num)
			{
				return randomSelection.GetValue();
			}
		}
		return -1;
	}

	public bool isShotting;

	private bool isUpgrade;

	private bool isBlind;

	private bool isActiveAngry;

	private float angryStat = 1f;

	[SerializeField]
	private SmoothFollow smoothFollow;

	[SerializeField]
	private PolygonCollider2D polygonCollider2D;

	[SerializeField]
	private AudioClip[] listSound;

	[SerializeField]
	private SkeletonAnimation skeletonAnimation;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animStart;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animIdle0;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animTransform;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animTransform1;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animUpgrade;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animIdle1;

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

	private float skill0_TimeDelay;

	private int[] listPosUbh = new int[]
	{
		180,
		160,
		210,
		175,
		230,
		140,
		190,
		240,
		130
	};

	private int skill0_NumberBullet;

	[SerializeField]
	private UbhShotCtrl[] skill0_Ubh;

	[SerializeField]
	private UbhSinWaveBulletNwayShot[] skill0_UbhSin;

	private float skill1_TimeDelay;

	[SerializeField]
	private GameObject skill1_Warning;

	[SerializeField]
	private GameObject fx_Water;

	private float skill2_TimeDelay;

	[SerializeField]
	private UbhShotCtrl[] skill2_Ubh;

	[SerializeField]
	private UbhLinearShot[] skill2_UbhLinearShot;

	[SerializeField]
	private UbhNwayShot[] skill2_UbhNwayShot;

	[SerializeField]
	private GameObject[] fx_TinyGuns;

	private float skill3_TimeDelay;

	private int skill3_numberBullet;

	[SerializeField]
	private UbhShotCtrl[] skill3_Ubh;

	[SerializeField]
	private UbhHomingShot[] skill3_UbhHommingShot;

	private float skill4_TimeDelay;

	private int skill4_numberBullet;

	[SerializeField]
	private UbhShotCtrl skill4_Ubh;

	[SerializeField]
	private UbhRandomSpiralShot skill4_UbhRandom;

	[SerializeField]
	private GameObject skill4_Warning;

	private float skill5_TimeDelay;

	private float skill5_SpeedDash = 1.2f;

	[SerializeField]
	private splineMove skill5_SplineMove;

	[SerializeField]
	private PathManager skill5_PathMoveDash;

	[SerializeField]
	private GameObject skill5_Warning;

	private float skill6_TimeDelay;

	[SerializeField]
	private GameObject[] skill6_Warning;

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
