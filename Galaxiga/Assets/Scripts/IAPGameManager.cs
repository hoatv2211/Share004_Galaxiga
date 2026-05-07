using System;
using EasyMobile;
using I2.Loc;
using UnityEngine;
using UnityEngine.Purchasing.Security;
using UnityEngine.UI;

public class IAPGameManager : MonoBehaviour
{
	public static IAPGameManager Current
	{
		get
		{
			if (IAPGameManager._ins == null)
			{
				IAPGameManager._ins = UnityEngine.Object.FindObjectOfType<IAPGameManager>();
			}
			return IAPGameManager._ins;
		}
	}

	private void Awake()
	{
	}

	private void OnEnable()
	{
		//InAppPurchasing.PurchaseCompleted += this.IAPManager_PurchaseCompleted;
		//InAppPurchasing.PurchaseFailed += this.IAPManager_PurchaseFailed;
		//InAppPurchasing.RestoreCompleted += this.IAPManager_RestoreCompleted;
		//InAppPurchasing.RestoreFailed += this.RestoreFailedHandler;
	}

	private void OnDisable()
	{
		//InAppPurchasing.PurchaseCompleted -= this.IAPManager_PurchaseCompleted;
		//InAppPurchasing.PurchaseFailed -= this.IAPManager_PurchaseFailed;
		//InAppPurchasing.RestoreCompleted -= this.IAPManager_RestoreCompleted;
		//InAppPurchasing.RestoreFailed -= this.RestoreFailedHandler;
	}

	private void Start()
	{
	}

	public void SetTextPrice()
	{
		//if (!this.isSetTextPrice)
		//{
		//	this.currencyCode = InAppPurchasing.GetCurrencyCode("Premium Pack");
		//	this.isSetTextPrice = true;
		//	this.textPricePremiunPack2.text = InAppPurchasing.GetPrice("Premium Pack");
		//	this.textPricePremiunPack2NoSale.text = InAppPurchasing.GetPrice("Ultra Starship Card");
		//	this.textPriceStarterPack.text = InAppPurchasing.GetPrice("Pack 2");
		//	this.textPriceStarterPackDiscount.text = InAppPurchasing.GetPrice("Starter Pack");
		//	this.textPriceStarterPack2.text = InAppPurchasing.GetPrice("Starter Pack");
		//	this.textPriceStarterPack2NoSale.text = InAppPurchasing.GetPrice("Pack 2");
		//	this.textPricePremiumPack.text = InAppPurchasing.GetPrice("Premium Pack");
		//	this.textPricePremiumPackNoSale.text = InAppPurchasing.GetPrice("Ultra Starship Card");
		//	this.textPricePackOneTime.text = InAppPurchasing.GetPrice("Pack 1");
		//	this.textPricePackOneTimeNoSale.text = InAppPurchasing.GetPrice("Ultra Starship Card");
		//	this.gemPack0 = this.shopGem.arrPackGem[5].numberGem;
		//	this.gemPack1 = this.shopGem.arrPackGem[0].numberGem;
		//	this.gemPack2 = this.shopGem.arrPackGem[1].numberGem;
		//	this.gemPack3 = this.shopGem.arrPackGem[2].numberGem;
		//	this.gemPack4 = this.shopGem.arrPackGem[3].numberGem;
		//	this.gemPack5 = this.shopGem.arrPackGem[4].numberGem;
		//	this.textGemPack1.text = string.Empty + this.gemPack1;
		//	this.textGemPack2.text = string.Empty + this.gemPack2;
		//	this.textGemPack3.text = string.Empty + this.gemPack3;
		//	this.textGemPack4.text = string.Empty + this.gemPack4;
		//	this.textGemPack5.text = string.Empty + this.gemPack5;
		//	this.textPackCoins[0].text = InAppPurchasing.GetPrice("10000 Coins");
		//	this.textPackCoins[1].text = InAppPurchasing.GetPrice("62500 Coins");
		//	this.textPackCoins[2].text = InAppPurchasing.GetPrice("1000000 Coins");
		//	this.textPriceUltraStarshipCardOneTime.text = InAppPurchasing.GetPrice("Ultra Starship Card");
		//	this.textPriceUltraDroneCardOneTime.text = InAppPurchasing.GetPrice("Ultra Drone Card");
		//	this.shopGem.SetTextLocalPrice();
		//}
	}

