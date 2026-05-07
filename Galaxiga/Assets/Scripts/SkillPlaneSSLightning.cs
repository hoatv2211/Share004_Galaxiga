using System;
using UnityEngine;

public class SkillPlaneSSLightning : PlayerSkillController
{
	public override void SetPowerSkil(int power, float _fireRate)
	{
		this.gunLightning.SetPower(power, _fireRate);
	}

	public override void StartSpecialSkill()
	{
		this.fxLightningBall.SetActive(true);
		this.gunLightning.StartFire();
		this.ubhShotCtrl.gameObject.SetActive(true);
		this.ubhShotCtrl.StartShotRoutine();
		PlaneIngameManager.current.SetPlayerImortal();
	}

	public override void StopSpecialSkill()
	{
		this.fxLightningBall.SetActive(false);
		this.gunLightning.StopFire();
		PlaneIngameManager.current.SetPlayerNoImortal();
		this.ubhShotCtrl.StopShotRoutine();
		this.ubhShotCtrl.gameObject.SetActive(false);
	}

	public GunLightning gunLightning;

	public GameObject fxLightningBall;

	public UbhShotCtrl ubhShotCtrl;
}
