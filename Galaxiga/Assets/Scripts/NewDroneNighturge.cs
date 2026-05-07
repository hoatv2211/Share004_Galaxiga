using System;
using System.Collections;
using UnityEngine;

public class NewDroneNighturge : Drone
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
		if (this.isStartSkill)
		{
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
		{
			this.StartShot();
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha2))
		{
			this.StartSkill();
		}
	}

	public override void StartSkillNow(Action startSkill, Action actionSkillComplete, GameContext.DronePosition dronePosition)
	{
		base.StartSkillNow(startSkill, actionSkillComplete, dronePosition);
	}

	public override void StartSkill()
	{
		this.isStartSkill = true;
		base.StartCoroutine(this.SpawnBigBomb());
	}

	public override void StopSkill()
	{
		this.isStartSkill = false;
	}

	public override void StartShot()
	{
		this.ubhShot0.StartShotRoutine();
		this.fx_Shot.SetActive(true);
	}

	public override void StopShot()
	{
		this.ubhShot0.StopShotRoutine();
		this.fx_Shot.SetActive(false);
	}

	private IEnumerator SpawnBigBomb()
	{
		this.fx_Skill.SetActive(true);
		this.StopShot();
		yield return new WaitForSeconds(1.5f);
		this.ubhShot1.StartShotRoutine();
		this.fx_Skill.SetActive(false);
		yield return new WaitForSeconds(1f);
		this.StartShot();
		yield break;
	}

	public override void SetDataDrone()
	{
	}

	public UbhShotCtrl ubhShot0;

	public UbhShotCtrl ubhShot1;

	public UbhLinearShot ubhLinearShot0;

	public UbhLinearShot ubhLinearShot1;

	[SerializeField]
	private GameObject fx_Skill;

	[SerializeField]
	private GameObject fx_Shot;

	private bool isStartSkill;
}