	public void SetTextLocalizePricePackOffer(Text textPrice, Text textPriceSale, IAPGameManager.Pack packID)
	{
		//switch (packID)
		//{
		//case IAPGameManager.Pack.Super_Spaceship_Pack:
		//	textPrice.text = InAppPurchasing.GetPrice("Super Spaceship Pack");
		//	textPriceSale.text = InAppPurchasing.GetPrice("Super Spaceship Pack Sale 50");
		//	break;
		//case IAPGameManager.Pack.Warlock_Pack:
		//	textPrice.text = InAppPurchasing.GetPrice("Warlock Pack");
		//	textPriceSale.text = InAppPurchasing.GetPrice("Warlock Pack Sale 60");
		//	break;
		//case IAPGameManager.Pack.SS_Lightning_Pack:
		//	textPrice.text = InAppPurchasing.GetPrice("SS Lightning Pack");
		//	textPriceSale.text = InAppPurchasing.GetPrice("SS Lightning Pack Sale 60");
		//	break;
		//case IAPGameManager.Pack.Spaceship_Pack:
		//	textPrice.text = InAppPurchasing.GetPrice("Sapceship Pack");
		//	textPriceSale.text = InAppPurchasing.GetPrice("Starship Pack Sale 60");
		//	break;
		//case IAPGameManager.Pack.Elite_Pack:
		//	textPrice.text = InAppPurchasing.GetPrice("Elite Pack");
		//	textPriceSale.text = InAppPurchasing.GetPrice("Elite Pack Sale 60");
		//	break;
		//case IAPGameManager.Pack.Super_Booster_Pack:
		//	textPrice.text = InAppPurchasing.GetPrice("Super Booster Pack");
		//	textPriceSale.text = InAppPurchasing.GetPrice("Super Booster Pack Sale 60");
		//	break;
		//case IAPGameManager.Pack.Booster_Pack:
		//	textPrice.text = InAppPurchasing.GetPrice("Booster Pack");
		//	textPriceSale.text = InAppPurchasing.GetPrice("Booster Pack Sale 70");
		//	break;
		//case IAPGameManager.Pack.Offer_Pack:
		//	textPrice.text = InAppPurchasing.GetPrice("Offer Pack");
		//	textPriceSale.text = InAppPurchasing.GetPrice("Offer Pack Sale 70");
		//	break;
		//}
	}

	public string GetLocalizePrice(string price)
	{
        //return InAppPurchasing.GetPrice(price);
        return "";
	}

	public void SetTextLocalizePrice(Text textPrice, string rank)
	{
		//if (rank == GameContext.Rank.C.ToString())
		//{
		//	textPrice.text = InAppPurchasing.GetPrice("Price Plane Rank C");
		//}
		//else if (rank == GameContext.Rank.B.ToString())
		//{
		//	textPrice.text = InAppPurchasing.GetPrice("Price Plane Rank B");
		//}
		//else if (rank == GameContext.Rank.A.ToString())
		//{
		//	textPrice.text = InAppPurchasing.GetPrice("Price Plane Rank A");
		//}
		//else if (rank == GameContext.Rank.S.ToString())
		//{
		//	textPrice.text = InAppPurchasing.GetPrice("Price Plane Rank S");
		//}
	}

	public void SetTextLocalizePricePackCandy(Text textPrice, int packIndex)
	{
		//if (packIndex != 0)
		//{
		//	if (packIndex != 1)
		//	{
		//		if (packIndex == 2)
		//		{
		//			textPrice.text = InAppPurchasing.GetPrice("Candy Mini Pack");
		//		}
		//	}
		//	else
		//	{
		//		textPrice.text = InAppPurchasing.GetPrice("Candy Pack");
		//	}
		//}
		//else
		//{
		//	textPrice.text = InAppPurchasing.GetPrice("Candy Big Pack");
		//}
	}

	private void RestoreFailedHandler()
	{
	}

	//private void IAPManager_PurchaseFailed(IAPProduct obj)
	//{
	//	global::Notification.Current.ShowNotification(ScriptLocalization.notify_payment_failed);
	//}

	private void IAPManager_RestoreCompleted()
	{
	}

	//private void IAPManager_PurchaseCompleted(IAPProduct product)
	//{
	//	this.ReadGooglePlayReceipt(product.Name);
	//}

