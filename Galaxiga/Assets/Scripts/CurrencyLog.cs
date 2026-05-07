using System;

public class CurrencyLog
{
	public static void LogGoldIn(int value, CurrencyLog.In where, string why)
	{
		SpaceForceFirebaseLogger.LogGoldIn(value, where.ToString(), why);
		LogManager.LogResource("source", where.ToString(), why, "gold", value);
	}

	public static void LogGoldOut(int value, CurrencyLog.Out where, string why)
	{
		SpaceForceFirebaseLogger.LogGoldOut(value, where.ToString(), why);
		LogManager.LogResource("sink", where.ToString(), why, "gold", value);
	}

	public static void LogGemIn(int value, CurrencyLog.In where, string why)
	{
		SpaceForceFirebaseLogger.LogGemIn(value, where.ToString(), why);
		LogManager.LogResource("source", where.ToString(), why, "gem", value);
	}

	public static void LogGemOut(int value, CurrencyLog.Out where, string why)
	{
		SpaceForceFirebaseLogger.LogGemOut(value, where.ToString(), why);
		LogManager.LogResource("sink", where.ToString(), why, "gem", value);
	}

	public static void LogInapp(string productId, string transactionId, string purchaseToken, string currencyCode, string price)
	{
		LogManager.LogInApp(productId, transactionId, purchaseToken, currencyCode, price, "Level_" + GameContext.maxLevelUnlocked);
	}

	[Serializable]
	public enum In
	{
		Inapp,
		GemToCoin,
		Victory,
		Victory_Campaign,
		Victory_Boss,
		Defeat,
		Defeat_Campaign,
		Defeat_Boss,
		Defeat_Endless,
		Vip,
		Prize,
		DailyQuest,
		DailyGift,
		WatchVideo,
		Other,
		Init
	}

	[Serializable]
	public enum Out
	{
		UpgradePlane,
		UpgradeDrone,
		EvolvePlane,
		EvolveDrone,
		BuyItemBooster,
		BuySkillPlane,
		BuyPlane,
		BuyEnergy,
		Revive,
		ShopItemCoin,
		ResetShopItemCoin,
		Prize,
		Shop
	}
}
