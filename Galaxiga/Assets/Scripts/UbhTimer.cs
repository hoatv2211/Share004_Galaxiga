using System;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class UbhTimer : UbhSingletonMonoBehavior<UbhTimer>
{
	public UbhUtil.TIME deltaTimeType
	{
		get
		{
			return this.m_deltaTimeType;
		}
		set
		{
			this.m_deltaTimeType = value;
		}
	}

	public bool pausing
	{
		get
		{
			return this.m_pausing;
		}
	}

	public float deltaTime
	{
		get
		{
			if (this.m_pausing)
			{
				return 0f;
			}
			switch (this.m_deltaTimeType)
			{
			case UbhUtil.TIME.UNSCALED_DELTA_TIME:
				return this.m_deltaTimeUnscaled;
			case UbhUtil.TIME.FIXED_DELTA_TIME:
				return this.m_deltaTimeFixed;
			}
			return this.m_deltaTime;
		}
	}

	public float deltaFrameCount
	{
		get
		{
			if (this.m_pausing)
			{
				return 0f;
			}
			switch (this.m_deltaTimeType)
			{
			case UbhUtil.TIME.UNSCALED_DELTA_TIME:
				return this.m_deltaFrameCountUnscaled;
			case UbhUtil.TIME.FIXED_DELTA_TIME:
				return this.m_deltaFrameCountFixed;
			}
			return this.m_deltaFrameCount;
		}
	}

	public float totalFrameCount
	{
		get
		{
			switch (this.m_deltaTimeType)
			{
			case UbhUtil.TIME.UNSCALED_DELTA_TIME:
				return this.m_totalFrameCountUnscaled;
			case UbhUtil.TIME.FIXED_DELTA_TIME:
				return this.m_totalFrameCountFixed;
			}
			return this.m_totalFrameCount;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		this.UpdateTimes();
	}

	private void Update()
	{
		this.UpdateTimes();
		UbhSingletonMonoBehavior<UbhBulletManager>.instance.UpdateBullets(this.deltaTime);
	}

	private void UpdateTimes()
	{
		this.m_deltaTime = Time.deltaTime;
		this.m_deltaTimeUnscaled = Time.unscaledDeltaTime;
		int vSyncCount = QualitySettings.vSyncCount;
		float num;
		if (vSyncCount == 1)
		{
			num = (float)Screen.currentResolution.refreshRate;
		}
		else if (vSyncCount == 2)
		{
			num = (float)Screen.currentResolution.refreshRate / 2f;
		}
		else
		{
			num = (float)Application.targetFrameRate;
		}
		if (num > 0f)
		{
			this.m_deltaTimeFixed = 0.0166666675f * (60f / num);
		}
		else
		{
			this.m_deltaTimeFixed = 0f;
		}
		this.m_deltaFrameCount = this.m_deltaTime / 0.0166666675f;
		this.m_deltaFrameCountUnscaled = this.m_deltaTimeUnscaled / 0.0166666675f;
		this.m_deltaFrameCountFixed = this.m_deltaTimeFixed / 0.0166666675f;
		if (!this.m_pausing)
		{
			this.m_totalFrameCount += this.m_deltaFrameCount;
			this.m_totalFrameCountUnscaled += this.m_deltaFrameCountUnscaled;
			this.m_totalFrameCountFixed += this.m_deltaFrameCountFixed;
		}
	}

	public void Pause()
	{
		this.m_pausing = true;
	}

	public void Resume()
	{
		this.m_pausing = false;
	}

	private const float FIXED_DELTA_TIME_BASE = 0.0166666675f;

	[SerializeField]
	private UbhUtil.TIME m_deltaTimeType;

	private float m_deltaTime;

	private float m_deltaTimeUnscaled;

	private float m_deltaTimeFixed;

	private float m_deltaFrameCount;

	private float m_deltaFrameCountUnscaled;

	private float m_deltaFrameCountFixed;

	private float m_totalFrameCount;

	private float m_totalFrameCountUnscaled;

	private float m_totalFrameCountFixed;

	private bool m_pausing;
}
