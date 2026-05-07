using System;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour
{
	public static SelectLevelManager current
	{
		get
		{
			if (SelectLevelManager._instance == null)
			{
				SelectLevelManager._instance = UnityEngine.Object.FindObjectOfType<SelectLevelManager>();
			}
			return SelectLevelManager._instance;
		}
	}

	private void Awake()
	{
		this.actionsBtnBack = new List<Action>();
		this.CheckUnlockBossMode();
	}

	private void Start()
	{
		if (Time.timeScale < 1f)
		{
			Time.timeScale = 1f;
		}
		this.transitionsFX.TransitionEnter();
		ProCamera2DTransitionsFX proCamera2DTransitionsFX = this.transitionsFX;
		proCamera2DTransitionsFX.OnTransitionExitEnded = (Action)Delegate.Combine(proCamera2DTransitionsFX.OnTransitionExitEnded, new Action(this.TransitionExitEnded));
		GameContext.Reset();
		IAPGameManager.Current.SetTextPrice();
	}

	public void LogVerticalNormalizedPosition()
	{
		this.scrollRectDecor.verticalNormalizedPosition = this.scrollRectLevel.verticalNormalizedPosition / 2f;
	}

	private void TransitionExitEnded()
	{
		SceneManager.LoadScene("Loading");
	}

	public void NextToLoadingScene()
	{
		this.transitionsFX.TransitionExit();
	}

	public void BackToHome()
	{
		SceneContext.sceneName = "Home";
		LoadingScenes.Current.LoadLevel("Home");
	}

	public void PlayPVP()
	{
		Notification.Current.ShowNotification(ScriptLocalization.notify_coming_soon);
	}

	public void PlayBossFight()
	{
		Notification.Current.ShowNotification(ScriptLocalization.notify_coming_soon);
	}

	private void CheckUnlockBossMode()
	{
		if (GameContext.maxLevelUnlocked < 6)
		{
			this.objLockBossMode.SetActive(true);
		}
	}

	public void AddAction(Action actionT)
	{
		if (!this.actionsBtnBack.Contains(actionT))
		{
			this.actionsBtnBack.Add(actionT);
			this.numberAction++;
		}
	}

	public void RemoveAction(Action actionT)
	{
		if (this.actionsBtnBack.Contains(actionT))
		{
			this.actionsBtnBack.Remove(actionT);
			this.numberAction--;
		}
	}

	public void Back()
	{
		if (this.numberAction > 0)
		{
			this.actionsBtnBack[this.numberAction - 1]();
		}
		else
		{
			this.BackToHome();
		}
	}

	public void SetPositionFxLevel(Transform parrent)
	{
		this.fxLevel.parent = parrent;
		this.fxLevel.localPosition = new Vector3(0f, 33f, 0f);
		this.fxLevel.SetAsFirstSibling();
		this.objArrow.parent = parrent;
		this.objArrow.localPosition = new Vector3(0f, 111f, 0f);
	}

	public void ShowPopupCollectStar()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePopupFortification));
		this.popupFortification.SetActive(true);
		DOTween.Restart("Fortification", true, -1f);
	}

	public void HidePopupFortification()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupFortification));
		DOTween.PlayBackwards("Fortification");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.popupFortification.SetActive(false);
		}));
	}

	public void CollectStar()
	{
		int num = SaveDataLevel.MinLevelNotMaxStar();
		this.arrViewStagesCampaign[GameContext.currentLevel - 1].StopDotween();
		this.arrViewStagesCampaign[num - 1].SetForcusLevel();
		this.SnapToLevelCampaign(this.arrViewStagesCampaign[num - 1]._element);
		this.HidePopupFortification();
	}

	public void SnapToLevelCampaign(RectTransform target)
	{
		Canvas.ForceUpdateCanvases();
		this.contentPanelLevelCampaign.anchoredPosition = this.scrollRectLevel.transform.InverseTransformPoint(this.contentPanelLevelCampaign.position) - this.scrollRectLevel.transform.InverseTransformPoint(target.position);
		this.contentPanelLevelCampaign.anchoredPosition = new Vector2(-350f, this.contentPanelLevelCampaign.anchoredPosition.y - 200f);
		this.LogVerticalNormalizedPosition();
	}

	public void SnapToLevelBoss(RectTransform target)
	{
		Canvas.ForceUpdateCanvases();
		this.contentPanelLevelBoss.anchoredPosition = this.scrollRectLevelBoss.transform.InverseTransformPoint(this.contentPanelLevelBoss.position) - this.scrollRectLevelBoss.transform.InverseTransformPoint(target.position);
		this.contentPanelLevelBoss.anchoredPosition = new Vector2(-350f, this.contentPanelLevelBoss.anchoredPosition.y - 200f);
		this.LogVerticalNormalizedPosition();
	}

	public void PlayGame()
	{
		switch (GameContext.currentModeGamePlay)
		{
		case GameContext.ModeGamePlay.Campaign:
			if (GameContext.currentLevel <= GameContext.maxLevelUnlocked && RewardStageManager.current.isUnlockCurrentLevel)
			{
				SceneContext.sceneName = "Level" + GameContext.currentLevel;
				if (GameContext.isPurchasePremiumPack)
				{
					this.PlayNow();
				}
				else if (GameContext.currentEnergyLevel <= GameContext.totalEnergy)
				{
					CacheGame.MinusEnergy(GameContext.currentEnergyLevel);
					ShopManager.Instance.UpdateTextEnergy();
					ShopEnergy.current.CheckEnergy();
					this.PlayNow();
				}
				else
				{
					Notification.Current.ShowNotification(ScriptLocalization.notify_you_need_buy_more_energy);
					ShopEnergy.current.ShowPanelShopEnergy();
				}
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.complete_previous_stages);
			}
			break;
		case GameContext.ModeGamePlay.Boss:
			if (!this.CanPlayCurrentBoss())
			{
				int num = GameContext.currentLevel * 5;
				Notification.Current.ShowNotification(string.Format(ScriptLocalization.notify_you_need_pass_stage_in_mode_campaign, num));
				return;
			}
			if (GameContext.currentLevel <= GameContext.maxLevelBosssUnlocked)
			{
				if (RewardStageManager.current.isUnlockCurrentLevel)
				{
					if (NewTutorial.current.currentStepTutorial_OpenBossMode == 7)
					{
						NewTutorial.current.OpenBossMode_Step7();
					}
					SceneContext.sceneName = "LevelBoss" + GameContext.currentLevel;
					if (GameContext.isPurchasePremiumPack)
					{
						this.PlayNow();
					}
					else if (GameContext.currentEnergyLevel <= GameContext.totalEnergy)
					{
						CacheGame.MinusEnergy(GameContext.currentEnergyLevel);
						ShopManager.Instance.UpdateTextEnergy();
						ShopEnergy.current.CheckEnergy();
						this.PlayNow();
					}
					else
					{
						Notification.Current.ShowNotification(ScriptLocalization.notify_you_need_buy_more_energy);
						ShopEnergy.current.ShowPanelShopEnergy();
					}
				}
				else
				{
					Notification.Current.ShowNotification(ScriptLocalization.complete_previous_stages);
				}
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.complete_previous_stages);
			}
			break;
		}
	}

	private void PlayNow()
	{
		NewTutorial.current.BuyItems_Step9();
		if (GameContext.isBonusLife)
		{
			GameContext.currentLive++;
			GameContext.totalItemPowerUpHeart--;
			CacheGame.NumberItemPowerHeart = GameContext.totalItemPowerUpHeart;
		}
		if (GameContext.isBonusShield)
		{
			GameContext.totalItemPowerShield--;
			CacheGame.NumberItemPowerShield = GameContext.totalItemPowerShield;
		}
		if (GameContext.isBonusPower)
		{
			int power = GameContext.power;
			if (power != 5)
			{
				if (power == 10)
				{
					CacheGame.NumberItemPowerUpBullet10 -= GameContext.totalItemPowerUpBullet10;
					GameContext.totalItemPowerUpBullet10--;
				}
			}
			else
			{
				GameContext.totalItemPowerUpBullet5--;
				CacheGame.NumberItemPowerUpBullet5 = GameContext.totalItemPowerUpBullet5;
			}
		}
		GameContext.ModeGamePlay currentModeGamePlay = GameContext.currentModeGamePlay;
		if (currentModeGamePlay != GameContext.ModeGamePlay.Campaign)
		{
			if (currentModeGamePlay == GameContext.ModeGamePlay.Boss)
			{
				SpaceForceFirebaseLogger.ExtraGameStart(GameContext.currentModeGamePlay, GameContext.currentModeLevel, GameContext.currentLevel);
				RewardStageManager.current.SaveDataRewardCurrentLevel();
			}
		}
		else
		{
			SpaceForceFirebaseLogger.LevelStart(GameContext.currentModeLevel, GameContext.currentLevel);
			RewardStageManager.current.SaveDataRewardCurrentLevel();
		}
		try
		{
			if (GameContext.audioMainMenu != null)
			{
				GameContext.audioMainMenu.Pause();
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
		}
		this.NextToLoadingScene();
	}

	private bool CanPlayCurrentBoss()
	{
		return GameContext.maxLevelUnlocked > GameContext.currentLevel * 5;
	}

	private static SelectLevelManager _instance;

	public ProCamera2DTransitionsFX transitionsFX;

	public ScrollRect scrollRectLevel;

	public ScrollRect scrollRectLevelBoss;

	public ScrollRect scrollRectDecor;

	public GameObject objLockBossMode;

	public Sprite sprBtnLevelUnlock;

	public Sprite sprBtnLock;

	public Sprite sprBtnLevelBosslUnlock;

	public Sprite sprBtnLevelBossLock;

	public List<Action> actionsBtnBack;

	public Transform fxLevel;

	public Transform objArrow;

	public RectTransform contentPanelLevelCampaign;

	public RectTransform contentPanelLevelBoss;

	public ShopItemPowerUp shopItemPowerUpCampaign;

	[Header("Foritifiacation")]
	public GameObject popupFortification;

	public Text textNumberStar;

	public ViewStage[] arrViewStagesCampaign;

	private int numberAction;
}
