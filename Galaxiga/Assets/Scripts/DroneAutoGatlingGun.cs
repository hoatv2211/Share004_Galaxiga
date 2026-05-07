using System;
using UnityEngine;

public class DroneAutoGatlingGun : Drone
{
	public override void Awake()
	{
		base.Awake();
	}

	public override void Update()
	{
		if (GameContext.isMissionComplete || GameContext.isMissionFail)
		{
			return;
		}
		if (this.isStartSkill && Time.time >= this.nextTime)
		{
			this.nextTime = Time.time + this._fireRate;
			this.SpawBulletSkill();
		}
		if (!this.isStopShot && Time.time >= this.nextTime2)
		{
			this.nextTime2 = Time.time + this.fireRate;
			this.SpawBullet();
		}
	}

	public override void StartSkillNow(Action startSkill, Action actionSkillComplete, GameContext.DronePosition dronePosition)
	{
		base.StartSkillNow(startSkill, actionSkillComplete, dronePosition);
	}

	public override void StartSkill()
	{
		this.isStartSkill = true;
	}

	public override void StopSkill()
	{
		this.isStartSkill = false;
	}

	public void SpawBulletSkill()
	{
		float y = UnityEngine.Random.Range(-GameContext.orthorgrhicSize - 1f, -GameContext.orthorgrhicSize - 3f);
		float x = UnityEngine.Random.Range(-GameContext.sizeBoudaryCam, GameContext.sizeBoudaryCam);
		Vector2 v = new Vector2(x, y);
		GameObject gameObject = GameUtil.ObjectPoolSpawnGameObject("BulletDrone", "Bullet Skill Drone Auto Gatling Gun", v, Quaternion.identity);
		BulletSkillDroneAutoGatlingGun component = gameObject.GetComponent<BulletSkillDroneAutoGatlingGun>();
		component.StartSkill();
	}

	public void SpawBullet()
	{
		GameObject gameObject = GameUtil.ObjectPoolSpawnGameObject("BulletDrone", "Bullet Drone Auto Gatling Gun", this.posSpawBullet.position, Quaternion.identity);
		HomingMissile component = gameObject.GetComponent<HomingMissile>();
		component.SetFollowTarget();
	}

	public override void StartShot()
	{
		this.isStopShot = false;
	}

	public override void StopShot()
	{
		this.isStopShot = true;
	}

	public Transform posSpawBullet;

	private float _fireRate = 0.6f;

	private float nextTime;

	private float nextTime2;

	private bool isStartSkill;

	private bool isStopShot = true;
}
