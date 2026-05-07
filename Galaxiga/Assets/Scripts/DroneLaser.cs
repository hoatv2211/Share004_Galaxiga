using System;
using UnityEngine;

public class DroneLaser : Drone
{
	public override void Awake()
	{
		base.Awake();
		this.bulletLaser = this.objLaser.GetComponent<BulletLaserPlayer>();
		this.bulletSkillLaser = this.objLaserSkill.GetComponent<BulletLaserPlayer>();
	}

	public override void Start()
	{
		base.Start();
		this.SetData();
	}

	private void SetData()
	{
		if (this.bulletLaser != null)
		{
			this.bulletLaser.power = (float)this.power;
			this.bulletLaser.fireRate = this.fireRate;
		}
		if (this.bulletSkillLaser != null)
		{
			this.bulletSkillLaser.power = (float)this.specPower;
			this.bulletSkillLaser.fireRate = this.fireRate;
		}
	}

	public override void StartSkillNow(Action startSkill, Action actionSkillComplete, GameContext.DronePosition dronePosition)
	{
		base.StartSkillNow(startSkill, actionSkillComplete, dronePosition);
	}

	public override void StartSkill()
	{
		this.objLaserSkill.SetActive(true);
		this.bulletSkillLaser.EnableLaser();
	}

	public override void StopSkill()
	{
		this.bulletSkillLaser.DisableLaser();
		this.objLaserSkill.SetActive(false);
	}

	public void SetPositionObjSkill(Transform _parent)
	{
		this.objLaserSkill.transform.parent = _parent;
		this.objLaserSkill.transform.localPosition = Vector3.zero;
	}

	public override void StartShot()
	{
		this.bulletLaser.StartFireLaser();
	}

	public override void StopShot()
	{
		this.bulletLaser.StopFireLaser();
	}

	public GameObject objLaser;

	public GameObject objLaserSkill;

	private BulletLaserPlayer bulletLaser;

	private BulletLaserPlayer bulletSkillLaser;
}
