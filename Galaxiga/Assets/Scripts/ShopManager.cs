using System;
using System.Collections;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
	public static ShopManager Instance
	{
		get
		{
			if (ShopManager._ins == null)
			{
				ShopManager._ins = UnityEngine.Object.FindObjectOfType<ShopManager>();
			}
			return ShopManager._ins;
		}
	}

	protected void Awake()
	{
		this.isPurchasePackWarlock = CacheGame.IsPurchaseWarlockPack;
		this.isPurchasePackSSLightning = CacheGame.IsPurchaseSSLightningPack;
		this.CheckShowMisc();
		this.CheckCanWatchVideo();
		this.GetDataGemCoin();
		if (CacheGame.IsPurchasePremiumPack)
		{
			this.HideBtnBuyPremiumPack();
		}
		if (GameContext.isPurchaseStarterPack || GameContext.isDisableStarterPack)
		{
			this.objBtnShowStarterPack.SetActive(false);
		}
		else
		{
			this.CheckTimeStarterPack();
		}
	}

	private void Start()
	{
		this.CheckTimePremiumPack();
	}

	private void SetTextPremiumPack()
	{
		this.textFreeRespawnOncePerDay.text = ScriptLocalization.Free + " " + ScriptLocalization.respawn_once_per_day;
		this.textUnlimitedEnergy.text = ScriptLocalization.unlimited + " " + ScriptLocalization.energy;
		this.textRemoveAds.text = ScriptLocalization.remove + " " + ScriptLocalization.ads;
	}

	private void GetDataGemCoin()
	{
		this.textCoin.text = GameUtil.FormatNumber(ShopContext.currentCoin);
		this.textGem.text = GameUtil.FormatNumber(ShopContext.currentGem);
		this.textEnergy.text = GameContext.totalEnergy + "/" + GameContext.MAX_ENERGY;
	}

	public void BuyGem()
	{
		this.HidePopupNotEnoughCoinGem();
		this.ShowPopupShopGem(3);
	}

	public void BuyCoin()
	{
		this.HidePopupNotEnoughCoinGem();
		this.ShowPopupShopGem(2);
	}

	public void UpdateTextCoinGem()
	{
		this.textCoin.text = GameUtil.FormatNumber(ShopContext.currentCoin);
		this.textGem.text = GameUtil.FormatNumber(ShopContext.currentGem);
	}

	public void UpdateTextEnergy()
	{
		this.textEnergy.text = GameContext.totalEnergy + "/" + GameContext.MAX_ENERGY;
	}

	public void UpdateTextEnergy(int numberEnergy)
	{
		this.textEnergy.text = numberEnergy + "/" + GameContext.MAX_ENERGY;
	}

	public void ShowPopupNotEnoughCoin()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePopupNotEnoughCoinGem));
		this.iconCoin.SetActive(true);
		this.iconGem.SetActive(false);
		this.ShowPopup(this.popupNotEnoughCoinGem, "NOT_ENOUGH_COIN_GEM");
		this.btnBuy.onClick.RemoveAllListeners();
		this.btnBuy.onClick.AddListener(new UnityAction(this.BuyCoin));
	}

	public void ShowPopupNotEnoughGem()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePopupNotEnoughCoinGem));
		this.iconCoin.SetActive(false);
		this.iconGem.SetActive(true);
		this.ShowPopup(this.popupNotEnoughCoinGem, "NOT_ENOUGH_COIN_GEM");
		this.btnBuy.onClick.RemoveAllListeners();
		this.btnBuy.onClick.AddListener(new UnityAction(this.BuyGem));
	}

	public void HidePopupNotEnoughCoinGem()
	{
		this.HidePopup(this.popupNotEnoughCoinGem, "NOT_ENOUGH_COIN_GEM");
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupNotEnoughCoinGem));
	}

	private void ShowPopup(GameObject popup, string idTweening)
	{
		popup.SetActive(true);
		DOTween.Restart(idTweening, true, -1f);
		DOTween.Play(idTweening);
	}

	private void HidePopup(GameObject popup, string idTweening)
	{
		DOTween.PlayBackwards(idTweening);
		base.StartCoroutine(this.DelayHidePopup(popup));
	}

	private IEnumerator DelayHidePopup(GameObject popup)
	{
		yield return new WaitForSecondsRealtime(0.1f);
		popup.SetActive(false);
		yield break;
	}

	[EnumAction(typeof(ShopManager.TabShop))]
	public void ShowPopupShopGem(int tab)
	{
		if (!this.isShowPanelShopGem)
		{
			if (VIPManager.current.isShowPanelVIP)
			{
				this.needOpenVIP = true;
			}
			this.isShowPanelShopGem = true;
			this.ShowPopup(this.panelShopGem, "SHOP_GEM");
			this.SelectTab(tab);
			EscapeManager.Current.AddAction(new Action(this.HidePanelShopGem));
			if (ShopEnergy.current.isShowPopupEnergy)
			{
				ShopEnergy.current.HidePanelShopEnergy();
			}
		}
		else
		{
			this.SelectTab(tab);
		}
	}

	public void HidePanelShopGem()
	{
		this.isShowPanelShopGem = false;
		this.HidePopup(this.panelShopGem, "SHOP_GEM");
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelShopGem));
		if (this.needOpenVIP)
		{
			this.needOpenVIP = false;
			VIPManager.current.ShowPanelVIP();
		}
	}

	public void ShowPremiumPack()
	{
		this.SetTextPremiumPack();
		this.ShowPopup(this.panelPremiumPack, "PREMIUM_PACK");
		this.StartCoroutineCountdownPremium();
		EscapeManager.Current.AddAction(new Action(this.HidePremiumPack));
	}

	public void HidePremiumPack()
	{
		this.HidePopup(this.panelPremiumPack, "PREMIUM_PACK");
		this.StopCoroutineCountdownPremium();
		EscapeManager.Current.RemoveAction(new Action(this.HidePremiumPack));
	}

	public void ShowStarterPack()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePopupStarterPack));
		this.ShowPopup(this.panelStarterPack, "STARTER_PACK");
	}

	public void HidePopupStarterPack()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupStarterPack));
		this.HidePopup(this.panelStarterPack, "STARTER_PACK");
	}

	public void HideButtonBuyStarterPack()
	{
		this.btnBuyStarterPack.SetActive(false);
	}

	public void ShowInfoPlaneStarterPack()
	{
		this.showInfoPlaneDrone.ShowInfoPlane(GameContext.Plane.SkyWraith);
	}

	public void ShowInfoDrone1StarterPack()
	{
		this.showInfoPlaneDrone.ShowInfoDrone(GameContext.Drone.GatlingGun);
	}

	public void ShowInfoDrone2StarterPack()
	{
		this.showInfoPlaneDrone.ShowInfoDrone(GameContext.Drone.Laser);
	}

	public void ShowPanelBuyStarterPackSuccess()
	{
		this.HidePopupStarterPack();
		this.ShowPopup(this.panelBuyStartePackSuccess, "BUY_STARTER_PACK_SUCCESS");
		EscapeManager.Current.AddAction(new Action(this.HidePanelBuyStarterPackSuccess));
	}

	public void HidePanelBuyStarterPackSuccess()
	{
		this.HidePopup(this.panelBuyStartePackSuccess, "BUY_STARTER_PACK_SUCCESS");
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelBuyStarterPackSuccess));
	}

	public void ShowPanelBuyPremiumPackSuccess()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePanelBuyPremiumPackSuccess));
		this.ShowPopup(this.panelBuyPremiumPackSuccess, "BUY_PREMIUM_PACK_SUCCESS");
		this.HideBtnBuyPremiumPack();
	}

	public void HidePanelBuyPremiumPackSuccess()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelBuyPremiumPackSuccess));
		this.HidePopup(this.panelBuyPremiumPackSuccess, "BUY_PREMIUM_PACK_SUCCESS");
	}

	public void HideBtnBuyPremiumPack()
	{
		this.btnBuyPremiumPack.SetActive(false);
		this.objYouArePremium.SetActive(true);
	}

	public void ShowBtnBuyPremiumPack()
	{
		this.btnBuyPremiumPack.SetActive(true);
		this.objYouArePremium.SetActive(false);
	}

	private IEnumerator CountTo(int numberStart, int target)
	{
		this.isCounting = true;
		int start = numberStart;
		for (float timer = 0f; timer <= this.duration; timer += Time.deltaTime)
		{
			float progress = timer / this.duration;
			int energy = (int)Mathf.Lerp((float)start, (float)target, progress);
			this.textCoin.text = GameUtil.FormatNumber(energy);
			yield return null;
		}
		numberStart = target;
		this.isCounting = false;
		this.textCoin.text = GameUtil.FormatNumber(target);
		yield break;
	}

	public void StartCountTo(int numberCoins)
	{
		int target = ShopContext.currentCoin + numberCoins;
		base.StartCoroutine(this.CountTo(ShopContext.currentCoin, target));
	}

	public void BuyCoinPack1()
	{
		if (!this.isCounting)
		{
			if (ShopContext.currentGem >= 10)
			{
				this.StartCountTo(1000);
				CacheGame.AddCoins(1000);
				CacheGame.MinusGems(10);
				CurrencyLog.LogGemOut(10, CurrencyLog.Out.Shop, "BuyCoinPack1");
				CurrencyLog.LogGoldIn(1000, CurrencyLog.In.GemToCoin, "BuyCoinPack1");
				Notification.Current.ShowNotification("Coins +1,000");
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.notify_you_need_buy_more);
			}
		}
	}

	public void BuyCoinPack2()
	{
		if (!this.isCounting)
		{
			if (ShopContext.currentGem >= 200)
			{
				this.StartCountTo(25000);
				CacheGame.AddCoins(25000);
				CacheGame.MinusGems(200);
				CurrencyLog.LogGemOut(200, CurrencyLog.Out.Shop, "BuyCoinPack2");
				CurrencyLog.LogGoldIn(25000, CurrencyLog.In.GemToCoin, "BuyCoinPack2");
				Notification.Current.ShowNotification("Coins +25,000");
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.notify_you_need_buy_more);
			}
		}
	}

	public void BuyCoinPack3()
	{
		if (!this.isCounting)
		{
			if (ShopContext.currentGem >= 1000)
			{
				this.StartCountTo(150000);
				CacheGame.AddCoins(150000);
				CacheGame.MinusGems(1000);
				CurrencyLog.LogGemOut(1000, CurrencyLog.Out.Shop, "BuyCoinPack3");
				CurrencyLog.LogGoldIn(150000, CurrencyLog.In.GemToCoin, "BuyCoinPack3");
				Notification.Current.ShowNotification("Coins +150,000");
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.notify_you_need_buy_more);
			}
		}
	}

	[EnumAction(typeof(ShopManager.TabShop))]
	public void SelectTab(int tabID)
	{
		if (this.currentTabID != tabID)
		{
			if (this.currentTabID != -1)
			{
				this.arrShop[this.currentTabID].HideTab();
			}
			this.currentTabID = tabID;
			this.arrShop[tabID].ShowTab();
		}
	}

	private void CheckPackNonComsumable()
	{
	}

	private void CheckCanWatchVideo()
	{
		if (this.canWatchVideo)
		{
			this.imgBtnWatchVideo.sprite = this.sprBtnEnable;
			this.textTimeCountDown.text = ScriptLocalization.watch;
		}
	}

	private IEnumerator CountDown()
	{
		while (this.timeCountDown > 0f)
		{
			string textFormat = TimeSpan.FromSeconds((double)this.timeCountDown).ToString("mm\\:ss");
			this.textTimeCountDown.text = textFormat;
			yield return new WaitForSecondsRealtime(1f);
			this.timeCountDown -= 1f;
		}
		this.textTimeCountDown.text = ScriptLocalization.watch;
		this.canWatchVideo = true;
		this.isGetReward = false;
		this.imgBtnWatchVideo.sprite = this.sprBtnEnable;
		yield break;
	}

	public void WatchVideoToGetCoin()
	{
		if (this.canWatchVideo)
		{
			GameContext.actionVideo = GameContext.ActionVideoAds.Get_Coin;
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.please_wait_a_moment);
		}
	}

	public void WatchVideoToGetCoinComplete()
	{
		if (!this.isGetReward)
		{
			this.isGetReward = true;
			this.canWatchVideo = false;
			this.timeCountDown = 300f;
			int num = UnityEngine.Random.Range(900, 1001);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, num);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			CacheGame.AddCoins(num);
			CurrencyLog.LogGoldIn(num, CurrencyLog.In.WatchVideo, "Shop_Coins");
			this.imgBtnWatchVideo.sprite = this.sprBtnDisable;
			base.StartCoroutine(this.CountDown());
		}
	}

	public void MiscBuyOverdrive()
	{
		if (ShopContext.currentGem >= 50)
		{
			CacheGame.MinusGems(50);
			CurrencyLog.LogGemOut(50, CurrencyLog.Out.Shop, "MiscBuyOverdrive");
			GameContext.totalSkillPlane += 30;
			CacheGame.AddSkillPlane(30);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Special_Skill, 10);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
		}
		else
		{
			this.ShowPopupNotEnoughGem();
		}
	}

	public void MiscBuyHeart()
	{
		if (ShopContext.currentGem >= 50)
		{
			CacheGame.MinusGems(50);
			CurrencyLog.LogGemOut(50, CurrencyLog.Out.Shop, "MiscBuyHeart");
			GameContext.totalItemPowerUpHeart += 10;
			CacheGame.NumberItemPowerHeart = GameContext.totalItemPowerUpHeart;
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Heart, 10);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
		}
		else
		{
			this.ShowPopupNotEnoughGem();
		}
	}

	public void MiscBuyShield()
	{
		if (ShopContext.currentGem >= 45)
		{
			CacheGame.MinusGems(45);
			CurrencyLog.LogGemOut(45, CurrencyLog.Out.Shop, "MiscBuyShield");
			GameContext.totalItemPowerShield += 10;
			CacheGame.NumberItemPowerShield = GameContext.totalItemPowerShield;
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Shield, 10);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
		}
		else
		{
			this.ShowPopupNotEnoughGem();
		}
	}

	public void MiscBuyPower10()
	{
		if (ShopContext.currentGem >= 90)
		{
			CacheGame.MinusGems(45);
			CurrencyLog.LogGemOut(45, CurrencyLog.Out.Shop, "MiscBuyPower10");
			GameContext.totalItemPowerUpBullet10 += 10;
			CacheGame.NumberItemPowerUpBullet10 = GameContext.totalItemPowerUpBullet10;
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Bullet_10, 10);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
		}
		else
		{
			this.ShowPopupNotEnoughGem();
		}
	}

	private void CheckShowMisc()
	{
		if (CacheGame.IsPurchaseUltraStarshipCard)
		{
			this.objMiscUltraStarshipCard.SetActive(false);
		}
		if (CacheGame.IsPurchaseUltraDroneCard)
		{
			this.objMiscUltraDroneCard.SetActive(false);
		}
		this.CheckPackOneTime();
	}

	private void CheckTimeStarterPack()
	{
		if (CacheGame.EndTimeStarterPack == string.Empty)
		{
			CacheGame.EndTimeStarterPack = DateTime.Now.AddDays((double)this.numberDayActiveStarterPack).ToBinary().ToString();
		}
		long dateData = Convert.ToInt64(CacheGame.EndTimeStarterPack);
		DateTime currentTime = DateTime.FromBinary(dateData);
		this.timeStarterPack = (float)GameContext.GetTimeSpan(currentTime, DateTime.Now).TotalSeconds;
		if (this.timeStarterPack > 0f)
		{
			base.StartCoroutine(this.CoroutineCountdownStarterPack());
		}
		else
		{
			CacheGame.IsDisableStarterPack = true;
			this.btnBuyStarterPack.SetActive(false);
		}
	}

	private IEnumerator CoroutineCountdownStarterPack()
	{
		while (this.timeStarterPack > 0f)
		{
			TimeSpan time = TimeSpan.FromSeconds((double)this.timeStarterPack);
			if (time.TotalDays >= 1.0)
			{
				this.textTimeStarterPack.text = string.Concat(new object[]
				{
					(int)time.TotalDays,
					"d ",
					time.Hours,
					"h"
				});
			}
			else
			{
				string text = time.ToString("hh\\:mm\\:ss");
				this.textTimeStarterPack.text = text;
			}
			yield return new WaitForSecondsRealtime(1f);
			this.timeStarterPack -= 1f;
		}
		CacheGame.IsDisableStarterPack = true;
		this.btnBuyStarterPack.SetActive(false);
		yield break;
	}

	private void CheckPackOneTime()
	{
		if (GameContext.isPurchaseStarterPack || GameContext.isDisableStarterPack)
		{
			this.objStarterPack.SetActive(false);
		}
		if (CacheGame.IsPurchasePackOneTime)
		{
			this.objOneTimePack.SetActive(false);
		}
	}

	public void CheckTimePremiumPack()
	{
		if (GameContext.isPurchasePremiumPack)
		{
			long dateData = Convert.ToInt64(CacheGame.EndTimePremiumPack);
			this.endTimeStaterPack = DateTime.FromBinary(dateData);
			this.timePremiumPack = GameContext.GetTimeSpan(this.endTimeStaterPack, DateTime.Now).TotalSeconds;
			if (this.timePremiumPack <= 0.0)
			{
				CacheGame.IsPurchasePremiumPack = false;
				GameContext.isPurchasePremiumPack = false;
			}
			if (HomeManager.current != null)
			{
				HomeManager.current.StartCoroutineCountdownPremium(this.timePremiumPack);
			}
		}
	}

	public void StartCoroutineCountdownPremium()
	{
		if (GameContext.isPurchasePremiumPack)
		{
			long dateData = Convert.ToInt64(CacheGame.EndTimePremiumPack);
			this.endTimeStaterPack = DateTime.FromBinary(dateData);
			this.timePremiumPack = GameContext.GetTimeSpan(this.endTimeStaterPack, DateTime.Now).TotalSeconds;
			base.StartCoroutine("CoroutineCountdownPremium");
		}
	}

	public void StopCoroutineCountdownPremium()
	{
		if (GameContext.isPurchasePremiumPack)
		{
			base.StopCoroutine("CoroutineCountdownPremium");
		}
	}

	private IEnumerator CoroutineCountdownPremium()
	{
		while (this.timePremiumPack > 0.0)
		{
			TimeSpan time = TimeSpan.FromSeconds(this.timePremiumPack);
			if (time.TotalDays >= 1.0)
			{
				this.textTimePremiumPack.text = string.Concat(new object[]
				{
					(int)time.TotalDays,
					"d ",
					time.Hours,
					"h"
				});
			}
			else if (time.TotalHours >= 1.0)
			{
				this.textTimePremiumPack.text = string.Concat(new object[]
				{
					(int)time.TotalSeconds,
					"h ",
					time.Minutes,
					"m"
				});
			}
			else
			{
				this.textTimePremiumPack.text = string.Concat(new object[]
				{
					(int)time.TotalMinutes,
					"m ",
					time.Seconds,
					"s"
				});
			}
			yield return new WaitForSecondsRealtime(1f);
			this.timePremiumPack -= 1.0;
		}
		CacheGame.IsPurchasePremiumPack = false;
		GameContext.isPurchasePremiumPack = false;
		this.ShowBtnBuyPremiumPack();
		yield break;
	}

	public static ShopManager _ins;

	public Text textCoin;

	public Text textGem;

	public Text textEnergy;

	public GameObject popupNotEnoughCoinGem;

	public GameObject panelShopGem;

	public GameObject panelPremiumPack;

	public GameObject panelStarterPack;

	public GameObject panelBuyStartePackSuccess;

	public GameObject panelBuyPremiumPackSuccess;

	public GameObject iconGem;

	public GameObject iconCoin;

	public GameObject btnBuyStarterPack;

	public GameObject btnBuyPremiumPack;

	public GameObject objYouArePremium;

	public Button btnBuy;

	public ShowInfoPlaneDrone showInfoPlaneDrone;

	public Image imgBtnWatchVideo;

	public Sprite sprBtnEnable;

	public Sprite sprBtnDisable;

	public Text textTimeCountDown;

	public Text textTitlePackWatchVideo;

	public GameObject objMiscUltraStarshipCard;

	public GameObject objMiscUltraDroneCard;

	public GameObject objBtnShowStarterPack;

	public GameObject objBtnShowPremiumPack;

	[HideInInspector]
	public float timeStarterPack;

	public Text textTimeStarterPack;

	public Text textTimePremiumPack;

	public GameObject objStarterPack;

	public GameObject objOneTimePack;

	public Text textFreeRespawnOncePerDay;

	public Text textUnlimitedEnergy;

	public Text textRemoveAds;

	private float timeCountDown = 300f;

	private int numberDayActiveStarterPack = 3;

	public ShopManager.Shop[] arrShop;

	private int[] pricePackCoins;

	private bool isPurchasePackWarlock;

	private bool isPurchasePackSSLightning;

	private bool needOpenVIP;

	[HideInInspector]
	public bool isShowPanelShopGem;

	private float duration = 0.5f;

	private bool isCounting;

	private int currentTabID = -1;

	private bool canWatchVideo = true;

	private bool isGetReward;

	private double timePremiumPack;

	private DateTime endTimeStaterPack;

	[Serializable]
	public class Shop
	{
		public void ShowTab()
		{
			this.objTab.SetActive(true);
			this.imbtBtnTab.sprite = this.sprBtnTabSelect;
		}

		public void HideTab()
		{
			this.objTab.SetActive(false);
			this.imbtBtnTab.sprite = this.sprBtnTab;
		}

		public ShopManager.TabShop tabShop;

		public GameObject objTab;

		public Image imbtBtnTab;

		public Sprite sprBtnTab;

		public Sprite sprBtnTabSelect;
	}

	public enum TabShop
	{
		Pack,
		Misc,
		Coin,
		Gem
	}
}
