using System;
using Hellmade.Sound;
using I2.Loc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
	private void Awake()
	{
		if (this.logEnabled)
		{
			UnityEngine.Debug.unityLogger.logEnabled = true;
		}
		else
		{
			UnityEngine.Debug.unityLogger.logEnabled = false;
		}
		if (!GameContext.deserialize)
		{
			Deserializer.Deserialize(this.set);
			GameContext.deserialize = true;
		}
	}

	private void Start()
	{
		IntroScene.GetDataFromeCache();
		this.SetBackgroundMusic();
		this.CheckGame();
	}

	private void SetBackgroundMusic()
	{
		int audioID = EazySoundManager.PlayMusic(AudioCache.Music.main_menu, 1f, true, true);
		GameContext.audioMainMenu = EazySoundManager.GetMusicAudio(audioID);
		Tung.Log("SetBackgroundMusic Complete");
	}

	private void CheckGame()
	{
		Application.targetFrameRate = 60;
		this.CheckLanguage();
		this.CheckVersion();
		this.CheckFirstGame();
		SceneManager.LoadScene("LoadingFirstGame");
	}

	private void CheckFirstGame()
	{
		string @string = PlayerPrefs.GetString("FirstGame");
		if (@string == string.Empty)
		{
			PlayerPrefs.SetString("FirstGame", "inited");
			this.InitFirstGame();
		}
	}

	private void InitFirstGame()
	{
		CacheGame.EndTimeStarterPack = DateTime.Now.AddDays(3.0).ToBinary().ToString();
		CacheGame.GetPlane(GameContext.Plane.BataFD01);
		CacheGame.GetPlane(GameContext.Plane.FuryOfAres);
		CacheGame.SetPlaneEquip(GameContext.Plane.BataFD01);
		CacheGame.CanCollectDailyGift = true;
		GameContext.availableWatchVideoToOpenBox = true;
		GameContext.canCollectDailyGift = true;
		GameContext.isOpenShopItemCoin = false;
		CacheGame.InitCoin(1000);
		CurrencyLog.LogGoldIn(1000, CurrencyLog.In.Init, "New_Player");
		CacheGame.InitGem(10);
		CurrencyLog.LogGemIn(10, CurrencyLog.In.Init, "New_Player");
		GameContext.firstInstallGame = true;
		Tung.Log("Init First Game Complete");
	}

	private void CheckVersion()
	{
		string version = Application.version;
		float num = float.Parse(version) * 10f;
		float version2 = CacheGame.Version;
		if (num != version2)
		{
			CacheGame.Version = num;
			if (version2 > 0f && version2 < 87f)
			{
				DataGame.Current.SetNumberCardDrone(GameContext.Drone.Nighturge, 0);
				DataGame.Current.SetNumberCardDrone(GameContext.Drone.Terigon, 0);
				DataGame.Current.SetNumberCardDrone(GameContext.Drone.GodOfThunder, 0);
				CacheGame.SetOwnedDrone(GameContext.Drone.Terigon, false);
				CacheGame.SetOwnedDrone(GameContext.Drone.Nighturge, false);
				CacheGame.SetOwnedDrone(GameContext.Drone.GodOfThunder, false);
			}
			if (version2 > 70f)
			{
				UITutorial.isTurnOnTutorial = true;
			}
		}
		GameContext.currentVersionGame = version2;
		Tung.Log("current version " + CacheGame.Version);
	}

	private void CheckLanguage()
	{
		if (!CacheGame.IsSetLanguage)
		{
			CacheGame.IsSetLanguage = true;
			SystemLanguage systemLanguage = Application.systemLanguage;
			switch (systemLanguage)
			{
			case SystemLanguage.Indonesian:
				GameContext.SetLanguage(GameContext.Language.German);
				break;
			case SystemLanguage.Italian:
				GameContext.SetLanguage(GameContext.Language.Italian);
				break;
			case SystemLanguage.Japanese:
				GameContext.SetLanguage(GameContext.Language.Japanese);
				break;
			case SystemLanguage.Korean:
				GameContext.SetLanguage(GameContext.Language.Korean);
				break;
			default:
				switch (systemLanguage)
				{
				case SystemLanguage.Spanish:
					GameContext.SetLanguage(GameContext.Language.Spanish);
					break;
				default:
					if (systemLanguage != SystemLanguage.French)
					{
						if (systemLanguage != SystemLanguage.German)
						{
							if (systemLanguage != SystemLanguage.Chinese)
							{
								if (systemLanguage != SystemLanguage.English)
								{
									GameContext.SetLanguage(GameContext.Language.English);
								}
								else
								{
									GameContext.SetLanguage(GameContext.Language.English);
								}
							}
							else
							{
								GameContext.SetLanguage(GameContext.Language.Chinese);
							}
						}
						else
						{
							GameContext.SetLanguage(GameContext.Language.Chinese);
						}
					}
					else
					{
						GameContext.SetLanguage(GameContext.Language.French);
					}
					break;
				case SystemLanguage.Thai:
					GameContext.SetLanguage(GameContext.Language.Thai);
					break;
				case SystemLanguage.Turkish:
					GameContext.SetLanguage(GameContext.Language.Turkish);
					break;
				case SystemLanguage.Vietnamese:
					GameContext.SetLanguage(GameContext.Language.Vietnamese);
					break;
				case SystemLanguage.ChineseSimplified:
					GameContext.SetLanguage(GameContext.Language.Chinese);
					break;
				case SystemLanguage.ChineseTraditional:
					GameContext.SetLanguage(GameContext.Language.Chinese);
					break;
				}
				break;
			case SystemLanguage.Portuguese:
				GameContext.SetLanguage(GameContext.Language.Portuguese);
				break;
			case SystemLanguage.Russian:
				GameContext.SetLanguage(GameContext.Language.Russian);
				break;
			}
		}
		else
		{
			int num = Enum.GetNames(typeof(GameContext.LanguageCode)).Length;
			for (int i = 0; i < num; i++)
			{
				if (Enum.GetName(typeof(GameContext.LanguageCode), i) == LocalizationManager.CurrentLanguageCode)
				{
					GameContext.CurrentIdLanguage = i;
					break;
				}
			}
		}
		Tung.Log("Check Language Complete ");
	}

	public static void GetDataFromeCache()
	{
		ShopContext.currentCoin = CacheGame.GetCoins();
		ShopContext.currentGem = CacheGame.GetGems();
		GameContext.totalUltraStarshipCard = CacheGame.GetTotalCardAllPlane();
		GameContext.totalUltraDroneCard = CacheGame.GetTotalCardAllDrone();
		GameContext.currentVIPPoint = CacheGame.VIPPoint;
		GameContext.sensitivity = CacheGame.Sensitivy;
		GameContext.typeMove = CacheGame.TypeMovePlayer;
		GameContext.currentIDPackGemX3 = CacheGame.IDPackGemX3;
		GameContext.currentPlaneIDEquiped = CacheGame.GetPlaneEquiped();
		GameContext.maxLevelUnlocked = CacheGame.GetMaxLevel();
		GameContext.maxLevelBosssUnlocked = CacheGame.GetMaxLevelBoss();
		GameContext.lastTimeQuitGame = CacheGame.LastTimeQuitGame;
		GameContext.totalEnergy = CacheGame.GetTotalEnergy();
		GameContext.totalSkillPlane = CacheGame.GetNumberSkillPlane();
		GameContext.totalItemPowerUpBullet5 = CacheGame.NumberItemPowerUpBullet5;
		GameContext.totalItemPowerUpBullet10 = CacheGame.NumberItemPowerUpBullet10;
		GameContext.totalItemPowerShield = CacheGame.NumberItemPowerShield;
		GameContext.totalItemPowerUpHeart = CacheGame.NumberItemPowerHeart;
		GameContext.totalKeySuperPrize = CacheGame.KeySuperRize;
		GameContext.isPurchasePremiumPack = CacheGame.IsPurchasePremiumPack;
		GameContext.volumeMusic = CacheGame.VolumeMusic;
		GameContext.volumeSound = CacheGame.VolumeSound;
		GameContext.isVibration = CacheGame.IsVibration;
		GameContext.isRemoveAds = CacheGame.IsRemoveAds;
		GameContext.currentSecondsTimeLeftWatchVideoToOpenBox = CacheGame.TimeLeftFreeBox;
		GameContext.canCollectDailyGift = CacheGame.CanCollectDailyGift;
		GameContext.canCollectDailyGiftVIP = CacheGame.CanCollectDailyGiftVIP;
		GameContext.isOpenShopItemCoin = CacheGame.IsOpenShopItemCoin;
		GameContext.isPurchaseStarterPack = CacheGame.IsPurchaseStarterPack;
		GameContext.isDisableStarterPack = CacheGame.IsDisableStarterPack;
		EazySoundManager.GlobalMusicVolume = GameContext.volumeMusic;
		EazySoundManager.GlobalSoundsVolume = GameContext.volumeSound;
		EazySoundManager.GlobalUISoundsVolume = GameContext.volumeSound;
		GameContext.numberCandyEventNoelCollected = CacheGame.NumberCandyEventNoelCollected;
		GameContext.currentNumberCandyEventNoel = CacheGame.CurrentNumberCandyEventNoel;
		SaveDataLevel.LoadData();
		SaveDataStateItemShopCoin.LoadData();
		SaveDataQuest.LoadData();
		SaveDataQuestEndless.LoadData();
		SaveDataLevelBossMode.LoadData();
		SaveDataStateClaimRewardVIP.LoadData();
		SaveDataStateOneTimeOfferPackGem.LoadData();
		Tung.Log("Load data frome cache complete");
	}

	public SerializableSet set;

	public bool logEnabled;
}
