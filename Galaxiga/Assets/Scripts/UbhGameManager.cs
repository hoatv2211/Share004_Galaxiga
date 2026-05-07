using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UbhGameManager : UbhMonoBehaviour
{
	private void Start()
	{
		this.m_goLetterBox.SetActive(!this.m_scaleToFit);
	}

	private void Update()
	{
		if (UbhUtil.IsMobilePlatform())
		{
			if (!this.IsPlaying() && Input.GetMouseButtonDown(0))
			{
				this.GameStart();
			}
		}
		else if (!this.IsPlaying() && UnityEngine.Input.GetKeyDown(KeyCode.X))
		{
			this.GameStart();
		}
	}

	private void GameStart()
	{
		if (this.m_score != null)
		{
			this.m_score.Initialize();
		}
		if (this.m_goTitle != null)
		{
			this.m_goTitle.SetActive(false);
		}
		this.CreatePlayer();
	}

	public void GameOver()
	{
		if (this.m_score != null)
		{
			this.m_score.Save();
		}
		if (this.m_goTitle != null)
		{
			this.m_goTitle.SetActive(true);
		}
		else
		{
			this.CreatePlayer();
		}
	}

	private void CreatePlayer()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.m_playerPrefab, this.m_playerPrefab.transform.position, this.m_playerPrefab.transform.rotation);
	}

	public bool IsPlaying()
	{
		return !(this.m_goTitle != null) || !this.m_goTitle.activeSelf;
	}

	public const int BASE_SCREEN_WIDTH = 600;

	public const int BASE_SCREEN_HEIGHT = 450;

	[FormerlySerializedAs("_ScaleToFit")]
	public bool m_scaleToFit;

	[SerializeField]
	[FormerlySerializedAs("_PlayerPrefab")]
	private GameObject m_playerPrefab;

	[SerializeField]
	[FormerlySerializedAs("_GoTitle")]
	private GameObject m_goTitle;

	[SerializeField]
	[FormerlySerializedAs("_GoLetterBox")]
	private GameObject m_goLetterBox;

	[SerializeField]
	[FormerlySerializedAs("_Score")]
	private UbhScore m_score;
}
