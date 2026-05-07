using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

public class GalagaEndlessLevel : LevelManager
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

	public int CurrentZigZagRealIndex
	{
		get
		{
			if (this.beginLoopIndex >= this.zigzags.Length)
			{
				UnityEngine.Debug.LogError("beginLoopIndex >= zigzags.Length, tự động gán beginLoopIndex = 0");
				this.beginLoopIndex = 0;
			}
			int result;
			if (this.CurrentZigZagIndex < this.zigzags.Length)
			{
				result = this.CurrentZigZagIndex;
			}
			else
			{
				result = this.beginLoopIndex + (this.CurrentZigZagIndex - this.beginLoopIndex) % (this.zigzags.Length - this.beginLoopIndex);
			}
			return result;
		}
	}

	public GalagaEndlessLevel.ZigZagConfig CurrentZigZag
	{
		[CompilerGenerated]
		get
		{
			return this.zigzags[this.CurrentZigZagRealIndex];
		}
	}

	protected override IEnumerator Start()
	{
		yield return this.CreateStartWaveCoroutine();
		yield return base.Start();
		yield break;
	}

	private IEnumerator CreateStartWaveCoroutine()
	{
		this.CurrentZigZagIndex = GalagaEndlessLevel.zigZagStartIndex - 1;
		yield return this.CreateRandomWave();
		this.OnNextSequenceWave = (Action<SequenceWave>)Delegate.Combine(this.OnNextSequenceWave, new Action<SequenceWave>(delegate(SequenceWave x)
		{
			if (base.CurrentSequenceWaveIndex == base.listSequenceWave.Count)
			{
				base.StartCoroutine(this.CreateRandomWave());
			}
		}));
		yield break;
	}

	private IEnumerator CreateRandomWave()
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
					" chứa wave ",
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

	[BoxGroup]
	[ListDrawerSettings(ShowIndexLabels = true)]
	public GalagaEndlessLevel.WaveSelector[] listOfWaves;

	public static int zigZagStartIndex;

	[ListDrawerSettings(ShowIndexLabels = true)]
	public GalagaEndlessLevel.ZigZagConfig[] zigzags;

	public int beginLoopIndex;

	[Serializable]
	public class WaveSelector
	{
		public SequenceWave GetRandomWave()
		{
			if (this.getRunCount == 0)
			{
				this.waves = (from x in this.waves
				orderby Fu.RandomInt
				select x).ToArray<SequenceWave>();
			}
			SequenceWave result = this.waves[this.getRunCount];
			this.getRunCount++;
			if (this.getRunCount >= this.waves.Length)
			{
				this.getRunCount = 0;
			}
			return result;
		}

		public SequenceWave[] waves;

		private int getRunCount;
	}

	[Serializable]
	public class ZigZagConfig
	{
		public int[] GetZigZagArray
		{
			get
			{
				int[] array2;
				if (this.zigzag.Contains('x'))
				{
					IEnumerable<string> source = this.zigzag.Split(new char[]
					{
						'x'
					}, StringSplitOptions.RemoveEmptyEntries);
					if (GalagaEndlessLevel.ZigZagConfig._003C_003Ef__mg_0024cache0 == null)
					{
						GalagaEndlessLevel.ZigZagConfig._003C_003Ef__mg_0024cache0 = new Func<string, int>(int.Parse);
					}
					int[] array = source.Select(GalagaEndlessLevel.ZigZagConfig._003C_003Ef__mg_0024cache0).ToArray<int>();
					array2 = new int[array[1]];
					for (int i = 0; i < array[1]; i++)
					{
						array2[i] = array[0];
					}
				}
				else
				{
					IEnumerable<string> source2 = this.zigzag.Split(new char[]
					{
						';',
						',',
						' '
					}, StringSplitOptions.RemoveEmptyEntries);
					if (GalagaEndlessLevel.ZigZagConfig._003C_003Ef__mg_0024cache1 == null)
					{
						GalagaEndlessLevel.ZigZagConfig._003C_003Ef__mg_0024cache1 = new Func<string, int>(int.Parse);
					}
					array2 = source2.Select(GalagaEndlessLevel.ZigZagConfig._003C_003Ef__mg_0024cache1).ToArray<int>();
				}
				if (this.zigzag.Length == 0)
				{
					UnityEngine.Debug.LogError("Lỗi khi parse zigzag: " + this.zigzag);
					return null;
				}
				return array2;
			}
		}

		public string zigzag;

		[CompilerGenerated]
		private static Func<string, int> _003C_003Ef__mg_0024cache0;

		[CompilerGenerated]
		private static Func<string, int> _003C_003Ef__mg_0024cache1;
	}
}
