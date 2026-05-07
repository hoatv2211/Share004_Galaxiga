using System;
using DG.Tweening;
using SWS;
using UnityEngine;

namespace SkyGameKit
{
	public class SgkSplineMove : splineMove
	{
		public bool IsMoving { get; private set; }

		private void Start()
		{
			if (this.onStart)
			{
				this.StartMove();
			}
		}

		private void FixedUpdate()
		{
			if (this.IsMoving && this.cr != null && this.cr.CurrentPercent < 1f)
			{
				MoveNextResult moveNextResult = this.cr.MoveNext(this.speed * Time.fixedDeltaTime);
				if (this.local)
				{
					if (this.pathMode != PathMode.Ignore)
					{
						base.transform.localRotation = Quaternion.Euler(Vector3.forward * (moveNextResult.rot + this.lookAhead));
					}
					base.transform.localPosition = moveNextResult.pos;
				}
				else
				{
					if (this.pathMode != PathMode.Ignore)
					{
						base.transform.rotation = Quaternion.Euler(Vector3.forward * (moveNextResult.rot + this.lookAhead));
					}
					base.transform.position = moveNextResult.pos;
				}
			}
		}

		public override void ChangeSpeed(float value)
		{
			this.speed = value;
		}

		public override void SetPath(PathManager newPath)
		{
			base.SetPath(newPath);
		}

		public override void StartMove()
		{
			BezierPathManager bezierPathManager = this.pathContainer as BezierPathManager;
			if (bezierPathManager == null)
			{
				SgkLog.LogWarning(base.gameObject.name + " không có path, hoặc không phải Bezier Path");
				return;
			}
			this.cr = new CatmullRom(bezierPathManager)
			{
				OnReachedEnd = delegate()
				{
					this.IsMoving = false;
				},
				OnWaypointReached = new Action<int>(this.OnWaypointReached)
			};
			this.Resume();
		}

		public override void Pause(float seconds = 0f)
		{
			this.IsMoving = false;
			if (seconds > 0f)
			{
				this.waitStream = this.Delay(seconds, new Action(this.Resume), false);
			}
		}

		public override void Resume()
		{
			if (this.waitStream != null)
			{
				this.waitStream.Dispose();
			}
			this.IsMoving = true;
		}

		public override void ResetToStart()
		{
			this.Stop();
			this.StartMove();
		}

		public override void Reverse()
		{
			SgkLog.LogError("Chưa viết hàm này, ĐỪNG DÙNG");
		}

		public override void Stop()
		{
			this.Pause(0f);
			this.cr = null;
		}

		public override void GoToWaypoint(int index)
		{
			this.cr.GoToWaypoint(index);
		}

		private void OnWaypointReached(int index)
		{
			this.currentPoint = index;
			if (this.events == null || this.events.Count - 1 < index || this.events[index] == null)
			{
				return;
			}
			this.events[index].Invoke();
		}

		private CatmullRom cr;

		private IDisposable waitStream;
	}
}