	private void ReadGooglePlayReceipt(string productName)
	{
		//GooglePlayReceipt googlePlayReceipt = InAppPurchasing.GetGooglePlayReceipt(productName);
		//if (googlePlayReceipt != null)
		//{
		//	switch (productName)
		//	{
		//	case "Premium Pack":
		//		this.PurchasePremiumPackSuccess();
		//		break;
		//	case "Pack 1":
		//		this.PurchasePack1Success();
		//		break;
		//	case "Pack 2":
		//		this.PurchasePack2Success();
		//		break;
		//	case "Pack 3":
		//		this.PurchasePack3Success();
		//		break;
		//	case "Pack 4":
		//		this.PurchasePack4Success();
		//		break;
		//	case "Pack 5":
		//		this.PurchasePack5Success();
		//		break;
		//	case "Pack 6":
		//		this.PurchasePack0Success();
		//		break;
		//	case "Price Plane Rank C":
		//		this.PurchasePlaneSuccess();
		//		this.vipPointPack = 2;
		//		break;
		//	case "Price Plane Rank B":
		//		this.PurchasePlaneSuccess();
		//		this.vipPointPack = 5;
		//		break;
		//	case "Price Plane Rank A":
		//		this.PurchasePlaneSuccess();
		//		this.vipPointPack = 10;
		//		break;
		//	case "Price Plane Rank S":
		//		this.PurchasePlaneSuccess();
		//		this.vipPointPack = 20;
		//		break;
		//	case "Starter Pack":
		//		this.PurchaseStarterPackSuccess();
		//		break;
		//	case "Plane SSLightning":
		//		this.PurchasePlaneSuccess();
		//		break;
		//	case "Plane Warlock":
		//		this.PurchasePlaneSuccess();
		//		break;
		//	case "10000 Coins":
		//		this.PurchaseCoinSuccess(0);
		//		break;
		//	case "62500 Coins":
		//		this.PurchaseCoinSuccess(1);
		//		break;
		//	case "150000 Coins":
		//		this.PurchaseCoinSuccess(2);
		//		break;
		//	case "350000 Coins":
		//		this.PurchaseCoinSuccess(3);
		//		break;
		//	case "1000000 Coins":
		//		this.PurchaseCoinSuccess(4);
		//		break;
		//	case "Super Spaceship Pack":
		//		this.PurchasePackSuccess(0);
		//		break;
		//	case "Super Spaceship Pack Sale 50":
		//		this.saleManager.StopSale();
		//		this.PurchasePackSuccess(0);
		//		break;
		//	case "Warlock Pack":
		//		this.PurchasePackSuccess(1);
		//		break;
		//	case "Warlock Pack Sale 60":
		//		this.PurchasePackSuccess(1);
		//		this.saleManager.StopSale();
		//		break;
		//	case "SS Lightning Pack":
		//		this.PurchasePackSuccess(2);
		//		break;
		//	case "SS Lightning Pack Sale 60":
		//		this.PurchasePackSuccess(2);
		//		this.saleManager.StopSale();
		//		break;
		//	case "Sapceship Pack":
		//		this.PurchasePackSuccess(3);
		//		break;
		//	case "Starship Pack Sale 60":
		//		this.PurchasePackSuccess(3);
		//		this.saleManager.StopSale();
		//		break;
		//	case "Elite Pack":
		//		this.PurchasePackSuccess(4);
		//		break;
		//	case "Elite Pack Sale 60":
		//		this.PurchasePackSuccess(4);
		//		this.saleManager.StopSale();
		//		break;
		//	case "Super Booster Pack":
		//		this.PurchasePackSuccess(5);
		//		break;
		//	case "Super Booster Pack Sale 60":
		//		this.PurchasePackSuccess(5);
		//		this.saleManager.StopSale();
		//		break;
		//	case "Booster Pack":
		//		this.PurchasePackSuccess(6);
		//		break;
		//	case "Booster Pack Sale 70":
		//		this.PurchasePackSuccess(6);
		//		this.saleManager.StopSale();
		//		break;
		//	case "Offer Pack":
		//		this.PurchasePackSuccess(7);
		//		break;
		//	case "Offer Pack Sale 70":
		//		this.PurchasePackSuccess(7);
		//		this.saleManager.StopSale();
		//		break;
		//	case "Ultra Starship Card":
		//		this.PurchasePackUltraStarshipCardOneTimeSuccess();
		//		break;
		//	case "Ultra Drone Card":
		//		this.PurchasePackUltraDroneCardOneTimeSuccess();
		//		break;
		//	case "Pack One Time":
		//		this.PurchasePackOneTime1Success();
		//		break;
		//	case "Endless Mode Pack One Time":
		//		this.PurchasePackOneTimeEndlessModeSuccess();
		//		break;
		//	}
		//	this.currentPrice = InAppPurchasing.GetPrice(productName);
		//	if (this.currencyCode == string.Empty)
		//	{
		//		this.currencyCode = InAppPurchasing.GetCurrencyCode(productName);
		//	}
		//	CurrencyLog.LogInapp(googlePlayReceipt.productID, googlePlayReceipt.transactionID, googlePlayReceipt.purchaseToken, this.currencyCode, this.currentPrice);
		//	SpaceForceFirebaseLogger.InappPurchase(googlePlayReceipt.productID, this.currentPrice);
		//	this.UpdateVIPPoint();
		//	AccountManager.Instance.UpdateToServer("Inapp Purchase");
		//}
	}

	private void UpdateVIPPoint()
	{
		GameContext.currentVIPPoint += this.vipPointPack;
		CacheGame.VIPPoint = GameContext.currentVIPPoint;
		VIPManager.current.UpdateVIPPoint();
		if (HomeManager.current != null)
		{
			HomeManager.current.CheckShowNotificationVIP();
		}
	}

	public void PurchasePremiumPack()
	{
		//if (!GameContext.isPurchasePremiumPack)
		//{
		//	InAppPurchasing.Purchase("Premium Pack");
		//}
	}

