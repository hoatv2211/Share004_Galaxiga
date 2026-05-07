using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SnapScrollView
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(ScrollRect))]
	public class ScrollSnap : UIBehaviour, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		protected override void Awake()
		{
			base.Awake();
			this.actualIndex = this.startingIndex;
			this.cellIndex = this.startingIndex;
			this.onLerpComplete = new ScrollSnap.OnLerpCompleteEvent();
			this.onRelease = new ScrollSnap.OnReleaseEvent();
			this.scrollRect = base.GetComponent<ScrollRect>();
			this.canvasGroup = base.GetComponent<CanvasGroup>();
			this.content = this.scrollRect.content;
			this.cellSize = this.content.GetComponent<GridLayoutGroup>().cellSize;
			this.content.anchoredPosition = new Vector2(-this.cellSize.x * (float)this.cellIndex, this.content.anchoredPosition.y);
			int num = this.LayoutElementCount();
			this.SetContentSize(num);
			if (this.startingIndex < num)
			{
				this.MoveToIndex(this.startingIndex);
			}
		}

		private void LateUpdate()
		{
			if (this.isLerping)
			{
				this.LerpToElement();
				if (this.ShouldStopLerping())
				{
					this.isLerping = false;
					this.canvasGroup.blocksRaycasts = true;
					this.onLerpComplete.Invoke();
					this.onLerpComplete.RemoveListener(new UnityAction(this.WrapElementAround));
				}
			}
		}

		public void PushLayoutElement(LayoutElement element)
		{
			element.transform.SetParent(this.content.transform, false);
			this.SetContentSize(this.LayoutElementCount());
		}

		public void PopLayoutElement()
		{
			LayoutElement[] componentsInChildren = this.content.GetComponentsInChildren<LayoutElement>();
			UnityEngine.Object.Destroy(componentsInChildren[componentsInChildren.Length - 1].gameObject);
			this.SetContentSize(this.LayoutElementCount() - 1);
			if (this.cellIndex == this.CalculateMaxIndex())
			{
				this.cellIndex--;
			}
		}

		public void UnshiftLayoutElement(LayoutElement element)
		{
			this.cellIndex++;
			element.transform.SetParent(this.content.transform, false);
			element.transform.SetAsFirstSibling();
			this.SetContentSize(this.LayoutElementCount());
			this.content.anchoredPosition = new Vector2(this.content.anchoredPosition.x - this.cellSize.x, this.content.anchoredPosition.y);
		}

		public void ShiftLayoutElement()
		{
			UnityEngine.Object.Destroy(base.GetComponentInChildren<LayoutElement>().gameObject);
			this.SetContentSize(this.LayoutElementCount() - 1);
			this.cellIndex--;
			this.content.anchoredPosition = new Vector2(this.content.anchoredPosition.x + this.cellSize.x, this.content.anchoredPosition.y);
		}

		public int LayoutElementCount()
		{
			return this.content.GetComponentsInChildren<LayoutElement>(false).Count((LayoutElement e) => e.transform.parent == this.content);
		}

		public int CurrentIndex
		{
			get
			{
				int num = this.LayoutElementCount();
				int num2 = this.actualIndex % num;
				return (num2 < 0) ? (num + num2) : num2;
			}
		}

		public void OnDrag(PointerEventData data)
		{
			float x = data.delta.x;
			float num = Time.deltaTime * 1000f;
			float num2 = Mathf.Abs(x / num);
			if (num2 > this.triggerAcceleration && num2 != float.PositiveInfinity)
			{
				this.indexChangeTriggered = true;
			}
		}

		public void OnEndDrag(PointerEventData data)
		{
			if (this.IndexShouldChangeFromDrag(data))
			{
				int num = (data.pressPosition.x - data.position.x <= 0f) ? -1 : 1;
				this.SnapToIndex(this.cellIndex + num);
			}
			else
			{
				this.StartLerping();
			}
		}

		public void SnapToNext()
		{
			this.SnapToIndex(this.cellIndex + 1);
		}

		public void SnapToPrev()
		{
			this.SnapToIndex(this.cellIndex - 1);
		}

		public void SnapToIndex(int newCellIndex)
		{
			int num = this.CalculateMaxIndex();
			if (this.wrapAround && num > 0)
			{
				this.actualIndex += newCellIndex - this.cellIndex;
				this.cellIndex = newCellIndex;
				this.onLerpComplete.AddListener(new UnityAction(this.WrapElementAround));
			}
			else if (newCellIndex >= 0 && newCellIndex <= num)
			{
				this.actualIndex += newCellIndex - this.cellIndex;
				this.cellIndex = newCellIndex;
			}
			this.onRelease.Invoke(this.cellIndex);
			this.StartLerping();
		}

		public void MoveToIndex(int newCellIndex)
		{
			int num = this.CalculateMaxIndex();
			if (newCellIndex >= 0 && newCellIndex <= num)
			{
				this.actualIndex += newCellIndex - this.cellIndex;
				this.cellIndex = newCellIndex;
			}
			this.onRelease.Invoke(this.cellIndex);
			this.content.anchoredPosition = this.CalculateTargetPoisition(this.cellIndex);
		}

		private void StartLerping()
		{
			this.releasedPosition = this.content.anchoredPosition;
			this.targetPosition = this.CalculateTargetPoisition(this.cellIndex);
			this.lerpStartedAt = DateTime.Now;
			this.canvasGroup.blocksRaycasts = false;
			this.isLerping = true;
		}

		public int CalculateMaxIndex()
		{
			int num = Mathf.FloorToInt(this.scrollRect.GetComponent<RectTransform>().rect.size.x / this.cellSize.x);
			return this.LayoutElementCount() - num;
		}

		private bool IndexShouldChangeFromDrag(PointerEventData data)
		{
			if (this.indexChangeTriggered)
			{
				this.indexChangeTriggered = false;
				return true;
			}
			float num = this.scrollRect.content.anchoredPosition.x + (float)this.cellIndex * this.cellSize.x;
			float num2 = Mathf.Abs(num / this.cellSize.x);
			return num2 * 100f > this.triggerPercent;
		}

		private void LerpToElement()
		{
			float t = (float)((DateTime.Now - this.lerpStartedAt).TotalMilliseconds / (double)this.lerpTimeMilliSeconds);
			float x = Mathf.Lerp(this.releasedPosition.x, this.targetPosition.x, t);
			this.content.anchoredPosition = new Vector2(x, this.content.anchoredPosition.y);
		}

		private void WrapElementAround()
		{
			if (this.cellIndex <= 0)
			{
				LayoutElement[] componentsInChildren = this.content.GetComponentsInChildren<LayoutElement>();
				componentsInChildren[componentsInChildren.Length - 1].transform.SetAsFirstSibling();
				this.cellIndex++;
				this.content.anchoredPosition = new Vector2(this.content.anchoredPosition.x - this.cellSize.x, this.content.anchoredPosition.y);
			}
			else if (this.cellIndex >= this.CalculateMaxIndex())
			{
				LayoutElement componentInChildren = this.content.GetComponentInChildren<LayoutElement>();
				componentInChildren.transform.SetAsLastSibling();
				this.cellIndex--;
				this.content.anchoredPosition = new Vector2(this.content.anchoredPosition.x + this.cellSize.x, this.content.anchoredPosition.y);
			}
		}

		private void SetContentSize(int elementCount)
		{
			this.content.sizeDelta = new Vector2(this.cellSize.x * (float)elementCount, this.content.rect.height);
		}

		private Vector2 CalculateTargetPoisition(int index)
		{
			return new Vector2(-this.cellSize.x * (float)index, this.content.anchoredPosition.y);
		}

		private bool ShouldStopLerping()
		{
			return (double)Mathf.Abs(this.content.anchoredPosition.x - this.targetPosition.x) < 0.001;
		}

		[SerializeField]
		public int startingIndex;

		[SerializeField]
		public bool wrapAround;

		[SerializeField]
		public float lerpTimeMilliSeconds = 200f;

		[SerializeField]
		public float triggerPercent = 5f;

		[Range(0f, 10f)]
		public float triggerAcceleration = 1f;

		public ScrollSnap.OnLerpCompleteEvent onLerpComplete;

		public ScrollSnap.OnReleaseEvent onRelease;

		private int actualIndex;

		public int cellIndex;

		private ScrollRect scrollRect;

		private CanvasGroup canvasGroup;

		private RectTransform content;

		private Vector2 cellSize;

		private bool indexChangeTriggered;

		private bool isLerping;

		private DateTime lerpStartedAt;

		private Vector2 releasedPosition;

		private Vector2 targetPosition;

		public class OnLerpCompleteEvent : UnityEvent
		{
		}

		public class OnReleaseEvent : UnityEvent<int>
		{
		}
	}
}
