using System;

public class SkillPlaneSkyWraith : PlayerSkillController
{
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
}
