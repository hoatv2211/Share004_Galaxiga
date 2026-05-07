using System;
using UnityEngine;
using UnityEngine.UI;

public class UbhDebugInfo : UbhMonoBehaviour
{
	private void Start()
	{
		if (!Debug.isDebugBuild)
		{
			base.gameObject.SetActive(false);
			return;
		}
		this.m_lastUpdateTime = Time.realtimeSinceStartup;
		this.m_bulletManager = UbhSingletonMonoBehavior<UbhBulletManager>.instance;
	}

	private void Update()
	{
		this.m_frame++;
		float num = Time.realtimeSinceStartup - this.m_lastUpdateTime;
		if (num < 1f)
		{
			return;
		}
		float num2 = (float)this.m_frame / num;
		if (this.m_fpsText != null)
		{
			this.m_fpsText.text = ((int)num2).ToString();
		}
		this.m_lastUpdateTime = Time.realtimeSinceStartup;
		this.m_frame = 0;
		if (this.m_bulletManager != null && this.m_bulletNumText != null)
		{
			this.m_bulletNumText.text = this.m_bulletManager.activeBulletCount.ToString();
		}
	}

	private const float INTERVAL_SEC = 1f;

	[SerializeField]
	private Text m_fpsText;

	[SerializeField]
	private Text m_bulletNumText;

	private UbhBulletManager m_bulletManager;

	private float m_lastUpdateTime;

	private int m_frame;
}
