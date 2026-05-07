using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	public class TreeViewDataSourceMgr : MonoBehaviour
	{
		public static TreeViewDataSourceMgr Get
		{
			get
			{
				if (TreeViewDataSourceMgr.instance == null)
				{
					TreeViewDataSourceMgr.instance = UnityEngine.Object.FindObjectOfType<TreeViewDataSourceMgr>();
				}
				return TreeViewDataSourceMgr.instance;
			}
		}

		private void Awake()
		{
			this.Init();
		}

		public void Init()
		{
			this.DoRefreshDataSource();
		}

		public TreeViewItemData GetItemDataByIndex(int index)
		{
			if (index < 0 || index >= this.mItemDataList.Count)
			{
				return null;
			}
			return this.mItemDataList[index];
		}

		public ItemData GetItemChildDataByIndex(int itemIndex, int childIndex)
		{
			TreeViewItemData itemDataByIndex = this.GetItemDataByIndex(itemIndex);
			if (itemDataByIndex == null)
			{
				return null;
			}
			return itemDataByIndex.GetChild(childIndex);
		}

		public int TreeViewItemCount
		{
			get
			{
				return this.mItemDataList.Count;
			}
		}

		public int TotalTreeViewItemAndChildCount
		{
			get
			{
				int count = this.mItemDataList.Count;
				int num = 0;
				for (int i = 0; i < count; i++)
				{
					num += this.mItemDataList[i].ChildCount;
				}
				return num;
			}
		}

		private void DoRefreshDataSource()
		{
			this.mItemDataList.Clear();
			for (int i = 0; i < this.mTreeViewItemCount; i++)
			{
				TreeViewItemData treeViewItemData = new TreeViewItemData();
				treeViewItemData.mName = "Item" + i;
				treeViewItemData.mIcon = ResManager.Get.GetSpriteNameByIndex(UnityEngine.Random.Range(0, 24));
				this.mItemDataList.Add(treeViewItemData);
				int num = this.mTreeViewChildItemCount;
				for (int j = 1; j <= num; j++)
				{
					ItemData itemData = new ItemData();
					itemData.mName = string.Concat(new object[]
					{
						"Item",
						i,
						":Child",
						j
					});
					itemData.mDesc = "Item Desc For " + itemData.mName;
					itemData.mIcon = ResManager.Get.GetSpriteNameByIndex(UnityEngine.Random.Range(0, 24));
					itemData.mStarCount = UnityEngine.Random.Range(0, 6);
					itemData.mFileSize = UnityEngine.Random.Range(20, 999);
					treeViewItemData.AddChild(itemData);
				}
			}
		}

		private List<TreeViewItemData> mItemDataList = new List<TreeViewItemData>();

		private static TreeViewDataSourceMgr instance;

		private int mTreeViewItemCount = 20;

		private int mTreeViewChildItemCount = 30;
	}
}
