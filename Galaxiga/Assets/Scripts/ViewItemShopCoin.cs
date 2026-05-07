using System;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ViewItemShopCoin : MonoBehaviour
{
	private bool IsCardPlane()
	{
		return this.rewardType != PrizeManager.RewardType.Card_Plane;
	}

	private bool IsCardDrone()
	{
		return this.rewardType != PrizeManager.RewardType.Card_Drone;
	}

	private void Awake()
	{
		this.canBuy = SaveDataStateItemShopCoin.StateItem(this.indexItem);
		this.btnShowInfoItem = base.GetComponent<Button>();
		this.btnShowInfoItem.onClick.AddListener(new UnityAction(this.ShowPopupInfoItem));
		this.btnBuy.onClick.AddListener(new UnityAction(this.BuyItem));
		if (this.rewardType == PrizeManager.RewardType.Card_Plane)
		{
			this.RandomPlane();
		}
		if (this.rewardType == PrizeManager.RewardType.Card_Drone)
		{
			this.RandomDrone();
		}
		this.SetView();
	}

	private void Start()
	{
	}

	private void SetView()
	{
		if (this.canBuy)
		{
			this.shopItemManager.SetActiveButton(this.imgBtnBuy);
		}
		else
		{
			this.shopItemManager.SetDeactiveButton(this.imgBtnBuy);
		}
		switch (this.rewardType)
		{
		case PrizeManager.RewardType.Card_All_Plane:
			this.numberItem = ShopCoinItemSheet.Get(0).numberItem;
			this.priceItem = ShopCoinItemSheet.Get(0).priceItem;
			break;
		case PrizeManager.RewardType.Card_All_Drone:
			this.numberItem = ShopCoinItemSheet.Get(1).numberItem;
			this.priceItem = ShopCoinItemSheet.Get(1).priceItem;
			break;
		case PrizeManager.RewardType.Gem:
			this.numberItem = ShopCoinItemSheet.Get(11).numberItem;
			this.priceItem = ShopCoinItemSheet.Get(11).priceItem;
			break;
		case PrizeManager.RewardType.Energy:
			this.numberItem = ShopCoinItemSheet.Get(4).numberItem;
			this.priceItem = ShopCoinItemSheet.Get(4).priceItem;
			break;
		case PrizeManager.RewardType.Key_Super_Prize:
			this.numberItem = ShopCoinItemSheet.Get(5).numberItem;
			this.priceItem = ShopCoinItemSheet.Get(5).priceItem;
			break;
		case PrizeManager.RewardType.Item_Heart:
			this.numberItem = ShopCoinItemSheet.Get(6).numberItem;
			this.priceItem = ShopCoinItemSheet.Get(6).priceItem;
			break;
		case PrizeManager.RewardType.Item_Bullet_10:
			this.numberItem = ShopCoinItemSheet.Get(8).numberItem;
			this.priceItem = ShopCoinItemSheet.Get(8).priceItem;
			break;
		case PrizeManager.RewardType.Item_Bullet_5:
			this.numberItem = ShopCoinItemSheet.Get(7).numberItem;
			this.priceItem = ShopCoinItemSheet.Get(7).priceItem;
			break;
		case PrizeManager.RewardType.Item_Shield:
			this.numberItem = ShopCoinItemSheet.Get(9).numberItem;
			this.priceItem = ShopCoinItemSheet.Get(9).priceItem;
			break;
		case PrizeManager.RewardType.Special_Skill:
			this.numberItem = ShopCoinItemSheet.Get(10).numberItem;
			this.priceItem = ShopCoinItemSheet.Get(10).priceItem;
			break;
		case PrizeManager.RewardType.Card_Plane:
			this.numberItem = ShopCoinItemSheet.Get(2).numberItem;
			this.priceItem = ShopCoinItemSheet.Get(2).priceItem;
			break;
		case PrizeManager.RewardType.Card_Drone:
			this.numberItem = ShopCoinItemSheet.Get(3).numberItem;
			this.priceItem = ShopCoinItemSheet.Get(3).priceItem;
			break;
		}
		this.textNumberItem.text = "x" + this.numberItem;
		this.textPrice.text = string.Empty + this.priceItem;
	}

	private void ShowPopupInfoItem()
	{
		if (this.rewardType == PrizeManager.RewardType.Card_Plane)
		{
			InfoItemReward.current._planeID = this.planeID;
		}
		else if (this.rewardType == PrizeManager.RewardType.Card_Drone)
		{
			InfoItemReward.current._droneID = this.droneID;
		}
		InfoItemReward.current.ShowPopupInfoItem(this.rewardType, this.numberItem);
	}

	public void BuyItem()
	{
		if (this.canBuy)
		{
			if (ShopContext.currentCoin >= this.priceItem)
			{
				this.canBuy = false;
				switch (this.rewardType)
				{
				case PrizeManager.RewardType.Card_All_Plane:
					CacheGame.AddCardAllPlane(this.numberItem);
					GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_All_Plane, this.numberItem);
					break;
				case PrizeManager.RewardType.Card_All_Drone:
					CacheGame.AddCardAllDrone(this.numberItem);
					GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_All_Drone, this.numberItem);
					break;
				case PrizeManager.RewardType.Gem:
					CacheGame.AddGems(this.numberItem);
					GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, this.numberItem);
					CurrencyLog.LogGemIn(this.numberItem, CurrencyLog.In.GemToCoin, "Shop_Item_Coin");
					break;
				case PrizeManager.RewardType.Energy:
					ShopEnergy.current.AddEnergy(this.numberItem);
					GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Energy, this.numberItem);
					break;
				case PrizeManager.RewardType.Key_Super_Prize:
				{
					int num = CacheGame.KeySuperRize;
					num += this.numberItem;
					CacheGame.KeySuperRize = num;
					GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Key_Super_Prize, this.numberItem);
					break;
				}
				case PrizeManager.RewardType.Item_Heart:
				{
					int num2 = CacheGame.NumberItemPowerHeart;
					num2 += this.numberItem;
					CacheGame.NumberItemPowerHeart = num2;
					GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Heart, this.numberItem);
					break;
				}
				case PrizeManager.RewardType.Item_Bullet_10:
				{
					int num3 = CacheGame.NumberItemPowerUpBullet10;
					num3 += this.numberItem;
					CacheGame.NumberItemPowerUpBullet5 = num3;
					GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Bullet_10, this.numberItem);
					break;
				}
				case PrizeManager.RewardType.Item_Bullet_5:
				{
					int num4 = CacheGame.NumberItemPowerUpBullet5;
					num4 += this.numberItem;
					CacheGame.NumberItemPowerUpBullet5 = num4;
					GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Bullet_5, this.numberItem);
					break;
				}
				case PrizeManager.RewardType.Item_Shield:
				{
					int num5 = CacheGame.NumberItemPowerShield;
					num5 += this.numberItem;
					CacheGame.NumberItemPowerShield = num5;
					GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Shield, this.numberItem);
					break;
				}
				case PrizeManager.RewardType.Special_Skill:
					CacheGame.AddSkillPlane(this.numberItem);
					GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Special_Skill, this.numberItem);
					break;
				case PrizeManager.RewardType.Card_Plane:
					DataGame.Current.AddCardPlane(this.planeID, this.numberItem);
					GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_Plane, this.numberItem, this.planeID);
					break;
				case PrizeManager.RewardType.Card_Drone:
					DataGame.Current.AddCardDrone(this.droneID, this.numberItem);
					DroneManager.current.CheckEnableEvolveDrone();
					GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_Drone, this.numberItem, this.droneID);
					break;
				}
				GetRewardSuccess.current.ShowPanelGetRewardSuccess();
				CacheGame.MinusCoins(this.priceItem);
				CurrencyLog.LogGoldOut(this.priceItem, CurrencyLog.Out.ShopItemCoin, this.rewardType.ToString());
				SaveDataStateItemShopCoin.SetData(this.indexItem);
				this.shopItemManager.SetDeactiveButton(this.imgBtnBuy);
			}
			else
			{
				ShopManager.Instance.ShowPopupNotEnoughCoin();
			}
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_you_cant_buy_item_any_more);
		}
	}

	public void RandomPlane()
	{
		this.planeID = DataGame.Current.RandomPlaneID;
		DataGame.Current.SetSpriteImagePlane(this.imgPlane, this.planeID);
		this.itemRewardInGame._planeID = this.planeID;
	}

	public void RandomDrone()
	{
		this.droneID = DataGame.Current.RandomDroneID();
		DataGame.Current.SetSpriteImageDrone(this.imgDrone, this.droneID);
		this.itemRewardInGame._droneID = this.droneID;
	}

	public void ResetItem()
	{
		if (!this.canBuy)
		{
			this.canBuy = true;
			this.shopItemManager.SetActiveButton(this.imgBtnBuy);
		}
		if (!this.IsCardPlane())
		{
			this.RandomPlane();
		}
		if (!this.IsCardDrone())
		{
			this.RandomDrone();
		}
	}

	public int indexItem;

	public ShopItemManager shopItemManager;

	public PrizeManager.RewardType rewardType;

	public Text textPrice;

	public Text textNumberItem;

	public Button btnBuy;

	public Image imgBtnBuy;

	[HideIf("IsCardPlane", true)]
	public Image imgPlane;

	[HideIf("IsCardDrone", true)]
	public Image imgDrone;

	public ItemRewardInGame itemRewardInGame;

	private int numberItem;

	private int priceItem;

	private GameContext.Plane planeID;

	private GameContext.Drone droneID;

	private Button btnShowInfoItem;

	private bool canBuy;
}
