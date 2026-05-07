using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using SkyGameKit;
using Spine;
using Spine.Unity;
using UnityEngine;

public class BossMode_BuomTim_Adult : NewBoss
{
	[EnemyAction(displayName = "Tuan/StartAttack")]
	public void StartAttack()
	{
		this.FindRandomTarget();
		this.RandomSkill();
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry")]
	public void ActiveStatusAngry()
	{
		base.ShowRedScreen();
		EazySoundManager.PlaySound(this.listSound[1], 1f);
	}

	[EnemyAction(displayName = "Tuan/Skill4_AreaBomb")]
	public void Skill4_AreaBomb(float timeDelay, int numberBullet)
	{
		this.timeDelay_AreaBomb = timeDelay;
		this.numberBullet_AreaBomb = numberBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill5_ShotByGuns")]
	public void Skill5_ShotByGuns(float timeDelay, int speedBullet)
	{
		this.timeDelay_ShotByGuns = timeDelay;
		this.ubhNwayShots[0].m_bulletSpeed = (float)speedBullet;
		this.ubhNwayShots[1].m_bulletSpeed = (float)speedBullet;
		this.ubhSpiralShots[0].m_bulletSpeed = (float)speedBullet;
		this.ubhSpiralShots[1].m_bulletSpeed = (float)speedBullet;
		this.ubhSpiralShots[2].m_bulletSpeed = (float)speedBullet;
		this.ubhSpiralShots[3].m_bulletSpeed = (float)speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill6_LaserGuns")]
	public void Skill6_LaserGuns(float timeDelay)
	{
		this.timeDelay_animLaserGuns = timeDelay;
	}

	[EnemyAction(displayName = "Tuan/Skill7_SpawnCocoon")]
	public void Skill7_SpawnCocoon(float timeDelay, int numberBullet)
	{
		this.timeDelay_SpawnCocoon = timeDelay;
		this.numberBullet_SpawnCocoon = numberBullet;
	}

	public override void Restart()
	{
		base.Restart();
		if (this.BaseStatBoss != null)
		{
			this.BaseStatBoss();
		}
		base.ShowHealthBar();
		base.SetNameBoss("Queen Butterfly");
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
		{
			base.StartCoroutine(this.AttackAreaBomb());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha2))
		{
			base.StartCoroutine(this.AttackShotByGuns());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha3))
		{
			base.StartCoroutine(this.AttackanimLaserGuns());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha4))
		{
			base.StartCoroutine(this.AttackSpawnCocoon());
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
		while (this.listID.Count < 4)
		{
			int item = UnityEngine.Random.Range(4, 8);
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
		case 4:
			base.StartCoroutine(this.AttackAreaBomb());
			break;
		case 5:
			base.StartCoroutine(this.AttackShotByGuns());
			break;
		case 6:
			base.StartCoroutine(this.AttackanimLaserGuns());
			break;
		case 7:
			base.StartCoroutine(this.AttackSpawnCocoon());
			break;
		}
	}

	private IEnumerator ChangeAnim(string anim)
	{
		this.skeletonAnimation.AnimationState.SetAnimation(0, anim, false).Complete += delegate(TrackEntry A_1)
		{
			this.skeletonAnimation.AnimationState.SetAnimation(0, this.animIdleAdult, true);
			this.RandomSkill();
			if (anim == this.animInvi)
			{
				this.polygonCollider2D.enabled = true;
			}
		};
		yield return null;
		yield break;
	}

	private IEnumerator AttackAreaBomb()
	{
		yield return new WaitForSeconds(this.timeDelay_AreaBomb);
		base.StartCoroutine(this.ChangeAnim(this.animInvi));
		EazySoundManager.PlaySound(this.listSound[3], 1f);
		this.warning_AreaBomb.SetActive(true);
		yield return new WaitForSeconds(1f);
		this.warning_AreaBomb.SetActive(false);
		this.polygonCollider2D.enabled = false;
		Vector2 pos = default(Vector2);
		for (int i = 0; i < this.numberBullet_AreaBomb; i++)
		{
			float[] x = new float[]
			{
				-3f,
				-2f,
				-1f,
				0f,
				1f,
				2f,
				3f
			};
			float[] y = new float[]
			{
				-5f,
				-4f,
				-3f,
				-2f,
				-1f,
				0f,
				1f,
				2f,
				3f,
				4f,
				5f
			};
			pos.x = x[UnityEngine.Random.Range(0, 7)];
			pos.y = y[UnityEngine.Random.Range(0, 11)];
			Fu.SpawnExplosion(this.bulletAura, new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
			EazySoundManager.PlaySound(this.listSound[4], 1f);
			yield return new WaitForSeconds(0.3f);
		}
		yield break;
	}

	private IEnumerator AttackShotByGuns()
	{
		yield return new WaitForSeconds(this.timeDelay_ShotByGuns);
		base.StartCoroutine(this.ChangeAnim(this.animShotByGuns));
		this.ubh_ShotByGuns[0].StartShotRoutine();
		this.ubh_ShotByGuns[1].StartShotRoutine();
		for (int i = 0; i < 18; i++)
		{
			EazySoundManager.PlaySound(this.listSound[5], 1f);
			yield return new WaitForSeconds(0.1f);
		}
		yield break;
	}

	private IEnumerator AttackanimLaserGuns()
	{
		yield return new WaitForSeconds(this.timeDelay_animLaserGuns);
		base.StartCoroutine(this.ChangeAnim(this.animanimLaserGuns));
		EazySoundManager.PlaySound(this.listSound[7], 1f);
		yield return new WaitForSeconds(1.3f);
		this.lazeLeft.SetActive(true);
		this.lazeRight.SetActive(true);
		EazySoundManager.PlaySound(this.listSound[8], 1f);
		yield return new WaitForSeconds(1f);
		EazySoundManager.PlaySound(this.listSound[8], 1f);
		yield return new WaitForSeconds(1f);
		EazySoundManager.PlaySound(this.listSound[8], 1f);
		yield return new WaitForSeconds(1f);
		this.lazeLeft.SetActive(false);
		this.lazeRight.SetActive(false);
		yield break;
	}

	private IEnumerator AttackSpawnCocoon()
	{
		yield return new WaitForSeconds(this.timeDelay_SpawnCocoon);
		for (int i = 0; i < this.numberBullet_SpawnCocoon; i++)
		{
			base.StartCoroutine(this.ChangeAnim(this.animSpawnCocoon));
			this.warning_SpawnCocoon.SetActive(true);
			yield return new WaitForSeconds(2f);
			this.warning_SpawnCocoon.SetActive(false);
			this.bfp_SpawnCocoon.Attack(0, 0, 5f, null, false);
			EazySoundManager.PlaySound(this.listSound[9], 0.8f);
		}
		yield break;
	}

	private int GetRandomValue(params BossMode_BuomTim_Adult.RandomSelection[] selections)
	{
		float value = UnityEngine.Random.value;
		float num = 0f;
		foreach (BossMode_BuomTim_Adult.RandomSelection randomSelection in selections)
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
	private string animIdleAdult;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animInvi;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByGuns;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animanimLaserGuns;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animSpawnCocoon;

	private float timeDelay_AreaBomb = 1f;

	private int numberBullet_AreaBomb = 7;

	[SerializeField]
	private GameObject bulletAura;

	[SerializeField]
	private GameObject warning_AreaBomb;

	private float timeDelay_ShotByGuns = 1f;

	[SerializeField]
	private UbhShotCtrl[] ubh_ShotByGuns;

	[SerializeField]
	private UbhNwayShot[] ubhNwayShots;

	[SerializeField]
	private UbhSpiralShot[] ubhSpiralShots;

	private float timeDelay_animLaserGuns = 1f;

	public GameObject lazeLeft;

	public GameObject lazeRight;

	private float timeDelay_SpawnCocoon = 1f;

	private int numberBullet_SpawnCocoon = 3;

	[SerializeField]
	private BulletFollowPath bfp_SpawnCocoon;

	[SerializeField]
	private GameObject warning_SpawnCocoon;

	[EnemyEventCustom(displayName = "BossTuan/BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

	private float rangeAttack = 30f;

	public LayerMask layerPlayer;

	public Transform player;

	public List<int> listID = new List<int>();

	private int currentTurnSkill;

	private int lastIdSkill = -1;

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
