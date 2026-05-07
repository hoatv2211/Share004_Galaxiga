using System;
using DG.Tweening;
using Hellmade.Sound;
using I2.Loc;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class ViewInforPlane : MonoBehaviour
{
	public string NamePlane
	{
		get
		{
			return this.planeData.namePlane;
		}
	}

	public bool IsUnlock
	{
		get
		{
			return CacheGame.IsUnlockPlane(this.planeID);
		}
		set
		{
			CacheGame.SetUnlockPlane(this.planeID);
		}
	}

	public bool IsOwned
	{
		get
		{
			return CacheGame.IsOwnedPlane(this.planeID);
		}
		set
		{
			CacheGame.SetOwnedPlane(this.planeID);
		}
	}

	public bool IsEnabaleCraft
	{
		get
		{
			return GameContext.totalUltraStarshipCard >= this.planeData.NumberCardToEvolve;
		}
	}

	public int Vip
	{
		get
		{
			return this.planeData.Vip;
		}
	}

	public string Rank
	{
		get
		{
			string currentRank = this.planeData.CurrentRank;
			this.currentIDRank = DataGame.Current.ConverRankPlaneToIndex(currentRank);
			return currentRank;
		}
	}

	private void Awake()
	{
		this.GetLevel();
		this.numberUpgradePlane = SaveDataQuest.ProgressQuest(DailyQuestManager.Quest.mechanic);
		this.GetBaseDataDamagePlane();
	}

	private void GetLevel()
	{
		this.currentLvl = this.planeData.Level;
		if (this.currentLvl <= 0)
		{
			this.planeData.Level = 1;
			this.currentLvl = 1;
		}
		this.maxLevel = this.planeData.MaxLevel;
	}

	private void Start()
	{
		this.SetAnimPlane();
		this.CheckCoin();
		this.CheckGem();
		if (!this.check)
		{
			this.CheckPriceGemUpgrade();
			this.check = true;
		}
		this.SetTextView();
		this.CheckPlane();
		this.actionCoinChange = delegate(Component sender, object param)
		{
			this.CheckCoin();
		};
		this.actionGemChange = delegate(Component sender, object param)
		{
			this.CheckGem();
		};
		EventDispatcher.Instance.RegisterListener(EventID.Coin, this.actionCoinChange);
		EventDispatcher.Instance.RegisterListener(EventID.Gem, this.actionGemChange);
	}

	private void CheckCoin()
	{
		int coinUpgrade = this.planeData.CoinUpgrade;
		if (this.check)
		{
			this.CheckPriceGemUpgrade();
		}
		if (ShopContext.currentCoin < coinUpgrade)
		{
			this.imgBtnCoinUpgrade.sprite = this.sprDisableBtnUpgradeCoin;
		}
		else
		{
			this.imgBtnCoinUpgrade.sprite = this.sprBtnUpgradePlaneCoin;
		}
	}

	private void CheckGem()
	{
		if (this.check)
		{
			this.CheckPriceGemUpgrade();
		}
		if (ShopContext.currentGem < this.priceGemUpgrade)
		{
			this.imgBtnGemUpgrade.sprite = this.sprDisableBtnUpgradeCoin;
		}
		else
		{
			this.imgBtnGemUpgrade.sprite = this.sprBtnUpgradePlaneGem;
		}
	}

	private void CheckPriceGemUpgrade()
	{
		string rank = this.Rank;
		int num = this.maxLevel - this.currentLvl;
		if (num < 0)
		{
			return;
		}
		if (num < 10)
		{
			this.priceGemUpgrade = CostGemUpgradePlaneDroneSheet.Get(this.currentLvl).plane;
			this.textAdd10Level.text = "+" + num + " LVL";
		}
		else
		{
			this.priceGemUpgrade = this.planeData.GemUpgrade;
			this.textAdd10Level.text = "+10 LVL";
		}
		if (GameContext.currentVIP > 3 && GameContext.currentVIP < 9)
		{
			this.priceGemUpgrade -= (int)((float)this.priceGemUpgrade * 0.05f);
		}
		else if (GameContext.currentVIP >= 9 && GameContext.currentVIP < 12)
		{
			this.priceGemUpgrade -= (int)((float)this.priceGemUpgrade * 0.15f);
		}
		else if (GameContext.currentVIP >= 12)
		{
			this.priceGemUpgrade -= (int)((float)this.priceGemUpgrade * 0.25f);
		}
	}

	public void SetTextView()
	{
		this.maxLevel = this.planeData.MaxLevel;
		this.textNamePlane.text = this.NamePlane;
		DataGame.Current.SetImageRank(this.imgRank, this.Rank);
		this.SetTextInforUpgrade();
		this.SetTextPriceUpgrade();
		this.SetDPSPlane();
	}

	private void SetTextInforUpgrade()
	{
		this.textLevelPlane.text = string.Concat(new object[]
		{
			"LVL: ",
			this.currentLvl,
			"/",
			this.maxLevel
		});
		this.textLevelPlane2.text = string.Empty + this.currentLvl;
		this.textNextMaxLvl2.text = string.Empty + this.planeData.NextMaxLevel;
	}

	private void SetTextPriceUpgrade()
	{
		this.textPriceCoinUpgrade.text = string.Empty + this.planeData.CoinUpgrade;
		this.textPriceGemUpgrade.text = string.Empty + this.priceGemUpgrade;
	}

	public void CheckPlane()
	{
		if (this.IsOwned)
		{
			if (this.currentLvl >= this.maxLevel)
			{
				this.panelMaxLevel.SetActive(true);
				if (this.currentLvl == 200)
				{
					this.objTextMaxLevel.localPosition = new Vector2(0f, this.objTextMaxLevel.localPosition.y);
					this.objArrowMaxLevel.SetActive(false);
				}
			}
			else
			{
				this.panelUpgradePlane.SetActive(true);
			}
			this.panelLockPlane.SetActive(false);
			this.panelBuyPlane.SetActive(false);
			this.textLevelPlane.gameObject.SetActive(true);
		}
		else
		{
			this.textLevelPlane.gameObject.SetActive(false);
			if (this.IsUnlock)
			{
				this.panelBuyPlane.SetActive(true);
				this.panelLockPlane.SetActive(false);
				if (this.planeData.isOnlyGem)
				{
					this.btnBuyPlaneCoin.SetActive(false);
					RectTransform component = this.btnBuyPlaneGem.GetComponent<RectTransform>();
					component.anchoredPosition = new Vector2(0f, component.anchoredPosition.y);
				}
				else
				{
					this.textPriceCoin.text = string.Empty + this.planeData.priceCoin;
					this.textPriceGem.text = string.Empty + this.planeData.priceGem;
				}
			}
			else if (CacheGame.IsPassLevel(this.planeData.ModeLevelUnlock, this.planeData.LevelUnlock))
			{
				CacheGame.SetUnlockPlane(this.planeID);
				this.CheckPlane();
			}
			else
			{
				this.panelLockPlane.SetActive(true);
				IAPGameManager.Current.SetTextLocalizePrice(this.textPriceInAppPlane, this.planeData.BaseRank.ToString());
				this.textInfoUnlockPlane.text = string.Format(ScriptLocalization.complete_stage_to_unlock_starship, this.planeData.LevelUnlock, this.planeData.ModeLevelUnlock.ToString());
			}
		}
	}

	public void UpradePlane()
	{
		int coinUpgrade = this.planeData.CoinUpgrade;
		if (ShopContext.currentCoin >= coinUpgrade)
		{
			if (this.currentLvl < this.maxLevel)
			{
				this.currentLvl++;
				CacheGame.SetLevelPlane(this.planeID, this.currentLvl);
				CacheGame.MinusCoins(coinUpgrade);
				CurrencyLog.LogGoldOut(coinUpgrade, CurrencyLog.Out.UpgradePlane, this.planeID.ToString());
				if (!PlaneManager.current.needUpdateToServer)
				{
					PlaneManager.current.needUpdateToServer = true;
				}
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					"level plane ",
					this.planeID.ToString(),
					" ",
					this.currentLvl
				}));
				this.SetDPSPlane();
				this.effectUpgrade.Play();
				this.SetTextInforUpgrade();
				this.SetTextPriceUpgrade();
				this.textLevelPlane2.text = string.Empty + this.currentLvl;
				this.dOTween.DORestart(false);
				this.dOTween.DOPlay();
				if (this.currentLvl == this.maxLevel)
				{
					UnityEngine.Debug.Log("Max Level");
					this.ShowPanelMaxLevel();
				}
				if (this.numberUpgradePlane < 2)
				{
					SaveDataQuest.SetProcessQuest(DailyQuestManager.Quest.mechanic, 1);
					this.numberUpgradePlane++;
				}
				EazySoundManager.PlayUISound(AudioCache.UISound.upgrade_plane_drone);
				if (NewTutorial.current.currentStepTutorial_UpgradePlane == 9)
				{
					NewTutorial.current.UpgradePlane_Step9();
				}
			}
			else
			{
				UnityEngine.Debug.Log("Max Level");
				this.ShowPanelMaxLevel();
			}
		}
		else
		{
			UnityEngine.Debug.Log("Not Enough Coins");
			ShopManager.Instance.ShowPopupNotEnoughCoin();
		}
	}

	public void UpradePlaneGem()
	{
		if (ShopContext.currentGem >= this.priceGemUpgrade)
		{
			if (this.currentLvl < this.maxLevel)
			{
				int num = this.currentLvl + 10;
				if (num > this.maxLevel)
				{
					num = this.maxLevel;
				}
				this.currentLvl = num;
				CacheGame.SetLevelPlane(this.planeID, this.currentLvl);
				CacheGame.MinusGems(this.priceGemUpgrade);
				CurrencyLog.LogGemOut(this.priceGemUpgrade, CurrencyLog.Out.UpgradePlane, this.planeID.ToString());
				this.SetDPSPlane();
				this.effectUpgrade.Play();
				this.SetTextInforUpgrade();
				this.SetTextPriceUpgrade();
				this.textLevelPlane2.text = string.Empty + this.currentLvl;
				this.dOTween.DORestart(false);
				this.dOTween.DOPlay();
				if (this.numberUpgradePlane < 2)
				{
					SaveDataQuest.SetProcessQuest(DailyQuestManager.Quest.mechanic, 1);
					this.numberUpgradePlane++;
				}
				EazySoundManager.PlayUISound(AudioCache.UISound.upgrade_plane_drone);
				if (this.currentLvl == this.maxLevel)
				{
					this.ShowPanelMaxLevel();
				}
			}
			else
			{
				this.ShowPanelMaxLevel();
			}
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void ShowPanelMaxLevel()
	{
		this.panelUpgradePlane.SetActive(false);
		this.panelMaxLevel.SetActive(true);
		if (this.currentLvl == 200)
		{
			this.objTextMaxLevel.localPosition = new Vector2(0f, this.objTextMaxLevel.localPosition.y);
			this.objArrowMaxLevel.SetActive(false);
		}
	}

	public void ShowPanelUpgradePlane()
	{
		this.panelUpgradePlane.SetActive(true);
		this.panelMaxLevel.SetActive(false);
		this.maxLevel = this.planeData.MaxLevel;
		this.CheckPriceGemUpgrade();
	}

	public void SetAnimPlane()
	{
		string skin = "E" + DataGame.Current.ConverRankPlaneToIndex(this.Rank);
		this.skeletonGraphicPlane.Skeleton.SetSkin(skin);
		this.skeletonGraphicBtnSelectPlane.Skeleton.SetSkin(skin);
	}

	public void PurchasePlane()
	{
		IAPGameManager.Current.PurchasePlaneRank(this.planeData.BaseRank);
	}

	private void SetDPSPlane()
	{
		float multiplierRank = this.planeData.MultiplierRank;
		int num = Mathf.RoundToInt(multiplierRank * (((float)this.baseMainDamage + 0.05f * (float)this.baseMainDamage * (float)(this.currentLvl - 1)) / this.fireRateMainGun * (float)this.numberBulletMainGun * this.accurateMainGun + ((float)this.baseSubDamage + 0.05f * (float)this.baseSubDamage * (float)(this.currentLvl - 1)) / this.fireRateSubGun * (float)this.numberBulletSubGun * this.accurateSubGun));
		int num2 = Mathf.RoundToInt(multiplierRank * (((float)this.baseMainDamage + 0.05f * (float)this.baseMainDamage * (float)this.currentLvl) / this.fireRateMainGun * (float)this.numberBulletMainGun * this.accurateMainGun + ((float)this.baseSubDamage + 0.05f * (float)this.baseSubDamage * (float)this.currentLvl) / this.fireRateSubGun * (float)this.numberBulletSubGun * this.accurateSubGun));
		if (this.currentIDRank >= 4)
		{
			num += Mathf.RoundToInt((float)num * 0.15f);
			num2 += Mathf.RoundToInt((float)num2 * 0.15f);
		}
		if (this.currentLvl < 200)
		{
			this.textPower.text = string.Concat(new object[]
			{
				ScriptLocalization.power,
				": ",
				num,
				string.Format("<color=#13be07ff>+{0}</color>", num2 - num)
			});
		}
		else
		{
			this.textPower.text = ScriptLocalization.power + ": " + num;
		}
	}

	private void GetBaseDataDamagePlane()
	{
		int power = 10;
		this.baseMainDamage = this.planeData.BaseMainDamageLVL1;
		this.baseSubDamage = this.planeData.BaseSubDamageLVL1;
		switch (this.planeID)
		{
		case GameContext.Plane.BataFD01:
			this.fireRateMainGun = ShipBataFD01Sheet.Get(power).mainFireRate;
			this.numberBulletMainGun = ShipBataFD01Sheet.Get(power).numberBulletMainGun;
			this.accurateMainGun = ShipBataFD01Sheet.Get(power).accurateMainGun;
			this.fireRateSubGun = ShipBataFD01Sheet.Get(power).subFireRate;
			this.numberBulletSubGun = ShipBataFD01Sheet.Get(power).numberBulletSubGun1;
			this.accurateSubGun = ShipBataFD01Sheet.Get(power).accurateSubGun1;
			break;
		case GameContext.Plane.FuryOfAres:
			this.fireRateMainGun = ShipFuryOfAresSheet.Get(power).mainFireRate;
			this.numberBulletMainGun = ShipFuryOfAresSheet.Get(power).numberBulletMainGun;
			this.accurateMainGun = ShipFuryOfAresSheet.Get(power).accurateMainGun;
			this.fireRateSubGun = ShipFuryOfAresSheet.Get(power).subFireRate;
			this.numberBulletSubGun = ShipFuryOfAresSheet.Get(power).numberBulletSubGun1;
			this.accurateSubGun = ShipFuryOfAresSheet.Get(power).accurateSubGun1;
			break;
		case GameContext.Plane.SkyWraith:
			this.fireRateMainGun = ShipSkywraithSheet.Get(power).mainFireRate;
			this.numberBulletMainGun = ShipSkywraithSheet.Get(power).numberBulletMainGun;
			this.accurateMainGun = ShipSkywraithSheet.Get(power).accurateMainGun;
			this.fireRateSubGun = ShipSkywraithSheet.Get(power).subFireRate;
			this.numberBulletSubGun = ShipSkywraithSheet.Get(power).numberBulletSubGun1;
			this.accurateSubGun = ShipSkywraithSheet.Get(power).accurateSubGun1;
			break;
		case GameContext.Plane.TwilightX:
			this.fireRateMainGun = ShipTwilightXSheet.Get(power).mainFireRate;
			this.numberBulletMainGun = ShipTwilightXSheet.Get(power).numberBulletMainGun;
			this.accurateMainGun = ShipTwilightXSheet.Get(power).accurateMainGun;
			this.fireRateSubGun = ShipTwilightXSheet.Get(power).subFireRate;
			this.numberBulletSubGun = ShipTwilightXSheet.Get(power).numberBulletSubGun1;
			this.accurateSubGun = ShipTwilightXSheet.Get(power).accurateSubGun1;
			break;
		case GameContext.Plane.Greataxe:
			this.fireRateMainGun = ShipGreataxeSheet.Get(power).mainFireRate;
			this.numberBulletMainGun = ShipGreataxeSheet.Get(power).numberBulletMainGun;
			this.accurateMainGun = ShipGreataxeSheet.Get(power).accurateMainGun;
			this.fireRateSubGun = ShipGreataxeSheet.Get(power).subFireRate;
			this.numberBulletSubGun = ShipGreataxeSheet.Get(power).numberBulletSubGun1;
			this.accurateSubGun = ShipGreataxeSheet.Get(power).accurateSubGun1;
			break;
		case GameContext.Plane.SSLightning:
			this.fireRateMainGun = ShipSSLightningSheet.Get(power).mainFireRate;
			this.numberBulletMainGun = ShipSSLightningSheet.Get(power).numberBulletMainGun;
			this.accurateMainGun = ShipSSLightningSheet.Get(power).accurateMainGun;
			this.fireRateSubGun = ShipSSLightningSheet.Get(power).subFireRate;
			this.numberBulletSubGun = ShipSSLightningSheet.Get(power).numberBulletSubGun1;
			this.accurateSubGun = ShipSSLightningSheet.Get(power).accurateSubGun1;
			break;
		case GameContext.Plane.Warlock:
			this.fireRateMainGun = ShipWarlockSheet.Get(power).mainFireRate;
			this.numberBulletMainGun = ShipWarlockSheet.Get(power).numberBulletMainGun;
			this.accurateMainGun = ShipWarlockSheet.Get(power).accurateMainGun;
			this.fireRateSubGun = ShipWarlockSheet.Get(power).subFireRate;
			this.numberBulletSubGun = ShipWarlockSheet.Get(power).numberBulletSubGun1;
			this.accurateSubGun = ShipWarlockSheet.Get(power).accurateSubGun1;
			break;
		}
	}

	public GameContext.Plane planeID;

	public PlaneData planeData;

	public Text textNamePlane;

	public Text textLevelPlane;

	public Text textInfoUnlockPlane;

	public Text textPriceInAppPlane;

	public Text textPriceCoin;

	public Text textPriceGem;

	public Text textPriceCoinUpgrade;

	public Text textPriceGemUpgrade;

	public Text textLevelPlane2;

	public Text textAdd10Level;

	public GameObject panelLockPlane;

	public GameObject panelBuyPlane;

	public GameObject panelUpgradePlane;

	public GameObject panelMaxLevel;

	public GameObject btnBuyPlaneCoin;

	public GameObject btnBuyPlaneGem;

	public ParticleSystem effectUpgrade;

	public Image imgBtnCoinUpgrade;

	public Image imgBtnGemUpgrade;

	public Sprite sprBtnUpgradePlaneCoin;

	public Sprite sprBtnUpgradePlaneGem;

	public Sprite sprDisableBtnUpgradeCoin;

	public EvolvePlaneManager evolvePlaneManager;

	public Text textNextMaxLvl2;

	public Image imgRank;

	public DOTweenAnimation dOTween;

	public SkeletonGraphic skeletonGraphicPlane;

	public SkeletonGraphic skeletonGraphicBtnSelectPlane;

	public GameObject objArrowMaxLevel;

	public Transform objTextMaxLevel;

	public Text textPower;

	private int currentIDRank;

	private Action<Component, object> actionCoinChange;

	private Action<Component, object> actionGemChange;

	private int currentLvl;

	private int maxLevel;

	private int priceGemUpgrade;

	private bool check;

	private int numberUpgradePlane;

	private float fireRateMainGun;

	private float accurateMainGun;

	private float fireRateSubGun;

	private float accurateSubGun;

	private int numberBulletMainGun;

	private int numberBulletSubGun;

	private int baseMainDamage;

	private int baseSubDamage;
}
