using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using SkyGameKit;
using TwoDLaserPack;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
	public List<BaseEnemy> listAliveEnemy
	{
		get
		{
			return SgkSingleton<LevelManager>.Instance.AliveEnemy;
		}
	}

	private void Awake()
	{
		this.anim = base.GetComponent<Animator>();
		base.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
	}

	private void Start()
	{
		if (this.turretType == Turret.TurretType.Laser)
		{
			int num = PlaneIngameManager.current.currentPlayerController.power;
			this.power = (int)((double)(num * num) * 0.7 / 10.0);
			this.basedLaser.OnLaserHitTriggered += this.BasedLaser_OnLaserHitTriggered;
			this.basedLaser.collisionTriggerInterval = this.fireRate;
		}
		else if (this.turretType == Turret.TurretType.HomingMissile)
		{
			this.ApplyConfig();
			this.homingShotCtr.m_shotRoutineFinishedCallbackEvents.AddListener(new UnityAction(this.Test));
		}
	}

	private void Test()
	{
		UnityEngine.Debug.Log("Fire");
	}

	private void BasedLaser_OnLaserHitTriggered(RaycastHit2D hitInfo)
	{
		if (hitInfo.collider.gameObject.CompareTag("Enemy"))
		{
			GameObject gameObject = hitInfo.collider.gameObject;
			EnemyHit component = gameObject.GetComponent<EnemyHit>();
			if (component != null)
			{
				this.SpawFxBulletTriggerEnemy(hitInfo.point);
				component.m_BaseEnemy.CurrentHP -= this.power;
			}
		}
	}

	private void Update()
	{
		if (this.closeEnemy != null && this.closeEnemy.activeInHierarchy)
		{
			base.transform.LookAt2D(this.closeEnemy.transform, Time.deltaTime * 60f);
			if (this.turretType == Turret.TurretType.Bullet && this.linearFire && Time.time > this.nextTime)
			{
				this.nextTime = Time.time + 0.2f;
				this.SpawLinearBullet();
			}
		}
		else
		{
			this.FindTheTarget();
		}
	}

	private void LookAtTarget()
	{
		Vector3 forward = this.closeEnemy.transform.position - base.transform.position;
		Quaternion b = Quaternion.LookRotation(forward);
		b.x = 0f;
		b.y = 0f;
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Mathf.Clamp01(3f * Time.maximumDeltaTime));
	}

	private void FindTheTarget()
	{
		BaseEnemy baseEnemy = (from x in this.listAliveEnemy
		where !Fu.Outside(x.transform.position, CameraManager.bottomLeft, CameraManager.topRight)
		orderby Vector3.Distance(x.transform.position, base.transform.position)
		select x).FirstOrDefault<BaseEnemy>();
		if (baseEnemy != null)
		{
			this.closeEnemy = baseEnemy.gameObject;
			this.StartFire();
		}
		else
		{
			this.StopFire();
		}
	}

	private Transform GetClosestEnemy(Collider2D[] enemies, Transform fromThis)
	{
		Transform result = null;
		float num = float.PositiveInfinity;
		Vector3 position = fromThis.position;
		foreach (Collider2D collider2D in enemies)
		{
			Transform transform = collider2D.gameObject.transform;
			float sqrMagnitude = (transform.position - position).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				num = sqrMagnitude;
				result = transform;
			}
		}
		return result;
	}

	private void ApplyConfig()
	{
		this.homingShot.m_betweenDelay = this.fireRate;
		this.homingShot.m_bulletNum = this.numberBullet;
		this.homingShot.m_bulletSpeed = this.speedBullet;
		this.homingShotCtr.m_shotList[0].m_afterDelay = this.delayShot;
	}

	private void StartFire()
	{
		base.StartCoroutine(GameContext.Delay(2f, delegate
		{
			switch (this.turretType)
			{
			case Turret.TurretType.Bullet:
				this.linearFire = true;
				break;
			case Turret.TurretType.Laser:
				if (!this.laserIsActive)
				{
					this.LaserStartFire();
				}
				break;
			case Turret.TurretType.HomingMissile:
				this.homingShot.m_targetTransform = this.closeEnemy.transform;
				this.homingShotCtr.StartShotRoutine();
				break;
			}
		}));
	}

	private void StopFire()
	{
		Turret.TurretType turretType = this.turretType;
		if (turretType != Turret.TurretType.Bullet)
		{
			if (turretType != Turret.TurretType.HomingMissile)
			{
				if (turretType == Turret.TurretType.Laser)
				{
					this.LaserStopFire();
				}
			}
			else
			{
				this.HomingBulletStopFire();
			}
		}
		else
		{
			this.linearFire = false;
		}
	}

	public void LaserStartFire()
	{
		this.laserIsActive = true;
		base.StartCoroutine("LaserFireNow");
	}

	public void LaserStopFire()
	{
		this.laserIsActive = false;
		base.StopCoroutine("LaserFireNow");
		this.SetAnimIdle();
	}

	private IEnumerator LaserFireNow()
	{
		for (;;)
		{
			if (!this.laser.activeInHierarchy)
			{
				this.laser.SetActive(true);
			}
			this.SetAnimFire();
			this.ShowFxFire();
			this.basedLaser.waitingForTriggerTime = false;
			yield return new WaitForSeconds(this.timeLaserActive);
			this.SetAnimIdle();
			this.HideFxFire();
			if (this.laser.activeInHierarchy)
			{
				this.laser.SetActive(false);
			}
			yield return new WaitForSeconds(this.delayShot);
		}
		yield break;
	}

	private void HomingBulletStopFire()
	{
		this.SetAnimIdle();
		this.homingShotCtr.StopShotRoutine();
	}

	private void SpawFxBulletTriggerEnemy(Vector3 pos)
	{
		Fu.SpawnExplosion(this.fxTriggerLaser, pos, Quaternion.identity);
	}

	private void SetAnimFire()
	{
		this.anim.Play("attack");
	}

	private void SetAnimIdle()
	{
		this.anim.Play("idle");
	}

	public void ShowFxFire()
	{
		this.fxFire.SetActive(true);
	}

	public void HideFxFire()
	{
		this.fxFire.SetActive(false);
	}

	private void SpawLinearBullet()
	{
		this.ShowFxFire();
		GameUtil.ObjectPoolSpawn("turret", this.bullet.name, this.posSpawnBullet.position, this.posSpawnBullet.rotation);
	}

	public Turret.TurretType turretType;

	[HideIf("turretType", Turret.TurretType.Bullet, true)]
	[HideIf("turretType", Turret.TurretType.HomingMissile, true)]
	public LayerMask layerEnemy;

	[HideIf("turretType", Turret.TurretType.Bullet, true)]
	[HideIf("turretType", Turret.TurretType.HomingMissile, true)]
	public GameObject laser;

	[HideIf("turretType", Turret.TurretType.Bullet, true)]
	[HideIf("turretType", Turret.TurretType.HomingMissile, true)]
	public LineBasedLaser basedLaser;

	[HideIf("turretType", Turret.TurretType.Bullet, true)]
	[HideIf("turretType", Turret.TurretType.HomingMissile, true)]
	public GameObject fxTriggerLaser;

	[HideIf("turretType", Turret.TurretType.Laser, true)]
	[HideIf("turretType", Turret.TurretType.Bullet, true)]
	public UbhShotCtrl homingShotCtr;

	[HideIf("turretType", Turret.TurretType.Laser, true)]
	[HideIf("turretType", Turret.TurretType.Bullet, true)]
	public UbhHomingEnemyShot homingShot;

	[HideIf("turretType", Turret.TurretType.Laser, true)]
	public int numberBullet;

	public float fireRate;

	public float delayShot;

	[HideIf("turretType", Turret.TurretType.Laser, true)]
	public float speedBullet;

	[HideIf("turretType", Turret.TurretType.Bullet, true)]
	[HideIf("turretType", Turret.TurretType.HomingMissile, true)]
	public float timeLaserActive;

	public GameObject fxFire;

	[HideIf("turretType", Turret.TurretType.HomingMissile, true)]
	[HideIf("turretType", Turret.TurretType.Laser, true)]
	public GameObject bullet;

	[HideIf("turretType", Turret.TurretType.HomingMissile, true)]
	[HideIf("turretType", Turret.TurretType.Laser, true)]
	public Transform posSpawnBullet;

	private float rangeAttack = 15f;

	private GameObject closeEnemy;

	private int power;

	private Animator anim;

	private bool linearFire;

	private float nextTime;

	private bool laserIsActive;

	public enum TurretType
	{
		Bullet,
		Laser,
		HomingMissile
	}
}
