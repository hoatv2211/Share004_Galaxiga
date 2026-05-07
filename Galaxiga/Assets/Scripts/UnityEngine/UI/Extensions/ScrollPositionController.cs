using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	public class ScrollPositionController : UIBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IEventSystemHandler
	{
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.pointerStartLocalPosition = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewport, eventData.position, eventData.pressEventCamera, out this.pointerStartLocalPosition);
			this.dragStartScrollPosition = this.currentScrollPosition;
			this.dragging = true;
		}

		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.dragging)
			{
				return;
			}
			Vector2 a;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewport, eventData.position, eventData.pressEventCamera, out a))
			{
				return;
			}
			Vector2 vector = a - this.pointerStartLocalPosition;
			float num = ((this.directionOfRecognize != ScrollPositionController.ScrollDirection.Horizontal) ? vector.y : (-vector.x)) / this.GetViewportSize() * this.scrollSensitivity + this.dragStartScrollPosition;
			float num2 = this.CalculateOffset(num);
			num += num2;
			if (this.movementType == ScrollPositionController.MovementType.Elastic && num2 != 0f)
			{
				num -= this.RubberDelta(num2, this.scrollSensitivity);
			}
			this.UpdatePosition(num);
		}

		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.dragging = false;
		}

		private float GetViewportSize()
		{
			return (this.directionOfRecognize != ScrollPositionController.ScrollDirection.Horizontal) ? this.viewport.rect.size.y : this.viewport.rect.size.x;
		}

		private float CalculateOffset(float position)
		{
			if (this.movementType == ScrollPositionController.MovementType.Unrestricted)
			{
				return 0f;
			}
			if (position < 0f)
			{
				return -position;
			}
			if (position > (float)(this.dataCount - 1))
			{
				return (float)(this.dataCount - 1) - position;
			}
			return 0f;
		}

		private void UpdatePosition(float position)
		{
			this.currentScrollPosition = position;
			if (this.OnUpdatePosition != null)
			{
				this.OnUpdatePosition.Invoke(this.currentScrollPosition);
			}
		}

		private float RubberDelta(float overStretching, float viewSize)
		{
			return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
		}

		public void SetDataCount(int dataCont)
		{
			this.dataCount = dataCont;
		}

		private void Update()
		{
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			float num = this.CalculateOffset(this.currentScrollPosition);
			if (this.autoScrolling)
			{
				float num2 = Mathf.Clamp01((Time.unscaledTime - this.autoScrollStartTime) / Mathf.Max(this.autoScrollDuration, float.Epsilon));
				float position = Mathf.Lerp(this.dragStartScrollPosition, this.autoScrollPosition, this.EaseInOutCubic(0f, 1f, num2));
				this.UpdatePosition(position);
				if (Mathf.Approximately(num2, 1f))
				{
					this.autoScrolling = false;
					if (this.OnItemSelected != null)
					{
						this.OnItemSelected.Invoke(Mathf.RoundToInt(this.GetLoopPosition(this.autoScrollPosition, this.dataCount)));
					}
				}
			}
			else if (!this.dragging && (num != 0f || this.velocity != 0f))
			{
				float num3 = this.currentScrollPosition;
				if (this.movementType == ScrollPositionController.MovementType.Elastic && num != 0f)
				{
					float num4 = this.velocity;
					num3 = Mathf.SmoothDamp(this.currentScrollPosition, this.currentScrollPosition + num, ref num4, this.elasticity, float.PositiveInfinity, unscaledDeltaTime);
					this.velocity = num4;
				}
				else if (this.inertia)
				{
					this.velocity *= Mathf.Pow(this.decelerationRate, unscaledDeltaTime);
					if (Mathf.Abs(this.velocity) < 0.001f)
					{
						this.velocity = 0f;
					}
					num3 += this.velocity * unscaledDeltaTime;
					if (this.snap.Enable && Mathf.Abs(this.velocity) < this.snap.VelocityThreshold)
					{
						this.ScrollTo(Mathf.RoundToInt(this.currentScrollPosition), this.snap.Duration);
					}
				}
				else
				{
					this.velocity = 0f;
				}
				if (this.velocity != 0f)
				{
					if (this.movementType == ScrollPositionController.MovementType.Clamped)
					{
						num = this.CalculateOffset(num3);
						num3 += num;
					}
					this.UpdatePosition(num3);
				}
			}
			if (!this.autoScrolling && this.dragging && this.inertia)
			{
				float b = (this.currentScrollPosition - this.prevScrollPosition) / unscaledDeltaTime;
				this.velocity = Mathf.Lerp(this.velocity, b, unscaledDeltaTime * 10f);
			}
			if (this.currentScrollPosition != this.prevScrollPosition)
			{
				this.prevScrollPosition = this.currentScrollPosition;
			}
		}

		public void ScrollTo(int index, float duration)
		{
			this.velocity = 0f;
			this.autoScrolling = true;
			this.autoScrollDuration = duration;
			this.autoScrollStartTime = Time.unscaledTime;
			this.dragStartScrollPosition = this.currentScrollPosition;
			this.autoScrollPosition = ((this.movementType != ScrollPositionController.MovementType.Unrestricted) ? ((float)index) : this.CalculateClosestPosition(index));
		}

		private float CalculateClosestPosition(int index)
		{
			float num = this.GetLoopPosition((float)index, this.dataCount) - this.GetLoopPosition(this.currentScrollPosition, this.dataCount);
			if (Mathf.Abs(num) > (float)this.dataCount * 0.5f)
			{
				num = Mathf.Sign(-num) * ((float)this.dataCount - Mathf.Abs(num));
			}
			return num + this.currentScrollPosition;
		}

		private float GetLoopPosition(float position, int length)
		{
			if (position < 0f)
			{
				position = (float)(length - 1) + (position + 1f) % (float)length;
			}
			else if (position > (float)(length - 1))
			{
				position %= (float)length;
			}
			return position;
		}

		private float EaseInOutCubic(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value * value + start;
			}
			value -= 2f;
			return end * 0.5f * (value * value * value + 2f) + start;
		}

		[SerializeField]
		private RectTransform viewport;

		[SerializeField]
		private ScrollPositionController.ScrollDirection directionOfRecognize;

		[SerializeField]
		private ScrollPositionController.MovementType movementType = ScrollPositionController.MovementType.Elastic;

		[SerializeField]
		private float elasticity = 0.1f;

		[SerializeField]
		private float scrollSensitivity = 1f;

		[SerializeField]
		private bool inertia = true;

		[SerializeField]
		[Tooltip("Only used when inertia is enabled")]
		private float decelerationRate = 0.03f;

		[SerializeField]
		[Tooltip("Only used when inertia is enabled")]
		private ScrollPositionController.Snap snap = new ScrollPositionController.Snap
		{
			Enable = true,
			VelocityThreshold = 0.5f,
			Duration = 0.3f
		};

		[SerializeField]
		private int dataCount;

		[Tooltip("Event that fires when the position of an item changes")]
		public ScrollPositionController.UpdatePositionEvent OnUpdatePosition;

		[Tooltip("Event that fires when an item is selected/focused")]
		public ScrollPositionController.ItemSelectedEvent OnItemSelected;

		private Vector2 pointerStartLocalPosition;

		private float dragStartScrollPosition;

		private float currentScrollPosition;

		private bool dragging;

		private float velocity;

		private float prevScrollPosition;

		private bool autoScrolling;

		private float autoScrollDuration;

		private float autoScrollStartTime;

		private float autoScrollPosition;

		[Serializable]
		public class UpdatePositionEvent : UnityEvent<float>
		{
		}

		[Serializable]
		public class ItemSelectedEvent : UnityEvent<int>
		{
		}

		[Serializable]
		private struct Snap
		{
			public bool Enable;

			public float VelocityThreshold;

			public float Duration;
		}

		private enum ScrollDirection
		{
			Vertical,
			Horizontal
		}

		private enum MovementType
		{
			Unrestricted,
			Elastic,
			Clamped
		}
	}
}
