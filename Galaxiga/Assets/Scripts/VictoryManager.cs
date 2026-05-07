using System;
using System.Collections;
using DG.Tweening;
using Hellmade.Sound;
using I2.Loc;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
	private void Awake()
	{
		VictoryManager.current = this;
		if (GameContext.currentModeGamePlay != GameContext.ModeGamePlay.Endless && GameContext.isMissionComplete)
		{
			this.CheckUpgrade();
			this.ShowPanelVictory();
			GameContext.ResetLevel();
		}
	}

	private void Start()
	{
		this.btnX2Coins.onClick.AddListener(new UnityAction(this.WatchVideoToX2Coin));
	}

	public void SetXCoins(int n)
	{
		this.nValue = n;
		this.textBtnX2Coin.text = "x" + n + " Coins";
	}

	private void SetPassLevel()
	{
		if (GameContext.currentLevel > 0)
		{
			GameContext.ModeGamePlay currentModeGamePlay = GameContext.currentModeGamePlay;
			if (currentModeGamePlay != GameContext.ModeGamePlay.Campaign)
			{
				if (currentModeGamePlay == GameContext.ModeGamePlay.Boss)
				{
					CacheGame.SetPassLevelBoss(GameContext.currentModeLevel, GameContext.currentLevel, GameContext.currentScore);
					SpaceForceFirebaseLogger.LevelCompleteExtra(GameContext.currentModeGamePlay, GameContext.currentModeLevel, GameContext.currentLevel);
				}
			}
			else
			{
				if (!GameContext.isPassCurrentLevel)
				{
					SpaceForceFirebaseLogger.LevelComplete(GameContext.currentModeLevel, GameContext.currentLevel);
				}
				CacheGame.SetPassLevel(GameContext.currentModeLevel, GameContext.currentLevel, GameContext.currentScore);
			}
		}
	}

	public void ShowPanelVictory()
	{
		if ((GameContext.currentLevel == 3 && NewTutorial.current.currentStepTutorial_UpgradePlane == 0) || (GameContext.currentLevel == 4 && NewTutorial.current.currentStepTutorial_EquipDrone == 0) || (GameContext.currentLevel == 5 && NewTutorial.current.currentStepTutorial_OpenBox == 0) || (GameContext.currentLevel == 10 && NewTutorial.current.currentStepTutorial_EvovlePlane == 0))
		{
			NewTutorial.current.ShowForcusBtnTakeGift();
			this.btnBack.GetComponent<Button>().enabled = false;
		}
		else if (GameContext.maxLevelUnlocked < 12)
		{
			NewTutorial.current.ShowForcusBtnNextStage();
			this.btnBack.GetComponent<Button>().enabled = true;
		}
		if (GameContext.maxLevelUnlocked < 4)
		{
			this.btnX2Coins.gameObject.SetActive(false);
		}
		this.textScore.text = string.Empty + GameContext.currentScore;
		int numberStarCurrentLvl = GameContext.numberStarCurrentLvl;
		if (numberStarCurrentLvl != 1)
		{
			if (numberStarCurrentLvl != 2)
			{
				if (numberStarCurrentLvl == 3)
				{
					this.starEasy.SetActive(true);
					this.starNormal.SetActive(true);
					this.starHard.SetActive(true);
				}
			}
			else
			{
				this.starEasy.SetActive(true);
				this.starNormal.SetActive(true);
			}
		}
		else
		{
			this.starEasy.SetActive(true);
		}
		if (GameContext.isPassCurrentLevel)
		{
			this.textRewardCoin.text = "+" + GameContext.maxCoinEarnedIn1Stage;
			CacheGame.AddCoins(GameContext.maxCoinEarnedIn1Stage);
			this.textX2Coin.text = "+" + GameContext.maxCoinEarnedIn1Stage;
			this.coinEarned = GameContext.maxCoinEarnedIn1Stage;
		}
		else
		{
			int num = GameContext.rewardCurrentLevelPlay.numberCoins + GameContext.maxCoinEarnedIn1Stage;
			this.textRewardCoin.text = "+" + num;
			this.textX2Coin.text = "+" + num;
			this.coinEarned = num;
			CacheGame.AddCoins(num);
			this.SpawnReward();
		}
		GameContext.ModeGamePlay currentModeGamePlay = GameContext.currentModeGamePlay;
		if (currentModeGamePlay != GameContext.ModeGamePlay.Campaign)
		{
			if (currentModeGamePlay == GameContext.ModeGamePlay.Boss)
			{
				this.textStage.text = "BOSS " + GameContext.currentLevel;
				BossModeManager.current.VictoryCheckAnimBtnReward();
				CurrencyLog.LogGoldIn(this.coinEarned, CurrencyLog.In.Victory_Boss, string.Concat(new object[]
				{
					"Boss_",
					GameContext.currentLevel,
					"_",
					GameContext.currentModeLevel.ToString()
				}));
			}
		}
		else
		{
			this.textStage.text = ScriptLocalization.stage + " " + GameContext.currentLevel;
			CurrencyLog.LogGoldIn(this.coinEarned, CurrencyLog.In.Victory_Campaign, string.Concat(new object[]
			{
				"Campaign_",
				GameContext.currentLevel,
				"_",
				GameContext.currentModeLevel.ToString()
			}));
		}
		if (GameContext.numberStarCurrentLvl != 3)
		{
			base.StartCoroutine(GameContext.Delay(0.2f, delegate
			{
				GameContext.ModeLevel currentModeLevel = GameContext.currentModeLevel;
				if (currentModeLevel != GameContext.ModeLevel.Easy)
				{
					if (currentModeLevel != GameContext.ModeLevel.Normal)
					{
						if (currentModeLevel == GameContext.ModeLevel.Hard)
						{
							this.starHard.SetActive(true);
						}
					}
					else
					{
						this.starNormal.SetActive(true);
					}
				}
				else
				{
					this.starEasy.SetActive(true);
				}
				EazySoundManager.PlayUISound(AudioCache.UISound.spawn_star);
			}));
		}
		EscapeManager.Current.AddAction(new Action(this.HidePanelVictory));
		this.ShowPanel();
	}

	private void ShowPanel()
	{
		this.panelVictory.SetActive(true);
		this.animBtnX2Coins.DOPlay();
		base.StartCoroutine(this.StartFxVictory());
	}

	public void HidePanelVictory()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelVictory));
		this.panelVictory.SetActive(false);
		//AccountManager.Instance.UpdateToServer("victory_stage_" + GameContext.currentLevel);
	}

	private void SpawnReward()
	{
		string unlockPlane = GameContext.rewardCurrentLevelPlay.unlockPlane;
		if (unlockPlane != "None" || unlockPlane == "none")
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.rewardUnlockPlane, this.transformReward.localPosition, Quaternion.identity);
			RewardUnlockPlane component = gameObject.GetComponent<RewardUnlockPlane>();
			gameObject.transform.parent = this.transformReward;
			gameObject.transform.localScale = Vector3.one;
			component.planeName = unlockPlane;
			component.SetView();
			switch (unlockPlane)
			{
			case "Bata FD 01":
				this.ShowPopupUnlockPlane(GameContext.Plane.BataFD01);
				break;
			case "Sky Wraith":
				this.ShowPopupUnlockPlane(GameContext.Plane.SkyWraith);
				break;
			case "Fury Of Ares":
				this.ShowPopupUnlockPlane(GameContext.Plane.FuryOfAres);
				break;
			case "Greataxe":
				this.ShowPopupUnlockPlane(GameContext.Plane.Greataxe);
				break;
			case "Twilight X":
				this.ShowPopupUnlockPlane(GameContext.Plane.TwilightX);
				break;
			case "SS Lightning":
				this.ShowPopupUnlockPlane(GameContext.Plane.TwilightX);
				break;
			case "Warlock":
				this.ShowPopupUnlockPlane(GameContext.Plane.TwilightX);
				break;
			}
		}
		if (GameContext.rewardCurrentLevelPlay.numberCardPlane > 0)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.rewardCardPlane, this.transformReward.localPosition, Quaternion.identity);
			RewardCardPlane component2 = gameObject2.GetComponent<RewardCardPlane>();
			gameObject2.transform.parent = this.transformReward;
			gameObject2.transform.localScale = Vector3.one;
			component2.SetData(GameContext.rewardCurrentLevelPlay.cardPlane, GameContext.rewardCurrentLevelPlay.numberCardPlane);
			component2.SetView();
		}
		if (GameContext.rewardCurrentLevelPlay.numberCardDrone > 0)
		{
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.rewardCardDrone, this.transformReward.localPosition, Quaternion.identity);
			RewardCardDrone component3 = gameObject3.GetComponent<RewardCardDrone>();
			gameObject3.transform.parent = this.transformReward;
			gameObject3.transform.localScale = Vector3.one;
			component3.SetData(GameContext.rewardCurrentLevelPlay.cardDrone, GameContext.rewardCurrentLevelPlay.numberCardDrone);
			component3.SetView();
		}
		int numberGem = GameContext.rewardCurrentLevelPlay.numberGem;
		if (numberGem > 0)
		{
			GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.rewardGem, this.transformReward.localPosition, Quaternion.identity);
			RewardGem component4 = gameObject4.GetComponent<RewardGem>();
			gameObject4.transform.parent = this.transformReward;
			gameObject4.transform.localScale = Vector3.one;
			component4.numberReward = numberGem;
			component4.SetView();
			GameContext.ModeGamePlay currentModeGamePlay = GameContext.currentModeGamePlay;
			if (currentModeGamePlay != GameContext.ModeGamePlay.Campaign)
			{
				if (currentModeGamePlay == GameContext.ModeGamePlay.Boss)
				{
					CurrencyLog.LogGemIn(numberGem, CurrencyLog.In.Victory_Boss, string.Concat(new object[]
					{
						"Boss_",
						GameContext.currentLevel,
						"_",
						GameContext.currentModeLevel
					}));
				}
			}
			else
			{
				CurrencyLog.LogGemIn(numberGem, CurrencyLog.In.Victory_Campaign, string.Concat(new object[]
				{
					"Campaign_",
					GameContext.currentLevel,
					"_",
					GameContext.currentModeLevel
				}));
			}
		}
		int numberBox = GameContext.rewardCurrentLevelPlay.numberBox;
		if (numberBox > 0)
		{
			for (int i = 0; i < numberBox; i++)
			{
				this.RandomRewardFromBox();
			}
		}
		int numberUltraStarshipCard = GameContext.rewardCurrentLevelPlay.numberUltraStarshipCard;
		if (numberUltraStarshipCard > 0)
		{
			GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.rewardCardAllPlane, this.transformReward.localPosition, Quaternion.identity);
			RewardCardAllPlane component5 = gameObject5.GetComponent<RewardCardAllPlane>();
			gameObject5.transform.parent = this.transformReward;
			gameObject5.transform.localScale = Vector3.one;
			component5.numberReward = numberUltraStarshipCard;
			component5.SetView();
		}
		int numberUltraDroneCard = GameContext.rewardCurrentLevelPlay.numberUltraDroneCard;
		if (numberUltraDroneCard > 0)
		{
			GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.rewardCardAllDrone, this.transformReward.localPosition, Quaternion.identity);
			RewardCardAllDrone component6 = gameObject6.GetComponent<RewardCardAllDrone>();
			gameObject6.transform.parent = this.transformReward;
			gameObject6.transform.localScale = Vector3.one;
			component6.numberReward = numberUltraDroneCard;
			component6.SetView();
		}
	}

	private void RandomRewardFromBox()
	{
		switch (UnityEngine.Random.Range(0, 5))
		{
		case 0:
		{
			int numberReward = UnityEngine.Random.Range(1, 3);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.rewardCardAllPlane, this.transformReward.localPosition, Quaternion.identity);
			RewardCardAllPlane component = gameObject.GetComponent<RewardCardAllPlane>();
			gameObject.transform.parent = this.transformReward;
			gameObject.transform.localScale = Vector3.one;
			component.numberReward = numberReward;
			component.SetView();
			break;
		}
		case 1:
		{
			int numberReward2 = UnityEngine.Random.Range(1, 3);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.rewardCardAllDrone, this.transformReward.localPosition, Quaternion.identity);
			RewardCardAllDrone component2 = gameObject2.GetComponent<RewardCardAllDrone>();
			gameObject2.transform.parent = this.transformReward;
			gameObject2.transform.localScale = Vector3.one;
			component2.numberReward = numberReward2;
			component2.SetView();
			break;
		}
		case 2:
		{
			int num = UnityEngine.Random.Range(1, 3);
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.rewardGem, this.transformReward.localPosition, Quaternion.identity);
			RewardGem component3 = gameObject3.GetComponent<RewardGem>();
			gameObject3.transform.parent = this.transformReward;
			gameObject3.transform.localScale = Vector3.one;
			component3.numberReward = num;
			component3.SetView();
			CurrencyLog.LogGemIn(num, CurrencyLog.In.Victory, "Victory_Random_Box");
			break;
		}
		case 3:
		{
			int numberReward3 = UnityEngine.Random.Range(1, 5);
			GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.rewardSkillPlane, this.transformReward.localPosition, Quaternion.identity);
			RewardSkillPlane component4 = gameObject4.GetComponent<RewardSkillPlane>();
			gameObject4.transform.parent = this.transformReward;
			gameObject4.transform.localScale = Vector3.one;
			component4.numberReward = numberReward3;
			component4.SetView();
			break;
		}
		case 4:
		{
			int numberReward4 = UnityEngine.Random.Range(5, 11);
			GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.rewardEnergy, this.transformReward.localPosition, Quaternion.identity);
			RewardEnergy component5 = gameObject5.GetComponent<RewardEnergy>();
			gameObject5.transform.parent = this.transformReward;
			gameObject5.transform.localScale = Vector3.one;
			component5.numberReward = numberReward4;
			component5.SetView();
			break;
		}
		default:
			UnityEngine.Debug.Log("Bạn quá đen");
			break;
		}
	}

	public void ShowPopupUnlockPlane(GameContext.Plane _planeID)
	{
		bool flag = CacheGame.IsUnlockPlane(_planeID);
		if (flag)
		{
			return;
		}
		EscapeManager.Current.AddAction(new Action(this.HidePopupOwnedPlane));
		this.SetViewPlane(_planeID);
		this.popupUnlockPlane.SetActive(true);
		DOTween.Restart("UNLOCK_PLANE", true, -1f);
		DOTween.Play("UNLOCK_PLANE");
	}

	public void HidePopupOwnedPlane()
	{
		DOTween.PlayBackwards("UNLOCK_PLANE");
		base.StartCoroutine(GameContext.Delay(0.2f, delegate
		{
			this.popupUnlockPlane.SetActive(false);
		}));
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupOwnedPlane));
	}

	private void SetViewPlane(GameContext.Plane planeID)
	{
		DataGame.Current.GetInfoPlane(planeID);
		this.imgPlane.sprite = DataGame.Current.SpritePlane(planeID);
		DataGame.Current.SetImageRank2(this.imgRank, DataGame.InfoPlane.CurrentRank);
		this.textNamePlaneUnlock.text = DataGame.InfoPlane.PlaneName;
		this.textMainPower.text = string.Empty + DataGame.InfoPlane.BasePower;
		this.textSubPower.text = string.Empty + DataGame.InfoPlane.BaseSubPower;
		this.textSpecialPower.text = string.Empty + DataGame.InfoPlane.BaseSpecialPower;
		this.textNameSkill.text = DataGame.InfoPlane.NameSkill;
		this.textDescriptionSkill.text = DataGame.InfoPlane.DescriptionSkill;
	}

	public void PlayNextLevel()
	{
		int num = GameContext.currentLevel + 1;
		int num2 = 0;
		GameContext.ModeGamePlay currentModeGamePlay = GameContext.currentModeGamePlay;
		if (currentModeGamePlay != GameContext.ModeGamePlay.Campaign)
		{
			if (currentModeGamePlay == GameContext.ModeGamePlay.Boss)
			{
				num2 = 14;
			}
		}
		else
		{
			num2 = 70;
		}
		if (num <= num2)
		{
			this.HidePanelVictory();
			GameContext.currentLevel = num;
			GameContext.ModeGamePlay currentModeGamePlay2 = GameContext.currentModeGamePlay;
			if (currentModeGamePlay2 != GameContext.ModeGamePlay.Campaign)
			{
				if (currentModeGamePlay2 == GameContext.ModeGamePlay.Boss)
				{
					GameContext.numberStarCurrentLvl = SaveDataLevelBossMode.NumberStars(num);
				}
			}
			else
			{
				GameContext.numberStarCurrentLvl = SaveDataLevel.NumberStar(num);
			}
			RewardStageManager.current.ShowPanelReward();
			EazySoundManager.PlayUISound(AudioCache.UISound.tap);
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_you_already_passed_all_levels);
		}
	}

	public void WatchVideoToX2Coin()
	{
		GameContext.actionVideo = GameContext.ActionVideoAds.X2_Coin;
	}

	public void WatchVideoToX2CoinComplete()
	{
		if (!this.isGetReward)
		{
			this.isGetReward = true;
			int num = this.coinEarned * (this.nValue - 1);
			Notification.Current.ShowNotification(string.Format(ScriptLocalization.notify_video_complete + ", +{0} Coins", num));
			CacheGame.AddCoins(num);
			this.btnX2Coins.interactable = false;
			this.animBtnX2Coins.DOPause();
			int num2 = this.coinEarned * this.nValue;
			this.textRewardCoin.text = "x" + num2;
			CurrencyLog.LogGoldIn(this.coinEarned, CurrencyLog.In.WatchVideo, "Victory_X2");
		}
	}

	private IEnumerator StartFxVictory()
	{
		this.skeletonGraphic.AnimationState.AddAnimation(1, "idle", true, 1f);
		yield return new WaitForSeconds(0.05f);
		base.StartCoroutine(GameContext.Delay(1f, delegate
		{
			DOTween.Play("FX_VICTORY_MOVE");
		}));
		yield return new WaitForSeconds(0.9f);
		DOTween.Play("FADE_VICTORY");
		yield break;
	}

	public void CheckUpgrade()
	{
		if ((GameContext.currentLevel == 3 && NewTutorial.current.currentStepTutorial_UpgradePlane == 0) || (GameContext.currentLevel == 4 && NewTutorial.current.currentStepTutorial_EquipDrone == 0 && !CacheGame.IsOwnedDrone(GameContext.Drone.GatlingGun)) || (GameContext.currentLevel == 5 && NewTutorial.current.currentStepTutorial_OpenBox == 0) || (GameContext.currentLevel == 10 && NewTutorial.current.currentStepTutorial_EvovlePlane == 0))
		{
			this.btnNextLevel.SetActive(false);
			this.btnUpgrade.SetActive(false);
			this.btnTakeGift.SetActive(true);
		}
		else
		{
			this.btnNextLevel.SetActive(true);
			this.btnUpgrade.SetActive(false);
			this.btnTakeGift.SetActive(false);
		}
		if (GameContext.maxLevelUnlocked > 11 && GameContext.maxLevelUnlocked < 18)
		{
			int currentLevelPlane = DataGame.Current.GetCurrentLevelPlane(GameContext.currentPlaneIDEquiped);
			int priceCoin = DroneCostUpgradeSheet.Get(currentLevelPlane).priceCoin;
			if (ShopContext.currentCoin >= priceCoin)
			{
				this.btnNextLevel.SetActive(false);
				this.btnUpgrade.SetActive(true);
				this.btnTakeGift.SetActive(false);
			}
		}
	}

	public void ShowPanelUpgradePlane()
	{
		PlaneManager.current.ShowPanelSelectPlane();
		this.HidePanelVictory();
	}

	public void TakeGift()
	{
		SelectLevelManager.current.Back();
	}

	public static VictoryManager current;

	public GameObject panelVictory;

	public Text textStage;

	public GameObject starEasy;

	public GameObject starNormal;

	public GameObject starHard;

	public GameObject effectStar;

	public Text textScore;

	public Text textBtnX2Coin;

	public Text textRewardCoin;

	public Text textX2Coin;

	public RectTransform transformReward;

	public GameObject rewardUnlockPlane;

	public GameObject rewardCardAllPlane;

	public GameObject rewardCardAllDrone;

	public GameObject rewardCardPlane;

	public GameObject rewardCardDrone;

	public GameObject rewardGem;

	public GameObject rewardEnergy;

	public GameObject rewardSkillPlane;

	public GameObject blueStarEndlessMode;

	[Header("Popup Unlock Plane")]
	public GameObject popupUnlockPlane;

	public Text textNamePlaneUnlock;

	public Text textMainPower;

	public Text textSubPower;

	public Text textSpecialPower;

	public Text textNameSkill;

	public Text textDescriptionSkill;

	public Image imgPlane;

	public Image imgRank;

	public BecomeRich becomeRich;

	public Button btnX2Coins;

	public DOTweenAnimation animBtnX2Coins;

	[Header("Fx Victory")]
	public GameObject panelFxVictory;

	public SkeletonGraphic skeletonGraphic;

	public GameObject btnNextLevel;

	public GameObject btnUpgrade;

	public GameObject btnTakeGift;

	public GameObject btnBack;

	private int nValue = 2;

	private int coinEarned;

	private bool isGetReward;
}
