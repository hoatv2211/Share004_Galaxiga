using System;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Hole Circle Shot")]
public class UbhHoleCircleShot : UbhBaseShot
{
	public override void Shot()
	{
		if (this.m_bulletNum <= 0 || this.m_bulletSpeed <= 0f)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed is not set.");
			return;
		}
		this.m_holeCenterAngle = UbhUtil.GetNormalizedAngle(this.m_holeCenterAngle);
		float num = this.m_holeCenterAngle - this.m_holeSize / 2f;
		float num2 = this.m_holeCenterAngle + this.m_holeSize / 2f;
		float num3 = 360f / (float)this.m_bulletNum;
		for (int i = 0; i < this.m_bulletNum; i++)
		{
			float num4 = num3 * (float)i;
			if (num > num4 || num4 > num2)
			{
				UbhBullet bullet = base.GetBullet(base.transform.position, false);
				if (bullet == null)
				{
					break;
				}
				base.ShotBullet(bullet, this.m_bulletSpeed, num4, false, null, 0f, false, 0f, 0f);
			}
		}
		base.FiredShot();
		base.FinishedShot();
	}

	[Header("===== HoleCircleShot Settings =====")]
	[Range(0f, 360f)]
	[FormerlySerializedAs("_HoleCenterAngle")]
	public float m_holeCenterAngle = 180f;

	[Range(0f, 360f)]
	[FormerlySerializedAs("_HoleSize")]
	public float m_holeSize = 20f;
}
