using System;
using PathologicalGames;
using SWS;
using UnityEngine;

namespace SkyGameKit
{
	[RequireComponent(typeof(splineMove))]
	public class OldSWSEnemy : SWSEnemy
	{
		protected override void StartMove()
		{
			this.PoolPathAndMove(this.path);
		}

		public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
		{
			base.Die(type);
			if (this.newPathTransform != null && PoolManager.Pools["PathPool"].IsSpawned(this.newPathTransform))
			{
				PoolManager.Pools["PathPool"].Despawn(this.newPathTransform);
			}
		}

		public virtual void PoolPathAndMove(PathManager path)
		{
			this.newPathTransform = PoolManager.Pools["PathPool"].Spawn(path.transform, base.transform.position, path.transform.rotation);
			BezierPathManager component = this.newPathTransform.GetComponent<BezierPathManager>();
			if (component != null)
			{
				component.CalculatePath();
				this.m_splineMove.SetPath(component);
			}
			else
			{
				PathManager component2 = this.newPathTransform.GetComponent<PathManager>();
				this.m_splineMove.SetPath(component2);
			}
		}

		[EnemyField]
		public PathManager path;

		protected Transform newPathTransform;
	}
}
