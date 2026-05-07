using System;
using MoreMountains.Tools;
using UnityEngine;

public static class SpaceForceFirebaseLogger
{
	public static FalconFirebaseLogger.DifficultyLevel Convert(GameContext.ModeLevel difficultyLevel)
	{
		return (FalconFirebaseLogger.DifficultyLevel)difficultyLevel;
	}

	public static FalconFirebaseLogger.ExtraGameMode Convert(GameContext.ModeGamePlay difficultyMode)
	{
		return (FalconFirebaseLogger.ExtraGameMode)difficultyMode;
	}

	

	public static void ExtraGameStart(GameContext.ModeGamePlay gameMode, GameContext.ModeLevel difficultyLevel = GameContext.ModeLevel.Easy, int level = -1)
	{
		FalconFirebaseLogger.ExtraGameStart(SpaceForceFirebaseLogger.Convert(gameMode), SpaceForceFirebaseLogger.Convert(difficultyLevel), level);
	}

	public static void PlayerDied(GameContext.ModeGamePlay gameMode, GameContext.ModeLevel difficultyLevel, int level, string waveName)
	{
		FalconFirebaseLogger.ExtraGamePlayerDied(SpaceForceFirebaseLogger.Convert(gameMode), SpaceForceFirebaseLogger.Convert(difficultyLevel), level, waveName);
	}

	public static void LevelStart(GameContext.ModeLevel difficultyLevel, int level)
	{
		FalconFirebaseLogger.LevelStart(SpaceForceFirebaseLogger.Convert(difficultyLevel), level);
	}

	public static void LevelFail(GameContext.ModeLevel difficultyLevel, int level)
	{
		FalconFirebaseLogger.LevelFail(SpaceForceFirebaseLogger.Convert(difficultyLevel), level);
	}

	public static void LevelComplete(GameContext.ModeLevel difficultyLevel, int level)
	{
		FalconFirebaseLogger.LevelComplete(SpaceForceFirebaseLogger.Convert(difficultyLevel), level);
	}

	public static void LevelCompleteExtra(GameContext.ModeGamePlay modeGamePlay, GameContext.ModeLevel difficultyLevel, int level)
	{
		
	}

	public static void PlayEndlessMode()
	{
		
	}

	public static void LogTimePlayEndlessMode(int totalSecond, int wave)
	{
		
	}

	public static void LogTimeVictoryModeCampaign(GameContext.ModeLevel modeLevel, int totalSecond)
	{
		
	}

	public static void LogTimeDefeatModeCampaign(GameContext.ModeLevel modeLevel, int totalSecond, int wave)
	{
		
	}

	public static void PlayerDied(GameContext.ModeLevel difficultyLevel, int level, string waveName)
	{
		FalconFirebaseLogger.PlayerDied(SpaceForceFirebaseLogger.Convert(difficultyLevel), level, waveName);
	}

	public static void LogLevelFailEndlessMode(int wave)
	{
		
	}

	public static void LogFailBossMode(int boss)
	{
		
	}

	public static void LogTestPlane(GameContext.Plane planeID)
	{
		
	}

	public static void InappPurchase(string productName, string value)
	{
		
	}

	public static void LogGoldIn(int value, string where, string why)
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

	private static int Load(string key)
	{
		return PlayerPrefs.GetInt("FirebaseCache_" + key, 0);
	}

	private static int InAppCount
	{
		get
		{
			return SpaceForceFirebaseLogger.Load("InAppCount");
		}
		set
		{
			SpaceForceFirebaseLogger.Save("InAppCount", value);
		}
	}

	private static int MaxLevelFailCount
	{
		get
		{
			return SpaceForceFirebaseLogger.Load("MaxLevelFailCount");
		}
		set
		{
			SpaceForceFirebaseLogger.Save("MaxLevelFailCount", value);
		}
	}

	public static int MaxLevel
	{
		get
		{
			return SpaceForceFirebaseLogger.Load("MaxLevel");
		}
		set
		{
		
			SpaceForceFirebaseLogger.Save("MaxLevel", value);
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
}
