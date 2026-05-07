using System;
using System.Collections;
using Hellmade.Sound;
using SkyGameKit;
using UnityEngine;

public class BossMode_BiNgo : NewBoss
{
	[EnemyAction(displayName = "Tuan/StartAttack")]
	public void StartAttack()
	{
		base.StartCoroutine(this.AttackMouth());
	}

	[EnemyAction(displayName = "Tuan/PathSpawnEnemy")]
	public void PathSpawnEnemy(SpawnByBoss spTurn)
	{
		spTurn.boss = base.transform;
		spTurn.StartWithDelayAndDisplay();
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry")]
	public void ActiveStatusAngry()
	{
		this.isActiveAngry = true;
		this.effectFire.SetActive(true);
		base.ShowRedScreen();
	}

	[EnemyAction(displayName = "Tuan/Skill0_AttackMouth")]
	public void Skill0_AttackMouth(float timeDelay, int numberBullet, float speedBullet, int numberWay)
	{
		this.timeDelay_AttackMouth = timeDelay;
		this.ubhSin.m_bulletNum = numberBullet;
		this.ubhSin.m_bulletSpeed = speedBullet;
		this.ubhSin.m_wayNum = numberWay;
	}

	[EnemyAction(displayName = "Tuan/Skill1_Tomahawk")]
	public void Skill1_Tomahawk(float timeDelay, int numberBullet, int timeFollow, float speedBullet)
	{
		this.timeDelay_Tomahawk = timeDelay;
		this.numberBullet_Tomahawk = numberBullet;
		this.timeFollow_Tomahawk = (float)timeFollow;
		this.speedBullet_Tomahawk = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill2_StatAttackHand")]
	public void Skill2_StatAttackHand(float timeDelay, int numberBullet, float speedBullet)
	{
		this.timeDelay_AttackHand = timeDelay;
		this.numberBullet_AttackHand = numberBullet;
		this.speed_AttackHand = speedBullet;
	}

	[EnemyAction(displayName = "Tuan/Skill3_AttackLaze")]
	public void Skill3_AttackLaze(float timeDelay, float timeAttack)
	{
		this.timeDelay_AttackLaser = timeDelay;
		this.time_AttackLaser = timeAttack;
	}

	public override void Restart()
	{
		base.Restart();
		if (this.BaseStatBoss != null)
		{
			this.BaseStatBoss();
		}
		base.ShowHealthBar();
		base.SetNameBoss("Ghost Pumpkin");
	}

	private void OnEnable()
	{
		this.FindRandomTarget();
		EazySoundManager.PlaySound(this.listSound[0], 0.7f);
	}

	private void Update()
	{
		if (this.isFlyDown)
		{
			this.FlyDown();
		}
		else if (this.isFlyUp)
		{
			this.FlyUp();
		}
	}

	private void FlyDown()
	{
		base.gameObject.transform.Translate(Vector3.down * Time.deltaTime * this.speedMove * 3f);
		if (base.gameObject.transform.position.y <= this.y && this.isFlyDown)
		{
			this.isFlyDown = false;
			base.StartCoroutine(this.AttackLaze());
			EazySoundManager.PlaySound(this.listSound[11], 1f);
		}
	}

	private void FlyUp()
	{
		base.gameObject.transform.Translate(Vector3.up * Time.deltaTime * this.speedMove);
		if (base.gameObject.transform.position.y >= 4.4f && this.isFlyUp)
		{
			this.isFlyUp = false;
			base.StartCoroutine(this.AttackMouth());
		}
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

	private IEnumerator TranformStatus(int id)
	{
		if (id == 0)
		{
			this.anim.SetTrigger("transform");
			yield return new WaitForSeconds(1.6f);
		}
		else if (id == 1)
		{
			this.anim.SetTrigger("transform2");
			yield return new WaitForSeconds(0.8f);
		}
		yield break;
	}

	private IEnumerator AttackMouth()
	{
		yield return new WaitForSeconds(this.timeDelay_AttackMouth);
		this.anim.SetTrigger("attackmouth");
		this.objAttackMouth.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[4], 1f);
		yield return new WaitForSeconds(2f);
		this.anim.SetTrigger("idlehappy");
		if (this.isActiveAngry)
		{
			base.StartCoroutine(this.AttackTomahawk());
		}
		else
		{
			base.StartCoroutine(this.AttackHand());
		}
		yield break;
	}

	private IEnumerator AttackHand()
	{
		yield return new WaitForSeconds(this.timeDelay_AttackHand);
		for (int i = 0; i < this.numberBullet_AttackHand; i++)
		{
			yield return new WaitForSeconds(0.8f);
			this.anim.SetTrigger("attack");
			yield return new WaitForSeconds(0.8f);
			EazySoundManager.PlaySound(this.listSound[8], 1f);
			this.bfp_AttackHand.Attack(0, 0, this.speed_AttackHand, this.player.transform, false);
			this.bfp_AttackHand.Attack(1, 0, this.speed_AttackHand, this.player.transform, false);
		}
		yield return new WaitForSeconds(2f);
		this.smoothFollow.isSmoothFollow = false;
		this.isFlyDown = true;
		yield break;
	}

	private IEnumerator AttackLaze()
	{
		CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
		base.StartCoroutine(this.TranformStatus(0));
		yield return new WaitForSeconds(1.6f);
		this.effectFire.SetActive(false);
		this.fx_AttackLaser.SetActive(true);
		if (!this.isActiveAngry)
		{
			this.warning_AttackLaser[0].SetActive(true);
		}
		else
		{
			this.warning_AttackLaser[0].SetActive(true);
			this.warning_AttackLaser[1].SetActive(true);
			this.warning_AttackLaser[2].SetActive(true);
		}
		EazySoundManager.PlaySound(this.listSound[9], 1f);
		yield return new WaitForSeconds(2.3f);
		this.ubhLaser.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[10], 0.3f);
		this.fx_AttackLaser.SetActive(false);
		if (!this.isActiveAngry)
		{
			this.warning_AttackLaser[0].SetActive(false);
		}
		else
		{
			this.warning_AttackLaser[0].SetActive(false);
			this.warning_AttackLaser[1].SetActive(false);
			this.warning_AttackLaser[2].SetActive(false);
		}
		if (!this.isActiveAngry)
		{
			this.objAttackLaser[0].SetActive(true);
		}
		else
		{
			this.objAttackLaser[0].SetActive(true);
			this.objAttackLaser[1].SetActive(true);
			this.objAttackLaser[2].SetActive(true);
		}
		CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
		yield return new WaitForSeconds(this.time_AttackLaser);
		if (!this.isActiveAngry)
		{
			this.objAttackLaser[0].SetActive(false);
		}
		else
		{
			this.objAttackLaser[0].SetActive(false);
			this.objAttackLaser[1].SetActive(false);
			this.objAttackLaser[2].SetActive(false);
		}
		if (this.isActiveAngry)
		{
			this.effectFire.SetActive(true);
		}
		base.StartCoroutine(this.TranformStatus(1));
		yield return new WaitForSeconds(1f);
		this.smoothFollow.isSmoothFollow = true;
		this.isFlyUp = true;
		yield break;
	}

	private IEnumerator AttackTomahawk()
	{
		yield return new WaitForSeconds(this.timeDelay_Tomahawk);
		base.StartCoroutine(this.TranformStatus(0));
		this.fx_AttackLaser.SetActive(true);
		EazySoundManager.PlaySound(this.listSound[5], 1f);
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < this.numberBullet_Tomahawk; i++)
		{
			EazySoundManager.PlaySound(this.listSound[3], 1f);
			UbhBullet objBullett = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.prefabsBullet, new Vector3(base.gameObject.transform.position.x, base.gameObject.transform.position.y + 1.5f, base.gameObject.transform.position.z), false);
			UbhBullet objBullett2 = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.prefabsBullet, new Vector3(base.gameObject.transform.position.x - 1.5f, base.gameObject.transform.position.y + 1f, base.gameObject.transform.position.z), false);
			UbhBullet objBullett3 = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.prefabsBullet, new Vector3(base.gameObject.transform.position.x + 1.5f, base.gameObject.transform.position.y + 1f, base.gameObject.transform.position.z), false);
			NewBoss_BulletHomming homingMissile = objBullett.GetComponent<NewBoss_BulletHomming>();
			NewBoss_BulletHomming homingMissile2 = objBullett2.GetComponent<NewBoss_BulletHomming>();
			NewBoss_BulletHomming homingMissile3 = objBullett3.GetComponent<NewBoss_BulletHomming>();
			homingMissile.SetFollowTargetX(this.timeFollow_Tomahawk);
			homingMissile2.SetFollowTargetX(this.timeFollow_Tomahawk);
			homingMissile3.SetFollowTargetX(this.timeFollow_Tomahawk);
			homingMissile.speedMove = this.speedBullet_Tomahawk;
			homingMissile2.speedMove = this.speedBullet_Tomahawk;
			homingMissile3.speedMove = this.speedBullet_Tomahawk;
			yield return new WaitForSeconds(0.3f);
		}
		yield return new WaitForSeconds((float)this.numberBullet_Tomahawk);
		base.StartCoroutine(this.TranformStatus(1));
		this.fx_AttackLaser.SetActive(false);
		base.StartCoroutine(this.AttackHand());
		yield break;
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		EazySoundManager.PlaySound(this.listSound[2], 1f);
	}

	private bool isFlyUp;

	private bool isFlyDown;

	private float speedMove = 2f;

	private bool isActiveAngry;

	[SerializeField]
	private Animator anim;

	[SerializeField]
	private SmoothFollow smoothFollow;

	[SerializeField]
	private GameObject effectFire;

	[SerializeField]
	private AudioClip[] listSound;

	private float timeDelay_AttackMouth;

	[SerializeField]
	private UbhShotCtrl objAttackMouth;

	[SerializeField]
	private UbhSinWaveBulletNwayShot ubhSin;

	private float timeDelay_AttackHand;

	private int numberBullet_AttackHand = 1;

	private float speed_AttackHand = 6f;

	[SerializeField]
	private BulletFollowPath bfp_AttackHand;

	private float timeDelay_Tomahawk;

	private int numberBullet_Tomahawk;

	private float timeFollow_Tomahawk;

	private float speedBullet_Tomahawk;

	[SerializeField]
	private GameObject prefabsBullet;

	private float timeDelay_AttackLaser;

	private float time_AttackLaser;

	[SerializeField]
	private GameObject fx_AttackLaser;

	[SerializeField]
	private GameObject[] warning_AttackLaser;

	[SerializeField]
	private GameObject[] objAttackLaser;

	[SerializeField]
	private UbhShotCtrl ubhLaser;

	[EnemyEventCustom(displayName = "BossTuan/BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

	[EnemyEventCustom(displayName = "BossTuan/CreatePathSpawnEnemy")]
	public EnemyEvent CreatePathSpawnEnemy;

	private float y = SgkCamera.bottomLeft.y + 2f;

	private float rangeAttack = 30f;

	public LayerMask layerPlayer;

	public Transform player;
}
