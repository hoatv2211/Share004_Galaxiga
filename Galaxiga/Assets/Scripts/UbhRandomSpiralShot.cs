using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Random Spiral Shot")]
public class UbhRandomSpiralShot : UbhBaseShot
{
	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		if (this.m_bulletNum <= 0 || this.m_randomSpeedMin <= 0f || this.m_randomSpeedMax <= 0f)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or RandomSpeedMin or RandomSpeedMax is not set.");
			yield break;
		}
		if (this.m_shooting)
		{
			yield break;
		}
		this.m_shooting = true;
		for (int i = 0; i < this.m_bulletNum; i++)
		{
			if (0 < i && 0f <= this.m_randomDelayMin && 0f < this.m_randomDelayMax)
			{
				base.FiredShot();
				float waitTime = UnityEngine.Random.Range(this.m_randomDelayMin, this.m_randomDelayMax);
				yield return UbhUtil.WaitForSeconds(waitTime);
			}
			UbhBullet bullet = base.GetBullet(base.transform.position, false);
			if (bullet == null)
			{
				break;
			}
			float bulletSpeed = UnityEngine.Random.Range(this.m_randomSpeedMin, this.m_randomSpeedMax);
			float centerAngle = this.m_startAngle + this.m_shiftAngle * (float)i;
			float minAngle = centerAngle - this.m_randomRangeSize / 2f;
			float maxAngle = centerAngle + this.m_randomRangeSize / 2f;
			float angle = UnityEngine.Random.Range(minAngle, maxAngle);
			base.ShotBullet(bullet, bulletSpeed, angle, false, null, 0f, false, 0f, 0f);
		}
		base.FiredShot();
		base.FinishedShot();
		yield break;
	}

	[Header("===== RandomSpiralShot Settings =====")]
	[Range(0f, 360f)]
	[FormerlySerializedAs("_StartAngle")]
	public float m_startAngle = 180f;

	[Range(-360f, 360f)]
	[FormerlySerializedAs("_ShiftAngle")]
	public float m_shiftAngle = 5f;

	[Range(0f, 360f)]
	[FormerlySerializedAs("_RandomRangeSize")]
	public float m_randomRangeSize = 30f;

	[FormerlySerializedAs("_RandomSpeedMin")]
	public float m_randomSpeedMin = 1f;

	[FormerlySerializedAs("_RandomSpeedMax")]
	public float m_randomSpeedMax = 3f;

	[FormerlySerializedAs("_RandomDelayMin")]
	public float m_randomDelayMin = 0.01f;

	[FormerlySerializedAs("_RandomDelayMax")]
	public float m_randomDelayMax = 0.1f;
}
