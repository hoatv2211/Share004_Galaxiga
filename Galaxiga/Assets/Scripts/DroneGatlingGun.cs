using System;
using UnityEngine;

public class DroneGatlingGun : Drone
{
	public override void Awake()
	{
		base.Awake();
	}

	public override void Start()
	{
		base.Start();
		this.StopShot();
	}

	public override void Update()
	{
		if (this.isStartSkill && Time.time >= this.nextTime)
		{
			this.nextTime = Time.time + this._fireRate;
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

	public void SpawBullet()
	{
		float y = UnityEngine.Random.Range(-GameContext.orthorgrhicSize - 1f, -GameContext.orthorgrhicSize - 3f);
		float x = UnityEngine.Random.Range(-GameContext.sizeBoudaryCam, GameContext.sizeBoudaryCam);
		Vector2 v = new Vector2(x, y);
		GameObject gameObject = GameUtil.ObjectPoolSpawnGameObject("BulletDrone", "Bullet Skill Gatling Gun", v, Quaternion.identity);
		BulletSkillDroneGatlingGun component = gameObject.GetComponent<BulletSkillDroneGatlingGun>();
		component.speedMove = UnityEngine.Random.Range(12f, 17f);
	}

	public override void StartShot()
	{
		this.ubhShot.StartShotRoutine();
	}

	public override void StopShot()
	{
		this.ubhShot.StopShotRoutine();
	}

	public override void SetDataDrone()
	{
		this.ubhLinearShot.m_betweenDelay = this.fireRate;
	}

	public UbhShotCtrl ubhShot;

	private float _fireRate = 0.035f;

	private float nextTime;

	public UbhLinearShot ubhLinearShot;

	private bool isStartSkill;
}
