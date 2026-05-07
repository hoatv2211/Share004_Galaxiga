using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class ClickAndLoadMoreDemoScript : MonoBehaviour
	{
		private void Start()
		{
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount + 1, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mScrollToInput = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputField").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
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
			LoopListViewItem2 loopListViewItem;
			if (index == DataSourceMgr.Get.TotalItemCount)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab0");
				if (!loopListViewItem.IsInitHandlerCalled)
				{
					loopListViewItem.IsInitHandlerCalled = true;
					ListItem11 component = loopListViewItem.GetComponent<ListItem11>();
					component.mRootButton.onClick.AddListener(new UnityAction(this.OnLoadMoreBtnClicked));
				}
				this.UpdateLoadingTip(loopListViewItem);
				return loopListViewItem;
			}
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(index);
			if (itemDataByIndex == null)
			{
				return null;
			}
			loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem2 component2 = loopListViewItem.GetComponent<ListItem2>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component2.Init();
			}
			component2.SetItemData(itemDataByIndex, index);
			return loopListViewItem;
		}

		private void UpdateLoadingTip(LoopListViewItem2 item)
		{
			if (item == null)
			{
				return;
			}
			ListItem11 component = item.GetComponent<ListItem11>();
			if (component == null)
			{
				return;
			}
			if (this.mLoadingTipStatus == LoadingTipStatus.None)
			{
				component.mText.text = "Click to Load More";
				component.mWaitingIcon.SetActive(false);
			}
			else if (this.mLoadingTipStatus == LoadingTipStatus.WaitLoad)
			{
				component.mWaitingIcon.SetActive(true);
				component.mText.text = "Loading ...";
			}
		}

		private void OnLoadMoreBtnClicked()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus != LoadingTipStatus.None)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			this.mLoadingTipStatus = LoadingTipStatus.WaitLoad;
			this.UpdateLoadingTip(shownItemByItemIndex);
			DataSourceMgr.Get.RequestLoadMoreDataList(this.mLoadMoreCount, new Action(this.OnDataSourceLoadMoreFinished));
		}

		private void OnDataSourceLoadMoreFinished()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus == LoadingTipStatus.WaitLoad)
			{
				this.mLoadingTipStatus = LoadingTipStatus.None;
				this.mLoopListView.SetListItemCount(DataSourceMgr.Get.TotalItemCount + 1, false);
				this.mLoopListView.RefreshAllShownItem();
			}
		}

		private void OnJumpBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mScrollToInput.text, out num))
			{
				return;
			}
			if (num < 0)
			{
				return;
			}
			this.mLoopListView.MovePanelToItemIndex(num, 0f);
		}

		public LoopListView2 mLoopListView;

		private LoadingTipStatus mLoadingTipStatus;

		private int mLoadMoreCount = 20;

		private Button mScrollToButton;

		private InputField mScrollToInput;

		private Button mBackButton;
	}
}
