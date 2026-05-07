using System;
using DG.Tweening;

public class SkillPlaneTwilightX : PlayerSkillController
{
	public override void SetPowerSkil(int power, float _fireRate)
	{
		for (int i = 0; i < this.spriteBasedLaser.Length; i++)
		{
			this.spriteBasedLaser[i].power = (float)power;
			this.spriteBasedLaser[i].fireRate = _fireRate;
		}
	}

	public override void StartSpecialSkill()
	{
		int num = this.spriteBasedLaser.Length;
		for (int i = 0; i < num; i++)
		{
			this.spriteBasedLaser[i].EnableLaser();
		}
		DOTween.Restart("SKILL_TWILIGHT_X", true, -1f);
		DOTween.Play("SKILL_TWILIGHT_X");
	}

	public override void StopSpecialSkill()
	{
		int num = this.spriteBasedLaser.Length;
		for (int i = 0; i < num; i++)
		{
			this.spriteBasedLaser[i].DisableLaser();
		}
	}

	public Laser[] spriteBasedLaser;
}
