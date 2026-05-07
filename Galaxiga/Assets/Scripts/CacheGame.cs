using System;
using DG.Tweening;
using UnityEngine;

public class CacheGame
{
	public static float Version
	{
		get
		{
			return PlayerPrefs.GetFloat("VersionCodeGame");
		}
		set
		{
			PlayerPrefs.SetFloat("VersionCodeGame", value);
		}
	}

	public static void SetAntiCheat()
	{
		int num = Enum.GetNames(typeof(GameContext.Plane)).Length;
		for (int i = 0; i < num; i++)
		{
			string str = Enum.GetName(typeof(GameContext.Plane), i).ToString();
			AntiCheat.SetInt("Owned" + str, PlayerPrefs.GetInt("Owned" + str), -95);
			AntiCheat.SetInt("Unlock" + str, PlayerPrefs.GetInt("Unlock" + str), -95);
			AntiCheat.SetInt("CurrenLevelPlane" + str, PlayerPrefs.GetInt("CurrenLevelPlane" + str, 1), -95);
		}
		UnityEngine.Debug.Log("DroneLeftEquiped " + PlayerPrefs.GetInt("CurrentPlaneEquiped"));
		AntiCheat.SetInt("CurrentPlaneEquiped", PlayerPrefs.GetInt("CurrentPlaneEquiped"), -95);
		AntiCheat.SetInt("DroneLeftEquiped", PlayerPrefs.GetInt("DroneLeftEquiped"), -95);
		AntiCheat.SetInt("DroneRightEquiped", PlayerPrefs.GetInt("DroneRightEquiped"), -95);
		int num2 = Enum.GetNames(typeof(GameContext.Drone)).Length;
		for (int j = 0; j < num2; j++)
		{
			string str2 = Enum.GetName(typeof(GameContext.Drone), j).ToString();
			AntiCheat.SetInt("OwenedDrone" + str2, PlayerPrefs.GetInt("OwenedDrone" + str2), -95);
			AntiCheat.SetInt("CurrenLevelDrone" + str2, PlayerPrefs.GetInt("CurrenLevelDrone" + str2, 1), -95);
		}
		AntiCheat.SetInt("TotalSkillPlane", PlayerPrefs.GetInt("TotalSkillPlane", 3), -95);
		AntiCheat.SetInt("TotalItemPowerUpBullet5", PlayerPrefs.GetInt("TotalItemPowerUpBullet5", 1), -95);
		AntiCheat.SetInt("TotalItemPowerUpBullet10", PlayerPrefs.GetInt("TotalItemPowerUpBullet10", 1), -95);
		AntiCheat.SetInt("TotalItemPowerUpHeart", PlayerPrefs.GetInt("TotalItemPowerUpHeart", 1), -95);
		AntiCheat.SetInt("TotalItemPowerShield", PlayerPrefs.GetInt("TotalItemPowerShield", 1), -95);
		AntiCheat.SetInt("TotalKeySuperPrize", PlayerPrefs.GetInt("TotalKeySuperPrize", 0), -95);
		AntiCheat.SetInt("HighestWaveEndlessMode", PlayerPrefs.GetInt("HighestWaveEndlessMode", 0), -95);
		AntiCheat.SetInt("HighestScoreEndlessMode", PlayerPrefs.GetInt("HighestScoreEndlessMode", 0), -95);
		AntiCheat.SetInt("MaxLevel", PlayerPrefs.GetInt("MaxLevel"), -95);
		AntiCheat.SetInt("MaxLevelNormal", PlayerPrefs.GetInt("MaxLevelNormal"), -95);
		AntiCheat.SetInt("MaxLevelHard", PlayerPrefs.GetInt("MaxLevelHard"), -95);
		AntiCheat.SetInt("MaxLevelBoss", PlayerPrefs.GetInt("MaxLevelBoss"), -95);
	}

	public static int VIPPoint
	{
		get
		{
			return AntiCheat.GetInt("VipPoint", 0, -95);
		}
		set
		{
			GameContext.currentVIPPoint = value;
			AntiCheat.SetInt("VipPoint", value, -95);
		}
	}

	public static int GetTotalCardAllPlane()
	{
		GameContext.totalUltraStarshipCard = AntiCheat.GetInt("TotalCardToCraftPlane", 0, -95);
		return GameContext.totalUltraStarshipCard;
	}

	public static void SetNumberCardAllPlane(int numberCard)
	{
		AntiCheat.SetInt("TotalCardToCraftPlane", numberCard, -95);
		GameContext.totalUltraStarshipCard = numberCard;
		EventDispatcher.Instance.PostEvent(EventID.UltraStarshipCard, new Component(), null);
	}

	public static void AddCardAllPlane(int numberCard)
	{
		int numberCardAllPlane = GameContext.totalUltraStarshipCard + numberCard;
		CacheGame.SetNumberCardAllPlane(numberCardAllPlane);
	}

	public static void MinusCardAllPlane(int numberItem)
	{
		int numberCardAllPlane = GameContext.totalUltraStarshipCard - numberItem;
		CacheGame.SetNumberCardAllPlane(numberCardAllPlane);
	}

