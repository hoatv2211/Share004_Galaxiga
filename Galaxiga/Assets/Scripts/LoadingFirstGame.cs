using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingFirstGame : MonoBehaviour
{
	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		DOTween.PlayBackwards("BAR_TOP");
		if (GameContext.firstInstallGame || GameContext.maxLevelUnlocked == 1)
		{
			GameContext.firstInstallGame = false;
			this.LoadSceneTutorial();
		}
		else
		{
			if (GameContext.maxLevelUnlocked > 3)
			{
				SceneContext.sceneName = "Home";
			}
			else
			{
				SceneContext.sceneName = "SelectLevel";
			}
			SceneManager.LoadScene("Loading");
		}
		yield break;
	}

	private void LoadSceneTutorial()
	{
		SceneContext.sceneName = "Level1";
		GameContext.currentLevel = 1;
		GameContext.currentLive = 100;
		GameContext.isTutorial = true;
		GameContext.isFirstGame = true;
		GameContext.currentNumberSkillPlane = 1;
		GameContext.rewardCurrentLevelPlay = DataGame.Current.RewardLevelCampain(1, GameContext.ModeLevel.Easy);
		SceneManager.LoadScene("Loading");
	}

	public static bool IS_RECEIVE_MESSAGE_LOAD_GAME;
}
