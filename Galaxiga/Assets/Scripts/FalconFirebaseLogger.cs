using System;
using MoreMountains.Tools;
using UnityEngine;

public static class FalconFirebaseLogger
{
	

	public static void ExtraGameStart(FalconFirebaseLogger.ExtraGameMode gameMode, FalconFirebaseLogger.DifficultyLevel difficultyLevel = FalconFirebaseLogger.DifficultyLevel.Normal, int level = -1)
	{
		
	}

	public static void ExtraGameFail(FalconFirebaseLogger.ExtraGameMode gameMode, FalconFirebaseLogger.DifficultyLevel difficultyLevel = FalconFirebaseLogger.DifficultyLevel.Normal, int level = -1)
	{
		
	}

	public static void ExtraGameComplete(FalconFirebaseLogger.ExtraGameMode gameMode, FalconFirebaseLogger.DifficultyLevel difficultyLevel = FalconFirebaseLogger.DifficultyLevel.Normal, int level = -1)
	{
		
	}

	public static void ExtraGamePlayerDied(FalconFirebaseLogger.ExtraGameMode gameMode, FalconFirebaseLogger.DifficultyLevel difficultyLevel, int level, string waveName)
	{
		
	}

	public static void LevelStart(FalconFirebaseLogger.DifficultyLevel difficultyLevel, int level)
	{
		
	}

	public static void LevelFail(FalconFirebaseLogger.DifficultyLevel difficultyLevel, int level)
	{
		
	}

	public static void LevelComplete(FalconFirebaseLogger.DifficultyLevel difficultyLevel, int level)
	{
		
	}

	public static void PlayerDied(FalconFirebaseLogger.DifficultyLevel difficultyLevel, int level, string waveName)
	{
		
	}

	public static void InappPurchase(string productName, int value)
	{
		
	}

	public static void LogGoldIn(int value, string why, string where)
	{
		
	}

	public static void LogGoldOut(int value, string why, string where)
	{
		
	}

	public static void LogGemIn(int value, string why, string where)
	{
		
	}

	public static void LogGemOut(int value, string why, string where)
	{
		
	}

	public static void LogCardIn(int value, string why, string where, string typeCard)
	{
		
	}

	public static void LogCardOut(int value, string why, string where, string typeCard)
	{
		
	}

	private static void Save(string key, int value)
	{
		PlayerPrefs.SetInt("FirebaseCache_" + key, value);
	}

	private static void Save(string key, string value)
	{
		PlayerPrefs.SetString("FirebaseCache_" + key, value);
	}

	private static int Load(string key)
	{
		return PlayerPrefs.GetInt("FirebaseCache_" + key, 0);
	}

	private static string LoadString(string key)
	{
		return PlayerPrefs.GetString("FirebaseCache_" + key, "EMPTY");
	}

	public static int InAppCount
	{
		get
		{
			return FalconFirebaseLogger.Load("InAppCount");
		}
		set
		{
			FalconFirebaseLogger.Save("InAppCount", value);
		}
	}

	private static int MaxLevelFailCount
	{
		get
		{
			return FalconFirebaseLogger.Load("MaxLevelFailCount");
		}
		set
		{
			FalconFirebaseLogger.Save("MaxLevelFailCount", value);
		}
	}

	public static int MaxLevel
	{
		get
		{
			return FalconFirebaseLogger.Load("MaxLevel");
		}
		set
		{
			
			FalconFirebaseLogger.Save("MaxLevel", value);
		}
	}

	public static string UserID
	{
		get
		{
			return FalconFirebaseLogger.LoadString("UserID");
		}
		set
		{
			
			FalconFirebaseLogger.Save("UserID", value);
		}
	}

	public const string Mission_why = "Mission";

	public const string FirstTimeMission_why = "FirstTimeMission";

	public const string LuckyWheel_why = "LuckyWheel";

	public const string MysticChest_why = "MysticChest";

	public const string SocialReward_why = "SocialReward";

	public const string PvP_why = "PvP";

	public const string Endless_why = "Endless";

	public const string Achievement_why = "Achievement";

	public const string DailyQuest_why = "DailyQuest";

	public const string DailyLogin_why = "DailyLogin";

	public const string IAP_why = "IAP";

	public const string GiftCode_why = "GiftCode";

	public const string X2VideoMission_why = "X2VideoMission";

	public const string X4VideoMission_why = "X4VideoMission";

	public const string VideoTop_why = "VideoTop";

	public const string ServerPush_why = "ServerPush";

	public const string UpgradeAircraft_why = "UpgradeAircraft";

	public const string UpgradeDrone_why = "UpgradeDrone";

	public const string PurchaseAircraft_why = "PurchaseAircraft";

	public const string PurchaseDrone_why = "PurchaseDrone";

	public const string EvolveAircraft_why = "EvolveAircraft";

	public const string EvolveDrone_why = "EvolveDrone";

	public enum ExtraGameMode
	{
		EndLess,
		PvP,
		CoOp,
		TryPlane,
		Tutorial,
		Boss,
		Other
	}

	public enum DifficultyLevel
	{
		Normal,
		Hard,
		Hardest,
		Other
	}
}
