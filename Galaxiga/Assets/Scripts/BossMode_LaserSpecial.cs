using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SkyGameKit;
using Spine;
using Spine.Unity;
using SWS;
using UnityEngine;

public class BossMode_LaserSpecial : NewBoss
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

	[EnemyAction(displayName = "Tuan/Skill0_RainRocket")]
	public void Skill0_ShotByMouth(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill1_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill1_ShotByMouth")]
	public void Skill1_ShotByMouth(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill1_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill2_ShotRocket")]
	public void Skill2_ShotRocket(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill2_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill3_Dash")]
	public void Skill3_Dash(float timeDelay, float speedMove)
	{
		this.skill3_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill4_ShotByMidEye")]
	public void Skill4_ShotByMidEye(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill4_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill5_LaserEyes")]
	public void Skill5_LaserEyes(float timeDelay, float speedBullet)
	{
		this.skill5_TimeDelay = timeDelay;
	}

	public override void Restart()
	{
		base.Restart();
		if (this.BaseStatBoss != null)
		{
			this.BaseStatBoss();
		}
		base.ShowHealthBar();
		base.SetNameBoss("Mr.ENDGAME");
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
		{
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill0a());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill0b());
			}
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha2))
		{
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill1a());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill1b());
			}
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha3))
		{
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill2a());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill2b());
			}
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha4))
		{
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill3a());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill3b());
			}
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha5))
		{
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill4a());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill4b());
			}
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha6))
		{
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill5a());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill5b());
			}
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha0))
		{
			this.isActiveAngry = true;
			this.Transform();
		}
		if (this.isRegen)
		{
			this.RegenHP();
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
				base.StartCoroutine(this.AttackSkill0a());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill0b());
			}
			break;
		case 1:
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill1a());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill1b());
			}
			break;
		case 2:
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill2a());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill2b());
			}
			break;
		case 3:
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill3a());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill3b());
			}
			break;
		case 4:
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill4a());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill4b());
			}
			break;
		case 5:
			if (!this.isActiveAngry)
			{
				base.StartCoroutine(this.AttackSkill5a());
			}
			else
			{
				base.StartCoroutine(this.AttackSkill5b());
			}
			break;
		}
	}

	private void ChangeAnim(string anim)
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
		};
	}

	private void Transform()
	{
		this.skeletonAnimation.AnimationState.SetAnimation(0, this.animTransform, false).Complete += delegate(TrackEntry A_1)
		{
			if (!this.isActiveAngry)
			{
				this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle0, true);
			}
			else
			{
				this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdle1, true);
			}
			this.isUpgrade = true;
			this.RandomSkill();
		};
	}

	private IEnumerator AttackSkill0a()
	{
		yield return new WaitForSeconds(this.skill0_TimeDelay);
		this.MoveMid(new Vector3(0f, 4f));
		yield return new WaitForSeconds(2f);
		for (int i = 0; i < 3; i++)
		{
			this.ChangeAnim(this.animSkill0a);
			yield return new WaitForSeconds(0.5f);
			this.skill0_Ubh[0].StartShotRoutine();
			yield return new WaitForSeconds(1f);
		}
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.Transform();
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill0b()
	{
		yield return new WaitForSeconds(this.skill0_TimeDelay);
		this.MoveMid(new Vector3(0f, 4f));
		yield return new WaitForSeconds(2f);
		for (int i = 0; i < 2; i++)
		{
			this.ChangeAnim(this.animSkill0b);
			yield return new WaitForSeconds(0.5f);
			this.skill0_Ubh[1].StartShotRoutine();
			yield return new WaitForSeconds(1.5f);
		}
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.Transform();
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill1a()
	{
		yield return new WaitForSeconds(this.skill1_TimeDelay);
		this.isBlind = true;
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid(new Vector2(0f, 6f));
		yield return new WaitForSeconds(3f);
		this.ChangeAnim(this.animSkill1a);
		this.splineMove.speed = this.skill1_TimeMove;
		this.splineMove.SetPath(this.skill1_PathMove);
		for (int i = 0; i < 6; i++)
		{
			this.skill1_Ubh[0].StartShotRoutine();
			yield return new WaitForSeconds(2f);
		}
		this.isBlind = false;
		this.smoothFollow.isSmoothFollow = true;
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.Transform();
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill1b()
	{
		yield return new WaitForSeconds(this.skill1_TimeDelay);
		this.isBlind = true;
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid(new Vector2(0f, 6f));
		yield return new WaitForSeconds(3f);
		this.ChangeAnim(this.animSkill1b);
		this.splineMove.speed = this.skill1_TimeMove;
		this.splineMove.SetPath(this.skill1_PathMove);
		for (int i = 0; i < 6; i++)
		{
			this.skill1_Ubh[1].StartShotRoutine();
			yield return new WaitForSeconds(2f);
		}
		this.isBlind = false;
		this.smoothFollow.isSmoothFollow = true;
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.Transform();
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill2a()
	{
		yield return new WaitForSeconds(this.skill2_TimeDelay);
		this.isBlind = true;
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid(new Vector2(0f, 0f));
		yield return new WaitForSeconds(1.5f);
		this.ChangeAnim(this.animSkill2a);
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < 5; i++)
		{
			this.skill2_Ubh[0].StartShotRoutine();
			yield return new WaitForSeconds(0.5f);
		}
		yield return new WaitForSeconds(1f);
		this.isBlind = false;
		this.smoothFollow.isSmoothFollow = true;
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.Transform();
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill2b()
	{
		yield return new WaitForSeconds(this.skill2_TimeDelay);
		this.isBlind = true;
		this.smoothFollow.isSmoothFollow = false;
		this.MoveMid(new Vector2(0f, 0f));
		yield return new WaitForSeconds(1.5f);
		this.ChangeAnim(this.animSkill2b);
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < 5; i++)
		{
			this.skill2_Ubh[1].StartShotRoutine();
			yield return new WaitForSeconds(0.5f);
		}
		this.smoothFollow.isSmoothFollow = true;
		yield return new WaitForSeconds(1f);
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.Transform();
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill3a()
	{
		yield return new WaitForSeconds(this.skill3_TimeDelay);
		this.isBlind = true;
		this.smoothFollow.isSmoothFollow = false;
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 1f, DG.Tweening.RotateMode.Fast);
		this.MoveMid(new Vector2(0f, 4f));
		yield return new WaitForSeconds(2f);
		this.ChangeAnim(this.animSkill3a);
		this.skill3_Laser.SetActive(true);
		this.splineMove.speed = this.skill3_TimeMove;
		this.splineMove.SetPath(this.skill3_PathMove);
		this.skill3_UbhRandomSpiral[0].m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill3_UbhRandomSpiral[1].m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill3_Ubh.StartShotRoutine();
		this.skill3_Fx[0].SetActive(true);
		this.skill3_Fx[1].SetActive(true);
		yield return new WaitForSeconds(this.skill3_TimeMove);
		this.skill3_Fx[0].SetActive(false);
		this.skill3_Fx[1].SetActive(false);
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 180f), 1.5f, DG.Tweening.RotateMode.Fast);
		yield return new WaitForSeconds(1.5f);
		this.ChangeAnim(this.animSkill3a);
		this.splineMove.SetPath(this.skill3_PathMove);
		this.skill3_UbhRandomSpiral[0].m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill3_UbhRandomSpiral[1].m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill3_Ubh.StartShotRoutine();
		this.skill3_Fx[0].SetActive(true);
		this.skill3_Fx[1].SetActive(true);
		yield return new WaitForSeconds(this.skill3_TimeMove);
		this.skill3_Fx[0].SetActive(false);
		this.skill3_Fx[1].SetActive(false);
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 1.5f, DG.Tweening.RotateMode.Fast);
		yield return new WaitForSeconds(1.5f);
		this.skill3_Laser.SetActive(false);
		this.isBlind = false;
		this.smoothFollow.isSmoothFollow = true;
		yield return new WaitForSeconds(1f);
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.Transform();
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill3b()
	{
		yield return new WaitForSeconds(this.skill3_TimeDelay);
		this.isBlind = true;
		this.smoothFollow.isSmoothFollow = false;
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 1f, DG.Tweening.RotateMode.Fast);
		this.MoveMid(new Vector2(0f, 4f));
		yield return new WaitForSeconds(2f);
		this.ChangeAnim(this.animSkill3b);
		this.skill3_Laser.SetActive(true);
		this.splineMove.speed = this.skill3_TimeMove;
		this.splineMove.SetPath(this.skill3_PathMove);
		this.skill3_UbhRandomSpiral[0].m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill3_UbhRandomSpiral[1].m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill3_Ubh.StartShotRoutine();
		this.skill3_Fx[0].SetActive(true);
		this.skill3_Fx[1].SetActive(true);
		yield return new WaitForSeconds(this.skill3_TimeMove);
		this.skill3_Fx[0].SetActive(false);
		this.skill3_Fx[1].SetActive(false);
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 180f), 1.5f, DG.Tweening.RotateMode.Fast);
		yield return new WaitForSeconds(1.5f);
		this.ChangeAnim(this.animSkill3b);
		this.splineMove.SetPath(this.skill3_PathMove);
		this.skill3_UbhRandomSpiral[0].m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill3_UbhRandomSpiral[1].m_startAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.skill3_Ubh.StartShotRoutine();
		this.skill3_Fx[0].SetActive(true);
		this.skill3_Fx[1].SetActive(true);
		yield return new WaitForSeconds(this.skill3_TimeMove);
		this.skill3_Fx[0].SetActive(false);
		this.skill3_Fx[1].SetActive(false);
		base.gameObject.transform.DORotate(new Vector3(0f, 0f, 0f), 1.5f, DG.Tweening.RotateMode.Fast);
		yield return new WaitForSeconds(1.5f);
		this.skill3_Laser.SetActive(false);
		this.isBlind = false;
		this.smoothFollow.isSmoothFollow = true;
		yield return new WaitForSeconds(2f);
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.Transform();
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill4a()
	{
		yield return new WaitForSeconds(this.skill4_TimeDelay);
		this.ChangeAnim(this.animSkill4a);
		this.skill4_Ubh[0].StartShotRoutine();
		yield return new WaitForSeconds(3f);
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.Transform();
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill4b()
	{
		yield return new WaitForSeconds(this.skill4_TimeDelay);
		this.ChangeAnim(this.animSkill4b);
		this.skill4_Ubh[1].StartShotRoutine();
		yield return new WaitForSeconds(3f);
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.Transform();
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill5a()
	{
		yield return new WaitForSeconds(this.skill5_TimeDelay);
		this.ChangeAnim(this.animSkill5a);
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < 3; i++)
		{
			this.skill5_Ubh[i].StartShotRoutine();
			yield return new WaitForSeconds(0.3f);
		}
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.Transform();
		}
		else
		{
			this.RandomSkill();
		}
		yield break;
	}

	private IEnumerator AttackSkill5b()
	{
		yield return new WaitForSeconds(this.skill5_TimeDelay);
		this.ChangeAnim(this.animSkill5b);
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < 5; i++)
		{
			if (i < 3)
			{
				this.skill5_Ubh[i].StartShotRoutine();
			}
			else
			{
				this.skill5_Ubh[i - 3].StartShotRoutine();
			}
			yield return new WaitForSeconds(0.2f);
		}
		if (this.isActiveAngry && !this.isUpgrade)
		{
			this.Transform();
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

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
	}

	private int GetRandomValue(params BossMode_LaserSpecial.RandomSelection[] selections)
	{
		float value = UnityEngine.Random.value;
		float num = 0f;
		foreach (BossMode_LaserSpecial.RandomSelection randomSelection in selections)
		{
			num += randomSelection.probability;
			if (value <= num)
			{
				return randomSelection.GetValue();
			}
		}
		return -1;
	}

	public void RegenHP()
	{
		if (this.CurrentHP < this.startHP)
		{
			this.CurrentHP += 2000;
		}
	}

	private bool isActiveAngry;

	private bool isUpgrade;

	private float angryStat = 1f;

	public bool isBlind;

	public bool isRegen;

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
	private string animTransform;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill0a;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill1a;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill2a;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill3a;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill4a;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill5a;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill0b;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill1b;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill2b;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill3b;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill4b;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSkill5b;

	private float skill0_TimeDelay = 1f;

	[SerializeField]
	private UbhShotCtrl[] skill0_Ubh;

	private float skill1_TimeDelay = 1f;

	private float skill1_TimeMove = 5f;

	[SerializeField]
	private UbhShotCtrl[] skill1_Ubh;

	[SerializeField]
	private PathManager skill1_PathMove;

	private float skill2_TimeDelay = 1f;

	[SerializeField]
	private UbhShotCtrl[] skill2_Ubh;

	private float skill3_TimeDelay = 1f;

	private float skill3_TimeMove = 3f;

	[SerializeField]
	private GameObject skill3_Laser;

	[SerializeField]
	private PathManager skill3_PathMove;

	[SerializeField]
	private splineMove splineMove;

	[SerializeField]
	private UbhShotCtrl skill3_Ubh;

	[SerializeField]
	private UbhRandomSpiralShot[] skill3_UbhRandomSpiral;

	[SerializeField]
	private GameObject[] skill3_Fx;

	private float skill4_TimeDelay = 1f;

	[SerializeField]
	private UbhShotCtrl[] skill4_Ubh;

	private float skill5_TimeDelay = 1f;

	[SerializeField]
	private UbhShotCtrl[] skill5_Ubh;

	[EnemyEventCustom(displayName = "BossTuan/CuaCu_BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

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
