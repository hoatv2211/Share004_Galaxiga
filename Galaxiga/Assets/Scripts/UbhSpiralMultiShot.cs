using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Spiral Multi Shot")]
public class UbhSpiralMultiShot : UbhBaseShot
{
	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		if (this.m_bulletNum <= 0 || this.m_bulletSpeed <= 0f || this.m_spiralWayNum <= 0)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed or SpiralWayNum is not set.");
			yield break;
		}
		if (this.m_shooting)
		{
			yield break;
		}
		this.m_shooting = true;
		float spiralWayShiftAngle = 360f / (float)this.m_spiralWayNum;
		int spiralWayIndex = 0;
		for (int i = 0; i < this.m_bulletNum; i++)
		{
			if (this.m_spiralWayNum <= spiralWayIndex)
			{
				spiralWayIndex = 0;
				if (0f < this.m_betweenDelay)
				{
					base.FiredShot();
					yield return UbhUtil.WaitForSeconds(this.m_betweenDelay);
				}
			}
			UbhBullet bullet = base.GetBullet(base.transform.position, false);
			if (bullet == null)
			{
				break;
			}
			float angle = this.m_startAngle + spiralWayShiftAngle * (float)spiralWayIndex + this.m_shiftAngle * Mathf.Floor((float)(i / this.m_spiralWayNum));
			base.ShotBullet(bullet, this.m_bulletSpeed, angle, false, null, 0f, false, 0f, 0f);
			spiralWayIndex++;
		}
		base.FiredShot();
		base.FinishedShot();
		yield break;
	}

	[Header("===== SpiralMultiShot Settings =====")]
	[FormerlySerializedAs("_SpiralWayNum")]
	public int m_spiralWayNum = 4;

	[Range(0f, 360f)]
	[FormerlySerializedAs("_StartAngle")]
	public float m_startAngle = 180f;

	[Range(-360f, 360f)]
	[FormerlySerializedAs("_ShiftAngle")]
	public float m_shiftAngle = 5f;

	[FormerlySerializedAs("_BetweenDelay")]
	public float m_betweenDelay = 0.2f;
}