	public void PurchasePremiumPackSuccess()
	{
		this.vipPointPack = 3;
		GameContext.isPurchasePremiumPack = true;
		CacheGame.IsPurchasePremiumPack = true;
		CacheGame.AddGems(300);
		CurrencyLog.LogGemIn(this.gemPack3, CurrencyLog.In.Inapp, "Premium_Pack");
		GameContext.numberFreeRespawnPlayer = 1;
		CacheGame.EndTimePremiumPack = DateTime.Now.AddDays(30.0).ToBinary().ToString();
		ShopManager.Instance.HidePremiumPack();
		ShopManager.Instance.ShowPanelBuyPremiumPackSuccess();
		CacheGame.IsRemoveAds = true;
		GameContext.isRemoveAds = true;
		ShopEnergy.current.ActiveIconInfinity();
	}

	public void PurchaseStaterPack()
	{
		//if (!GameContext.isPurchaseStarterPack)
		//{
		//	InAppPurchasing.Purchase("Starter Pack");
		//}
	}

	private void PurchaseStarterPackSuccess()
	{
		CacheGame.IsPurchaseStarterPack = true;
		GameContext.isPurchaseStarterPack = true;
		this.vipPointPack = 2;
		CacheGame.GetPlane(GameContext.Plane.SkyWraith);
		CacheGame.SetOwnedDrone(GameContext.Drone.AutoGatlingGun);
		CacheGame.SetOwnedDrone(GameContext.Drone.Laser);
		CacheGame.SetLevelPlane(GameContext.Plane.SkyWraith, 20);
		PlaneManager.current.CheckPlane(GameContext.Plane.SkyWraith);
		CacheGame.SetLevelDrone(GameContext.Drone.AutoGatlingGun, 20);
		CacheGame.SetLevelDrone(GameContext.Drone.Laser, 20);
		DroneManager.current.CheckAllDrone();
		CacheGame.AddCoins(50000);
		CurrencyLog.LogGemIn(this.gemPack3, CurrencyLog.In.Inapp, "Starter_Pack");
		ShopManager.Instance.HideButtonBuyStarterPack();
		ShopManager.Instance.ShowPanelBuyStarterPackSuccess();
		if (PlaneManager.current != null)
		{
			PlaneManager.current.CheckPlaneStarterPack();
		}
		if (DroneManager.current != null)
		{
			DroneManager.current.CheckDroneStarterPack();
		}
		if (HomeManager.current != null)
		{
			HomeManager.current.btnBuyStarterPack.SetActive(false);
		}
	}

	public void PurchasePack0()
	{
		//InAppPurchasing.Purchase("Pack 6");
	}

	public void PurchasePack0Success()
	{
		CurrencyLog.LogGemIn(this.gemPack0, CurrencyLog.In.Inapp, "Gem_Pack_0");
		CacheGame.AddGems(this.gemPack0);
		this.AddGemPurchased(this.gemPack0);
		this.vipPointPack = 2;
		if (GameContext.currentIDPackGemX3 == 6)
		{
			this.shopGem.SetNextTimeX3Pack();
			this.shopGem.SetViewPack(5);
			this.shopGem.arrPackGem[5].DisableX3Pack();
		}
		else if (SaveDataStateOneTimeOfferPackGem.IsOfferPack(5))
		{
			this.shopGem.DisableOneTimeOfferPack(6);
		}
		this.gemPack0 = this.shopGem.arrPackGem[5].numberGem;
	}

	public void PurchasePack1()
	{
		//InAppPurchasing.Purchase("Pack 1");
	}

	public void PurchasePack1Success()
	{
		CurrencyLog.LogGemIn(this.gemPack1, CurrencyLog.In.Inapp, "Gem_Pack_1");
		CacheGame.AddGems(this.gemPack1);
		this.AddGemPurchased(this.gemPack1);
		this.vipPointPack = 5;
		if (GameContext.currentIDPackGemX3 == 1)
		{
			this.shopGem.SetNextTimeX3Pack();
			this.shopGem.SetViewPack(0);
			this.shopGem.arrPackGem[0].DisableX3Pack();
		}
		else if (SaveDataStateOneTimeOfferPackGem.IsOfferPack(0))
		{
			this.shopGem.DisableOneTimeOfferPack(1);
		}
		this.gemPack1 = this.shopGem.arrPackGem[0].numberGem;
	}

	public void PurchasePack2()
	{
		//InAppPurchasing.Purchase("Pack 2");
	}