	public static void SetMaxLevel(int level)
	{
		if (level > 70)
		{
			return;
		}
		AntiCheat.SetInt("MaxLevel", level, -95);
		GameContext.maxLevelUnlocked = level;
		UnityEngine.Debug.Log("Current Max Level " + level);
	}

	public static void SetMaxLevelNormalPass(int level)
	{
		AntiCheat.SetInt("MaxLevelNormal", level, -95);
		GameContext.maxLevelNormalPass = level;
		UnityEngine.Debug.Log("Current Max Level Hard" + level);
	}

	public static int GetMaxLevelNormalPass()
	{
		return AntiCheat.GetInt("MaxLevelNormal", 1, -95);
	}

	public static void SetMaxLevelHardPass(int level)
	{
		AntiCheat.SetInt("MaxLevelHard", level, -95);
		GameContext.maxLevelHardPass = level;
		UnityEngine.Debug.Log("Current Max Level Hard " + level);
	}

	public static int GetMaxLevelHardPass()
	{
		return AntiCheat.GetInt("MaxLevelHard", 1, -95);
	}

	public static int GetMaxLevel()
	{
		GameContext.maxLevelUnlocked = AntiCheat.GetInt("MaxLevel", 1, -95);
		return GameContext.maxLevelUnlocked;
	}

	public static int GetMaxLevelBoss()
	{
		GameContext.maxLevelBosssUnlocked = AntiCheat.GetInt("MaxLevelBoss", 1, -95);
		return GameContext.maxLevelBosssUnlocked;
	}

	public static void SetMaxLevelBoss(int level)
	{
		AntiCheat.SetInt("MaxLevelBoss", level, -95);
		GameContext.maxLevelBosssUnlocked = level;
		UnityEngine.Debug.Log("Current Max Level Boss" + level);
	}

	public static bool IsPassLevel(GameContext.ModeLevel modeLevel, int level)
	{
		bool result = false;
		int num = SaveDataLevel.NumberStar(level);
		if (modeLevel != GameContext.ModeLevel.Easy)
		{
			if (modeLevel != GameContext.ModeLevel.Normal)
			{
				if (modeLevel == GameContext.ModeLevel.Hard)
				{
					if (num == 3)
					{
						result = true;
					}
				}
			}
			else if (num >= 2)
			{
				result = true;
			}
		}
		else if (num >= 1)
		{
			result = true;
		}
		return result;
	}

	public static bool IsPassLevelBoss(GameContext.ModeLevel modeLevel, int level)
	{
		bool result = false;
		int num = SaveDataLevelBossMode.NumberStars(level);
		if (modeLevel != GameContext.ModeLevel.Easy)
		{
			if (modeLevel != GameContext.ModeLevel.Normal)
			{
				if (modeLevel == GameContext.ModeLevel.Hard)
				{
					if (num == 3)
					{
						result = true;
					}
				}
			}
			else if (num >= 2)
			{
				result = true;
			}
		}
		else if (num >= 1)
		{
			result = true;
		}
		return result;
	}

	public static void SetPassLevel(GameContext.ModeLevel modeLevel, int level, int score)
	{
		DataLevel dataLevel;
		if (level > SaveDataLevel.dataLevelContainer.dataLevels.Count)
		{
			dataLevel = new DataLevel(level, 0, 0, 0, 0);
		}
		else
		{
			dataLevel = SaveDataLevel.dataLevelContainer.dataLevels[level - 1];
		}
		if (modeLevel != GameContext.ModeLevel.Easy)
		{
			if (modeLevel != GameContext.ModeLevel.Normal)
			{
				if (modeLevel == GameContext.ModeLevel.Hard)
				{
					if (dataLevel.numberStar < 3)
					{
						dataLevel.numberStar = 3;
						SaveDataLevel.stars++;
						if (level >= GameContext.maxLevelHardPass)
						{
							CacheGame.SetMaxLevelHardPass(level);
						}
					}
					if (score > dataLevel.scoreModeHard)
					{
						int num = score - dataLevel.scoreModeHard;
						SaveDataLevel.score += num;
						dataLevel.scoreModeHard = score;
					}
				}
			}
			else
			{
				if (dataLevel.numberStar < 2)
				{
					dataLevel.numberStar = 2;
					if (level >= GameContext.maxLevelNormalPass)
					{
						CacheGame.SetMaxLevelNormalPass(level);
					}
					SaveDataLevel.stars++;
				}
				if (score > dataLevel.scoreModeNormal)
				{
					int num2 = score - dataLevel.scoreModeNormal;
					SaveDataLevel.score += num2;
					dataLevel.scoreModeNormal = score;
				}
			}
		}
		else
		{
			if (dataLevel.numberStar < 1)
			{
				SaveDataLevel.stars++;
				dataLevel.numberStar = 1;
			}
			if (score > dataLevel.scoreModeEasy)
			{
				int num3 = score - dataLevel.scoreModeEasy;
				SaveDataLevel.score += num3;
				dataLevel.scoreModeEasy = score;
			}
		}
		SaveDataLevel.SetDataLevel(dataLevel, level);
	}

