using System;
using System.Collections;
using DG.Tweening;
using Hellmade.Sound;
using I2.Loc;
using Sirenix.OdinInspector;
using SkyGameKit;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
	private void Awake()
	{
		UIGameManager.current = this;
		this.skeletonWarningBoss = this.fxBossWarning.GetComponent<SkeletonGraphic>();
		this.LoadDataSetting();
	}

	private void OnEnable()
	{
		if (SgkSingleton<LevelManager>.Instance != null)
		{
			LevelManager instance = SgkSingleton<LevelManager>.Instance;
			instance.OnScoreChange = (Action<int>)Delegate.Combine(instance.OnScoreChange, new Action<int>(this.ScoreChange));
		}
		else
		{
			this.Delay(0.5f, delegate
			{
				LevelManager instance2 = SgkSingleton<LevelManager>.Instance;
				instance2.OnScoreChange = (Action<int>)Delegate.Combine(instance2.OnScoreChange, new Action<int>(this.ScoreChange));
			}, false);
		}
		this.sliderMusic.onValueChanged.AddListener(delegate(float A_1)
		{
			this.OnChangeValueSliderMusic();
		});
		this.sliderSound.onValueChanged.AddListener(delegate(float A_1)
		{
			this.OnChangeValueSliderSound();
		});
		this.sliderSensitivity.onValueChanged.AddListener(delegate(float A_1)
		{
			this.OnChangeValueSliderSensitivity();
		});
	}

	private void OnDisable()
	{
		if (SgkSingleton<LevelManager>.Instance != null)
		{
			LevelManager instance = SgkSingleton<LevelManager>.Instance;
			instance.OnScoreChange = (Action<int>)Delegate.Remove(instance.OnScoreChange, new Action<int>(this.ScoreChange));
		}
	}

	private void ScoreChange(int _score)
	{
		this.textScore.text = SgkSingleton<LevelManager>.Instance.Score.ToString("D7");
		GameContext.currentScore = SgkSingleton<LevelManager>.Instance.Score;
	}

	private void Start()
	{
		if (GameContext.currentModeLevel == GameContext.ModeLevel.Hard)
		{
			this.objPanelStageRed.SetActive(true);
		}
		this.CheckEndlessMode();
	}

	private void Update()
	{
		if (!GameContext.isMissionComplete && !GameContext.isMissionFail && !GameContext.isPlayerDie && !this.isPause)
		{
			this.timer += Time.deltaTime;
			if (this.showTimer)
			{
				this.SetTextTime();
			}
		}
	}

	private void SetTextTime()
	{
		this.textTimer.text = GameUtil.GetFormatTimeFrom(this.timer);
	}

	public void ShowPopupPause()
	{
		if (GameContext.isGameReady)
		{
			this.isPause = true;
			GameUtil.ShowPopup(this.popupPause, "PAUSE");
			Time.timeScale = 0f;
			EazySoundManager.GlobalMusicVolume = CacheGame.VolumeMusic / 3f;
			if (this._currentWave != this.saveCurrentWave)
			{
				this.saveCurrentWave = this._currentWave;
				switch (GameContext.currentModeGamePlay)
				{
				case GameContext.ModeGamePlay.Endless:
					this.textCurrentStage.text = string.Concat(new object[]
					{
						ScriptLocalization.endless,
						" - ",
						ScriptLocalization.wave,
						" ",
						this._currentWave
					});
					break;
				case GameContext.ModeGamePlay.Campaign:
					this.textCurrentStage.text = string.Concat(new object[]
					{
						ScriptLocalization.stage,
						" ",
						GameContext.currentLevel,
						" - ",
						ScriptLocalization.wave,
						" ",
						this._currentWave,
						"/",
						this.currentTotalWave
					});
					break;
				case GameContext.ModeGamePlay.Boss:
					this.textCurrentStage.text = string.Concat(new object[]
					{
						"BOSS ",
						GameContext.currentLevel,
						" - ",
						ScriptLocalization.wave,
						" ",
						this._currentWave,
						"/",
						this.currentTotalWave
					});
					break;
				}
			}
		}
	}

	public void HidePopupPause()
	{
		DOTween.PlayBackwards("PAUSE");
		this.StartCountDownToContinue();
	}

	private void HidePause()
	{
		Time.timeScale = 1f;
		this.isPause = false;
		this.popupPause.SetActive(false);
		this.SaveDataSetting();
	}

	public void HidePause2()
	{
		this.popupPause.SetActive(false);
		this.HideUI();
		this.HideButtonSkillPlane();
		EscapeManager.Current.RemoveAction(new Action(GameScreenManager.current.ContinueGame));
		this.SaveDataSetting();
	}

	private void HidePopup(GameObject obj, string IDTweening)
	{
		DOTween.PlayBackwards(IDTweening);
		base.StartCoroutine(this.DelayHidePopup(obj));
	}

	private IEnumerator DelayHidePopup(GameObject popup)
	{
		yield return new WaitForSeconds(0.15f);
		popup.SetActive(false);
		yield break;
	}

	public void SetTextPower()
	{
		this.textPower.text = ((GameContext.power != 10) ? (string.Empty + GameContext.power) : "Max");
	}

	public void ShowUI()
	{
		base.StartCoroutine(this.DelayShowUI());
	}

	private IEnumerator DelayShowUI()
	{
		yield return new WaitForSeconds(1f);
		DOTween.Restart("UI_GAME_SCREEN", true, -1f);
		if (!GameContext.isFirstGame && (NewTutorial.current.currentStepTutorial_UseSkillPlane > 4 || GameContext.maxLevelUnlocked > 1))
		{
			this.ShowButtonSkillPlane();
		}
		yield break;
	}

	public void ShowUI2()
	{
		DOTween.Restart("UI_GAME_SCREEN", true, -1f);
	}

	public void HideUI()
	{
		DOTween.PlayBackwards("UI_GAME_SCREEN");
	}

	public void ShowButtonSkillPlane()
	{
		DOTween.Restart("UI_GAME_SCREEN_BTN_SKILL_PLANE", true, -1f);
	}

	public void HideButtonSkillPlane()
	{
		DOTween.PlayBackwards("UI_GAME_SCREEN_BTN_SKILL_PLANE");
	}

	public void ShowUIPlayGame()
	{
		this.ShowUI2();
		this.ShowButtonSkillPlane();
	}

	public void ShowFXStartGame()
	{
		this.fxStart.SetActive(true);
	}

	public void ContinueGame()
	{
		this.HidePopupPause();
	}

	public void ExitGame()
	{
		if (GameContext.isTryPlane)
		{
			CameraManager.curret.ExitGameScreen(GameContext.sceneNameTryPlane);
			GameContext.currentLive = 0;
		}
		else
		{
			if (GameContext.isTutorial)
			{
				GameContext.currentLive = 0;
			}
			CameraManager.curret.ExitGameScreen("SelectLevel");
		}
	}

	private void StartCountDownToContinue()
	{
		base.StartCoroutine(this.CoroutineCountdownToContinue());
	}

	private IEnumerator CoroutineCountdownToContinue()
	{
		this.timeToContinue = 3;
		this.objCoundownToContinue.SetActive(true);
		this.textCountDown.text = string.Empty + this.timeToContinue;
		while (this.timeToContinue > 0)
		{
			this.textCountDown.text = string.Empty + this.timeToContinue;
			yield return new WaitForSecondsRealtime(1f);
			EazySoundManager.PlayUISound(AudioCache.UISound.tik_tok);
			this.timeToContinue--;
		}
		this.objCoundownToContinue.SetActive(false);
		EazySoundManager.GlobalMusicVolume = CacheGame.VolumeMusic;
		this.HidePause();
		yield break;
	}

	public void ShowFXVictory()
	{
		this.fxVictory.SetActive(true);
		GameScreenManager.current.StopMusic();
		EazySoundManager.PlaySound(AudioCache.Sound.win);
		this.HideUI();
		if (!GameContext.isPassCurrentLevel)
		{
			int num = (int)this.timer;
			SpaceForceFirebaseLogger.LogTimeVictoryModeCampaign(GameContext.currentModeLevel, num);
			UnityEngine.Debug.Log(num);
		}
	}

	public void ShowFxDefeat()
	{
		this.fxDefeat.SetActive(true);
		GameScreenManager.current.StopMusic();
		EazySoundManager.PlaySound(AudioCache.Sound.gameover);
		this.HideUI();
		int num = (int)this.timer;
		switch (GameContext.currentModeGamePlay)
		{
		case GameContext.ModeGamePlay.Endless:
			SpaceForceFirebaseLogger.LogTimePlayEndlessMode(num, GameContext.currentWaveEndlessMode);
			break;
		case GameContext.ModeGamePlay.Campaign:
			if (!GameContext.isPassCurrentLevel)
			{
				SpaceForceFirebaseLogger.LogTimeDefeatModeCampaign(GameContext.currentModeLevel, num, SgkSingleton<LevelManager>.Instance.CurrentSequenceWaveIndex);
				LogManager.LogLevel(GameContext.currentLevel, GameContext.maxLevelUnlocked, (LevelDifficulty)GameContext.currentModeLevel, num, PassLevelStatus.Fail, "Wave_" + this._currentWave);
			}
			else
			{
				LogManager.LogLevel(GameContext.currentLevel, GameContext.maxLevelUnlocked, (LevelDifficulty)GameContext.currentModeLevel, num, PassLevelStatus.FailAfterPassed, "Wave_" + this._currentWave);
			}
			break;
		}
	}

	public void ShowNextWave(int currentWave, int totalWave)
	{
		this.currentTotalWave = totalWave;
		this._currentWave = currentWave;
		GameContext.numberWavePassInstage = currentWave - 1;
		this.currentWaveIngame = ScriptLocalization.wave + " " + currentWave;
		this.objWay.SetActive(true);
		this.textWay.text = string.Concat(new object[]
		{
			ScriptLocalization.wave,
			" ",
			currentWave,
			"/",
			totalWave
		});
		this.textWaveOnPanelRevive.text = string.Concat(new object[]
		{
			ScriptLocalization.wave,
			" ",
			currentWave,
			"/",
			totalWave
		});
		DOTween.Restart("NEXT_WAVE", true, -1f);
		base.StartCoroutine(this.DelayHideNextWave());
	}

	public void ShowNextWave(int currentWave)
	{
		this._currentWave = currentWave;
		this.objWay.SetActive(true);
		this.textWay.text = ScriptLocalization.wave + " " + currentWave;
		this.textWaveOnPanelRevive.text = ScriptLocalization.wave + " " + currentWave;
		DOTween.Restart("NEXT_WAVE", true, -1f);
		DOTween.Play("NEXT_WAVE");
		base.StartCoroutine(this.DelayHideNextWave());
		GameContext.currentWaveEndlessMode = currentWave;
	}

	private IEnumerator DelayHideNextWave()
	{
		yield return new WaitForSeconds(2f);
		DOTween.PlayBackwards("NEXT_WAVE");
		yield return new WaitForSeconds(1f);
		this.objWay.SetActive(false);
		yield break;
	}

	public void ShowFxBossWarning()
	{
		this.fxBossWarning.SetActive(true);
		this.skeletonWarningBoss.AnimationState.SetAnimation(0, "animation", false);
		EazySoundManager.PlaySound(AudioCache.Sound.warning_boss);
		base.StartCoroutine(this.DelayHideFxBossWarning());
	}

	private IEnumerator DelayHideFxBossWarning()
	{
		yield return new WaitForSeconds(4f);
		this.fxBossWarning.SetActive(false);
		yield break;
	}

	private void OnApplicationPause(bool pause)
	{
	}

	public void UpDateNumberCandy()
	{
		this.numberCandy++;
		this.textNumberCandyEventNoel.text = string.Empty + this.numberCandy;
		GameContext.numberCandyEarnedInStage = this.numberCandy;
	}

	private void OnChangeValueSliderMusic()
	{
		float value = this.sliderMusic.value;
		EazySoundManager.GlobalMusicVolume = value;
		GameContext.volumeMusic = value;
		this.isChangeDataSetting = true;
	}

	private void OnChangeValueSliderSound()
	{
		float value = this.sliderSound.value;
		EazySoundManager.GlobalSoundsVolume = value;
		EazySoundManager.GlobalUISoundsVolume = value;
		GameContext.volumeSound = value;
		this.isChangeDataSetting = true;
	}

	private void OnChangeValueSliderSensitivity()
	{
		int num = (int)this.sliderSensitivity.value;
		GameContext.sensitivity = 1f + (float)num * 0.25f;
		UnityEngine.Debug.Log("sensitivity " + GameContext.sensitivity);
		this.isChangeDataSetting = true;
	}

	private void SaveDataSetting()
	{
		if (this.isChangeDataSetting)
		{
			CacheGame.VolumeMusic = GameContext.volumeMusic;
			CacheGame.VolumeSound = GameContext.volumeSound;
			CacheGame.Sensitivy = GameContext.sensitivity;
			this.isChangeDataSetting = false;
		}
	}

	private void LoadDataSetting()
	{
		this.sliderMusic.value = GameContext.volumeMusic;
		this.sliderSound.value = GameContext.volumeSound;
		this.sliderSensitivity.value = (GameContext.sensitivity - 1f) / 0.25f;
	}

	private void CheckEndlessMode()
	{
		if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Endless)
		{
			this.objStar.SetActive(true);
			this.objTimer.SetActive(true);
			GameContext.currentBlueStarCollectInGame = 0;
			this.showTimer = true;
		}
	}

	public void UpdateBlueStar()
	{
		GameContext.currentBlueStarCollectInGame++;
		this.textNumberStar.text = string.Empty + GameContext.currentBlueStarCollectInGame;
	}

	public static UIGameManager current;

	public GameObject popupPause;

	public Text textPower;

	public Text textScore;

	public GameObject objCoundownToContinue;

	public Text textCountDown;

	public GameObject fxStart;

	public GameObject fxVictory;

	public GameObject fxDefeat;

	public GameObject fxBossWarning;

	public GameObject objWay;

	public Text textWay;

	public Text textWaveOnPanelRevive;

	public GameObject objPanelStageRed;

	[Header("Current Wave")]
	public string currentWaveIngame = string.Empty;

	public bool isSceneLevelNoel;

	[ShowIf("isSceneLevelNoel", true)]
	public TextMeshProUGUI textNumberCandyEventNoel;

	[Header("Setting")]
	public Slider sliderMusic;

	public Slider sliderSound;

	public Slider sliderSensitivity;

	public Text textCurrentStage;

	[Header("Endless Mode")]
	public GameObject objStar;

	public GameObject objTimer;

	public Text textNumberStar;

	public Text textTimer;

	private SkeletonGraphic skeletonWarningBoss;

	[HideInInspector]
	public float timer;

	[HideInInspector]
	public bool isPause;

	private int saveCurrentWave = -1;

	private int timeToContinue = 3;

	private int currentTotalWave;

	private int _currentWave;

	private int numberCandy;

	private bool isChangeDataSetting;

	private bool showTimer;
}
