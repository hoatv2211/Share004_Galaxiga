using System;
using System.Collections;
using DG.Tweening;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class RewardStageManager : MonoBehaviour
{
	private void Awake()
	{
		RewardStageManager.current = this;
	}

	private void Start()
	{
	}

	[EnumAction(typeof(GameContext.ModeLevel))]
	public void SelectModeGame(int mode)
	{
		if (this.currentModeLevel != mode)
		{
			this.modeLevel[this.indexArrModeLevel].HideObjReward();
			this.currentModeLevel = mode;
			if (mode != 0)
			{
				if (mode != 1)
				{
					if (mode == 2)
					{
						GameContext.currentEnergyLevel = DataEnergyLevelSheet.Get(GameContext.currentLevel).energyHard;
						this.imgBGModeStage.color = this.colorStageHard;
					}
				}
				else
				{
					GameContext.currentEnergyLevel = DataEnergyLevelSheet.Get(GameContext.currentLevel).energyNormal;
					this.imgBGModeStage.color = this.colorStageNormal;
				}
			}
			else
			{
				GameContext.currentEnergyLevel = DataEnergyLevelSheet.Get(GameContext.currentLevel).energyEasy;
				this.imgBGModeStage.color = this.colorStageEasy;
			}
			this.indexArrModeLevel = mode;
			this.SetDataRewardCurrentLevel((GameContext.ModeLevel)mode);
			GameContext.currentModeLevel = (GameContext.ModeLevel)mode;
			GameContext.isPassCurrentLevel = this.modeLevel[mode].isPassLevel;
			this.isUnlockCurrentLevel = this.modeLevel[mode].isUnlockLevel;
			this.modeLevel[mode].ShowObjReward();
			if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Boss)
			{
				GameContext.currentEnergyLevel = 8;
			}
			this.textEnergyLevel.text = string.Empty + GameContext.currentEnergyLevel;
		}
	}

	public void ShowPanelReward()
	{
		if (GameContext.maxLevelUnlocked < 12)
		{
			if (NewTutorial.current.currentStepTutorial_UseSkillPlane == 7 && NewTutorial.current.currentStepTutorial_BuyItems == 0)
			{
				NewTutorial.current.BuyItems_Step0();
			}
			else
			{
				NewTutorial.current.ShowForcusBtnPlayGame();
			}
		}
		EscapeManager.Current.AddAction(new Action(this.HidePanelReward));
		this.objRewardCampain.SetActive(true);
		this.iconEnergy.SetActive(true);
		this.panelReward.SetActive(true);
		for (int i = 0; i < 3; i++)
		{
			this.modeLevel[i].CheckLevel();
		}
		if (this.modeLevel[2].isUnlockLevel)
		{
			this.SelectModeGame(2);
		}
		else if (this.modeLevel[1].isUnlockLevel)
		{
			this.SelectModeGame(1);
		}
		else
		{
			this.SelectModeGame(0);
		}
		this.CheckPlayGame();
		this.CheckPassCurrentLevel();
		GameContext.ModeGamePlay currentModeGamePlay = GameContext.currentModeGamePlay;
		if (currentModeGamePlay != GameContext.ModeGamePlay.Campaign)
		{
			if (currentModeGamePlay == GameContext.ModeGamePlay.Boss)
			{
				GameContext.currentEnergyBackupLevel = 7;
				this.textStage.text = ScriptLocalization.boss + " " + GameContext.currentLevel;
			}
		}
		else
		{
			GameContext.currentEnergyBackupLevel = DataEnergyLevelSheet.Get(GameContext.currentLevel).backupEnergy;
			this.textStage.text = ScriptLocalization.stage + " " + GameContext.currentLevel;
		}
		DOTween.Restart("REWARD_STAGE", true, -1f);
		DOTween.Play("REWARD_STAGE");
	}

	public void HidePanelReward()
	{
		DOTween.PlayBackwards("REWARD_STAGE");
		this.shopItemPowerUp.DeselectAllItem();
		base.StartCoroutine(this.DelayHide());
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelReward));
	}

	private IEnumerator DelayHide()
	{
		yield return new WaitForSeconds(0.1f);
		this.panelReward.SetActive(false);
		this.currentModeLevel = -1;
		yield break;
	}

	public void ShowPanelInfoItemPowerUp()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePanelInfoItemPowerUp));
		this.panelInfoItemPowerUp.SetActive(true);
		DOTween.Restart("INFO_ITEM_POWER_UP", true, -1f);
		DOTween.Play("INFO_ITEM_POWER_UP");
	}

	public void HidePanelInfoItemPowerUp()
	{
		DOTween.PlayBackwards("INFO_ITEM_POWER_UP");
		base.StartCoroutine(GameContext.Delay(0.2f, delegate
		{
			this.panelInfoItemPowerUp.SetActive(false);
		}));
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelInfoItemPowerUp));
	}

	public void CheckPlayGame()
	{
		GameContext.ModeGamePlay currentModeGamePlay = GameContext.currentModeGamePlay;
		if (currentModeGamePlay != GameContext.ModeGamePlay.Campaign)
		{
			if (currentModeGamePlay == GameContext.ModeGamePlay.Boss)
			{
				this.maxLevelCurrentModeGamePlay = GameContext.maxLevelBosssUnlocked;
			}
		}
		else
		{
			this.maxLevelCurrentModeGamePlay = GameContext.maxLevelUnlocked;
		}
		if (GameContext.currentLevel <= this.maxLevelCurrentModeGamePlay)
		{
			if (GameContext.currentEnergyLevel <= GameContext.totalEnergy)
			{
				this.imgBtnPlay.sprite = this.sprBtnPlay;
			}
			else
			{
				this.imgBtnPlay.sprite = this.sprBtnPlayDisable;
			}
		}
		else
		{
			this.imgBtnPlay.sprite = this.sprBtnPlayDisable;
		}
	}

	private void SetDataRewardCurrentLevel(GameContext.ModeLevel mode)
	{
		if (mode != GameContext.ModeLevel.Easy)
		{
			if (mode != GameContext.ModeLevel.Normal)
			{
				if (mode == GameContext.ModeLevel.Hard)
				{
					if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Campaign)
					{
						this.rewardCurrentLevel = DataGame.Current.RewardLevelCampain(GameContext.currentLevel, GameContext.ModeLevel.Hard);
					}
					else if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Boss)
					{
						this.rewardCurrentLevel = new RewardLevel("None", "None", "None", 0, 0, DataRewardBossModeHardSheet.Get(GameContext.currentLevel).coin, 0, 0, DataRewardBossModeHardSheet.Get(GameContext.currentLevel).cardRandomPlane, DataRewardBossModeHardSheet.Get(GameContext.currentLevel).cardRandomDrone);
					}
					RewardLevel rewardLevel = DataGame.Current.RewardLevelCampain(GameContext.currentLevel, GameContext.ModeLevel.Normal);
					GameContext.maxCoinEarnedIn1Stage = (int)((float)rewardLevel.numberCoins * 0.15f);
				}
			}
			else
			{
				if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Campaign)
				{
					this.rewardCurrentLevel = DataGame.Current.RewardLevelCampain(GameContext.currentLevel, GameContext.ModeLevel.Normal);
				}
				else if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Boss)
				{
					this.rewardCurrentLevel = new RewardLevel("None", "None", "None", 0, 0, DataRewardBossModeNormalSheet.Get(GameContext.currentLevel).coin, 0, 0, DataRewardBossModeNormalSheet.Get(GameContext.currentLevel).cardRandomPlane, DataRewardBossModeNormalSheet.Get(GameContext.currentLevel).cardRandomDrone);
				}
				GameContext.maxCoinEarnedIn1Stage = (int)((float)this.rewardCurrentLevel.numberCoins * 0.1f);
			}
		}
		else
		{
			if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Campaign)
			{
				this.rewardCurrentLevel = DataGame.Current.RewardLevelCampain(GameContext.currentLevel, GameContext.ModeLevel.Easy);
			}
			else if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Boss)
			{
				this.rewardCurrentLevel = new RewardLevel("None", "None", "None", 0, 0, DataRewardBossModeEasySheet.Get(GameContext.currentLevel).coin, 0, 0, DataRewardBossModeEasySheet.Get(GameContext.currentLevel).cardRandomPlane, DataRewardBossModeEasySheet.Get(GameContext.currentLevel).cardRandomDrone);
			}
			GameContext.maxCoinEarnedIn1Stage = (int)((float)this.rewardCurrentLevel.numberCoins * 0.1f);
		}
		this.CheckReward(this.rewardCurrentLevel);
		int num = this.arrReward.Length;
		for (int i = 0; i < num; i++)
		{
			this.arrReward[i].CheckGetReward(mode);
		}
	}

	private void CheckReward(RewardLevel reward)
	{
		string unlockPlane = reward.unlockPlane;
		if (unlockPlane == "None" || unlockPlane == "none")
		{
			this.arrReward[0].HideReward();
		}
		else
		{
			this.arrReward[0].ShowReward();
			if (unlockPlane == "Bata FD 01")
			{
				this.arrReward[0].SetImagePlaneDrone(GameContext.Plane.BataFD01);
			}
			else if (unlockPlane == "Sky Wraith")
			{
				this.arrReward[0].SetImagePlaneDrone(GameContext.Plane.SkyWraith);
			}
			else if (unlockPlane == "Fury Of Ares")
			{
				this.arrReward[0].SetImagePlaneDrone(GameContext.Plane.FuryOfAres);
			}
			else if (unlockPlane == "Greataxe")
			{
				this.arrReward[0].SetImagePlaneDrone(GameContext.Plane.Greataxe);
			}
			else if (unlockPlane == "Twilight X")
			{
				this.arrReward[0].SetImagePlaneDrone(GameContext.Plane.TwilightX);
			}
			else if (unlockPlane == "SS Lightning")
			{
				this.arrReward[0].SetImagePlaneDrone(GameContext.Plane.SSLightning);
			}
			else if (unlockPlane == "Warlock")
			{
				this.arrReward[0].SetImagePlaneDrone(GameContext.Plane.Warlock);
			}
		}
		string cardPlane = reward.cardPlane;
		if (reward.numberCardPlane > 0)
		{
			this.arrReward[1].ShowReward();
			this.arrReward[1].SetTextNumberReward(reward.numberCardPlane);
			if (cardPlane == "Bata FD 01")
			{
				this.arrReward[1].SetImagePlaneDrone(GameContext.Plane.BataFD01);
			}
			else if (cardPlane == "Sky Wraith")
			{
				this.arrReward[1].SetImagePlaneDrone(GameContext.Plane.SSLightning);
			}
			else if (cardPlane == "Fury Of Ares")
			{
				this.arrReward[1].SetImagePlaneDrone(GameContext.Plane.FuryOfAres);
			}
			else if (cardPlane == "Greataxe")
			{
				this.arrReward[1].SetImagePlaneDrone(GameContext.Plane.Greataxe);
			}
			else if (cardPlane == "Twilight X")
			{
				this.arrReward[1].SetImagePlaneDrone(GameContext.Plane.TwilightX);
			}
			else if (cardPlane == "SS Lightning")
			{
				this.arrReward[1].SetImagePlaneDrone(GameContext.Plane.SSLightning);
			}
			else if (cardPlane == "Warlock")
			{
				this.arrReward[1].SetImagePlaneDrone(GameContext.Plane.Warlock);
			}
		}
		else
		{
			this.arrReward[1].HideReward();
		}
		string cardDrone = reward.cardDrone;
		if (reward.numberCardDrone > 0)
		{
			this.arrReward[2].ShowReward();
			this.arrReward[2].SetTextNumberReward(reward.numberCardDrone);
			if (cardDrone == "Galing Gun")
			{
				this.arrReward[2].SetImagePlaneDrone(GameContext.Drone.GatlingGun);
			}
			else if (cardDrone == "Auto Gatling Gun")
			{
				this.arrReward[2].SetImagePlaneDrone(GameContext.Drone.AutoGatlingGun);
			}
			else if (cardDrone == "Laser")
			{
				this.arrReward[2].SetImagePlaneDrone(GameContext.Drone.Laser);
			}
			if (cardDrone == "Nighturge")
			{
				this.arrReward[2].SetImagePlaneDrone(GameContext.Drone.Nighturge);
			}
			else if (cardDrone == "God Of Thunder")
			{
				this.arrReward[2].SetImagePlaneDrone(GameContext.Drone.GodOfThunder);
			}
			else if (cardDrone == "Terigon")
			{
				this.arrReward[2].SetImagePlaneDrone(GameContext.Drone.Terigon);
			}
		}
		else
		{
			this.arrReward[2].HideReward();
		}
		if (reward.numberCoins == 0)
		{
			this.arrReward[3].HideReward();
		}
		else
		{
			this.arrReward[3].ShowReward();
			this.arrReward[3].SetTextNumberReward(reward.numberCoins);
		}
		if (reward.numberGem == 0)
		{
			this.arrReward[4].HideReward();
		}
		else
		{
			this.arrReward[4].ShowReward();
			this.arrReward[4].SetTextNumberReward(reward.numberGem);
		}
		if (reward.numberBox == 0)
		{
			this.arrReward[5].HideReward();
		}
		else
		{
			this.arrReward[5].ShowReward();
			this.arrReward[5].SetTextNumberReward(reward.numberBox);
		}
		if (reward.numberUltraStarshipCard == 0)
		{
			this.arrReward[6].HideReward();
		}
		else
		{
			this.arrReward[6].ShowReward();
			this.arrReward[6].SetTextNumberReward(reward.numberUltraStarshipCard);
		}
		if (reward.numberUltraDroneCard == 0)
		{
			this.arrReward[7].HideReward();
		}
		else
		{
			this.arrReward[7].ShowReward();
			this.arrReward[7].SetTextNumberReward(reward.numberUltraDroneCard);
		}
	}

	private void CheckPassCurrentLevel()
	{
		bool isPassLevel = this.modeLevel[0].isPassLevel;
		bool isPassLevel2 = this.modeLevel[1].isPassLevel;
		bool isPassLevel3 = this.modeLevel[2].isPassLevel;
		if (isPassLevel)
		{
			this.smallStarEasy.SetActive(true);
		}
		else
		{
			this.smallStarEasy.SetActive(false);
		}
		if (isPassLevel2)
		{
			this.smallStarNormal.SetActive(true);
		}
		else
		{
			this.smallStarNormal.SetActive(false);
		}
		if (isPassLevel3)
		{
			this.smallStarHard.SetActive(true);
		}
		else
		{
			this.smallStarHard.SetActive(false);
		}
		GameContext.ModeLevel modeLevel = GameContext.currentModeLevel;
		if (modeLevel != GameContext.ModeLevel.Easy)
		{
			if (modeLevel != GameContext.ModeLevel.Normal)
			{
				if (modeLevel == GameContext.ModeLevel.Hard)
				{
					GameContext.isPassCurrentLevel = isPassLevel3;
				}
			}
			else
			{
				GameContext.isPassCurrentLevel = isPassLevel2;
			}
		}
		else
		{
			GameContext.isPassCurrentLevel = isPassLevel;
		}
	}

	public void SaveDataRewardCurrentLevel()
	{
		if (!GameContext.isPassCurrentLevel)
		{
			GameContext.rewardCurrentLevelPlay = this.rewardCurrentLevel;
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"Max Coin Earned In Stage + ",
				GameContext.currentLevel,
				" : ",
				GameContext.maxCoinEarnedIn1Stage
			}));
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"planeUnlock ",
				this.rewardCurrentLevel.unlockPlane,
				" || cardPlane ",
				this.rewardCurrentLevel.cardPlane,
				" || number cacd plane ",
				this.rewardCurrentLevel.numberCardPlane,
				" || cardDrone ",
				this.rewardCurrentLevel.cardDrone,
				" || number card drone ",
				this.rewardCurrentLevel.numberCardDrone,
				" || coin ",
				this.rewardCurrentLevel.numberCoins,
				" || gem ",
				this.rewardCurrentLevel.numberGem,
				" || box ",
				this.rewardCurrentLevel.numberBox
			}));
		}
		else
		{
			UnityEngine.Debug.Log("You have already passed current level");
		}
	}

	private string RandomRewardCardPlane()
	{
		string result = string.Empty;
		switch (DataGame.Current.RandomPlaneID)
		{
		case GameContext.Plane.BataFD01:
			result = "Bata FD 01";
			break;
		case GameContext.Plane.FuryOfAres:
			result = "Fury Of Ares";
			break;
		case GameContext.Plane.SkyWraith:
			result = "Sky Wraith";
			break;
		case GameContext.Plane.TwilightX:
			result = "Twilight X";
			break;
		case GameContext.Plane.Greataxe:
			result = "Greataxe";
			break;
		case GameContext.Plane.SSLightning:
			result = "SS Lightning";
			break;
		case GameContext.Plane.Warlock:
			result = "Warlock";
			break;
		}
		return result;
	}

	private string RandomRewardCardDrone()
	{
		string result = string.Empty;
		switch (DataGame.Current.RandomDroneID())
		{
		case GameContext.Drone.GatlingGun:
			result = "Galing Gun";
			break;
		case GameContext.Drone.AutoGatlingGun:
			result = "Auto Gatling Gun";
			break;
		case GameContext.Drone.Laser:
			result = "Laser";
			break;
		case GameContext.Drone.Nighturge:
			result = "Nighturge";
			break;
		case GameContext.Drone.GodOfThunder:
			result = "God Of Thunder";
			break;
		case GameContext.Drone.Terigon:
			result = "Terigon";
			break;
		}
		return result;
	}

	public static RewardStageManager current;

	public GameObject panelReward;

	public GameObject panelInfoItemPowerUp;

	public GameObject smallStarEasy;

	public GameObject smallStarNormal;

	public GameObject smallStarHard;

	public ShopItemPowerUp shopItemPowerUp;

	public Text textStage;

	public Image imgBtnPlay;

	public Sprite sprBtnPlay;

	public Sprite sprBtnPlayDisable;

	public Text textEnergyLevel;

	public Image imgBGModeStage;

	[HideInInspector]
	public bool isUnlockCurrentLevel;

	public GameObject objRewardCampain;

	public GameObject iconEnergy;

	public Text textPlay;

	public RewardStageManager.ModeLevel[] modeLevel;

	public RewardStageManager.Reward[] arrReward;

	public const string DRONE_GATLING_GUN = "Galing Gun";

	public const string DRONE_AUTO_GATLING_GUN = "Auto Gatling Gun";

	public const string DRONE_LASER = "Laser";

	public const string DRONE_NIGHTURGE = "Nighturge";

	public const string DRONE_GOD_OF_THUNDER = "God Of Thunder";

	public const string DRONE_TERIGON = "Terigon";

	public const string PLANE_BATA_FD_01 = "Bata FD 01";

	public const string PLANE_SKYWRAITH = "Sky Wraith";

	public const string PLANE_FURY_OF_ARES = "Fury Of Ares";

	public const string PLANE_GREATAXE = "Greataxe";

	public const string PLANE_TWIGLING_X = "Twilight X";

	public const string PLANE_SS_LIGHTNING = "SS Lightning";

	public const string PLANE_WARLOCK = "Warlock";

	private Color colorStageEasy = GameContext.ColorConvert(44f, 192f, 243f);

	private Color colorStageNormal = GameContext.ColorConvert(255f, 204f, 85f);

	private Color colorStageHard = GameContext.ColorConvert(244f, 73f, 104f);

	private int currentModeLevel = -1;

	private int indexArrModeLevel;

	private int maxLevelCurrentModeGamePlay;

	private RewardLevel rewardCurrentLevel;

	[Serializable]
	public class Reward
	{
		public bool IsRewardCoinGemBox()
		{
			return this.rewardType == GameContext.RewardType.Coin || this.rewardType == GameContext.RewardType.Gem || this.rewardType == GameContext.RewardType.Box || this.rewardType == GameContext.RewardType.Energy || this.rewardType == GameContext.RewardType.CardAllPlane || this.rewardType == GameContext.RewardType.CardAllDrone;
		}

		public bool IsRewardUnlockPlane()
		{
			return this.rewardType == GameContext.RewardType.UnlockPlane;
		}

		public void HideReward()
		{
			this.objReward.SetActive(false);
		}

		public void ShowReward()
		{
			this.objReward.SetActive(true);
		}

		public void CheckGetReward(GameContext.ModeLevel modeLevel)
		{
			if (modeLevel != GameContext.ModeLevel.Easy)
			{
				if (modeLevel != GameContext.ModeLevel.Normal)
				{
					if (modeLevel == GameContext.ModeLevel.Hard)
					{
						if (GameContext.numberStarCurrentLvl == 3)
						{
							this.objGetRewarded.SetActive(true);
						}
						else
						{
							this.objGetRewarded.SetActive(false);
						}
					}
				}
				else if (GameContext.numberStarCurrentLvl >= 2)
				{
					this.objGetRewarded.SetActive(true);
				}
				else
				{
					this.objGetRewarded.SetActive(false);
				}
			}
			else if (GameContext.numberStarCurrentLvl >= 1)
			{
				this.objGetRewarded.SetActive(true);
			}
			else
			{
				this.objGetRewarded.SetActive(false);
			}
		}

		public void SetTextNumberReward(int number)
		{
			this.textNumber.text = "x" + number;
			this.rewardView.numberReward = number;
		}

		public void SetImagePlaneDrone(GameContext.Plane planeID)
		{
			this.rewardView._planeID = planeID;
			DataGame.Current.SetSpriteImagePlane(this.imgPlaneDrone, planeID);
			DataGame.Current.SetImageRank2(this.imgBG, DataGame.Current.RankPlane(planeID));
		}

		public void SetImagePlaneDrone(GameContext.Drone droneID)
		{
			this.rewardView._droneID = droneID;
			DataGame.Current.SetSpriteImageDrone(this.imgPlaneDrone, droneID);
			DataGame.Current.SetImageRank2(this.imgBG, DataGame.Current.RankDrone(droneID));
		}

		public GameContext.RewardType rewardType;

		public GameObject objReward;

		[HideIf("IsRewardCoinGemBox", true)]
		public Image imgBG;

		[HideIf("IsRewardCoinGemBox", true)]
		public Image imgPlaneDrone;

		[HideIf("IsRewardUnlockPlane", true)]
		public Text textNumber;

		public GameObject objGetRewarded;

		public RewardView rewardView;
	}

	[Serializable]
	public class ModeLevel
	{
		public void ShowObjReward()
		{
			this.imgSelect.SetActive(true);
			DOTween.Restart("REWARD_EASY_MODE", true, -1f);
		}

		public void HideObjReward()
		{
			this.imgSelect.SetActive(false);
		}

		public void CheckLevel()
		{
			GameContext.ModeGamePlay currentModeGamePlay = GameContext.currentModeGamePlay;
			if (currentModeGamePlay != GameContext.ModeGamePlay.Campaign)
			{
				if (currentModeGamePlay == GameContext.ModeGamePlay.Boss)
				{
					this.isPassLevel = CacheGame.IsPassLevelBoss(this.modeLevel, GameContext.currentLevel);
					this.maxLevel = GameContext.maxLevelBosssUnlocked;
				}
			}
			else
			{
				this.isPassLevel = CacheGame.IsPassLevel(this.modeLevel, GameContext.currentLevel);
				this.maxLevel = GameContext.maxLevelUnlocked;
			}
			GameContext.ModeLevel modeLevel = this.modeLevel;
			if (modeLevel != GameContext.ModeLevel.Easy)
			{
				if (modeLevel != GameContext.ModeLevel.Normal)
				{
					if (modeLevel == GameContext.ModeLevel.Hard)
					{
						if (GameContext.numberStarCurrentLvl >= 2)
						{
							this.objLockLevel.SetActive(false);
							this.isUnlockLevel = true;
						}
						else
						{
							this.objLockLevel.SetActive(true);
							this.isUnlockLevel = false;
						}
					}
				}
				else if (GameContext.numberStarCurrentLvl >= 1)
				{
					this.objLockLevel.SetActive(false);
					this.isUnlockLevel = true;
				}
				else
				{
					this.objLockLevel.SetActive(true);
					this.isUnlockLevel = false;
				}
			}
			else if (this.maxLevel >= GameContext.currentLevel)
			{
				this.objLockLevel.SetActive(false);
				this.isUnlockLevel = true;
			}
			else
			{
				this.objLockLevel.SetActive(true);
				this.isUnlockLevel = false;
			}
		}

		public GameContext.ModeLevel modeLevel;

		public GameObject imgSelect;

		public GameObject objLockLevel;

		public bool isPassLevel;

		[HideInInspector]
		public bool isUnlockLevel;

		private int maxLevel;
	}
}
