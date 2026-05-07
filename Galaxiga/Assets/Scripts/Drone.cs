using System;
using System.Collections;
using UnityEngine;

public class Drone : MonoBehaviour
{
	public virtual void Awake()
	{
	}

	private void OnEnable()
	{
		this.GetData();
	}

	public virtual void Start()
	{
		this.skillIsCooldown = false;
		this.SetDataDrone();
	}

	public void GetData()
	{
		this.power = this.droneData.CurrentDamage;
		this.specPower = this.droneData.CurrentSuperDamage;
		this.durationSkill = this.droneData.DurationSkill;
		this.cooldownSkill = this.droneData.CurrentCoolDownSkill;
		this.fireRate = this.droneData.BaseFireRate;
	}

	public virtual void Update()
	{
	}

	public virtual void StartSkillNow(Action actionStartSkill, Action actionSkillComplete, GameContext.DronePosition dronePosition)
	{
		if (!this.skillIsCooldown)
		{
			GameContext.currentDroneActiveSkill = dronePosition;
			actionStartSkill();
			base.StartCoroutine(this.DelaySkill(actionSkillComplete));
			if (NewTutorial.current.currentStepTutorial_EquipDrone == 11)
			{
				NewTutorial.current.EquipDrone_Step11();
			}
		}
		else
		{
			UnityEngine.Debug.Log("skill is actived");
		}
	}

	private IEnumerator DelaySkill(Action ActionSkillComplete)
	{
		this.skillIsCooldown = true;
		this.skillIsActive = true;
		this.StartSkill();
		yield return new WaitForSecondsRealtime(this.durationSkill);
		this.StopSkill();
		ActionSkillComplete();
		this.skillIsActive = false;
		yield break;
	}

	public virtual void StartSkill()
	{
	}

	public virtual void StopSkill()
	{
	}

	public virtual void StartShot()
	{
	}

	public virtual void StopShot()
	{
	}

	public virtual void SetDataDrone()
	{
	}

	public DroneData droneData;

	[HideInInspector]
	public bool skillIsCooldown;

	public int power;

	public int specPower;

	public float fireRate;

	public float cooldownSkill;

	public float durationSkill;

	public bool skillIsActive;
}
