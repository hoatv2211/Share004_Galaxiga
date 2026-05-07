using System;

public class SkillPlaneFuryOfAres : PlayerSkillController
{
	public override void Start()
	{
		this.fireRate = PlaneDataSheet.Get(1).fireRateSuperPower;
		for (int i = 0; i < this.ubhLinearShots.Length; i++)
		{
			this.ubhLinearShots[i].m_betweenDelay = this.fireRate;
		}
	}

	public override void StartSpecialSkill()
	{
		this.shotCtrl.gameObject.SetActive(true);
		this.shotCtrl.StartShotRoutine();
	}

	public override void StopSpecialSkill()
	{
		this.shotCtrl.StopShotRoutine();
		this.shotCtrl.gameObject.SetActive(false);
	}

	public UbhShotCtrl shotCtrl;

	public UbhLinearShot[] ubhLinearShots;
}
