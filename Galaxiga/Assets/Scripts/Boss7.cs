using System;
using SkyGameKit;

public class Boss7 : BossGeneral
{
	[EnemyAction(displayName = "Boss7/ShootMainWeaponCenter")]
	public void ShootMainWeaponCenter(float nextShootDelay)
	{
		this.Delay(nextShootDelay, delegate
		{
			this.center.ShootMainWeaponStep1();
			base.CheckAction();
		}, false);
	}

	[EnemyAction(displayName = "Boss7/ShootMainWeanponHead")]
	public void ShootMainWeanponHead(float nextShootDelay)
	{
		this.Delay(nextShootDelay, delegate
		{
			this.head.ShootMainWeaponStep1();
			base.CheckAction();
		}, false);
	}

	[EnemyAction(displayName = "Boss7/ShootWeanponLeftRight")]
	public void ShootWeanponLeftRight(float nextShootDelay)
	{
		this.Delay(nextShootDelay, delegate
		{
			this.left.ShootMainWeaponStep1();
			this.right.ShootMainWeaponStep1();
			base.CheckAction();
		}, false);
	}

	public Boss7MainWeanponCenter center;

	public Boss7MainWeanponHead head;

	public Boss7MainWeanponLeftRight left;

	public Boss7MainWeanponLeftRight right;
}
