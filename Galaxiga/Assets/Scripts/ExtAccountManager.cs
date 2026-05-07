using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtAccountManager
{
	public static ExtAccountManager Instance
	{
		get
		{
			if (ExtAccountManager._instance == null)
			{
				ExtAccountManager._instance = new ExtAccountManager();
			}
			return ExtAccountManager._instance;
		}
	}

	public ExtClientData GetExtClientData()
	{
		ExtClientData extClientData = new ExtClientData();
		extClientData.coin = CacheGame.GetCoins();
		extClientData.gem = CacheGame.GetGems();
		extClientData.energy = CacheGame.GetTotalEnergy();
		extClientData.ultraCardPlane = CacheGame.GetTotalCardAllPlane();
		extClientData.ultraCardDrone = CacheGame.GetTotalCardAllDrone();
		extClientData.dataPlane = SaveDataPlane.Instance.planeInfoContainer.arrPlanes;
		extClientData.dataDrone = SaveDataDrone.Instance.droneInfoContainer.arrDrones;
		SaveDataLevel.LoadData();
		extClientData.dataLevelCampaign = SaveDataLevel.dataLevelsToSaved;
		SaveDataLevelBossMode.LoadData();
		extClientData.dataLevelBossModes = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes;
		SaveDataQuest.LoadData();
		extClientData.dataDailyQuests = SaveDataQuest.dataQuestContainer.dataQuests;
		SaveDataStateItemShopCoin.LoadData();
		extClientData.stateItemShopCoin = SaveDataStateItemShopCoin.stateItemShopCoin.canBuyItem;
		SaveDataStateClaimRewardVIP.LoadData();
		extClientData.stateClaimRewardVIPOneTime = SaveDataStateClaimRewardVIP.stateClaimRewardVIP.arrBool;
		extClientData.booster = SaveDataItemBooster.Instance.itemBooster;
		extClientData.otherDataClient = new OtherDataClient();
		extClientData.otherDataClient.canCollectDailyGift = CacheGame.CanCollectDailyGift;
		extClientData.otherDataClient.canCollectDailyGiftVIP = CacheGame.CanCollectDailyGiftVIP;
		extClientData.otherDataClient.currentLeftDroneEquiped = CacheGame.GetDroneLeftEquiped();
		extClientData.otherDataClient.currentRightDroneEquiped = CacheGame.GetDroneRightEquiped();
		extClientData.otherDataClient.currentMaxLevelBossMode = CacheGame.GetMaxLevelBoss();
		extClientData.otherDataClient.currentMaxLevelCampaign = CacheGame.GetMaxLevel();
		extClientData.otherDataClient.currentMaxLevelCampaignNormal = CacheGame.GetMaxLevelNormalPass();
		extClientData.otherDataClient.currentMaxLevelCampaignHard = CacheGame.GetMaxLevelHardPass();
		extClientData.otherDataClient.currentPlaneEquiped = CacheGame.GetPlaneEquiped();
		if (CacheGame.EndTimePremiumPack != string.Empty && CacheGame.EndTimePremiumPack.Length > 0)
		{
			long endTimePremiumPack = GameUtil.DateTimeToUnixTimestamp(CacheGame.EndTimePremiumPack);
			extClientData.otherDataClient.endTimePremiumPack = endTimePremiumPack;
		}
		if (CacheGame.EndTimeStarterPack != string.Empty && CacheGame.EndTimeStarterPack.Length > 0)
		{
			long endTimeStarterPack = GameUtil.DateTimeToUnixTimestamp(CacheGame.EndTimeStarterPack);
			extClientData.otherDataClient.endTimeStarterPack = endTimeStarterPack;
		}
		extClientData.otherDataClient.isCompleteTutorialSceneHome = CacheGame.IsCompleteTutorialSceneHome;
		extClientData.otherDataClient.isCompleteTutorialSceneSelectLevel = CacheGame.IsCompleteTutorialSceneSelectLevel;
		extClientData.otherDataClient.isDisableStarterPack = CacheGame.IsDisableStarterPack;
		extClientData.otherDataClient.isOpenShopItemCoin = CacheGame.IsOpenShopItemCoin;
		extClientData.otherDataClient.isPurchasePackOneTime = CacheGame.IsPurchasePackOneTime;
		extClientData.otherDataClient.isPurchasePremiumPack = CacheGame.IsPurchasePremiumPack;
		extClientData.otherDataClient.isPurchaseStarterPack = CacheGame.IsPurchaseStarterPack;
		extClientData.otherDataClient.isPurchaseUltraStarshipCard = CacheGame.IsPurchaseUltraStarshipCard;
		extClientData.otherDataClient.isPurchaseUltraDroneCard = CacheGame.IsPurchaseUltraDroneCard;
		extClientData.otherDataClient.isRemoveAds = CacheGame.IsRemoveAds;
		extClientData.otherDataClient.isSetSensitivy = CacheGame.IsSetSensitivy;
		extClientData.otherDataClient.isVibration = CacheGame.IsVibration;
		if (CacheGame.LastTimeQuitGame != string.Empty && CacheGame.LastTimeQuitGame.Length > 0)
		{
			long lastTimeQuitGame = GameUtil.DateTimeToUnixTimestamp(CacheGame.LastTimeQuitGame);
			extClientData.otherDataClient.lastTimeQuitGame = lastTimeQuitGame;
		}
		if (CacheGame.NextTimeSale != string.Empty && CacheGame.NextTimeSale.Length > 0)
		{
			long nextTimeSale = GameUtil.DateTimeToUnixTimestamp(CacheGame.NextTimeSale);
			extClientData.otherDataClient.nextTimeSale = nextTimeSale;
		}
		if (CacheGame.NextTimeX3Pack != string.Empty && CacheGame.NextTimeX3Pack.Length > 0)
		{
			long nextTimeX3Pack = GameUtil.DateTimeToUnixTimestamp(CacheGame.NextTimeX3Pack);
			extClientData.otherDataClient.nextTimeX3Pack = nextTimeX3Pack;
		}
		extClientData.otherDataClient.numberPlayEndlessModeInDay = CacheGame.NumberPlayEndlessModeInDay;
		extClientData.otherDataClient.numberWatchVideoToBecomeRich = CacheGame.NumberWatchVideoToBecomeRich;
		extClientData.otherDataClient.numberWatchVideoToGetFreeGiftInDay = CacheGame.NumberWatchVideoToGetFreeGiftInDay;
		extClientData.otherDataClient.numBuyEnergy1 = CacheGame.NumberBuyEnergy1;
		extClientData.otherDataClient.numDayCollectGift = CacheGame.NumberDayCollectGift;
		extClientData.otherDataClient.numGemPurchased = CacheGame.NumberGemPurchased;
		extClientData.otherDataClient.numKeySuperPrize = CacheGame.KeySuperRize;
		extClientData.otherDataClient.numResetShopCoinItem = CacheGame.NumberResetShopCoinItem;
		extClientData.otherDataClient.numWatchVideoGetEnergy = CacheGame.NumberWatchVideoGetEnergy;
		if (CacheGame.OldDay != string.Empty && CacheGame.OldDay.Length > 0)
		{
			long oldDay = GameUtil.DateTimeToUnixTimestamp(CacheGame.OldDay);
			extClientData.otherDataClient.oldDay = oldDay;
		}
		extClientData.otherDataClient.packSale = CacheGame.PackSale;
		extClientData.otherDataClient.secondRecoveryEnergy = CacheGame.SecondRecoveryEnergy;
		extClientData.otherDataClient.sensitivy = (int)(CacheGame.Sensitivy * 100f);
		extClientData.otherDataClient.timeLeftFreeBox = CacheGame.TimeLeftFreeBox;
		extClientData.otherDataClient.totalSecondCountDownFreeGift = CacheGame.TotalSecondCountDownFreeGift;
		extClientData.otherDataClient.typeMovePlayer = CacheGame.TypeMovePlayer;
		extClientData.otherDataClient.volumeMusic = (int)(CacheGame.VolumeMusic * 100f);
		extClientData.otherDataClient.volumeSound = (int)(CacheGame.VolumeSound * 100f);
		extClientData.otherDataClient.currentVIPPoint = CacheGame.VIPPoint;
		return extClientData;
	}

	public ExtServerData GetExtServerData()
	{
		return null;
	}

	public void SetAccountData(AccountData accountData)
	{
		if (GameContext.newGame)
		{
			return;
		}
		try
		{
			CacheGame.SetCoins(accountData.clientData.extClientData.coin);
			CacheGame.SetGems(accountData.clientData.extClientData.gem);
			CacheGame.SetTotalEnergy(accountData.clientData.extClientData.energy);
			CacheGame.SetNumberCardAllPlane(accountData.clientData.extClientData.ultraCardPlane);
			CacheGame.SetNumberCardAllDrone(accountData.clientData.extClientData.ultraCardDrone);
			SaveDataPlane.Instance.SetData(accountData.clientData.extClientData.dataPlane);
			SaveDataDrone.Instance.SetData(accountData.clientData.extClientData.dataDrone);
			SaveDataLevel.SetData(accountData.clientData.extClientData.dataLevelCampaign);
			SaveDataLevelBossMode.SetData(accountData.clientData.extClientData.dataLevelBossModes);
			SaveDataQuest.SetData(accountData.clientData.extClientData.dataDailyQuests);
			SaveDataStateItemShopCoin.SetData(accountData.clientData.extClientData.stateItemShopCoin);
			SaveDataStateClaimRewardVIP.SetData(accountData.clientData.extClientData.stateClaimRewardVIPOneTime);
			SaveDataItemBooster.Instance.SetData(accountData.clientData.extClientData.booster);
			CacheGame.CanCollectDailyGift = accountData.clientData.extClientData.otherDataClient.canCollectDailyGift;
			CacheGame.CanCollectDailyGiftVIP = accountData.clientData.extClientData.otherDataClient.canCollectDailyGiftVIP;
			CacheGame.SetDroneLeftEquip((GameContext.Drone)accountData.clientData.extClientData.otherDataClient.currentLeftDroneEquiped);
			CacheGame.SetDroneRightEquip((GameContext.Drone)accountData.clientData.extClientData.otherDataClient.currentRightDroneEquiped);
			CacheGame.SetMaxLevelBoss(accountData.clientData.extClientData.otherDataClient.currentMaxLevelBossMode);
			CacheGame.SetMaxLevel(accountData.clientData.extClientData.otherDataClient.currentMaxLevelCampaign);
			CacheGame.SetMaxLevelNormalPass(accountData.clientData.extClientData.otherDataClient.currentMaxLevelCampaignNormal);
			CacheGame.SetMaxLevelHardPass(accountData.clientData.extClientData.otherDataClient.currentMaxLevelCampaignHard);
			CacheGame.SetPlaneEquip((GameContext.Plane)accountData.clientData.extClientData.otherDataClient.currentPlaneEquiped);
			CacheGame.EndTimePremiumPack = GameUtil.UnixTimestampToDateTimeLocal(accountData.clientData.extClientData.otherDataClient.endTimePremiumPack).ToBinary().ToString();
			CacheGame.EndTimeStarterPack = GameUtil.UnixTimestampToDateTimeLocal(accountData.clientData.extClientData.otherDataClient.endTimeStarterPack).ToBinary().ToString();
			CacheGame.IsCompleteTutorialSceneHome = accountData.clientData.extClientData.otherDataClient.isCompleteTutorialSceneHome;
			CacheGame.IsCompleteTutorialSceneSelectLevel = accountData.clientData.extClientData.otherDataClient.isCompleteTutorialSceneSelectLevel;
			CacheGame.IsDisableStarterPack = accountData.clientData.extClientData.otherDataClient.isDisableStarterPack;
			CacheGame.IsOpenShopItemCoin = accountData.clientData.extClientData.otherDataClient.isOpenShopItemCoin;
			CacheGame.IsPurchasePackOneTime = accountData.clientData.extClientData.otherDataClient.isPurchasePackOneTime;
			CacheGame.IsPurchasePremiumPack = accountData.clientData.extClientData.otherDataClient.isPurchasePremiumPack;
			CacheGame.IsPurchaseStarterPack = accountData.clientData.extClientData.otherDataClient.isPurchaseStarterPack;
			CacheGame.IsPurchaseUltraStarshipCard = accountData.clientData.extClientData.otherDataClient.isPurchaseUltraStarshipCard;
			CacheGame.IsPurchaseUltraDroneCard = accountData.clientData.extClientData.otherDataClient.isPurchaseUltraDroneCard;
			CacheGame.IsRemoveAds = accountData.clientData.extClientData.otherDataClient.isRemoveAds;
			CacheGame.IsSetSensitivy = accountData.clientData.extClientData.otherDataClient.isSetSensitivy;
			CacheGame.IsVibration = accountData.clientData.extClientData.otherDataClient.isVibration;
			CacheGame.LastTimeQuitGame = GameUtil.UnixTimestampToDateTimeLocal(accountData.clientData.extClientData.otherDataClient.lastTimeQuitGame).ToBinary().ToString();
			CacheGame.NextTimeSale = GameUtil.UnixTimestampToDateTimeLocal(accountData.clientData.extClientData.otherDataClient.nextTimeSale).ToBinary().ToString();
			CacheGame.NextTimeX3Pack = GameUtil.UnixTimestampToDateTimeLocal(accountData.clientData.extClientData.otherDataClient.nextTimeX3Pack).ToBinary().ToString();
			CacheGame.NumberPlayEndlessModeInDay = accountData.clientData.extClientData.otherDataClient.numberPlayEndlessModeInDay;
			CacheGame.NumberWatchVideoToBecomeRich = accountData.clientData.extClientData.otherDataClient.numberWatchVideoToBecomeRich;
			CacheGame.NumberWatchVideoToGetFreeGiftInDay = accountData.clientData.extClientData.otherDataClient.numberWatchVideoToGetFreeGiftInDay;
			CacheGame.NumberBuyEnergy1 = accountData.clientData.extClientData.otherDataClient.numBuyEnergy1;
			CacheGame.NumberDayCollectGift = accountData.clientData.extClientData.otherDataClient.numDayCollectGift;
			CacheGame.NumberGemPurchased = accountData.clientData.extClientData.otherDataClient.numGemPurchased;
			CacheGame.KeySuperRize = accountData.clientData.extClientData.otherDataClient.numKeySuperPrize;
			CacheGame.NumberResetShopCoinItem = accountData.clientData.extClientData.otherDataClient.numResetShopCoinItem;
			CacheGame.NumberWatchVideoGetEnergy = accountData.clientData.extClientData.otherDataClient.numWatchVideoGetEnergy;
			CacheGame.OldDay = GameUtil.UnixTimestampToDateTimeLocal(accountData.clientData.extClientData.otherDataClient.oldDay).ToBinary().ToString();
			CacheGame.PackSale = accountData.clientData.extClientData.otherDataClient.packSale;
			CacheGame.SecondRecoveryEnergy = accountData.clientData.extClientData.otherDataClient.secondRecoveryEnergy;
			CacheGame.Sensitivy = (float)(accountData.clientData.extClientData.otherDataClient.sensitivy / 100);
			CacheGame.TimeLeftFreeBox = accountData.clientData.extClientData.otherDataClient.timeLeftFreeBox;
			CacheGame.TotalSecondCountDownFreeGift = accountData.clientData.extClientData.otherDataClient.totalSecondCountDownFreeGift;
			CacheGame.TypeMovePlayer = accountData.clientData.extClientData.otherDataClient.typeMovePlayer;
			CacheGame.VolumeMusic = (float)(accountData.clientData.extClientData.otherDataClient.volumeMusic / 100);
			CacheGame.VolumeSound = (float)(accountData.clientData.extClientData.otherDataClient.volumeSound / 100);
			CacheGame.VIPPoint = accountData.clientData.extClientData.otherDataClient.currentVIPPoint;
			IntroScene.GetDataFromeCache();
			GameContext.isFirstGame = false;
			GameContext.Reset();
			SceneManager.LoadScene("LoadingFirstGame");
			NewTutorial.isOldUser = true;
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex.Message);
		}
	}

	public void DeleteAccountData()
	{
		GameContext.newGame = true;
		PlayerPrefs.DeleteAll();
		LoadingScenes.Current.LoadLevel("Intro");
	}

	private static ExtAccountManager _instance;
}
