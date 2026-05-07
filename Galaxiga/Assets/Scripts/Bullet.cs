using System;
using SkyGameKit;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public virtual void Start()
	{
	}

	private void OnEnable()
	{
	}

	public virtual void SetPower()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (collision.CompareTag("DeadZone") || collision.CompareTag("DestroyBulletPlayer"))
		{
			this.DespawnBullet();
		}
		else if (collision.CompareTag("Enemy"))
		{
			if (!this.isTriggerEnemy)
			{
				this.isTriggerEnemy = true;
				this.TakeDamageEnemy(collision.gameObject);
			}
		}
		else if (collision.CompareTag("Wall"))
		{
			if (base.GetComponent<SgkBullet>() != null)
			{
				base.GetComponent<SgkBullet>().Explosion();
			}
			else
			{
				this.SpawFxExplosion();
				this.DespawnBullet();
			}
		}
	}

	public virtual void TakeDamageEnemy(GameObject objEnemy)
	{
		this.TakeDamage(objEnemy);
		this.DespawnBullet();
	}

	public virtual void TakeDamage(GameObject obj)
	{
		this.SpawFxExplosion();
	}

	public virtual void DespawnBullet()
	{
		this.isTriggerEnemy = false;
	}

	public virtual void SpawFxExplosion()
	{
	}

	public virtual void OnBecameInvisible()
	{
		if (GameContext.isMissionComplete || GameContext.isMissionFail)
		{
			return;
		}
		this.DespawnBullet();
	}

	public float power;

	public bool needSetPower = true;

	private bool isTriggerEnemy;
}
