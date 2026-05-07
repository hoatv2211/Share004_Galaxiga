using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Spiral Shot")]
public class UbhSpiralShot : UbhBaseShot
{
	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		if (this.m_bulletNum <= 0 || this.m_bulletSpeed <= 0f)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed is not set.");
			yield break;
		}
		if (this.m_shooting)
		{
			yield break;
		}
		this.m_shooting = true;
		for (int i = 0; i < this.m_bulletNum; i++)
		{
			if (0 < i && 0f < this.m_betweenDelay)
			{
				base.FiredShot();
				yield return UbhUtil.WaitForSeconds(this.m_betweenDelay);
			}
			UbhBullet bullet = base.GetBullet(base.transform.position, false);
			if (bullet == null)
			{
				break;
			}
			float angle = this.m_startAngle + this.m_shiftAngle * (float)i;
			base.ShotBullet(bullet, this.m_bulletSpeed, angle, false, null, 0f, false, 0f, 0f);
		}
		base.FiredShot();
		base.FinishedShot();
		yield break;
	}

	[Header("===== SpiralShot Settings =====")]
	[Range(0f, 360f)]
	[FormerlySerializedAs("_StartAngle")]
	public float m_startAngle = 180f;

	[Range(-360f, 360f)]
	[FormerlySerializedAs("_ShiftAngle")]
	public float m_shiftAngle = 5f;

	[FormerlySerializedAs("_BetweenDelay")]
	public float m_betweenDelay = 0.2f;
}
