using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Homing Shot")]
public class UbhHomingShot : UbhBaseShot
{
	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	public virtual void SetTarget()
	{
		if (this.m_targetTransform != null && PlaneIngameManager.current != null)
		{
			this.m_targetTransform = PlaneIngameManager.current.CurrentTransformPlayer;
		}
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
		this.SetTarget();
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
			if (this.m_targetTransform == null && this.m_setTargetFromTag)
			{
				this.m_targetTransform = UbhUtil.GetTransformFromTagName(this.m_targetTagName, this.m_randomSelectTagTarget);
			}
			float angle = UbhUtil.GetAngleFromTwoPosition(base.transform, this.m_targetTransform, base.shotCtrl.m_axisMove);
			base.ShotBullet(bullet, this.m_bulletSpeed, angle, true, this.m_targetTransform, this.m_homingAngleSpeed, false, 0f, 0f);
		}
		base.FiredShot();
		base.FinishedShot();
		yield break;
	}

	[Header("===== HomingShot Settings =====")]
	[FormerlySerializedAs("_BetweenDelay")]
	public float m_betweenDelay = 0.1f;

	[FormerlySerializedAs("_HomingAngleSpeed")]
	public float m_homingAngleSpeed = 20f;

	[FormerlySerializedAs("_SetTargetFromTag")]
	public bool m_setTargetFromTag = true;

	[FormerlySerializedAs("_TargetTagName")]
	[UbhConditionalHide("m_setTargetFromTag")]
	public string m_targetTagName = "Player";

	public bool m_randomSelectTagTarget;

	[FormerlySerializedAs("_TargetTransform")]
	public Transform m_targetTransform;
}
