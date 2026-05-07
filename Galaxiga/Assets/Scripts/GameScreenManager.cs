using System;
using System.Collections;
using DG.Tweening;
using FalconSDK;
using Hellmade.Sound;
using I2.Loc;
using UnityEngine;

public class GameScreenManager : MonoBehaviour
{
	public Transform CurrentTransformPlayer
	{
		get
		{
			return this.planeIngameManager.CurrentTransformPlayer;
		}
	}

	private void Awake()
	{
		GameScreenManager.current = this;
		this.ABTestingIncreaseHP4FirstLevel();
	}

	private void Start()
	{
		FSDK.CrossPromotion.CanShowFixedPromo(false);
		GameContext.isMissionComplete = false;
		this.SetMusic();
	}

	private void ABTestingIncreaseHP4FirstLevel()
	{
		if (GameContext.currentLevel < 5 && GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Campaign && !GameContext.isFirstGame)
		{
			GameContext.currentLive++;
		}
	}

	private void Update()
	{
	}

	private void SetMusic()
	{
		if (GameContext.currentModeLevel == GameContext.ModeLevel.Hard)
		{
			int num = UnityEngine.Random.Range(0, 2);
			if (num == 0)
			{
				this.currentMusicID = EazySoundManager.PlayMusic(AudioCache.Music.hard_bg1, 1f, true, false);
			}
			else if (num == 1)
			{
				this.currentMusicID = EazySoundManager.PlayMusic(AudioCache.Music.hard_bg2, 1f, true, false);
			}
		}
		else
		{
			int num2 = UnityEngine.Random.Range(0, 3);
			if (num2 == 0)
			{
				this.currentMusicID = EazySoundManager.PlayMusic(AudioCache.Music.normal_bg1, 1f, true, false);
			}
			else if (num2 == 1)
			{
				this.currentMusicID = EazySoundManager.PlayMusic(AudioCache.Music.normal_bg2, 1f, true, false);
			}
			else if (num2 == 2)
			{
				this.currentMusicID = EazySoundManager.PlayMusic(AudioCache.Music.normal_bg3, 1f, true, false);
			}
		}
		this.backgroundMusicAudio = EazySoundManager.GetAudio(this.currentMusicID);
		this.volumeMusic = CacheGame.VolumeMusic;
	}

	public void StopMusic()
	{
		this.backgroundMusicAudio.Stop();
	}

	public void PauseMusic()
	{
		EazySoundManager.GlobalMusicVolume = this.volumeMusic / 3f;
	}

	public void ResumeMusic()
	{
		EazySoundManager.GlobalMusicVolume = this.volumeMusic;
	}

	public void PowerUpPlane()
	{
	}

	public void PowerDownPlane()
	{
	}

	public void StopAllShot()
	{
		this.droneIngameManager.StopShot();
		this.planeIngameManager.PlaneStopShot();
	}

	public void StartAllShot()
	{
		this.planeIngameManager.PlaneStartShot();
		this.droneIngameManager.StartShot();
	}

	public void HideDrone()
	{
		this.droneIngameManager.HideDrone();
	}

	public void ShowDrone()
	{
		this.droneIngameManager.ShowDrone();
	}

	public void PauseGame()
	{
		EscapeManager.Current.AddAction(new Action(this.ContinueGame));
		this.StopAllShot();
		this.uIGameManager.ShowPopupPause();
	}

	public void StartGame()
	{
		this.StartAllShot();
		this.ShowFXStartGame();
	}

	public void ContinueGame()
	{
		this.StartAllShot();
		this.uIGameManager.ContinueGame();
		EscapeManager.Current.RemoveAction(new Action(this.ContinueGame));
	}

	public void ShowFXStartGame()
	{
		this.uIGameManager.ShowFXStartGame();
	}

	public void PlayerDie()
	{
		this.StopAllShot();
		this.HideDrone();
		base.StartCoroutine(this.DelayShowPanelRevive());
		this.uIGameManager.HideButtonSkillPlane();
	}

	private IEnumerator DelayShowPanelRevive()
	{
		yield return new WaitForSeconds(1f);
		BackupPlaneManager.current.ShowPanelRevive();
		yield break;
	}

	public void MissionComplete()
	{
		this.StopAllShot();
		this.uIGameManager.ShowFXVictory();
		this.SetPassLevel();
		this.planeIngameManager.SetPlayerImortal();
		this.StopMusic();
		EazySoundManager.PlaySound(AudioCache.Sound.win);
		base.StartCoroutine(this.DelayExitGame());
	}

