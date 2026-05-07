using System;
using SkyGameKit;

public class SpawnBulletChild : SgkBullet
{
	protected override void OnEnable()
	{
		base.OnEnable();
		this.Delay(this.timeSpawn, delegate
		{
			if (this.spawnBullet)
			{
				this.ubhShot.StartShotRoutine();
			}
			this.Explosion();
		}, false);
	}

	public float timeSpawn = 5f;

	public bool spawnBullet = true;

	public UbhShotCtrl ubhShot;
}
