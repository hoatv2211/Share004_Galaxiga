using System;
using SkyGameKit;
using UnityEngine;

public class BulletTurret : MonoBehaviour
{
	private void Awake()
	{
		if (this.turretType == BulletTurret.TurretType.HomingMissile)
		{
			this.ubhBullet = base.GetComponent<UbhBullet>();
			this.sgkBullet = base.GetComponent<SgkBullet>();
		}
	}

	private void Update()
	{
		if (this.turretType == BulletTurret.TurretType.Bullet)
		{
			base.transform.Translate(Vector2.up * Time.deltaTime * 10f);
		}
	}

	private void Start()
	{
		int num = PlaneIngameManager.current.currentPlayerController.power;
		BulletTurret.TurretType turretType = this.turretType;
		if (turretType != BulletTurret.TurretType.Bullet)
		{
			if (turretType == BulletTurret.TurretType.HomingMissile)
			{
				this.power = num * 4;
				this.sgkBullet.power = this.power;
			}
		}
		else
		{
			this.power = num * 2;
		}
	}

	private void OnBecameInvisible()
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (this.turretType == BulletTurret.TurretType.HomingMissile)
			{
				if (UbhSingletonMonoBehavior<UbhObjectPool>.instance != null)
				{
					UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this.ubhBullet, false);
				}
			}
			else if (base.gameObject.activeInHierarchy)
			{
				GameUtil.ObjectPoolDespawn("turret", base.gameObject);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (this.turretType == BulletTurret.TurretType.Bullet && collision.CompareTag("Enemy"))
		{
			EnemyHit component = collision.GetComponent<EnemyHit>();
			if (component != null)
			{
				component.m_BaseEnemy.CurrentHP -= this.power;
			}
			if (base.gameObject.activeInHierarchy)
			{
				GameUtil.ObjectPoolDespawn("turret", base.gameObject);
			}
			Fu.SpawnExplosion(this.fxTriggerBullet, base.transform.position, Quaternion.identity);
		}
	}

	public BulletTurret.TurretType turretType;

	private UbhBullet ubhBullet;

	private SgkBullet sgkBullet;

	public int power;

	public GameObject fxTriggerBullet;

	public enum TurretType
	{
		Bullet,
		HomingMissile
	}
}
