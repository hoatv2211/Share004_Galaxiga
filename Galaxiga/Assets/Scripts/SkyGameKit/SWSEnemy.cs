using System;
using SWS;
using UnityEngine.Events;

namespace SkyGameKit
{
	public abstract class SWSEnemy : BaseEnemy
	{
		protected virtual void Awake()
		{
			this.m_splineMove = base.GetComponent<splineMove>();
		}

		public override void Restart()
		{
			base.Restart();
			this.StartMove();
			if (this.reachingWaypoint != null)
			{
				this.reachingWaypoint.ForEach(delegate(EnemyEventUnit<int> x)
				{
					if (x.param < this.m_splineMove.events.Count)
					{
						this.m_splineMove.events[x.param].AddListener(new UnityAction(x.Invoke));
					}
				});
			}
			if (this.saveSpeed < 0f)
			{
				this.saveSpeed = this.m_splineMove.speed;
			}
			else
			{
				this.m_splineMove.ChangeSpeed(this.saveSpeed);
			}
		}

		protected abstract void StartMove();

		[EnemyAction(displayName = "SWSEnemy/SetPath")]
		public virtual void SetPath(PathManager newPath)
		{
			this.m_splineMove.SetPath(newPath);
		}

		[EnemyAction(displayName = "SWSEnemy/Pause")]
		public virtual void Pause(float seconds)
		{
			this.m_splineMove.Pause(seconds);
		}

		[EnemyAction(displayName = "SWSEnemy/Resume")]
		public virtual void Resume()
		{
			this.m_splineMove.Resume();
		}

		[EnemyAction(displayName = "SWSEnemy/ChangeSpeed")]
		public virtual void ChangeSpeed(float speed)
		{
			this.m_splineMove.ChangeSpeed(speed);
		}

		protected splineMove m_splineMove;

		[EnemyEventCustom(paramName = "Point")]
		public EnemyEvent<int> reachingWaypoint;

		private float saveSpeed = -1f;
	}
}
