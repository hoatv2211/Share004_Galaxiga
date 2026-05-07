using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class ResponsiveGridViewDemoScript2 : MonoBehaviour
	{
		private void Start()
		{
			this.mLoopListView.InitListView(this.GetMaxRowCount() + 2, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mDragChangSizeScript.mOnDragEndAction = new Action(this.OnViewPortSizeChanged);
			this.mLoopListView.mOnDragingAction = new Action(this.OnDraging);
			this.mLoopListView.mOnEndDragAction = new Action(this.OnEndDrag);
			this.OnViewPortSizeChanged();
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

		private void UpdateItemPrefab()
		{
			ItemPrefabConfData itemPrefabConfData = this.mLoopListView.GetItemPrefabConfData("ItemPrefab2");
			GameObject mItemPrefab = itemPrefabConfData.mItemPrefab;
			RectTransform component = mItemPrefab.GetComponent<RectTransform>();
			ListItem6 component2 = mItemPrefab.GetComponent<ListItem6>();
			float viewPortWidth = this.mLoopListView.ViewPortWidth;
			int count = component2.mItemList.Count;
			GameObject gameObject = component2.mItemList[0].gameObject;
			RectTransform component3 = gameObject.GetComponent<RectTransform>();
			float width = component3.rect.width;
			int num = Mathf.FloorToInt(viewPortWidth / width);
			if (num == 0)
			{
				num = 1;
			}
			this.mItemCountPerRow = num;
			float num2 = (viewPortWidth - width * (float)num) / (float)(num + 1);
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			component.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, viewPortWidth);
			if (num > count)
			{
				int num3 = num - count;
				for (int i = 0; i < num3; i++)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity, component);
					RectTransform component4 = gameObject2.GetComponent<RectTransform>();
					component4.localScale = Vector3.one;
					component4.anchoredPosition3D = Vector3.zero;
					component4.rotation = Quaternion.identity;
					ListItem5 component5 = gameObject2.GetComponent<ListItem5>();
					component2.mItemList.Add(component5);
				}
			}
			else if (num < count)
			{
				int num4 = count - num;
				for (int j = 0; j < num4; j++)
				{
					ListItem5 listItem = component2.mItemList[component2.mItemList.Count - 1];
					component2.mItemList.RemoveAt(component2.mItemList.Count - 1);
					UnityEngine.Object.DestroyImmediate(listItem.gameObject);
				}
			}
			float num5 = num2;
			for (int k = 0; k < component2.mItemList.Count; k++)
			{
				GameObject gameObject3 = component2.mItemList[k].gameObject;
				gameObject3.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(num5, 0f, 0f);
				num5 = num5 + width + num2;
			}
			this.mLoopListView.OnItemPrefabChanged("ItemPrefab2");
		}

		private void OnViewPortSizeChanged()
		{
			this.UpdateItemPrefab();
			this.mLoopListView.SetListItemCount(this.GetMaxRowCount() + 2, false);
			this.mLoopListView.RefreshAllShownItem();
		}

		private int GetMaxRowCount()
		{
			int num = DataSourceMgr.Get.TotalItemCount / this.mItemCountPerRow;
			if (DataSourceMgr.Get.TotalItemCount % this.mItemCountPerRow > 0)
			{
				num++;
			}
			return num;
		}

		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int row)
		{
			if (row < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem;
			if (row == 0)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab0");
				this.UpdateLoadingTip1(loopListViewItem);
				return loopListViewItem;
			}
			if (row == this.GetMaxRowCount() + 1)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab1");
				this.UpdateLoadingTip2(loopListViewItem);
				return loopListViewItem;
			}
			int num = row - 1;
			loopListViewItem = listView.NewListViewItem("ItemPrefab2");
			ListItem6 component = loopListViewItem.GetComponent<ListItem6>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			for (int i = 0; i < this.mItemCountPerRow; i++)
			{
				int num2 = num * this.mItemCountPerRow + i;
				if (num2 >= DataSourceMgr.Get.TotalItemCount)
				{
					component.mItemList[i].gameObject.SetActive(false);
				}
				else
				{
					ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(num2);
					if (itemDataByIndex != null)
					{
						component.mItemList[i].gameObject.SetActive(true);
						component.mItemList[i].SetItemData(itemDataByIndex, num2);
					}
					else
					{
						component.mItemList[i].gameObject.SetActive(false);
					}
				}
			}
			return loopListViewItem;
		}

		private void UpdateLoadingTip1(LoopListViewItem2 item)
		{
			if (item == null)
			{
				return;
			}
			item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.mLoopListView.ViewPortWidth);
			ListItem17 component = item.GetComponent<ListItem17>();
			if (component == null)
			{
				return;
			}
			if (this.mLoadingTipStatus1 == LoadingTipStatus.None)
			{
				component.mRoot1.SetActive(false);
				component.mRoot.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitContinureDrag)
			{
				component.mRoot1.SetActive(true);
				component.mRoot.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitRelease)
			{
				component.mRoot1.SetActive(false);
				component.mRoot.SetActive(true);
				component.mText.text = "Release to Refresh";
				component.mArrow.SetActive(true);
				component.mWaitingIcon.SetActive(false);
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight1);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitLoad)
			{
				component.mRoot1.SetActive(false);
				component.mRoot.SetActive(true);
				component.mArrow.SetActive(false);
				component.mWaitingIcon.SetActive(true);
				component.mText.text = "Loading ...";
				item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.mLoadingTipItemHeight1);
			}
			else if (this.mLoadingTipStatus1 == LoadingTipStatus.Loaded)
			{
				component.mRoot1.SetActive(false);
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
			if (this.mLoadingTipStatus1 != LoadingTipStatus.None && this.mLoadingTipStatus1 != LoadingTipStatus.WaitRelease && this.mLoadingTipStatus1 != LoadingTipStatus.WaitContinureDrag)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			ScrollRect scrollRect = this.mLoopListView.ScrollRect;
			Vector3 anchoredPosition3D = scrollRect.content.anchoredPosition3D;
			if (anchoredPosition3D.y >= 0f)
			{
				if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitContinureDrag)
				{
					this.mLoadingTipStatus1 = LoadingTipStatus.None;
					this.UpdateLoadingTip1(shownItemByItemIndex);
					shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, 0f, 0f);
				}
			}
			else if (anchoredPosition3D.y < 0f && anchoredPosition3D.y > -this.mLoadingTipItemHeight1)
			{
				if (this.mLoadingTipStatus1 == LoadingTipStatus.None || this.mLoadingTipStatus1 == LoadingTipStatus.WaitRelease)
				{
					this.mLoadingTipStatus1 = LoadingTipStatus.WaitContinureDrag;
					this.UpdateLoadingTip1(shownItemByItemIndex);
					shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, 0f, 0f);
				}
			}
			else if (anchoredPosition3D.y <= -this.mLoadingTipItemHeight1 && this.mLoadingTipStatus1 == LoadingTipStatus.WaitContinureDrag)
			{
				this.mLoadingTipStatus1 = LoadingTipStatus.WaitRelease;
				this.UpdateLoadingTip1(shownItemByItemIndex);
				shownItemByItemIndex.CachedRectTransform.anchoredPosition3D = new Vector3(0f, this.mLoadingTipItemHeight1, 0f);
			}
		}

		private void OnEndDrag1()
		{
			if (this.mLoopListView.ShownItemCount == 0)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(0);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			this.mLoopListView.OnItemSizeChanged(shownItemByItemIndex.ItemIndex);
			if (this.mLoadingTipStatus1 == LoadingTipStatus.WaitRelease)
			{
				this.mLoadingTipStatus1 = LoadingTipStatus.WaitLoad;
				this.UpdateLoadingTip1(shownItemByItemIndex);
				DataSourceMgr.Get.RequestRefreshDataList(new Action(this.OnDataSourceRefreshFinished));
			}
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
			item.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.mLoopListView.ViewPortWidth);
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
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(this.GetMaxRowCount() + 1);
			if (shownItemByItemIndex == null)
			{
				return;
			}
			LoopListViewItem2 shownItemByItemIndex2 = this.mLoopListView.GetShownItemByItemIndex(this.GetMaxRowCount());
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
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(this.GetMaxRowCount() + 1);
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
				this.mLoopListView.SetListItemCount(this.GetMaxRowCount() + 2, false);
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
				num = 0;
			}
			num++;
			int num2 = num / this.mItemCountPerRow;
			if (num % this.mItemCountPerRow > 0)
			{
				num2++;
			}
			if (num2 > 0)
			{
				num2--;
			}
			num2++;
			this.mLoopListView.MovePanelToItemIndex(num2, 0f);
		}

		public LoopListView2 mLoopListView;

		private LoadingTipStatus mLoadingTipStatus1;

		private LoadingTipStatus mLoadingTipStatus2;

		private float mDataLoadedTipShowLeftTime;

		private float mLoadingTipItemHeight1 = 100f;

		private float mLoadingTipItemHeight2 = 100f;

		private int mLoadMoreCount = 20;

		private Button mScrollToButton;

		private InputField mScrollToInput;

		private Button mBackButton;

		private int mItemCountPerRow = 3;

		public DragChangSizeScript mDragChangSizeScript;
	}
}
