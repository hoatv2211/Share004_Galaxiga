using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class PullToLoadMoreDemoScript : MonoBehaviour
	{
		private void Start()
		{
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount + 1, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mLoopListView.mOnBeginDragAction = new Action(this.OnBeginDrag);
			this.mLoopListView.mOnDragingAction = new Action(this.OnDraging);
			this.mLoopListView.mOnEndDragAction = new Action(this.OnEndDrag);
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
				this.UpdateLoadingTip(loopListViewItem);
				return loopListViewItem;
			}
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(index);
			if (itemDataByIndex == null)
			{
				return null;
			}
			loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem2 component = loopListViewItem.GetComponent<ListItem2>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			if (index == DataSourceMgr.Get.TotalItemCount - 1)
			{
				loopListViewItem.Padding = 0f;
			}
			component.SetItemData(itemDataByIndex, index);
			return loopListViewItem;
		}

		private void UpdateLoadingTip(LoopListViewItem2 item)
		{
			if (item == null)
			{
				return;
			}
			ListItem0 component = item.GetComponent<ListItem0>();
			if (component == null)
			{
				return;
			}
			if (this.mLoadingTipStatus == LoadingTipStatus.None)
			{
				component.mRoot.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
			}
			else if (this.mLoadingTipStatus == LoadingTipStatus.WaitRelease)
			{
				component.mRoot.SetActive(true);
				component.mText.text = "Release to Load More";
				component.mArrow.SetActive(true);
				component.mWaitingIcon.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight);
			}
			else if (this.mLoadingTipStatus == LoadingTipStatus.WaitLoad)
			{
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(true);
				component.mText.text = "Loading ...";
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight);
			}
		}

		private void OnBeginDrag()
		{
		}

		private void OnDraging()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus != LoadingTipStatus.None && this.mLoadingTipStatus != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex2 = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount - 1);
			if (shownItemByItemIndex2 == null)
			{
				return;
			}
			float y = this.mLoopListView.GetItemCornerPosInViewPort(shownItemByItemIndex2, ItemCornerEnum.LeftBottom).y;
			if (y + this.mLoopListView.ViewPortSize >= this.mLoadingTipItemHeight)
			{
				if (this.mLoadingTipStatus != LoadingTipStatus.None)
				{
					return;
				}
				this.mLoadingTipStatus = LoadingTipStatus.WaitRelease;
				this.UpdateLoadingTip(shownItemByItemIndex);
			}
			else
			{
				if (this.mLoadingTipStatus != LoadingTipStatus.WaitRelease)
				{
					return;
				}
				this.mLoadingTipStatus = LoadingTipStatus.None;
				this.UpdateLoadingTip(shownItemByItemIndex);
			}
		}

		private void OnEndDrag()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus != LoadingTipStatus.None && this.mLoadingTipStatus != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			this.mLoopListView.OnItemSizeChanged(shownItemByItemIndex.ItemIndex);
			if (this.mLoadingTipStatus != LoadingTipStatus.WaitRelease)
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

		private float mLoadingTipItemHeight = 100f;

		private int mLoadMoreCount = 20;

		private Button mScrollToButton;

		private InputField mScrollToInput;

		private Button mBackButton;
	}
}
