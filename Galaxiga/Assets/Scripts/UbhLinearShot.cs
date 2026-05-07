using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Linear Shot")]
public class UbhLinearShot : UbhBaseShot
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
			base.ShotBullet(bullet, this.m_bulletSpeed, this.m_angle, false, null, 0f, false, 0f, 0f);
		}
		base.FiredShot();
		base.FinishedShot();
		yield break;
	}

	[Header("===== LinearShot Settings =====")]
	[Range(0f, 360f)]
	[FormerlySerializedAs("_Angle")]
	public float m_angle = 180f;

	[FormerlySerializedAs("_BetweenDelay")]
	public float m_betweenDelay = 0.1f;
}
