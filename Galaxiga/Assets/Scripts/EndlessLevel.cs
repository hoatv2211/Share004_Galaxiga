using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

public class EndlessLevel : LevelManager
{
	public int CurrentZigZagIndex { get; private set; } = -1;

	private int OffsetIndex
	{
		get
		{
			if (this.CurrentZigZagIndex >= this.zigzags.Length)
			{
				return this.CurrentZigZagIndex - this.zigzags.Length + 1;
			}
			return 0;
		}
	}

	public EndlessLevel.ZigZagConfig CurrentZigZag
	{
		[CompilerGenerated]
		get
		{
			return this.zigzags[Mathf.Clamp(this.CurrentZigZagIndex, 0, this.zigzags.Length - 1)];
		}
	}

	protected override IEnumerator Start()
	{
		yield return this.StartWaveCoroutine();
		yield return base.Start();
		yield break;
	}

	private IEnumerator StartWaveCoroutine()
	{
		yield return this.MakeNewWave();
		this.OnNextSequenceWave = (Action<SequenceWave>)Delegate.Combine(this.OnNextSequenceWave, new Action<SequenceWave>(delegate(SequenceWave x)
		{
			if (base.CurrentSequenceWaveIndex == base.listSequenceWave.Count)
			{
				base.StartCoroutine(this.MakeNewWave());
			}
		}));
		yield break;
	}

	private IEnumerator MakeNewWave()
	{
		this.CurrentZigZagIndex++;
		MonoBehaviour.print(this.CurrentZigZagIndex);
		int[] zigZagArray = this.CurrentZigZag.GetZigZagArray;
		for (int i = 0; i < zigZagArray.Length; i++)
		{
			if (zigZagArray[i] >= this.listOfWaves.Length)
			{
				SgkLog.LogError(string.Concat(new object[]
				{
					"Phần tử Zigzag ",
					this.CurrentZigZagIndex,
					" chứa waves ",
					zigZagArray[i],
					" vượt quá giới hạn"
				}));
			}
			else
			{
				SequenceWave newWave = this.listOfWaves[zigZagArray[i]].GetRandomWave();
				newWave = UnityEngine.Object.Instantiate<GameObject>(newWave.gameObject, base.transform).GetComponent<SequenceWave>();
				base.AddSequenceWave(newWave);
				yield return null;
			}
		}
		yield break;
	}

	public override void OnEnemySpawned(BaseEnemy enemy)
	{
		base.OnEnemySpawned(enemy);
		enemy.CurrentHP = Mathf.RoundToInt((this.CurrentZigZag.hpX + this.lastHpX * (float)this.OffsetIndex) * (float)enemy.CurrentHP);
	}

	public override void OnStartTurn(TurnManager turn)
	{
		base.OnStartTurn(turn);
		ITurnCanIntegrateEndless turnCanIntegrateEndless = turn as ITurnCanIntegrateEndless;
		if (turnCanIntegrateEndless != null)
		{
			turnCanIntegrateEndless.NumberOfEnemySelected = Mathf.RoundToInt((this.CurrentZigZag.numberX + this.lastNumberX * (float)this.OffsetIndex) * (float)turnCanIntegrateEndless.NumberOfEnemySelected);
			turnCanIntegrateEndless.TimeToNextAction /= this.CurrentZigZag.timeX + this.lastTimeX * (float)this.OffsetIndex;
		}
	}

	[ListDrawerSettings(ShowIndexLabels = true)]
	public EndlessLevel.WaveSelector[] listOfWaves;

	[ListDrawerSettings(ShowIndexLabels = true)]
	public EndlessLevel.ZigZagConfig[] zigzags;

	public float lastHpX = 0.3f;

	public float lastNumberX = 0.3f;

	public float lastTimeX = 0.3f;

	[Serializable]
	public class WaveSelector
	{
		public SequenceWave GetRandomWave()
		{
			if (!this.shuffled)
			{
				this.waves = (from x in this.waves
				orderby Fu.RandomInt
				select x).ToArray<SequenceWave>();
				this.shuffled = true;
			}
			return this.waves[Mathf.Abs(this.repeatCount++) % this.waves.Length];
		}

		public SequenceWave[] waves;

		private bool shuffled;

		private int repeatCount;
	}

	[Serializable]
	public class ZigZagConfig
	{
		public int[] GetZigZagArray
		{
			[CompilerGenerated]
			get
			{
				IEnumerable<string> source = this.zigzag.Split(new char[]
				{
					';',
					',',
					' '
				}, StringSplitOptions.RemoveEmptyEntries);
				if (EndlessLevel.ZigZagConfig._003C_003Ef__mg_0024cache0 == null)
				{
					EndlessLevel.ZigZagConfig._003C_003Ef__mg_0024cache0 = new Func<string, int>(int.Parse);
				}
				return source.Select(EndlessLevel.ZigZagConfig._003C_003Ef__mg_0024cache0).ToArray<int>();
			}
		}

		public string zigzag;

		public float hpX = 1f;

		public float numberX = 1f;

		public float timeX = 1f;

		[CompilerGenerated]
		private static Func<string, int> _003C_003Ef__mg_0024cache0;
	}
}
