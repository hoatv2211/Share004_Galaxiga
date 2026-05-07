using System;
using SkyGameKit;
using UnityEngine;

public class CheckLevel : MonoBehaviour
{
	private void Awake()
	{
		this.levelNormal.SetActive(false);
		this.levelEasy.SetActive(false);
		this.levelHard.SetActive(false);
		this.Delay(0.2f, delegate
		{
			GameContext.ModeLevel currentModeLevel = GameContext.currentModeLevel;
			if (currentModeLevel != GameContext.ModeLevel.Easy)
			{
				if (currentModeLevel != GameContext.ModeLevel.Normal)
				{
					if (currentModeLevel == GameContext.ModeLevel.Hard)
					{
						this.levelHard.SetActive(true);
					}
				}
				else
				{
					this.levelNormal.SetActive(true);
				}
			}
			else
			{
				this.levelEasy.SetActive(true);
			}
		}, false);
	}

	public GameObject levelEasy;

	public GameObject levelNormal;

	public GameObject levelHard;
}
