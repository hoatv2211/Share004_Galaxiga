using System;
using System.Collections;
using Hellmade.Sound;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public static class GameContext
{
	public static int MAX_ENERGY
	{
		get
		{
			if (GameContext.currentVIP == 3)
			{
				return 110;
			}
			if (GameContext.currentVIP == 4)
			{
				return 120;
			}
			if (GameContext.currentVIP == 5)
			{
				return 130;
			}
			if (GameContext.currentVIP == 6)
			{
				return 140;
			}
			if (GameContext.currentVIP == 7)
			{
				return 150;
			}
			if (GameContext.currentVIP == 8)
			{
				return 160;
			}
			if (GameContext.currentVIP == 9)
			{
				return 180;
			}
			if (GameContext.currentVIP == 10)
			{
				return 230;
			}
			if (GameContext.currentVIP == 12)
			{
				return 300;
			}
			return 100;
		}
	}

	public static int MAX_NUMBER_SKILL_IN_GAME
	{
		get
		{
			if (GameContext.isPurchasePremiumPack)
			{
				if (GameContext.currentVIP >= 10)
				{
					return 7;
				}
				return 5;
			}
			else
			{
				if (GameContext.currentVIP >= 10)
				{
					return 5;
				}
				return 3;
			}
		}
	}

	public static Color ColorConvert(float r, float g, float b)
	{
		return new Color(r / 255f, g / 255f, b / 255f);
	}

	public static void SetColor(Image img, string rank)
	{
		if (rank == GameContext.Rank.C.ToString())
		{
			img.color = GameContext.COLOR_RANK_C;
		}
		else if (rank == GameContext.Rank.B.ToString())
		{
			img.color = GameContext.COLOR_RANK_B;
		}
		else if (rank == GameContext.Rank.A.ToString())
		{
			img.color = GameContext.COLOR_RANK_A;
		}
		else if (rank == GameContext.Rank.S.ToString())
		{
			img.color = GameContext.COLOR_RANK_S;
		}
		else if (rank == GameContext.Rank.SS.ToString())
		{
			img.color = GameContext.COLOR_RANK_SS;
		}
		else if (rank == GameContext.Rank.SSS.ToString())
		{
			img.color = GameContext.COLOR_RANK_SSS;
		}
	}

	public static GameContext.Rank GetRankEnum(string rank)
	{
		return (GameContext.Rank)Enum.Parse(typeof(GameContext.Rank), rank, true);
	}

	public static void Reset()
	{
		GameContext.skillIsActivating = false;
		GameContext.isGameReady = false;
		GameContext.power = 1;
		GameContext.savePower = 1;
		GameContext.isPause = false;
		GameContext.isBonusLife = false;
		GameContext.isBonusPower = false;
		GameContext.isBonusShield = false;
		GameContext.currentLive = 0;
	}

	public static IEnumerator Delay(float _time, Action actionT)
	{
		yield return new WaitForSecondsRealtime(_time);
		actionT();
		yield break;
	}

	public static void ResetLevel()
	{
		GameContext.currentScore = 0;
		GameContext.numberStarCurrentLvl = 0;
		GameContext.isMissionComplete = false;
		GameContext.isMissionFail = false;
		GameContext.totalCoinEarnedInCurrentLvl = 0;
		GameContext.skillIsActivating = false;
		GameContext.percentCoinLevel = 0f;
		GameContext.isFirstGame = false;
	}

	public static TimeSpan GetTimeSpan(DateTime currentTime, DateTime oldTime)
	{
		return currentTime.Subtract(oldTime);
	}

	public static TimeSpan GetTimeSpan(DateTime currentTime, string timeSaved)
	{
		long dateData = Convert.ToInt64(timeSaved);
		DateTime value = DateTime.FromBinary(dateData);
		return currentTime.Subtract(value);
	}

	public static void SetLanguage(GameContext.Language language)
	{
		if (GameContext.CurrentIdLanguage != (int)language)
		{
			string name = Enum.GetName(typeof(GameContext.LanguageCode), (int)language);
			GameContext.SetLang(name);
			UnityEngine.Debug.Log("Set Language " + language.ToString());
		}
	}

	public static void SetLang(string languageCode)
	{
		if (LocalizationManager.GetAllLanguagesCode(true, true).Contains(languageCode))
		{
			LocalizationManager.CurrentLanguageCode = languageCode;
			int num = Enum.GetNames(typeof(GameContext.LanguageCode)).Length;
			for (int i = 0; i < num; i++)
			{
				if (Enum.GetName(typeof(GameContext.LanguageCode), i) == languageCode)
				{
					GameContext.CurrentIdLanguage = i;
					break;
				}
			}
			UnityEngine.Debug.Log("index language " + GameContext.CurrentIdLanguage);
		}
		else
		{
			GameContext.CurrentIdLanguage = 0;
			LocalizationManager.CurrentLanguageCode = GameContext.LanguageCode.en.ToString();
		}
	}

	public static int getMaxLevelPassedByMode(GameContext.ModeLevel mode)
	{
		if (mode == GameContext.ModeLevel.Easy)
		{
			return GameContext.maxLevelUnlocked - 1;
		}
		if (mode == GameContext.ModeLevel.Normal)
		{
			return GameContext.maxLevelNormalPass;
		}
		return GameContext.maxLevelHardPass;
	}

	public static float currentVersionGame = 0f;

	public static int power = 1;

	public static int savePower = 1;

	public static float sensitivity = 1f;

	public static int typeMove = 0;

	public static bool isPause = false;

	public static bool isMissionFail = false;

	public static bool isMissionComplete = false;

	public static bool isGameReady = false;

	public static bool isBonusLife = false;

	public static bool isBonusPower = false;

	public static bool isBonusShield = false;

	public static bool isPassCurrentLevel = false;

	public static bool isPlayerDie = false;

	public static bool skillIsActivating = false;

	public static bool isTryPlane = false;

	public static bool isTutorial = false;

	public static bool isFirstGame = false;

	public static bool isCompleteTutorialUpgradePlane = false;

	public static bool isCompleteTutorialSceneHome = false;

	public static bool isPurchasePremiumPack = false;

	public static bool isPurchaseStarterPack = false;

	public static bool isCollectGemPremiumPack = false;

	public static bool isRemoveAds = false;

	public static bool isShowPack = false;

	public static bool firstTimeInSceneHome = true;

	public static bool availableWatchVideoToOpenBox = false;

	public static bool isNewDay = false;

	public static bool isDisableStarterPack = false;

	public static bool firstInstallGame = false;

	public static bool isOpenShopItemCoin = true;

	public static bool freeGift_FirstGame = true;

	public static bool canCollectDailyGift = false;

	public static bool canCollectDailyGiftVIP = false;

	public static bool needShowPanelSelectPlane = false;

	public static bool needShowPanelPrize = false;

	public static bool needShowForcusBtnEndless = false;

	public static bool needShowForcusBtnBoss = false;

	public static bool needShowPanelSale = false;

	public static bool newGame = false;

	public static GameContext.Plane planeTry;

	public static string sceneNameTryPlane = "Home";

	public static int totalUltraStarshipCard = 0;

	public static int totalUltraDroneCard = 0;

	public static int currentLevel = 0;

	public static int currentNumberSkillPlane = 3;

	public static int currentLive = 0;

	public static int totalEnergy = 0;

	public static int currentEnergyLevel = 0;

	public static int currentEnergyBackupLevel = 0;

	public static int currentPlaneIDEquiped = 0;

	public static int currentIDPackGemX3 = 0;

	public static int maxLevelUnlocked = 1;

	public static int maxLevelNormalPass = 1;

	public static int maxLevelHardPass = 1;

	public static int maxLevelBosssUnlocked = 1;

	public static int numberStarCurrentLvl = 0;

	public static int currentScore = 0;

	public static int totalCoinEarnedInCurrentLvl = 0;

	public static int numberFreeRespawnPlayer = 0;

	public static int currentSecondsTimeLeftWatchVideoToOpenBox = 0;

	public static int galaxiga_ab_ads = 0;

	public static int totalSkillPlane = 0;

	public static int totalItemPowerUpBullet3 = 0;

	public static int totalItemPowerShield = 0;

	public static int totalItemPowerUpBullet5 = 0;

	public static int totalItemPowerUpBullet10 = 0;

	public static int totalItemPowerUpHeart = 0;

	public static int totalKeySuperPrize = 0;

	public static int currentVIPPoint = 0;

	public static int currentVIP = 0;

	public static float sizeBoudaryCam = 0f;

	public static float orthorgrhicSize = 0f;

	public static float percentCoinLevel = 0f;

	public static float volumeMusic = 0f;

	public static float volumeSound = 0f;

	public static bool isVibration = false;

	public static int currentWaveEndlessMode = 0;

	public static int totalSecondEndGame = 0;

	public static int numberCandyEventNoelCollected = 0;

	public static int currentNumberCandyEventNoel = 0;

	public static int numberPlayEventNoel = 0;

	public static int numberWavePassInstage = 0;

	public static int numberCandyEarnedInStage = 0;

	public static bool needShowPanelEventNoel = false;

	public static bool needUpdateDataPlayerToSever = true;

	public static bool isHomeOK = false;

	public static int currentUniverse = 0;

	public static int currentBlueStarCollectInGame = 0;

	public const int MAX_POWER = 10;

	public const int MAX_BACKUP = 3;

	public const int MAX_REVIVES = 3;

	public const int MAX_LEVEL = 70;

	public const int MAX_LEVEL_BOSS = 14;

	public const int TIME_COUNT_DOWN = 5;

	public const float COOLDOWN_TIME_SKILL_PLANE = 1f;

	public const float DURATION_SHIELD_POWER_UP = 10f;

	public const float DURATION_SHIELD_START = 5f;

	public static GameContext.ModeLevel currentModeLevel;

	public static GameContext.ModeGamePlay currentModeGamePlay = GameContext.ModeGamePlay.Campaign;

	public static DateTime lastTimeInSceneHome = DateTime.Now;

	public static Color COLOR_RANK_C = new Color(0.7f, 0.7f, 0.7f, 1f);

	public static Color COLOR_RANK_B = new Color(0f, 0.737f, 0.447f, 1f);

	public static Color COLOR_RANK_A = new Color(0f, 0.835f, 1f, 1f);

	public static Color COLOR_RANK_S = new Color(1f, 0.329f, 0.937f, 1f);

	public static Color COLOR_RANK_SS = new Color(1f, 0.533f, 0.114f, 1f);

	public static Color COLOR_RANK_SSS = new Color(1f, 1f, 0.11f, 1f);

	public static bool deserialize = false;

	public static Audio audioMainMenu;

	public static GameContext.DronePosition currentDroneActiveSkill;

	public static GameContext.ActionVideoAds actionVideo;

	public static RewardLevel rewardCurrentLevelPlay;

	public static int maxCoinEarnedIn1Stage = 0;

	public static string lastTimeQuitGame;

	public static int CurrentIdLanguage = 0;

	[Serializable]
	public enum Plane
	{
		BataFD01,
		FuryOfAres,
		SkyWraith,
		TwilightX,
		Greataxe,
		SSLightning,
		Warlock
	}

	[Serializable]
	public enum Rank
	{
		C,
		B,
		A,
		S,
		SS,
		SSS
	}

	[Serializable]
	public enum ModeLevel
	{
		Easy,
		Normal,
		Hard
	}

	[Serializable]
	public enum ModeGamePlay
	{
		Endless,
		Campaign,
		Boss
	}

	[Serializable]
	public enum Drone
	{
		NoDrone,
		GatlingGun,
		AutoGatlingGun,
		Laser,
		Nighturge,
		GodOfThunder,
		Terigon
	}

	[Serializable]
	public enum DronePosition
	{
		Left,
		Right
	}

	[Serializable]
	public enum RewardType
	{
		CardPlane,
		CardDrone,
		Coin,
		Gem,
		UnlockPlane,
		Box,
		Energy,
		CardAllPlane,
		CardAllDrone,
		SkillPlane
	}

	public enum ActionVideoAds
	{
		Box,
		Daily_Quest,
		Revive_Player,
		X2_Coin,
		Get_Rich,
		Random_Reward,
		Get_Energy,
		Free_Gift,
		Defeat_X2_Coin,
		Get_Coin,
		X2_Blue_Star
	}

	public enum Booster
	{
		power5,
		power10,
		Shield,
		Heart,
		Overdrive
	}

	public enum Language
	{
		English,
		Chinese,
		Russian,
		Japanese,
		Korean,
		French,
		German,
		Spanish,
		Portuguese,
		Italian,
		Vietnamese,
		Thai,
		Indonesia,
		Turkish
	}

	public enum LanguageCode
	{
		en,
		zh,
		ru,
		ja,
		ko,
		fr,
		de,
		es,
		pt,
		it,
		vi,
		th,
		tr,
		id
	}
}
