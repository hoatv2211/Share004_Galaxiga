using System;
using PathologicalGames;
using UnityEngine;

namespace SkyGameKit
{
	public class SgkExplosion : MonoBehaviour
	{
		public virtual void Despawn(float delay)
		{
			this.Delay(delay, delegate
			{
				this.OnAnimationExplosionFinish();
			}, false);
		}

		public virtual void OnAnimationExplosionFinish()
		{
			if (PoolManager.Pools["ExplosionPool"].IsSpawned(base.transform))
			{
				PoolManager.Pools["ExplosionPool"].Despawn(base.transform);
			}
		}

		protected virtual void Update()
		{
			if (this.target != null)
			{
				base.transform.position = this.target.TransformPoint(this.offset);
			}
		}

		protected virtual void OnDespawned()
		{
			this.target = null;
		}

		[HideInInspector]
		public Transform target;

		[HideInInspector]
		public Vector2 offset;
	}
}
