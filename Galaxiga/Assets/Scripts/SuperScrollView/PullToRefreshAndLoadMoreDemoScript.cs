using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class PullToRefreshAndLoadMoreDemoScript : MonoBehaviour
	{
		private void Start()
		{
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount + 2, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
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
			if (index == 0)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab0");
				this.UpdateLoadingTip1(loopListViewItem);
				return loopListViewItem;
			}
			if (index == DataSourceMgr.Get.TotalItemCount + 1)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab1");
				this.UpdateLoadingTip2(loopListViewItem);
				return loopListViewItem;
			}
			int num = index - 1;
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(num);
			if (itemDataByIndex == null)
			{
				return null;
			}
			loopListViewItem = listView.NewListViewItem("ItemPrefab2");
			ListItem2 component = loopListViewItem.GetComponent<ListItem2>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			if (index == DataSourceMgr.Get.TotalItemCount)
			{
				loopListViewItem.Padding = 0f;
			}
			component.SetItemData(itemDataByIndex, num);
			return loopListViewItem;
		}

		private void UpdateLoadingTip1(LoopListViewItem2 item)
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
			if (this.mLoadingTipStatus1 == LoadingTipStatus.None)
			{
				component.mRoot.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitRelease)
			{
				component.mRoot.SetActive(true);
				component.mText.text = "Release to Refresh";
				component.mArrow.SetActive(true);
				component.mWaitingIcon.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight1);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitLoad)
			{
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(true);
				component.mText.text = "Loading ...";
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight1);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.Loaded)
			{
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(false);
				component.mText.text = "Refreshed Success";
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight1);
			}
		}

		private void OnDraging()
		{
			this.OnDraging1();
			this.OnDraging2();
		}

		private void OnEndDrag()
		{
			this.OnEndDrag1();
			this.OnEndDrag2();
		}

		private void OnDraging1()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus1 != LoadingTipStatus.None && this.mLoadingTipStatus1 != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			ScrollRect scrollRect = this.mLoopListView.ScrollRect;
			if (scrollRect.content.anchoredPosition3D.y < -this.mLoadingTipItemHeight1)
			{
				if (this.mLoadingTipStatus1 != LoadingTipStatus.None)
				{
					return;
				}
				this.mLoadingTipStatus1 = LoadingTipStatus.WaitRelease;
				this.UpdateLoadingTip1(shownItemByItemIndex);
				shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mLoadingTipItemHeight1, 0f);
			}
			else
			{
				if (this.mLoadingTipStatus1 != LoadingTipStatus.WaitRelease)
				{
					return;
				}
				this.mLoadingTipStatus1 = LoadingTipStatus.None;
				this.UpdateLoadingTip1(shownItemByItemIndex);
				shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, 0f, 0f);
			}
		}

		private void OnEndDrag1()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus1 != LoadingTipStatus.None && this.mLoadingTipStatus1 != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			this.mLoopListView.OnItemSizeChanged(shownItemByItemIndex.ItemIndex);
			if (this.mLoadingTipStatus1 != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			this.mLoadingTipStatus1 = LoadingTipStatus.WaitLoad;
			this.UpdateLoadingTip1(shownItemByItemIndex);
			DataSourceMgr.Get.RequestRefreshDataList(new Action(this.OnDataSourceRefreshFinished));
		}

		private void OnDataSourceRefreshFinished()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitLoad)
			{
				this.mLoadingTipStatus1 = LoadingTipStatus.Loaded;
				this.mDataLoadedTipShowLeftTime = 0.7f;
				LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
				if (shownItemByItemIndex == null)
				{
					return;
				}
				this.UpdateLoadingTip1(shownItemByItemIndex);
				this.mLoopListView.RefreshAllShownItem();
			}
		}

		private void Update()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus1 == LoadingTipStatus.Loaded)
			{
				this.mDataLoadedTipShowLeftTime -= Time.deltaTime;
				if (this.mDataLoadedTipShowLeftTime <= 0f)
				{
					this.mLoadingTipStatus1 = LoadingTipStatus.None;
					LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
					if (shownItemByItemIndex == null)
					{
						return;
					}
					this.UpdateLoadingTip1(shownItemByItemIndex);
					shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, -this.mLoadingTipItemHeight1, 0f);
					this.mLoopListView.OnItemSizeChanged(0);
				}
			}
		}

		private void UpdateLoadingTip2(LoopListViewItem2 item)
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
			if (this.mLoadingTipStatus2 == LoadingTipStatus.None)
			{
				component.mRoot.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
			}
			else if (this.mLoadingTipStatus2 == LoadingTipStatus.WaitRelease)
			{
				component.mRoot.SetActive(true);
				component.mText.text = "Release to Load More";
				component.mArrow.SetActive(true);
				component.mWaitingIcon.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight2);
			}
			else if (this.mLoadingTipStatus2 == LoadingTipStatus.WaitLoad)
			{
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(true);
				component.mText.text = "Loading ...";
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight2);
			}
		}

		private void OnDraging2()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus2 != LoadingTipStatus.None && this.mLoadingTipStatus2 != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount + 1);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex2 = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount);
			if (shownItemByItemIndex2 == null)
			{
				return;
			}
			float y = this.mLoopListView.GetItemCornerPosInViewPort(shownItemByItemIndex2, ItemCornerEnum.LeftBottom).y;
			if (y + this.mLoopListView.ViewPortSize >= this.mLoadingTipItemHeight2)
			{
				if (this.mLoadingTipStatus2 != LoadingTipStatus.None)
				{
					return;
				}
				this.mLoadingTipStatus2 = LoadingTipStatus.WaitRelease;
				this.UpdateLoadingTip2(shownItemByItemIndex);
			}
			else
			{
				if (this.mLoadingTipStatus2 != LoadingTipStatus.WaitRelease)
				{
					return;
				}
				this.mLoadingTipStatus2 = LoadingTipStatus.None;
				this.UpdateLoadingTip2(shownItemByItemIndex);
			}
		}

		private void OnEndDrag2()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus2 != LoadingTipStatus.None && this.mLoadingTipStatus2 != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(DataSourceMgr.Get.TotalItemCount + 1);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			this.mLoopListView.OnItemSizeChanged(shownItemByItemIndex.ItemIndex);
			if (this.mLoadingTipStatus2 != LoadingTipStatus.WaitRelease)
			{
				return;
			}
			this.mLoadingTipStatus2 = LoadingTipStatus.WaitLoad;
			this.UpdateLoadingTip2(shownItemByItemIndex);
			DataSourceMgr.Get.RequestLoadMoreDataList(this.mLoadMoreCount, new Action(this.OnDataSourceLoadMoreFinished));
		}

		private void OnDataSourceLoadMoreFinished()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus2 == LoadingTipStatus.WaitLoad)
			{
				this.mLoadingTipStatus2 = LoadingTipStatus.None;
				this.mLoopListView.SetListItemCount(DataSourceMgr.Get.TotalItemCount + 2, false);
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
			num++;
			this.mLoopListView.MovePanelToItemIndex(num, 0f);
		}

		public LoopListView2 mLoopListView;

		private LoadingTipStatus mLoadingTipStatus1;

		private LoadingTipStatus mLoadingTipStatus2;

		private float mDataLoadedTipShowLeftTime;

		private float mLoadingTipItemHeight1 = 100f;

		private float mLoadingTipItemHeight2 = 100f;

		private int mLoadMoreCount = 20;

		private Button mScrollToButton;

		private Button mAddItemButton;

		private Button mSetCountButton;

		private InputField mScrollToInput;

		private InputField mAddItemInput;

		private InputField mSetCountInput;

		private Button mBackButton;
	}
}
