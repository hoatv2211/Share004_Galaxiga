using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class GridViewDeleteItemDemoScript : MonoBehaviour
	{
		private void Start()
		{
			this.mListItemTotalCount = DataSourceMgr.Get.TotalItemCount;
			int num = this.mListItemTotalCount / 3;
			if (this.mListItemTotalCount % 3 > 0)
			{
				num++;
			}
			this.mLoopListView.InitListView(num, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
			this.mSelectAllButton.onClick.AddListener(new UnityAction(this.OnSelectAllBtnClicked));
			this.mCancelAllButton.onClick.AddListener(new UnityAction(this.OnCancelAllBtnClicked));
			this.mDeleteButton.onClick.AddListener(new UnityAction(this.OnDeleteBtnClicked));
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
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem10 component = loopListViewItem.GetComponent<ListItem10>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			for (int i = 0; i < 3; i++)
			{
				int num = index * 3 + i;
				if (num >= this.mListItemTotalCount)
				{
					component.mItemList[i].gameObject.SetActive(false);
				}
				else
				{
					ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(num);
					if (itemDataByIndex != null)
					{
						component.mItemList[i].gameObject.SetActive(true);
						component.mItemList[i].SetItemData(itemDataByIndex, num);
					}
					else
					{
						component.mItemList[i].gameObject.SetActive(false);
					}
				}
			}
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
			this.SetListItemTotalCount(DataSourceMgr.Get.TotalItemCount);
		}

		private void SetListItemTotalCount(int count)
		{
			this.mListItemTotalCount = count;
			if (this.mListItemTotalCount < 0)
			{
				this.mListItemTotalCount = 0;
			}
			if (this.mListItemTotalCount > DataSourceMgr.Get.TotalItemCount)
			{
				this.mListItemTotalCount = DataSourceMgr.Get.TotalItemCount;
			}
			int num = this.mListItemTotalCount / 3;
			if (this.mListItemTotalCount % 3 > 0)
			{
				num++;
			}
			this.mLoopListView.SetListItemCount(num, false);
			this.mLoopListView.RefreshAllShownItem();
		}

		public LoopListView2 mLoopListView;

		public Button mSelectAllButton;

		public Button mCancelAllButton;

		public Button mDeleteButton;

		public Button mBackButton;

		private const int mItemCountPerRow = 3;

		private int mListItemTotalCount;
	}
}
