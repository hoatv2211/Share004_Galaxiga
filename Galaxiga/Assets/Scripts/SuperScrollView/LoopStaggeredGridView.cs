using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class LoopStaggeredGridView : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IEventSystemHandler
	{
		public ListItemArrangeType ArrangeType
		{
			get
			{
				return this.mArrangeType;
			}
			set
			{
				this.mArrangeType = value;
			}
		}

		public List<StaggeredGridItemPrefabConfData> ItemPrefabDataList
		{
			get
			{
				return this.mItemPrefabDataList;
			}
		}

		public int ListUpdateCheckFrameCount
		{
			get
			{
				return this.mListUpdateCheckFrameCount;
			}
		}

		public bool IsVertList
		{
			get
			{
				return this.mIsVertList;
			}
		}

		public int ItemTotalCount
		{
			get
			{
				return this.mItemTotalCount;
			}
		}

		public RectTransform ContainerTrans
		{
			get
			{
				return this.mContainerTrans;
			}
		}

		public ScrollRect ScrollRect
		{
			get
			{
				return this.mScrollRect;
			}
		}

		public bool IsDraging
		{
			get
			{
				return this.mIsDraging;
			}
		}

		public StaggeredGridItemGroup GetItemGroupByIndex(int index)
		{
			int count = this.mItemGroupList.Count;
			if (index < 0 || index >= count)
			{
				return null;
			}
			return this.mItemGroupList[index];
		}

		public StaggeredGridItemPrefabConfData GetItemPrefabConfData(string prefabName)
		{
			foreach (StaggeredGridItemPrefabConfData staggeredGridItemPrefabConfData in this.mItemPrefabDataList)
			{
				if (staggeredGridItemPrefabConfData.mItemPrefab == null)
				{
					UnityEngine.Debug.LogError("A item prefab is null ");
				}
				else if (prefabName == staggeredGridItemPrefabConfData.mItemPrefab.name)
				{
					return staggeredGridItemPrefabConfData;
				}
			}
			return null;
		}

		public void InitListView(int itemTotalCount, GridViewLayoutParam layoutParam, Func<LoopStaggeredGridView, int, LoopStaggeredGridViewItem> onGetItemByItemIndex, StaggeredGridViewInitParam initParam = null)
		{
			this.mLayoutParam = layoutParam;
			if (this.mLayoutParam == null)
			{
				UnityEngine.Debug.LogError("layoutParam can not be null!");
				return;
			}
			if (!this.mLayoutParam.CheckParam())
			{
				return;
			}
			if (initParam != null)
			{
				this.mDistanceForRecycle0 = initParam.mDistanceForRecycle0;
				this.mDistanceForNew0 = initParam.mDistanceForNew0;
				this.mDistanceForRecycle1 = initParam.mDistanceForRecycle1;
				this.mDistanceForNew1 = initParam.mDistanceForNew1;
				this.mItemDefaultWithPaddingSize = initParam.mItemDefaultWithPaddingSize;
			}
			this.mScrollRect = base.gameObject.GetComponent<ScrollRect>();
			if (this.mScrollRect == null)
			{
				UnityEngine.Debug.LogError("LoopStaggeredGridView Init Failed! ScrollRect component not found!");
				return;
			}
			if (this.mDistanceForRecycle0 <= this.mDistanceForNew0)
			{
				UnityEngine.Debug.LogError("mDistanceForRecycle0 should be bigger than mDistanceForNew0");
			}
			if (this.mDistanceForRecycle1 <= this.mDistanceForNew1)
			{
				UnityEngine.Debug.LogError("mDistanceForRecycle1 should be bigger than mDistanceForNew1");
			}
			this.mScrollRectTransform = this.mScrollRect.GetComponent<RectTransform>();
			this.mContainerTrans = this.mScrollRect.content;
			this.mViewPortRectTransform = this.mScrollRect.viewport;
			this.mGroupCount = this.mLayoutParam.mColumnOrRowCount;
			if (this.mViewPortRectTransform == null)
			{
				this.mViewPortRectTransform = this.mScrollRectTransform;
			}
			if (this.mScrollRect.horizontalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport && this.mScrollRect.horizontalScrollbar != null)
			{
				UnityEngine.Debug.LogError("ScrollRect.horizontalScrollbarVisibility cannot be set to AutoHideAndExpandViewport");
			}
			if (this.mScrollRect.verticalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport && this.mScrollRect.verticalScrollbar != null)
			{
				UnityEngine.Debug.LogError("ScrollRect.verticalScrollbarVisibility cannot be set to AutoHideAndExpandViewport");
			}
			this.mIsVertList = (this.mArrangeType == ListItemArrangeType.TopToBottom || this.mArrangeType == ListItemArrangeType.BottomToTop);
			this.mScrollRect.horizontal = !this.mIsVertList;
			this.mScrollRect.vertical = this.mIsVertList;
			this.AdjustPivot(this.mViewPortRectTransform);
			this.AdjustAnchor(this.mContainerTrans);
			this.AdjustContainerPivot(this.mContainerTrans);
			this.InitItemPool();
			this.mOnGetItemByItemIndex = onGetItemByItemIndex;
			if (this.mListViewInited)
			{
				UnityEngine.Debug.LogError("LoopStaggeredGridView.InitListView method can be called only once.");
			}
			this.mListViewInited = true;
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			this.mContainerTrans.anchoredPosition3D = Vector3.zero;
			this.mItemTotalCount = itemTotalCount;
			if (this.mLayoutParam.mCustomColumnOrRowOffsetArray == null)
			{
				this.mLayoutParam.mCustomColumnOrRowOffsetArray = new float[this.mGroupCount];
				float num = this.mLayoutParam.mItemWidthOrHeight * (float)this.mGroupCount;
				float num2;
				if (this.IsVertList)
				{
					num2 = (this.ViewPortWidth - this.mLayoutParam.mPadding1 - this.mLayoutParam.mPadding2 - num) / (float)(this.mGroupCount - 1);
				}
				else
				{
					num2 = (this.ViewPortHeight - this.mLayoutParam.mPadding1 - this.mLayoutParam.mPadding2 - num) / (float)(this.mGroupCount - 1);
				}
				float num3 = this.mLayoutParam.mPadding1;
				for (int i = 0; i < this.mGroupCount; i++)
				{
					if (this.IsVertList)
					{
						this.mLayoutParam.mCustomColumnOrRowOffsetArray[i] = num3;
					}
					else
					{
						this.mLayoutParam.mCustomColumnOrRowOffsetArray[i] = -num3;
					}
					num3 = num3 + this.mLayoutParam.mItemWidthOrHeight + num2;
				}
			}
			for (int j = 0; j < this.mGroupCount; j++)
			{
				StaggeredGridItemGroup staggeredGridItemGroup = new StaggeredGridItemGroup();
				staggeredGridItemGroup.Init(this, this.mItemTotalCount, j, new Func<int, int, LoopStaggeredGridViewItem>(this.GetNewItemByGroupAndIndex));
				this.mItemGroupList.Add(staggeredGridItemGroup);
			}
			this.UpdateContentSize();
		}

		public LoopStaggeredGridViewItem NewListViewItem(string itemPrefabName)
		{
			StaggeredGridItemPool staggeredGridItemPool = null;
			if (!this.mItemPoolDict.TryGetValue(itemPrefabName, out staggeredGridItemPool))
			{
				return null;
			}
			LoopStaggeredGridViewItem item = staggeredGridItemPool.GetItem();
			RectTransform component = item.GetComponent<RectTransform>();
			component.SetParent(this.mContainerTrans);
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			item.ParentListView = this;
			return item;
		}

		public void SetListItemCount(int itemCount, bool resetPos = true)
		{
			if (itemCount == this.mItemTotalCount)
			{
				return;
			}
			int count = this.mItemGroupList.Count;
			this.mItemTotalCount = itemCount;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].SetListItemCount(this.mItemTotalCount);
			}
			this.UpdateContentSize();
			if (this.mItemTotalCount == 0)
			{
				this.mItemIndexDataList.Clear();
				this.ClearAllTmpRecycledItem();
				return;
			}
			int count2 = this.mItemIndexDataList.Count;
			if (count2 > this.mItemTotalCount)
			{
				this.mItemIndexDataList.RemoveRange(this.mItemTotalCount, count2 - this.mItemTotalCount);
			}
			if (resetPos)
			{
				this.MovePanelToItemIndex(0, 0f);
				return;
			}
			if (count2 > this.mItemTotalCount)
			{
				this.MovePanelToItemIndex(this.mItemTotalCount - 1, 0f);
			}
		}

		public void MovePanelToItemIndex(int itemIndex, float offset)
		{
			this.mScrollRect.StopMovement();
			if (this.mItemTotalCount == 0 || itemIndex < 0)
			{
				return;
			}
			this.CheckAllGroupIfNeedUpdateItemPos();
			this.UpdateContentSize();
			float viewPortSize = this.ViewPortSize;
			float contentSize = this.GetContentSize();
			if (contentSize <= viewPortSize)
			{
				if (this.IsVertList)
				{
					this.SetAnchoredPositionY(this.mContainerTrans, 0f);
				}
				else
				{
					this.SetAnchoredPositionX(this.mContainerTrans, 0f);
				}
				return;
			}
			if (itemIndex >= this.mItemTotalCount)
			{
				itemIndex = this.mItemTotalCount - 1;
			}
			float itemAbsPosByItemIndex = this.GetItemAbsPosByItemIndex(itemIndex);
			if (itemAbsPosByItemIndex < 0f)
			{
				return;
			}
			if (this.IsVertList)
			{
				float num = (float)((this.mArrangeType != ListItemArrangeType.TopToBottom) ? -1 : 1);
				float num2 = itemAbsPosByItemIndex + offset;
				if (num2 < 0f)
				{
					num2 = 0f;
				}
				if (contentSize - num2 >= viewPortSize)
				{
					this.SetAnchoredPositionY(this.mContainerTrans, num * num2);
				}
				else
				{
					this.SetAnchoredPositionY(this.mContainerTrans, num * (contentSize - viewPortSize));
					this.UpdateListView(viewPortSize + 100f, viewPortSize + 100f, viewPortSize, viewPortSize);
					this.ClearAllTmpRecycledItem();
					this.UpdateContentSize();
					contentSize = this.GetContentSize();
					if (contentSize - num2 >= viewPortSize)
					{
						this.SetAnchoredPositionY(this.mContainerTrans, num * num2);
					}
					else
					{
						this.SetAnchoredPositionY(this.mContainerTrans, num * (contentSize - viewPortSize));
					}
				}
			}
			else
			{
				float num3 = (float)((this.mArrangeType != ListItemArrangeType.RightToLeft) ? -1 : 1);
				float num4 = itemAbsPosByItemIndex + offset;
				if (num4 < 0f)
				{
					num4 = 0f;
				}
				if (contentSize - num4 >= viewPortSize)
				{
					this.SetAnchoredPositionX(this.mContainerTrans, num3 * num4);
				}
				else
				{
					this.SetAnchoredPositionX(this.mContainerTrans, num3 * (contentSize - viewPortSize));
					this.UpdateListView(viewPortSize + 100f, viewPortSize + 100f, viewPortSize, viewPortSize);
					this.ClearAllTmpRecycledItem();
					this.UpdateContentSize();
					contentSize = this.GetContentSize();
					if (contentSize - num4 >= viewPortSize)
					{
						this.SetAnchoredPositionX(this.mContainerTrans, num3 * num4);
					}
					else
					{
						this.SetAnchoredPositionX(this.mContainerTrans, num3 * (contentSize - viewPortSize));
					}
				}
			}
		}

		public LoopStaggeredGridViewItem GetShownItemByItemIndex(int itemIndex)
		{
			ItemIndexData itemIndexData = this.GetItemIndexData(itemIndex);
			if (itemIndexData == null)
			{
				return null;
			}
			StaggeredGridItemGroup itemGroupByIndex = this.GetItemGroupByIndex(itemIndexData.mGroupIndex);
			return itemGroupByIndex.GetShownItemByIndexInGroup(itemIndexData.mIndexInGroup);
		}

		public void RefreshAllShownItem()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].RefreshAllShownItem();
			}
		}

		public void OnItemSizeChanged(int itemIndex)
		{
			ItemIndexData itemIndexData = this.GetItemIndexData(itemIndex);
			if (itemIndexData == null)
			{
				return;
			}
			StaggeredGridItemGroup itemGroupByIndex = this.GetItemGroupByIndex(itemIndexData.mGroupIndex);
			itemGroupByIndex.OnItemSizeChanged(itemIndexData.mIndexInGroup);
		}

		public void RefreshItemByItemIndex(int itemIndex)
		{
			ItemIndexData itemIndexData = this.GetItemIndexData(itemIndex);
			if (itemIndexData == null)
			{
				return;
			}
			StaggeredGridItemGroup itemGroupByIndex = this.GetItemGroupByIndex(itemIndexData.mGroupIndex);
			itemGroupByIndex.RefreshItemByIndexInGroup(itemIndexData.mIndexInGroup);
		}

		public void ResetListView(bool resetPos = true)
		{
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			if (resetPos)
			{
				this.mContainerTrans.anchoredPosition3D = Vector3.zero;
			}
		}

		public float ViewPortSize
		{
			get
			{
				if (this.mIsVertList)
				{
					return this.mViewPortRectTransform.rect.height;
				}
				return this.mViewPortRectTransform.rect.width;
			}
		}

		public float ViewPortWidth
		{
			get
			{
				return this.mViewPortRectTransform.rect.width;
			}
		}

		public float ViewPortHeight
		{
			get
			{
				return this.mViewPortRectTransform.rect.height;
			}
		}

		public void RecycleAllItem()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].RecycleAllItem();
			}
		}

		public void RecycleItemTmp(LoopStaggeredGridViewItem item)
		{
			if (item == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(item.ItemPrefabName))
			{
				return;
			}
			StaggeredGridItemPool staggeredGridItemPool = null;
			if (!this.mItemPoolDict.TryGetValue(item.ItemPrefabName, out staggeredGridItemPool))
			{
				return;
			}
			staggeredGridItemPool.RecycleItem(item);
		}

		public void ClearAllTmpRecycledItem()
		{
			int count = this.mItemPoolList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemPoolList[i].ClearTmpRecycledItem();
			}
		}

		private void AdjustContainerPivot(RectTransform rtf)
		{
			Vector2 pivot = rtf.pivot;
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				pivot.y = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				pivot.y = 1f;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				pivot.x = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				pivot.x = 1f;
			}
			rtf.pivot = pivot;
		}

		private void AdjustPivot(RectTransform rtf)
		{
			Vector2 pivot = rtf.pivot;
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				pivot.y = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				pivot.y = 1f;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				pivot.x = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				pivot.x = 1f;
			}
			rtf.pivot = pivot;
		}

		private void AdjustContainerAnchor(RectTransform rtf)
		{
			Vector2 anchorMin = rtf.anchorMin;
			Vector2 anchorMax = rtf.anchorMax;
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				anchorMin.y = 0f;
				anchorMax.y = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				anchorMin.y = 1f;
				anchorMax.y = 1f;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				anchorMin.x = 0f;
				anchorMax.x = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				anchorMin.x = 1f;
				anchorMax.x = 1f;
			}
			rtf.anchorMin = anchorMin;
			rtf.anchorMax = anchorMax;
		}

		private void AdjustAnchor(RectTransform rtf)
		{
			Vector2 anchorMin = rtf.anchorMin;
			Vector2 anchorMax = rtf.anchorMax;
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				anchorMin.y = 0f;
				anchorMax.y = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				anchorMin.y = 1f;
				anchorMax.y = 1f;
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				anchorMin.x = 0f;
				anchorMax.x = 0f;
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				anchorMin.x = 1f;
				anchorMax.x = 1f;
			}
			rtf.anchorMin = anchorMin;
			rtf.anchorMax = anchorMax;
		}

		private void InitItemPool()
		{
			foreach (StaggeredGridItemPrefabConfData staggeredGridItemPrefabConfData in this.mItemPrefabDataList)
			{
				if (staggeredGridItemPrefabConfData.mItemPrefab == null)
				{
					UnityEngine.Debug.LogError("A item prefab is null ");
				}
				else
				{
					string name = staggeredGridItemPrefabConfData.mItemPrefab.name;
					if (this.mItemPoolDict.ContainsKey(name))
					{
						UnityEngine.Debug.LogError("A item prefab with name " + name + " has existed!");
					}
					else
					{
						RectTransform component = staggeredGridItemPrefabConfData.mItemPrefab.GetComponent<RectTransform>();
						if (component == null)
						{
							UnityEngine.Debug.LogError("RectTransform component is not found in the prefab " + name);
						}
						else
						{
							this.AdjustAnchor(component);
							this.AdjustPivot(component);
							LoopStaggeredGridViewItem component2 = staggeredGridItemPrefabConfData.mItemPrefab.GetComponent<LoopStaggeredGridViewItem>();
							if (component2 == null)
							{
								staggeredGridItemPrefabConfData.mItemPrefab.AddComponent<LoopStaggeredGridViewItem>();
							}
							StaggeredGridItemPool staggeredGridItemPool = new StaggeredGridItemPool();
							staggeredGridItemPool.Init(staggeredGridItemPrefabConfData.mItemPrefab, staggeredGridItemPrefabConfData.mPadding, staggeredGridItemPrefabConfData.mInitCreateCount, this.mContainerTrans);
							this.mItemPoolDict.Add(name, staggeredGridItemPool);
							this.mItemPoolList.Add(staggeredGridItemPool);
						}
					}
				}
			}
		}

		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.mIsDraging = true;
			this.CacheDragPointerEventData(eventData);
			if (this.mOnBeginDragAction != null)
			{
				this.mOnBeginDragAction();
			}
		}

		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.mIsDraging = false;
			this.mPointerEventData = null;
			if (this.mOnEndDragAction != null)
			{
				this.mOnEndDragAction();
			}
		}

		public virtual void OnDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.CacheDragPointerEventData(eventData);
			if (this.mOnDragingAction != null)
			{
				this.mOnDragingAction();
			}
		}

		private void CacheDragPointerEventData(PointerEventData eventData)
		{
			if (this.mPointerEventData == null)
			{
				this.mPointerEventData = new PointerEventData(EventSystem.current);
			}
			this.mPointerEventData.button = eventData.button;
			this.mPointerEventData.position = eventData.position;
			this.mPointerEventData.pointerPressRaycast = eventData.pointerPressRaycast;
			this.mPointerEventData.pointerCurrentRaycast = eventData.pointerCurrentRaycast;
		}

		public int CurMaxCreatedItemIndexCount
		{
			get
			{
				return this.mItemIndexDataList.Count;
			}
		}

		private void SetAnchoredPositionX(RectTransform rtf, float x)
		{
			Vector3 anchoredPosition3D = rtf.anchoredPosition3D;
			anchoredPosition3D.x = x;
			rtf.anchoredPosition3D = anchoredPosition3D;
		}

		private void SetAnchoredPositionY(RectTransform rtf, float y)
		{
			Vector3 anchoredPosition3D = rtf.anchoredPosition3D;
			anchoredPosition3D.y = y;
			rtf.anchoredPosition3D = anchoredPosition3D;
		}

		public ItemIndexData GetItemIndexData(int itemIndex)
		{
			int count = this.mItemIndexDataList.Count;
			if (itemIndex < 0 || itemIndex >= count)
			{
				return null;
			}
			return this.mItemIndexDataList[itemIndex];
		}

		public void UpdateAllGroupShownItemsPos()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].UpdateAllShownItemsPos();
			}
		}

		private void CheckAllGroupIfNeedUpdateItemPos()
		{
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].CheckIfNeedUpdateItemPos();
			}
		}

		public float GetItemAbsPosByItemIndex(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= this.mItemIndexDataList.Count)
			{
				return -1f;
			}
			ItemIndexData itemIndexData = this.mItemIndexDataList[itemIndex];
			return this.mItemGroupList[itemIndexData.mGroupIndex].GetItemPos(itemIndexData.mIndexInGroup);
		}

		public LoopStaggeredGridViewItem GetNewItemByGroupAndIndex(int groupIndex, int indexInGroup)
		{
			if (indexInGroup < 0)
			{
				return null;
			}
			if (this.mItemTotalCount == 0)
			{
				return null;
			}
			List<int> itemIndexMap = this.mItemGroupList[groupIndex].ItemIndexMap;
			int count = itemIndexMap.Count;
			if (count > indexInGroup)
			{
				int num = itemIndexMap[indexInGroup];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mOnGetItemByItemIndex(this, num);
				if (loopStaggeredGridViewItem == null)
				{
					return null;
				}
				loopStaggeredGridViewItem.StartPosOffset = this.mLayoutParam.mCustomColumnOrRowOffsetArray[groupIndex];
				loopStaggeredGridViewItem.ItemIndexInGroup = indexInGroup;
				loopStaggeredGridViewItem.ItemIndex = num;
				loopStaggeredGridViewItem.ItemCreatedCheckFrameCount = this.mListUpdateCheckFrameCount;
				return loopStaggeredGridViewItem;
			}
			else
			{
				if (count != indexInGroup)
				{
					return null;
				}
				int count2 = this.mItemIndexDataList.Count;
				if (count2 >= this.mItemTotalCount)
				{
					return null;
				}
				int num = count2;
				LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mOnGetItemByItemIndex(this, num);
				if (loopStaggeredGridViewItem == null)
				{
					return null;
				}
				itemIndexMap.Add(num);
				ItemIndexData itemIndexData = new ItemIndexData();
				itemIndexData.mGroupIndex = groupIndex;
				itemIndexData.mIndexInGroup = indexInGroup;
				this.mItemIndexDataList.Add(itemIndexData);
				loopStaggeredGridViewItem.StartPosOffset = this.mLayoutParam.mCustomColumnOrRowOffsetArray[groupIndex];
				loopStaggeredGridViewItem.ItemIndexInGroup = indexInGroup;
				loopStaggeredGridViewItem.ItemIndex = num;
				loopStaggeredGridViewItem.ItemCreatedCheckFrameCount = this.mListUpdateCheckFrameCount;
				return loopStaggeredGridViewItem;
			}
		}

		private int GetCurShouldAddNewItemGroupIndex()
		{
			float num = float.MaxValue;
			int count = this.mItemGroupList.Count;
			int result = 0;
			for (int i = 0; i < count; i++)
			{
				float shownItemPosMaxValue = this.mItemGroupList[i].GetShownItemPosMaxValue();
				if (shownItemPosMaxValue < num)
				{
					num = shownItemPosMaxValue;
					result = i;
				}
			}
			return result;
		}

		public void UpdateListViewWithDefault()
		{
			this.UpdateListView(this.mDistanceForRecycle0, this.mDistanceForRecycle1, this.mDistanceForNew0, this.mDistanceForNew1);
			this.UpdateContentSize();
		}

		private void Update()
		{
			if (!this.mListViewInited)
			{
				return;
			}
			this.UpdateListViewWithDefault();
			this.ClearAllTmpRecycledItem();
			this.mLastFrameContainerPos = this.mContainerTrans.anchoredPosition3D;
		}

		public void UpdateListView(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			this.mListUpdateCheckFrameCount++;
			bool flag = true;
			int num = 0;
			int num2 = 9999;
			int count = this.mItemGroupList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemGroupList[i].UpdateListViewPart1(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
			}
			while (flag)
			{
				num++;
				if (num >= num2)
				{
					UnityEngine.Debug.LogError("UpdateListView while loop " + num + " times! something is wrong!");
					break;
				}
				int curShouldAddNewItemGroupIndex = this.GetCurShouldAddNewItemGroupIndex();
				flag = this.mItemGroupList[curShouldAddNewItemGroupIndex].UpdateListViewPart2(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
			}
		}

		public float GetContentSize()
		{
			if (this.mIsVertList)
			{
				return this.mContainerTrans.rect.height;
			}
			return this.mContainerTrans.rect.width;
		}

		public void UpdateContentSize()
		{
			int count = this.mItemGroupList.Count;
			float num = 0f;
			for (int i = 0; i < count; i++)
			{
				float contentPanelSize = this.mItemGroupList[i].GetContentPanelSize();
				if (contentPanelSize > num)
				{
					num = contentPanelSize;
				}
			}
			if (this.mIsVertList)
			{
				if (this.mContainerTrans.rect.height != num)
				{
					this.mContainerTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num);
				}
			}
			else if (this.mContainerTrans.rect.width != num)
			{
				this.mContainerTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, num);
			}
		}

		private Dictionary<string, StaggeredGridItemPool> mItemPoolDict = new Dictionary<string, StaggeredGridItemPool>();

		private List<StaggeredGridItemPool> mItemPoolList = new List<StaggeredGridItemPool>();

		[SerializeField]
		private List<StaggeredGridItemPrefabConfData> mItemPrefabDataList = new List<StaggeredGridItemPrefabConfData>();

		[SerializeField]
		private ListItemArrangeType mArrangeType;

		private RectTransform mContainerTrans;

		private ScrollRect mScrollRect;

		private int mGroupCount;

		private List<StaggeredGridItemGroup> mItemGroupList = new List<StaggeredGridItemGroup>();

		private List<ItemIndexData> mItemIndexDataList = new List<ItemIndexData>();

		private RectTransform mScrollRectTransform;

		private RectTransform mViewPortRectTransform;

		private float mItemDefaultWithPaddingSize = 20f;

		private int mItemTotalCount;

		private bool mIsVertList;

		private Func<LoopStaggeredGridView, int, LoopStaggeredGridViewItem> mOnGetItemByItemIndex;

		private Vector3[] mItemWorldCorners = new Vector3[4];

		private Vector3[] mViewPortRectLocalCorners = new Vector3[4];

		private float mDistanceForRecycle0 = 300f;

		private float mDistanceForNew0 = 200f;

		private float mDistanceForRecycle1 = 300f;

		private float mDistanceForNew1 = 200f;

		private bool mIsDraging;

		private PointerEventData mPointerEventData;

		public Action mOnBeginDragAction;

		public Action mOnDragingAction;

		public Action mOnEndDragAction;

		private Vector3 mLastFrameContainerPos = Vector3.zero;

		private bool mListViewInited;

		private int mListUpdateCheckFrameCount;

		private GridViewLayoutParam mLayoutParam;
	}
}
