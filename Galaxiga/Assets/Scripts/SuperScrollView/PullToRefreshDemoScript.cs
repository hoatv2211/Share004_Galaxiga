using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class PullToRefreshDemoScript : MonoBehaviour
	{
		private void Start()
		{
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount + 1, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mLoopListView.mOnBeginDragAction = new Action(this.OnBeginDrag);
			this.mLoopListView.mOnDragingAction = new Action(this.OnDraging);
			this.mLoopListView.mOnEndDragAction = new Action(this.OnEndDrag);
			this.mSetCountButton = GameObject.Find("ButtonPanel/buttonGroup1/SetCountButton").GetComponent<Button>();
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mAddItemButton = GameObject.Find("ButtonPanel/buttonGroup3/AddItemButton").GetComponent<Button>();
			this.mSetCountInput = GameObject.Find("ButtonPanel/buttonGroup1/SetCountInputField").GetComponent<InputField>();
			this.mScrollToInput = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputField").GetComponent<InputField>();
			this.mAddItemInput = GameObject.Find("ButtonPanel/buttonGroup3/AddItemInputField").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mAddItemButton.onClick.AddListener(new UnityAction(this.OnAddItemBtnClicked));
			this.mSetCountButton.onClick.AddListener(new UnityAction(this.OnSetItemCountBtnClicked));
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
		}

		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0 || index > DataSourceMgr.Get.TotalItemCount)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem;
			if (index == 0)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab0");
				this.UpdateLoadingTip(loopListViewItem);
				return loopListViewItem;
			}
			int num = index - 1;
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(num);
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
			component.SetItemData(itemDataByIndex, num);
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
				component.mText.text = "Release to Refresh";
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
			else if (this.mLoadingTipStatus == LoadingTipStatus.Loaded)
			{
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(false);
				component.mText.text = "Refreshed Success";
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
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			ScrollRect scrollRect = this.mLoopListView.ScrollRect;
			if (scrollRect.content.anchoredPosition3D.y < -this.mLoadingTipItemHeight)
			{
				if (this.mLoadingTipStatus != LoadingTipStatus.None)
				{
					return;
				}
				this.mLoadingTipStatus = LoadingTipStatus.WaitRelease;
				this.UpdateLoadingTip(shownItemByItemIndex);
				shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mLoadingTipItemHeight, 0f);
			}
			else
			{
				if (this.mLoadingTipStatus != LoadingTipStatus.WaitRelease)
				{
					return;
				}
				this.mLoadingTipStatus = LoadingTipStatus.None;
				this.UpdateLoadingTip(shownItemByItemIndex);
				shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, 0f, 0f);
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
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
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
			DataSourceMgr.Get.RequestRefreshDataList(new Action(this.OnDataSourceRefreshFinished));
		}

		private void OnDataSourceRefreshFinished()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus == LoadingTipStatus.WaitLoad)
			{
				this.mLoadingTipStatus = LoadingTipStatus.Loaded;
				this.mDataLoadedTipShowLeftTime = 0.7f;
				LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
				if (shownItemByItemIndex == null)
				{
					return;
				}
				this.UpdateLoadingTip(shownItemByItemIndex);
				this.mLoopListView.RefreshAllShownItem();
			}
		}

		private void Update()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			if (this.mLoadingTipStatus == LoadingTipStatus.Loaded)
			{
				this.mDataLoadedTipShowLeftTime -= Time.deltaTime;
				if (this.mDataLoadedTipShowLeftTime <= 0f)
				{
					this.mLoadingTipStatus = LoadingTipStatus.None;
					LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
					if (shownItemByItemIndex == null)
					{
						return;
					}
					this.UpdateLoadingTip(shownItemByItemIndex);
					shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, -this.mLoadingTipItemHeight, 0f);
					this.mLoopListView.OnItemSizeChanged(0);
				}
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

		private void OnAddItemBtnClicked()
		{
			if (this.mLoopListView.ItemTotalCount < 0)
			{
				return;
			}
			int num = 0;
			if (!int.TryParse(this.mAddItemInput.text, out num))
			{
				return;
			}
			num = this.mLoopListView.ItemTotalCount + num;
			if (num < 0 || num > DataSourceMgr.Get.TotalItemCount + 1)
			{
				return;
			}
			this.mLoopListView.SetListItemCount(num, false);
		}

		private void OnSetItemCountBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mSetCountInput.text, out num))
			{
				return;
			}
			if (num < 0 || num > DataSourceMgr.Get.TotalItemCount)
			{
				return;
			}
			num++;
			this.mLoopListView.SetListItemCount(num, false);
		}

		public LoopListView2 mLoopListView;

		private LoadingTipStatus mLoadingTipStatus;

		private float mDataLoadedTipShowLeftTime;

		private float mLoadingTipItemHeight = 100f;

		private Button mScrollToButton;

		private Button mAddItemButton;

		private Button mSetCountButton;

		private InputField mScrollToInput;

		private InputField mAddItemInput;

		private InputField mSetCountInput;

		private Button mBackButton;
	}
}
