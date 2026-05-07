using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class DeleteItemDemoScript : MonoBehaviour
	{
		private void Start()
		{
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mSelectAllButton.onClick.AddListener(new UnityAction(this.OnSelectAllBtnClicked));
			this.mCancelAllButton.onClick.AddListener(new UnityAction(this.OnCancelAllBtnClicked));
			this.mDeleteButton.onClick.AddListener(new UnityAction(this.OnDeleteBtnClicked));
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
		}

		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
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
			ListItem3 component = loopListViewItem.GetComponent<ListItem3>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.SetItemData(itemDataByIndex, index);
			return loopListViewItem;
		}

		private void OnSelectAllBtnClicked()
		{
			DataSourceMgr.Get.CheckAllItem();
			this.mLoopListView.RefreshAllShownItem();
		}

		private void OnCancelAllBtnClicked()
		{
			DataSourceMgr.Get.UnCheckAllItem();
			this.mLoopListView.RefreshAllShownItem();
		}

		private void OnDeleteBtnClicked()
		{
			if (!DataSourceMgr.Get.DeleteAllCheckedItem())
			{
				return;
			}
			this.mLoopListView.SetListItemCount(DataSourceMgr.Get.TotalItemCount, false);
			this.mLoopListView.RefreshAllShownItem();
		}

		public LoopListView2 mLoopListView;

		public Button mSelectAllButton;

		public Button mCancelAllButton;

		public Button mDeleteButton;

		public Button mBackButton;
	}
}
