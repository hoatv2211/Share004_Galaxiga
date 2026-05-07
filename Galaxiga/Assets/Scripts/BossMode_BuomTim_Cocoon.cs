using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using SkyGameKit;
using Spine;
using Spine.Unity;
using UnityEngine;

public class BossMode_BuomTim_Cocoon : NewBoss
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
		EazySoundManager.PlaySound(this.listSound[1], 1f);
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAdult")]
	public void ActiveStatusAdult()
	{
		this.isActiveAdult = true;
	}

	[EnemyAction(displayName = "Tuan/ActiveCallButterFly")]
	public void ActiveCallButterFly(bool isActive)
	{
		this.isCallButterFly = isActive;
	}

	[EnemyAction(displayName = "Tuan/Skill0_CocoonFalling")]
	public void Skill0_CocoonFalling(float timeDelay)
	{
		this.timeDelay_Falling = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill1_ComboRocket")]
	public void Skill1_ComboRocket(float timeDelay, int numberBullet, float speedBullet)
	{
		this.timeDelay_ComboRocket = timeDelay;
		this.numberBullet_ComboRocket = numberBullet;
		this.speedBullet_ComboRocket = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill2_ShotByStomach")]
	public void Skill2_ShotByStomach(float timeDelay, int numberBullet, float speedBullet)
	{
		this.timeDelay_ShotByStomach = timeDelay;
		this.ubh_SpriralShot.m_bulletNum = numberBullet;
		this.ubh_SpriralShot.m_bulletSpeed = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill3_DestroyCocoon")]
	public void Skill3_DestroyCocoon(float timeDelay, float speedBullet)
	{
		this.timeDelay_DestroyCocoon = timeDelay;
		this.ubh_HoleCircleShot[0].m_bulletSpeed = speedBullet;
		this.ubh_HoleCircleShot[1].m_bulletSpeed = speedBullet;
		this.ubh_HoleCircleShot[2].m_bulletSpeed = speedBullet;
		this.ubh_HoleCircleShot[3].m_bulletSpeed = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/ShowProtect")]
	public void ShowProtect()
	{
		this.protect.SetActive(true);
		base.HideHealthBar();
	}

	public override void Restart()
	{
		base.Restart();
		if (this.BaseStatBoss != null)
		{
			this.BaseStatBoss();
		}
		base.ShowHealthBar();
		base.SetNameBoss("Monster Cocoon");
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
		{
			base.StartCoroutine(this.AttackFalling());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha2))
		{
			base.StartCoroutine(this.AttackComboRocket());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha3))
		{
			base.StartCoroutine(this.AttackShotByStomach());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha4))
		{
			base.StartCoroutine(this.AttackDestroyCocoon());
		}
		this.Move();
	}

	private void OnEnable()
	{
		this.isMoveStart = true;
		EazySoundManager.PlaySound(this.listSound[0], 0.3f);
	}

	private void RandomListID()
	{
		this.listID.Clear();
		while (this.listID.Count < 3)
		{
			int item = UnityEngine.Random.Range(0, 3);
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
		int num = this.listID[this.currentTurnSkill];
		if (num != 0)
		{
			if (num != 1)
			{
				if (num == 2)
				{
					base.StartCoroutine(this.AttackShotByStomach());
				}
			}
			else
			{
				base.StartCoroutine(this.AttackComboRocket());
			}
		}
		else
		{
			base.StartCoroutine(this.AttackFalling());
		}
	}

	private IEnumerator ChangeAnim(string animStart, string animEnd)
	{
		this.skeletonAnimation.AnimationState.SetAnimation(0, animStart, false).Complete += delegate(TrackEntry A_1)
		{
			this.skeletonAnimation.AnimationState.SetAnimation(0, animEnd, true);
			if (this.isActiveAdult && !this.isFlyOut)
			{
				this.isFlyOut = true;
				this.StartCoroutine(this.AttackDestroyCocoon());
			}
			else if (animEnd != this.animIdleAdult)
			{
				this.RandomSkill();
			}
		};
		yield return null;
		yield break;
	}

	private IEnumerator AttackFalling()
	{
		yield return new WaitForSeconds(this.timeDelay_Falling - 1f);
		this.warning_Falling[0].SetActive(true);
		this.warning_Falling[1].SetActive(true);
		this.warning_Falling[2].SetActive(true);
		yield return new WaitForSeconds(1f);
		base.StartCoroutine(this.ChangeAnim(this.animFalling, this.animIdleCocoon));
		for (int i = 0; i < 3; i++)
		{
			yield return new WaitForSeconds(0.5f);
			EazySoundManager.PlaySound(this.listSound[3], 0.8f);
			yield return new WaitForSeconds(0.5f);
			EazySoundManager.PlaySound(this.listSound[4], 0.8f);
			Fu.SpawnExplosion(this.bulletAura, this.posFalling.position, Quaternion.identity);
			this.warning_Falling[i].SetActive(false);
		}
		yield break;
	}

	private IEnumerator AttackComboRocket()
	{
		yield return new WaitForSeconds(this.timeDelay_ComboRocket);
		base.StartCoroutine(this.ChangeAnim(this.animComboRocket, this.animIdleCocoon));
		yield return new WaitForSeconds(1f);
		this.warning_ComboRocket.SetActive(true);
		yield return new WaitForSeconds(0.3f);
		for (int i = 0; i < this.numberBullet_ComboRocket; i += 2)
		{
			this.bfp_ComboRocket.Attack(i, 0, this.speedBullet_ComboRocket, null, false);
			this.bfp_ComboRocket.Attack(i + 1, 0, this.speedBullet_ComboRocket, null, false);
			EazySoundManager.PlaySound(this.listSound[5], 1f);
			yield return new WaitForSeconds(2.8f / (float)this.numberBullet_ComboRocket);
		}
		yield return new WaitForSeconds(1f);
		this.warning_ComboRocket.SetActive(false);
		yield break;
	}

	private IEnumerator AttackShotByStomach()
	{
		yield return new WaitForSeconds(this.timeDelay_ShotByStomach);
		base.StartCoroutine(this.ChangeAnim(this.animShotByStomach, this.animIdleCocoon));
		yield return new WaitForSeconds(1f);
		this.ubh_ShotByStomach.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[6], 0.8f);
		yield return new WaitForSeconds(1f);
		EazySoundManager.PlaySound(this.listSound[7], 0.8f);
		yield return new WaitForSeconds(2.4f);
		EazySoundManager.PlaySound(this.listSound[8], 0.8f);
		yield break;
	}

	private IEnumerator AttackDestroyCocoon()
	{
		yield return new WaitForSeconds(this.timeDelay_DestroyCocoon);
		base.StartCoroutine(this.ChangeAnim(this.animDestroyCocoon, this.animIdleAdult));
		yield return new WaitForSeconds(0.2f);
		this.ubh_DestroyCocoon.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[7], 1f);
		yield return new WaitForSeconds(0.67f);
		EazySoundManager.PlaySound(this.listSound[7], 1f);
		yield return new WaitForSeconds(0.67f);
		EazySoundManager.PlaySound(this.listSound[7], 1f);
		yield return new WaitForSeconds(0.67f);
		this.protect.SetActive(false);
		EazySoundManager.PlaySound(this.listSound[9], 1f);
		yield return new WaitForSeconds(1f);
		this.isMoveEnd = true;
		yield break;
	}

	private void Move()
	{
		if (this.isMoveStart)
		{
			base.gameObject.transform.position = Vector3.Lerp(base.transform.position, this.posStart, 0.01f);
			if (base.gameObject.transform.position.y <= this.posStart.y + 0.5f)
			{
				this.isMoveStart = false;
			}
		}
		if (this.isMoveEnd)
		{
			base.gameObject.transform.position = Vector3.Lerp(base.transform.position, this.posEnd, 0.01f);
			if (base.gameObject.transform.position.y >= this.posEnd.y - 5f)
			{
				this.isMoveEnd = false;
				if (!this.isCallButterFly)
				{
					this.Die(EnemyKilledBy.Player);
				}
				else if (this.CallButterflyAdult != null)
				{
					this.CallButterflyAdult();
				}
			}
		}
	}

	private int GetRandomValue(params BossMode_BuomTim_Cocoon.RandomSelection[] selections)
	{
		float value = UnityEngine.Random.value;
		float num = 0f;
		foreach (BossMode_BuomTim_Cocoon.RandomSelection randomSelection in selections)
		{
			num += randomSelection.probability;
			if (value <= num)
			{
				return randomSelection.GetValue();
			}
		}
		return -1;
	}

	private bool isActiveAdult;

	private bool isFlyOut;

	[SerializeField]
	private bool isCallButterFly;

	[SerializeField]
	private AudioClip[] listSound;

	[SerializeField]
	private SkeletonAnimation skeletonAnimation;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animIdleCocoon;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animFalling;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animComboRocket;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByStomach;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animDestroyCocoon;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animIdleAdult;

	private float timeDelay_Falling = 2f;

	[SerializeField]
	private Transform posFalling;

	[SerializeField]
	private GameObject bulletAura;

	[SerializeField]
	private GameObject[] warning_Falling;

	private float timeDelay_ComboRocket = 2f;

	private int numberBullet_ComboRocket = 8;

	private float speedBullet_ComboRocket = 5f;

	[SerializeField]
	private BulletFollowPath bfp_ComboRocket;

	[SerializeField]
	private GameObject warning_ComboRocket;

	private float timeDelay_ShotByStomach = 2f;

	[SerializeField]
	private UbhShotCtrl ubh_ShotByStomach;

	[SerializeField]
	private UbhSpiralShot ubh_SpriralShot;

	private float timeDelay_DestroyCocoon = 2f;

	[SerializeField]
	private UbhShotCtrl ubh_DestroyCocoon;

	[SerializeField]
	private UbhHoleCircleShot[] ubh_HoleCircleShot;

	[SerializeField]
	private GameObject protect;

	[EnemyEventCustom(displayName = "BossTuan/BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

	[EnemyEventCustom(displayName = "BossTuan/CallButterflyAdult")]
	public EnemyEvent CallButterflyAdult;

	public List<int> listID = new List<int>();

	private int currentTurnSkill;

	private int lastIdSkill = -1;

	public bool isMoveStart;

	public bool isMoveEnd;

	private Vector3 posStart = new Vector3(0f, SgkCamera.topRight.y - 4.75f, 0f);

	private Vector3 posEnd = new Vector3(0f, SgkCamera.topRight.y + 15f, 0f);

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
