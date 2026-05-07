using System;
using DG.Tweening;
using I2.Loc;
using SnapScrollView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VIPManager : MonoBehaviour
{
	public static VIPManager current
	{
		get
		{
			if (VIPManager._ins == null)
			{
				VIPManager._ins = UnityEngine.Object.FindObjectOfType<VIPManager>();
			}
			return VIPManager._ins;
		}
	}

	private void Awake()
	{
		this.CheckVip();
	}

	private void Start()
	{
		this.SetTextInfoNextVIP();
		this.SetImageBtnClaimDailyGiftVIP();
	}

	public void CheckVip()
	{
		if (GameContext.currentVIPPoint >= this.arrVIPs[0].vipPoint && GameContext.currentVIPPoint < this.arrVIPs[1].vipPoint)
		{
			GameContext.currentVIP = 1;
		}
		else if (GameContext.currentVIPPoint >= this.arrVIPs[1].vipPoint && GameContext.currentVIPPoint < this.arrVIPs[2].vipPoint)
		{
			GameContext.currentVIP = 2;
		}
		else if (GameContext.currentVIPPoint >= this.arrVIPs[2].vipPoint && GameContext.currentVIPPoint < this.arrVIPs[3].vipPoint)
		{
			GameContext.currentVIP = 3;
		}
		else if (GameContext.currentVIPPoint >= this.arrVIPs[3].vipPoint && GameContext.currentVIPPoint < this.arrVIPs[4].vipPoint)
		{
			GameContext.currentVIP = 4;
		}
		else if (GameContext.currentVIPPoint >= this.arrVIPs[4].vipPoint && GameContext.currentVIPPoint < this.arrVIPs[5].vipPoint)
		{
			GameContext.currentVIP = 5;
		}
		else if (GameContext.currentVIPPoint >= this.arrVIPs[5].vipPoint && GameContext.currentVIPPoint < this.arrVIPs[6].vipPoint)
		{
			GameContext.currentVIP = 6;
		}
		else if (GameContext.currentVIPPoint >= this.arrVIPs[6].vipPoint && GameContext.currentVIPPoint < this.arrVIPs[7].vipPoint)
		{
			GameContext.currentVIP = 7;
		}
		else if (GameContext.currentVIPPoint >= this.arrVIPs[7].vipPoint && GameContext.currentVIPPoint < this.arrVIPs[8].vipPoint)
		{
			GameContext.currentVIP = 8;
		}
		else if (GameContext.currentVIPPoint >= this.arrVIPs[8].vipPoint && GameContext.currentVIPPoint < this.arrVIPs[9].vipPoint)
		{
			GameContext.currentVIP = 9;
		}
		else if (GameContext.currentVIPPoint >= this.arrVIPs[9].vipPoint && GameContext.currentVIPPoint < this.arrVIPs[10].vipPoint)
		{
			GameContext.currentVIP = 10;
		}
		else if (GameContext.currentVIPPoint >= this.arrVIPs[10].vipPoint && GameContext.currentVIPPoint < this.arrVIPs[11].vipPoint)
		{
			GameContext.currentVIP = 11;
		}
		else if (GameContext.currentVIPPoint >= this.arrVIPs[11].vipPoint)
		{
			GameContext.currentVIP = 12;
		}
		this.textCurrentVIP.text = "VIP " + GameContext.currentVIP;
		UnityEngine.Debug.Log("Current VIP " + GameContext.currentVIP);
	}

	private void ScrollSnapLerpComplete()
	{
		this.arrVIPs[this.currentIndexPage].oneTimeRewardVip.HideObjReward();
		this.currentIndexPage = this.scrollSnap.cellIndex;
		this.canCollectReward = SaveDataStateClaimRewardVIP.StateItem(this.currentIndexPage);
		this.SetRewardVIP();
		UnityEngine.Debug.Log("current index page " + this.currentIndexPage);
	}

	private void MoveToVIP()
	{
		if (GameContext.currentVIP > 0)
		{
			this.currentIndexPage = GameContext.currentVIP - 1;
		}
		else
		{
			this.currentIndexPage = 0;
		}
		this.scrollSnap.MoveToIndex(this.currentIndexPage);
		this.canCollectReward = SaveDataStateClaimRewardVIP.StateItem(this.currentIndexPage);
		this.SetRewardVIP();
	}

	public void NextPage()
	{
		this.currentIndexPage = this.scrollSnap.cellIndex;
		if (this.currentIndexPage < this.maxPage)
		{
			this.arrVIPs[this.currentIndexPage].oneTimeRewardVip.HideObjReward();
			this.currentIndexPage++;
			this.scrollSnap.MoveToIndex(this.currentIndexPage);
			this.canCollectReward = SaveDataStateClaimRewardVIP.StateItem(this.currentIndexPage);
			this.SetRewardVIP();
		}
	}

	public void PrevPage()
	{
		this.currentIndexPage = this.scrollSnap.cellIndex;
		if (this.currentIndexPage > 0)
		{
			this.arrVIPs[this.currentIndexPage].oneTimeRewardVip.HideObjReward();
			this.currentIndexPage--;
			this.scrollSnap.MoveToIndex(this.currentIndexPage);
			this.canCollectReward = SaveDataStateClaimRewardVIP.StateItem(this.currentIndexPage);
			this.SetRewardVIP();
		}
	}

	public void SetRewardVIP()
	{
		if (GameContext.currentVIP >= this.currentIndexPage + 1)
		{
			if (this.canCollectReward)
			{
				this.imgBtnClaim.sprite = this.sprBtn;
				this.textClaim.text = ScriptLocalization.Claim;
				this.objRedNotificationClaimRewardOneTime.SetActive(true);
			}
			else
			{
				this.imgBtnClaim.sprite = this.sprBtnDisable;
				this.textClaim.text = ScriptLocalization.Claimed;
				this.objRedNotificationClaimRewardOneTime.SetActive(false);
			}
		}
		else
		{
			this.imgBtnClaim.sprite = this.sprBtnDisable;
			this.textClaim.text = ScriptLocalization.Claim;
			this.objRedNotificationClaimRewardOneTime.SetActive(false);
		}
		this.rewardGem.SetView2(this.arrVIPs[this.currentIndexPage].oneTimeRewardVip.gem);
		this.rewardCoin.SetView2(this.arrVIPs[this.currentIndexPage].oneTimeRewardVip.coin);
		this.arrVIPs[this.currentIndexPage].oneTimeRewardVip.ShowObjReward();
	}

	public void ClaimOneTimeRewardVIP()
	{
		if (GameContext.currentVIP >= this.currentIndexPage + 1)
		{
			if (this.canCollectReward)
			{
				this.arrVIPs[this.currentIndexPage].oneTimeRewardVip.ClaimReward(this.currentIndexPage + 1);
				SaveDataStateClaimRewardVIP.SetData(this.currentIndexPage);
				this.imgBtnClaim.sprite = this.sprBtnDisable;
				this.textClaim.text = ScriptLocalization.Claimed;
				this.canCollectReward = false;
				this.objRedNotificationClaimRewardOneTime.SetActive(false);
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.notify_already_claimed);
			}
		}
		else
		{
			Notification.Current.ShowNotification(string.Format(ScriptLocalization.you_need_more_vip_point, this.arrVIPs[this.currentIndexPage].vipPoint - GameContext.currentVIPPoint, this.currentIndexPage + 1));
		}
	}

	public void ShowPanelVIP()
	{
		if (!this.isShowPanelVIP)
		{
			this.isShowPanelVIP = true;
		}
		EscapeManager.Current.AddAction(new Action(this.HidePanelVIP));
		this.panelVIP.SetActive(true);
		if (ShopManager.Instance.isShowPanelShopGem)
		{
			this.needOpenPanelShopGem = true;
			ShopManager.Instance.HidePanelShopGem();
		}
		if (!this.isAddListener)
		{
			this.scrollSnap.onLerpComplete.AddListener(new UnityAction(this.ScrollSnapLerpComplete));
			this.maxPage = this.scrollSnap.CalculateMaxIndex();
			this.currentIndexPage = this.scrollSnap.cellIndex;
			this.isAddListener = true;
		}
		if (this.needSetDailyGiftVIP)
		{
			this.needSetDailyGiftVIP = false;
			this.SetDailyGift();
		}
		this.MoveToVIP();
		DOTween.Restart("PANEL_VIP", true, -1f);
	}

	public void HidePanelVIP()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelVIP));
		DOTween.PlayBackwards("PANEL_VIP");
		this.arrVIPs[this.currentIndexPage].oneTimeRewardVip.HideObjReward();
		this.needSetDailyGiftVIP = true;
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.isShowPanelVIP = false;
			this.panelVIP.SetActive(false);
		}));
		if (HomeManager.current != null)
		{
			HomeManager.current.CheckShowNotificationVIP();
		}
		if (this.needOpenPanelShopGem)
		{
			ShopManager.Instance.ShowPopupShopGem(3);
			this.needOpenPanelShopGem = false;
		}
	}

	private void SetTextInfoNextVIP()
	{
		if (GameContext.currentVIP < 12)
		{
			int num = this.arrVIPs[GameContext.currentVIP].vipPoint - GameContext.currentVIPPoint;
			this.youNeedMoreVIP.text = string.Format(ScriptLocalization.you_need_more_vip_point, num, GameContext.currentVIP + 1);
			this.textYouWillGet.text = string.Format(ScriptLocalization.you_wiil_get_1_vip_point, IAPGameManager.Current.GetLocalizePrice("10000 Coins"));
		}
	}

	public void ShowPanelInfoNextVIP()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePanelInfoNextVIP));
		this.panelInfoNextVIP.SetActive(true);
		DOTween.Restart("PANEL_INFO_NEXT_VIP", true, -1f);
	}

	public void HidePanelInfoNextVIP()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelInfoNextVIP));
		DOTween.PlayBackwards("PANEL_INFO_NEXT_VIP");
		base.StartCoroutine(GameContext.Delay(0.2f, delegate
		{
			this.panelInfoNextVIP.SetActive(false);
		}));
	}

	private void SetImageBtnClaimDailyGiftVIP()
	{
		if (GameContext.currentVIP == 0)
		{
			this.textTitle.text = ScriptLocalization.daily_gift + " VIP 1";
		}
		else
		{
			this.textTitle.text = ScriptLocalization.daily_gift + " VIP " + GameContext.currentVIP;
		}
		if (GameContext.canCollectDailyGiftVIP && GameContext.currentVIP > 0)
		{
			this.imgBtnClaimDailyGift.sprite = this.sprBtn;
			this.imgBtnShowPanelDailyGift.sprite = this.sprBtn;
			this.objRedNotification.SetActive(true);
		}
		else
		{
			if (GameContext.currentVIP > 0)
			{
				this.textClaimDailyGift.text = ScriptLocalization.Claimed;
			}
			this.imgBtnClaimDailyGift.sprite = this.sprBtnDisable;
			this.imgBtnShowPanelDailyGift.sprite = this.sprBtnDisable;
		}
	}

	private void SetDailyGift()
	{
		switch (GameContext.currentVIP)
		{
		case 0:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(0, 1000, 0, 0, 0, 0);
			break;
		case 1:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(0, 1000, 0, 0, 0, 0);
			break;
		case 2:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(0, 1000, 0, 0, 0, 0);
			break;
		case 3:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(0, 1000, 0, 0, 0, 0);
			break;
		case 4:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(0, 1000, 0, 0, 0, 0);
			break;
		case 5:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(0, 1000, 0, 0, 0, 0);
			break;
		case 6:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(0, 1000, 0, 1, 0, 1);
			break;
		case 7:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(0, 1000, 0, 1, 1, 1);
			break;
		case 8:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(0, 1000, 0, 1, 1, 1);
			break;
		case 9:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(0, 1000, 0, 1, 1, 1);
			break;
		case 10:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(0, 1000, 3, 1, 1, 1);
			break;
		case 11:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(0, 10000, 5, 2, 2, 2);
			break;
		case 12:
			this.dailyGiftVIP = new VIPManager.DailyGiftVIP(15, 10000, 5, 2, 2, 2);
			break;
		}
		this.SetShowGift();
	}

	private void SetShowGift()
	{
		if (this.dailyGiftVIP.gem > 0)
		{
			this.rewardGemDaily.gameObject.SetActive(true);
			this.rewardGemDaily.SetView2(this.dailyGiftVIP.gem);
		}
		if (this.dailyGiftVIP.coin > 0)
		{
			this.rewardCoinDaily.gameObject.SetActive(true);
			this.rewardCoinDaily.SetView2(this.dailyGiftVIP.coin);
		}
		if (this.dailyGiftVIP.overdrive > 0)
		{
			this.rewardOverdrive.gameObject.SetActive(true);
			this.rewardOverdrive.SetView2(this.dailyGiftVIP.coin);
		}
		if (this.dailyGiftVIP.heart > 0)
		{
			this.rewardItemHeart.gameObject.SetActive(true);
			this.rewardItemHeart.SetView2(this.dailyGiftVIP.heart);
		}
		if (this.dailyGiftVIP.shield > 0)
		{
			this.rewardItemShield.gameObject.SetActive(true);
			this.rewardItemShield.SetView2(this.dailyGiftVIP.heart);
		}
		if (this.dailyGiftVIP.power10 > 0)
		{
			this.rewardPower10.gameObject.SetActive(true);
			this.rewardPower10.SetView2(this.dailyGiftVIP.heart);
		}
	}

	public void ClaimDailyGiftVIP()
	{
		if (GameContext.canCollectDailyGiftVIP && GameContext.currentVIP > 0)
		{
			this.dailyGiftVIP.CollectDailyGiftVIP();
			GameContext.canCollectDailyGiftVIP = false;
			CacheGame.CanCollectDailyGiftVIP = false;
			this.imgBtnClaimDailyGift.sprite = this.sprBtnDisable;
			this.imgBtnShowPanelDailyGift.sprite = this.sprBtnDisable;
			this.objRedNotification.SetActive(false);
		}
		else if (GameContext.currentVIP > 0)
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_already_claimed);
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.increase_vip_level);
		}
	}

	public void ShowPanelDailyGiftVIP()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePanelDailyGiftVIP));
		this.panelDailyGiftVIP.SetActive(true);
		DOTween.Restart("PANEL_DAILY_GIFT_VIP", true, -1f);
	}

	public void HidePanelDailyGiftVIP()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelDailyGiftVIP));
		DOTween.PlayBackwards("PANEL_DAILY_GIFT_VIP");
		base.StartCoroutine(GameContext.Delay(0.2f, delegate
		{
			this.panelDailyGiftVIP.SetActive(false);
		}));
	}

	public void UpdateVIPPoint()
	{
		this.CheckVip();
		this.SetTextInfoNextVIP();
		this.SetImageBtnClaimDailyGiftVIP();
		int num = this.viewVIPs.Length;
		for (int i = 0; i < num; i++)
		{
			this.viewVIPs[i].UpdateVIP();
		}
	}

	public static VIPManager _ins;

	public GameObject panelVIP;

	public VIPManager.VIPData[] arrVIPs;

	public Text textCurrentVIP;

	public Button btnClaim;

	public Text textClaim;

	public Image imgBtnClaim;

	public Sprite sprBtn;

	public Sprite sprBtnDisable;

	public ScrollSnap scrollSnap;

	public RewardGem rewardGem;

	public RewardCoin rewardCoin;

	[Header("Panel Info Next VIP")]
	public GameObject panelInfoNextVIP;

	public Text textYouWillGet;

	public Text youNeedMoreVIP;

	[Header("Panel Daily Gift VIP")]
	public GameObject panelDailyGiftVIP;

	public Text textTitle;

	public Text textClaimDailyGift;

	public Image imgBtnClaimDailyGift;

	public Image imgBtnShowPanelDailyGift;

	public RewardGem rewardGemDaily;

	public RewardCoin rewardCoinDaily;

	public RewardSkillPlane rewardOverdrive;

	public RewardItemHeart rewardItemHeart;

	public RewardShield rewardItemShield;

	public RewardItemBullet10 rewardPower10;

	public GameObject objRedNotification;

	public GameObject objRedNotificationClaimRewardOneTime;

	[Space(10f)]
	public ViewVIP[] viewVIPs;

	private int currentIndexPage;

	private int maxPage;

	private bool canCollectReward;

	private bool isAddListener;

	private bool needSetDailyGiftVIP = true;

	private bool needOpenPanelShopGem;

	[HideInInspector]
	public bool isShowPanelVIP;

	private VIPManager.DailyGiftVIP dailyGiftVIP;

	[Serializable]
	public class VIPData
	{
		public int vipPoint;

		public VIPManager.RewardVip oneTimeRewardVip;
	}

	[Serializable]
	public class RewardVip
	{
		public void ShowObjReward()
		{
			this.objReward.SetActive(true);
		}

		public void HideObjReward()
		{
			this.objReward.SetActive(false);
		}

		public void ClaimReward(int vip)
		{
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, this.gem);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, this.coin);
			GetRewardSuccess.current.AddItemToView(this.otherItem, this.numberItem);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			CacheGame.AddGems(this.gem);
			CacheGame.AddCoins(this.coin);
			CurrencyLog.LogGemIn(this.gem, CurrencyLog.In.Vip, "Reward_One_Time_VIP_" + vip);
			CurrencyLog.LogGoldIn(this.coin, CurrencyLog.In.Vip, "Reward_One_Time_VIP_" + vip);
			PrizeManager.RewardType rewardType = this.otherItem;
			switch (rewardType)
			{
			case PrizeManager.RewardType.Key_Super_Prize:
				CacheGame.KeySuperRize += this.numberItem;
				GameContext.totalKeySuperPrize += this.numberItem;
				break;
			case PrizeManager.RewardType.Item_Heart:
				CacheGame.NumberItemPowerHeart += this.numberItem;
				GameContext.totalItemPowerUpHeart += this.numberItem;
				break;
			case PrizeManager.RewardType.Item_Bullet_10:
				CacheGame.NumberItemPowerUpBullet10 += this.numberItem;
				GameContext.totalItemPowerUpBullet10 += this.numberItem;
				break;
			default:
				if (rewardType != PrizeManager.RewardType.Card_All_Plane)
				{
					if (rewardType == PrizeManager.RewardType.Card_All_Drone)
					{
						CacheGame.AddCardAllDrone(this.numberItem);
					}
				}
				else
				{
					CacheGame.AddCardAllPlane(this.numberItem);
				}
				break;
			case PrizeManager.RewardType.Item_Shield:
				CacheGame.NumberItemPowerShield += this.numberItem;
				GameContext.totalItemPowerShield += this.numberItem;
				break;
			case PrizeManager.RewardType.Special_Skill:
				CacheGame.AddSkillPlane(this.numberItem);
				break;
			}
		}

		public int gem;

		public int coin;

		public PrizeManager.RewardType otherItem;

		public int numberItem;

		public GameObject objReward;
	}

	public class DailyGiftVIP
	{
		public DailyGiftVIP(int _gem, int _coint, int _overdrive, int _heart, int _shield, int _power10)
		{
			this.gem = _gem;
			this.coin = _coint;
			this.overdrive = _overdrive;
			this.heart = _heart;
			this.shield = _shield;
			this.power10 = _power10;
		}

		public void CollectDailyGiftVIP()
		{
			if (this.gem > 0)
			{
				GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, this.gem);
				CacheGame.AddGems(this.gem);
				CurrencyLog.LogGemIn(this.gem, CurrencyLog.In.Vip, "Vip_Daily_Reward");
			}
			if (this.coin > 0)
			{
				GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, this.coin);
				CacheGame.AddCoins(this.coin);
				CurrencyLog.LogGoldIn(this.coin, CurrencyLog.In.Vip, "Vip_Daily_Reward");
			}
			if (this.overdrive > 0)
			{
				GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Special_Skill, this.overdrive);
				CacheGame.AddSkillPlane(this.overdrive);
			}
			if (this.heart > 0)
			{
				GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Heart, this.heart);
				CacheGame.NumberItemPowerHeart += this.heart;
			}
			if (this.shield > 0)
			{
				GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Shield, this.shield);
				CacheGame.NumberItemPowerShield += this.shield;
			}
			if (this.power10 > 0)
			{
				GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Bullet_10, this.power10);
				CacheGame.NumberItemPowerUpBullet10 += this.power10;
			}
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
		}

		public int gem;

		public int coin;

		public int overdrive;

		public int heart;

		public int shield;

		public int power10;
	}
}
