using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class StaggeredGridItemGroup
	{
		public void Init(LoopStaggeredGridView parent, int itemTotalCount, int groupIndex, Func<int, int, LoopStaggeredGridViewItem> onGetItemByIndex)
		{
			this.mGroupIndex = groupIndex;
			this.mParentGridView = parent;
			this.mArrangeType = this.mParentGridView.ArrangeType;
			this.mGameObject = this.mParentGridView.gameObject;
			this.mScrollRect = this.mGameObject.GetComponent<ScrollRect>();
			this.mItemPosMgr = new ItemPosMgr(this.mItemDefaultWithPaddingSize);
			this.mScrollRectTransform = this.mScrollRect.GetComponent<RectTransform>();
			this.mContainerTrans = this.mScrollRect.content;
			this.mViewPortRectTransform = this.mScrollRect.viewport;
			if (this.mViewPortRectTransform == null)
			{
				this.mViewPortRectTransform = this.mScrollRectTransform;
			}
			this.mIsVertList = (this.mArrangeType == ListItemArrangeType.TopToBottom || this.mArrangeType == ListItemArrangeType.BottomToTop);
			this.mOnGetItemByIndex = onGetItemByIndex;
			this.mItemTotalCount = itemTotalCount;
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
			if (this.mItemTotalCount < 0)
			{
				this.mSupportScrollBar = false;
			}
			if (this.mSupportScrollBar)
			{
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			else
			{
				this.mItemPosMgr.SetItemMaxCount(0);
			}
			this.mCurReadyMaxItemIndex = 0;
			this.mCurReadyMinItemIndex = 0;
			this.mNeedCheckNextMaxItem = true;
			this.mNeedCheckNextMinItem = true;
		}

		public List<int> ItemIndexMap
		{
			get
			{
				return this.mItemIndexMap;
			}
		}

		public void ResetListView()
		{
			this.mViewPortRectTransform.GetLocalCorners(this.mViewPortRectLocalCorners);
		}

		public LoopStaggeredGridViewItem GetShownItemByItemIndex(int itemIndex)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return null;
			}
			if (itemIndex < this.mItemList[0].ItemIndex || itemIndex > this.mItemList[count - 1].ItemIndex)
			{
				return null;
			}
			for (int i = 0; i < count; i++)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[i];
				if (loopStaggeredGridViewItem.ItemIndex == itemIndex)
				{
					return loopStaggeredGridViewItem;
				}
			}
			return null;
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

		private bool IsDraging
		{
			get
			{
				return this.mParentGridView.IsDraging;
			}
		}

		public LoopStaggeredGridViewItem GetShownItemByIndexInGroup(int indexInGroup)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return null;
			}
			if (indexInGroup < this.mItemList[0].ItemIndexInGroup || indexInGroup > this.mItemList[count - 1].ItemIndexInGroup)
			{
				return null;
			}
			int index = indexInGroup - this.mItemList[0].ItemIndexInGroup;
			return this.mItemList[index];
		}

		public int GetIndexInShownItemList(LoopStaggeredGridViewItem item)
		{
			if (item == null)
			{
				return -1;
			}
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return -1;
			}
			for (int i = 0; i < count; i++)
			{
				if (this.mItemList[i] == item)
				{
					return i;
				}
			}
			return -1;
		}

		public void RefreshAllShownItem()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			this.RefreshAllShownItemWithFirstIndexInGroup(this.mItemList[0].ItemIndexInGroup);
		}

		public void OnItemSizeChanged(int indexInGroup)
		{
			LoopStaggeredGridViewItem shownItemByIndexInGroup = this.GetShownItemByIndexInGroup(indexInGroup);
			if (shownItemByIndexInGroup == null)
			{
				return;
			}
			if (this.mSupportScrollBar)
			{
				if (this.mIsVertList)
				{
					this.SetItemSize(indexInGroup, shownItemByIndexInGroup.CachedRectTransform.rect.height, shownItemByIndexInGroup.Padding);
				}
				else
				{
					this.SetItemSize(indexInGroup, shownItemByIndexInGroup.CachedRectTransform.rect.width, shownItemByIndexInGroup.Padding);
				}
			}
			this.UpdateAllShownItemsPos();
		}

		public void RefreshItemByIndexInGroup(int indexInGroup)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			if (indexInGroup < this.mItemList[0].ItemIndexInGroup || indexInGroup > this.mItemList[count - 1].ItemIndexInGroup)
			{
				return;
			}
			int itemIndexInGroup = this.mItemList[0].ItemIndexInGroup;
			int index = indexInGroup - itemIndexInGroup;
			LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[index];
			Vector3 anchoredPosition3D = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D;
			this.RecycleItemTmp(loopStaggeredGridViewItem);
			LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(indexInGroup);
			if (newItemByIndexInGroup == null)
			{
				this.RefreshAllShownItemWithFirstIndexInGroup(itemIndexInGroup);
				return;
			}
			this.mItemList[index] = newItemByIndexInGroup;
			if (this.mIsVertList)
			{
				anchoredPosition3D.x = newItemByIndexInGroup.StartPosOffset;
			}
			else
			{
				anchoredPosition3D.y = newItemByIndexInGroup.StartPosOffset;
			}
			newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
			this.OnItemSizeChanged(indexInGroup);
			this.ClearAllTmpRecycledItem();
		}

		public void RefreshAllShownItemWithFirstIndexInGroup(int firstItemIndexInGroup)
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[0];
			Vector3 anchoredPosition3D = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D;
			this.RecycleAllItem();
			for (int i = 0; i < count; i++)
			{
				int num = firstItemIndexInGroup + i;
				LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num);
				if (newItemByIndexInGroup == null)
				{
					break;
				}
				if (this.mIsVertList)
				{
					anchoredPosition3D.x = newItemByIndexInGroup.StartPosOffset;
				}
				else
				{
					anchoredPosition3D.y = newItemByIndexInGroup.StartPosOffset;
				}
				newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = anchoredPosition3D;
				if (this.mSupportScrollBar)
				{
					if (this.mIsVertList)
					{
						this.SetItemSize(num, newItemByIndexInGroup.CachedRectTransform.rect.height, newItemByIndexInGroup.Padding);
					}
					else
					{
						this.SetItemSize(num, newItemByIndexInGroup.CachedRectTransform.rect.width, newItemByIndexInGroup.Padding);
					}
				}
				this.mItemList.Add(newItemByIndexInGroup);
			}
			this.UpdateAllShownItemsPos();
			this.ClearAllTmpRecycledItem();
		}

		public void RefreshAllShownItemWithFirstIndexAndPos(int firstItemIndexInGroup, Vector3 pos)
		{
			this.RecycleAllItem();
			LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(firstItemIndexInGroup);
			if (newItemByIndexInGroup == null)
			{
				return;
			}
			if (this.mIsVertList)
			{
				pos.x = newItemByIndexInGroup.StartPosOffset;
			}
			else
			{
				pos.y = newItemByIndexInGroup.StartPosOffset;
			}
			newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = pos;
			if (this.mSupportScrollBar)
			{
				if (this.mIsVertList)
				{
					this.SetItemSize(firstItemIndexInGroup, newItemByIndexInGroup.CachedRectTransform.rect.height, newItemByIndexInGroup.Padding);
				}
				else
				{
					this.SetItemSize(firstItemIndexInGroup, newItemByIndexInGroup.CachedRectTransform.rect.width, newItemByIndexInGroup.Padding);
				}
			}
			this.mItemList.Add(newItemByIndexInGroup);
			this.UpdateAllShownItemsPos();
			this.mParentGridView.UpdateListViewWithDefault();
			this.ClearAllTmpRecycledItem();
		}

		private void SetItemSize(int itemIndex, float itemSize, float padding)
		{
			this.mItemPosMgr.SetItemSize(itemIndex, itemSize + padding);
			if (itemIndex >= this.mLastItemIndex)
			{
				this.mLastItemIndex = itemIndex;
				this.mLastItemPadding = padding;
			}
		}

		private bool GetPlusItemIndexAndPosAtGivenPos(float pos, ref int index, ref float itemPos)
		{
			return this.mItemPosMgr.GetItemIndexAndPosAtGivenPos(pos, ref index, ref itemPos);
		}

		public float GetItemPos(int itemIndex)
		{
			return this.mItemPosMgr.GetItemPos(itemIndex);
		}

		public Vector3 GetItemCornerPosInViewPort(LoopStaggeredGridViewItem item, ItemCornerEnum corner = ItemCornerEnum.LeftBottom)
		{
			item.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
			return this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[(int)corner]);
		}

		public void RecycleItemTmp(LoopStaggeredGridViewItem item)
		{
			this.mParentGridView.RecycleItemTmp(item);
		}

		public void RecycleAllItem()
		{
			foreach (LoopStaggeredGridViewItem item in this.mItemList)
			{
				this.RecycleItemTmp(item);
			}
			this.mItemList.Clear();
		}

		public void ClearAllTmpRecycledItem()
		{
			this.mParentGridView.ClearAllTmpRecycledItem();
		}

		private LoopStaggeredGridViewItem GetNewItemByIndexInGroup(int indexInGroup)
		{
			return this.mParentGridView.GetNewItemByGroupAndIndex(this.mGroupIndex, indexInGroup);
		}

		public int HadCreatedItemCount
		{
			get
			{
				return this.mItemIndexMap.Count;
			}
		}

		public void SetListItemCount(int itemCount)
		{
			if (itemCount == this.mItemTotalCount)
			{
				return;
			}
			int num = this.mItemTotalCount;
			this.mItemTotalCount = itemCount;
			this.UpdateItemIndexMap(num);
			if (num < this.mItemTotalCount)
			{
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			else
			{
				this.mItemPosMgr.SetItemMaxCount(this.HadCreatedItemCount);
				this.mItemPosMgr.SetItemMaxCount(this.mItemTotalCount);
			}
			this.RecycleAllItem();
			if (this.mItemTotalCount == 0)
			{
				this.mCurReadyMaxItemIndex = 0;
				this.mCurReadyMinItemIndex = 0;
				this.mNeedCheckNextMaxItem = false;
				this.mNeedCheckNextMinItem = false;
				this.mItemIndexMap.Clear();
				return;
			}
			if (this.mCurReadyMaxItemIndex >= this.mItemTotalCount)
			{
				this.mCurReadyMaxItemIndex = this.mItemTotalCount - 1;
			}
			this.mNeedCheckNextMaxItem = true;
			this.mNeedCheckNextMinItem = true;
		}

		private void UpdateItemIndexMap(int oldItemTotalCount)
		{
			int count = this.mItemIndexMap.Count;
			if (count == 0)
			{
				return;
			}
			if (this.mItemTotalCount == 0)
			{
				this.mItemIndexMap.Clear();
				return;
			}
			if (this.mItemTotalCount >= oldItemTotalCount)
			{
				return;
			}
			int itemTotalCount = this.mParentGridView.ItemTotalCount;
			if (this.mItemIndexMap[count - 1] < itemTotalCount)
			{
				return;
			}
			int i = 0;
			int num = count - 1;
			int num2 = 0;
			while (i <= num)
			{
				int num3 = (i + num) / 2;
				int num4 = this.mItemIndexMap[num3];
				if (num4 == itemTotalCount)
				{
					num2 = num3;
					break;
				}
				if (num4 >= itemTotalCount)
				{
					break;
				}
				i = num3 + 1;
				num2 = i;
			}
			int num5 = 0;
			for (int j = num2; j < count; j++)
			{
				if (this.mItemIndexMap[j] >= itemTotalCount)
				{
					num5 = j;
					break;
				}
			}
			this.mItemIndexMap.RemoveRange(num5, count - num5);
		}

		public void UpdateListViewPart1(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mSupportScrollBar)
			{
				this.mItemPosMgr.Update(false);
			}
			this.mListUpdateCheckFrameCount = this.mParentGridView.ListUpdateCheckFrameCount;
			bool flag = true;
			int num = 0;
			int num2 = 9999;
			while (flag)
			{
				num++;
				if (num >= num2)
				{
					UnityEngine.Debug.LogError("UpdateListViewPart1 while loop " + num + " times! something is wrong!");
					break;
				}
				if (this.mIsVertList)
				{
					flag = this.UpdateForVertListPart1(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
				}
				else
				{
					flag = this.UpdateForHorizontalListPart1(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
				}
			}
			this.mLastFrameContainerPos = this.mContainerTrans.anchoredPosition3D;
		}

		public bool UpdateListViewPart2(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mIsVertList)
			{
				return this.UpdateForVertListPart2(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
			}
			return this.UpdateForHorizontalListPart2(distanceForRecycle0, distanceForRecycle1, distanceForNew0, distanceForNew1);
		}

		public bool UpdateForVertListPart1(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mItemTotalCount == 0)
			{
				if (this.mItemList.Count > 0)
				{
					this.RecycleAllItem();
				}
				return false;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				if (this.mItemList.Count == 0)
				{
					float num = this.mContainerTrans.anchoredPosition3D.y;
					if (num < 0f)
					{
						num = 0f;
					}
					int num2 = 0;
					float num3 = -num;
					if (this.mSupportScrollBar)
					{
						if (!this.GetPlusItemIndexAndPosAtGivenPos(num, ref num2, ref num3))
						{
							return false;
						}
						num3 = -num3;
					}
					LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num2);
					if (newItemByIndexInGroup == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndexInGroup.CachedRectTransform.rect.height, newItemByIndexInGroup.Padding);
					}
					this.mItemList.Add(newItemByIndexInGroup);
					newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup.StartPosOffset, num3, 0f);
					return true;
				}
				else
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[0];
					loopStaggeredGridViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (!this.IsDraging && loopStaggeredGridViewItem.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector2.y - this.mViewPortRectLocalCorners[1].y > distanceForRecycle0)
					{
						this.mItemList.RemoveAt(0);
						this.RecycleItemTmp(loopStaggeredGridViewItem);
						if (!this.mSupportScrollBar)
						{
							this.CheckIfNeedUpdateItemPos();
						}
						return true;
					}
					LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
					loopStaggeredGridViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector3 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (!this.IsDraging && loopStaggeredGridViewItem2.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[0].y - vector3.y > distanceForRecycle1)
					{
						this.mItemList.RemoveAt(this.mItemList.Count - 1);
						this.RecycleItemTmp(loopStaggeredGridViewItem2);
						if (!this.mSupportScrollBar)
						{
							this.CheckIfNeedUpdateItemPos();
						}
						return true;
					}
					if (vector.y - this.mViewPortRectLocalCorners[1].y < distanceForNew0)
					{
						if (loopStaggeredGridViewItem.ItemIndexInGroup < this.mCurReadyMinItemIndex)
						{
							this.mCurReadyMinItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMinItem = true;
						}
						int num4 = loopStaggeredGridViewItem.ItemIndexInGroup - 1;
						if (num4 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup2 = this.GetNewItemByIndexInGroup(num4);
							if (!(newItemByIndexInGroup2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num4, newItemByIndexInGroup2.CachedRectTransform.rect.height, newItemByIndexInGroup2.Padding);
								}
								this.mItemList.Insert(0, newItemByIndexInGroup2);
								float y = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D.y + newItemByIndexInGroup2.CachedRectTransform.rect.height + newItemByIndexInGroup2.Padding;
								newItemByIndexInGroup2.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup2.StartPosOffset, y, 0f);
								this.CheckIfNeedUpdateItemPos();
								if (num4 < this.mCurReadyMinItemIndex)
								{
									this.mCurReadyMinItemIndex = num4;
								}
								return true;
							}
							this.mCurReadyMinItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMinItem = false;
						}
					}
					if (this.mViewPortRectLocalCorners[0].y - vector4.y < distanceForNew1)
					{
						if (loopStaggeredGridViewItem2.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = true;
						}
						int num5 = loopStaggeredGridViewItem2.ItemIndexInGroup + 1;
						if (num5 >= this.mItemIndexMap.Count)
						{
							return false;
						}
						if (num5 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup3 = this.GetNewItemByIndexInGroup(num5);
							if (newItemByIndexInGroup3 == null)
							{
								this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
								this.mNeedCheckNextMaxItem = false;
								this.CheckIfNeedUpdateItemPos();
								return false;
							}
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num5, newItemByIndexInGroup3.CachedRectTransform.rect.height, newItemByIndexInGroup3.Padding);
							}
							this.mItemList.Add(newItemByIndexInGroup3);
							float y2 = loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D.y - loopStaggeredGridViewItem2.CachedRectTransform.rect.height - loopStaggeredGridViewItem2.Padding;
							newItemByIndexInGroup3.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup3.StartPosOffset, y2, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num5 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num5;
							}
							return true;
						}
					}
				}
			}
			else if (this.mItemList.Count == 0)
			{
				float num6 = this.mContainerTrans.anchoredPosition3D.y;
				if (num6 > 0f)
				{
					num6 = 0f;
				}
				int num7 = 0;
				float y3 = -num6;
				if (this.mSupportScrollBar && !this.GetPlusItemIndexAndPosAtGivenPos(-num6, ref num7, ref y3))
				{
					return false;
				}
				LoopStaggeredGridViewItem newItemByIndexInGroup4 = this.GetNewItemByIndexInGroup(num7);
				if (newItemByIndexInGroup4 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num7, newItemByIndexInGroup4.CachedRectTransform.rect.height, newItemByIndexInGroup4.Padding);
				}
				this.mItemList.Add(newItemByIndexInGroup4);
				newItemByIndexInGroup4.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup4.StartPosOffset, y3, 0f);
				return true;
			}
			else
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem3 = this.mItemList[0];
				loopStaggeredGridViewItem3.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector5 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector6 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
				if (!this.IsDraging && loopStaggeredGridViewItem3.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[0].y - vector5.y > distanceForRecycle0)
				{
					this.mItemList.RemoveAt(0);
					this.RecycleItemTmp(loopStaggeredGridViewItem3);
					if (!this.mSupportScrollBar)
					{
						this.CheckIfNeedUpdateItemPos();
					}
					return true;
				}
				LoopStaggeredGridViewItem loopStaggeredGridViewItem4 = this.mItemList[this.mItemList.Count - 1];
				loopStaggeredGridViewItem4.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector7 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector8 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
				if (!this.IsDraging && loopStaggeredGridViewItem4.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector8.y - this.mViewPortRectLocalCorners[1].y > distanceForRecycle1)
				{
					this.mItemList.RemoveAt(this.mItemList.Count - 1);
					this.RecycleItemTmp(loopStaggeredGridViewItem4);
					if (!this.mSupportScrollBar)
					{
						this.CheckIfNeedUpdateItemPos();
					}
					return true;
				}
				if (this.mViewPortRectLocalCorners[0].y - vector6.y < distanceForNew0)
				{
					if (loopStaggeredGridViewItem3.ItemIndexInGroup < this.mCurReadyMinItemIndex)
					{
						this.mCurReadyMinItemIndex = loopStaggeredGridViewItem3.ItemIndexInGroup;
						this.mNeedCheckNextMinItem = true;
					}
					int num8 = loopStaggeredGridViewItem3.ItemIndexInGroup - 1;
					if (num8 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup5 = this.GetNewItemByIndexInGroup(num8);
						if (!(newItemByIndexInGroup5 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num8, newItemByIndexInGroup5.CachedRectTransform.rect.height, newItemByIndexInGroup5.Padding);
							}
							this.mItemList.Insert(0, newItemByIndexInGroup5);
							float y4 = loopStaggeredGridViewItem3.CachedRectTransform.anchoredPosition3D.y - newItemByIndexInGroup5.CachedRectTransform.rect.height - newItemByIndexInGroup5.Padding;
							newItemByIndexInGroup5.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup5.StartPosOffset, y4, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num8 < this.mCurReadyMinItemIndex)
							{
								this.mCurReadyMinItemIndex = num8;
							}
							return true;
						}
						this.mCurReadyMinItemIndex = loopStaggeredGridViewItem3.ItemIndexInGroup;
						this.mNeedCheckNextMinItem = false;
					}
				}
				if (vector7.y - this.mViewPortRectLocalCorners[1].y < distanceForNew1)
				{
					if (loopStaggeredGridViewItem4.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem4.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = true;
					}
					int num9 = loopStaggeredGridViewItem4.ItemIndexInGroup + 1;
					if (num9 >= this.mItemIndexMap.Count)
					{
						return false;
					}
					if (num9 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup6 = this.GetNewItemByIndexInGroup(num9);
						if (newItemByIndexInGroup6 == null)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem4.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdateItemPos();
							return false;
						}
						if (this.mSupportScrollBar)
						{
							this.SetItemSize(num9, newItemByIndexInGroup6.CachedRectTransform.rect.height, newItemByIndexInGroup6.Padding);
						}
						this.mItemList.Add(newItemByIndexInGroup6);
						float y5 = loopStaggeredGridViewItem4.CachedRectTransform.anchoredPosition3D.y + loopStaggeredGridViewItem4.CachedRectTransform.rect.height + loopStaggeredGridViewItem4.Padding;
						newItemByIndexInGroup6.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup6.StartPosOffset, y5, 0f);
						this.CheckIfNeedUpdateItemPos();
						if (num9 > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = num9;
						}
						return true;
					}
				}
			}
			return false;
		}

		public bool UpdateForVertListPart2(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mItemTotalCount == 0)
			{
				if (this.mItemList.Count > 0)
				{
					this.RecycleAllItem();
				}
				return false;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				if (this.mItemList.Count == 0)
				{
					float num = this.mContainerTrans.anchoredPosition3D.y;
					if (num < 0f)
					{
						num = 0f;
					}
					int num2 = 0;
					float num3 = -num;
					if (this.mSupportScrollBar)
					{
						if (!this.GetPlusItemIndexAndPosAtGivenPos(num, ref num2, ref num3))
						{
							return false;
						}
						num3 = -num3;
					}
					LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num2);
					if (newItemByIndexInGroup == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndexInGroup.CachedRectTransform.rect.height, newItemByIndexInGroup.Padding);
					}
					this.mItemList.Add(newItemByIndexInGroup);
					newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup.StartPosOffset, num3, 0f);
					return true;
				}
				else
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[this.mItemList.Count - 1];
					loopStaggeredGridViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[0]);
					if (this.mViewPortRectLocalCorners[0].y - vector.y < distanceForNew1)
					{
						if (loopStaggeredGridViewItem.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = true;
						}
						int num4 = loopStaggeredGridViewItem.ItemIndexInGroup + 1;
						if (num4 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup2 = this.GetNewItemByIndexInGroup(num4);
							if (newItemByIndexInGroup2 == null)
							{
								this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
								this.mNeedCheckNextMaxItem = false;
								this.CheckIfNeedUpdateItemPos();
								return false;
							}
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num4, newItemByIndexInGroup2.CachedRectTransform.rect.height, newItemByIndexInGroup2.Padding);
							}
							this.mItemList.Add(newItemByIndexInGroup2);
							float y = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D.y - loopStaggeredGridViewItem.CachedRectTransform.rect.height - loopStaggeredGridViewItem.Padding;
							newItemByIndexInGroup2.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup2.StartPosOffset, y, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num4 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num4;
							}
							return true;
						}
					}
				}
			}
			else if (this.mItemList.Count == 0)
			{
				float num5 = this.mContainerTrans.anchoredPosition3D.y;
				if (num5 > 0f)
				{
					num5 = 0f;
				}
				int num6 = 0;
				float y2 = -num5;
				if (this.mSupportScrollBar && !this.GetPlusItemIndexAndPosAtGivenPos(-num5, ref num6, ref y2))
				{
					return false;
				}
				LoopStaggeredGridViewItem newItemByIndexInGroup3 = this.GetNewItemByIndexInGroup(num6);
				if (newItemByIndexInGroup3 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num6, newItemByIndexInGroup3.CachedRectTransform.rect.height, newItemByIndexInGroup3.Padding);
				}
				this.mItemList.Add(newItemByIndexInGroup3);
				newItemByIndexInGroup3.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup3.StartPosOffset, y2, 0f);
				return true;
			}
			else
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
				loopStaggeredGridViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]).y - this.mViewPortRectLocalCorners[1].y < distanceForNew1)
				{
					if (loopStaggeredGridViewItem2.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = true;
					}
					int num7 = loopStaggeredGridViewItem2.ItemIndexInGroup + 1;
					if (num7 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup4 = this.GetNewItemByIndexInGroup(num7);
						if (newItemByIndexInGroup4 == null)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdateItemPos();
							return false;
						}
						if (this.mSupportScrollBar)
						{
							this.SetItemSize(num7, newItemByIndexInGroup4.CachedRectTransform.rect.height, newItemByIndexInGroup4.Padding);
						}
						this.mItemList.Add(newItemByIndexInGroup4);
						float y3 = loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D.y + loopStaggeredGridViewItem2.CachedRectTransform.rect.height + loopStaggeredGridViewItem2.Padding;
						newItemByIndexInGroup4.CachedRectTransform.anchoredPosition3D = new Vector3(newItemByIndexInGroup4.StartPosOffset, y3, 0f);
						this.CheckIfNeedUpdateItemPos();
						if (num7 > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = num7;
						}
						return true;
					}
				}
			}
			return false;
		}

		public bool UpdateForHorizontalListPart1(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mItemTotalCount == 0)
			{
				if (this.mItemList.Count > 0)
				{
					this.RecycleAllItem();
				}
				return false;
			}
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				if (this.mItemList.Count == 0)
				{
					float num = this.mContainerTrans.anchoredPosition3D.x;
					if (num > 0f)
					{
						num = 0f;
					}
					int num2 = 0;
					float x = -num;
					if (this.mSupportScrollBar && !this.GetPlusItemIndexAndPosAtGivenPos(-num, ref num2, ref x))
					{
						return false;
					}
					LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num2);
					if (newItemByIndexInGroup == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndexInGroup.CachedRectTransform.rect.width, newItemByIndexInGroup.Padding);
					}
					this.mItemList.Add(newItemByIndexInGroup);
					newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = new Vector3(x, newItemByIndexInGroup.StartPosOffset, 0f);
					return true;
				}
				else
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[0];
					loopStaggeredGridViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector2 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
					if (!this.IsDraging && loopStaggeredGridViewItem.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[1].x - vector2.x > distanceForRecycle0)
					{
						this.mItemList.RemoveAt(0);
						this.RecycleItemTmp(loopStaggeredGridViewItem);
						if (!this.mSupportScrollBar)
						{
							this.CheckIfNeedUpdateItemPos();
						}
						return true;
					}
					LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
					loopStaggeredGridViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					Vector3 vector3 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
					Vector3 vector4 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
					if (!this.IsDraging && loopStaggeredGridViewItem2.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector3.x - this.mViewPortRectLocalCorners[2].x > distanceForRecycle1)
					{
						this.mItemList.RemoveAt(this.mItemList.Count - 1);
						this.RecycleItemTmp(loopStaggeredGridViewItem2);
						if (!this.mSupportScrollBar)
						{
							this.CheckIfNeedUpdateItemPos();
						}
						return true;
					}
					if (this.mViewPortRectLocalCorners[1].x - vector.x < distanceForNew0)
					{
						if (loopStaggeredGridViewItem.ItemIndexInGroup < this.mCurReadyMinItemIndex)
						{
							this.mCurReadyMinItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMinItem = true;
						}
						int num3 = loopStaggeredGridViewItem.ItemIndexInGroup - 1;
						if (num3 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup2 = this.GetNewItemByIndexInGroup(num3);
							if (!(newItemByIndexInGroup2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num3, newItemByIndexInGroup2.CachedRectTransform.rect.width, newItemByIndexInGroup2.Padding);
								}
								this.mItemList.Insert(0, newItemByIndexInGroup2);
								float x2 = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D.x - newItemByIndexInGroup2.CachedRectTransform.rect.width - newItemByIndexInGroup2.Padding;
								newItemByIndexInGroup2.CachedRectTransform.anchoredPosition3D = new Vector3(x2, newItemByIndexInGroup2.StartPosOffset, 0f);
								this.CheckIfNeedUpdateItemPos();
								if (num3 < this.mCurReadyMinItemIndex)
								{
									this.mCurReadyMinItemIndex = num3;
								}
								return true;
							}
							this.mCurReadyMinItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMinItem = false;
						}
					}
					if (vector4.x - this.mViewPortRectLocalCorners[2].x < distanceForNew1)
					{
						if (loopStaggeredGridViewItem2.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = true;
						}
						int num4 = loopStaggeredGridViewItem2.ItemIndexInGroup + 1;
						if (num4 >= this.mItemIndexMap.Count)
						{
							return false;
						}
						if (num4 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup3 = this.GetNewItemByIndexInGroup(num4);
							if (!(newItemByIndexInGroup3 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num4, newItemByIndexInGroup3.CachedRectTransform.rect.width, newItemByIndexInGroup3.Padding);
								}
								this.mItemList.Add(newItemByIndexInGroup3);
								float x3 = loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D.x + loopStaggeredGridViewItem2.CachedRectTransform.rect.width + loopStaggeredGridViewItem2.Padding;
								newItemByIndexInGroup3.CachedRectTransform.anchoredPosition3D = new Vector3(x3, newItemByIndexInGroup3.StartPosOffset, 0f);
								this.CheckIfNeedUpdateItemPos();
								if (num4 > this.mCurReadyMaxItemIndex)
								{
									this.mCurReadyMaxItemIndex = num4;
								}
								return true;
							}
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdateItemPos();
						}
					}
				}
			}
			else if (this.mItemList.Count == 0)
			{
				float num5 = this.mContainerTrans.anchoredPosition3D.x;
				if (num5 < 0f)
				{
					num5 = 0f;
				}
				int num6 = 0;
				float num7 = -num5;
				if (this.mSupportScrollBar)
				{
					if (!this.GetPlusItemIndexAndPosAtGivenPos(num5, ref num6, ref num7))
					{
						return false;
					}
					num7 = -num7;
				}
				LoopStaggeredGridViewItem newItemByIndexInGroup4 = this.GetNewItemByIndexInGroup(num6);
				if (newItemByIndexInGroup4 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num6, newItemByIndexInGroup4.CachedRectTransform.rect.width, newItemByIndexInGroup4.Padding);
				}
				this.mItemList.Add(newItemByIndexInGroup4);
				newItemByIndexInGroup4.CachedRectTransform.anchoredPosition3D = new Vector3(num7, newItemByIndexInGroup4.StartPosOffset, 0f);
				return true;
			}
			else
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem3 = this.mItemList[0];
				loopStaggeredGridViewItem3.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector5 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector6 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
				if (!this.IsDraging && loopStaggeredGridViewItem3.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && vector5.x - this.mViewPortRectLocalCorners[2].x > distanceForRecycle0)
				{
					this.mItemList.RemoveAt(0);
					this.RecycleItemTmp(loopStaggeredGridViewItem3);
					if (!this.mSupportScrollBar)
					{
						this.CheckIfNeedUpdateItemPos();
					}
					return true;
				}
				LoopStaggeredGridViewItem loopStaggeredGridViewItem4 = this.mItemList[this.mItemList.Count - 1];
				loopStaggeredGridViewItem4.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector7 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				Vector3 vector8 = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]);
				if (!this.IsDraging && loopStaggeredGridViewItem4.ItemCreatedCheckFrameCount != this.mListUpdateCheckFrameCount && this.mViewPortRectLocalCorners[1].x - vector8.x > distanceForRecycle1)
				{
					this.mItemList.RemoveAt(this.mItemList.Count - 1);
					this.RecycleItemTmp(loopStaggeredGridViewItem4);
					if (!this.mSupportScrollBar)
					{
						this.CheckIfNeedUpdateItemPos();
					}
					return true;
				}
				if (vector6.x - this.mViewPortRectLocalCorners[2].x < distanceForNew0)
				{
					if (loopStaggeredGridViewItem3.ItemIndexInGroup < this.mCurReadyMinItemIndex)
					{
						this.mCurReadyMinItemIndex = loopStaggeredGridViewItem3.ItemIndexInGroup;
						this.mNeedCheckNextMinItem = true;
					}
					int num8 = loopStaggeredGridViewItem3.ItemIndexInGroup - 1;
					if (num8 >= this.mCurReadyMinItemIndex || this.mNeedCheckNextMinItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup5 = this.GetNewItemByIndexInGroup(num8);
						if (!(newItemByIndexInGroup5 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num8, newItemByIndexInGroup5.CachedRectTransform.rect.width, newItemByIndexInGroup5.Padding);
							}
							this.mItemList.Insert(0, newItemByIndexInGroup5);
							float x4 = loopStaggeredGridViewItem3.CachedRectTransform.anchoredPosition3D.x + newItemByIndexInGroup5.CachedRectTransform.rect.width + newItemByIndexInGroup5.Padding;
							newItemByIndexInGroup5.CachedRectTransform.anchoredPosition3D = new Vector3(x4, newItemByIndexInGroup5.StartPosOffset, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num8 < this.mCurReadyMinItemIndex)
							{
								this.mCurReadyMinItemIndex = num8;
							}
							return true;
						}
						this.mCurReadyMinItemIndex = loopStaggeredGridViewItem3.ItemIndexInGroup;
						this.mNeedCheckNextMinItem = false;
					}
				}
				if (this.mViewPortRectLocalCorners[1].x - vector7.x < distanceForNew1)
				{
					if (loopStaggeredGridViewItem4.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem4.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = true;
					}
					int num9 = loopStaggeredGridViewItem4.ItemIndexInGroup + 1;
					if (num9 >= this.mItemIndexMap.Count)
					{
						return false;
					}
					if (num9 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup6 = this.GetNewItemByIndexInGroup(num9);
						if (!(newItemByIndexInGroup6 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num9, newItemByIndexInGroup6.CachedRectTransform.rect.width, newItemByIndexInGroup6.Padding);
							}
							this.mItemList.Add(newItemByIndexInGroup6);
							float x5 = loopStaggeredGridViewItem4.CachedRectTransform.anchoredPosition3D.x - loopStaggeredGridViewItem4.CachedRectTransform.rect.width - loopStaggeredGridViewItem4.Padding;
							newItemByIndexInGroup6.CachedRectTransform.anchoredPosition3D = new Vector3(x5, newItemByIndexInGroup6.StartPosOffset, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num9 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num9;
							}
							return true;
						}
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem4.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = false;
						this.CheckIfNeedUpdateItemPos();
					}
				}
			}
			return false;
		}

		public bool UpdateForHorizontalListPart2(float distanceForRecycle0, float distanceForRecycle1, float distanceForNew0, float distanceForNew1)
		{
			if (this.mItemTotalCount == 0)
			{
				if (this.mItemList.Count > 0)
				{
					this.RecycleAllItem();
				}
				return false;
			}
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				if (this.mItemList.Count == 0)
				{
					float num = this.mContainerTrans.anchoredPosition3D.x;
					if (num > 0f)
					{
						num = 0f;
					}
					int num2 = 0;
					float x = -num;
					if (this.mSupportScrollBar && !this.GetPlusItemIndexAndPosAtGivenPos(-num, ref num2, ref x))
					{
						return false;
					}
					LoopStaggeredGridViewItem newItemByIndexInGroup = this.GetNewItemByIndexInGroup(num2);
					if (newItemByIndexInGroup == null)
					{
						return false;
					}
					if (this.mSupportScrollBar)
					{
						this.SetItemSize(num2, newItemByIndexInGroup.CachedRectTransform.rect.width, newItemByIndexInGroup.Padding);
					}
					this.mItemList.Add(newItemByIndexInGroup);
					newItemByIndexInGroup.CachedRectTransform.anchoredPosition3D = new Vector3(x, newItemByIndexInGroup.StartPosOffset, 0f);
					return true;
				}
				else
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[this.mItemList.Count - 1];
					loopStaggeredGridViewItem.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
					if (this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[2]).x - this.mViewPortRectLocalCorners[2].x < distanceForNew1)
					{
						if (loopStaggeredGridViewItem.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
						{
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = true;
						}
						int num3 = loopStaggeredGridViewItem.ItemIndexInGroup + 1;
						if (num3 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
						{
							LoopStaggeredGridViewItem newItemByIndexInGroup2 = this.GetNewItemByIndexInGroup(num3);
							if (!(newItemByIndexInGroup2 == null))
							{
								if (this.mSupportScrollBar)
								{
									this.SetItemSize(num3, newItemByIndexInGroup2.CachedRectTransform.rect.width, newItemByIndexInGroup2.Padding);
								}
								this.mItemList.Add(newItemByIndexInGroup2);
								float x2 = loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D.x + loopStaggeredGridViewItem.CachedRectTransform.rect.width + loopStaggeredGridViewItem.Padding;
								newItemByIndexInGroup2.CachedRectTransform.anchoredPosition3D = new Vector3(x2, newItemByIndexInGroup2.StartPosOffset, 0f);
								this.CheckIfNeedUpdateItemPos();
								if (num3 > this.mCurReadyMaxItemIndex)
								{
									this.mCurReadyMaxItemIndex = num3;
								}
								return true;
							}
							this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem.ItemIndexInGroup;
							this.mNeedCheckNextMaxItem = false;
							this.CheckIfNeedUpdateItemPos();
						}
					}
				}
			}
			else if (this.mItemList.Count == 0)
			{
				float num4 = this.mContainerTrans.anchoredPosition3D.x;
				if (num4 < 0f)
				{
					num4 = 0f;
				}
				int num5 = 0;
				float num6 = -num4;
				if (this.mSupportScrollBar)
				{
					if (!this.GetPlusItemIndexAndPosAtGivenPos(num4, ref num5, ref num6))
					{
						return false;
					}
					num6 = -num6;
				}
				LoopStaggeredGridViewItem newItemByIndexInGroup3 = this.GetNewItemByIndexInGroup(num5);
				if (newItemByIndexInGroup3 == null)
				{
					return false;
				}
				if (this.mSupportScrollBar)
				{
					this.SetItemSize(num5, newItemByIndexInGroup3.CachedRectTransform.rect.width, newItemByIndexInGroup3.Padding);
				}
				this.mItemList.Add(newItemByIndexInGroup3);
				newItemByIndexInGroup3.CachedRectTransform.anchoredPosition3D = new Vector3(num6, newItemByIndexInGroup3.StartPosOffset, 0f);
				return true;
			}
			else
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
				loopStaggeredGridViewItem2.CachedRectTransform.GetWorldCorners(this.mItemWorldCorners);
				Vector3 vector = this.mViewPortRectTransform.InverseTransformPoint(this.mItemWorldCorners[1]);
				if (this.mViewPortRectLocalCorners[1].x - vector.x < distanceForNew1)
				{
					if (loopStaggeredGridViewItem2.ItemIndexInGroup > this.mCurReadyMaxItemIndex)
					{
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = true;
					}
					int num7 = loopStaggeredGridViewItem2.ItemIndexInGroup + 1;
					if (num7 <= this.mCurReadyMaxItemIndex || this.mNeedCheckNextMaxItem)
					{
						LoopStaggeredGridViewItem newItemByIndexInGroup4 = this.GetNewItemByIndexInGroup(num7);
						if (!(newItemByIndexInGroup4 == null))
						{
							if (this.mSupportScrollBar)
							{
								this.SetItemSize(num7, newItemByIndexInGroup4.CachedRectTransform.rect.width, newItemByIndexInGroup4.Padding);
							}
							this.mItemList.Add(newItemByIndexInGroup4);
							float x3 = loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D.x - loopStaggeredGridViewItem2.CachedRectTransform.rect.width - loopStaggeredGridViewItem2.Padding;
							newItemByIndexInGroup4.CachedRectTransform.anchoredPosition3D = new Vector3(x3, newItemByIndexInGroup4.StartPosOffset, 0f);
							this.CheckIfNeedUpdateItemPos();
							if (num7 > this.mCurReadyMaxItemIndex)
							{
								this.mCurReadyMaxItemIndex = num7;
							}
							return true;
						}
						this.mCurReadyMaxItemIndex = loopStaggeredGridViewItem2.ItemIndexInGroup;
						this.mNeedCheckNextMaxItem = false;
						this.CheckIfNeedUpdateItemPos();
					}
				}
			}
			return false;
		}

		public float GetContentPanelSize()
		{
			float num = (this.mItemPosMgr.mTotalSize <= 0f) ? 0f : (this.mItemPosMgr.mTotalSize - this.mLastItemPadding);
			if (num < 0f)
			{
				num = 0f;
			}
			return num;
		}

		public float GetShownItemPosMaxValue()
		{
			if (this.mItemList.Count == 0)
			{
				return 0f;
			}
			LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[this.mItemList.Count - 1];
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				return Mathf.Abs(loopStaggeredGridViewItem.BottomY);
			}
			if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				return Mathf.Abs(loopStaggeredGridViewItem.TopY);
			}
			if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				return Mathf.Abs(loopStaggeredGridViewItem.RightX);
			}
			if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				return Mathf.Abs(loopStaggeredGridViewItem.LeftX);
			}
			return 0f;
		}

		public void CheckIfNeedUpdateItemPos()
		{
			if (this.mItemList.Count == 0)
			{
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[0];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[this.mItemList.Count - 1];
				if (loopStaggeredGridViewItem.TopY > 0f || (loopStaggeredGridViewItem.ItemIndexInGroup == this.mCurReadyMinItemIndex && loopStaggeredGridViewItem.TopY != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				float contentPanelSize = this.GetContentPanelSize();
				if (-loopStaggeredGridViewItem2.BottomY > contentPanelSize || (loopStaggeredGridViewItem2.ItemIndexInGroup == this.mCurReadyMaxItemIndex && -loopStaggeredGridViewItem2.BottomY != contentPanelSize))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem3 = this.mItemList[0];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem4 = this.mItemList[this.mItemList.Count - 1];
				if (loopStaggeredGridViewItem3.BottomY < 0f || (loopStaggeredGridViewItem3.ItemIndexInGroup == this.mCurReadyMinItemIndex && loopStaggeredGridViewItem3.BottomY != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				float contentPanelSize2 = this.GetContentPanelSize();
				if (loopStaggeredGridViewItem4.TopY > contentPanelSize2 || (loopStaggeredGridViewItem4.ItemIndexInGroup == this.mCurReadyMaxItemIndex && loopStaggeredGridViewItem4.TopY != contentPanelSize2))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem5 = this.mItemList[0];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem6 = this.mItemList[this.mItemList.Count - 1];
				if (loopStaggeredGridViewItem5.LeftX < 0f || (loopStaggeredGridViewItem5.ItemIndexInGroup == this.mCurReadyMinItemIndex && loopStaggeredGridViewItem5.LeftX != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				float contentPanelSize3 = this.GetContentPanelSize();
				if (loopStaggeredGridViewItem6.RightX > contentPanelSize3 || (loopStaggeredGridViewItem6.ItemIndexInGroup == this.mCurReadyMaxItemIndex && loopStaggeredGridViewItem6.RightX != contentPanelSize3))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				LoopStaggeredGridViewItem loopStaggeredGridViewItem7 = this.mItemList[0];
				LoopStaggeredGridViewItem loopStaggeredGridViewItem8 = this.mItemList[this.mItemList.Count - 1];
				if (loopStaggeredGridViewItem7.RightX > 0f || (loopStaggeredGridViewItem7.ItemIndexInGroup == this.mCurReadyMinItemIndex && loopStaggeredGridViewItem7.RightX != 0f))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
				float contentPanelSize4 = this.GetContentPanelSize();
				if (-loopStaggeredGridViewItem8.LeftX > contentPanelSize4 || (loopStaggeredGridViewItem8.ItemIndexInGroup == this.mCurReadyMaxItemIndex && -loopStaggeredGridViewItem8.LeftX != contentPanelSize4))
				{
					this.UpdateAllShownItemsPos();
					return;
				}
			}
		}

		public void UpdateAllShownItemsPos()
		{
			int count = this.mItemList.Count;
			if (count == 0)
			{
				return;
			}
			if (this.mArrangeType == ListItemArrangeType.TopToBottom)
			{
				float num = 0f;
				if (this.mSupportScrollBar)
				{
					num = -this.GetItemPos(this.mItemList[0].ItemIndexInGroup);
				}
				float y = this.mItemList[0].CachedRectTransform.anchoredPosition3D.y;
				float num2 = num;
				for (int i = 0; i < count; i++)
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem = this.mItemList[i];
					loopStaggeredGridViewItem.CachedRectTransform.anchoredPosition3D = new Vector3(loopStaggeredGridViewItem.StartPosOffset, num2, 0f);
					num2 = num2 - loopStaggeredGridViewItem.CachedRectTransform.rect.height - loopStaggeredGridViewItem.Padding;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.BottomToTop)
			{
				float num3 = 0f;
				if (this.mSupportScrollBar)
				{
					num3 = this.GetItemPos(this.mItemList[0].ItemIndexInGroup);
				}
				float y2 = this.mItemList[0].CachedRectTransform.anchoredPosition3D.y;
				float num4 = num3;
				for (int j = 0; j < count; j++)
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem2 = this.mItemList[j];
					loopStaggeredGridViewItem2.CachedRectTransform.anchoredPosition3D = new Vector3(loopStaggeredGridViewItem2.StartPosOffset, num4, 0f);
					num4 = num4 + loopStaggeredGridViewItem2.CachedRectTransform.rect.height + loopStaggeredGridViewItem2.Padding;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.LeftToRight)
			{
				float num5 = 0f;
				if (this.mSupportScrollBar)
				{
					num5 = this.GetItemPos(this.mItemList[0].ItemIndexInGroup);
				}
				float x = this.mItemList[0].CachedRectTransform.anchoredPosition3D.x;
				float num6 = num5;
				for (int k = 0; k < count; k++)
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem3 = this.mItemList[k];
					loopStaggeredGridViewItem3.CachedRectTransform.anchoredPosition3D = new Vector3(num6, loopStaggeredGridViewItem3.StartPosOffset, 0f);
					num6 = num6 + loopStaggeredGridViewItem3.CachedRectTransform.rect.width + loopStaggeredGridViewItem3.Padding;
				}
			}
			else if (this.mArrangeType == ListItemArrangeType.RightToLeft)
			{
				float num7 = 0f;
				if (this.mSupportScrollBar)
				{
					num7 = -this.GetItemPos(this.mItemList[0].ItemIndexInGroup);
				}
				float x2 = this.mItemList[0].CachedRectTransform.anchoredPosition3D.x;
				float num8 = num7;
				for (int l = 0; l < count; l++)
				{
					LoopStaggeredGridViewItem loopStaggeredGridViewItem4 = this.mItemList[l];
					loopStaggeredGridViewItem4.CachedRectTransform.anchoredPosition3D = new Vector3(num8, loopStaggeredGridViewItem4.StartPosOffset, 0f);
					num8 = num8 - loopStaggeredGridViewItem4.CachedRectTransform.rect.width - loopStaggeredGridViewItem4.Padding;
				}
			}
		}

		private LoopStaggeredGridView mParentGridView;

		private ListItemArrangeType mArrangeType;

		private List<LoopStaggeredGridViewItem> mItemList = new List<LoopStaggeredGridViewItem>();

		private RectTransform mContainerTrans;

		private ScrollRect mScrollRect;

		public int mGroupIndex;

		private GameObject mGameObject;

		private List<int> mItemIndexMap = new List<int>();

		private RectTransform mScrollRectTransform;

		private RectTransform mViewPortRectTransform;

		private float mItemDefaultWithPaddingSize;

		private int mItemTotalCount;

		private bool mIsVertList;

		private Func<int, int, LoopStaggeredGridViewItem> mOnGetItemByIndex;

		private Vector3[] mItemWorldCorners = new Vector3[4];

		private Vector3[] mViewPortRectLocalCorners = new Vector3[4];

		private int mCurReadyMinItemIndex;

		private int mCurReadyMaxItemIndex;

		private bool mNeedCheckNextMinItem = true;

		private bool mNeedCheckNextMaxItem = true;

		private ItemPosMgr mItemPosMgr;

		private bool mSupportScrollBar = true;

		private int mLastItemIndex;

		private float mLastItemPadding;

		private Vector3 mLastFrameContainerPos = Vector3.zero;

		private int mListUpdateCheckFrameCount;
	}
}
