using System;
using SkyGameKit;
using UnityEngine;

public class BulletPlayerTakeDamage : Bullet
{
	private void Awake()
	{
	}

	public override void Start()
	{
		base.Start();
	}

	private void OnEnable()
	{
		if (this.savePowerBullet != GameContext.power)
		{
			this.savePowerBullet = GameContext.power;
			if (this.needSetPower)
			{
				this.SetPower();
			}
		}
	}

	public override void SetPower()
	{
		if (!this.isBulletSkill)
		{
			this.power = (float)this.planeData.CurrentSubPower;
		}
		else
		{
			this.power = (float)this.planeData.CurrentSuperDamage;
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
			component.m_BaseEnemy.CurrentHP -= (int)this.power;
			Fu.SpawnExplosion(this.fxBulletTriggerEnemy, base.transform.position, Quaternion.identity);
		}
	}

	public override void DespawnBullet()
	{
		if (base.gameObject.activeInHierarchy)
		{
			GameUtil.ObjectPoolDespawn("BulletPlayer", base.gameObject);
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

	public PlaneData planeData;

	public bool isBulletSkill;

	public GameObject fxBulletTriggerEnemy;

	private int savePowerBullet;
}
