using System;
using System.Collections.Generic;

namespace SuperScrollView
{
	public class TreeViewItemCountMgr
	{
		public void AddTreeItem(int count, bool isExpand)
		{
			TreeViewItemCountData treeViewItemCountData = new TreeViewItemCountData();
			treeViewItemCountData.mTreeItemIndex = this.mTreeItemDataList.Count;
			treeViewItemCountData.mChildCount = count;
			treeViewItemCountData.mIsExpand = isExpand;
			this.mTreeItemDataList.Add(treeViewItemCountData);
			this.mIsDirty = true;
		}

		public void Clear()
		{
			this.mTreeItemDataList.Clear();
			this.mLastQueryResult = null;
			this.mIsDirty = true;
		}

		public TreeViewItemCountData GetTreeItem(int treeIndex)
		{
			if (treeIndex < 0 || treeIndex >= this.mTreeItemDataList.Count)
			{
				return null;
			}
			return this.mTreeItemDataList[treeIndex];
		}

		public void SetItemChildCount(int treeIndex, int count)
		{
			if (treeIndex < 0 || treeIndex >= this.mTreeItemDataList.Count)
			{
				return;
			}
			this.mIsDirty = true;
			TreeViewItemCountData treeViewItemCountData = this.mTreeItemDataList[treeIndex];
			treeViewItemCountData.mChildCount = count;
		}

		public void SetItemExpand(int treeIndex, bool isExpand)
		{
			if (treeIndex < 0 || treeIndex >= this.mTreeItemDataList.Count)
			{
				return;
			}
			this.mIsDirty = true;
			TreeViewItemCountData treeViewItemCountData = this.mTreeItemDataList[treeIndex];
			treeViewItemCountData.mIsExpand = isExpand;
		}

		public void ToggleItemExpand(int treeIndex)
		{
			if (treeIndex < 0 || treeIndex >= this.mTreeItemDataList.Count)
			{
				return;
			}
			this.mIsDirty = true;
			TreeViewItemCountData treeViewItemCountData = this.mTreeItemDataList[treeIndex];
			treeViewItemCountData.mIsExpand = !treeViewItemCountData.mIsExpand;
		}

		public bool IsTreeItemExpand(int treeIndex)
		{
			TreeViewItemCountData treeItem = this.GetTreeItem(treeIndex);
			return treeItem != null && treeItem.mIsExpand;
		}

		private void UpdateAllTreeItemDataIndex()
		{
			if (!this.mIsDirty)
			{
				return;
			}
			this.mLastQueryResult = null;
			this.mIsDirty = false;
			int count = this.mTreeItemDataList.Count;
			if (count == 0)
			{
				return;
			}
			TreeViewItemCountData treeViewItemCountData = this.mTreeItemDataList[0];
			treeViewItemCountData.mBeginIndex = 0;
			treeViewItemCountData.mEndIndex = ((!treeViewItemCountData.mIsExpand) ? 0 : treeViewItemCountData.mChildCount);
			int mEndIndex = treeViewItemCountData.mEndIndex;
			for (int i = 1; i < count; i++)
			{
				TreeViewItemCountData treeViewItemCountData2 = this.mTreeItemDataList[i];
				treeViewItemCountData2.mBeginIndex = mEndIndex + 1;
				treeViewItemCountData2.mEndIndex = treeViewItemCountData2.mBeginIndex + ((!treeViewItemCountData2.mIsExpand) ? 0 : treeViewItemCountData2.mChildCount);
				mEndIndex = treeViewItemCountData2.mEndIndex;
			}
		}

		public int TreeViewItemCount
		{
			get
			{
				return this.mTreeItemDataList.Count;
			}
		}

		public int GetTotalItemAndChildCount()
		{
			int count = this.mTreeItemDataList.Count;
			if (count == 0)
			{
				return 0;
			}
			this.UpdateAllTreeItemDataIndex();
			return this.mTreeItemDataList[count - 1].mEndIndex + 1;
		}

		public TreeViewItemCountData QueryTreeItemByTotalIndex(int totalIndex)
		{
			if (totalIndex < 0)
			{
				return null;
			}
			int count = this.mTreeItemDataList.Count;
			if (count == 0)
			{
				return null;
			}
			this.UpdateAllTreeItemDataIndex();
			if (this.mLastQueryResult != null && this.mLastQueryResult.mBeginIndex <= totalIndex && this.mLastQueryResult.mEndIndex >= totalIndex)
			{
				return this.mLastQueryResult;
			}
			int i = 0;
			int num = count - 1;
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				TreeViewItemCountData treeViewItemCountData = this.mTreeItemDataList[num2];
				if (treeViewItemCountData.mBeginIndex <= totalIndex && treeViewItemCountData.mEndIndex >= totalIndex)
				{
					this.mLastQueryResult = treeViewItemCountData;
					return treeViewItemCountData;
				}
				if (totalIndex > treeViewItemCountData.mEndIndex)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return null;
		}

		private List<TreeViewItemCountData> mTreeItemDataList = new List<TreeViewItemCountData>();

		private TreeViewItemCountData mLastQueryResult;

		private bool mIsDirty = true;
	}
}
