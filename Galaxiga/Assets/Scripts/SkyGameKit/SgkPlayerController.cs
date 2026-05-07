using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using SkyGameKit.QuickAccess;
using UnityEngine;

namespace SkyGameKit
{
	public class SgkPlayerController : APlayerMove
	{
		public override bool LockMove
		{
			get
			{
				return base.LockMove;
			}
			set
			{
				if (this.onAutoMove)
				{
					SgkLog.Log("Player đang tự di chuyển, không được set LookMove");
				}
				else
				{
					base.LockMove = value;
				}
			}
		}

		protected virtual void Awake()
		{
			this.onLockMoveChange = (Action)Delegate.Combine(this.onLockMoveChange, new Action(this.OnLockMoveChangeHandle));
		}

		protected virtual void OnLockMoveChangeHandle()
		{
			if (!this.LockMove)
			{
				this.currentTouchId = -1;
				if (Input.GetMouseButton(0))
				{
					this.mousePos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
					this.mousePos.z = this.playerTransformZ;
					this.lastMousePos = this.mousePos;
				}
			}
		}

		protected virtual void Start()
		{
			this.playerTransformZ = base.transform.position.z;
			this.bottomLeft = SgkCamera.bottomLeft;
			this.bottomLeft.x = this.bottomLeft.x + this.marginRightAndLeft;
			this.bottomLeft.y = this.bottomLeft.y + this.marginBottom;
			this.topRight = SgkCamera.topRight;
			this.topRight.x = this.topRight.x - this.marginRightAndLeft;
			this.topRight.y = this.topRight.y - this.marginTop;
			if (this.speed <= 0f)
			{
				this.speed = 10f;
			}
		}

		protected virtual void Update()
		{
			if (this.LockMove)
			{
				return;
			}
			if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
			{
				if (UnityEngine.Input.touchCount > 0)
				{
					if (this.currentTouchId < 0)
					{
						this.currentTouchId = Input.touches[0].fingerId;
						this.currentTouchChanged = true;
					}
					this.currentTouch = SgkPlayerController.GetTouch(this.currentTouchId);
					if (this.currentTouch.phase == TouchPhase.Ended || this.currentTouch.phase == TouchPhase.Canceled)
					{
						this.currentTouchId = -1;
					}
					this.mousePos = Camera.main.ScreenToWorldPoint(this.currentTouch.position);
				}
				else
				{
					this.mousePos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
				}
				this.mousePos.z = this.playerTransformZ;
				this.step = this.speed * Time.deltaTime;
				if (this.moveMode == MoveMode.Touch)
				{
					this.TouchMove();
				}
				else
				{
					this.RelativeMove();
				}
			}
		}

		public static Touch GetTouch(int fingerId)
		{
			foreach (Touch result in Input.touches)
			{
				if (result.fingerId == fingerId)
				{
					return result;
				}
			}
			return new Touch
			{
				fingerId = -1
			};
		}

		protected virtual void TouchMove()
		{
			this.mousePos += (Vector3)this.offsetTouch;
			if (Input.GetMouseButtonDown(0) && Fu.IsPointerOverUIObject())
			{
				this.overUI = true;
			}
			if (this.overUI)
			{
				if (Input.GetMouseButton(0))
				{
					if (!Fu.IsPointerOverUIObject())
					{
						this.overUI = false;
					}
				}
				else if (Input.GetMouseButtonUp(0))
				{
					this.overUI = false;
				}
			}
			else
			{
				Vector3 position;
				if (this.lerpMove)
				{
					position = Vector3.Lerp(base.transform.position, this.mousePos, (0.4f * this.step >= 1f) ? 1f : (0.5f * this.step));
				}
				else
				{
					position = Vector3.MoveTowards(base.transform.position, this.mousePos, this.step);
				}
				position.x = Mathf.Clamp(position.x, this.bottomLeft.x, this.topRight.x);
				position.y = Mathf.Clamp(position.y, this.bottomLeft.y, this.topRight.y);
				position.z = base.transform.position.z;
				base.transform.position = position;
			}
		}

		protected virtual void RelativeMove()
		{
			if (UnityEngine.Input.touchCount == 0)
			{
				if (Input.GetMouseButtonDown(0))
				{
					this.lastMousePos = this.mousePos;
					return;
				}
			}
			else if (this.currentTouchChanged)
			{
				this.lastMousePos = this.mousePos;
				this.currentTouchChanged = false;
				return;
			}
			Vector3 a = this.mousePos - this.lastMousePos;
			Vector3 vector;
			if (this.lerpMove)
			{
				vector = base.transform.position + a * ((0.4f * this.step >= 1f) ? 1f : (0.5f * this.step)) * this.relativeScale;
			}
			else
			{
				vector = Vector3.MoveTowards(base.transform.position, base.transform.position + a * this.relativeScale, this.step);
			}
			if (this.relativeScale > 1.01f)
			{
				this.lastMousePos = this.mousePos;
			}
			else
			{
				this.lastMousePos += vector - base.transform.position;
			}
			vector.x = Mathf.Clamp(vector.x, this.bottomLeft.x, this.topRight.x);
			vector.y = Mathf.Clamp(vector.y, this.bottomLeft.y, this.topRight.y);
			vector.z = base.transform.position.z;
			base.transform.position = vector;
		}

		public override Tweener AutoMove(Vector2 destination, float t = 1f, TweenCallback onComplete = null)
		{
			this.lockMoveSaveValue = this.LockMove;
			this.LockMove = true;
			this.onAutoMove = true;
			Vector3 endValue = new Vector3(destination.x, destination.y, base.transform.position.z);
			onComplete = (TweenCallback)Delegate.Combine(onComplete, new TweenCallback(delegate()
			{
				this.onAutoMove = false;
				this.LockMove = this.lockMoveSaveValue;
			}));
			return base.transform.DOMove(endValue, t, false).OnComplete(onComplete);
		}

		private float step;

		protected Vector3 lastMousePos;

		private float playerTransformZ;

		private Vector2 bottomLeft;

		private Vector2 topRight;

		private int currentTouchId = -1;

		private Touch currentTouch;

		private bool currentTouchChanged;

		[HideIf("moveMode", MoveMode.Relative, false)]
		public Vector2 offsetTouch = Vector2.down;

		[ShowIf("moveMode", MoveMode.Relative, false)]
		public float relativeScale = 1f;

		public bool lerpMove = true;

		public float marginTop;

		public float marginBottom;

		public float marginRightAndLeft;

		private bool overUI;

		private Vector3 mousePos;

		private bool onAutoMove;

		private bool lockMoveSaveValue;
	}
}
