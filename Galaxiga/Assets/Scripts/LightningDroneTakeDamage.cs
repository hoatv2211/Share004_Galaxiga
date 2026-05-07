using System;
using DigitalRuby.ThunderAndLightning;
using SkyGameKit;
using UnityEngine;

public class LightningDroneTakeDamage : LightningBoltTransformTrackerScript
{
	public void SetPower(int _power, float _fireRate)
	{
		this.power = _power;
		this.fireRate = _fireRate;
	}

	public override void TakeDamage()
	{
		if (this.baseEnemy.gameObject.activeInHierarchy)
		{
			if (Time.time > this.nextFire)
			{
				this.nextFire = Time.time + this.fireRate;
				this.baseEnemy.CurrentHP -= this.power;
				Fu.SpawnExplosion(this.fxTriggerBullet, this.baseEnemy.transform.position, Quaternion.identity);
			}
		}
		else
		{
			this.gunLightning.GetRandomTarget(this.indexLightning, this.baseEnemy);
		}
	}

	[Header("Lightning Properties")]
	public int indexLightning;

	public GunLightning gunLightning;

	[HideInInspector]
	public BaseEnemy baseEnemy;

	public int power;

	public float fireRate = 0.15f;

	public GameObject fxTriggerBullet;

	private float nextFire;
}
