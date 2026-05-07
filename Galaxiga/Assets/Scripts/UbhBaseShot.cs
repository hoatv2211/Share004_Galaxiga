using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class UbhBaseShot : UbhMonoBehaviour
{
	public UbhShotCtrl shotCtrl
	{
		get
		{
			if (this.m_shotCtrl == null)
			{
				this.m_shotCtrl = base.transform.GetComponentInParent<UbhShotCtrl>();
			}
			return this.m_shotCtrl;
		}
	}

	public bool shooting
	{
		get
		{
			return this.m_shooting;
		}
	}

	public virtual bool lockOnShot
	{
		get
		{
			return false;
		}
	}

	protected virtual void OnDisable()
	{
		this.m_shooting = false;
	}

	public abstract void Shot();

	public void SetShotCtrl(UbhShotCtrl shotCtrl)
	{
		this.m_shotCtrl = shotCtrl;
	}

	protected void FiredShot()
	{
		this.m_shotFiredCallbackEvents.Invoke();
	}

	protected void FinishedShot()
	{
		this.m_shooting = false;
		this.m_shotFinishedCallbackEvents.Invoke();
	}

	protected UbhBullet GetBullet(Vector3 position, bool forceInstantiate = false)
	{
		if (this.m_bulletPrefab == null)
		{
			UnityEngine.Debug.LogWarning("Cannot generate a bullet because BulletPrefab is not set.");
			return null;
		}
		UbhBullet bullet = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.m_bulletPrefab, position, forceInstantiate);
		if (bullet == null)
		{
			return null;
		}
		return bullet;
	}

	protected void ShotBullet(UbhBullet bullet, float speed, float angle, bool homing = false, Transform homingTarget = null, float homingAngleSpeed = 0f, bool wave = false, float waveSpeed = 0f, float waveRangeSize = 0f)
	{
		if (bullet == null)
		{
			return;
		}
		bullet.Shot(this, speed, angle, this.m_accelerationSpeed, this.m_accelerationTurn, homing, homingTarget, homingAngleSpeed, wave, waveSpeed, waveRangeSize, this.m_usePauseAndResume, this.m_pauseTime, this.m_resumeTime, this.m_useAutoRelease, this.m_autoReleaseTime, this.m_shotCtrl.m_axisMove, this.m_shotCtrl.m_inheritAngle, this.m_useMaxSpeed, this.m_maxSpeed, this.m_useMinSpeed, this.m_minSpeed);
	}

	[Header("===== Common Settings =====")]
	[FormerlySerializedAs("_BulletPrefab")]
	public GameObject m_bulletPrefab;

	[FormerlySerializedAs("_BulletNum")]
	public int m_bulletNum = 10;

	[FormerlySerializedAs("_BulletSpeed")]
	public float m_bulletSpeed = 2f;

	[FormerlySerializedAs("_AccelerationSpeed")]
	public float m_accelerationSpeed;

	public bool m_useMaxSpeed;

	[UbhConditionalHide("m_useMaxSpeed")]
	public float m_maxSpeed;

	public bool m_useMinSpeed;

	[UbhConditionalHide("m_useMinSpeed")]
	public float m_minSpeed;

	[FormerlySerializedAs("_AccelerationTurn")]
	public float m_accelerationTurn;

	[FormerlySerializedAs("_UsePauseAndResume")]
	public bool m_usePauseAndResume;

	[FormerlySerializedAs("_PauseTime")]
	[UbhConditionalHide("m_usePauseAndResume")]
	public float m_pauseTime;

	[FormerlySerializedAs("_ResumeTime")]
	[UbhConditionalHide("m_usePauseAndResume")]
	public float m_resumeTime;

	[FormerlySerializedAs("_UseAutoRelease")]
	public bool m_useAutoRelease;

	[FormerlySerializedAs("_AutoReleaseTime")]
	[UbhConditionalHide("m_useAutoRelease")]
	public float m_autoReleaseTime = 10f;

	[Space(10f)]
	public UnityEvent m_shotFiredCallbackEvents = new UnityEvent();

	public UnityEvent m_shotFinishedCallbackEvents = new UnityEvent();

	protected bool m_shooting;

	private UbhShotCtrl m_shotCtrl;
}
