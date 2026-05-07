using System;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.isStopFire)
		{
			return;
		}
		if (Time.time >= this.nextTime)
		{
			this.nextTime = Time.time + this.fireRate;
			this.Fire();
		}
	}

	private void Fire()
	{
		GameObject gameObject = GameUtil.ObjectPoolSpawnGameObject("BulletPlayer", this.homingMissile.name, base.gameObject.transform.position, base.transform.localRotation);
		HomingMissile component = gameObject.GetComponent<HomingMissile>();
		component.SetFollowTarget();
	}

	public void StartFire()
	{
		this.isStopFire = false;
	}

	public void StopFire()
	{
		this.isStopFire = true;
	}

	public float fireRate = 0.5f;

	private float nextTime;

	public GameObject homingMissile;

	private bool isStopFire = true;
}
