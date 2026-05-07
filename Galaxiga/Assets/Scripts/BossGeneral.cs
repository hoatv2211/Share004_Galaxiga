using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using PathologicalGames;
using SkyGameKit;
using UnityEngine;

public class BossGeneral : BaseEnemy
{
	public override int CurrentHP
	{
		get
		{
			return base.CurrentHP;
		}
		set
		{
			if (!this.noHit)
			{
				base.CurrentHP = value;
			}
			float num = 1f;
			if ((float)this.CurrentHP * 1f / (float)this.startHP < 1f)
			{
				num = (float)this.CurrentHP * 1f / (float)this.startHP;
			}
			if ((double)num < 1.0 && this.CurrentHP > 0)
			{
				this.HitBossEvent();
			}
			if (num < 0f)
			{
				num = 0f;
			}
			this.healthBar.transform.localScale = new Vector3(num, this.healthBar.transform.localScale.y, 0f);
		}
	}

	[EnemyAction(displayName = "Action/Không Mất Máu")]
	public void NotHit(float time)
	{
		this.noHit = true;
		base.StartCoroutine(this.NotHitIE(time));
	}

	private IEnumerator NotHitIE(float time)
	{
		yield return new WaitForSeconds(time);
		this.noHit = false;
		yield return null;
		yield break;
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		if (type == EnemyKilledBy.Player)
		{
			EazySoundManager.PlaySound(AudioCache.Sound.bossdie);
		}
		if (this.winMap)
		{
			this.waveControl.GetComponent<WaveControl>().StartShowWin();
		}
		if (SgkSingleton<FreeWave>.Instance != null)
		{
			FreeWave.StopAll();
		}
		if (base.MotherTurn.transform.parent.GetComponent<FreeWave>() == null && this.waveControl.spawItemNumberDrop)
		{
			for (int i = 0; i < this.waveControl.numberDrop; i++)
			{
				float num = (float)UnityEngine.Random.Range(0, 100);
				if (num < this.waveControl.percentSpawnItemEvent)
				{
					this.waveControl.addNumberDrop++;
					if (this.waveControl.addNumberDrop <= this.waveControl.numberDrop)
					{
						PoolManager.Pools["ItemPool"].Spawn(this.waveControl.itemSpawnDrop, base.transform.position, Quaternion.identity);
					}
				}
			}
		}
		if (SgkSingleton<LevelInfo>.Instance.mode == LevelInfo.modeLevel.EndLess)
		{
			SaveDataQuestEndless.SetProcessQuest(EndlessDailyQuestManager.Quest.defeat_boss0, 1);
		}
	}

	public void CheckRatioHealthBoss()
	{
	}

	public override void Restart()
	{
		if (base.MotherTurn.transform.parent.GetComponent<FreeWave>() == null)
		{
			this.waveControl = base.MotherTurn.MotherWave.GetComponent<WaveControl>();
		}
		if (SgkSingleton<LevelInfo>.Instance.mode == LevelInfo.modeLevel.Campain)
		{
			this.CurrentHP = (int)((float)BaseEnemyDataSheet.Get(this.id).healthEnemy * LevelHardEnemySheet.Get(SgkSingleton<LevelInfo>.Instance.numHard).percentHealth * LevelEnemySheet.Get(SgkSingleton<LevelInfo>.Instance.numPlanet).percentHealth * WaveEnemySheet.Get(SgkSingleton<LevelManager>.Instance.CurrentSequenceWaveIndex + 1).percentHeath);
		}
		if (SgkSingleton<LevelInfo>.Instance.mode == LevelInfo.modeLevel.EndLess)
		{
			GalagaEndlessLevel galagaEndlessLevel = SgkSingleton<LevelManager>.Instance as GalagaEndlessLevel;
			if (galagaEndlessLevel != null)
			{
				int num = GalagaEndlessLevel.zigZagStartIndex * 10 + SgkSingleton<LevelManager>.Instance.CurrentSequenceWaveIndex + 1;
				if (galagaEndlessLevel.CurrentZigZagIndex < 1)
				{
					this.CurrentHP = (int)((float)BaseEnemyDataSheet.Get(this.id).healthEnemy * LevelEnemySheet.Get(num / 10 + 1).percentHealth * WaveEnemySheet.Get(SgkSingleton<LevelManager>.Instance.CurrentSequenceWaveIndex + 1).percentHeath);
				}
				else
				{
					this.CurrentHP = (int)((float)BaseEnemyDataSheet.Get(this.id).healthEnemy * LevelEnemySheet.Get(num / 10 + 1).percentHealth * WaveEnemySheet.Get(9).percentHeath);
				}
			}
		}
		this.startHP = this.CurrentHP;
		base.Restart();
		if (this.LoopEvent != null)
		{
			this.LoopEvent.ForEach(delegate(EnemyEventUnit<float> x)
			{
				this.DoActionEveryTime(x.param, new Action(x.Invoke), false, false);
			});
		}
	}

	public void HitBossEvent()
	{
		if (this.healthPercentBoss != null)
		{
			this.healthPercentBoss.ForEach(delegate(EnemyEventUnit<float> x)
			{
				if ((float)this.CurrentHP * 1f * 100f / (float)this.startHP < x.param)
				{
					x.param = -1f;
					x.Invoke();
				}
			});
		}
		if (!this.checkEventLoop && (float)this.CurrentHP * 1f * 100f / (float)this.startHP < this.percentHealthChangeTurn)
		{
			this.checkEventLoop = true;
			if (this.HealthEvent != null)
			{
				this.HealthEvent.ForEach(delegate(EnemyEventUnit<float> x)
				{
					this.DoActionEveryTime(x.param, new Action(x.Invoke), false, false);
				});
			}
		}
	}

