using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UbhSetting : UbhMonoBehaviour
{
	private void Start()
	{
		this.SetValue();
	}

	private void Update()
	{
		if (UbhUtil.IsMobilePlatform() && UnityEngine.Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	private void SetValue()
	{
		this.m_vsyncCount = Mathf.Clamp(this.m_vsyncCount, 0, 2);
		QualitySettings.vSyncCount = this.m_vsyncCount;
		this.m_frameRate = Mathf.Clamp(this.m_frameRate, 1, 120);
		Application.targetFrameRate = this.m_frameRate;
	}

	[Range(0f, 2f)]
	[FormerlySerializedAs("_VsyncCount")]
	public int m_vsyncCount = 1;

	[Range(0f, 120f)]
	[FormerlySerializedAs("_FrameRate")]
	public int m_frameRate = 60;
}