	private void SetPassLevel()
	{
		if (!GameContext.isTryPlane)
		{
			GameContext.isMissionComplete = true;
			int duration = (int)this.uIGameManager.timer;
			if (GameContext.currentLevel > 0)
			{
				GameContext.ModeGamePlay currentModeGamePlay = GameContext.currentModeGamePlay;
				if (currentModeGamePlay != GameContext.ModeGamePlay.Campaign)
				{
					if (currentModeGamePlay == GameContext.ModeGamePlay.Boss)
					{
						if (GameContext.currentModeLevel == GameContext.ModeLevel.Easy && GameContext.currentLevel >= GameContext.maxLevelBosssUnlocked)
						{
							CacheGame.SetMaxLevelBoss(GameContext.currentLevel + 1);
						}
						CacheGame.SetPassLevelBoss(GameContext.currentModeLevel, GameContext.currentLevel, GameContext.currentScore);
						SpaceForceFirebaseLogger.LevelCompleteExtra(GameContext.currentModeGamePlay, GameContext.currentModeLevel, GameContext.currentLevel);
					}
				}
				else
				{
					GameContext.ModeLevel currentModeLevel = GameContext.currentModeLevel;
					if (currentModeLevel != GameContext.ModeLevel.Easy)
					{
						if (currentModeLevel != GameContext.ModeLevel.Normal)
						{
							if (currentModeLevel == GameContext.ModeLevel.Hard)
							{
								SaveDataQuest.SetProcessQuest(DailyQuestManager.Quest.hunter_hard, 1);
								if (!GameContext.isPassCurrentLevel)
								{
									LogManager.LogLevel(GameContext.currentLevel, GameContext.maxLevelHardPass, LevelDifficulty.Hard, duration, PassLevelStatus.Pass, string.Empty);
								}
								else
								{
									LogManager.LogLevel(GameContext.currentLevel, GameContext.maxLevelUnlocked, LevelDifficulty.Hard, duration, PassLevelStatus.Repass, string.Empty);
								}
							}
						}
						else
						{
							SaveDataQuest.SetProcessQuest(DailyQuestManager.Quest.hunter_normal, 1);
							if (!GameContext.isPassCurrentLevel)
							{
								LogManager.LogLevel(GameContext.currentLevel, GameContext.maxLevelNormalPass, LevelDifficulty.Normal, duration, PassLevelStatus.Pass, string.Empty);
							}
							else
							{
								LogManager.LogLevel(GameContext.currentLevel, GameContext.maxLevelUnlocked, LevelDifficulty.Normal, duration, PassLevelStatus.Repass, string.Empty);
							}
						}
					}
					else
					{
						if (GameContext.currentLevel >= GameContext.maxLevelUnlocked)
						{
							CacheGame.SetMaxLevel(GameContext.currentLevel + 1);
							if (GameContext.currentLevel == 5)
							{
								Notification.Current.ShowNotification(ScriptLocalization.notify_boss_fight_mode_was_unlocked);
								GameContext.needShowForcusBtnBoss = true;
							}
						}
						if (!GameContext.isPassCurrentLevel)
						{
							LogManager.LogLevel(GameContext.currentLevel, GameContext.maxLevelUnlocked, LevelDifficulty.Easy, duration, PassLevelStatus.Pass, string.Empty);
						}
						else
						{
							LogManager.LogLevel(GameContext.currentLevel, GameContext.maxLevelUnlocked, LevelDifficulty.Easy, duration, PassLevelStatus.Repass, string.Empty);
						}
						SaveDataQuest.SetProcessQuest(DailyQuestManager.Quest.hunter_easy, 1);
					}
					if (!GameContext.isPassCurrentLevel)
					{
						SpaceForceFirebaseLogger.LevelComplete(GameContext.currentModeLevel, GameContext.currentLevel);
					}
					CacheGame.SetPassLevel(GameContext.currentModeLevel, GameContext.currentLevel, GameContext.currentScore);
				}
			}
		}
	}

	private IEnumerator DelayExitGame()
	{
		yield return new WaitForSeconds(1.5f);
		this.planeIngameManager.MissionComplete(new TweenCallback(this.ExitGame));
		yield break;
	}

	private void ExitGame()
	{
		this.uIGameManager.ExitGame();
	}

	public void PauseExitGame()
	{
		if (GameContext.isTryPlane)
		{
			CameraManager.curret.ExitGameScreen("Home");
			GameContext.currentLive = 0;
		}
		else
		{
			this.planeIngameManager.SetPlayerImortal();
			this.uIGameManager.HidePause2();
			BackupPlaneManager.current.GameOver();
		}
	}

	public static GameScreenManager current;

	public DroneIngameManager droneIngameManager;

	public PlaneIngameManager planeIngameManager;

	public UIGameManager uIGameManager;

	private int currentMusicID;

	private Audio backgroundMusicAudio;

	private float volumeMusic;
}