	public void PurchasePack2Success()
	{
		CurrencyLog.LogGemIn(this.gemPack2, CurrencyLog.In.Inapp, "Gem_Pack_2");
		CacheGame.AddGems(this.gemPack2);
		this.AddGemPurchased(this.gemPack2);
		this.vipPointPack = 10;
		if (GameContext.currentIDPackGemX3 == 2)
		{
			this.shopGem.SetNextTimeX3Pack();
			this.shopGem.SetViewPack(1);
			this.shopGem.arrPackGem[1].DisableX3Pack();
		}
		else if (SaveDataStateOneTimeOfferPackGem.IsOfferPack(1))
		{
			this.shopGem.DisableOneTimeOfferPack(2);
		}
		this.gemPack2 = this.shopGem.arrPackGem[1].numberGem;
	}

	public void PurchasePack3()
	{
		//InAppPurchasing.Purchase("Pack 3");
	}

	public void PurchasePack3Success()
	{
		CurrencyLog.LogGemIn(this.gemPack3, CurrencyLog.In.Inapp, "Gem_Pack_3");
		CacheGame.AddGems(this.gemPack3);
		this.AddGemPurchased(this.gemPack3);
		this.vipPointPack = 20;
		if (GameContext.currentIDPackGemX3 == 3)
		{
			this.shopGem.SetNextTimeX3Pack();
			this.shopGem.SetViewPack(2);
			this.shopGem.arrPackGem[2].DisableX3Pack();
		}
		else if (SaveDataStateOneTimeOfferPackGem.IsOfferPack(2))
		{
			this.shopGem.DisableOneTimeOfferPack(3);
		}
		this.gemPack3 = this.shopGem.arrPackGem[2].numberGem;
	}

	public void PurchasePack4()
	{
		//InAppPurchasing.Purchase("Pack 4");
	}

	public void PurchasePack4Success()
	{
		CurrencyLog.LogGemIn(this.gemPack4, CurrencyLog.In.Inapp, "Gem_Pack_4");
		CacheGame.AddGems(this.gemPack4);
		this.AddGemPurchased(this.gemPack4);
		this.vipPointPack = 50;
		if (GameContext.currentIDPackGemX3 == 4)
		{
			this.shopGem.SetNextTimeX3Pack();
			this.shopGem.SetViewPack(3);
			this.shopGem.arrPackGem[3].DisableX3Pack();
		}
		else if (SaveDataStateOneTimeOfferPackGem.IsOfferPack(3))
		{
			this.shopGem.DisableOneTimeOfferPack(4);
		}
		this.gemPack4 = this.shopGem.arrPackGem[3].numberGem;
	}

	public void PurchasePack5()
	{
		//InAppPurchasing.Purchase("Pack 5");
	}

	public void PurchasePack5Success()
	{
		CurrencyLog.LogGemIn(this.gemPack5, CurrencyLog.In.Inapp, "Gem_Pack_5");
		CacheGame.AddGems(this.gemPack5);
		this.AddGemPurchased(this.gemPack5);
		this.vipPointPack = 100;
		if (GameContext.currentIDPackGemX3 == 5)
		{
			this.shopGem.SetNextTimeX3Pack();
			this.shopGem.SetViewPack(4);
			this.shopGem.arrPackGem[4].DisableX3Pack();
		}
		else if (SaveDataStateOneTimeOfferPackGem.IsOfferPack(4))
		{
			this.shopGem.DisableOneTimeOfferPack(5);
		}
		this.gemPack5 = this.shopGem.arrPackGem[4].numberGem;
	}

