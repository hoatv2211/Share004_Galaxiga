using System;
using SkyGameKit;
using UnityEngine;

public class BulletDroneTakeDamage : Bullet
{
	private void Awake()
	{
	}

	public override void Start()
	{
		if (this.needSetPower)
		{
			this.SetPower();
		}
	}

	public override void SetPower()
	{
		if (!this.isBulletSkill)
		{
			this.power = (float)this.droneData.CurrentDamage;
		}
		else
		{
			this.power = (float)this.droneData.CurrentSuperDamage;
		}
	}

	public override void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D(collision);
	}

	public override void TakeDamage(GameObject obj)
	{
		EnemyHit component = obj.GetComponent<EnemyHit>();
		if (component != null)
		{
			this.SpawFxExplosion();
			component.m_BaseEnemy.CurrentHP -= (int)this.power;
		}
	}

	public override void DespawnBullet()
	{
		if (base.gameObject.activeInHierarchy)
		{
			GameUtil.ObjectPoolDespawn("BulletDrone", base.gameObject);
		}
		base.DespawnBullet();
	}

	public override void SpawFxExplosion()
	{
		Fu.SpawnExplosion(this.fxBulletTriggerEnemy, base.transform.position, Quaternion.identity);
	}

	public override void OnBecameInvisible()
	{
		base.OnBecameInvisible();
	}

	public DroneData droneData;

	public bool isBulletSkill;

	public GameObject fxBulletTriggerEnemy;
}
