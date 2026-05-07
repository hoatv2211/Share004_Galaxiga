using System;
using I2.Loc;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drone Data", menuName = "Drone Data")]
public class DroneData : ScriptableObject
{
	public int BaseDamage
	{
		get
		{
			return DroneDataSheet.Get((int)this.droneID).power;
		}
	}

	public int BaseSuperDamage
	{
		get
		{
			return DroneDataSheet.Get((int)this.droneID).specPower;
		}
	}

	public int CurrentDamage
	{
		get
		{
			return Mathf.RoundToInt(this.MultiplierRank * ((float)this.BaseDamage + (float)this.BaseDamage * 0.05f * (float)(this.Level - 1)));
		}
	}

	public int CurrentSuperDamage
	{
		get
		{
			UnityEngine.Debug.Log("droneID: " + this.droneID);
			UnityEngine.Debug.Log("MultiplierRank: " + this.MultiplierRank);
			UnityEngine.Debug.Log("BaseSuperDamage: " + this.BaseSuperDamage);
			UnityEngine.Debug.Log("Level: " + this.Level);
			UnityEngine.Debug.Log("Damage: " + Mathf.RoundToInt(this.MultiplierRank * ((float)this.BaseSuperDamage + (float)this.BaseSuperDamage * 0.05f * (float)(this.Level - 1))));
			return Mathf.RoundToInt(this.MultiplierRank * ((float)this.BaseSuperDamage + (float)this.BaseSuperDamage * 0.05f * (float)(this.Level - 1)));
		}
	}

	public float MultiplierRank
	{
		get
		{
			string currentRank = this.CurrentRank;
			int num = DataGame.Current.ConverRankPlaneToIndex(this.CurrentRank);
			return PlaneDataMaxSheet.Get(num - 1).multiPlier;
		}
	}

	public int DPS
	{
		get
		{
			this.GetBaseDataDrone();
			int level = this.Level;
			return Mathf.RoundToInt(this.MultiplierRank * (((float)this.BaseDamage + 0.05f * (float)this.BaseDamage * (float)(level - 1)) / this.fireRateMainGun * this.numberBulletMainGun * this.accurateMainGun));
		}
	}

	public int NexDPSRank
	{
		get
		{
			this.GetBaseDataDrone();
			int level = this.Level;
			return Mathf.RoundToInt(this.MultiplierRank * (((float)this.BaseDamage + 0.05f * (float)this.BaseDamage * (float)level) / this.fireRateMainGun * this.numberBulletMainGun * this.accurateMainGun));
		}
	}

	private void GetBaseDataDrone()
	{
		UnityEngine.Debug.Log("GetBaseDataDrone:  fireRateMainGun" + this.fireRateMainGun);
		UnityEngine.Debug.Log("GetBaseDataDrone:  accurateMainGun" + this.accurateMainGun);
		UnityEngine.Debug.Log("GetBaseDataDrone:  numberBulletMainGun" + this.numberBulletMainGun);
		if (this.fireRateMainGun == 0f)
		{
			this.fireRateMainGun = DroneDataSheet.Get((int)this.droneID).fireRate;
		}
		if (this.accurateMainGun == 0f)
		{
			this.accurateMainGun = (float)DroneDataSheet.Get((int)this.droneID).accurateMainGun;
		}
		if (this.numberBulletMainGun == 0f)
		{
			this.numberBulletMainGun = (float)DroneDataSheet.Get((int)this.droneID).numberBulletMainGun;
		}
	}

	public float DurationSkill
	{
		get
		{
			return DroneDataSheet.Get((int)this.droneID).durationSkill;
		}
	}

	public float CooldownSkill
	{
		get
		{
			return DroneDataSheet.Get((int)this.droneID).cooldownSkill;
		}
	}

	public string BaseRank
	{
		get
		{
			return DroneDataSheet.Get((int)this.droneID).rank;
		}
	}

	public int NumberCardCraft
	{
		get
		{
			return DroneDataSheet.Get((int)this.droneID).numberCardCraft;
		}
	}

	public int NumberCardToEvolve
	{
		get
		{
			string currentRank = this.CurrentRank;
			if (currentRank != GameContext.Rank.SSS.ToString())
			{
				int rankID = DataGame.Current.ConverRankPlaneToIndex(currentRank);
				return DroneDataMaxSheet.Get(rankID).cardCraft;
			}
			return 0;
		}
	}

	public int CardEvolveDrone
	{
		get
		{
			return PlayerPrefs.GetInt("CardDrone" + this.NameDrone, 0);
		}
		set
		{
			PlayerPrefs.SetInt("CardDrone" + this.NameDrone, value);
			SaveDataDrone.Instance.droneInfoContainer.arrDrones[this.droneID - GameContext.Drone.GatlingGun].cardDrone = value;
			Tung.Log(string.Concat(new object[]
			{
				"card drone ",
				this.droneID.ToString(),
				" ",
				value
			}));
		}
	}

	public int MaxLevel
	{
		get
		{
			int num = DataGame.Current.ConverRankPlaneToIndex(this.CurrentRank);
			return DroneDataMaxSheet.Get(num - 1).maxLevel;
		}
	}

	public int NextMaxLevel
	{
		get
		{
			string currentRank = this.CurrentRank;
			if (currentRank != GameContext.Rank.SSS.ToString())
			{
				int rankID = DataGame.Current.ConverRankPlaneToIndex(currentRank);
				return DroneDataMaxSheet.Get(rankID).maxLevel;
			}
			return 0;
		}
	}

	public int Level
	{
		get
		{
			return CacheGame.GetCurrentLevelDrone(this.droneID);
		}
		set
		{
			CacheGame.SetLevelDrone(this.droneID, value);
		}
	}

	public string CurrentRank
	{
		get
		{
			return PlayerPrefs.GetString("Curren Rank " + this.NameDrone, this.BaseRank);
		}
		set
		{
			PlayerPrefs.SetString("Curren Rank " + this.NameDrone, value);
			SaveDataDrone.Instance.droneInfoContainer.arrDrones[this.droneID - GameContext.Drone.GatlingGun].rank = value;
			UnityEngine.Debug.Log("rank drone " + this.droneID.ToString() + " " + value);
		}
	}

	public float BaseFireRate
	{
		get
		{
			return DroneDataSheet.Get((int)this.droneID).fireRate;
		}
	}

	public float CurrentCoolDownSkill
	{
		get
		{
			return this.CooldownSkill;
		}
	}

	public int CoinUpgrade
	{
		get
		{
			return DroneCostUpgradeSheet.Get(this.Level).priceCoin;
		}
	}

	public int GemUpgrade
	{
		get
		{
			return DroneCostUpgradeSheet.Get(this.Level).priceGem;
		}
	}

	public bool IsOwned
	{
		get
		{
			return CacheGame.IsOwnedDrone(this.droneID);
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
				result = ScriptLocalization.missile_strike;
				break;
			case GameContext.Drone.AutoGatlingGun:
				result = ScriptLocalization.missile_shroud;
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

	public string NameDrone;

	private float fireRateMainGun;

	private float accurateMainGun;

	private float numberBulletMainGun;
}
