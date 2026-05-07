using System;

public class SkillPlaneGreataxe : PlayerSkillController
{
	public override void SetPowerSkil(int power, float _fireRate)
	{
		for (int i = 0; i < this.missiles.Length; i++)
		{
			this.missiles[i].fireRate = _fireRate;
		}
	}

	public override void StartSpecialSkill()
	{
		int num = this.missiles.Length;
		for (int i = 0; i < num; i++)
		{
			this.missiles[i].gameObject.SetActive(true);
			this.missiles[i].StartFire();
		}
	}

	public override void StopSpecialSkill()
	{
		int num = this.missiles.Length;
		for (int i = 0; i < num; i++)
		{
			this.missiles[i].gameObject.SetActive(false);
			this.missiles[i].StopFire();
		}
	}

	public FireBullet[] missiles;
}
