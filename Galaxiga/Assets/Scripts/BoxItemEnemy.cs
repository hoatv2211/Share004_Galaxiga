using System;
using DG.Tweening;
using PathologicalGames;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

public class BoxItemEnemy : FlyShape
{
	public override void Restart()
	{
		if (base.MotherTurn.transform.parent.GetComponent<FreeWave>() == null)
		{
			this.waveControl = base.MotherTurn.MotherWave.GetComponent<WaveControl>();
		}
		this.spawnItem = false;
		if (SgkSingleton<LevelInfo>.Instance.mode == LevelInfo.modeLevel.Campain)
		{
			this.CurrentHP = (int)((float)BaseEnemyDataSheet.Get(this.id).healthEnemy * LevelHardEnemySheet.Get(SgkSingleton<LevelInfo>.Instance.numHard).percentHealth * LevelEnemySheet.Get(SgkSingleton<LevelInfo>.Instance.numPlanet).percentHealth * WaveEnemySheet.Get(SgkSingleton<LevelManager>.Instance.CurrentSequenceWaveIndex + 1).percentHeath);
		}
		else if (SgkSingleton<LevelInfo>.Instance.mode == LevelInfo.modeLevel.EndLess)
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
		if (this.stRotateAvatar)
		{
			this.avatarItem.rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-360f, 360f));
		}
		base.Restart();
	}

	[EnemyAction(displayName = "Item/Item Power Up")]
	public void SpawItem(float percent)
	{
		if (this.waveControl.item != null && GameContext.power < 10 && !this.spawnItem)
		{
			int num = UnityEngine.Random.Range(0, 100);
			if ((float)num < percent && this.waveControl.addItem < this.waveControl.numberItem)
			{
				this.waveControl.addItem++;
				PoolManager.Pools["ItemPool"].Spawn(this.waveControl.item, base.transform.position, Quaternion.identity);
				this.spawnItem = true;
			}
		}
	}

	[EnemyAction(displayName = "Item/Item Super Power")]
	public void SpawItemSuper(float percent)
	{
		if (this.waveControl.itemSuper != null && !this.spawnItem)
		{
			int num = UnityEngine.Random.Range(0, 100);
			if ((float)num < percent && this.waveControl.addItemSuper < this.waveControl.numberItemSuper)
			{
				this.waveControl.addItemSuper++;
				PoolManager.Pools["ItemPool"].Spawn(this.waveControl.itemSuper, base.transform.position, Quaternion.identity);
				this.spawnItem = true;
			}
		}
	}

	[EnemyAction(displayName = "Item/Item Plane")]
	public void SpawItemPlane(float percent)
	{
		if (!this.spawnItem && this.waveControl.ListItemPlane != null)
		{
			int index = UnityEngine.Random.Range(0, this.waveControl.ListItemPlane.Count);
			int num = UnityEngine.Random.Range(0, 100);
			if ((float)num < percent && this.waveControl.addItemPlane < this.waveControl.numberItemPlane)
			{
				if (this.waveControl.spawnPlaneOnList)
				{
					if (this.waveControl.ListItemPlane[index].GetComponent<ItemChangePlane>().plane != PlaneIngameManager.current.currenPlaneActive)
					{
						this.waveControl.addItemPlane++;
						PoolManager.Pools["ItemPool"].Spawn(this.waveControl.ListItemPlane[index], base.transform.position, Quaternion.identity);
						this.waveControl.ListItemPlane.Remove(this.waveControl.ListItemPlane[index]);
						this.spawnItem = true;
					}
				}
				else
				{
					index = UnityEngine.Random.Range(0, DataGame.Current.ListPlaneOwned.Count);
					for (int i = 0; i < this.waveControl.ListItemPlane.Count; i++)
					{
						if (this.waveControl.ListItemPlane[i] != null && this.waveControl.ListItemPlane[i].GetComponent<ItemChangePlane>().plane == DataGame.Current.ListPlaneOwned[index] && this.waveControl.ListItemPlane[i].GetComponent<ItemChangePlane>().plane != PlaneIngameManager.current.currenPlaneActive)
						{
							this.waveControl.addItemPlane++;
							PoolManager.Pools["ItemPool"].Spawn(this.waveControl.ListItemPlane[i], base.transform.position, Quaternion.identity);
							this.spawnItem = true;
							break;
						}
					}
				}
			}
		}
	}

	[EnemyAction(displayName = "Item/CoinGem")]
	public void SpawnGemCoin(float percentSpawn, bool ramdomNumberCoin, int numberCoin, bool randomItem, int type)
	{
		float num = (float)UnityEngine.Random.Range(0, 100);
		if (num < percentSpawn && this.waveControl.ListCoinGem != null)
		{
			if (ramdomNumberCoin)
			{
				numberCoin = UnityEngine.Random.Range(1, numberCoin);
			}
			if (base.transform.localScale.x >= 1f)
			{
				numberCoin = (int)((float)numberCoin * base.transform.localScale.x);
			}
			else
			{
				numberCoin = (int)((float)numberCoin * base.transform.localScale.x) / 2;
			}
			for (int i = 0; i < numberCoin; i++)
			{
				if (randomItem)
				{
					type = UnityEngine.Random.Range(1, this.waveControl.ListCoinGem.Count + 1);
				}
				if (type > 0)
				{
					PoolManager.Pools["ItemPool"].Spawn(this.waveControl.ListCoinGem[type - 1], base.transform.position, Quaternion.identity);
				}
			}
			SgkSingleton<LevelInfo>.Instance.totalcoin += numberCoin;
		}
	}

	[EnemyAction(displayName = "Enemy/Tăng Máu")]
	public void SetHealth(float ratio)
	{
		this.CurrentHP = (int)((float)this.CurrentHP * ratio);
	}

	protected override void ClearData()
	{
		base.transform.DOKill(false);
		base.ClearData();
	}

	public WaveControl waveControl;

	private bool spawnItem;

	public bool stRotateAvatar;

	[ShowIf("stRotateAvatar", true)]
	public Transform avatarItem;

	public bool spawnCoin;
}
