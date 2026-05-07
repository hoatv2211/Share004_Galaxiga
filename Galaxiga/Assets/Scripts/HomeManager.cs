using System;
using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;
using I2.Loc;
using OSNet;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
	private void Awake()
	{
		HomeManager.current = this;
		if (GameContext.isTryPlane)
		{
			GameContext.isTryPlane = false;
			PlaneManager.current.ShowPanelSelectPlane(GameContext.planeTry);
		}
		if (!GameContext.isHomeOK)
		{
			GameContext.isHomeOK = true;
			new CSHomeOK().Send(false);
		}
	}

	private void OnEnable()
	{
		ShopManager.Instance.UpdateTextEnergy();
		if (GameContext.isShowPack && GameContext.maxLevelUnlocked >= 10)
		{
			SaleManager.Currrent.CheckSale();
		}
		GameContext.isShowPack = false;
	}

	private void Start()
	{
		this.SetTextPriceInapp();
		this.CheckShowNotificationVIP();
		CacheGame.IsCompleteTutorialSceneSelectLevel = true;
		if (GameContext.needShowPanelSelectPlane)
		{
			PlaneManager.current.ShowPanelSelectPlane();
		}
		this.SelectTab(2);
		this.transitionsFX.TransitionEnter();
		this.CheckShowBtnSale();
		this.CheckShowBtnStarterPack();
		if (GameContext.canCollectDailyGift && GameContext.maxLevelUnlocked > 3)
		{
			DailyGiftManager.Current.ShowPanelDailyGift();
		}
	}

	private void SetTextPriceInapp()
	{
		IAPGameManager.Current.SetTextPrice();
		SaleManager.Currrent.SetTextPricePackSale();
	}

	public void Campaign()
	{
		this.TransitionExitEnded();
	}

	private void TransitionExitEnded()
	{
		GameContext.lastTimeInSceneHome = DateTime.Now;
		SceneContext.sceneName = "SelectLevel";
		LoadingScenes.Current.LoadLevel("SelectLevel");
	}

	public void ChangeImagePlane(int planeID)
	{
		this.arrPlanes[this.indexPlane].SetActive(false);
		this.indexPlane = planeID;
		this.arrPlanes[this.indexPlane].SetActive(true);
		this.SetAnimationPlane(this.indexPlane, (GameContext.Plane)planeID);
	}

	public void SetAnimationPlane(int indexPlane, GameContext.Plane plane)
	{
		string skin = "E" + DataGame.Current.IndexRankPlane(plane);
		this.arrAnimPlane[indexPlane].Skeleton.SetSkin(skin);
	}

	public void ShowStarterPack()
	{
		ShopManager.Instance.ShowStarterPack();
	}

	public void ShowPremiumPack()
	{
		ShopManager.Instance.ShowPremiumPack();
	}

	[EnumAction(typeof(HomeManager.TabType))]
	public void SelectTab(int tabID)
	{
		if (this.currentTabID != tabID)
		{
			this.DeselectTab();
			this.currentTabID = tabID;
			switch (tabID)
			{
			case 1:
				if (Application.internetReachability == NetworkReachability.NotReachable)
				{
					Notification.Current.ShowNotification(ScriptLocalization.no_internet);
					return;
				}
				EscapeManager.Current.AddAction(new Action(this.SelectTabPlane));
				this.currentIndexTab = 1;
				break;
			case 2:
				if (NewTutorial.current.currentStepTutorial_RewardDailyGift == 11)
				{
					NewTutorial.current.RewardDailyGift_Step11();
				}
				this.currentIndexTab = 2;
				EscapeManager.Current.RemoveAction(new Action(this.SelectTabPlane));
				break;
			case 3:
				if (NewTutorial.current.currentStepTutorial_RewardDailyGift == 1)
				{
					NewTutorial.current.RewardDailyGift_Step1();
				}
				this.currentIndexTab = 3;
				DailyQuestManager.Current.ShowTimeResetDay();
				EscapeManager.Current.AddAction(new Action(this.SelectTabPlane));
				break;
			case 4:
				this.currentIndexTab = 4;
				this.shopItemManager.StartCountdownResetDay();
				EscapeManager.Current.AddAction(new Action(this.SelectTabPlane));
				if (!GameContext.isOpenShopItemCoin)
				{
					GameContext.isOpenShopItemCoin = true;
					WarningManager.Current.HideNotificationShopCoinItem();
				}
				break;
			}
			this.arrTabHome[this.currentIndexTab].SelectTab();
		}
	}

	private void DeselectTab()
	{
		if (this.currentIndexTab != -1)
		{
			this.arrTabHome[this.currentIndexTab].DeselectTab();
			if (this.currentIndexTab == 3)
			{
				DailyQuestManager.Current.HideTimeResetDay();
			}
			if (this.currentTabID == 4)
			{
				this.shopItemManager.StopCountdownResetDay();
			}
		}
	}

	private void SelectTabPlane()
	{
		this.SelectTab(2);
	}

	public void Cheat()
	{
		if (this.canCheat)
		{
			if (!this.isCheat)
			{
				this.isCheat = true;
				this.timeTouch = Time.time;
			}
			this.countTouch++;
			Tung.Log("touch " + this.countTouch);
			if (this.countTouch > 15)
			{
				this.isCheat = false;
				this.countTouch = 0;
				float time = Time.time;
				float num = time - this.timeTouch;
				Tung.Log("time touch " + num);
				if (num < 5f)
				{
					CacheGame.AddCoins(1000000);
					CacheGame.AddGems(10000);
					CacheGame.AddCardAllPlane(10000);
					CacheGame.GetPlane(GameContext.Plane.FuryOfAres);
					CacheGame.GetPlane(GameContext.Plane.SkyWraith);
					CacheGame.GetPlane(GameContext.Plane.TwilightX);
					CacheGame.GetPlane(GameContext.Plane.Greataxe);
					CacheGame.GetPlane(GameContext.Plane.Warlock);
					CacheGame.GetPlane(GameContext.Plane.SSLightning);
					CacheGame.SetOwnedDrone(GameContext.Drone.AutoGatlingGun);
					CacheGame.SetOwnedDrone(GameContext.Drone.GatlingGun);
					CacheGame.SetOwnedDrone(GameContext.Drone.Laser);
					CacheGame.SetOwnedDrone(GameContext.Drone.Nighturge);
					CacheGame.SetOwnedDrone(GameContext.Drone.GodOfThunder);
					CacheGame.SetOwnedDrone(GameContext.Drone.Terigon);
					CacheGame.SetMaxLevel(70);
					CacheGame.SetMaxLevelBoss(14);
					this.timeTouch = Time.time;
					SceneManager.LoadScene("Intro");
				}
			}
		}
	}

	public void ShowPanelVIP()
	{
		VIPManager.current.ShowPanelVIP();
	}

	public void CheckShowNotificationVIP()
	{
		if (GameContext.currentVIP > 0)
		{
			if (GameContext.canCollectDailyGiftVIP)
			{
				this.objNotificationVIP.SetActive(true);
				return;
			}
			for (int i = 0; i < GameContext.currentVIP; i++)
			{
				if (SaveDataStateClaimRewardVIP.stateClaimRewardVIP.arrBool[i])
				{
					this.objNotificationVIP.SetActive(true);
					break;
				}
			}
		}
	}

	private void CheckShowBtnSale()
	{
		if (SaleManager.Currrent.isSale)
		{
			this.objBtnSale.SetActive(true);
			this.textPercentDiscount.text = SaleManager.Currrent.textPercenDiscount.text;
			this.timeSale = SaleManager.Currrent.timeSale;
			base.StartCoroutine(this.CountDownSale());
		}
	}

	public void ShowPanelSale()
	{
		SaleManager.Currrent.ShowPanelSale();
	}

	public void HideBtnSale()
	{
		this.objBtnSale.SetActive(false);
	}

	private IEnumerator CountDownSale()
	{
		while (this.timeSale > 0f)
		{
			string textFormat = TimeSpan.FromSeconds((double)this.timeSale).ToString("hh\\:mm\\:ss");
			this.textEndTimeSale.text = textFormat;
			yield return new WaitForSecondsRealtime(1f);
			this.timeSale -= 1f;
		}
		yield break;
	}

	private void CheckShowBtnStarterPack()
	{
		if (GameContext.isPurchaseStarterPack || GameContext.isDisableStarterPack)
		{
			this.btnBuyStarterPack.SetActive(false);
		}
		else
		{
			this.CheckTimeStarterPack();
		}
	}

	private void CheckTimeStarterPack()
	{
		this.textTimeStarterPack.text = ShopManager.Instance.textTimeStarterPack.text;
	}

	public void ShowPanelSocial()
	{
		Notification.Current.ShowNotification(ScriptLocalization.notify_coming_soon);
	}

	public void StartCoroutineCountdownPremium(double _timePremiumPack)
	{
		this.textTimePremiumPack.gameObject.SetActive(true);
		base.StartCoroutine(this.CoroutineCountdownPremium(_timePremiumPack));
	}

	private IEnumerator CoroutineCountdownPremium(double timePremiumPack)
	{
		while (timePremiumPack > 0.0)
		{
			TimeSpan time = TimeSpan.FromSeconds(timePremiumPack);
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
			timePremiumPack -= 1.0;
		}
		yield break;
	}

	public static HomeManager current;

	public Camera mainCamera;

	public ProCamera2DTransitionsFX transitionsFX;

	public ShopItemManager shopItemManager;

	public GameObject btnBuyStarterPack;

	public Text textTimeStarterPack;

	public Text textTimePremiumPack;

	public GameObject[] arrPlanes;

	public SkeletonGraphic[] arrAnimPlane;

	public GameObject objNotificationVIP;

	public GameObject rankBarButton;

	public GameObject socialBarButton;

	public GameObject objBtnSale;

	public Text textPercentDiscount;

	public Text textEndTimeSale;

	public bool canCheat;

	public HomeManager.Tab[] arrTabHome;

	private int indexPlane;

	private int currentTabID = -1;

	private int currentIndexTab = -1;

	private bool isCheat;

	private int countTouch;

	private float timeTouch;

	private float timeSale;

	public enum TabType
	{
		Ranking,
		Social,
		Airfield,
		Quest,
		Shop
	}

	[Serializable]
	public class Tab
	{
		public void SelectTab()
		{
			if (this.tabID != HomeManager.TabType.Airfield)
			{
				this.objTab.SetActive(true);
			}
			DOTween.Restart(this.idDotween, true, -1f);
			DOTween.Play(this.idDotween);
			this.objTextNameTab.SetActive(true);
			this.imgSelect.SetActive(true);
		}

		public void DeselectTab()
		{
			DOTween.PlayBackwards(this.idDotween);
			this.objTextNameTab.SetActive(false);
			this.imgSelect.SetActive(false);
			if (this.tabID != HomeManager.TabType.Airfield)
			{
				this.objTab.SetActive(false);
			}
		}

		public HomeManager.TabType tabID;

		public GameObject imgSelect;

		public GameObject objTextNameTab;

		public string idDotween;

		[HideIf("tabID", HomeManager.TabType.Airfield, true)]
		public GameObject objTab;
	}
}
