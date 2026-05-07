using System;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class DataGame : MonoBehaviour
{
	public static DataGame Current
	{
		get
		{
			if (DataGame.instance == null)
			{
				DataGame.instance = UnityEngine.Object.FindObjectOfType<DataGame>();
			}
			return DataGame.instance;
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
	}

	public List<GameContext.Plane> ListPlaneUnlock
	{
		get
		{
			List<GameContext.Plane> list = new List<GameContext.Plane>();
			for (int i = 0; i < this.arrPlanes.Length; i++)
			{
				if (this.arrPlanes[i].planeData.IsUnlock)
				{
					list.Add(this.arrPlanes[i].planeID);
				}
			}
			return list;
		}
	}

	public List<GameContext.Plane> ListPlaneOwned
	{
		get
		{
			List<GameContext.Plane> list = new List<GameContext.Plane>();
			for (int i = 0; i < this.arrPlanes.Length; i++)
			{
				if (this.arrPlanes[i].planeData.IsOwned)
				{
					list.Add(this.arrPlanes[i].planeID);
				}
			}
			return list;
		}
	}

	public List<GameContext.Drone> ListDroneOwned
	{
		get
		{
			List<GameContext.Drone> list = new List<GameContext.Drone>();
			for (int i = 0; i < this.arrDrones.Length; i++)
			{
				if (this.arrDrones[i].droneData.IsOwned)
				{
					list.Add(this.arrDrones[i].droneID);
				}
			}
			return list;
		}
	}

	public string RankPlane(GameContext.Plane planeID)
	{
		return this.arrPlanes[(int)planeID].CurrentRank;
	}

	public int IndexRankPlane(GameContext.Plane planeID)
	{
		string rank = string.Empty;
		rank = this.arrPlanes[(int)planeID].CurrentRank;
		return this.ConverRankPlaneToIndex(rank);
	}

	public int ConverRankPlaneToIndex(string rank)
	{
		int result = 0;
		int length = Enum.GetValues(typeof(GameContext.Rank)).Length;
		for (int i = 0; i < length; i++)
		{
			string name = Enum.GetName(typeof(GameContext.Rank), i);
			if (name == rank)
			{
				result = i + 1;
				break;
			}
		}
		return result;
	}

	public GameContext.Rank GetRank(string rank)
	{
		GameContext.Rank result = GameContext.Rank.C;
		int length = Enum.GetValues(typeof(GameContext.Rank)).Length;
		for (int i = 0; i < length; i++)
		{
			string name = Enum.GetName(typeof(GameContext.Rank), i);
			if (name == rank)
			{
				result = (GameContext.Rank)i;
				break;
			}
		}
		return result;
	}

	public string RankDrone(GameContext.Drone droneID)
	{
		string result = string.Empty;
		switch (droneID)
		{
		case GameContext.Drone.NoDrone:
			result = string.Empty;
			break;
		case GameContext.Drone.GatlingGun:
			result = this.arrDrones[0].CurrentRank;
			break;
		case GameContext.Drone.AutoGatlingGun:
			result = this.arrDrones[1].CurrentRank;
			break;
		case GameContext.Drone.Laser:
			result = this.arrDrones[2].CurrentRank;
			break;
		case GameContext.Drone.Nighturge:
			result = this.arrDrones[3].CurrentRank;
			break;
		case GameContext.Drone.GodOfThunder:
			result = this.arrDrones[4].CurrentRank;
			break;
		case GameContext.Drone.Terigon:
			result = this.arrDrones[5].CurrentRank;
			break;
		}
		return result;
	}

	public void AddCardPlane(GameContext.Plane planeID, int numberCard)
	{
		int num = this.arrPlanes[(int)planeID].planeData.CardEvolvePlane;
		num += numberCard;
		this.arrPlanes[(int)planeID].planeData.CardEvolvePlane = num;
		if (PlaneManager.current != null)
		{
			PlaneManager.current.CheckPlane(planeID);
		}
	}

	public void AddCardDrone(GameContext.Drone droneID, int numberCard)
	{
		int num = this.arrDrones[droneID - GameContext.Drone.GatlingGun].droneData.CardEvolveDrone;
		num += numberCard;
		this.arrDrones[droneID - GameContext.Drone.GatlingGun].droneData.CardEvolveDrone = num;
		if (DroneManager.current != null)
		{
			DroneManager.current.CheckDrone(droneID);
		}
	}

	public int GetNumberCardDrone(GameContext.Drone droneID)
	{
		return this.arrDrones[droneID - GameContext.Drone.GatlingGun].droneData.CardEvolveDrone;
	}

	public void SetNumberCardDrone(GameContext.Drone droneID, int number)
	{
		this.arrDrones[droneID - GameContext.Drone.GatlingGun].droneData.CardEvolveDrone = number;
	}

	public GameContext.Plane RandomPlaneID
	{
		get
		{
			return this.arrPlanes[UnityEngine.Random.Range(0, this.arrPlanes.Length)].planeID;
		}
	}

	public GameContext.Drone RandomDroneID()
	{
		GameContext.Drone droneID = this.arrDrones[UnityEngine.Random.Range(0, this.arrDrones.Length)].droneID;
		if (this.arrDroneNotRandom.Contains(droneID))
		{
			return this.RandomDroneID();
		}
		return droneID;
	}

	public int GetCurrentLevelPlane(GameContext.Plane planeID)
	{
		return this.arrPlanes[(int)planeID].Level;
	}

	public int GetCurrentLevelPlane(int indexPlane)
	{
		return this.arrPlanes[indexPlane].Level;
	}

	public void GetInfoPlane(GameContext.Plane _planeID)
	{
		try
		{
			this.SetData(this.arrPlanes[(int)_planeID]);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
		}
	}

	public void GetInfoDrone(GameContext.Drone _droneID)
	{
		try
		{
			this.SetData(this.arrDrones[_droneID - GameContext.Drone.GatlingGun].droneData);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
		}
	}

	private void SetData(DataGame.Planes planeData)
	{
		DataGame.InfoPlane.PlaneName = planeData.PlaneName;
		DataGame.InfoPlane.Level = planeData.Level;
		DataGame.InfoPlane.BaseRank = planeData.BaseRank;
		DataGame.InfoPlane.CurrentRank = planeData.CurrentRank;
		DataGame.InfoPlane.DPS = planeData.DPS;
		DataGame.InfoPlane.BasePower = planeData.BaseMainPower;
		DataGame.InfoPlane.CurrentPower = planeData.CurrentPower;
		DataGame.InfoPlane.BaseSubPower = planeData.BaseSubPower;
		DataGame.InfoPlane.CurrentSubPower = planeData.CurrentSubPower;
		DataGame.InfoPlane.BaseSpecialPower = planeData.BaseSpecialPower;
		DataGame.InfoPlane.CurrentSpecialPower = planeData.CurrentSpecialPower;
		DataGame.InfoPlane.DescriptionSkill = planeData.DescriptionSkill;
		DataGame.InfoPlane.NameSkill = planeData.NameSkillPlane;
		DataGame.InfoPlane.NumberCardToEvolve = planeData.NumberCardToEvolution;
		DataGame.InfoPlane.CurrentNumberCard = planeData.CurrentNumberCardToEvolution;
	}

	private void SetData(DroneData droneData)
	{
		DataGame.InfoDrone.DroneName = droneData.NameDrone;
		DataGame.InfoDrone.IsOwned = droneData.IsOwned;
		DataGame.InfoDrone.Level = droneData.Level;
		DataGame.InfoDrone.CurrentRank = droneData.CurrentRank;
		DataGame.InfoDrone.CurrentPower = droneData.CurrentDamage;
		DataGame.InfoDrone.CurrentSpecialPower = droneData.CurrentSuperDamage;
		DataGame.InfoDrone.NameSkill = droneData.NameSkill;
		DataGame.InfoDrone.DescriptionSkill = droneData.DescriptionSkill;
		DataGame.InfoDrone.NumberCardToEvolve = droneData.NumberCardToEvolve;
		DataGame.InfoDrone.CurrentNumberCard = droneData.CardEvolveDrone;
	}

	public void SetSpriteImagePlane(Image imgPlane, GameContext.Plane _planeID)
	{
		imgPlane.sprite = this.arrPlanes[(int)_planeID].sprPlane;
		imgPlane.SetNativeSize();
	}

	public void SetSpriteImageDrone(Image imgDrone, GameContext.Drone _dronID)
	{
		try
		{
			Tung.Log("drone ID" + _dronID);
			imgDrone.sprite = this.arrDrones[_dronID - GameContext.Drone.GatlingGun].sprDrone;
			imgDrone.SetNativeSize();
		}
		catch (Exception ex)
		{
			Tung.LogError(ex.Message);
		}
	}

	public void SetSpriteImageSkillDrone(Image imgSkillDrone, GameContext.Drone _droneID)
	{
		switch (_droneID)
		{
		case GameContext.Drone.GatlingGun:
			imgSkillDrone.sprite = this.arrDrones[0].sprSkillDrone;
			break;
		case GameContext.Drone.AutoGatlingGun:
			imgSkillDrone.sprite = this.arrDrones[1].sprSkillDrone;
			break;
		case GameContext.Drone.Laser:
			imgSkillDrone.sprite = this.arrDrones[2].sprSkillDrone;
			break;
		case GameContext.Drone.Nighturge:
			imgSkillDrone.sprite = this.arrDrones[3].sprSkillDrone;
			break;
		case GameContext.Drone.GodOfThunder:
			imgSkillDrone.sprite = this.arrDrones[4].sprSkillDrone;
			break;
		case GameContext.Drone.Terigon:
			imgSkillDrone.sprite = this.arrDrones[5].sprSkillDrone;
			break;
		}
		imgSkillDrone.SetNativeSize();
	}

	public void SetRankPlane(GameContext.Plane plane, GameContext.Rank rank)
	{
		try
		{
			this.arrPlanes[(int)plane].planeData.CurrentRank = rank.ToString();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
		}
	}

	public void SetRankDrone(GameContext.Drone drone, GameContext.Rank rank)
	{
		try
		{
			this.arrDrones[drone - GameContext.Drone.GatlingGun].droneData.CurrentRank = rank.ToString();
		}
		catch (Exception ex)
		{
			Tung.Log(ex.Message);
		}
	}

	public Sprite SpritePlane(GameContext.Plane _planeID)
	{
		return this.arrPlanes[(int)_planeID].sprPlane;
	}

	public Sprite SpriteDrone(GameContext.Drone _dronID)
	{
		return this.arrDrones[_dronID - GameContext.Drone.GatlingGun].sprDrone;
	}

	public void SetImageRank(Image img, string rank)
	{
		if (rank == GameContext.Rank.C.ToString())
		{
			img.sprite = this.arrSprRank[0];
		}
		else if (rank == GameContext.Rank.B.ToString())
		{
			img.sprite = this.arrSprRank[1];
		}
		else if (rank == GameContext.Rank.A.ToString())
		{
			img.sprite = this.arrSprRank[2];
		}
		else if (rank == GameContext.Rank.S.ToString())
		{
			img.sprite = this.arrSprRank[3];
		}
		else if (rank == GameContext.Rank.SS.ToString())
		{
			img.sprite = this.arrSprRank[4];
		}
		else if (rank == GameContext.Rank.SSS.ToString())
		{
			img.sprite = this.arrSprRank[5];
		}
		img.SetNativeSize();
	}

	public void SetImageRank2(Image img, string rank)
	{
		if (rank == GameContext.Rank.C.ToString())
		{
			img.sprite = this.arrSprRank2[0];
		}
		else if (rank == GameContext.Rank.B.ToString())
		{
			img.sprite = this.arrSprRank2[1];
		}
		else if (rank == GameContext.Rank.A.ToString())
		{
			img.sprite = this.arrSprRank2[2];
		}
		else if (rank == GameContext.Rank.S.ToString())
		{
			img.sprite = this.arrSprRank2[3];
		}
		else if (rank == GameContext.Rank.SS.ToString())
		{
			img.sprite = this.arrSprRank2[4];
		}
		else if (rank == GameContext.Rank.SSS.ToString())
		{
			img.sprite = this.arrSprRank2[5];
		}
		img.SetNativeSize();
	}

	public RewardLevel RewardLevelCampain(int level, GameContext.ModeLevel modeLevel)
	{
		RewardLevel result;
		switch (modeLevel)
		{
		case GameContext.ModeLevel.Easy:
		{
			string cardPlane = RewardModeLevelEasySheet.Get(level).cardPlane;
			string cardDrone = RewardModeLevelEasySheet.Get(level).cardDrone;
			int numberCardPlane = RewardModeLevelEasySheet.Get(level).numberCardPlane;
			int numberCardDrone = RewardModeLevelEasySheet.Get(level).numberCardDrone;
			string unlockPlane = RewardModeLevelEasySheet.Get(level).unlockPlane;
			int numberCoins = RewardModeLevelEasySheet.Get(level).numberCoins;
			int numberGems = RewardModeLevelEasySheet.Get(level).numberGems;
			int numberBox = RewardModeLevelEasySheet.Get(level).numberBox;
			result = new RewardLevel(unlockPlane, cardPlane, cardDrone, numberCardPlane, numberCardDrone, numberCoins, numberGems, numberBox, 0, 0);
			break;
		}
		case GameContext.ModeLevel.Normal:
		{
			string unlockPlane2 = RewardModeLevelNormalSheet.Get(level).unlockPlane;
			string cardPlane2 = RewardModeLevelNormalSheet.Get(level).cardPlane;
			string cardDrone2 = RewardModeLevelNormalSheet.Get(level).cardDrone;
			int numberCardPlane2 = RewardModeLevelNormalSheet.Get(level).numberCardPlane;
			int numberCardDrone2 = RewardModeLevelNormalSheet.Get(level).numberCardDrone;
			int numberCoins2 = RewardModeLevelNormalSheet.Get(level).numberCoins;
			int numberGems2 = RewardModeLevelNormalSheet.Get(level).numberGems;
			int numberBox2 = RewardModeLevelNormalSheet.Get(level).numberBox;
			result = new RewardLevel(unlockPlane2, cardPlane2, cardDrone2, numberCardPlane2, numberCardDrone2, numberCoins2, numberGems2, numberBox2, 0, 0);
			break;
		}
		case GameContext.ModeLevel.Hard:
		{
			string cardPlane3 = RewardModeLevelHardSheet.Get(level).cardPlane;
			string cardDrone3 = RewardModeLevelHardSheet.Get(level).cardDrone;
			int numberCardPlane3 = RewardModeLevelHardSheet.Get(level).numberCardPlane;
			int numberCardDrone3 = RewardModeLevelHardSheet.Get(level).numberCardDrone;
			string unlockPlane3 = RewardModeLevelHardSheet.Get(level).unlockPlane;
			int numberCoins3 = RewardModeLevelHardSheet.Get(level).numberCoins;
			int numberGems3 = RewardModeLevelHardSheet.Get(level).numberGems;
			int numberBox3 = RewardModeLevelHardSheet.Get(level).numberBox;
			result = new RewardLevel(unlockPlane3, cardPlane3, cardDrone3, numberCardPlane3, numberCardDrone3, numberCoins3, numberGems3, numberBox3, 0, 0);
			break;
		}
		default:
		{
			string cardPlane4 = RewardModeLevelEasySheet.Get(level).cardPlane;
			string cardDrone4 = RewardModeLevelEasySheet.Get(level).cardDrone;
			int numberCardPlane4 = RewardModeLevelEasySheet.Get(level).numberCardPlane;
			int numberCardDrone4 = RewardModeLevelEasySheet.Get(level).numberCardDrone;
			string unlockPlane4 = RewardModeLevelEasySheet.Get(level).unlockPlane;
			int numberCoins4 = RewardModeLevelEasySheet.Get(level).numberCoins;
			int numberGems4 = RewardModeLevelEasySheet.Get(level).numberGems;
			int numberBox4 = RewardModeLevelEasySheet.Get(level).numberBox;
			result = new RewardLevel(unlockPlane4, cardPlane4, cardDrone4, numberCardPlane4, numberCardDrone4, numberCoins4, numberGems4, numberBox4, 0, 0);
			break;
		}
		}
		return result;
	}

	public string NameSkillPlane(int planeID)
	{
		string result = string.Empty;
		switch (planeID)
		{
		case 0:
			result = ScriptLocalization.bullet_hell;
			break;
		case 1:
			result = ScriptLocalization.dead_bullet;
			break;
		case 2:
			result = ScriptLocalization.rising_sun;
			break;
		case 3:
			result = ScriptLocalization.cruel_angle;
			break;
		case 4:
			result = ScriptLocalization.hammer_of_the_gods;
			break;
		case 5:
			result = ScriptLocalization.god_of_thunder;
			break;
		case 6:
			result = ScriptLocalization.super_homing_missile;
			break;
		}
		return result;
	}

	public string DesSkillPlane(int planeID)
	{
		string result = string.Empty;
		switch (planeID)
		{
		case 0:
			result = ScriptLocalization.des_skill_starship_batafd01;
			break;
		case 1:
			result = ScriptLocalization.des_skill_starship_fury;
			break;
		case 2:
			result = ScriptLocalization.des_skill_starship_skywraith;
			break;
		case 3:
			result = ScriptLocalization.des_skill_starship_twilightx;
			break;
		case 4:
			result = ScriptLocalization.des_skill_starship_greataxe;
			break;
		case 5:
			result = ScriptLocalization.des_skill_starship_sslightning;
			break;
		case 6:
			result = ScriptLocalization.des_skill_starship_warlock;
			break;
		}
		return result;
	}

	private static DataGame instance;

	public DataGame.Planes[] arrPlanes;

	public DataGame.Drone[] arrDrones;

	public Sprite[] arrSprRank;

	public Sprite[] arrSprRank2;

	public List<GameContext.Drone> arrDroneNotRandom = new List<GameContext.Drone>
	{
		GameContext.Drone.GodOfThunder
	};

	[Serializable]
	public class Planes
	{
		public string PlaneName
		{
			get
			{
				return this.planeData.namePlane;
			}
		}

		public int Level
		{
			get
			{
				return this.planeData.Level;
			}
		}

		public string BaseRank
		{
			get
			{
				return PlaneDataSheet.Get((int)this.planeID).rank;
			}
		}

		public string CurrentRank
		{
			get
			{
				return this.planeData.CurrentRank;
			}
		}

		public int DPS
		{
			get
			{
				return this.planeData.DPS;
			}
		}

		public int BaseMainPower
		{
			get
			{
				return this.planeData.BaseMainDamage;
			}
		}

		public int CurrentPower
		{
			get
			{
				return this.planeData.CurrentMainDamage;
			}
		}

		public int BaseSubPower
		{
			get
			{
				return this.planeData.BaseSubDamage;
			}
		}

		public int CurrentSubPower
		{
			get
			{
				return this.planeData.CurrentSubPower;
			}
		}

		public int BaseSpecialPower
		{
			get
			{
				return this.planeData.BaseSuperDamage;
			}
		}

		public int CurrentSpecialPower
		{
			get
			{
				return this.planeData.CurrentSuperDamage;
			}
		}

		public string DescriptionSkill
		{
			get
			{
				return this.planeData.DescriptionSkill;
			}
		}

		public string NameSkillPlane
		{
			get
			{
				string result = string.Empty;
				switch (this.planeID)
				{
				case GameContext.Plane.BataFD01:
					result = ScriptLocalization.bullet_hell;
					break;
				case GameContext.Plane.FuryOfAres:
					result = ScriptLocalization.dead_bullet;
					break;
				case GameContext.Plane.SkyWraith:
					result = ScriptLocalization.rising_sun;
					break;
				case GameContext.Plane.TwilightX:
					result = ScriptLocalization.cruel_angle;
					break;
				case GameContext.Plane.Greataxe:
					result = ScriptLocalization.hammer_of_the_gods;
					break;
				case GameContext.Plane.SSLightning:
					result = ScriptLocalization.god_of_thunder;
					break;
				case GameContext.Plane.Warlock:
					result = ScriptLocalization.super_homing_missile;
					break;
				}
				return result;
			}
		}

		public int NumberCardToEvolution
		{
			get
			{
				return this.planeData.NumberCardToEvolve;
			}
		}

		public int CurrentNumberCardToEvolution
		{
			get
			{
				return this.planeData.CardEvolvePlane;
			}
		}

		public GameContext.Plane planeID;

		public PlaneData planeData;

		public Sprite sprPlane;
	}

	[Serializable]
	public class Drone
	{
		public string DroneName
		{
			get
			{
				return this.droneData.NameDrone;
			}
		}

		public int BasePower
		{
			get
			{
				return this.droneData.BaseDamage;
			}
		}

		public int CurrentPower
		{
			get
			{
				return this.droneData.CurrentDamage;
			}
		}

		public int BaseSpecialPower
		{
			get
			{
				return this.droneData.BaseSuperDamage;
			}
		}

		public int CurrentSpecialPower
		{
			get
			{
				return this.droneData.CurrentSuperDamage;
			}
		}

		public string BaseRank
		{
			get
			{
				return this.droneData.BaseRank;
			}
		}

		public string CurrentRank
		{
			get
			{
				return this.droneData.CurrentRank;
			}
		}

		public string NameSkill
		{
			get
			{
				string result = string.Empty;
				switch (this.droneID)
				{
				case GameContext.Drone.GatlingGun:
					result = ScriptLocalization.missile_shroud;
					break;
				case GameContext.Drone.AutoGatlingGun:
					result = ScriptLocalization.missile_strike;
					break;
				case GameContext.Drone.Laser:
					result = ScriptLocalization.dead_laser;
					break;
				case GameContext.Drone.Nighturge:
					result = ScriptLocalization.big_bomb;
					break;
				case GameContext.Drone.GodOfThunder:
					result = ScriptLocalization.thunder_ball;
					break;
				case GameContext.Drone.Terigon:
					result = ScriptLocalization.fire_spirits;
					break;
				}
				return result;
			}
		}

		public string DescriptionSkill
		{
			get
			{
				string result = string.Empty;
				switch (this.droneID)
				{
				case GameContext.Drone.GatlingGun:
					result = ScriptLocalization.fire_a_barrage;
					break;
				case GameContext.Drone.AutoGatlingGun:
					result = ScriptLocalization.fire_a_barrage_of_homing;
					break;
				case GameContext.Drone.Laser:
					result = ScriptLocalization.firepower_with_powerful_laser_gun;
					break;
				case GameContext.Drone.Nighturge:
					result = ScriptLocalization.fire_a_bigbomb;
					break;
				case GameContext.Drone.GodOfThunder:
					result = ScriptLocalization.fire_a_thunderball;
					break;
				case GameContext.Drone.Terigon:
					result = ScriptLocalization.summon_4_firespirits;
					break;
				}
				return result;
			}
		}

		public GameContext.Drone droneID;

		public DroneData droneData;

		public Sprite sprDrone;

		public Sprite sprSkillDrone;
	}

	public static class InfoPlane
	{
		public static string PlaneName;

		public static int Level;

		public static string BaseRank;

		public static string CurrentRank;

		public static int DPS;

		public static int BasePower;

		public static int BaseSubPower;

		public static int BaseSpecialPower;

		public static int CurrentPower;

		public static int CurrentSubPower;

		public static int CurrentSpecialPower;

		public static string NameSkill;

		public static string DescriptionSkill;

		public static int CurrentNumberCard;

		public static int NumberCardToEvolve;
	}

	public static class InfoDrone
	{
		public static string DroneName;

		public static bool IsOwned;

		public static int Level;

		public static string CurrentRank;

		public static int CurrentPower;

		public static int CurrentSpecialPower;

		public static string NameSkill;

		public static string DescriptionSkill;

		public static int CurrentNumberCard;

		public static int NumberCardToEvolve;
	}
}
