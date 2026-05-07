using System;
using UnityEngine;

namespace SkyGameKit
{
	public class PointWave : WaveManager, IComparable<PointWave>
	{
		public override void EndWave()
		{
			base.EndWave();
			SgkSingleton<LevelManager>.Instance.EndPointWave(this);
		}

		public int CompareTo(PointWave other)
		{
			if (other == null)
			{
				return (int)(base.transform.position.y * 1000f) + 1;
			}
			return (int)(base.transform.position.y - other.transform.position.y * 1000f);
		}

		protected virtual void Update()
		{
			if (base.State != WaveState.Stopped)
			{
				base.transform.position += Vector3.down * Time.deltaTime * PointWave.speedProvider.GetSpeed();
				if (base.transform.position.y < PointWave.topCamera && base.State == WaveState.NotStarted)
				{
					this.StartWave();
				}
			}
		}

		public static float topCamera;

		public static ICanGetSpeed speedProvider;
	}
}