	public static void SetPassLevelBoss(GameContext.ModeLevel modeLevel, int level, int score)
	{
		DataLevelBossMode dataLevelBossMode = SaveDataLevelBossMode.dataLevelBossMode.dataLevelBossModes[level - 1];
		if (modeLevel != GameContext.ModeLevel.Easy)
		{
			if (modeLevel != GameContext.ModeLevel.Normal)
			{
				if (modeLevel == GameContext.ModeLevel.Hard)
				{
					if (dataLevelBossMode.numberStars < 3)
					{
						dataLevelBossMode.numberStars = 3;
					}
					if (score > dataLevelBossMode.scoreModeHard)
					{
						dataLevelBossMode.scoreModeHard = score;
					}
					dataLevelBossMode.wasPassedBossHard = true;
				}
			}
			else
			{
				if (dataLevelBossMode.numberStars < 2)
				{
					dataLevelBossMode.numberStars = 2;
				}
				if (score > dataLevelBossMode.scoreModeNormal)
				{
					dataLevelBossMode.scoreModeNormal = score;
				}
				dataLevelBossMode.wasPassedBossNormal = true;
			}
		}
		else
		{
			if (dataLevelBossMode.numberStars < 1)
			{
				dataLevelBossMode.numberStars = 1;
			}
			if (score > dataLevelBossMode.scoreModeEasy)
			{
				dataLevelBossMode.scoreModeEasy = score;
			}
			dataLevelBossMode.wasPassedBossEasy = true;
		}
		SaveDataLevelBossMode.SetDataLevel(dataLevelBossMode, level);
	}

	public static int GetPlaneEquiped()
	{
		return AntiCheat.GetInt("CurrentPlaneEquiped", 0, -95);
	}

	public static void SetPlaneEquip(GameContext.Plane plane)
	{
		AntiCheat.SetInt("CurrentPlaneEquiped", (int)plane, -95);
	}

	public static void SetOwnedPlane(GameContext.Plane plane)
	{
		AntiCheat.SetInt("Owned" + plane.ToString(), 1, -95);
		SaveDataPlane.Instance.planeInfoContainer.arrPlanes[(int)plane].isOwned = true;
	}

	public static bool IsOwnedPlane(GameContext.Plane plane)
	{
		int @int = AntiCheat.GetInt("Owned" + plane.ToString(), 0, -95);
		return @int == 1;
	}

	public static bool IsUnlockPlane(GameContext.Plane plane)
	{
		int @int = AntiCheat.GetInt("Unlock" + plane.ToString(), 0, -95);
		return @int == 1;
	}

	public static void SetUnlockPlane(GameContext.Plane plane)
	{
		AntiCheat.SetInt("Unlock" + plane.ToString(), 1, -95);
		SaveDataPlane.Instance.planeInfoContainer.arrPlanes[(int)plane].isUnlock = true;
	}

	public static void GetPlane(GameContext.Plane plane)
	{
		CacheGame.SetUnlockPlane(plane);
		CacheGame.SetOwnedPlane(plane);
	}

	public static int GetCurrentLevelPlane(GameContext.Plane plane)
	{
		return AntiCheat.GetInt("CurrenLevelPlane" + plane.ToString(), 1, -95);
	}

	public static void SetLevelPlane(GameContext.Plane plane, int level)
	{
		AntiCheat.SetInt("CurrenLevelPlane" + plane.ToString(), level, -95);
		SaveDataPlane.Instance.planeInfoContainer.arrPlanes[(int)plane].level = level;
	}

	public static int GetDroneLeftEquiped()
	{
		return AntiCheat.GetInt("DroneLeftEquiped", 0, -95);
	}

	public static int GetDroneRightEquiped()
	{
		return AntiCheat.GetInt("DroneRightEquiped", 0, -95);
	}

	public static void SetDroneLeftEquip(GameContext.Drone droneID)
	{
		AntiCheat.SetInt("DroneLeftEquiped", (int)droneID, -95);
	}

	public static void SetDroneRightEquip(GameContext.Drone droneID)
	{
		AntiCheat.SetInt("DroneRightEquiped", (int)droneID, -95);
	}

	public static bool IsOwnedDrone(GameContext.Drone droneID)
	{
		int @int = AntiCheat.GetInt("OwenedDrone" + droneID.ToString(), 0, -95);
		return @int == 1;
	}

	public static void SetOwnedDrone(GameContext.Drone droneID)
	{
		AntiCheat.SetInt("OwenedDrone" + droneID.ToString(), 1, -95);
		SaveDataDrone.Instance.droneInfoContainer.arrDrones[droneID - GameContext.Drone.GatlingGun].isOwned = true;
	}

	public static void SetOwnedDrone(GameContext.Drone droneID, bool isOwned)
	{
		AntiCheat.SetInt("OwenedDrone" + droneID.ToString(), 1, -95);
		SaveDataDrone.Instance.droneInfoContainer.arrDrones[droneID - GameContext.Drone.GatlingGun].isOwned = isOwned;
	}

	public static int GetTotalCardAllDrone()
	{
		GameContext.totalUltraDroneCard = AntiCheat.GetInt("TotalItemCraft", 0, -95);
		return GameContext.totalUltraDroneCard;
	}

