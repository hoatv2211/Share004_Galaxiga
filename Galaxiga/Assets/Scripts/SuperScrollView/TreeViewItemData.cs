using System;
using System.Collections.Generic;

namespace SuperScrollView
{
	public class TreeViewItemData
	{
		public int ChildCount
		{
			get
			{
				return this.mChildItemDataList.Count;
			}
		}

		public void AddChild(ItemData data)
		{
			this.mChildItemDataList.Add(data);
		}

		public ItemData GetChild(int index)
		{
			if (index < 0 || index >= this.mChildItemDataList.Count)
			{
				return null;
			}
			return this.mChildItemDataList[index];
		}

		public string mName;

		public string mIcon;

		private List<ItemData> mChildItemDataList = new List<ItemData>();
	}
}
