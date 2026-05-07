using System;

public class DroneGodOfThunder : Drone
{
	public override void Awake()
	{
		base.Awake();
	}

	public override void Update()
	{
		base.Update();
	}

	public override void StartSkillNow(Action actionStartSkill, Action actionSkillComplete, GameContext.DronePosition dronePosition)
	{
		base.StartSkillNow(actionStartSkill, actionSkillComplete, dronePosition);
	}

	public override void StartSkill()
	{
		base.StartSkill();
	}

	public override void StopSkill()
	{
		base.StopSkill();
	}

	public override void StartShot()
	{
	}

	public override void StopShot()
	{
	}
}
