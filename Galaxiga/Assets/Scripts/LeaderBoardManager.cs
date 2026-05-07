using System;
using SuperScrollView;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
	{
		if (index < 0 || index >= DataSourceMgr.Get.TotalItemCount)
		{
			return null;
		}
		ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(index);
		if (itemDataByIndex == null)
		{
			return null;
		}
		LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
		ListItem2 component = loopListViewItem.GetComponent<ListItem2>();
		if (!loopListViewItem.IsInitHandlerCalled)
		{
			loopListViewItem.IsInitHandlerCalled = true;
			component.Init();
		}
		component.SetItemData(itemDataByIndex, index);
		return loopListViewItem;
	}

	public LoopListView2 mLoopListView;
}
