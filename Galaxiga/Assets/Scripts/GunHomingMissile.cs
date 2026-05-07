using System;
using System.Collections.Generic;
using System.Linq;
using SkyGameKit;
using UnityEngine;

public class GunHomingMissile : MonoBehaviour
{
	private List<BaseEnemy> ListEnemyAlive
	{
		get
		{
			return SgkSingleton<LevelManager>.Instance.AliveEnemy;
		}
	}

	private void Awake()
	{
		this.length = this.arrPosSpawnBullet.Length;
		this.lengthFX = this.arrFxSpawnBullet.Length;
	}

	private void Start()
	{
	}

	public void SetPower(float _fireRate, int _power)
	{
		this.fireRate = _fireRate;
		this.power = _power;
		this.baseFireRate = this.fireRate;
	}

	public void StartFire()
	{
		this.isStopFire = false;
		this.CheckPower();
	}

	public void StopFire()
	{
		this.isStopFire = true;
	}

	private void Update()
	{
		if (this.isStopFire)
		{
			return;
		}
		if (this.target == null || !this.target.activeInHierarchy)
		{
			this.SetTarget();
		}
		if (Time.time >= this.nextTime)
		{
			this.nextTime = Time.time + this.fireRate;
			this.Fire();
		}
	}

	private void SetTarget()
	{
		if (this.ListEnemyAlive.Count > 0)
		{
			BaseEnemy baseEnemy = this.GetTarget();
			if (baseEnemy != null)
			{
				this.target = baseEnemy.gameObject;
			}
		}
		else
		{
			this.target = null;
		}
	}

	private BaseEnemy GetTarget()
	{
		return (from x in this.ListEnemyAlive
		where !Fu.Outside(x.transform.position, CameraManager.bottomLeft, CameraManager.topRight)
		orderby Vector2.Distance(x.transform.position, this.transformPlayer.position)
		select x).FirstOrDefault<BaseEnemy>();
	}

	private void Fire()
	{
		for (int i = 0; i < this.length; i++)
		{
			if (i < this.lengthFX)
			{
				this.arrFxSpawnBullet[i].SetActive(true);
			}
			this.SpawnBullet(i);
		}
	}

	private void SpawnBullet(int indexBullet)
	{
		if (this.arrPosSpawnBullet[indexBullet].gameObject.activeInHierarchy)
		{
			GameObject gameObject = GameUtil.ObjectPoolSpawnGameObject("BulletPlayer", this.bullet.name, this.arrPosSpawnBullet[indexBullet].position, this.arrPosSpawnBullet[indexBullet].localRotation);
			HomingMissile component = gameObject.GetComponent<HomingMissile>();
			if (this.target != null && this.target.activeInHierarchy)
			{
				component.SetTarget(this.target.transform);
			}
			component.SetFollowTarget(0.005f);
		}
	}

	public void CheckPower()
	{
		switch (GameContext.power)
		{
		case 5:
			this.fireRate = this.baseFireRate * 0.9f;
			break;
		case 6:
			this.fireRate = this.baseFireRate * 0.9f;
			this.arrPosSpawnBullet[1].gameObject.SetActive(true);
			this.arrPosSpawnBullet[this.length / 2 + 1].gameObject.SetActive(true);
			break;
		case 7:
			this.fireRate = this.baseFireRate * 0.9f;
			this.arrPosSpawnBullet[1].gameObject.SetActive(true);
			this.arrPosSpawnBullet[this.length / 2 + 1].gameObject.SetActive(true);
			break;
		case 8:
			this.fireRate = this.baseFireRate * 0.9f;
			this.ActiveAllShot();
			break;
		case 9:
			this.fireRate = this.baseFireRate * 0.8f;
			this.ActiveAllShot();
			break;
		case 10:
			this.fireRate = this.baseFireRate * 0.7f;
			this.ActiveAllShot();
			break;
		}
	}

	private void ActiveAllShot()
	{
		if (!this.arrPosSpawnBullet[1].gameObject.activeInHierarchy)
		{
			this.arrPosSpawnBullet[1].gameObject.SetActive(true);
		}
		if (!this.arrPosSpawnBullet[2].gameObject.activeInHierarchy)
		{
			this.arrPosSpawnBullet[2].gameObject.SetActive(true);
		}
		if (!this.arrPosSpawnBullet[this.length / 2 + 1].gameObject.activeInHierarchy)
		{
			this.arrPosSpawnBullet[this.length / 2 + 1].gameObject.SetActive(true);
		}
		if (!this.arrPosSpawnBullet[this.length / 2 + 2].gameObject.activeInHierarchy)
		{
			this.arrPosSpawnBullet[this.length / 2 + 2].gameObject.SetActive(true);
		}
	}

	public GameObject target;

	public Transform[] arrPosSpawnBullet;

	public GameObject[] arrFxSpawnBullet;

	public int power;

	public float fireRate;

	public GameObject bullet;

	public Transform transformPlayer;

	private float nextTime;

	private bool isStopFire = true;

	private float baseFireRate;

	private int length;

	private int lengthFX;
}
