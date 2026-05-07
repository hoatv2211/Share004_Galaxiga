using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class TreeViewDemoScript : MonoBehaviour
	{
		private void Start()
		{
			int treeViewItemCount = TreeViewDataSourceMgr.Get.TreeViewItemCount;
			for (int i = 0; i < treeViewItemCount; i++)
			{
				int childCount = TreeViewDataSourceMgr.Get.GetItemDataByIndex(i).ChildCount;
				this.mTreeItemCountMgr.AddTreeItem(childCount, true);
			}
			this.mLoopListView.InitListView(this.mTreeItemCountMgr.GetTotalItemAndChildCount(), new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mExpandAllButton = GameObject.Find("ButtonPanel/buttonGroup1/ExpandAllButton").GetComponent<Button>();
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mCollapseAllButton = GameObject.Find("ButtonPanel/buttonGroup3/CollapseAllButton").GetComponent<Button>();
			this.mScrollToInputItem = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputFieldItem").GetComponent<InputField>();
			this.mScrollToInputChild = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputFieldChild").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
			this.mExpandAllButton.onClick.AddListener(new UnityAction(this.OnExpandAllBtnClicked));
			this.mCollapseAllButton.onClick.AddListener(new UnityAction(this.OnCollapseAllBtnClicked));
		}

		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0)
			{
				return null;
			}
			TreeViewItemCountData treeViewItemCountData = this.mTreeItemCountMgr.QueryTreeItemByTotalIndex(index);
			if (treeViewItemCountData == null)
			{
				return null;
			}
			int mTreeItemIndex = treeViewItemCountData.mTreeItemIndex;
			TreeViewItemData itemDataByIndex = TreeViewDataSourceMgr.Get.GetItemDataByIndex(mTreeItemIndex);
			if (!treeViewItemCountData.IsChild(index))
			{
				LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
				ListItem12 component = loopListViewItem.GetComponent<ListItem12>();
				if (!loopListViewItem.IsInitHandlerCalled)
				{
					loopListViewItem.IsInitHandlerCalled = true;
					component.Init();
					component.SetClickCallBack(new Action<int>(this.OnExpandClicked));
				}
				component.mText.text = itemDataByIndex.mName;
				component.SetItemData(mTreeItemIndex, treeViewItemCountData.mIsExpand);
				return loopListViewItem;
			}
			int childIndex = treeViewItemCountData.GetChildIndex(index);
			ItemData child = itemDataByIndex.GetChild(childIndex);
			if (child == null)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem2 = listView.NewListViewItem("ItemPrefab2");
			ListItem13 component2 = loopListViewItem2.GetComponent<ListItem13>();
			if (!loopListViewItem2.IsInitHandlerCalled)
			{
				loopListViewItem2.IsInitHandlerCalled = true;
				component2.Init();
			}
			component2.SetItemData(child, mTreeItemIndex, childIndex);
			return loopListViewItem2;
		}

		public void OnExpandClicked(int index)
		{
			this.mTreeItemCountMgr.ToggleItemExpand(index);
			this.mLoopListView.SetListItemCount(this.mTreeItemCountMgr.GetTotalItemAndChildCount(), false);
			this.mLoopListView.RefreshAllShownItem();
		}

		private void OnJumpBtnClicked()
		{
			int treeIndex = 0;
			int num = 0;
			if (!int.TryParse(this.mScrollToInputItem.text, out treeIndex))
			{
				return;
			}
			if (!int.TryParse(this.mScrollToInputChild.text, out num))
			{
				num = 0;
			}
			if (num < 0)
			{
				num = 0;
			}
			TreeViewItemCountData treeItem = this.mTreeItemCountMgr.GetTreeItem(treeIndex);
			if (treeItem == null)
			{
				return;
			}
			int mChildCount = treeItem.mChildCount;
			int itemIndex;
			if (!treeItem.mIsExpand || mChildCount == 0 || num == 0)
			{
				itemIndex = treeItem.mBeginIndex;
			}
			else
			{
				if (num > mChildCount)
				{
					num = mChildCount;
				}
				if (num < 1)
				{
					num = 1;
				}
				itemIndex = treeItem.mBeginIndex + num;
			}
			this.mLoopListView.MovePanelToItemIndex(itemIndex, 0f);
		}

		private void OnExpandAllBtnClicked()
		{
			int treeViewItemCount = this.mTreeItemCountMgr.TreeViewItemCount;
			for (int i = 0; i < treeViewItemCount; i++)
			{
				this.mTreeItemCountMgr.SetItemExpand(i, true);
			}
			this.mLoopListView.SetListItemCount(this.mTreeItemCountMgr.GetTotalItemAndChildCount(), false);
			this.mLoopListView.RefreshAllShownItem();
		}

		private void OnCollapseAllBtnClicked()
		{
			int treeViewItemCount = this.mTreeItemCountMgr.TreeViewItemCount;
			for (int i = 0; i < treeViewItemCount; i++)
			{
				this.mTreeItemCountMgr.SetItemExpand(i, false);
			}
			this.mLoopListView.SetListItemCount(this.mTreeItemCountMgr.GetTotalItemAndChildCount(), false);
			this.mLoopListView.RefreshAllShownItem();
		}

		public LoopListView2 mLoopListView;

		private Button mScrollToButton;

		private Button mExpandAllButton;

		private Button mCollapseAllButton;

		private InputField mScrollToInputItem;

		private InputField mScrollToInputChild;

		private Button mBackButton;

		private TreeViewItemCountMgr mTreeItemCountMgr = new TreeViewItemCountMgr();
	}
}
