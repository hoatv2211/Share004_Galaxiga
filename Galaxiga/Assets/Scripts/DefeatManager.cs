using System;
using System.Collections;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DefeatManager : MonoBehaviour
{
	private void Awake()
	{
		DefeatManager.current = this;
	}

	private void Start()
	{
		this.btnX2Coins.onClick.AddListener(new UnityAction(this.WatchVideoToX2Coin));
		if (GameContext.isMissionFail && GameContext.currentModeGamePlay != GameContext.ModeGamePlay.Endless)
		{
			this.SetText();
			this.ShowPanelDefeat();
		}
	}

	public void SetText()
	{
		GameContext.ModeGamePlay currentModeGamePlay = GameContext.currentModeGamePlay;
		if (currentModeGamePlay != GameContext.ModeGamePlay.Campaign)
		{
			if (currentModeGamePlay == GameContext.ModeGamePlay.Boss)
			{
				this.textStage.text = "BOSS " + GameContext.currentLevel;
				this.bestScore = SaveDataLevelBossMode.ScoreLevel(GameContext.currentLevel, GameContext.currentModeLevel);
				if (GameContext.currentScore > this.bestScore)
				{
					this.sliderScore.maxValue = (float)GameContext.currentScore;
					SaveDataLevelBossMode.SetScoreLevel(GameContext.currentModeLevel, GameContext.currentLevel, GameContext.currentScore);
				}
				else
				{
					this.sliderScore.maxValue = (float)this.bestScore;
				}
				this.RewardPlayer();
			}
		}
		else
		{
			this.textStage.text = ScriptLocalization.stage + " " + GameContext.currentLevel;
			this.bestScore = SaveDataLevel.ScoreLevel(GameContext.currentLevel, GameContext.currentModeLevel);
			if (GameContext.currentScore > this.bestScore)
			{
				this.sliderScore.maxValue = (float)GameContext.currentScore;
				SaveDataLevel.SetScoreLevel(GameContext.currentModeLevel, GameContext.currentLevel, GameContext.currentScore);
			}
			else
			{
				this.sliderScore.maxValue = (float)this.bestScore;
			}
			this.RewardPlayer();
		}
		this.textBestScore.text = string.Empty + this.bestScore;
	}

	private void RewardPlayer()
	{
		switch (GameContext.numberStarCurrentLvl)
		{
		case 1:
			this.starEasy.SetActive(true);
			break;
		case 2:
			this.starEasy.SetActive(true);
			this.starNormal.SetActive(true);
			break;
		case 3:
			this.starEasy.SetActive(true);
			this.starNormal.SetActive(true);
			this.starHard.SetActive(true);
			break;
		}
		int num = (int)(GameContext.percentCoinLevel * (float)GameContext.maxCoinEarnedIn1Stage);
		if (num > 0)
		{
			this.coinEarned = (int)UnityEngine.Random.Range((float)num * 0.75f, (float)num * 1.25f);
			if (this.coinEarned > 0)
			{
				if (this.coinEarned > 100000)
				{
					this.coinEarned = 100;
				}
				this.textRewardCoin.text = string.Empty + this.coinEarned;
				CacheGame.AddCoins(this.coinEarned);
			}
			this.textRewardCoin.text = string.Empty + this.coinEarned;
			this.textX2Coin.text = string.Empty + this.coinEarned;
			this.animButtonX2Coin.DOPlay();
			CurrencyLog.LogGoldIn(this.coinEarned, CurrencyLog.In.Defeat, "Defeat_Campaign");
		}
		else
		{
			this.btnX2Coins.GetComponent<DOTweenAnimation>().DOPause();
			this.btnX2Coins.interactable = false;
			this.textRewardCoin.text = string.Empty + 0;
			this.textX2Coin.text = string.Empty + 0;
		}
	}

	private IEnumerator CountTo(int target)
	{
		int start = 0;
		for (float timer = 0f; timer <= this.duration; timer += Time.deltaTime)
		{
			float progress = timer / this.duration;
			this.score = (int)Mathf.Lerp((float)start, (float)target, progress);
			this.textScore.text = string.Empty + this.score;
			this.sliderScore.value = (float)this.score;
			yield return null;
		}
		this.score = target;
		this.sliderScore.value = (float)this.score;
		this.textScore.text = string.Empty + this.score;
		if (this.score > this.bestScore)
		{
			this.textBestScore.text = string.Empty + this.score;
		}
		yield break;
	}

	private void ShowDefeatEndlessMode()
	{
		this.objStars.SetActive(false);
		if (this.rewardEndlessMode.gems > 0)
		{
			this.rewardGem.SetActive(true);
			RewardGem component = this.rewardGem.GetComponent<RewardGem>();
			component.numberReward = this.rewardEndlessMode.gems;
			component.SetView();
			CurrencyLog.LogGemIn(this.rewardEndlessMode.gems, CurrencyLog.In.Defeat, "Defeat_Endless_Mode");
		}
		if (this.rewardEndlessMode.cardAllPlane > 0)
		{
			this.rewardCardAllPlane.SetActive(true);
			RewardCardAllPlane component2 = this.rewardCardAllPlane.GetComponent<RewardCardAllPlane>();
			component2.numberReward = this.rewardEndlessMode.cardAllPlane;
			component2.SetView();
		}
		if (this.rewardEndlessMode.cardAllDrone > 0)
		{
			this.rewardCardAllDrone.SetActive(true);
			RewardCardAllDrone component3 = this.rewardCardAllDrone.GetComponent<RewardCardAllDrone>();
			component3.numberReward = this.rewardEndlessMode.cardAllDrone;
			component3.SetView();
		}
		if (this.rewardEndlessMode.cardRandomPlane > 0)
		{
			GameContext.Plane randomPlaneID = DataGame.Current.RandomPlaneID;
			string planeName = string.Empty;
			switch (randomPlaneID)
			{
			case GameContext.Plane.BataFD01:
				planeName = "Bata FD 01";
				break;
			case GameContext.Plane.FuryOfAres:
				planeName = "Fury Of Ares";
				break;
			case GameContext.Plane.SkyWraith:
				planeName = "Sky Wraith";
				break;
			case GameContext.Plane.TwilightX:
				planeName = "Twilight X";
				break;
			case GameContext.Plane.Greataxe:
				planeName = "Greataxe";
				break;
			case GameContext.Plane.SSLightning:
				planeName = "SS Lightning";
				break;
			case GameContext.Plane.Warlock:
				planeName = "Warlock";
				break;
			}
			this.rewardCardRandomPlane.SetActive(true);
			RewardCardPlane component4 = this.rewardCardRandomPlane.GetComponent<RewardCardPlane>();
			component4.SetData(planeName, this.rewardEndlessMode.cardRandomPlane);
			component4.SetView();
		}
		if (this.rewardEndlessMode.cardRandomDrone > 0)
		{
			GameContext.Drone drone = DataGame.Current.RandomDroneID();
			string droneName = string.Empty;
			switch (drone)
			{
			case GameContext.Drone.GatlingGun:
				droneName = "Galing Gun";
				break;
			case GameContext.Drone.AutoGatlingGun:
				droneName = "Auto Gatling Gun";
				break;
			case GameContext.Drone.Laser:
				droneName = "Laser";
				break;
			case GameContext.Drone.Nighturge:
				droneName = "Nighturge";
				break;
			case GameContext.Drone.GodOfThunder:
				droneName = "God Of Thunder";
				break;
			case GameContext.Drone.Terigon:
				droneName = "Terigon";
				break;
			}
			this.rewardCardRandomDrone.SetActive(true);
			RewardCardDrone component5 = this.rewardCardRandomDrone.GetComponent<RewardCardDrone>();
			component5.SetData(droneName, this.rewardEndlessMode.cardRandomDrone);
			component5.SetView();
		}
		this.objGroupReward.SetActive(true);
		this.rectTransformContent.localPosition = new Vector2(this.rectTransformContent.localPosition.x, this.rectTransformContent.localPosition.y + 130f);
	}

	public void ShowPanelDefeat()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePanelDefeat));
		NewTutorial.current.ShowForcusBtnRestart();
		if (GameContext.maxLevelUnlocked <= 3)
		{
			this.groupBtnUpgradePlane.SetActive(false);
			this.btnX2Coins.gameObject.SetActive(false);
		}
		base.StartCoroutine(GameContext.Delay(0.2f, delegate
		{
			base.StartCoroutine(this.CountTo(GameContext.currentScore));
		}));
		this.paneDefeat.SetActive(true);
		this.paneDefeat.transform.localScale = Vector3.one;
	}

	public void HidePanelDefeat()
	{
		GameContext.ResetLevel();
		DOTween.PlayBackwards("DEFEAT");
		this.paneDefeat.SetActive(false);
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelDefeat));
		//AccountManager.Instance.UpdateToServer("defeat_stage_" + GameContext.currentLevel);
	}

	public void Restart()
	{
		this.HidePanelDefeat();
		switch (GameContext.currentModeGamePlay)
		{
		case GameContext.ModeGamePlay.Campaign:
			GameContext.numberStarCurrentLvl = SaveDataLevel.NumberStar(GameContext.currentLevel);
			RewardStageManager.current.ShowPanelReward();
			break;
		case GameContext.ModeGamePlay.Boss:
			GameContext.numberStarCurrentLvl = SaveDataLevelBossMode.NumberStars(GameContext.currentLevel);
			RewardStageManager.current.ShowPanelReward();
			break;
		}
	}

	public void UpgradePlane()
	{
		PlaneManager.current.ShowPanelSelectPlane();
	}

	public void SupplyBox()
	{
		PrizeManager.current.ShowPanelPrize();
	}

	public void OfferPack()
	{
		if (!GameContext.isDisableStarterPack)
		{
			if (!GameContext.isPurchaseStarterPack)
			{
				ShopManager.Instance.ShowStarterPack();
			}
			else if (!GameContext.isPurchasePremiumPack)
			{
				ShopManager.Instance.ShowPremiumPack();
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.notify_no_offer_packs_to_show);
			}
		}
		else if (!GameContext.isPurchasePremiumPack)
		{
			ShopManager.Instance.ShowPremiumPack();
		}
		else if (SaleManager.Currrent.isSale)
		{
			SaleManager.Currrent.ShowPanelSale();
		}
		else
		{
			ShopManager.Instance.ShowPopupShopGem(0);
		}
	}

	public void WatchVideoToX2Coin()
	{
		GameContext.actionVideo = GameContext.ActionVideoAds.Defeat_X2_Coin;
	}

	public void WatchVideoToX2CoinComplete()
	{
		Notification.Current.ShowNotification(string.Format(ScriptLocalization.notify_video_complete + ", +{0} Coins", this.coinEarned));
		CacheGame.AddCoins(this.coinEarned);
		this.btnX2Coins.interactable = false;
		this.animButtonX2Coin.DOPause();
		int num = this.coinEarned * 2;
		this.textRewardCoin.text = "x" + num;
		CurrencyLog.LogGoldIn(this.coinEarned, CurrencyLog.In.WatchVideo, "Defeat_X2");
	}

	public static DefeatManager current;

	public GameObject paneDefeat;

	public Text textStage;

	public GameObject starEasy;

	public GameObject starNormal;

	public GameObject starHard;

	public GameObject bgStarEasy;

	public GameObject bgStarNormal;

	public GameObject bgStarHard;

	public Text textBestScore;

	public Text textScore;

	public Text textRewardCoin;

	public Slider sliderScore;

	public GameObject objContent;

	public GameObject objGroupReward;

	public GameObject objStars;

	public RectTransform rectTransformContent;

	public GameObject rewardGem;

	public GameObject rewardCardAllPlane;

	public GameObject rewardCardAllDrone;

	public GameObject rewardCardRandomPlane;

	public GameObject rewardCardRandomDrone;

	public GameObject groupBtnUpgradePlane;

	public Button btnX2Coins;

	public Text textX2Coin;

	public DOTweenAnimation animButtonX2Coin;

	private int bestScore = 100000;

	private float duration = 0.7f;

	private int score;

	private DefeatManager.RewardEndlessMode rewardEndlessMode;

	private int coinEarned;

	private class RewardEndlessMode
	{
		public int coins;

		public int gems;

		public int cardAllPlane;

		public int cardAllDrone;

		public int cardRandomPlane;

		public int cardRandomDrone;
	}
}
