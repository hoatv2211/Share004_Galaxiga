using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/nWay Shot")]
public class UbhNwayShot : UbhBaseShot
{
	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		if (this.m_bulletNum <= 0 || this.m_bulletSpeed <= 0f || this.m_wayNum <= 0)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed or WayNum is not set.");
			yield break;
		}
		if (this.m_shooting)
		{
			yield break;
		}
		this.m_shooting = true;
		int wayIndex = 0;
		for (int i = 0; i < this.m_bulletNum; i++)
		{
			if (this.m_wayNum <= wayIndex)
			{
				wayIndex = 0;
				if (0f < this.m_nextLineDelay)
				{
					base.FiredShot();
					yield return UbhUtil.WaitForSeconds(this.m_nextLineDelay);
				}
			}
			UbhBullet bullet = base.GetBullet(base.transform.position, false);
			if (bullet == null)
			{
				break;
			}
			float baseAngle = (this.m_wayNum % 2 != 0) ? this.m_centerAngle : (this.m_centerAngle - this.m_betweenAngle / 2f);
			float angle = UbhUtil.GetShiftedAngle(wayIndex, baseAngle, this.m_betweenAngle);
			base.ShotBullet(bullet, this.m_bulletSpeed, angle, false, null, 0f, false, 0f, 0f);
			wayIndex++;
		}
		base.FiredShot();
		base.FinishedShot();
		yield break;
	}

	[Header("===== NwayShot Settings =====")]
	[FormerlySerializedAs("_WayNum")]
	public int m_wayNum = 5;

	[Range(0f, 360f)]
	[FormerlySerializedAs("_CenterAngle")]
	public float m_centerAngle = 180f;

	[Range(0f, 360f)]
	[FormerlySerializedAs("_BetweenAngle")]
	public float m_betweenAngle = 10f;

	[FormerlySerializedAs("_NextLineDelay")]
	public float m_nextLineDelay = 0.1f;
}
