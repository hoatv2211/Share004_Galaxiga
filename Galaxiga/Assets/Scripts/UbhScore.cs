using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UbhScore : UbhMonoBehaviour
{
	private void Start()
	{
		this.Initialize();
	}

	private void Update()
	{
		if (this.m_highScore < this.m_score)
		{
			this.m_highScore = this.m_score;
		}
		this.m_scoreText.text = this.m_score.ToString();
		this.m_highScoreText.text = "HighScore : " + this.m_highScore.ToString();
	}

	public void Initialize()
	{
		if (this.m_deleteScore)
		{
		}
		this.m_score = 0;
		this.m_highScore = PlayerPrefs.GetInt("highScoreKey", 0);
	}

	public void AddPoint(int point)
	{
		this.m_score += point;
	}

	public void Save()
	{
		PlayerPrefs.SetInt("highScoreKey", this.m_highScore);
		PlayerPrefs.Save();
		this.Initialize();
	}

	private const string HIGH_SCORE_KEY = "highScoreKey";

	private const string HIGH_SCORE_TITLE = "HighScore : ";

	[SerializeField]
	[FormerlySerializedAs("_DeleteScore")]
	private bool m_deleteScore;

	[SerializeField]
	private Text m_scoreText;

	[SerializeField]
	private Text m_highScoreText;

	private int m_score;

	private int m_highScore;
}
