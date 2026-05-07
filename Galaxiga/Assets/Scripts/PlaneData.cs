using System;
using I2.Loc;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plane Data", menuName = "Plane Data")]
public class PlaneData : ScriptableObject
{
	public int BaseMainDamage
	{
		get
		{
			int result;
			switch (this.plane)
			{
			case GameContext.Plane.BataFD01:
				result = ShipBataFD01Sheet.Get(this.indexPower).mainDamage;
				break;
			case GameContext.Plane.FuryOfAres:
				result = ShipFuryOfAresSheet.Get(this.indexPower).mainDamage;
				break;
			case GameContext.Plane.SkyWraith:
				result = ShipSkywraithSheet.Get(this.indexPower).mainDamage;
				break;
			case GameContext.Plane.TwilightX:
				result = ShipTwilightXSheet.Get(this.indexPower).mainDamage;
				break;
			case GameContext.Plane.Greataxe:
				result = ShipGreataxeSheet.Get(this.indexPower).mainDamage;
				break;
			case GameContext.Plane.SSLightning:
				result = ShipSSLightningSheet.Get(this.indexPower).mainDamage;
				break;
			case GameContext.Plane.Warlock:
				result = ShipWarlockSheet.Get(this.indexPower).mainDamage;
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}
	}

	public int BaseMainDamageLVL1
	{
		get
		{
			int result;
			switch (this.plane)
			{
			case GameContext.Plane.BataFD01:
				result = ShipBataFD01Sheet.Get(10).mainDamage;
				break;
			case GameContext.Plane.FuryOfAres:
				result = ShipFuryOfAresSheet.Get(10).mainDamage;
				break;
			case GameContext.Plane.SkyWraith:
				result = ShipSkywraithSheet.Get(10).mainDamage;
				break;
			case GameContext.Plane.TwilightX:
				result = ShipTwilightXSheet.Get(10).mainDamage;
				break;
			case GameContext.Plane.Greataxe:
				result = ShipGreataxeSheet.Get(10).mainDamage;
				break;
			case GameContext.Plane.SSLightning:
				result = ShipSSLightningSheet.Get(10).mainDamage;
				break;
			case GameContext.Plane.Warlock:
				result = ShipWarlockSheet.Get(10).mainDamage;
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}
	}

	public int MainDamage
	{
		get
		{
			int result;
			switch (this.plane)
			{
			case GameContext.Plane.BataFD01:
				result = ShipBataFD01Sheet.Get(GameContext.power).mainDamage;
				break;
			case GameContext.Plane.FuryOfAres:
				result = ShipFuryOfAresSheet.Get(GameContext.power).mainDamage;
				break;
			case GameContext.Plane.SkyWraith:
				result = ShipSkywraithSheet.Get(GameContext.power).mainDamage;
				break;
			case GameContext.Plane.TwilightX:
				result = ShipTwilightXSheet.Get(GameContext.power).mainDamage;
				break;
			case GameContext.Plane.Greataxe:
				result = ShipGreataxeSheet.Get(GameContext.power).mainDamage;
				break;
			case GameContext.Plane.SSLightning:
				result = ShipSSLightningSheet.Get(GameContext.power).mainDamage;
				break;
			case GameContext.Plane.Warlock:
				result = ShipWarlockSheet.Get(GameContext.power).mainDamage;
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}
	}

	public int MainSuperDamage
	{
		get
		{
			int result;
			switch (this.plane)
			{
			case GameContext.Plane.BataFD01:
				result = ShipBataFD01Sheet.Get(11).mainDamage;
				break;
			case GameContext.Plane.FuryOfAres:
				result = ShipFuryOfAresSheet.Get(11).mainDamage;
				break;
			case GameContext.Plane.SkyWraith:
				result = ShipSkywraithSheet.Get(11).mainDamage;
				break;
			case GameContext.Plane.TwilightX:
				result = ShipTwilightXSheet.Get(11).mainDamage;
				break;
			case GameContext.Plane.Greataxe:
				result = ShipGreataxeSheet.Get(11).mainDamage;
				break;
			case GameContext.Plane.SSLightning:
				result = ShipSSLightningSheet.Get(11).mainDamage;
				break;
			case GameContext.Plane.Warlock:
				result = ShipWarlockSheet.Get(11).mainDamage;
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}
	}

	public int CurrentBaseMainDamage
	{
		get
		{
			return this.BaseMainDamage + this.DeltaDamage * (this.Level - 1);
		}
	}

	public int CurrentMainDamage
	{
		get
		{
			int mainDamage = this.MainDamage;
			return Mathf.RoundToInt(this.MultiplierRank * ((float)mainDamage + (float)mainDamage * 0.05f * (float)(this.Level - 1)));
		}
	}

	public int DamageRankC
	{
		get
		{
			return this.BaseMainDamage + Mathf.RoundToInt((float)this.BaseMainDamage * 0.05f) * (this.Level - 1);
		}
	}

	public int DeltaDamage
	{
		get
		{
			return Mathf.RoundToInt((float)this.BaseMainDamage * 0.05f * this.MultiplierRank);
		}
	}

	public int indexPower
	{
		get
		{
			string baseRank = this.BaseRank;
			if (baseRank != null)
			{
				if (baseRank == "C")
				{
					return 1;
				}
				if (baseRank == "B")
				{
					return 3;
				}
				if (baseRank == "A")
				{
					return 5;
				}
				if (baseRank == "S")
				{
					return 5;
				}
				if (baseRank == "SS")
				{
					return 8;
				}
				if (baseRank == "SSS")
				{
					return 8;
				}
			}
			return 8;
		}
	}

	public int BaseSubDamage
	{
		get
		{
			int result;
			switch (this.plane)
			{
			case GameContext.Plane.BataFD01:
				result = ShipBataFD01Sheet.Get(this.indexPower).subDamage;
				break;
			case GameContext.Plane.FuryOfAres:
				result = ShipFuryOfAresSheet.Get(this.indexPower).subDamage;
				break;
			case GameContext.Plane.SkyWraith:
				result = ShipSkywraithSheet.Get(this.indexPower).subDamage;
				break;
			case GameContext.Plane.TwilightX:
				result = ShipTwilightXSheet.Get(this.indexPower).subDamage;
				break;
			case GameContext.Plane.Greataxe:
				result = ShipGreataxeSheet.Get(this.indexPower).subDamage;
				break;
			case GameContext.Plane.SSLightning:
				result = ShipSSLightningSheet.Get(this.indexPower).subDamage;
				break;
			case GameContext.Plane.Warlock:
				result = ShipWarlockSheet.Get(this.indexPower).subDamage;
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}
	}

	public int BaseSubDamageLVL1
	{
		get
		{
			int result;
			switch (this.plane)
			{
			case GameContext.Plane.BataFD01:
				result = ShipBataFD01Sheet.Get(10).subDamage;
				break;
			case GameContext.Plane.FuryOfAres:
				result = ShipFuryOfAresSheet.Get(10).subDamage;
				break;
			case GameContext.Plane.SkyWraith:
				result = ShipSkywraithSheet.Get(10).subDamage;
				break;
			case GameContext.Plane.TwilightX:
				result = ShipTwilightXSheet.Get(10).subDamage;
				break;
			case GameContext.Plane.Greataxe:
				result = ShipGreataxeSheet.Get(10).subDamage;
				break;
			case GameContext.Plane.SSLightning:
				result = ShipSSLightningSheet.Get(10).subDamage;
				break;
			case GameContext.Plane.Warlock:
				result = ShipWarlockSheet.Get(10).subDamage;
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}
	}

	public int CurrentSubPower
	{
		get
		{
			int result;
			switch (this.plane)
			{
			case GameContext.Plane.BataFD01:
			case GameContext.Plane.FuryOfAres:
			case GameContext.Plane.SkyWraith:
				result = 0;
				break;
			default:
				result = (int)(this.MultiplierRank * (float)this.SubDamageRankC);
				break;
			}
			return result;
		}
	}

	public int SubDamageRankC
	{
		get
		{
			int num = this.BaseSubDamage;
			int num2 = Mathf.RoundToInt((float)num * 0.05f * this.MultiplierRank);
			return num + num2 * (this.Level - 1);
		}
	}

	public int BaseSuperDamage
	{
		get
		{
			int result;
			switch (this.plane)
			{
			case GameContext.Plane.BataFD01:
				result = ShipBataFD01Sheet.Get(11).mainDamage;
				break;
			case GameContext.Plane.FuryOfAres:
				result = ShipFuryOfAresSheet.Get(11).mainDamage;
				break;
			case GameContext.Plane.SkyWraith:
				result = ShipSkywraithSheet.Get(11).mainDamage;
				break;
			case GameContext.Plane.TwilightX:
				result = ShipTwilightXSheet.Get(11).mainDamage;
				break;
			case GameContext.Plane.Greataxe:
				result = ShipGreataxeSheet.Get(11).mainDamage;
				break;
			case GameContext.Plane.SSLightning:
				result = ShipSSLightningSheet.Get(11).mainDamage;
				break;
			case GameContext.Plane.Warlock:
				result = ShipWarlockSheet.Get(11).mainDamage;
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}
	}

	public int CurrentSuperDamage
	{
		get
		{
			int mainSuperDamage = this.MainSuperDamage;
			return Mathf.RoundToInt(this.MultiplierRank * ((float)mainSuperDamage + (float)mainSuperDamage * 0.05f * (float)(this.Level - 1)));
		}
	}

	public float fireRateSkill
	{
		get
		{
			float result;
			switch (this.plane)
			{
			case GameContext.Plane.BataFD01:
				result = ShipBataFD01Sheet.Get(11).mainFireRate;
				break;
			case GameContext.Plane.FuryOfAres:
				result = ShipFuryOfAresSheet.Get(11).mainFireRate;
				break;
			case GameContext.Plane.SkyWraith:
				result = ShipSkywraithSheet.Get(11).mainFireRate;
				break;
			case GameContext.Plane.TwilightX:
				result = ShipTwilightXSheet.Get(11).mainFireRate;
				break;
			case GameContext.Plane.Greataxe:
				result = ShipGreataxeSheet.Get(11).mainFireRate;
				break;
			case GameContext.Plane.SSLightning:
				result = ShipSSLightningSheet.Get(11).mainFireRate;
				break;
			case GameContext.Plane.Warlock:
				result = ShipWarlockSheet.Get(11).mainFireRate;
				break;
			default:
				result = 0f;
				break;
			}
			return result;
		}
	}

	public float fireRateSubBullet
	{
		get
		{
			float result;
			switch (this.plane)
			{
			case GameContext.Plane.BataFD01:
				result = ShipBataFD01Sheet.Get(GameContext.power).subFireRate;
				break;
			case GameContext.Plane.FuryOfAres:
				result = ShipFuryOfAresSheet.Get(GameContext.power).subFireRate;
				break;
			case GameContext.Plane.SkyWraith:
				result = ShipSkywraithSheet.Get(GameContext.power).subFireRate;
				break;
			case GameContext.Plane.TwilightX:
				result = ShipTwilightXSheet.Get(GameContext.power).subFireRate;
				break;
			case GameContext.Plane.Greataxe:
				result = ShipGreataxeSheet.Get(GameContext.power).subFireRate;
				break;
			case GameContext.Plane.SSLightning:
				result = ShipSSLightningSheet.Get(GameContext.power).subFireRate;
				break;
			case GameContext.Plane.Warlock:
				result = ShipWarlockSheet.Get(GameContext.power).subFireRate;
				break;
			default:
				result = 0f;
				break;
			}
			return result;
		}
	}

	public int DPS
	{
		get
		{
			this.GetBaseDataDamagePlane();
			int level = this.Level;
			return Mathf.RoundToInt(this.MultiplierRank * (((float)this.baseMainDamage + 0.05f * (float)this.baseMainDamage * (float)(level - 1)) / this.fireRateMainGun * (float)this.numberBulletMainGun * this.accurateMainGun + ((float)this.baseSubDamage + 0.05f * (float)this.baseSubDamage * (float)(level - 1)) / this.fireRateSubGun * (float)this.numberBulletSubGun * this.accurateSubGun));
		}
	}

	public int NextDPS
	{
		get
		{
			this.GetBaseDataDamagePlane();
			int level = this.Level;
			return Mathf.RoundToInt(this.MultiplierRank * (((float)this.baseMainDamage + 0.05f * (float)this.baseMainDamage * (float)level) / this.fireRateMainGun * (float)this.numberBulletMainGun * this.accurateMainGun + ((float)this.baseSubDamage + 0.05f * (float)this.baseSubDamage * (float)level) / this.fireRateSubGun * (float)this.numberBulletSubGun * this.accurateSubGun));
		}
	}

	public int NextRankDPS
	{
		get
		{
			this.GetBaseDataDamagePlane();
			int level = this.Level;
			return Mathf.RoundToInt(this.MultiplierNextRank * (((float)this.baseMainDamage + 0.05f * (float)this.baseMainDamage * (float)(level - 1)) / this.fireRateMainGun * (float)this.numberBulletMainGun * this.accurateMainGun + ((float)this.baseSubDamage + 0.05f * (float)this.baseSubDamage * (float)(level - 1)) / this.fireRateSubGun * (float)this.numberBulletSubGun * this.accurateSubGun));
		}
	}

	private void GetBaseDataDamagePlane()
	{
		if (this.baseMainDamage == 0)
		{
			this.baseMainDamage = this.BaseMainDamageLVL1;
		}
		if (this.baseSubDamage == 0)
		{
			this.baseSubDamage = this.BaseSubDamageLVL1;
		}
		switch (this.plane)
		{
		case GameContext.Plane.BataFD01:
			this.fireRateMainGun = ShipBataFD01Sheet.Get(this.indexPower).mainFireRate;
			this.numberBulletMainGun = ShipBataFD01Sheet.Get(this.indexPower).numberBulletMainGun;
			this.accurateMainGun = ShipBataFD01Sheet.Get(this.indexPower).accurateMainGun;
			this.fireRateSubGun = ShipBataFD01Sheet.Get(this.indexPower).subFireRate;
			this.numberBulletSubGun = ShipBataFD01Sheet.Get(this.indexPower).numberBulletSubGun1;
			this.accurateSubGun = ShipBataFD01Sheet.Get(this.indexPower).accurateSubGun1;
			break;
		case GameContext.Plane.FuryOfAres:
			this.fireRateMainGun = ShipFuryOfAresSheet.Get(this.indexPower).mainFireRate;
			this.numberBulletMainGun = ShipFuryOfAresSheet.Get(this.indexPower).numberBulletMainGun;
			this.accurateMainGun = ShipFuryOfAresSheet.Get(this.indexPower).accurateMainGun;
			this.fireRateSubGun = ShipFuryOfAresSheet.Get(this.indexPower).subFireRate;
			this.numberBulletSubGun = ShipFuryOfAresSheet.Get(this.indexPower).numberBulletSubGun1;
			this.accurateSubGun = ShipFuryOfAresSheet.Get(this.indexPower).accurateSubGun1;
			break;
		case GameContext.Plane.SkyWraith:
			this.fireRateMainGun = ShipSkywraithSheet.Get(this.indexPower).mainFireRate;
			this.numberBulletMainGun = ShipSkywraithSheet.Get(this.indexPower).numberBulletMainGun;
			this.accurateMainGun = ShipSkywraithSheet.Get(this.indexPower).accurateMainGun;
			this.fireRateSubGun = ShipSkywraithSheet.Get(this.indexPower).subFireRate;
			this.numberBulletSubGun = ShipSkywraithSheet.Get(this.indexPower).numberBulletSubGun1;
			this.accurateSubGun = ShipSkywraithSheet.Get(this.indexPower).accurateSubGun1;
			break;
		case GameContext.Plane.TwilightX:
			this.fireRateMainGun = ShipTwilightXSheet.Get(this.indexPower).mainFireRate;
			this.numberBulletMainGun = ShipTwilightXSheet.Get(this.indexPower).numberBulletMainGun;
			this.accurateMainGun = ShipTwilightXSheet.Get(this.indexPower).accurateMainGun;
			this.fireRateSubGun = ShipTwilightXSheet.Get(this.indexPower).subFireRate;
			this.numberBulletSubGun = ShipTwilightXSheet.Get(this.indexPower).numberBulletSubGun1;
			this.accurateSubGun = ShipTwilightXSheet.Get(this.indexPower).accurateSubGun1;
			break;
		case GameContext.Plane.Greataxe:
			this.fireRateMainGun = ShipGreataxeSheet.Get(this.indexPower).mainFireRate;
			this.numberBulletMainGun = ShipGreataxeSheet.Get(this.indexPower).numberBulletMainGun;
			this.accurateMainGun = ShipGreataxeSheet.Get(this.indexPower).accurateMainGun;
			this.fireRateSubGun = ShipGreataxeSheet.Get(this.indexPower).subFireRate;
			this.numberBulletSubGun = ShipGreataxeSheet.Get(this.indexPower).numberBulletSubGun1;
			this.accurateSubGun = ShipGreataxeSheet.Get(this.indexPower).accurateSubGun1;
			break;
		case GameContext.Plane.SSLightning:
			this.fireRateMainGun = ShipSSLightningSheet.Get(this.indexPower).mainFireRate;
			this.numberBulletMainGun = ShipSSLightningSheet.Get(this.indexPower).numberBulletMainGun;
			this.accurateMainGun = ShipSSLightningSheet.Get(this.indexPower).accurateMainGun;
			this.fireRateSubGun = ShipSSLightningSheet.Get(this.indexPower).subFireRate;
			this.numberBulletSubGun = ShipSSLightningSheet.Get(this.indexPower).numberBulletSubGun1;
			this.accurateSubGun = ShipSSLightningSheet.Get(this.indexPower).accurateSubGun1;
			break;
		case GameContext.Plane.Warlock:
			this.fireRateMainGun = ShipWarlockSheet.Get(this.indexPower).mainFireRate;
			this.numberBulletMainGun = ShipWarlockSheet.Get(this.indexPower).numberBulletMainGun;
			this.accurateMainGun = ShipWarlockSheet.Get(this.indexPower).accurateMainGun;
			this.fireRateSubGun = ShipWarlockSheet.Get(this.indexPower).subFireRate;
			this.numberBulletSubGun = ShipWarlockSheet.Get(this.indexPower).numberBulletSubGun1;
			this.accurateSubGun = ShipWarlockSheet.Get(this.indexPower).accurateSubGun1;
			break;
		}
	}

	public GameContext.ModeLevel ModeLevelUnlock
	{
		get
		{
			string modeLevelUnlock = PlaneDataSheet.Get((int)this.plane).modeLevelUnlock;
			if (modeLevelUnlock == GameContext.ModeLevel.Easy.ToString())
			{
				this.mode = GameContext.ModeLevel.Easy;
			}
			else if (modeLevelUnlock == GameContext.ModeLevel.Normal.ToString())
			{
				this.mode = GameContext.ModeLevel.Normal;
			}
			else if (modeLevelUnlock == GameContext.ModeLevel.Hard.ToString())
			{
				this.mode = GameContext.ModeLevel.Hard;
			}
			return this.mode;
		}
	}

	public int LevelUnlock
	{
		get
		{
			return PlaneDataSheet.Get((int)this.plane).levelUnlock;
		}
	}

	public string BaseRank
	{
		get
		{
			return PlaneDataSheet.Get((int)this.plane).rank;
		}
	}

	public int priceCoin
	{
		get
		{
			return PlaneDataSheet.Get((int)this.plane).priceCoin;
		}
	}

	public int priceGem
	{
		get
		{
			return PlaneDataSheet.Get((int)this.plane).priceGem;
		}
	}

	public int Vip
	{
		get
		{
			return PlaneDataSheet.Get((int)this.plane).vipToGet;
		}
	}

	public int NumberCardToEvolve
	{
		get
		{
			string currentRank = this.CurrentRank;
			if (currentRank == GameContext.Rank.C.ToString())
			{
				return PlaneDataMaxSheet.Get(1).cardEvovle;
			}
			if (currentRank == GameContext.Rank.B.ToString())
			{
				return PlaneDataMaxSheet.Get(2).cardEvovle;
			}
			if (currentRank == GameContext.Rank.A.ToString())
			{
				return PlaneDataMaxSheet.Get(3).cardEvovle;
			}
			if (currentRank == GameContext.Rank.S.ToString())
			{
				return PlaneDataMaxSheet.Get(4).cardEvovle;
			}
			if (currentRank == GameContext.Rank.SS.ToString())
			{
				return PlaneDataMaxSheet.Get(5).cardEvovle;
			}
			if (currentRank == GameContext.Rank.SSS.ToString())
			{
				return PlaneDataMaxSheet.Get(5).cardEvovle;
			}
			return 0;
		}
	}

	public int MaxLevel
	{
		get
		{
			string currentRank = this.CurrentRank;
			if (currentRank == GameContext.Rank.C.ToString())
			{
				return PlaneDataMaxSheet.Get(0).maxLevel;
			}
			if (currentRank == GameContext.Rank.B.ToString())
			{
				return PlaneDataMaxSheet.Get(1).maxLevel;
			}
			if (currentRank == GameContext.Rank.A.ToString())
			{
				return PlaneDataMaxSheet.Get(2).maxLevel;
			}
			if (currentRank == GameContext.Rank.S.ToString())
			{
				return PlaneDataMaxSheet.Get(3).maxLevel;
			}
			if (currentRank == GameContext.Rank.SS.ToString())
			{
				return PlaneDataMaxSheet.Get(4).maxLevel;
			}
			if (currentRank == GameContext.Rank.SSS.ToString())
			{
				return PlaneDataMaxSheet.Get(5).maxLevel;
			}
			return 0;
		}
	}

	public int NextMaxLevel
	{
		get
		{
			string currentRank = this.CurrentRank;
			if (currentRank == GameContext.Rank.C.ToString())
			{
				return PlaneDataMaxSheet.Get(1).maxLevel;
			}
			if (currentRank == GameContext.Rank.B.ToString())
			{
				return PlaneDataMaxSheet.Get(2).maxLevel;
			}
			if (currentRank == GameContext.Rank.A.ToString())
			{
				return PlaneDataMaxSheet.Get(3).maxLevel;
			}
			if (currentRank == GameContext.Rank.S.ToString())
			{
				return PlaneDataMaxSheet.Get(4).maxLevel;
			}
			if (currentRank == GameContext.Rank.SS.ToString())
			{
				return PlaneDataMaxSheet.Get(5).maxLevel;
			}
			return 0;
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

	public float MultiplierNextRank
	{
		get
		{
			string currentRank = this.CurrentRank;
			if (currentRank != GameContext.Rank.SSS.ToString())
			{
				int rankID = DataGame.Current.ConverRankPlaneToIndex(this.CurrentRank);
				return PlaneDataMaxSheet.Get(rankID).multiPlier;
			}
			return PlaneDataMaxSheet.Get(6).multiPlier;
		}
	}

	public float DurationSkill
	{
		get
		{
			return PlaneDataSheet.Get((int)this.plane).durationSkill;
		}
	}

	public int Level
	{
		get
		{
			return CacheGame.GetCurrentLevelPlane(this.plane);
		}
		set
		{
			CacheGame.SetLevelPlane(this.plane, value);
		}
	}

	public int CoinUpgrade
	{
		get
		{
			return PlaneCostUpgradeSheet.Get(this.Level).costCoin;
		}
	}

	public int GemUpgrade
	{
		get
		{
			return PlaneCostUpgradeSheet.Get(this.Level).costGem;
		}
	}

	public int CardEvolvePlane
	{
		get
		{
			return PlayerPrefs.GetInt("CardPlane" + this.namePlane, 0);
		}
		set
		{
			PlayerPrefs.SetInt("CardPlane" + this.namePlane, value);
			SaveDataPlane.Instance.planeInfoContainer.arrPlanes[(int)this.plane].cardPlane = value;
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"card plane ",
				this.plane.ToString(),
				" ",
				value
			}));
		}
	}

	public string NameSkill
	{
		get
		{
			if (this.plane == GameContext.Plane.BataFD01)
			{
				return ScriptLocalization.bullet_hell;
			}
			if (this.plane == GameContext.Plane.FuryOfAres)
			{
				return ScriptLocalization.dead_bullet;
			}
			if (this.plane == GameContext.Plane.SkyWraith)
			{
				return ScriptLocalization.rising_sun;
			}
			if (this.plane == GameContext.Plane.Greataxe)
			{
				return ScriptLocalization.hammer_of_the_gods;
			}
			if (this.plane == GameContext.Plane.TwilightX)
			{
				return ScriptLocalization.cruel_angle;
			}
			if (this.plane == GameContext.Plane.SSLightning)
			{
				return ScriptLocalization.god_of_thunder;
			}
			if (this.plane == GameContext.Plane.Warlock)
			{
				return ScriptLocalization.super_homing_missile;
			}
			return string.Empty;
		}
	}

	public string DescriptionSkill
	{
		get
		{
			if (this.plane == GameContext.Plane.BataFD01)
			{
				return string.Empty;
			}
			if (this.plane == GameContext.Plane.FuryOfAres)
			{
				return string.Empty;
			}
			if (this.plane == GameContext.Plane.SkyWraith)
			{
				return string.Empty;
			}
			if (this.plane == GameContext.Plane.Greataxe)
			{
				return ScriptLocalization.des_skill_starship_greataxe;
			}
			if (this.plane == GameContext.Plane.TwilightX)
			{
				return ScriptLocalization.des_skill_starship_twilightx;
			}
			if (this.plane == GameContext.Plane.SSLightning)
			{
				return ScriptLocalization.des_skill_starship_sslightning;
			}
			if (this.plane == GameContext.Plane.Warlock)
			{
				return string.Empty;
			}
			return string.Empty;
		}
	}

	public string CurrentRank
	{
		get
		{
			return PlayerPrefs.GetString("Curren Rank " + this.namePlane, this.BaseRank);
		}
		set
		{
			PlayerPrefs.SetString("Curren Rank " + this.namePlane, value);
			SaveDataPlane.Instance.planeInfoContainer.arrPlanes[(int)this.plane].rank = value;
			UnityEngine.Debug.Log("current rank plane " + this.plane.ToString() + " " + value);
		}
	}

	public bool IsOwned
	{
		get
		{
			return CacheGame.IsOwnedPlane(this.plane);
		}
	}

	public bool IsUnlock
	{
		get
		{
			return CacheGame.IsUnlockPlane(this.plane);
		}
	}

	public GameContext.Plane plane;

	[Header("Properties")]
	public string namePlane;

	private float fireRateMainGun;

	private float accurateMainGun;

	private float fireRateSubGun;

	private float accurateSubGun;

	private int numberBulletMainGun;

	private int numberBulletSubGun;

	private int baseMainDamage;

	private int baseSubDamage;

	[Header("Level Unlock")]
	private GameContext.ModeLevel mode;

	[Header("Price")]
	public bool isOnlyGem;
}
