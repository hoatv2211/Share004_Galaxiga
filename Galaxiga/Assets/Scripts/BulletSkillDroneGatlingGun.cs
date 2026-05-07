using System;
using SkyGameKit;
using UnityEngine;

public class BulletSkillDroneGatlingGun : Bullet
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
		this.power = (float)this.droneData.CurrentSuperDamage;
	}

	public override void Update()
	{
		base.transform.Translate(Vector2.up * Time.deltaTime * this.speedMove);
	}

	public override void OnTriggerEnter2D(Collider2D c)
	{
		if (c.CompareTag("Enemy") && !this.isTriggerEnm)
		{
			this.isTriggerEnm = true;
			this.TakeDamageEnemy(c.gameObject);
		}
	}

	public override void TakeDamageEnemy(GameObject objEnemy)
	{
		EnemyHit component = objEnemy.GetComponent<EnemyHit>();
		if (component != null)
		{
			this.SpawFxBulletTriggerEnemy();
			component.m_BaseEnemy.CurrentHP -= (int)this.power;
			this.DespawnBullet();
		}
	}

	private void SpawFxBulletTriggerEnemy()
	{
		Fu.SpawnExplosion(this.fxBulletTriggerEnemy, base.transform.position, Quaternion.identity);
	}

	public override void DespawnBullet()
	{
		this.isTriggerEnm = false;
		if (base.gameObject.activeInHierarchy)
		{
			GameUtil.ObjectPoolDespawn("BulletDrone", base.gameObject);
		}
	}

	public override void OnBecameInvisible()
	{
		if (this.isVisible)
		{
			this.DespawnBullet();
			this.isVisible = false;
		}
	}

	private void OnBecameVisible()
	{
		if (!this.isVisible)
		{
			this.isVisible = true;
		}
	}

	public DroneData droneData;

	public GameObject fxBulletTriggerEnemy;

	[HideInInspector]
	public float speedMove;

	private bool isTriggerEnm;

	private bool isVisible;
}
