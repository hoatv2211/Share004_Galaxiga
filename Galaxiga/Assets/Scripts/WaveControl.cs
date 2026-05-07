using System;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

public class WaveControl : SequenceWave
{
	public override void EndWave()
	{
		base.EndWave();
		if (SgkSingleton<LevelInfo>.Instance.mode != LevelInfo.modeLevel.EndLess)
		{
			if (SgkSingleton<LevelManager>.Instance.CurrentSequenceWaveIndex >= SgkSingleton<LevelManager>.Instance.listSequenceWave.Count)
			{
				this.StartShowWin();
			}
		}
		else if (SgkSingleton<LevelInfo>.Instance.mode == LevelInfo.modeLevel.EndLess)
		{
			SaveDataQuestEndless.SetProcessQuest(EndlessDailyQuestManager.Quest.fly_through_waves0, 1);
		}
	}

	public void StartShowWin()
	{
		UnityEngine.Debug.Log("StartShowWin");
		PlaneIngameManager.current.SetPlayerImortal();
		base.StartCoroutine(this.ShowWin());
	}

	private IEnumerator ShowWin()
	{
		yield return new WaitForSeconds(this.delayWin / 2f);
		if (GameContext.currentLevel == 1)
		{
			NewTutorial.current.UseSkillPlane_Step5();
		}
		yield return new WaitForSeconds(this.delayWin / 2f);
		UnityEngine.Debug.LogError("<color=#76ff03>Tổng số vàng trong Level: </color>" + SgkSingleton<LevelInfo>.Instance.totalcoin);
		UnityEngine.Debug.LogError("<color=#76ff03>Tổng số vàng đã ăn: </color>" + SgkSingleton<LevelInfo>.Instance.totalcoinadd);
		SgkSingleton<LevelInfo>.Instance.percentCoin = (float)SgkSingleton<LevelInfo>.Instance.totalcoinadd * 1f / (float)SgkSingleton<LevelInfo>.Instance.totalcoin;
		GameContext.percentCoinLevel = SgkSingleton<LevelInfo>.Instance.percentCoin;
		GameScreenManager.current.MissionComplete();
		yield return null;
		yield break;
	}

	public override void StartWave()
	{
		base.StartWave();
		if (this.showWarning)
		{
			this.ShowWarning();
		}
		base.StartCoroutine(this.ShowUI(this.warningTime / 2f));
	}

	private void ShowWarning()
	{
		if (this.ListPoinWarning != null)
		{
			for (int i = 0; i < this.ListPoinWarning.Count; i++)
			{
				if (this.ListPoinWarning[i] != null)
				{
					PoolManager.Pools["ItemPool"].Spawn(this.warning, this.ListPoinWarning[i].position, Quaternion.identity);
				}
				else
				{
					UnityEngine.Debug.LogError("<color=#76ff03>Thiếu warning thứ: </color>" + i);
				}
			}
		}
	}

	private IEnumerator ShowUI(float timedelay)
	{
		yield return new WaitForSeconds(timedelay);
		if (SgkSingleton<LevelInfo>.Instance.mode == LevelInfo.modeLevel.Campain)
		{
			if (SgkSingleton<LevelManager>.Instance.CurrentSequenceWaveIndex + 1 <= SgkSingleton<LevelManager>.Instance.listSequenceWave.Count)
			{
				UIGameManager.current.ShowNextWave(SgkSingleton<LevelManager>.Instance.CurrentSequenceWaveIndex + 1, SgkSingleton<LevelManager>.Instance.listSequenceWave.Count);
			}
		}
		else if (SgkSingleton<LevelInfo>.Instance.mode == LevelInfo.modeLevel.EndLess)
		{
			GalagaEndlessLevel galagaEndlessLevel = SgkSingleton<LevelManager>.Instance as GalagaEndlessLevel;
			UIGameManager.current.ShowNextWave(GalagaEndlessLevel.zigZagStartIndex * 10 + SgkSingleton<LevelManager>.Instance.CurrentSequenceWaveIndex + 1);
		}
		yield return null;
		if (this.waveBoss)
		{
			base.StartCoroutine(this.ShowWarningBoss(this.warningTime / 1.2f));
		}
		yield break;
	}

	private IEnumerator ShowWarningBoss(float timedelay)
	{
		yield return new WaitForSeconds(timedelay);
		UIGameManager.current.ShowFxBossWarning();
		yield return null;
		yield break;
	}

	[Header("ITEM")]
	public int numberItem;

	public int addItem;

	public GameObject item;

	[Header("SUPER - ITEM")]
	public int numberItemSuper;

	public int addItemSuper;

	public GameObject itemSuper;

	[Header("ITEM - PLANE")]
	public int numberItemPlane;

	public int addItemPlane;

	public List<GameObject> ListItemPlane = new List<GameObject>();

	public bool spawnPlaneOnList;

	[Header("BOSS")]
	public bool waveBoss;

	[Header("Coin - Gem")]
	public List<GameObject> ListCoinGem = new List<GameObject>();

	public float delayWin = 5f;

	[Header("Item - Number Drop")]
	public bool spawItemNumberDrop;

	[ShowIf("spawItemNumberDrop", true)]
	public int numberDrop;

	[ShowIf("spawItemNumberDrop", true)]
	public int addNumberDrop;

	[ShowIf("spawItemNumberDrop", true)]
	public float percentSpawnItemEvent = 100f;

	[ShowIf("spawItemNumberDrop", true)]
	public GameObject itemSpawnDrop;

	public const string textColor = "#76ff03";

	[Header("Show_Warning")]
	public bool showWarning;

	[ShowIf("showWarning", true)]
	public GameObject warning;

	[ShowIf("showWarning", true)]
	public List<Transform> ListPoinWarning = new List<Transform>();

	public int numberEnemyMove;

	public int maxEnemyMove = 3;
}
