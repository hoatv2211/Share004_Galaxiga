using System;
using DG.Tweening;
using Hellmade.Sound;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class ViewInfoDrone : MonoBehaviour
{
	private void Awake()
	{
	}

	private void GetDataLevel()
	{
		this.level = this.droneData.Level;
		if (this.level <= 0)
		{
			this.droneData.Level = 1;
			this.level = 1;
		}
		this.currentMaxLevel = this.droneData.MaxLevel;
		this.numberUpgradeDrone = SaveDataQuest.ProgressQuest(DailyQuestManager.Quest.upgrade_drone);
	}

	private void Start()
	{
		this.GetDataLevel();
		this.CheckPriceGemUpgrade();
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
		this.CheckIsOwenedDrone();
		this.SetTextDescritptionDrone();
		this.SetViewInfoDrone();
	}

	private void CheckCoin()
	{
		int coinUpgrade = this.droneData.CoinUpgrade;
		this.CheckPriceGemUpgrade();
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
		this.CheckPriceGemUpgrade();
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
		string currentRank = this.droneData.CurrentRank;
		int num = this.currentMaxLevel - this.level;
		if (num < 0)
		{
			return;
		}
		if (num < 10)
		{
			this.priceGemUpgrade = DroneCostUpgradeSheet.Get(this.level).priceGem;
			this.textAdd10Level.text = "+" + num + " LVL";
		}
		else
		{
			this.priceGemUpgrade = this.droneData.GemUpgrade;
			this.textAdd10Level.text = "+10 LVL";
		}
		if (GameContext.currentVIP > 4 && GameContext.currentVIP < 10)
		{
			this.priceGemUpgrade -= (int)((float)this.priceGemUpgrade * 0.05f);
		}
		else if (GameContext.currentVIP >= 10 && GameContext.currentVIP < 12)
		{
			this.priceGemUpgrade -= (int)((float)this.priceGemUpgrade * 0.15f);
		}
		else if (GameContext.currentVIP >= 12)
		{
			this.priceGemUpgrade -= (int)((float)this.priceGemUpgrade * 0.25f);
		}
	}

	private void SetTextDescritptionDrone()
	{
		if (!this.droneData.IsOwned)
		{
			switch (this.droneData.droneID)
			{
			case GameContext.Drone.GatlingGun:
				this.textDescription.text = ScriptLocalization.super_heavy_drone;
				break;
			case GameContext.Drone.AutoGatlingGun:
				this.textDescription.text = ScriptLocalization.a_powerful_military;
				break;
			case GameContext.Drone.Laser:
				this.textDescription.text = ScriptLocalization.medium_drone_quipped;
				break;
			case GameContext.Drone.Nighturge:
				this.textDescription.text = ScriptLocalization.high_drone_bomb;
				break;
			case GameContext.Drone.GodOfThunder:
				this.textDescription.text = ScriptLocalization.high_drone_thunder;
				break;
			case GameContext.Drone.Terigon:
				this.textDescription.text = ScriptLocalization.high_drone_fire;
				break;
			}
		}
		this.textNameDrone.text = this.droneData.NameDrone;
		this.SetTextRank();
		this.SetTextPriceUpgrade();
	}

	public void SetViewInfoDrone()
	{
		this.textLevelDrone.text = string.Concat(new object[]
		{
			"LVL ",
			this.level,
			"/",
			this.currentMaxLevel
		});
		this.textLevelDrone2.text = string.Empty + this.level;
		int dps = this.droneData.DPS;
		int nexDPSRank = this.droneData.NexDPSRank;
		if (this.level < 200)
		{
			this.textPower.text = string.Concat(new object[]
			{
				ScriptLocalization.power,
				": ",
				dps,
				string.Format("<color=#13be07ff>+{0}</color>", nexDPSRank - dps)
			});
		}
		else
		{
			this.textPower.text = ScriptLocalization.power + ": " + dps;
		}
	}

	public void SetTextRank()
	{
		string currentRank = this.droneData.CurrentRank;
		DataGame.Current.SetImageRank(this.imgRank, currentRank);
		this.currentMaxLevel = this.droneData.MaxLevel;
		this.level = this.droneData.Level;
		this.textNextMaxLevel.text = string.Empty + this.droneData.NextMaxLevel;
	}

	private void SetTextPriceUpgrade()
	{
		this.textPriceCoinUpgrade.text = string.Empty + this.droneData.CoinUpgrade;
		this.textPriceGemUpgrade.text = string.Empty + this.priceGemUpgrade;
	}

	private void CheckIsOwenedDrone()
	{
		if (CacheGame.IsOwnedDrone(this.droneData.droneID))
		{
			this.panelUpgrade.SetActive(true);
			if (this.level == this.currentMaxLevel)
			{
				this.panelMaxLevel.SetActive(true);
				this.panelBtnUpgrades.SetActive(false);
			}
		}
		else
		{
			this.textDescription.gameObject.SetActive(true);
		}
	}

	public void ShowPanelUpgrade()
	{
		this.panelUpgrade.SetActive(true);
		this.textDescription.gameObject.SetActive(false);
	}

	public void UpgradeDrone()
	{
		int coinUpgrade = this.droneData.CoinUpgrade;
		if (ShopContext.currentCoin >= coinUpgrade)
		{
			if (this.level < this.currentMaxLevel)
			{
				this.level++;
				this.droneData.Level = this.level;
				CacheGame.MinusCoins(coinUpgrade);
				CurrencyLog.LogGoldOut(coinUpgrade, CurrencyLog.Out.UpgradeDrone, this.droneData.droneID.ToString());
				this.effectUpgrade.Play();
				this.SetViewInfoDrone();
				this.SetTextPriceUpgrade();
				this.textLevelDrone2.text = string.Empty + this.level;
				this.dOTween.DORestart(false);
				this.dOTween.DOPlay();
				if (this.level == this.currentMaxLevel)
				{
					this.ShowPanelMaxLevel();
				}
				EazySoundManager.PlayUISound(AudioCache.UISound.upgrade_plane_drone);
				if (this.numberUpgradeDrone < 2)
				{
					SaveDataQuest.SetProcessQuest(DailyQuestManager.Quest.upgrade_drone, 1);
					this.numberUpgradeDrone++;
				}
				if (!PlaneManager.current.needUpdateToServer)
				{
					PlaneManager.current.needUpdateToServer = true;
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

	public void UpgradeDroneGem()
	{
		int num = this.droneData.Level;
		if (num < this.droneData.MaxLevel)
		{
			if (ShopContext.currentGem >= this.priceGemUpgrade)
			{
				int num2 = num + 10;
				if (num2 > this.currentMaxLevel)
				{
					num2 = this.currentMaxLevel;
				}
				this.droneData.Level = num2;
				this.level = num2;
				CacheGame.MinusGems(this.priceGemUpgrade);
				CurrencyLog.LogGemOut(this.priceGemUpgrade, CurrencyLog.Out.UpgradeDrone, this.droneData.droneID.ToString());
				this.effectUpgrade.Play();
				this.SetViewInfoDrone();
				this.SetTextPriceUpgrade();
				this.textLevelDrone2.text = string.Empty + num2;
				this.dOTween.DORestart(false);
				this.dOTween.DOPlay();
				if (this.level == this.currentMaxLevel)
				{
					this.ShowPanelMaxLevel();
				}
				EazySoundManager.PlayUISound(AudioCache.UISound.upgrade_plane_drone);
				if (this.numberUpgradeDrone < 2)
				{
					SaveDataQuest.SetProcessQuest(DailyQuestManager.Quest.upgrade_drone, 1);
					this.numberUpgradeDrone++;
				}
				if (!PlaneManager.current.needUpdateToServer)
				{
					PlaneManager.current.needUpdateToServer = true;
				}
			}
			else
			{
				UnityEngine.Debug.Log("Not Enough Gem");
				ShopManager.Instance.ShowPopupNotEnoughGem();
			}
		}
		else
		{
			UnityEngine.Debug.Log("Max Level");
			this.ShowPanelMaxLevel();
		}
	}

	public void ShowPanelMaxLevel()
	{
		this.panelBtnUpgrades.SetActive(false);
		this.panelMaxLevel.SetActive(true);
	}

	public void ShowPanelUpgradeDrone()
	{
		this.panelBtnUpgrades.SetActive(true);
		this.panelMaxLevel.SetActive(false);
	}

	public void EvolutionDroneComplete()
	{
		this.SetTextRank();
		this.CheckPriceGemUpgrade();
		this.SetViewInfoDrone();
		this.ShowPanelUpgradeDrone();
		this.SetTextPriceUpgrade();
	}

	public DroneData droneData;

	public Text textNameDrone;

	public Text textLevelDrone;

	public Text textLevelDrone2;

	public Text textPower;

	public GameObject panelUpgrade;

	public GameObject panelMaxLevel;

	public GameObject panelBtnUpgrades;

	public Text textDescription;

	public Text textPriceCoinUpgrade;

	public Text textPriceGemUpgrade;

	public Text textAdd10Level;

	public ParticleSystem effectUpgrade;

	public Text textNextMaxLevel;

	public Image imgBtnCoinUpgrade;

	public Image imgBtnGemUpgrade;

	public Sprite sprBtnUpgradePlaneCoin;

	public Sprite sprBtnUpgradePlaneGem;

	public Sprite sprDisableBtnUpgradeCoin;

	public Image imgRank;

	public DOTweenAnimation dOTween;

	private int currentMaxLevel;

	private int level;

	private int priceGemUpgrade;

	private Action<Component, object> actionCoinChange;

	private Action<Component, object> actionGemChange;

	private bool check;

	private int nextPower;

	private int nextSpecPower;

	private int numberUpgradeDrone;
}