	public void AddGemPurchased(int number)
	{
		int num = CacheGame.NumberGemPurchased;
		num += number;
		CacheGame.NumberGemPurchased = num;
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, number);
		GetRewardSuccess.current.ShowPanelGetRewardSuccess();
	}

	public void PurchasePlaneRank(string rank)
	{
		//if (rank == GameContext.Rank.C.ToString())
		//{
		//	InAppPurchasing.Purchase("Price Plane Rank C");
		//}
		//else if (rank == GameContext.Rank.B.ToString())
		//{
		//	InAppPurchasing.Purchase("Price Plane Rank B");
		//}
		//else if (rank == GameContext.Rank.A.ToString())
		//{
		//	InAppPurchasing.Purchase("Price Plane Rank A");
		//}
		//else if (rank == GameContext.Rank.S.ToString())
		//{
		//	InAppPurchasing.Purchase("Price Plane Rank S");
		//}
	}

	public void PurchasePlaneWarlock()
	{
		//InAppPurchasing.Purchase("Plane Warlock");
	}

	public void PurchasePlaneSSLightning()
	{
		//InAppPurchasing.Purchase("Plane SSLightning");
	}

	public void PurchasePlaneWarlockSuccess()
	{
		PlaneManager.current.SetOwenedPlane(true);
	}

	public void PurchasePlaneSuccess()
	{
		PlaneManager.current.SetOwenedPlane(true);
	}

	private TimeSpan GetDistanceTime(string stringSaveTime)
	{
		long dateData = Convert.ToInt64(PlayerPrefs.GetString(stringSaveTime));
		DateTime value = DateTime.FromBinary(dateData);
		return DateTime.Now.Subtract(value);
	}

	[EnumAction(typeof(IAPGameManager.PackCoin))]
	public void PurchasePackCoin(int packID)
	{
		//switch (packID)
		//{
		//case 0:
		//	InAppPurchasing.Purchase("10000 Coins");
		//	break;
		//case 1:
		//	InAppPurchasing.Purchase("62500 Coins");
		//	break;
		//case 2:
		//	InAppPurchasing.Purchase("150000 Coins");
		//	break;
		//case 3:
		//	InAppPurchasing.Purchase("350000 Coins");
		//	break;
		//case 4:
		//	InAppPurchasing.Purchase("1000000 Coins");
		//	break;
		//}
	}

	public void PurchaseCoinSuccess(int packID)
	{
		int num = 0;
		switch (packID)
		{
		case 0:
			num = 10000;
			this.vipPointPack = 1;
			break;
		case 1:
			num = 62500;
			this.vipPointPack = 5;
			break;
		case 2:
			num = 150000;
			this.vipPointPack = 10;
			break;
		case 3:
			num = 350000;
			this.vipPointPack = 20;
			break;
		case 4:
			num = 1000000;
			this.vipPointPack = 50;
			break;
		}
		int value = num;
		CurrencyLog.In where = CurrencyLog.In.Inapp;
		IAPGameManager.PackCoin packCoin = (IAPGameManager.PackCoin)packID;
		CurrencyLog.LogGoldIn(value, where, packCoin.ToString());
		CacheGame.AddCoins(num);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, num);
		GetRewardSuccess.current.ShowPanelGetRewardSuccess();
	}

	[EnumAction(typeof(IAPGameManager.Pack))]
	public void PurchasePack(int packID)
	{
		//switch (packID)
		//{
		//case 0:
		//	InAppPurchasing.Purchase("Super Spaceship Pack");
		//	break;
		//case 1:
		//	InAppPurchasing.Purchase("Warlock Pack");
		//	break;
		//case 2:
		//	InAppPurchasing.Purchase("SS Lightning Pack");
		//	break;
		//case 3:
		//	InAppPurchasing.Purchase("Super Spaceship Pack");
		//	break;
		//case 4:
		//	InAppPurchasing.Purchase("Elite Pack");
		//	break;
		//case 5:
		//	InAppPurchasing.Purchase("Super Booster Pack");
		//	break;
		//case 6:
		//	InAppPurchasing.Purchase("Booster Pack");
		//	break;
		//case 7:
		//	InAppPurchasing.Purchase("Offer Pack");
		//	break;
		//}
	}

	public void PurchasePackSuccess(int packID)
	{
		switch (packID)
		{
		case 0:
			this.vipPointPack = 50;
			CacheGame.GetPlane(GameContext.Plane.Warlock);
			CacheGame.GetPlane(GameContext.Plane.SSLightning);
			CacheGame.AddCoins(500000);
			CurrencyLog.LogGoldIn(500000, CurrencyLog.In.Inapp, IAPGameManager.Pack.Super_Spaceship_Pack.ToString());
			if (PlaneManager.current != null)
			{
				PlaneManager.current.CheckAllPlane();
				PlaneManager.current.CheckEvolutionPlane(GameContext.Plane.Warlock);
				PlaneManager.current.CheckEvolutionPlane(GameContext.Plane.SSLightning);
			}
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Unlock_Plane, GameContext.Plane.Warlock);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Unlock_Plane, GameContext.Plane.SSLightning);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, 500000);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 1:
			this.vipPointPack = 25;
			CacheGame.GetPlane(GameContext.Plane.Warlock);
			CacheGame.AddCoins(200000);
			CurrencyLog.LogGoldIn(200000, CurrencyLog.In.Inapp, IAPGameManager.Pack.Warlock_Pack.ToString());
			if (PlaneManager.current != null)
			{
				PlaneManager.current.CheckAllPlane();
				PlaneManager.current.CheckEvolutionPlane(GameContext.Plane.Warlock);
			}
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Unlock_Plane, GameContext.Plane.Warlock);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, 200000);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 2:
			this.vipPointPack = 25;
			CacheGame.GetPlane(GameContext.Plane.SSLightning);
			CacheGame.AddCoins(200000);
			CurrencyLog.LogGoldIn(200000, CurrencyLog.In.Inapp, IAPGameManager.Pack.SS_Lightning_Pack.ToString());
			if (PlaneManager.current != null)
			{
				PlaneManager.current.CheckAllPlane();
				PlaneManager.current.CheckEvolutionPlane(GameContext.Plane.SSLightning);
			}
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Unlock_Plane, GameContext.Plane.SSLightning);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, 200000);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 3:
			this.vipPointPack = 20;
			CacheGame.GetPlane(GameContext.Plane.TwilightX);
			CacheGame.GetPlane(GameContext.Plane.Greataxe);
			CacheGame.AddCoins(100000);
			CurrencyLog.LogGoldIn(100000, CurrencyLog.In.Inapp, IAPGameManager.Pack.Spaceship_Pack.ToString());
			if (PlaneManager.current != null)
			{
				PlaneManager.current.CheckAllPlane();
				PlaneManager.current.CheckEvolutionPlane(GameContext.Plane.TwilightX);
				PlaneManager.current.CheckEvolutionPlane(GameContext.Plane.Greataxe);
			}
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Unlock_Plane, GameContext.Plane.TwilightX);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Unlock_Plane, GameContext.Plane.Greataxe);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, 100000);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 4:
			this.vipPointPack = 25;
			CurrencyLog.LogGoldIn(200000, CurrencyLog.In.Inapp, IAPGameManager.Pack.Elite_Pack.ToString());
			CurrencyLog.LogGemIn(2000, CurrencyLog.In.Inapp, IAPGameManager.Pack.Elite_Pack.ToString());
			CacheGame.AddGems(2000);
			CacheGame.AddCoins(200000);
			GameContext.totalItemPowerUpHeart += 60;
			CacheGame.NumberItemPowerHeart = GameContext.totalItemPowerUpHeart;
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Heart, 60);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, 200000);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, 2000);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 5:
			this.vipPointPack = 15;
			GameContext.totalItemPowerShield += 50;
			GameContext.totalItemPowerUpBullet10 += 50;
			GameContext.totalItemPowerUpHeart += 50;
			CacheGame.NumberItemPowerHeart = GameContext.totalItemPowerUpHeart;
			CacheGame.NumberItemPowerShield = GameContext.totalItemPowerShield;
			CacheGame.NumberItemPowerUpBullet10 = GameContext.totalItemPowerUpBullet10;
			CacheGame.AddSkillPlane(200);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Heart, 50);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Shield, 50);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Bullet_10, 50);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Special_Skill, 200);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 6:
			this.vipPointPack = 6;
			GameContext.totalItemPowerShield += 20;
			GameContext.totalItemPowerUpBullet10 += 20;
			GameContext.totalItemPowerUpHeart += 20;
			CacheGame.NumberItemPowerHeart = GameContext.totalItemPowerUpHeart;
			CacheGame.NumberItemPowerShield = GameContext.totalItemPowerShield;
			CacheGame.NumberItemPowerUpBullet10 = GameContext.totalItemPowerUpBullet10;
			CacheGame.AddSkillPlane(100);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Heart, 20);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Shield, 20);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Bullet_10, 20);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Special_Skill, 100);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 7:
			this.vipPointPack = 6;
			GameContext.totalItemPowerShield += 10;
			GameContext.totalItemPowerUpBullet10 += 10;
			GameContext.totalItemPowerUpHeart += 10;
			CacheGame.NumberItemPowerHeart = GameContext.totalItemPowerUpHeart;
			CacheGame.NumberItemPowerShield = GameContext.totalItemPowerShield;
			CacheGame.NumberItemPowerUpBullet10 = GameContext.totalItemPowerUpBullet10;
			CacheGame.AddCoins(50000);
			CurrencyLog.LogGoldIn(50000, CurrencyLog.In.Inapp, IAPGameManager.Pack.Offer_Pack.ToString());
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Heart, 10);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Shield, 10);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Bullet_10, 10);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, 50000);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		}
	}

	public void PurchaseCandyBigPack()
	{
		//InAppPurchasing.Purchase("Candy Big Pack");
	}

	public void PurchaseCandyBigPackSuccess()
	{
		GameContext.numberCandyEventNoelCollected += 920;
		GameContext.currentNumberCandyEventNoel += 920;
		CacheGame.NumberCandyEventNoelCollected = GameContext.numberCandyEventNoelCollected;
		CacheGame.CurrentNumberCandyEventNoel = GameContext.currentNumberCandyEventNoel;
		global::Notification.Current.ShowNotification(ScriptLocalization.notify_purchase_success_candy + " +920");
	}

	public void PurchaseCandyPack()
	{
		//InAppPurchasing.Purchase("Candy Pack");
	}

	public void PurchaseCandyPackSuccess()
	{
		GameContext.numberCandyEventNoelCollected += 420;
		GameContext.currentNumberCandyEventNoel += 420;
		CacheGame.NumberCandyEventNoelCollected = GameContext.numberCandyEventNoelCollected;
		CacheGame.CurrentNumberCandyEventNoel = GameContext.currentNumberCandyEventNoel;
		global::Notification.Current.ShowNotification(ScriptLocalization.notify_purchase_success_candy + " +420");
	}

	public void PurchaseCandyMiniPack()
	{
		//InAppPurchasing.Purchase("Candy Mini Pack");
	}

	public void PurchaseCandyMiniPackSuccess()
	{
		GameContext.numberCandyEventNoelCollected += 70;
		GameContext.currentNumberCandyEventNoel += 70;
		CacheGame.NumberCandyEventNoelCollected = GameContext.numberCandyEventNoelCollected;
		CacheGame.CurrentNumberCandyEventNoel = GameContext.currentNumberCandyEventNoel;
		global::Notification.Current.ShowNotification(ScriptLocalization.notify_purchase_success_candy + " +70");
	}

	private void SetImgBtnLanguage()
	{
	}

	public void Purchase(string product)
	{
		//InAppPurchasing.Purchase(product);
	}

	public void PurchasePackUltraStarshipCardOneTime()
	{
		//InAppPurchasing.Purchase("Ultra Starship Card");
	}

	public void PurchasePackUltraStarshipCardOneTimeSuccess()
	{
		CacheGame.AddCardAllPlane(500);
		this.vipPointPack = 25;
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_All_Plane, 500);
		GetRewardSuccess.current.ShowPanelGetRewardSuccess();
		ShopManager.Instance.objMiscUltraStarshipCard.SetActive(false);
		CacheGame.IsPurchaseUltraStarshipCard = true;
	}

	public void PurchasePackUltraDroneCardOneTime()
	{
		//InAppPurchasing.Purchase("Ultra Drone Card");
	}

	public void PurchasePackUltraDroneCardOneTimeSuccess()
	{
		CacheGame.AddCardAllDrone(500);
		this.vipPointPack = 25;
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_All_Drone, 500);
		GetRewardSuccess.current.ShowPanelGetRewardSuccess();
		ShopManager.Instance.objMiscUltraDroneCard.SetActive(false);
		CacheGame.IsPurchaseUltraDroneCard = true;
	}

	public void PurchasePackOneTime1()
	{
		//InAppPurchasing.Purchase("Pack One Time");
	}

	private void PurchasePackOneTime1Success()
	{
		this.vipPointPack = 5;
		CacheGame.IsPurchasePackOneTime = true;
		ShopManager.Instance.objOneTimePack.SetActive(false);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, 200);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, 100000);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Heart, 10);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Shield, 10);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Bullet_10, 10);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Special_Skill, 20);
		GetRewardSuccess.current.ShowPanelGetRewardSuccess();
		CacheGame.AddGems(200);
		CacheGame.AddCoins(100000);
		CurrencyLog.LogGemIn(200, CurrencyLog.In.Inapp, "PackOneTime");
		CurrencyLog.LogGoldIn(100000, CurrencyLog.In.Inapp, "PackOneTime");
		GameContext.totalItemPowerUpHeart += 10;
		CacheGame.NumberItemPowerHeart = GameContext.totalItemPowerUpHeart;
		GameContext.totalItemPowerShield += 10;
		CacheGame.NumberItemPowerShield = GameContext.totalItemPowerShield;
		GameContext.totalItemPowerUpBullet10 += 10;
		CacheGame.NumberItemPowerUpBullet10 = GameContext.totalItemPowerUpBullet10;
		CacheGame.AddSkillPlane(20);
	}

	public void PurchasePackOneTimeEndlessMode()
	{
		//InAppPurchasing.Purchase("Endless Mode Pack One Time");
	}

	public void PurchasePackOneTimeEndlessModeSuccess()
	{
		EndlessModeShopManager.current.PurchasePackOneTimeSuccess();
	}

	public static IAPGameManager _ins;

	public ShopGem shopGem;

	public SaleManager saleManager;

	public Text textPricePremiunPack2;

	public Text textPricePremiunPack2NoSale;

	public Text textPriceStarterPack;

	public Text textPriceStarterPackDiscount;

	public Text textGemPack1;

	public Text textGemPack2;

	public Text textGemPack3;

	public Text textGemPack4;

	public Text textGemPack5;

	public Text textPriceUltraStarshipCardOneTime;

	public Text textPriceUltraDroneCardOneTime;

	public Text textPricePremiumPack;

	public Text textPricePremiumPackNoSale;

	public Text textPriceStarterPack2;

	public Text textPriceStarterPack2NoSale;

	public Text textPricePackOneTime;

	public Text textPricePackOneTimeNoSale;

	public Text[] textPackCoins;

	[HideInInspector]
	public string currencyCode = string.Empty;

	private string currentPrice = string.Empty;

	private int gemPack0;

	private int gemPack1;

	private int gemPack2;

	private int gemPack3;

	private int gemPack4;

	private int gemPack5;

	private int vipPointPack;

	private bool isSetTextPrice;

	public enum PackCoin
	{
		Coins_10000,
		Coins_62500,
		Coins_150000,
		Coins_350000,
		Coins_1000000
	}

	[Serializable]
	public enum Pack
	{
		Super_Spaceship_Pack,
		Warlock_Pack,
		SS_Lightning_Pack,
		Spaceship_Pack,
		Elite_Pack,
		Super_Booster_Pack,
		Booster_Pack,
		Offer_Pack
	}
}
