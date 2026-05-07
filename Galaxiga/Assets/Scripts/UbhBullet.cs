using System;
using UnityEngine;

[DisallowMultipleComponent]
public class UbhBullet : UbhMonoBehaviour
{
	public UbhBaseShot parentShot
	{
		get
		{
			return this.m_parentBaseShot;
		}
	}

	public virtual bool isActive
	{
		get
		{
			return base.gameObject.activeSelf;
		}
	}

	private void Awake()
	{
		this.m_transformCache = base.transform;
		this.m_tentacleBullet = base.GetComponent<UbhTentacleBullet>();
	}

	private void OnDisable()
	{
		if (!this.m_shooting || UbhSingletonMonoBehavior<UbhObjectPool>.instance == null)
		{
			return;
		}
		UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this, false);
	}

	public virtual void SetActive(bool isActive)
	{
		base.gameObject.SetActive(isActive);
	}

	public void OnFinishedShot()
	{
		if (!this.m_shooting)
		{
			return;
		}
		this.m_shooting = false;
		this.m_parentBaseShot = null;
		this.m_homingTarget = null;
		this.m_transformCache.ResetPosition(false);
		this.m_transformCache.ResetRotation(false);
	}

	public void Shot(UbhBaseShot parentBaseShot, float speed, float angle, float accelSpeed, float accelTurn, bool homing, Transform homingTarget, float homingAngleSpeed, bool wave, float waveSpeed, float waveRangeSize, bool pauseAndResume, float pauseTime, float resumeTime, bool useAutoRelease, float autoReleaseTime, UbhUtil.AXIS axisMove, bool inheritAngle, bool useMaxSpeed, float maxSpeed, bool useMinSpeed, float minSpeed)
	{
		if (this.m_shooting)
		{
			return;
		}
		this.m_shooting = true;
		this.m_parentBaseShot = parentBaseShot;
		this.m_speed = speed;
		this.m_angle = angle;
		this.m_accelSpeed = accelSpeed;
		this.m_accelTurn = accelTurn;
		this.m_homing = homing;
		this.m_homingTarget = homingTarget;
		this.m_homingAngleSpeed = homingAngleSpeed;
		this.m_wave = wave;
		this.m_waveSpeed = waveSpeed;
		this.m_waveRangeSize = waveRangeSize;
		this.m_pauseAndResume = pauseAndResume;
		this.m_pauseTime = pauseTime;
		this.m_resumeTime = resumeTime;
		this.m_useAutoRelease = useAutoRelease;
		this.m_autoReleaseTime = autoReleaseTime;
		this.m_axisMove = axisMove;
		this.m_useMaxSpeed = useMaxSpeed;
		this.m_maxSpeed = maxSpeed;
		this.m_useMinSpeed = useMinSpeed;
		this.m_minSpeed = minSpeed;
		this.m_baseAngle = 0f;
		if (inheritAngle && !this.m_parentBaseShot.lockOnShot)
		{
			if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
			{
				this.m_baseAngle = this.m_parentBaseShot.shotCtrl.transform.eulerAngles.y;
			}
			else
			{
				this.m_baseAngle = this.m_parentBaseShot.shotCtrl.transform.eulerAngles.z;
			}
		}
		if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
		{
			this.m_transformCache.SetEulerAnglesY(this.m_baseAngle - this.m_angle);
		}
		else
		{
			this.m_transformCache.SetEulerAnglesZ(this.m_baseAngle + this.m_angle);
		}
		this.m_selfFrameCnt = 0f;
		this.m_selfTimeCount = 0f;
	}

	public void UpdateMove(float deltaTime)
	{
		if (!this.m_shooting)
		{
			return;
		}
		this.m_selfTimeCount += deltaTime;
		if (this.m_useAutoRelease && this.m_autoReleaseTime > 0f && this.m_selfTimeCount >= this.m_autoReleaseTime)
		{
			UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this, false);
			return;
		}
		if (this.m_pauseAndResume && this.m_pauseTime >= 0f && this.m_resumeTime > this.m_pauseTime && this.m_pauseTime <= this.m_selfTimeCount && this.m_selfTimeCount < this.m_resumeTime)
		{
			return;
		}
		Vector3 eulerAngles = this.m_transformCache.rotation.eulerAngles;
		Quaternion rotation = this.m_transformCache.rotation;
		if (this.m_homing)
		{
			if (this.m_homingTarget != null && 0f < this.m_homingAngleSpeed)
			{
				float angleFromTwoPosition = UbhUtil.GetAngleFromTwoPosition(this.m_transformCache, this.m_homingTarget, this.m_axisMove);
				float current;
				if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
				{
					current = -eulerAngles.y;
				}
				else
				{
					current = eulerAngles.z;
				}
				float num = Mathf.MoveTowardsAngle(current, angleFromTwoPosition, deltaTime * this.m_homingAngleSpeed);
				if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
				{
					rotation = Quaternion.Euler(eulerAngles.x, -num, eulerAngles.z);
				}
				else
				{
					rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, num);
				}
			}
		}
		else if (this.m_wave)
		{
			this.m_angle += this.m_accelTurn * deltaTime;
			if (0f < this.m_waveSpeed && 0f < this.m_waveRangeSize)
			{
				float num2 = this.m_angle + this.m_waveRangeSize / 2f * Mathf.Sin(this.m_selfFrameCnt * this.m_waveSpeed / 100f);
				if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
				{
					rotation = Quaternion.Euler(eulerAngles.x, this.m_baseAngle - num2, eulerAngles.z);
				}
				else
				{
					rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, this.m_baseAngle + num2);
				}
			}
			this.m_selfFrameCnt += UbhSingletonMonoBehavior<UbhTimer>.instance.deltaFrameCount;
		}
		else
		{
			float num3 = this.m_accelTurn * deltaTime;
			if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
			{
				rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y - num3, eulerAngles.z);
			}
			else
			{
				rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z + num3);
			}
		}
		this.m_speed += this.m_accelSpeed * deltaTime;
		if (this.m_useMaxSpeed && this.m_speed > this.m_maxSpeed)
		{
			this.m_speed = this.m_maxSpeed;
		}
		if (this.m_useMinSpeed && this.m_speed < this.m_minSpeed)
		{
			this.m_speed = this.m_minSpeed;
		}
		Vector3 position;
		if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
		{
			position = this.m_transformCache.position + this.m_transformCache.forward * (this.m_speed * deltaTime);
		}
		else
		{
			position = this.m_transformCache.position + this.m_transformCache.up * (this.m_speed * deltaTime);
		}
		this.m_transformCache.SetPositionAndRotation(position, rotation);
		if (this.m_tentacleBullet != null)
		{
			this.m_tentacleBullet.UpdateRotate();
		}
	}

	private Transform m_transformCache;

	private UbhBaseShot m_parentBaseShot;

	private float m_speed;

	private float m_angle;

	private float m_accelSpeed;

	private float m_accelTurn;

	private bool m_homing;

	private Transform m_homingTarget;

	private float m_homingAngleSpeed;

	private bool m_wave;

	private float m_waveSpeed;

	private float m_waveRangeSize;

	private bool m_pauseAndResume;

	private float m_pauseTime;

	private float m_resumeTime;

	private bool m_useAutoRelease;

	private float m_autoReleaseTime;

	private UbhUtil.AXIS m_axisMove;

	private bool m_useMaxSpeed;

	private float m_maxSpeed;

	private bool m_useMinSpeed;

	private float m_minSpeed;

	private float m_baseAngle;

	private float m_selfFrameCnt;

	private float m_selfTimeCount;

	private UbhTentacleBullet m_tentacleBullet;

	private bool m_shooting;
}
