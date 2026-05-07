using System;
using UnityEngine;
using UnityEngine.UI;

public class SetBackgroundInGame : MonoBehaviour
{
	private void Awake()
	{
		if (GameContext.currentModeGamePlay != GameContext.ModeGamePlay.Endless)
		{
			this.SetBackground();
		}
		else
		{
			this.SetBackgroundEndlessMode();
		}
	}

	private void SetBackground()
	{
		switch (GameContext.currentLevel)
		{
		case 1:
			this.arrBG[0].SetActive(true);
			break;
		case 2:
			this.arrBG[1].SetActive(true);
			break;
		case 3:
			this.arrBG[2].SetActive(true);
			break;
		case 4:
			this.arrBG[3].SetActive(true);
			break;
		case 5:
			this.arrBG[4].SetActive(true);
			break;
		default:
		{
			int num = this.arrBG.Length;
			if (GameContext.currentLevel < num * this.levelChangeBackground)
			{
				for (int i = 0; i < num; i++)
				{
					int num2 = (i + 1) * 3;
					if (num2 >= GameContext.currentLevel)
					{
						this.arrBG[i].SetActive(true);
						break;
					}
				}
			}
			else
			{
				int num3 = UnityEngine.Random.Range(0, num);
				if (num3 < num)
				{
					this.arrBG[num3].SetActive(true);
				}
			}
			break;
		}
		}
	}

	private void SetBackgroundEndlessMode()
	{
		this.arrBGEndlessMode[GameContext.currentUniverse].SetActive(true);
	}

	public GameObject[] arrBG;

	public GameObject[] arrBGEndlessMode;

	private Image background;

	public int levelChangeBackground = 3;
}
