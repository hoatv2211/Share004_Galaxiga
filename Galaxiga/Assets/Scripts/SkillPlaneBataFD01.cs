using System;

public class SkillPlaneBataFD01 : PlayerSkillController
{
	public override void Start()
	{
		this.fireRate = PlaneDataSheet.Get(0).fireRateSuperPower;
		this.nwayShot.m_nextLineDelay = this.fireRate;
	}

	public override void StartSpecialSkill()
	{
		this.ubhShot.gameObject.SetActive(true);
		this.ubhShot.StartShotRoutine();
	}

	public override void StopSpecialSkill()
	{
		this.ubhShot.StopShotRoutine();
		this.ubhShot.gameObject.SetActive(false);
	}

	public UbhShotCtrl ubhShot;

	public UbhNwayShot nwayShot;
}
