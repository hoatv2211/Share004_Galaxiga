using System;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Spread nWay Shot")]
public class UbhSpreadNwayShot : UbhBaseShot
{
	public override void Shot()
	{
		if (this.m_bulletNum <= 0 || this.m_bulletSpeed <= 0f || this.m_wayNum <= 0)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed or WayNum is not set.");
			return;
		}
		if (this.m_shooting)
		{
			return;
		}
		this.m_shooting = true;
		int num = 0;
		float num2 = this.m_bulletSpeed;
		for (int i = 0; i < this.m_bulletNum; i++)
		{
			if (this.m_wayNum <= num)
			{
				num = 0;
				for (num2 -= this.m_diffSpeed; num2 <= 0f; num2 += Mathf.Abs(this.m_diffSpeed))
				{
				}
			}
			UbhBullet bullet = base.GetBullet(base.transform.position, false);
			if (bullet == null)
			{
				break;
			}
			float baseAngle = (this.m_wayNum % 2 != 0) ? this.m_centerAngle : (this.m_centerAngle - this.m_betweenAngle / 2f);
			float shiftedAngle = UbhUtil.GetShiftedAngle(num, baseAngle, this.m_betweenAngle);
			base.ShotBullet(bullet, num2, shiftedAngle, false, null, 0f, false, 0f, 0f);
			num++;
		}
		base.FiredShot();
		base.FinishedShot();
	}

	[Header("===== SpreadNwayShot Settings =====")]
	[FormerlySerializedAs("_WayNum")]
	public int m_wayNum = 8;

	[Range(0f, 360f)]
	[FormerlySerializedAs("_CenterAngle")]
	public float m_centerAngle = 180f;

	[Range(0f, 360f)]
	[FormerlySerializedAs("_BetweenAngle")]
	public float m_betweenAngle = 10f;

	[FormerlySerializedAs("_DiffSpeed")]
	public float m_diffSpeed = 0.5f;
}
