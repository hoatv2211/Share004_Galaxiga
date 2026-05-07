using System;
using DG.Tweening;
using UnityEngine;

namespace SkyGameKit.QuickAccess
{
	public abstract class APlayerMove : MonoBehaviour
	{
		public virtual bool LockMove
		{
			get
			{
				return this._lockMove;
			}
			set
			{
				this._lockMove = value;
				if (this.onLockMoveChange != null)
				{
					this.onLockMoveChange();
				}
			}
		}

		public virtual Tweener AutoMove(Vector2 s, Vector2 d, float t = 1f, TweenCallback onComplete = null)
		{
			base.transform.position = new Vector3(s.x, s.y, base.transform.position.z);
			return this.AutoMove(d, t, onComplete);
		}

		public abstract Tweener AutoMove(Vector2 destination, float t = 1f, TweenCallback onComplete = null);

		public float speed;

		public MoveMode moveMode;

		private bool _lockMove;

		public Action onLockMoveChange;
	}
}
