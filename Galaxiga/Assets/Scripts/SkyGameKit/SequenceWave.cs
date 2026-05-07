using System;

namespace SkyGameKit
{
	public class SequenceWave : WaveManager
	{
		public override void EndWave()
		{
			base.EndWave();
			SgkSingleton<LevelManager>.Instance.NextSequenceWave(1);
		}
	}
}
