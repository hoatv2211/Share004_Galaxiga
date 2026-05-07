using System;

public class SkillPlaneWarlock : PlayerSkillController
{
	public override void SetPowerSkil(int power, float _fireRate)
	{
		base.SetPowerSkil(power, _fireRate);
		this.homingMissile.SetPower(_fireRate, power);
	}

	public override void StartSpecialSkill()
	{
		this.homingMissile.gameObject.SetActive(true);
		this.homingMissile.StartFire();
		this.shotCtrlSkill.gameObject.SetActive(true);
		this.shotCtrlSkill.StartShotRoutine();
	}

	public override void StopSpecialSkill()
	{
		this.shotCtrlSkill.StopShotRoutine();
		this.shotCtrlSkill.gameObject.SetActive(false);
		this.homingMissile.StopFire();
		this.homingMissile.gameObject.SetActive(false);
	}

	public GunHomingMissile homingMissile;

	public UbhShotCtrl shotCtrlSkill;
}
