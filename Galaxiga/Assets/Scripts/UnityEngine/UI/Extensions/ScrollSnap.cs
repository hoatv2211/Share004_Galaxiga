using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(ScrollRect))]
	[AddComponentMenu("UI/Extensions/Scroll Snap")]
	public class ScrollSnap : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollSnap, IEventSystemHandler
	{
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ScrollSnap.PageSnapChange onPageChange;

		private void Start()
		{
			this._lerp = false;
			this._scroll_rect = base.gameObject.GetComponent<ScrollRect>();
			this._scrollRectTransform = base.gameObject.GetComponent<RectTransform>();
			this._listContainerTransform = this._scroll_rect.content;
			this._listContainerRectTransform = this._listContainerTransform.GetComponent<RectTransform>();
			this.UpdateListItemsSize();
			this.UpdateListItemPositions();
			this.PageChanged(this.CurrentPage());
			if (this.NextButton)
			{
				this.NextButton.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.NextScreen();
				});
			}
			if (this.PrevButton)
			{
				this.PrevButton.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.PreviousScreen();
				});
			}
			if (this._scroll_rect.horizontalScrollbar != null && this._scroll_rect.horizontal)
			{
				ScrollSnapScrollbarHelper scrollSnapScrollbarHelper = this._scroll_rect.horizontalScrollbar.gameObject.AddComponent<ScrollSnapScrollbarHelper>();
				scrollSnapScrollbarHelper.ss = this;
			}
			if (this._scroll_rect.verticalScrollbar != null && this._scroll_rect.vertical)
			{
				ScrollSnapScrollbarHelper scrollSnapScrollbarHelper2 = this._scroll_rect.verticalScrollbar.gameObject.AddComponent<ScrollSnapScrollbarHelper>();
				scrollSnapScrollbarHelper2.ss = this;
			}
		}

		public void UpdateListItemsSize()
		{
			float num;
			float num2;
			if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
			{
				num = this._scrollRectTransform.rect.width / (float)this.ItemsVisibleAtOnce;
				num2 = this._listContainerRectTransform.rect.width / (float)this._itemsCount;
			}
			else
			{
				num = this._scrollRectTransform.rect.height / (float)this.ItemsVisibleAtOnce;
				num2 = this._listContainerRectTransform.rect.height / (float)this._itemsCount;
			}
			this._itemSize = num;
			if (this.LinkScrolrectScrollSensitivity)
			{
				this._scroll_rect.scrollSensitivity = this._itemSize;
			}
			if (this.AutoLayoutItems && num2 != num && this._itemsCount > 0)
			{
				if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
				{
					IEnumerator enumerator = this._listContainerTransform.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							GameObject gameObject = ((Transform)obj).gameObject;
							if (gameObject.activeInHierarchy)
							{
								LayoutElement layoutElement = gameObject.GetComponent<LayoutElement>();
								if (layoutElement == null)
								{
									layoutElement = gameObject.AddComponent<LayoutElement>();
								}
								layoutElement.minWidth = this._itemSize;
							}
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
				}
				else
				{
					IEnumerator enumerator2 = this._listContainerTransform.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							GameObject gameObject2 = ((Transform)obj2).gameObject;
							if (gameObject2.activeInHierarchy)
							{
								LayoutElement layoutElement2 = gameObject2.GetComponent<LayoutElement>();
								if (layoutElement2 == null)
								{
									layoutElement2 = gameObject2.AddComponent<LayoutElement>();
								}
								layoutElement2.minHeight = this._itemSize;
							}
						}
					}
					finally
					{
						IDisposable disposable2;
						if ((disposable2 = (enumerator2 as IDisposable)) != null)
						{
							disposable2.Dispose();
						}
					}
				}
			}
		}

		public void UpdateListItemPositions()
		{
			if (!this._listContainerRectTransform.rect.size.Equals(this._listContainerCachedSize))
			{
				int num = 0;
				IEnumerator enumerator = this._listContainerTransform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						if (((Transform)obj).gameObject.activeInHierarchy)
						{
							num++;
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				this._itemsCount = 0;
				Array.Resize<Vector3>(ref this._pageAnchorPositions, num);
				if (num > 0)
				{
					this._pages = Mathf.Max(num - this.ItemsVisibleAtOnce + 1, 1);
					if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
					{
						this._scroll_rect.horizontalNormalizedPosition = 0f;
						this._listContainerMaxPosition = this._listContainerTransform.localPosition.x;
						this._scroll_rect.horizontalNormalizedPosition = 1f;
						this._listContainerMinPosition = this._listContainerTransform.localPosition.x;
						this._listContainerSize = this._listContainerMaxPosition - this._listContainerMinPosition;
						for (int i = 0; i < this._pages; i++)
						{
							this._pageAnchorPositions[i] = new Vector3(this._listContainerMaxPosition - this._itemSize * (float)i, this._listContainerTransform.localPosition.y, this._listContainerTransform.localPosition.z);
						}
					}
					else
					{
						this._scroll_rect.verticalNormalizedPosition = 1f;
						this._listContainerMinPosition = this._listContainerTransform.localPosition.y;
						this._scroll_rect.verticalNormalizedPosition = 0f;
						this._listContainerMaxPosition = this._listContainerTransform.localPosition.y;
						this._listContainerSize = this._listContainerMaxPosition - this._listContainerMinPosition;
						for (int j = 0; j < this._pages; j++)
						{
							this._pageAnchorPositions[j] = new Vector3(this._listContainerTransform.localPosition.x, this._listContainerMinPosition + this._itemSize * (float)j, this._listContainerTransform.localPosition.z);
						}
					}
					this.UpdateScrollbar(this.LinkScrolbarSteps);
					this._startingPage = Mathf.Min(this._startingPage, this._pages);
					this.ResetPage();
				}
				if (this._itemsCount != num)
				{
					this.PageChanged(this.CurrentPage());
				}
				this._itemsCount = num;
				this._listContainerCachedSize.Set(this._listContainerRectTransform.rect.size.x, this._listContainerRectTransform.rect.size.y);
			}
		}

		public void ResetPage()
		{
			if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
			{
				this._scroll_rect.horizontalNormalizedPosition = ((this._pages <= 1) ? 0f : ((float)this._startingPage / (float)(this._pages - 1)));
			}
			else
			{
				this._scroll_rect.verticalNormalizedPosition = ((this._pages <= 1) ? 0f : ((float)(this._pages - this._startingPage - 1) / (float)(this._pages - 1)));
			}
		}

		private void UpdateScrollbar(bool linkSteps)
		{
			if (linkSteps)
			{
				if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
				{
					if (this._scroll_rect.horizontalScrollbar != null)
					{
						this._scroll_rect.horizontalScrollbar.numberOfSteps = this._pages;
					}
				}
				else if (this._scroll_rect.verticalScrollbar != null)
				{
					this._scroll_rect.verticalScrollbar.numberOfSteps = this._pages;
				}
			}
			else if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
			{
				if (this._scroll_rect.horizontalScrollbar != null)
				{
					this._scroll_rect.horizontalScrollbar.numberOfSteps = 0;
				}
			}
			else if (this._scroll_rect.verticalScrollbar != null)
			{
				this._scroll_rect.verticalScrollbar.numberOfSteps = 0;
			}
		}

		private void LateUpdate()
		{
			this.UpdateListItemsSize();
			this.UpdateListItemPositions();
			if (this._lerp)
			{
				this.UpdateScrollbar(false);
				this._listContainerTransform.localPosition = Vector3.Lerp(this._listContainerTransform.localPosition, this._lerpTarget, 7.5f * Time.deltaTime);
				if (Vector3.Distance(this._listContainerTransform.localPosition, this._lerpTarget) < 0.001f)
				{
					this._listContainerTransform.localPosition = this._lerpTarget;
					this._lerp = false;
					this.UpdateScrollbar(this.LinkScrolbarSteps);
				}
				if (Vector3.Distance(this._listContainerTransform.localPosition, this._lerpTarget) < 10f)
				{
					this.PageChanged(this.CurrentPage());
				}
			}
			if (this._fastSwipeTimer)
			{
				this._fastSwipeCounter++;
			}
		}

		public void NextScreen()
		{
			this.UpdateListItemPositions();
			if (this.CurrentPage() < this._pages - 1)
			{
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[this.CurrentPage() + 1];
				this.PageChanged(this.CurrentPage() + 1);
			}
		}

		public void PreviousScreen()
		{
			this.UpdateListItemPositions();
			if (this.CurrentPage() > 0)
			{
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[this.CurrentPage() - 1];
				this.PageChanged(this.CurrentPage() - 1);
			}
		}

		private void NextScreenCommand()
		{
			if (this._pageOnDragStart < this._pages - 1)
			{
				int num = Mathf.Min(this._pages - 1, this._pageOnDragStart + this.ItemsVisibleAtOnce);
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[num];
				this.PageChanged(num);
			}
		}

		private void PrevScreenCommand()
		{
			if (this._pageOnDragStart > 0)
			{
				int num = Mathf.Max(0, this._pageOnDragStart - this.ItemsVisibleAtOnce);
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[num];
				this.PageChanged(num);
			}
		}

		public int CurrentPage()
		{
			float num;
			if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
			{
				num = this._listContainerMaxPosition - this._listContainerTransform.localPosition.x;
				num = Mathf.Clamp(num, 0f, this._listContainerSize);
			}
			else
			{
				num = this._listContainerTransform.localPosition.y - this._listContainerMinPosition;
				num = Mathf.Clamp(num, 0f, this._listContainerSize);
			}
			float f = num / this._itemSize;
			return Mathf.Clamp(Mathf.RoundToInt(f), 0, this._pages);
		}

		public void SetLerp(bool value)
		{
			this._lerp = value;
		}

		public void ChangePage(int page)
		{
			if (0 <= page && page < this._pages)
			{
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[page];
				this.PageChanged(page);
			}
		}

		private void PageChanged(int currentPage)
		{
			this._startingPage = currentPage;
			if (this.NextButton)
			{
				this.NextButton.interactable = (currentPage < this._pages - 1);
			}
			if (this.PrevButton)
			{
				this.PrevButton.interactable = (currentPage > 0);
			}
			if (this.onPageChange != null)
			{
				this.onPageChange(currentPage);
			}
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			this.UpdateScrollbar(false);
			this._fastSwipeCounter = 0;
			this._fastSwipeTimer = true;
			this._positionOnDragStart = eventData.position;
			this._pageOnDragStart = this.CurrentPage();
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			this._startDrag = true;
			float num;
			if (this.direction == ScrollSnap.ScrollDirection.Horizontal)
			{
				num = this._positionOnDragStart.x - eventData.position.x;
			}
			else
			{
				num = -this._positionOnDragStart.y + eventData.position.y;
			}
			if (this.UseFastSwipe)
			{
				this.fastSwipe = false;
				this._fastSwipeTimer = false;
				if (this._fastSwipeCounter <= this._fastSwipeTarget && Math.Abs(num) > (float)this.FastSwipeThreshold)
				{
					this.fastSwipe = true;
				}
				if (this.fastSwipe)
				{
					if (num > 0f)
					{
						this.NextScreenCommand();
					}
					else
					{
						this.PrevScreenCommand();
					}
				}
				else
				{
					this._lerp = true;
					this._lerpTarget = this._pageAnchorPositions[this.CurrentPage()];
				}
			}
			else
			{
				this._lerp = true;
				this._lerpTarget = this._pageAnchorPositions[this.CurrentPage()];
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			this._lerp = false;
			if (this._startDrag)
			{
				this.OnBeginDrag(eventData);
				this._startDrag = false;
			}
		}

		public void StartScreenChange()
		{
		}

		private ScrollRect _scroll_rect;

		private RectTransform _scrollRectTransform;

		private Transform _listContainerTransform;

		private int _pages;

		private int _startingPage;

		private Vector3[] _pageAnchorPositions;

		private Vector3 _lerpTarget;

		private bool _lerp;

		private float _listContainerMinPosition;

		private float _listContainerMaxPosition;

		private float _listContainerSize;

		private RectTransform _listContainerRectTransform;

		private Vector2 _listContainerCachedSize;

		private float _itemSize;

		private int _itemsCount;

		private bool _startDrag = true;

		private Vector3 _positionOnDragStart = default(Vector3);

		private int _pageOnDragStart;

		private bool _fastSwipeTimer;

		private int _fastSwipeCounter;

		private int _fastSwipeTarget = 10;

		[Tooltip("Button to go to the next page. (optional)")]
		public Button NextButton;

		[Tooltip("Button to go to the previous page. (optional)")]
		public Button PrevButton;

		[Tooltip("Number of items visible in one page of scroll frame.")]
		[Range(1f, 100f)]
		public int ItemsVisibleAtOnce = 1;

		[Tooltip("Sets minimum width of list items to 1/itemsVisibleAtOnce.")]
		public bool AutoLayoutItems = true;

		[Tooltip("If you wish to update scrollbar numberOfSteps to number of active children on list.")]
		public bool LinkScrolbarSteps;

		[Tooltip("If you wish to update scrollrect sensitivity to size of list element.")]
		public bool LinkScrolrectScrollSensitivity;

		public bool UseFastSwipe = true;

		public int FastSwipeThreshold = 100;

		public ScrollSnap.ScrollDirection direction;

		private bool fastSwipe;

		public enum ScrollDirection
		{
			Horizontal,
			Vertical
		}

		public delegate void PageSnapChange(int page);
	}
}
