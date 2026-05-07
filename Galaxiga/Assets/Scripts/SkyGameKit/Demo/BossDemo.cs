using System;
using DG.Tweening;
using UnityEngine;

namespace SkyGameKit.Demo
{
	public class BossDemo : BaseEnemy
	{
		[EnemyAction]
		public void MoveRandom(float duration)
		{
			base.transform.DOMove(new Vector2((float)UnityEngine.Random.Range(-4, 4), (float)UnityEngine.Random.Range(0, 6)), duration, false);
		}

		[EnemyAction(displayName = "BossDemo/StartFreeTurn")]
		public virtual void StartSpawnByBoss(SpawnByBoss turn, Vector2 offset)
		{
			turn.boss = base.transform;
			turn.offset = offset;
			this.childTurn = FreeWave.StartTurn(turn);
		}

		[EnemyAction(displayName = "BossDemo/StopFreeTurn")]
		public virtual void StopSpawnByBoss()
		{
			this.childTurn.ForceStopAndKillAllEnemies();
		}

		public override void Restart()
		{
			base.Restart();
			if (this.EveryXSecond != null)
			{
				this.EveryXSecond.ForEach(delegate(EnemyEventUnit<float> x)
				{
					this.DoActionEveryTime(x.param, new Action(x.Invoke), false, false);
				});
			}
		}

		[EnemyEventCustom(paramName = "X")]
		public EnemyEvent<float> EveryXSecond = new EnemyEvent<float>();

		private TurnManager childTurn;
	}
}
