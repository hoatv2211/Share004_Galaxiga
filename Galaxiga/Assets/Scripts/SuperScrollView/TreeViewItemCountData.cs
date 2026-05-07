using System;

namespace SuperScrollView
{
	public class TreeViewItemCountData
	{
		public bool IsChild(int index)
		{
			return index != this.mBeginIndex;
		}

		public int GetChildIndex(int index)
		{
			if (!this.IsChild(index))
			{
				return -1;
			}
			return index - this.mBeginIndex - 1;
		}

		public int mTreeItemIndex;

		public int mChildCount;

		public bool mIsExpand = true;

		public int mBeginIndex;

		public int mEndIndex;
	}
}
