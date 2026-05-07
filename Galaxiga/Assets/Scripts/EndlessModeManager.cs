using System;
using System.Collections.Generic;
using BayatGames.SaveGamePro;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class EndlessModeManager : MonoBehaviour
{
	private void Awake()
	{
		EndlessModeManager.current = this;
		if (GameContext.maxLevelUnlocked > 15)
		{
			EndlessModeManager.GetDataEndlessMode();
			this.objLockBtnShowPanelEndlessMode.SetActive(false);
			this.endlessModeShopManager.CheckPackOneTime();
		}
		if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Endless)
		{
			this.ShowPanelEndlessMode();
		}
	}

	private void Start()
	{
		if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Endless)
		{
			this.ShowPanelCompleteEndlessMode();
		}
	}

	public static void GetDataEndlessMode()
	{
		EndlessModeManager.endlessModeData = new EndessModeData();
		EndlessModeManager.endlessModeData = SaveGame.Load<EndessModeData>("endless_data_1");
		if (EndlessModeManager.endlessModeData == null)
		{
			UnityEngine.Debug.Log("init data endless mode");
			EndlessModeManager.InitDataEndlessMode();
		}
		Tung.Log("number blue star " + EndlessModeManager.endlessModeData.numberBlueStar);
	}

	private static void InitDataEndlessMode()
	{
		EndlessModeManager.endlessModeData = new EndessModeData();
		EndlessModeManager.endlessModeData.maxWave = 0;
		EndlessModeManager.endlessModeData.numberBlueStar = 0;
		EndlessModeManager.endlessModeData.numberTicketUniverse2 = 0;
		EndlessModeManager.endlessModeData.numberTicketUniverse3 = 0;
		EndlessModeManager.endlessModeData.numberTicketUniverse4 = 0;
		EndlessModeManager.endlessModeData.numberTicketUniverse5 = 0;
		EndlessModeManager.endlessModeData.universeDatas = new List<EndessModeData.UniverseData>();
		for (int i = 0; i < 5; i++)
		{
			EndessModeData.UniverseData item = new EndessModeData.UniverseData(i, 0, 0, 0);
			EndlessModeManager.endlessModeData.universeDatas.Add(item);
		}
		EndlessModeManager.endlessModeData.arrBoolStateRewardOneTime = new List<bool>();
		for (int j = 0; j < 30; j++)
		{
			EndlessModeManager.endlessModeData.arrBoolStateRewardOneTime.Add(false);
		}
		EndlessModeManager.SaveDataEndlessMode();
	}

	private void CheckUniverse()
	{
		int maxWave = EndlessModeManager.endlessModeData.maxWave;
		for (int i = 0; i < this.universes.Length; i++)
		{
			if (maxWave >= this.universes[i].minWave)
			{
				this.universes[i].SetUnlockUniverse();
				this.maxUniverseUnlocked = i;
			}
		}
	}

	private void SetViewPanelEndlessMode()
	{
		if (this.needSetViewPanelEndless)
		{
			this.needSetViewPanelEndless = false;
			this.panelLoading.SetActive(true);
			this.CheckUniverse();
			this.textNumberBlueStar.text = string.Empty + EndlessModeManager.endlessModeData.numberBlueStar;
			this.textNumberBlueStar2.text = string.Empty + EndlessModeManager.endlessModeData.numberBlueStar;
			this.textNumberTicketUniver2.text = string.Empty + EndlessModeManager.endlessModeData.numberTicketUniverse2;
			this.textNumberTicketUniver3.text = string.Empty + EndlessModeManager.endlessModeData.numberTicketUniverse3;
			this.textNumberTicketUniver4.text = string.Empty + EndlessModeManager.endlessModeData.numberTicketUniverse4;
			this.textNumberTicketUniver5.text = string.Empty + EndlessModeManager.endlessModeData.numberTicketUniverse5;
			this.panelLoading.SetActive(false);
			this.SetViewInfoCurrentHighestWave();
		}
	}

	private void SetViewInfoCurrentHighestWave()
	{
		this.textMaxWave.text = string.Empty + EndlessModeManager.endlessModeData.maxWave;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < EndlessModeManager.endlessModeData.universeDatas.Count; i++)
		{
			num += EndlessModeManager.endlessModeData.universeDatas[i].bestScore;
			num2 += EndlessModeManager.endlessModeData.universeDatas[i].time;
		}
		this.textTotalScoreAllGalaxy.text = GameUtil.FormatNumber(num);
		this.textTotalTimeAllGalaxy.text = GameUtil.GetFormatTimeFrom(num2);
	}

	[EnumAction(typeof(EndlessModeManager.UniverseID))]
	public void SelectUniverse(int _universe)
	{
		if (_universe <= this.maxUniverseUnlocked)
		{
			GameContext.currentUniverse = _universe;
			this.ShowPanelSelectUniverse();
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.complete_previous_stages);
		}
	}

	public void ShowPanelEndlessMode()
	{
		if (GameContext.maxLevelUnlocked > 15)
		{
			GameContext.currentModeGamePlay = GameContext.ModeGamePlay.Endless;
			this.panelEndlessMode.SetActive(true);
			this.SetViewPanelEndlessMode();
			DOTween.Restart("ENDLESS_MODE", true, -1f);
		}
		else
		{
			Notification.Current.ShowNotification(string.Format(ScriptLocalization.notify_you_need_pass_stage_in_mode_campaign, 15));
		}
	}

	public void HidePanelEndlessMode()
	{
		EndlessModeManager.SaveDataEndlessMode();
		GameContext.currentModeGamePlay = GameContext.ModeGamePlay.Campaign;
		DOTween.PlayBackwards("ENDLESS_MODE");
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelEndlessMode));
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.panelEndlessMode.SetActive(false);
		}));
	}

	private void SetViewPanelSelectUniverse()
	{
		this.universe = GameContext.currentUniverse;
		this.panelLoading.SetActive(true);
		this.textUniverse.text = ScriptLocalization.universe + " " + (GameContext.currentUniverse + 1);
		this.textScore.text = ScriptLocalization.Score + " " + EndlessModeManager.endlessModeData.universeDatas[GameContext.currentUniverse].bestScore;
		int maxWave = EndlessModeManager.endlessModeData.maxWave;
		this.textHighestWave.text = string.Format("<color=#37d45aff >{0}</color>", maxWave);
		string formatTimeFrom = GameUtil.GetFormatTimeFrom(EndlessModeManager.endlessModeData.universeDatas[GameContext.currentUniverse].time);
		this.textTime.text = string.Format(ScriptLocalization.time + ": <color=#37d45aff >{0}</color>", formatTimeFrom);
		int num = GameContext.currentUniverse * 40 + 40;
		this.sliderProgress.minValue = (float)this.universes[GameContext.currentUniverse].minWave;
		this.sliderProgress.maxValue = (float)num;
		this.sliderProgress.value = (float)maxWave;
		for (int i = 0; i < 4; i++)
		{
			int num2 = i * 10 + (this.universe * 40 + 10);
			this.arrTextWave[i].text = string.Empty + num2;
			int index = num2 / 10 - 1;
			if (EndlessModeManager.endlessModeData.arrBoolStateRewardOneTime[index])
			{
				this.arrImgBtnGift[i].sprite = this.sprGiftOpenned;
				this.arrAnimBtnGift[i].DOPause();
			}
			else if (EndlessModeManager.endlessModeData.maxWave >= num2)
			{
				this.arrImgBtnGift[i].sprite = this.sprGiftIdle;
				this.arrAnimBtnGift[i].DOPlay();
			}
			else
			{
				this.arrImgBtnGift[i].sprite = this.sprGiftIdle;
				this.arrAnimBtnGift[i].DOPause();
			}
		}
		switch (this.universe)
		{
		case 0:
			this.imgTicketUniverse.gameObject.SetActive(false);
			this.imgBtnPlay.rectTransform.sizeDelta = new Vector2(315f, 76f);
			break;
		case 1:
			this.imgTicketUniverse.gameObject.SetActive(true);
			this.numberTicketCurrentUniverse = EndlessModeManager.endlessModeData.numberTicketUniverse2;
			this.imgBtnPlay.rectTransform.sizeDelta = new Vector2(410f, 76f);
			break;
		case 2:
			this.imgTicketUniverse.gameObject.SetActive(true);
			this.numberTicketCurrentUniverse = EndlessModeManager.endlessModeData.numberTicketUniverse3;
			this.imgBtnPlay.rectTransform.sizeDelta = new Vector2(410f, 76f);
			break;
		case 3:
			this.imgTicketUniverse.gameObject.SetActive(true);
			this.numberTicketCurrentUniverse = EndlessModeManager.endlessModeData.numberTicketUniverse4;
			this.imgBtnPlay.rectTransform.sizeDelta = new Vector2(410f, 76f);
			break;
		case 4:
			this.imgTicketUniverse.gameObject.SetActive(true);
			this.numberTicketCurrentUniverse = EndlessModeManager.endlessModeData.numberTicketUniverse5;
			this.imgBtnPlay.rectTransform.sizeDelta = new Vector2(410f, 76f);
			break;
		}
		this.imgTicketUniverse.sprite = this.universes[this.universe].sprTicket;
		this.panelLoading.SetActive(false);
	}

	public void ShowPanelSelectUniverse()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePanelSelectUniverse));
		this.panelSelectUniverse.SetActive(true);
		if (GameContext.currentUniverse != this.universe)
		{
			this.SetViewPanelSelectUniverse();
		}
		DOTween.Restart("SELECT_UNIVERSE", true, -1f);
	}

	public void HidePanelSelectUniverse()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelSelectUniverse));
		DOTween.PlayBackwards("SELECT_UNIVERSE");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.panelSelectUniverse.SetActive(false);
		}));
	}

	public void ShowPanelShop()
	{
		this.panelShop.SetActive(true);
		DOTween.Restart("SHOP_ENDLESS", true, -1f);
	}

	public void HidePanelShop()
	{
		DOTween.PlayBackwards("SHOP_ENDLESS");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.panelShop.SetActive(false);
		}));
	}

	public void ShowPanelQuest()
	{
		this.panelQuest.SetActive(true);
		DOTween.Restart("QUEST_ENDLESS", true, -1f);
	}

	public void HidePanelQuest()
	{
		DOTween.PlayBackwards("QUEST_ENDLESS");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.panelQuest.SetActive(false);
		}));
	}

	public void ShowPanelUpgrade()
	{
		PlaneManager.current.ShowPanelSelectPlane();
	}

	public void CollectRewardOneTimeEndless()
	{
		if (this.canClaimReward)
		{
			this.ClaimReward();
		}
		else if (this.isClaimReward)
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_already_claimed);
		}
		else
		{
			Notification.Current.ShowNotification(string.Format(ScriptLocalization.notify_you_need_pass_wave, this.waveRewardOneTime));
		}
	}

	private void ClaimReward()
	{
		this.canClaimReward = false;
		this.isClaimReward = true;
		EndlessModeManager.endlessModeData.arrBoolStateRewardOneTime[this.currentIDBoolStateRewardOneTime] = true;
		this.arrAnimBtnGift[this.saveIDGift].DORestart(false);
		this.arrAnimBtnGift[this.saveIDGift].DOPause();
		this.arrImgBtnGift[this.saveIDGift].sprite = this.sprGiftOpenned;
		this.imgButtonClaim.sprite = this.sprBtnDisable;
		this.HidePopupRewardOneTime();
		CacheGame.AddCoins(this.numberCoinRewardOneTime);
		this.AddBlueStar(this.numberBlueStarRewardOneTime);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, this.numberCoinRewardOneTime);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Blue_Star, this.numberCoinRewardOneTime);
	}

	private void CheckRewardOneTimeEndless()
	{
		this.currentIDBoolStateRewardOneTime = this.waveRewardOneTime / 10 - 1;
		this.textWaveReward.text = ScriptLocalization.wave + " " + this.waveRewardOneTime;
		if (EndlessModeManager.endlessModeData.arrBoolStateRewardOneTime[this.currentIDBoolStateRewardOneTime])
		{
			this.imgButtonClaim.sprite = this.sprBtnDisable;
			this.isClaimReward = true;
			this.canClaimReward = false;
		}
		else if (EndlessModeManager.endlessModeData.maxWave >= this.waveRewardOneTime)
		{
			this.imgButtonClaim.sprite = this.sprBtn;
			this.canClaimReward = true;
			this.isClaimReward = false;
		}
		else
		{
			this.imgButtonClaim.sprite = this.sprBtnDisable;
			this.canClaimReward = false;
			this.isClaimReward = false;
		}
		this.numberCoinRewardOneTime = RewardOnedTimeEndlessModeSheet.Get(this.waveRewardOneTime).coins;
		this.numberBlueStarRewardOneTime = RewardOnedTimeEndlessModeSheet.Get(this.waveRewardOneTime).blueStars;
		this.itemBlueStar.numberReward = this.numberBlueStarRewardOneTime;
		this.itemBlueStar.SetView();
		this.itemCoin.numberReward = this.numberCoinRewardOneTime;
		this.itemCoin.SetView();
	}

	public void ShowPopupRewardOneTime(int idGift)
	{
		if (idGift != this.saveIDGift)
		{
			this.saveIDGift = idGift;
			this.waveRewardOneTime = idGift * 10 + (this.universe * 40 + 10);
			this.CheckRewardOneTimeEndless();
		}
		EscapeManager.Current.AddAction(new Action(this.HidePopupRewardOneTime));
		this.panelRewardOneTime.SetActive(true);
		DOTween.Restart("POPUP_REWARD_ONE_TIME_ENDLESS", true, -1f);
	}

	public void HidePopupRewardOneTime()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupRewardOneTime));
		DOTween.PlayBackwards("POPUP_REWARD_ONE_TIME_ENDLESS");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.panelRewardOneTime.SetActive(false);
		}));
	}

	private void SetViewPanelInfoItemEndless()
	{
		try
		{
			this.textTicketUniverse2.text = string.Format(ScriptLocalization.des_ticket, "DH880");
			this.textTicketUniverse3.text = string.Format(ScriptLocalization.des_ticket, "Brokenage");
			this.textTicketUniverse4.text = string.Format(ScriptLocalization.des_ticket, "NGC1200");
			this.textTicketUniverse5.text = string.Format(ScriptLocalization.des_ticket, "NGC1200");
		}
		catch (Exception ex)
		{
			Tung.Log(ex.Message);
		}
	}

	public void ShowPanelInfoItemEndless()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePanelInfoItemEndless));
		if (!this.isSetView)
		{
			this.isSetView = true;
			this.SetViewPanelInfoItemEndless();
		}
		this.panelInfoItem.SetActive(true);
		DOTween.Restart("INFO_ITEM_ENDLESS", true, -1f);
	}

	public void HidePanelInfoItemEndless()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelInfoItemEndless));
		DOTween.PlayBackwards("INFO_ITEM_ENDLESS");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.panelInfoItem.SetActive(false);
		}));
	}

	private void CompleteEndless()
	{
		SpaceForceFirebaseLogger.LogLevelFailEndlessMode(GameContext.currentWaveEndlessMode);
		if (GameContext.currentWaveEndlessMode > EndlessModeManager.endlessModeData.maxWave)
		{
			EndlessModeManager.endlessModeData.maxWave = GameContext.currentWaveEndlessMode;
			this.SetViewInfoCurrentHighestWave();
		}
		switch (GameContext.currentUniverse)
		{
		case 0:
			this.textNameGalaxy.text = string.Format(ScriptLocalization.universe, "Milki Way");
			break;
		case 1:
			this.textNameGalaxy.text = string.Format(ScriptLocalization.universe, "DH880");
			break;
		case 2:
			this.textNameGalaxy.text = string.Format(ScriptLocalization.universe, "Brokenage");
			break;
		case 3:
			this.textNameGalaxy.text = string.Format(ScriptLocalization.universe, "NGC1200");
			break;
		case 4:
			this.textNameGalaxy.text = string.Format(ScriptLocalization.universe, "NGC1200");
			break;
		}
		if (GameContext.currentWaveEndlessMode > EndlessModeManager.endlessModeData.universeDatas[GameContext.currentUniverse].wave)
		{
			EndlessModeManager.endlessModeData.universeDatas[GameContext.currentUniverse].wave = GameContext.currentWaveEndlessMode;
			EndlessModeManager.endlessModeData.universeDatas[GameContext.currentUniverse].bestScore = GameContext.currentScore;
			EndlessModeManager.endlessModeData.universeDatas[GameContext.currentUniverse].time = GameContext.totalSecondEndGame;
			this.CheckUniverse();
		}
		this.textHighestWave2.text = string.Empty + EndlessModeManager.endlessModeData.maxWave;
		this.textWavePasse.text = string.Empty + GameContext.currentWaveEndlessMode;
		this.textScoreComplete.text = string.Empty + GameContext.currentScore;
		this.textTimeComplete.text = GameUtil.GetFormatTimeFrom(GameContext.totalSecondEndGame);
		this.textStarCollected.text = "+" + GameContext.currentBlueStarCollectInGame;
		this.textStarX2.text = "+" + GameContext.currentBlueStarCollectInGame;
		this.AddBlueStar(GameContext.currentBlueStarCollectInGame);
	}

	public void WatchVideoToX2BlueStar()
	{
		GameContext.actionVideo = GameContext.ActionVideoAds.X2_Blue_Star;
	}

	public void WatchVideoToX2BlueStarComplete()
	{
		this.AddBlueStar(GameContext.currentBlueStarCollectInGame);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Blue_Star, GameContext.currentBlueStarCollectInGame);
		GetRewardSuccess.current.ShowPanelGetRewardSuccess();
		this.HidePanelCompleteEndlessMode();
	}

	public void ShowPanelCompleteEndlessMode()
	{
		this.CompleteEndless();
		EscapeManager.Current.AddAction(new Action(this.HidePanelCompleteEndlessMode));
		this.panelCompleteEndless.SetActive(true);
		DOTween.Play("COMPLETE_ENDLESS_MODE");
	}

	public void HidePanelCompleteEndlessMode()
	{
		GameContext.Reset();
		GameContext.ResetLevel();
		GameContext.currentWaveEndlessMode = 0;
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelCompleteEndlessMode));
		DOTween.PlayBackwards("COMPLETE_ENDLESS_MODE");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.panelCompleteEndless.SetActive(false);
		}));
	}

	public void PlayEndless()
	{
		switch (this.universe)
		{
		case 1:
			this.numberTicketCurrentUniverse = EndlessModeManager.endlessModeData.numberTicketUniverse2;
			break;
		case 2:
			this.numberTicketCurrentUniverse = EndlessModeManager.endlessModeData.numberTicketUniverse3;
			break;
		case 3:
			this.numberTicketCurrentUniverse = EndlessModeManager.endlessModeData.numberTicketUniverse4;
			break;
		case 4:
			this.numberTicketCurrentUniverse = EndlessModeManager.endlessModeData.numberTicketUniverse5;
			break;
		}
		if (this.numberTicketCurrentUniverse > 0 || this.universe == 0)
		{
			if (GameContext.isPurchasePremiumPack)
			{
				this.PlayNow();
			}
			else if (GameContext.totalEnergy > 10)
			{
				this.PlayNow();
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.notify_you_need_buy_more_energy);
			}
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.dont_have_ticket);
		}
	}

	private void PlayNow()
	{
		switch (this.universe)
		{
		case 1:
			EndlessModeManager.endlessModeData.numberTicketUniverse2--;
			break;
		case 2:
			EndlessModeManager.endlessModeData.numberTicketUniverse3--;
			break;
		case 3:
			EndlessModeManager.endlessModeData.numberTicketUniverse4--;
			break;
		case 4:
			EndlessModeManager.endlessModeData.numberTicketUniverse5--;
			break;
		}
		EndlessModeManager.SaveDataEndlessMode();
		SceneContext.sceneName = "LevelEndLess";
		SelectLevelManager.current.NextToLoadingScene();
	}

	public static void SaveDataEndlessMode()
	{
		SaveGame.Save<EndessModeData>("endless_data_1", EndlessModeManager.endlessModeData);
	}

	public static void LoadDataEndlessMode()
	{
		EndlessModeManager.endlessModeData = SaveGame.Load<EndessModeData>("endless_data_1");
	}

	public void AddBlueStar(int number)
	{
		this.BlueStar += number;
		DOTween.Restart("PUNCH_SCALE_BLUESTAR", true, -1f);
	}

	public int BlueStar
	{
		get
		{
			return EndlessModeManager.endlessModeData.numberBlueStar;
		}
		set
		{
			EndlessModeManager.endlessModeData.numberBlueStar = value;
			this.textNumberBlueStar.text = string.Empty + EndlessModeManager.endlessModeData.numberBlueStar;
			this.textNumberBlueStar2.text = string.Empty + EndlessModeManager.endlessModeData.numberBlueStar;
			EndlessModeManager.SaveDataEndlessMode();
		}
	}

	public int TicketGalaxy1
	{
		get
		{
			return EndlessModeManager.endlessModeData.numberTicketUniverse2;
		}
		set
		{
			EndlessModeManager.endlessModeData.numberTicketUniverse2 = value;
			this.textNumberTicketUniver2.text = string.Empty + EndlessModeManager.endlessModeData.numberTicketUniverse2;
		}
	}

	public int TicketGalaxy2
	{
		get
		{
			return EndlessModeManager.endlessModeData.numberTicketUniverse3;
		}
		set
		{
			EndlessModeManager.endlessModeData.numberTicketUniverse3 = value;
			this.textNumberTicketUniver3.text = string.Empty + EndlessModeManager.endlessModeData.numberTicketUniverse3;
		}
	}

	public int TicketGalaxy3
	{
		get
		{
			return EndlessModeManager.endlessModeData.numberTicketUniverse4;
		}
		set
		{
			EndlessModeManager.endlessModeData.numberTicketUniverse4 = value;
			this.textNumberTicketUniver4.text = string.Empty + EndlessModeManager.endlessModeData.numberTicketUniverse4;
		}
	}

	public int TicketGalaxy4
	{
		get
		{
			return EndlessModeManager.endlessModeData.numberTicketUniverse5;
		}
		set
		{
			EndlessModeManager.endlessModeData.numberTicketUniverse5 = value;
			this.textNumberTicketUniver5.text = string.Empty + EndlessModeManager.endlessModeData.numberTicketUniverse5;
		}
	}

	public static EndlessModeManager current;

	public GameObject panelEndlessMode;

	public GameObject panelShop;

	public GameObject panelQuest;

	public GameObject panelLoading;

	public static EndessModeData endlessModeData;

	public Sprite sprBtn;

	public Sprite sprBtnDisable;

	[Header("Panel Endless Mode")]
	public Text textNumberBlueStar;

	[Header("Panel Endless Mode")]
	public Text textNumberBlueStar2;

	public Text textNumberTicketUniver2;

	public Text textNumberTicketUniver3;

	public Text textNumberTicketUniver4;

	public Text textNumberTicketUniver5;

	public Text textTotalScoreAllGalaxy;

	public Text textTotalTimeAllGalaxy;

	public Text textMaxWave;

	public GameObject objLockBtnShowPanelEndlessMode;

	public EndlessModeShopManager endlessModeShopManager;

	public EndlessModeManager.Universe[] universes;

	[Header("Panel Select Universe")]
	public GameObject panelSelectUniverse;

	public Text textUniverse;

	public Text textScore;

	public Text textHighestWave;

	public Text textTime;

	public Slider sliderProgress;

	public Image imgTicketUniverse;

	public Image imgBtnPlay;

	public Sprite sprGiftOpenned;

	public Sprite sprGiftIdle;

	public DOTweenAnimation[] arrAnimBtnGift;

	public Image[] arrImgBtnGift;

	public Text[] arrTextWave;

	[Header("Panel Reward One-Time")]
	public GameObject panelRewardOneTime;

	public Text textWaveReward;

	public ItemRewardInGame itemCoin;

	public ItemRewardInGame itemBlueStar;

	public Image imgButtonClaim;

	[Header("Panel Info Item Endless")]
	public GameObject panelInfoItem;

	public Text textTicketUniverse2;

	public Text textTicketUniverse3;

	public Text textTicketUniverse4;

	public Text textTicketUniverse5;

	[Header("Panel Complete Endless Mode")]
	public GameObject panelCompleteEndless;

	public Text textNameGalaxy;

	public Text textHighestWave2;

	public Text textWavePasse;

	public Text textScoreComplete;

	public Text textTimeComplete;

	public Text textStarCollected;

	public Text textStarX2;

	public const string NAME_UNIVERSE_1 = "Milki Way";

	public const string NAME_UNIVERSE_2 = "DH880";

	public const string NAME_UNIVERSE_3 = "Brokenage";

	public const string NAME_UNIVERSE_4 = "NGC1200";

	public const string NAME_UNIVERSE_5 = "NGC1200";

	private const string KEY_DATA = "endless_data_1";

	private const int LEVEL_UNLOCK_ENDLESS_MODE = 15;

	private int universe = -1;

	private int waveRewardOneTime;

	private int numberTicketCurrentUniverse;

	private int maxUniverseUnlocked;

	private bool needSetViewPanelEndless = true;

	private bool canClaimReward;

	private bool isClaimReward;

	private int currentIDBoolStateRewardOneTime;

	private int numberCoinRewardOneTime;

	private int numberBlueStarRewardOneTime;

	private int saveIDGift = -1;

	private bool isSetView;

	[Serializable]
	public class Universe
	{
		public void SetUnlockUniverse()
		{
			this.imgUniverse.sprite = this.sprUniverseUnlock;
			this.isUnlocked = true;
		}

		public Image imgUniverse;

		public Sprite sprUniverseUnlock;

		public Sprite sprTicket;

		public int minWave = 40;

		public bool isUnlocked;
	}

	public enum UniverseID
	{
		Universe_1,
		Universe_2,
		Universe_3,
		Universe_4,
		Universe_5
	}
}
