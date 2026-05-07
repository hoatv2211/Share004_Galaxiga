using System;
using OSNet;

public class LogManager
{
	public static void LogLevel(int level, int maxPassedLevel, LevelDifficulty difficulty, int duration, PassLevelStatus status, string where = "")
	{
		new CSLevelLog(level, maxPassedLevel, difficulty.ToString().ToLower(), duration, status.ToString().ToLower(), where).Send(false);
	}

	public static void LogInApp(string productId, string transactionId, string purchaseToken, string currencyCode, string price, string where = "")
	{
		new CSInAppLog(productId, transactionId, purchaseToken, currencyCode, price, where).Send(false);
	}

	public static void LogResource(string flowType, string itemType, string itemId, string currency, int amount)
	{
		new CSResourceLog(flowType, itemType, itemId, currency, amount).Send(false);
	}
}
