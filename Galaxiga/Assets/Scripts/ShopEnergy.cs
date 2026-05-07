using System;
using System.Collections;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class ShopEnergy : MonoBehaviour
{
	public static ShopEnergy current
	{
		get
		{
			if (ShopEnergy._current == null)
			{
				ShopEnergy._current = UnityEngine.Object.FindObjectOfType<ShopEnergy>();
			}
			return ShopEnergy._current;
		}
	}

	private float timeRecovery1Energy
	{
		get
		{
			if (GameContext.currentVIP == 3)
			{
				return 0.125f;
			}
			if (GameContext.currentVIP == 4)
			{
				return 0.25f;
			}
			if (GameContext.currentVIP == 5)
			{
				return 0.375f;
			}
			if (GameContext.currentVIP == 6)
			{
				return 0.5f;
			}
			if (GameContext.currentVIP == 7)
			{
				return 0.625f;
			}
			if (GameContext.currentVIP == 8)
			{
				return 0.75f;
			}
			if (GameContext.currentVIP == 9)
			{
				return 0.875f;
			}
			if (GameContext.currentVIP == 10)
			{
				return 1f;
			}
			if (GameContext.currentVIP == 12)
			{
				return 1.25f;
			}
			return 5f;
		}
	}

	private void Awake()
	{
		this.numberBuyEnergy1 = CacheGame.NumberBuyEnergy1;
		this.numberWatchVideoToGetEnergy = CacheGame.NumberWatchVideoGetEnergy;
		this.GetDataEnergy();
		if (GameContext.isPurchasePremiumPack)
		{
			this.ActiveIconInfinity();
		}
		else
		{
			this.textBuyPremium.text = ScriptLocalization.buy_premium_to_get_unlimited_energy;
			this.objInfinity.SetActive(false);
			this.objTextEnergy.SetActive(true);
		}
		this.CheckRecoveryEnergy();
		this.actionGemChange = delegate(Component sender, object param)
		{
			this.CheckGem();
		};
		EventDispatcher.Instance.RegisterListener(EventID.Gem, this.actionGemChange);
		this.SetText();
	}

	public void ActiveIconInfinity()
	{
		this.textBuyPremium.gameObject.SetActive(false);
		this.textPremium.text = ScriptLocalization.purchased;
		this.objInfinity.SetActive(true);
		this.objTextEnergy.SetActive(false);
	}

	private void GetDataEnergy()
	{
		if (ShopEnergy.dataEnergy1 == null)
		{
			ShopEnergy.dataEnergy1 = new ShopEnergy.DataEnergy(ShopEnergyDataSheet.Get(1).numberEnergy, ShopEnergyDataSheet.Get(1).numberAvailbale, ShopEnergyDataSheet.Get(1).price0, ShopEnergyDataSheet.Get(1).price1, ShopEnergyDataSheet.Get(1).price2, ShopEnergyDataSheet.Get(1).price3);
		}
	}

	private void CheckGem()
	{
		if (GameContext.isPurchasePremiumPack)
		{
			return;
		}
		if (ShopContext.currentGem >= this.currentPriceBuyEnergy1)
		{
			this.imgBtnBuyEnergy1.sprite = this.sprBtn;
		}
		else
		{
			this.imgBtnBuyEnergy1.sprite = this.sprBtnUnvailable;
		}
		if (this.numberWatchVideoToGetEnergy < 3)
		{
			this.imgBtnWatchVideo.sprite = this.sprBtn;
		}
		else
		{
			this.imgBtnWatchVideo.sprite = this.sprBtnUnvailable;
		}
	}

	private void SetText()
	{
		this.textNumberEnergy1.text = "+" + ShopEnergy.dataEnergy1.numberEnergy;
	}

	public void BuyEnergy1()
	{
		if (!this.isCounting)
		{
			if (this.numberBuyEnergy1 < ShopEnergy.dataEnergy1.numberAvailable)
			{
				if (ShopContext.currentGem >= this.currentPriceBuyEnergy1)
				{
					this.imgBtnBuyEnergy1.sprite = this.sprBtn;
					this.numberBuyEnergy1++;
					CacheGame.MinusGems(this.currentPriceBuyEnergy1);
					CurrencyLog.LogGemOut(this.currentPriceBuyEnergy1, CurrencyLog.Out.BuyEnergy, "Buy_Energy");
					this.SetPriceEnergy1();
					CacheGame.NumberBuyEnergy1 = this.numberBuyEnergy1;
					this.textAvailable1.text = ShopEnergy.dataEnergy1.numberAvailable - this.numberBuyEnergy1 + "/" + ShopEnergy.dataEnergy1.numberAvailable;
					this.StartCountTo(ShopEnergy.dataEnergy1.numberEnergy);
					CacheGame.AddEnergy(ShopEnergy.dataEnergy1.numberEnergy);
					if (!GameContext.isGameReady && RewardStageManager.current != null)
					{
						RewardStageManager.current.CheckPlayGame();
					}
				}
				else
				{
					ShopManager.Instance.ShowPopupNotEnoughGem();
					this.imgBtnBuyEnergy1.sprite = this.sprBtnUnvailable;
				}
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.notify_can_not_buy_more);
			}
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_waiting_for_buy);
		}
	}

	public void ShowPanelPremium()
	{
		ShopManager.Instance.ShowPremiumPack();
	}

	private void SetPriceEnergy1()
	{
		switch (this.numberBuyEnergy1)
		{
		case 1:
			this.currentPriceBuyEnergy1 = ShopEnergy.dataEnergy1.price1;
			break;
		case 2:
			this.currentPriceBuyEnergy1 = ShopEnergy.dataEnergy1.price2;
			break;
		case 3:
			this.currentPriceBuyEnergy1 = ShopEnergy.dataEnergy1.price3;
			break;
		default:
			this.currentPriceBuyEnergy1 = ShopEnergy.dataEnergy1.price0;
			break;
		}
		this.textPrice1.text = string.Empty + this.currentPriceBuyEnergy1;
	}

	public void WatchVideoToGetEnergy()
	{
		if (this.numberWatchVideoToGetEnergy < 3)
		{
			GameContext.actionVideo = GameContext.ActionVideoAds.Get_Energy;
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_you_cant_do_this);
		}
	}

	public void WatchVideoToGetEneryComplete()
	{
		if (!this.isGetReward)
		{
			this.isGetReward = true;
			this.StartCountTo(30);
			CacheGame.AddEnergy(30);
			this.numberWatchVideoToGetEnergy++;
			CacheGame.NumberWatchVideoGetEnergy = this.numberWatchVideoToGetEnergy;
			this.textAvailabeWatchVideo.text = 3 - this.numberWatchVideoToGetEnergy + "/3";
		}
	}

	public void ShowPanelShopEnergy()
	{
		if (!this.isShowPopupEnergy)
		{
			this.isShowPopupEnergy = true;
			if (GameContext.isPurchasePremiumPack)
			{
				this.ActiveIconInfinity();
			}
			else
			{
				this.textBuyPremium.text = ScriptLocalization.buy_premium_to_get_unlimited_energy;
				this.objInfinity.SetActive(false);
				this.objTextEnergy.SetActive(true);
			}
			this.timerDayleft = (float)CheckNewDay.Current.CurrentSecondDayleft();
			base.StartCoroutine("CoroutineShopEnergyCountdown");
			this.SetPriceEnergy1();
			this.CheckGem();
			this.textAvailable1.text = ShopEnergy.dataEnergy1.numberAvailable - this.numberBuyEnergy1 + "/" + ShopEnergy.dataEnergy1.numberAvailable;
			this.textAvailabeWatchVideo.text = 3 - this.numberWatchVideoToGetEnergy + "/3";
			if (ShopManager.Instance.isShowPanelShopGem)
			{
				ShopManager.Instance.HidePanelShopGem();
			}
			this.panelEnergyShop.SetActive(true);
			DOTween.Restart("SHOP_ENERGY", true, -1f);
			DOTween.Play("SHOP_ENERGY");
			EscapeManager.Current.AddAction(new Action(this.HidePanelShopEnergy));
		}
		else
		{
			this.HidePanelShopEnergy();
		}
	}

	public void HidePanelShopEnergy()
	{
		this.isShowPopupEnergy = false;
		base.StopCoroutine("CoroutineShopEnergyCountdown");
		DOTween.PlayBackwards("SHOP_ENERGY");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.panelEnergyShop.SetActive(false);
		}));
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelShopEnergy));
	}

	private IEnumerator CountTo(int target)
	{
		this.isCounting = true;
		int start = GameContext.totalEnergy;
		for (float timer = 0f; timer <= this.duration; timer += Time.deltaTime)
		{
			float progress = timer / this.duration;
			int energy = (int)Mathf.Lerp((float)start, (float)target, progress);
			ShopManager.Instance.UpdateTextEnergy(energy);
			yield return null;
		}
		GameContext.totalEnergy = target;
		this.isCounting = false;
		this.isGetReward = false;
		ShopManager.Instance.UpdateTextEnergy();
		yield break;
	}

	public void StartCountTo(int numberEnergy)
	{
		if (Time.timeScale < 1f)
		{
			CacheGame.AddEnergy(numberEnergy);
			ShopManager.Instance.UpdateTextEnergy(GameContext.totalEnergy);
			this.isGetReward = false;
		}
		else
		{
			int target = GameContext.totalEnergy + numberEnergy;
			base.StartCoroutine(this.CountTo(target));
		}
	}

	public void AddEnergy(int numberEnergy)
	{
		int target = GameContext.totalEnergy + numberEnergy;
		base.StartCoroutine(this.CountTo(target));
		CacheGame.AddEnergy(numberEnergy);
	}

	private void SetTime()
	{
		this.timer -= Time.deltaTime;
		if (this.timer <= 0f)
		{
			this.timer = 0f;
			this.AddEnergy(1);
			if (GameContext.totalEnergy >= GameContext.MAX_ENERGY)
			{
				this.isSetTime = false;
			}
			else
			{
				this.timer = this.timeRecovery1Energy * 60f;
			}
		}
		else
		{
			int num = Mathf.FloorToInt(this.timer / 60f);
			int num2 = Mathf.FloorToInt(this.timer - (float)(num * 60));
			string text = string.Format("{0:0}:{1:00}", num, num2);
			this.textTime.text = text;
		}
	}

	private void ShowDayleft()
	{
		this.timerDayleft -= Time.deltaTime;
		if (this.timerDayleft <= 0f)
		{
			this.timerDayleft = 86400f;
			CheckNewDay.Current.NewDay();
		}
		else
		{
			int num = (int)this.timerDayleft / 3600;
			int num2 = (int)this.timerDayleft % 3600 / 60;
			int num3 = (int)this.timerDayleft % 3600 % 60;
			string text = string.Format("{0:0}:{1:00}:{2:0}", num, num2, num3);
			this.textTimeDayleft.text = text;
		}
	}

	private void CheckRecoveryEnergy()
	{
		if (GameContext.totalEnergy >= GameContext.MAX_ENERGY)
		{
			this.textTime.text = "00:00";
			this.isSetTime = false;
			return;
		}
		int secondRecoveryEnergy = CacheGame.SecondRecoveryEnergy;
		if (GameContext.totalEnergy >= GameContext.MAX_ENERGY)
		{
			this.textTime.text = "00:00";
			this.isSetTime = false;
		}
		else
		{
			TimeSpan timeSpan = GameContext.GetTimeSpan(DateTime.Now, GameContext.lastTimeQuitGame);
			double totalMinutes = timeSpan.TotalMinutes;
			if (totalMinutes >= (double)this.timeRecovery1Energy)
			{
				float num = (float)totalMinutes / this.timeRecovery1Energy;
				UnityEngine.Debug.Log((float)totalMinutes % this.timeRecovery1Energy);
				int num2 = GameContext.totalEnergy + (int)num;
				if (num2 >= GameContext.MAX_ENERGY)
				{
					this.textTime.text = "00:00";
					this.isSetTime = false;
					CacheGame.SetTotalEnergy(GameContext.MAX_ENERGY);
				}
				else
				{
					this.isSetTime = true;
					CacheGame.SetTotalEnergy(num2);
					this.timer = this.timeRecovery1Energy * 60f;
					base.StartCoroutine("CoroutineRecoveryEnergyCountdown");
				}
			}
			else
			{
				this.isSetTime = true;
				double totalSeconds = timeSpan.TotalSeconds;
				this.timer = (float)secondRecoveryEnergy - (float)totalSeconds;
				base.StartCoroutine("CoroutineRecoveryEnergyCountdown");
			}
		}
	}

	private IEnumerator CoroutineShopEnergyCountdown()
	{
		while (this.timerDayleft > 0f)
		{
			string textFormat = TimeSpan.FromSeconds((double)this.timerDayleft).ToString("hh\\:mm\\:ss");
			this.textTimeDayleft.text = textFormat;
			yield return new WaitForSecondsRealtime(1f);
			this.timerDayleft -= 1f;
		}
		this.timerDayleft = 86400f;
		CheckNewDay.Current.NewDay();
		base.StartCoroutine("CoroutineShopEnergyCountdown");
		yield break;
	}

	private IEnumerator CoroutineRecoveryEnergyCountdown()
	{
		while (this.timer > 0f)
		{
			string textFormat = TimeSpan.FromSeconds((double)this.timer).ToString("mm\\:ss");
			this.textTime.text = textFormat;
			yield return new WaitForSecondsRealtime(1f);
			this.timer -= 1f;
		}
		if (GameContext.totalEnergy < GameContext.MAX_ENERGY)
		{
			this.AddEnergy(1);
		}
		this.timer = this.timeRecovery1Energy * 60f;
		if (GameContext.totalEnergy >= GameContext.MAX_ENERGY)
		{
			if (this.coroutineCountdown != null)
			{
				base.StopCoroutine(this.coroutineCountdown);
			}
		}
		else
		{
			if (this.coroutineCountdown != null)
			{
				base.StopCoroutine(this.coroutineCountdown);
			}
			this.coroutineCountdown = base.StartCoroutine(this.CoroutineRecoveryEnergyCountdown());
		}
		yield break;
	}

	private void OnApplicationQuit()
	{
		CacheGame.SecondRecoveryEnergy = (int)this.timer;
	}

	public void CheckEnergy()
	{
		if (GameContext.totalEnergy < GameContext.MAX_ENERGY)
		{
			this.isSetTime = true;
			this.timer = this.timeRecovery1Energy * 60f;
			this.coroutineCountdown = base.StartCoroutine(this.CoroutineRecoveryEnergyCountdown());
		}
	}

	public static ShopEnergy _current;

	public GameObject panelEnergyShop;

	public Text textNumberEnergy1;

	public Text textAvailable1;

	public Text textAvailabeWatchVideo;

	public Text textPrice1;

	public Text textTime;

	public Text textTimeDayleft;

	public Text textBuyPremium;

	public Text textPremium;

	public Image imgBtnBuyEnergy1;

	public Image imgBtnWatchVideo;

	public Sprite sprBtn;

	public Sprite sprBtnUnvailable;

	public GameObject objInfinity;

	public GameObject objTextEnergy;

	private int numberBuyEnergy1;

	private int numberWatchVideoToGetEnergy;

	private int currentPriceBuyEnergy1;

	private int currentPriceBuyEnergy2;

	private Action<Component, object> actionGemChange;

	private static ShopEnergy.DataEnergy dataEnergy1;

	private bool isSetTime;

	private DateTime lastTimeQuitGame;

	private bool isGetReward;

	[HideInInspector]
	public bool isShowPopupEnergy;

	private float duration = 0.5f;

	private bool isCounting;

	private float timer;

	private float timerDayleft;

	private Coroutine coroutineCountdown;

	private class DataEnergy
	{
		public DataEnergy(int _numberEnergy, int _numberAvailable, int _price0, int _price1, int _price2, int _price3)
		{
			this.numberEnergy = _numberEnergy;
			this.price0 = _price0;
			this.price1 = _price1;
			this.price2 = _price2;
			this.price3 = _price3;
			switch (GameContext.currentVIP)
			{
			case 0:
				this.numberAvailable = _numberAvailable;
				break;
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
				this.numberAvailable = 4;
				break;
			case 9:
				this.numberAvailable = 5;
				break;
			case 10:
			case 11:
			case 12:
				this.numberAvailable = 6;
				break;
			}
		}

		public int numberEnergy;

		public int numberAvailable;

		public int price0;

		public int price1;

		public int price2;

		public int price3;
	}
}
