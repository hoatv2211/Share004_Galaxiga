using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Random Shot")]
public class UbhRandomShot : UbhBaseShot
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
		List<int> numList = new List<int>(this.m_bulletNum);
		for (int i = 0; i < this.m_bulletNum; i++)
		{
			numList.Add(i);
		}
		while (0 < numList.Count)
		{
			int index = UnityEngine.Random.Range(0, numList.Count);
			UbhBullet bullet = base.GetBullet(base.transform.position, false);
			if (bullet == null)
			{
				break;
			}
			float bulletSpeed = UnityEngine.Random.Range(this.m_randomSpeedMin, this.m_randomSpeedMax);
			float minAngle = this.m_randomCenterAngle - this.m_randomRangeSize / 2f;
			float maxAngle = this.m_randomCenterAngle + this.m_randomRangeSize / 2f;
			float angle = 0f;
			if (this.m_evenlyDistribute)
			{
				float num = Mathf.Floor((float)this.m_bulletNum / 4f);
				float num2 = Mathf.Floor((float)numList[index] / num);
				float num3 = Mathf.Abs(maxAngle - minAngle) / 4f;
				angle = UnityEngine.Random.Range(minAngle + num3 * num2, minAngle + num3 * (num2 + 1f));
			}
			else
			{
				angle = UnityEngine.Random.Range(minAngle, maxAngle);
			}
			base.ShotBullet(bullet, bulletSpeed, angle, false, null, 0f, false, 0f, 0f);
			numList.RemoveAt(index);
			if (0 < numList.Count && 0f <= this.m_randomDelayMin && 0f < this.m_randomDelayMax)
			{
				base.FiredShot();
				float waitTime = UnityEngine.Random.Range(this.m_randomDelayMin, this.m_randomDelayMax);
				yield return UbhUtil.WaitForSeconds(waitTime);
			}
		}
		base.FiredShot();
		base.FinishedShot();
		yield break;
	}

	[Header("===== RandomShot Settings =====")]
	[Range(0f, 360f)]
	[FormerlySerializedAs("_RandomCenterAngle")]
	public float m_randomCenterAngle = 180f;

	[Range(0f, 360f)]
	[FormerlySerializedAs("_RandomRangeSize")]
	public float m_randomRangeSize = 360f;

	[FormerlySerializedAs("_RandomSpeedMin")]
	public float m_randomSpeedMin = 1f;

	[FormerlySerializedAs("_RandomSpeedMax")]
	public float m_randomSpeedMax = 3f;

	[FormerlySerializedAs("_RandomDelayMin")]
	public float m_randomDelayMin = 0.01f;

	[FormerlySerializedAs("_RandomDelayMax")]
	public float m_randomDelayMax = 0.1f;

	[FormerlySerializedAs("_EvenlyDistribute")]
	public bool m_evenlyDistribute = true;
}
