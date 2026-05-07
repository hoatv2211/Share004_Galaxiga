using System;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Spread nWay Shot (Lock On)")]
public class UbhSpreadNwayLockOnShot : UbhSpreadNwayShot
{
	public override bool lockOnShot
	{
		get
		{
			return true;
		}
	}

	public override void Shot()
	{
		if (this.m_shooting)
		{
			return;
		}
		this.AimTarget();
		if (this.m_targetTransform == null)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because TargetTransform is not set.");
			return;
		}
		base.Shot();
	}

	private void AimTarget()
	{
		if (this.m_targetTransform == null && this.m_setTargetFromTag)
		{
			this.m_targetTransform = UbhUtil.GetTransformFromTagName(this.m_targetTagName, this.m_randomSelectTagTarget);
		}
		if (this.m_targetTransform != null)
		{
			this.m_centerAngle = UbhUtil.GetAngleFromTwoPosition(base.transform, this.m_targetTransform, base.shotCtrl.m_axisMove);
		}
	}

	[Header("===== SpreadNwayLockOnShot Settings =====")]
	[FormerlySerializedAs("_SetTargetFromTag")]
	public bool m_setTargetFromTag = true;

	[FormerlySerializedAs("_TargetTagName")]
	[UbhConditionalHide("m_setTargetFromTag")]
	public string m_targetTagName = "Player";

	public bool m_randomSelectTagTarget;

	[FormerlySerializedAs("_TargetTransform")]
	public Transform m_targetTransform;
}
