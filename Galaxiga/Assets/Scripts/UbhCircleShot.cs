using System;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Circle Shot")]
public class UbhCircleShot : UbhBaseShot
{
	public override void Shot()
	{
		if (this.m_bulletNum <= 0 || this.m_bulletSpeed <= 0f)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed is not set.");
			return;
		}
		float num = 360f / (float)this.m_bulletNum;
		for (int i = 0; i < this.m_bulletNum; i++)
		{
			UbhBullet bullet = base.GetBullet(base.transform.position, false);
			if (bullet == null)
			{
				break;
			}
			float angle = num * (float)i;
			base.ShotBullet(bullet, this.m_bulletSpeed, angle, false, null, 0f, false, 0f, 0f);
		}
		base.FiredShot();
		base.FinishedShot();
	}
}
