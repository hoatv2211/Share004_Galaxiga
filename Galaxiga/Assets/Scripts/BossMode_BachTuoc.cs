using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SkyGameKit;
using UnityEngine;

public class BossMode_BachTuoc : NewBoss
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

	[EnemyAction(displayName = "Tuan/Skill0_ShotPaint")]
	public void Skill0_ShotPaint(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill1_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill1_ThrowBullet")]
	public void Skill1_ThrowBullet(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill1_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill2_Laser")]
	public void Skill2_Laser(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill2_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill3_Bubble")]
	public void Skill3_Bubble(float timeDelay, float speedMove)
	{
		this.skill3_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill4_Smoke")]
	public void Skill4_Smoke(float timeDelay, int numberBullet, float speedBullet)
	{
		this.skill4_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill5_Homming")]
	public void Skill5_Homming(float timeDelay, float speedBullet)
	{
		this.skill5_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill6_Around")]
	public void Skill6_Around(float timeDelay, float speedBullet)
	{
		this.skill6_TimeDelay = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill7_Dash")]
	public void Skill7_Dash(float timeDelay, float speedBullet)
	{
		this.skill7_TimeDelay = timeDelay;
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
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1) && !this.isActiveAngry)
		{
			base.StartCoroutine(this.AttackSkill0());
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
		}
	}

	private void ChangeAnim(string strAnim)
	{
		this.anim.SetTrigger(strAnim);
	}

	private void Transform()
	{
	}

	private IEnumerator AttackSkill0()
	{
		yield return new WaitForSeconds(this.skill0_TimeDelay);
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

	private IEnumerator AttackSkill1()
	{
		yield return new WaitForSeconds(this.skill1_TimeDelay);
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

	private IEnumerator AttackSkill2()
	{
		yield return new WaitForSeconds(this.skill2_TimeDelay);
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

	private IEnumerator AttackSkill3()
	{
		yield return new WaitForSeconds(this.skill3_TimeDelay);
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

	private IEnumerator AttackSkill4()
	{
		yield return new WaitForSeconds(this.skill4_TimeDelay);
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

	private IEnumerator AttackSkill5()
	{
		yield return new WaitForSeconds(this.skill5_TimeDelay);
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

	private IEnumerator AttackSkill6()
	{
		yield return new WaitForSeconds(this.skill6_TimeDelay);
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

	private IEnumerator AttackSkill7()
	{
		yield return new WaitForSeconds(this.skill7_TimeDelay);
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

	private int GetRandomValue(params BossMode_BachTuoc.RandomSelection[] selections)
	{
		float value = UnityEngine.Random.value;
		float num = 0f;
		foreach (BossMode_BachTuoc.RandomSelection randomSelection in selections)
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

	public bool isBlind;

	[SerializeField]
	private SmoothFollow smoothFollow;

	[SerializeField]
	private Animator anim;

	[SerializeField]
	private AudioClip[] listSound;

	private float skill0_TimeDelay = 1f;

	[SerializeField]
	private UbhShotCtrl[] skill0_Ubh;

	private float skill1_TimeDelay = 1f;

	[SerializeField]
	private UbhShotCtrl[] skill1_Ubh;

	private float skill2_TimeDelay = 1f;

	[SerializeField]
	private UbhShotCtrl[] skill2_Ubh;

	private float skill3_TimeDelay = 1f;

	private float skill4_TimeDelay = 1f;

	private float skill5_TimeDelay = 1f;

	private float skill6_TimeDelay = 1f;

	[SerializeField]
	private UbhShotCtrl[] skill6_Ubh;

	private float skill7_TimeDelay = 1f;

	[SerializeField]
	private UbhShotCtrl[] skill7_Ubh;

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