	public bool CheckHealthAction(float healthCheck)
	{
		bool result = false;
		if (healthCheck == 0f)
		{
			result = true;
		}
		float num = (float)this.CurrentHP * 1f * 100f / (float)this.startHP;
		if (num <= healthCheck)
		{
			result = true;
		}
		return result;
	}

	public void CheckAction()
	{
		this.numberAct++;
		bool check = false;
		this.numberTurn.ForEach(delegate(EnemyEventUnit<int> x)
		{
			if (this.numberAct == x.param)
			{
				check = true;
				x.Invoke();
			}
		});
		if (!check)
		{
			this.numberAct = 0;
			this.CheckAction();
		}
	}

	[EnemyAction(displayName = "Enemy/Tăng Máu")]
	public void SetHealth(float ratio)
	{
		this.CurrentHP = (int)((float)this.CurrentHP * ratio);
		this.startHP = this.CurrentHP;
	}

	[EnemyAction(displayName = "Item/Item Power Up")]
	public void SpawItem(float percent, float xSpawn)
	{
		int num = UnityEngine.Random.Range(0, 100);
		if ((float)num < percent && this.waveControl.addItem < this.waveControl.numberItem)
		{
			this.waveControl.addItem++;
			PoolManager.Pools["ItemPool"].Spawn(this.waveControl.item, new Vector3(base.transform.position.x + xSpawn, base.transform.position.y, base.transform.position.z), Quaternion.identity);
		}
	}

	[EnemyAction(displayName = "Item/Item Super Power")]
	public void SpawItemSuper(float percent, float xSpawn)
	{
		int num = UnityEngine.Random.Range(0, 100);
		if ((float)num < percent && this.waveControl.addItemSuper < this.waveControl.numberItemSuper)
		{
			this.waveControl.addItemSuper++;
			PoolManager.Pools["ItemPool"].Spawn(this.waveControl.itemSuper, new Vector3(base.transform.position.x + xSpawn, base.transform.position.y, base.transform.position.z), Quaternion.identity);
		}
	}

	[EnemyAction(displayName = "Item/Item Plane")]
	public void SpawItemPlane(float percent, float xSpawn)
	{
		int num = UnityEngine.Random.Range(0, 100);
		if ((float)num < percent && this.waveControl.addItemPlane < this.waveControl.numberItemPlane)
		{
			int index = UnityEngine.Random.Range(0, this.waveControl.ListItemPlane.Count);
			this.waveControl.addItemPlane++;
			PoolManager.Pools["ItemPool"].Spawn(this.waveControl.ListItemPlane[index], new Vector3(base.transform.position.x + xSpawn, base.transform.position.y, base.transform.position.z), Quaternion.identity);
			this.waveControl.ListItemPlane.Remove(this.waveControl.ListItemPlane[index]);
		}
	}

	[EnemyAction(displayName = "Item/CoinGem")]
	public void SpawnGemCoin(float percentSpawn, bool ramdomNumberCoin, int numberCoin, float randomXSpaw, float randomYSpaw, bool randomItem, int type)
	{
		float num = (float)UnityEngine.Random.Range(0, 100);
		if (num < percentSpawn && this.waveControl.ListCoinGem != null)
		{
			if (ramdomNumberCoin)
			{
				numberCoin = UnityEngine.Random.Range(1, numberCoin);
			}
			for (int i = 0; i < numberCoin; i++)
			{
				Vector3 pos = new Vector3(UnityEngine.Random.Range(base.transform.position.x - randomXSpaw, base.transform.position.x + randomXSpaw), UnityEngine.Random.Range(base.transform.position.y - randomYSpaw, base.transform.position.y + randomYSpaw), base.transform.position.z);
				if (randomItem)
				{
					type = UnityEngine.Random.Range(1, this.waveControl.ListCoinGem.Count + 1);
				}
				PoolManager.Pools["ItemPool"].Spawn(this.waveControl.ListCoinGem[type - 1], pos, Quaternion.identity);
			}
		}
		SgkSingleton<LevelInfo>.Instance.totalcoin += numberCoin;
	}

	[EnemyAction(displayName = "BossEvent/Item Event")]
	public void SpawItemEvent(float percent, float xSpawn)
	{
		if (this.waveControl.itemSpawnDrop != null)
		{
			while (this.waveControl.addNumberDrop < this.waveControl.numberDrop)
			{
				PoolManager.Pools["ItemPool"].Spawn(this.waveControl.itemSpawnDrop, base.transform.position, Quaternion.identity);
				this.waveControl.addNumberDrop = this.waveControl.addNumberDrop++;
			}
		}
	}

	[Header("HEALTH_BOSS")]
	public GameObject parenthealthBar;

	public GameObject healthBar;

	private WaveControl waveControl;

	public List<PartOfBoss> targetsList = new List<PartOfBoss>();

	[EnemyEventCustom(displayName = "Boss/Mất Máu", paramName = "Phần Trăm Máu")]
	public EnemyEvent<float> healthPercentBoss = new EnemyEvent<float>();

	[EnemyEventCustom(displayName = "Boss/Action", paramName = "Thứ Tự Turn")]
	public EnemyEvent<int> numberTurn = new EnemyEvent<int>();

	[EnemyEventCustom(displayName = "Boss/Lặp từ khi bắt đầu turn", paramName = "time")]
	public EnemyEvent<float> LoopEvent = new EnemyEvent<float>();

	[EnemyEventCustom(displayName = "Boss/Lặp theo % máu", paramName = "time")]
	public EnemyEvent<float> HealthEvent = new EnemyEvent<float>();

	private int numberAct;

	[Header("WIN_MAP")]
	[EnemyField]
	public bool winMap;

	public float percentHealthChangeTurn = 50f;

	private bool checkEventLoop;

	private bool noHit;
}