	public static void SetNumberCardAllDrone(int number)
	{
		AntiCheat.SetInt("TotalItemCraft", number, -95);
		GameContext.totalUltraDroneCard = number;
		EventDispatcher.Instance.PostEvent(EventID.UltraDroneCard, new Component(), null);
	}

	public static void AddCardAllDrone(int number)
	{
		int numberCardAllDrone = GameContext.totalUltraDroneCard + number;
		CacheGame.SetNumberCardAllDrone(numberCardAllDrone);
	}

	public static void MinusCardAllDrone(int number)
	{
		int numberCardAllDrone = GameContext.totalUltraDroneCard - number;
		CacheGame.SetNumberCardAllDrone(numberCardAllDrone);
	}

	public static int GetCurrentLevelDrone(GameContext.Drone drone)
	{
		return AntiCheat.GetInt("CurrenLevelDrone" + drone.ToString(), 1, -95);
	}

	public static void SetLevelDrone(GameContext.Drone drone, int level)
	{
		AntiCheat.SetInt("CurrenLevelDrone" + drone.ToString(), level, -95);
		SaveDataDrone.Instance.droneInfoContainer.arrDrones[drone - GameContext.Drone.GatlingGun].level = level;
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"level drone ",
			drone.ToString(),
			" ",
			level
		}));
	}

	public static int GetNumberUpgradeDrone(GameContext.Drone droneID)
	{
		return AntiCheat.GetInt("NumberUpgradeDrone" + droneID.ToString(), 0, -95);
	}

	public static void SetNumberUpgradeDrone(GameContext.Drone droneID, int number)
	{
		AntiCheat.SetInt("NumberUpgradeDrone" + droneID.ToString(), number, -95);
	}

	public static int GetCoins()
	{
		ShopContext.currentCoin = AntiCheat.GetInt("TotalCoinsOwned", 0, -95);
		return ShopContext.currentCoin;
	}

	public static void InitCoin(int coins)
	{
		AntiCheat.SetInt("TotalCoinsOwned", coins, -95);
		ShopContext.currentCoin = coins;
	}

	public static void SetCoins(int coins)
	{
		AntiCheat.SetInt("TotalCoinsOwned", coins, -95);
		ShopContext.currentCoin = coins;
		EventDispatcher.Instance.PostEvent(EventID.Coin, new Component(), null);
		if (ShopManager.Instance != null)
		{
			ShopManager.Instance.UpdateTextCoinGem();
		}
	}

	public static void AddCoins(int coins)
	{
		int num = CacheGame.GetCoins();
		num += coins;
		CacheGame.SetCoins(num);
		DOTween.Restart("PUNCH_COIN", true, -1f);
		DOTween.Play("PUNCH_COIN");
		SaveDataQuest.SetProcessQuest(DailyQuestManager.Quest.coiner, coins);
	}

	public static void MinusCoins(int coins)
	{
		int num = CacheGame.GetCoins();
		num -= coins;
		if (num < 0)
		{
			num = 0;
		}
		CacheGame.SetCoins(num);
		SaveDataQuest.SetProcessQuest(DailyQuestManager.Quest.investor, coins);
	}

	public static int GetGems()
	{
		ShopContext.currentGem = AntiCheat.GetInt("TotalGemsOwned", 0, -95);
		return ShopContext.currentGem;
	}

	public static void InitGem(int gems)
	{
		AntiCheat.SetInt("TotalGemsOwned", gems, -95);
		ShopContext.currentGem = gems;
	}

	public static void SetGems(int gems)
	{
		AntiCheat.SetInt("TotalGemsOwned", gems, -95);
		ShopContext.currentGem = gems;
		EventDispatcher.Instance.PostEvent(EventID.Gem, new Component(), null);
		if (ShopManager.Instance != null)
		{
			ShopManager.Instance.UpdateTextCoinGem();
		}
	}

	public static void AddGems(int gems)
	{
		int num = CacheGame.GetGems();
		num += gems;
		CacheGame.SetGems(num);
		DOTween.Restart("PUNCH_GEM", true, -1f);
		DOTween.Play("PUNCH_GEM");
	}

	public static void MinusGems(int gems)
	{
		int num = CacheGame.GetGems();
		num -= gems;
		if (num < 0)
		{
			num = 0;
		}
		CacheGame.SetGems(num);
		SaveDataQuest.SetProcessQuest(DailyQuestManager.Quest.im_rick, gems);
	}

	public static int GetTotalEnergy()
	{
		GameContext.totalEnergy = AntiCheat.GetInt("TotalEnergy", 200, -95);
		return GameContext.totalEnergy;
	}

	public static void SetTotalEnergy(int numberEnergy)
	{
		AntiCheat.SetInt("TotalEnergy", numberEnergy, -95);
		GameContext.totalEnergy = numberEnergy;
	}

	public static void AddEnergy(int numberEnergy)
	{
		GameContext.totalEnergy += numberEnergy;
		CacheGame.SetTotalEnergy(GameContext.totalEnergy);
		DOTween.Restart("PUNCH_ENERGY", true, -1f);
		DOTween.Play("PUNCH_ENERGY");
	}

	public static void MinusEnergy(int numberEnergy)
	{
		GameContext.totalEnergy -= numberEnergy;
		CacheGame.SetTotalEnergy(GameContext.totalEnergy);
	}

	public static void SetNumberSkillPlane(int numberSkill)
	{
		AntiCheat.SetInt("TotalSkillPlane", numberSkill, -95);
		GameContext.totalSkillPlane = numberSkill;
	}

	public static int GetNumberSkillPlane()
	{
		GameContext.totalSkillPlane = AntiCheat.GetInt("TotalSkillPlane", 3, -95);
		if (GameContext.totalSkillPlane < 0)
		{
			GameContext.totalSkillPlane = 0;
		}
		return GameContext.totalSkillPlane;
	}

	public static void AddSkillPlane(int number)
	{
		GameContext.totalSkillPlane += number;
		CacheGame.SetNumberSkillPlane(GameContext.totalSkillPlane);
	}

	public static void MinusSkillPlane(int number)
	{
		GameContext.totalSkillPlane -= number;
		if (GameContext.totalSkillPlane < 0)
		{
			GameContext.totalSkillPlane = 0;
		}
		CacheGame.SetNumberSkillPlane(GameContext.totalSkillPlane);
	}

	public static int NumberItemPowerUpBullet5
	{
		get
		{
			return AntiCheat.GetInt("TotalItemPowerUpBullet5", 1, -95);
		}
		set
		{
			AntiCheat.SetInt("TotalItemPowerUpBullet5", value, -95);
		}
	}

	public static int NumberItemPowerUpBullet10
	{
		get
		{
			return AntiCheat.GetInt("TotalItemPowerUpBullet10", 1, -95);
		}
		set
		{
			AntiCheat.SetInt("TotalItemPowerUpBullet10", value, -95);
		}
	}

	public static int NumberItemPowerHeart
	{
		get
		{
			return AntiCheat.GetInt("TotalItemPowerUpHeart", 1, -95);
		}
		set
		{
			AntiCheat.SetInt("TotalItemPowerUpHeart", value, -95);
		}
	}

	public static int NumberItemPowerShield
	{
		get
		{
			return AntiCheat.GetInt("TotalItemPowerShield", 0, -95);
		}
		set
		{
			AntiCheat.SetInt("TotalItemPowerShield", value, -95);
		}
	}

	public static int NumberBuyEnergy1
	{
		get
		{
			return PlayerPrefs.GetInt("NumberBuyEnergy1", 0);
		}
		set
		{
			PlayerPrefs.SetInt("NumberBuyEnergy1", value);
		}
	}

	public static int NumberWatchVideoGetEnergy
	{
		get
		{
			return PlayerPrefs.GetInt("NumberWatchVideoGetEnergy", 0);
		}
		set
		{
			PlayerPrefs.SetInt("NumberWatchVideoGetEnergy", value);
		}
	}

	public static int NumberGemPurchased
	{
		get
		{
			return PlayerPrefs.GetInt("NumberGemPurchased", 0);
		}
		set
		{
			PlayerPrefs.SetInt("NumberGemPurchased", value);
		}
	}

	public static int KeySuperRize
	{
		get
		{
			return AntiCheat.GetInt("TotalKeySuperPrize", 0, -95);
		}
		set
		{
			AntiCheat.SetInt("TotalKeySuperPrize", value, -95);
		}
	}

	public static bool IsVibration
	{
		get
		{
			return PlayerPrefs.GetInt("IsVibration", 1) == 1;
		}
		set
		{
			if (!value)
			{
				PlayerPrefs.SetInt("IsVibration", 0);
			}
			else
			{
				PlayerPrefs.SetInt("IsVibration", 1);
			}
		}
	}

	public static float VolumeMusic
	{
		get
		{
			return PlayerPrefs.GetFloat("VolumeMusic", 1f);
		}
		set
		{
			PlayerPrefs.SetFloat("VolumeMusic", value);
		}
	}

	public static float VolumeSound
	{
		get
		{
			return PlayerPrefs.GetFloat("VolumeSound", 1f);
		}
		set
		{
			PlayerPrefs.SetFloat("VolumeSound", value);
		}
	}

	public static int GetNumberBackUpPlane(GameContext.Plane planeID)
	{
		return PlayerPrefs.GetInt("NumberBackUpPlane" + planeID.ToString(), 3);
	}

	public static void SetNumberBackUpPlane(GameContext.Plane planeID, int _value)
	{
		PlayerPrefs.SetInt("NumberBackUpPlane" + planeID.ToString(), _value);
	}

	public static void ResetNumberBackupPlane(GameContext.Plane planeID)
	{
		PlayerPrefs.SetInt("NumberBackUpPlane" + planeID.ToString(), 3);
	}

	public static bool IsCompleteTutorialSceneSelectLevel
	{
		get
		{
			return PlayerPrefs.GetInt("TutorialSceneSelectLevel", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("TutorialSceneSelectLevel", 1);
			}
		}
	}

	public static bool IsCompleteTutorialSceneHome
	{
		get
		{
			return PlayerPrefs.GetInt("TutorialSceneHome", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("TutorialSceneHome", 1);
			}
		}
	}

	public static string LastTimeQuitGame
	{
		get
		{
			return PlayerPrefs.GetString("LastTimeQuitGame", DateTime.Now.ToBinary().ToString());
		}
		set
		{
			PlayerPrefs.SetString("LastTimeQuitGame", value);
		}
	}

	public static int SecondRecoveryEnergy
	{
		get
		{
			return PlayerPrefs.GetInt("SecondRecoveryEnergy", 300);
		}
		set
		{
			PlayerPrefs.SetInt("SecondRecoveryEnergy", value);
		}
	}

	public static bool IsDisableStarterPack
	{
		get
		{
			return PlayerPrefs.GetInt("IsDisableStarterPack", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("IsDisableStarterPack", 1);
			}
			else
			{
				PlayerPrefs.SetInt("IsDisableStarterPack", 0);
			}
		}
	}

	public static bool IsPurchaseStarterPack
	{
		get
		{
			return PlayerPrefs.GetInt("IsPurchaseStarterPack", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("IsPurchaseStarterPack", 1);
			}
			else
			{
				PlayerPrefs.SetInt("IsPurchaseStarterPack", 0);
			}
		}
	}

	public static bool IsPurchasePremiumPack
	{
		get
		{
			return PlayerPrefs.GetInt("IsPurchasePremiumPack", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("IsPurchasePremiumPack", 1);
			}
			else
			{
				PlayerPrefs.SetInt("IsPurchasePremiumPack", 0);
			}
		}
	}

	public static string FirstTimeInitGame
	{
		get
		{
			return PlayerPrefs.GetString("FirstTimeInitGame");
		}
		set
		{
			PlayerPrefs.SetString("FirstTimeInitGame", value);
		}
	}

	public static string EndTimeStarterPack
	{
		get
		{
			return PlayerPrefs.GetString("EndTimeStarterPack");
		}
		set
		{
			PlayerPrefs.SetString("EndTimeStarterPack", value);
		}
	}

	public static string OldDay
	{
		get
		{
			return PlayerPrefs.GetString("OldDay");
		}
		set
		{
			PlayerPrefs.SetString("OldDay", value);
		}
	}

	public static string EndTimePremiumPack
	{
		get
		{
			return PlayerPrefs.GetString("EndTimePremiumPack");
		}
		set
		{
			PlayerPrefs.SetString("EndTimePremiumPack", value);
		}
	}

	public static string QuestData
	{
		get
		{
			return PlayerPrefs.GetString("QuestData");
		}
		set
		{
			PlayerPrefs.SetString("QuestData", value);
		}
	}

	public static string QuestEndlessData
	{
		get
		{
			return PlayerPrefs.GetString("QuestEndlessData");
		}
		set
		{
			PlayerPrefs.SetString("QuestEndlessData", value);
		}
	}

	public static string DataLevel
	{
		get
		{
			return PlayerPrefs.GetString("DataLevel");
		}
		set
		{
			PlayerPrefs.SetString("DataLevel", value);
		}
	}

	public static string DataLevelBossMode
	{
		get
		{
			return PlayerPrefs.GetString("DataLevelBossMode");
		}
		set
		{
			PlayerPrefs.SetString("DataLevelBossMode", value);
		}
	}

	public static int NumberResetShopCoinItem
	{
		get
		{
			return PlayerPrefs.GetInt("NumberResetShopCoinItem", 0);
		}
		set
		{
			PlayerPrefs.SetInt("NumberResetShopCoinItem", value);
		}
	}

	public static string StateItemShopCoin
	{
		get
		{
			return PlayerPrefs.GetString("StateItemShopCoin");
		}
		set
		{
			PlayerPrefs.SetString("StateItemShopCoin", value);
		}
	}

	public static string StateClaimRewardOneTimeEndlessMode
	{
		get
		{
			return PlayerPrefs.GetString("StateClaimRewardOneTimeEndlessMode");
		}
		set
		{
			PlayerPrefs.SetString("StateClaimRewardOneTimeEndlessMode", value);
		}
	}

	public static bool IsRemoveAds
	{
		get
		{
			return PlayerPrefs.GetInt("IsRemoveAds", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("IsRemoveAds", 1);
			}
			else
			{
				PlayerPrefs.SetInt("IsRemoveAds", 0);
			}
		}
	}

	public static int TimeLeftFreeBox
	{
		get
		{
			return PlayerPrefs.GetInt("TimeLeftFreeBox", 3600);
		}
		set
		{
			PlayerPrefs.SetInt("TimeLeftFreeBox", value);
		}
	}

	public static int NumberDayCollectGift
	{
		get
		{
			return PlayerPrefs.GetInt("NumberDayCollectGift", 0);
		}
		set
		{
			PlayerPrefs.SetInt("NumberDayCollectGift", value);
		}
	}

	public static bool CanCollectDailyGift
	{
		get
		{
			return PlayerPrefs.GetInt("CanCollectDailyGift", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("CanCollectDailyGift", 1);
			}
			else
			{
				PlayerPrefs.SetInt("CanCollectDailyGift", 0);
			}
		}
	}

	public static bool IsOpenShopItemCoin
	{
		get
		{
			return PlayerPrefs.GetInt("IsOpenShopItemCoin", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("IsOpenShopItemCoin", 1);
			}
			else
			{
				PlayerPrefs.SetInt("IsOpenShopItemCoin", 0);
			}
		}
	}

	public static string DataPlane
	{
		get
		{
			return PlayerPrefs.GetString("DataPlaneGame");
		}
		set
		{
			PlayerPrefs.SetString("DataPlaneGame", value);
		}
	}

	public static string DataDrone
	{
		get
		{
			return PlayerPrefs.GetString("DataDroneGame");
		}
		set
		{
			PlayerPrefs.SetString("DataDroneGame", value);
		}
	}

	public static int RewardOneTimeEndlessModeCollected
	{
		get
		{
			return PlayerPrefs.GetInt("RewardOneTimeEndlessModeCollected", 0);
		}
		set
		{
			PlayerPrefs.SetInt("RewardOneTimeEndlessModeCollected", value);
		}
	}

	public static int NumberPlayEndlessModeInDay
	{
		get
		{
			return PlayerPrefs.GetInt("NumberPlayEndlessModeInDay", 0);
		}
		set
		{
			PlayerPrefs.SetInt("NumberPlayEndlessModeInDay", value);
		}
	}

	public static int NumberWatchVideoToGetFreeGiftInDay
	{
		get
		{
			return PlayerPrefs.GetInt("NumberWatchVideoToGetFreeGiftInDay", 0);
		}
		set
		{
			PlayerPrefs.SetInt("NumberWatchVideoToGetFreeGiftInDay", value);
		}
	}

	public static int TotalSecondCountDownFreeGift
	{
		get
		{
			return PlayerPrefs.GetInt("TotalSecondCountDownFreeGift", 0);
		}
		set
		{
			PlayerPrefs.SetInt("TotalSecondCountDownFreeGift", value);
		}
	}

	public static bool IsPurchaseSuperSpaceShipPack
	{
		get
		{
			return PlayerPrefs.GetInt("IsPurchaseSuperSpaceShipPack", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("IsPurchaseSuperSpaceShipPack", 1);
			}
			else
			{
				PlayerPrefs.SetInt("IsPurchaseSuperSpaceShipPack", 0);
			}
		}
	}

	public static bool IsPurchaseWarlockPack
	{
		get
		{
			return PlayerPrefs.GetInt("IsPurchaseWarlockPack", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("IsPurchaseWarlockPack", 1);
			}
			else
			{
				PlayerPrefs.SetInt("IsPurchaseWarlockPack", 0);
			}
		}
	}

	public static bool IsPurchaseSSLightningPack
	{
		get
		{
			return PlayerPrefs.GetInt("IsPurchaseSSLightningPack", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("IsPurchaseSSLightningPack", 1);
			}
			else
			{
				PlayerPrefs.SetInt("IsPurchaseSSLightningPack", 0);
			}
		}
	}

	public static bool IsPurchaseSpaceshipPack
	{
		get
		{
			return PlayerPrefs.GetInt("IsPurchaseSpaceshipPack", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("IsPurchaseSpaceshipPack", 1);
			}
			else
			{
				PlayerPrefs.SetInt("IsPurchaseSpaceshipPack", 0);
			}
		}
	}

	public static int NumberWatchVideoToBecomeRich
	{
		get
		{
			return PlayerPrefs.GetInt("NumberWatchVideoToBecomeRich", 0);
		}
		set
		{
			PlayerPrefs.SetInt("NumberWatchVideoToBecomeRich", value);
		}
	}

	public static string StateClaimRewardEventNoel
	{
		get
		{
			return PlayerPrefs.GetString("StateClaimRewardEventNoel");
		}
		set
		{
			PlayerPrefs.SetString("StateClaimRewardEventNoel", value);
		}
	}

	public static int NumberCandyEventNoelCollected
	{
		get
		{
			return PlayerPrefs.GetInt("NumberCandyEventNoelCollected", 0);
		}
		set
		{
			PlayerPrefs.SetInt("NumberCandyEventNoelCollected", value);
		}
	}

	public static int CurrentNumberCandyEventNoel
	{
		get
		{
			return PlayerPrefs.GetInt("CurrentNumberCandyEventNoel", 0);
		}
		set
		{
			PlayerPrefs.SetInt("CurrentNumberCandyEventNoel", value);
		}
	}

	public static string EndTimeEventNoel
	{
		get
		{
			return PlayerPrefs.GetString("EndTimeEventNoel");
		}
		set
		{
			PlayerPrefs.SetString("EndTimeEventNoel", value);
		}
	}

	public static string DataLevelNoel
	{
		get
		{
			return PlayerPrefs.GetString("DataLevelNoel");
		}
		set
		{
			PlayerPrefs.SetString("DataLevelNoel", value);
		}
	}

	public static float Sensitivy
	{
		get
		{
			return PlayerPrefs.GetFloat("Sensitivy", 1f);
		}
		set
		{
			PlayerPrefs.SetFloat("Sensitivy", value);
		}
	}

	public static bool IsSetSensitivy
	{
		get
		{
			return PlayerPrefs.GetFloat("IsSetSensitivy", 0f) == 1f;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetFloat("IsSetSensitivy", 1f);
			}
			else
			{
				PlayerPrefs.SetFloat("IsSetSensitivy", 0f);
			}
		}
	}

	public static int TypeMovePlayer
	{
		get
		{
			return PlayerPrefs.GetInt("TypeMovePlayer", 1);
		}
		set
		{
			PlayerPrefs.SetInt("TypeMovePlayer", value);
		}
	}

	public static string LanguageCode
	{
		get
		{
			return PlayerPrefs.GetString("LanguageCode", "en");
		}
		set
		{
			PlayerPrefs.SetString("LanguageCode", value);
		}
	}

	public static string LastTimeShowBecomeRich
	{
		get
		{
			return PlayerPrefs.GetString("LastTimeShowBecomeRich");
		}
		set
		{
			PlayerPrefs.SetString("LastTimeShowBecomeRich", value);
		}
	}

	public static bool IsSetLanguage
	{
		get
		{
			return PlayerPrefs.GetFloat("IsSetLanguage", 0f) == 1f;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetFloat("IsSetLanguage", 1f);
			}
			else
			{
				PlayerPrefs.SetFloat("IsSetLanguage", 0f);
			}
		}
	}

	public static string StateClaimRewardVIP
	{
		get
		{
			return PlayerPrefs.GetString("StateClaimRewardVIP");
		}
		set
		{
			PlayerPrefs.SetString("StateClaimRewardVIP", value);
		}
	}

	public static bool CanCollectDailyGiftVIP
	{
		get
		{
			return PlayerPrefs.GetInt("CanCollectDailyGiftVIP", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("CanCollectDailyGiftVIP", 1);
			}
			else
			{
				PlayerPrefs.SetInt("CanCollectDailyGiftVIP", 0);
			}
		}
	}

	public static string StateOneTimeOfferPackGem
	{
		get
		{
			return PlayerPrefs.GetString("StateOneTimeOfferPackGem");
		}
		set
		{
			PlayerPrefs.SetString("StateOneTimeOfferPackGem", value);
		}
	}

	public static int IDPackGemX3
	{
		get
		{
			return PlayerPrefs.GetInt("IDPackGemX3", 1);
		}
		set
		{
			PlayerPrefs.SetInt("IDPackGemX3", value);
		}
	}

	public static string NextTimeX3Pack
	{
		get
		{
			return PlayerPrefs.GetString("NextTimeX3Pack");
		}
		set
		{
			PlayerPrefs.SetString("NextTimeX3Pack", value);
		}
	}

	public static string PackSale
	{
		get
		{
			return PlayerPrefs.GetString("PackSale");
		}
		set
		{
			PlayerPrefs.SetString("PackSale", value);
		}
	}

	public static string NextTimeSale
	{
		get
		{
			return PlayerPrefs.GetString("NextTimeX3Pack");
		}
		set
		{
			PlayerPrefs.SetString("NextTimeX3Pack", value);
		}
	}

	public static bool IsPurchaseUltraStarshipCard
	{
		get
		{
			return PlayerPrefs.GetInt("IsPurchaseUltraStarshipCard", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("IsPurchaseUltraStarshipCard", 1);
			}
			else
			{
				PlayerPrefs.SetInt("IsPurchaseUltraStarshipCard", 0);
			}
		}
	}

	public static bool IsPurchaseUltraDroneCard
	{
		get
		{
			return PlayerPrefs.GetInt("IsPurchaseUltraDroneCard", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("IsPurchaseUltraDroneCard", 1);
			}
			else
			{
				PlayerPrefs.SetInt("IsPurchaseUltraDroneCard", 0);
			}
		}
	}

	public static bool IsPurchasePackOneTime
	{
		get
		{
			return PlayerPrefs.GetInt("IsPurchasePackOneTime", 0) == 1;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("IsPurchasePackOneTime", 1);
			}
			else
			{
				PlayerPrefs.SetInt("IsPurchasePackOneTime", 0);
			}
		}
	}

	public static int NumberWatchAdsGemCoin
	{
		get
		{
			return PlayerPrefs.GetInt("NumberWatchAdsGemCoin", 5);
		}
		set
		{
			PlayerPrefs.SetInt("NumberWatchAdsGemCoin", value);
		}
	}

	public static int NumberWatchAdsCard
	{
		get
		{
			return PlayerPrefs.GetInt("NumberWatchAdsCard", 5);
		}
		set
		{
			PlayerPrefs.SetInt("NumberWatchAdsCard", value);
		}
	}

	public static int NumberWatchAdsItemBooster
	{
		get
		{
			return PlayerPrefs.GetInt("NumberWatchAdsItemBooster", 5);
		}
		set
		{
			PlayerPrefs.SetInt("NumberWatchAdsItemBooster", value);
		}
	}

	private const string strCoinOwned = "TotalCoinsOwned";

	private const string strGemsOwned = "TotalGemsOwned";
}
