using System;
using UnityEngine;

public class NewDroneGodOfThunder : Drone
{
	public override void Awake()
	{
		base.Awake();
	}

	public override void Update()
	{
		base.Update();
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha3))
		{
			this.StartShot();
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha4))
		{
			this.StartSkill();
		}
	}

	public override void StartSkillNow(Action actionStartSkill, Action actionSkillComplete, GameContext.DronePosition dronePosition)
	{
		base.StartSkillNow(actionStartSkill, actionSkillComplete, dronePosition);
	}

	public override void StartSkill()
	{
		base.StartSkill();
		this.ActiveSkill();
	}

	public override void StopSkill()
	{
		base.StopSkill();
		this.InactiveSkill();
	}

	public override void StartShot()
	{
		this.ubhShot.StartShotRoutine();
		this.fx_Shot.SetActive(true);
	}

	public override void StopShot()
	{
		this.ubhShot.StopShotRoutine();
		this.fx_Shot.SetActive(false);
	}

	private void ActiveSkill()
	{
		GameObject gameObject = GameUtil.ObjectPoolSpawnGameObject("BulletDrone", this.objSkill.name, base.gameObject.transform.position, Quaternion.identity);
		this.gunLightning = gameObject.transform.GetChild(1).GetComponent<GunLightning>();
		this.gunLightning.StartFire();
		this.fx_Skill.Play();
	}

	private void InactiveSkill()
	{
	}

	[SerializeField]
	private UbhShotCtrl ubhShot;

	[SerializeField]
	private UbhRandomShot ubhRandomShot;

	[SerializeField]
	private GameObject objSkill;

	[SerializeField]
	private ParticleSystem fx_Skill;

	[SerializeField]
	private GameObject fx_Shot;

	private GunLightning gunLightning;
}
