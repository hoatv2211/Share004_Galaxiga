using System;
using UnityEngine;

namespace SuperScrollView
{
	public class GridViewSampleDemo : MonoBehaviour
	{
		private void Start()
		{
			int num = this.mItemTotalCount / 3;
			if (this.mItemTotalCount % 3 > 0)
			{
				num++;
			}
			this.mLoopListView.InitListView(num, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
		}

		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int rowIndex)
		{
			if (rowIndex < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("RowPrefab");
			ListItem15 component = loopListViewItem.GetComponent<ListItem15>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			for (int i = 0; i < 3; i++)
			{
				int num = rowIndex * 3 + i;
				if (num >= this.mItemTotalCount)
				{
					component.mItemList[i].gameObject.SetActive(false);
				}
				else
				{
					component.mItemList[i].gameObject.SetActive(true);
					component.mItemList[i].mNameText.text = "Item" + num;
				}
			}
			return loopListViewItem;
		}

		public LoopListView2 mLoopListView;

		private const int mItemCountPerRow = 3;

		private int mItemTotalCount = 100;
	}
}
