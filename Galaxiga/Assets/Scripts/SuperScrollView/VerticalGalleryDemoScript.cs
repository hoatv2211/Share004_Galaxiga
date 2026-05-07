using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class VerticalGalleryDemoScript : MonoBehaviour
	{
		private void Start()
		{
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
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

		private void LateUpdate()
		{
			this.mLoopListView.UpdateAllShownItemSnapData();
			int shownItemCount = this.mLoopListView.ShownItemCount;
			for (int i = 0; i < shownItemCount; i++)
			{
				LoopListViewItem2 shownItemByIndex = this.mLoopListView.GetShownItemByIndex(i);
				ListItem2 component = shownItemByIndex.GetComponent<ListItem2>();
				float num = 1f - Mathf.Abs(shownItemByIndex.DistanceWithViewPortSnapCenter) / 700f;
				num = Mathf.Clamp(num, 0.4f, 1f);
				component.mContentRootObj.GetComponent<CanvasGroup>().alpha = num;
				component.mContentRootObj.transform.localScale = new Vector3(num, num, 1f);
			}
		}

		private void OnJumpBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mScrollToInput.text, out num))
			{
				return;
			}
			num -= 2;
			if (num < 0)
			{
				num = 0;
			}
			this.mLoopListView.MovePanelToItemIndex(num, 0f);
			this.mLoopListView.FinishSnapImmediately();
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
			if (num < 0 || num > DataSourceMgr.Get.TotalItemCount)
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
			this.mLoopListView.SetListItemCount(num, false);
		}

		public LoopListView2 mLoopListView;

		private Button mScrollToButton;

		private Button mAddItemButton;

		private Button mSetCountButton;

		private InputField mScrollToInput;

		private InputField mAddItemInput;

		private InputField mSetCountInput;

		private Button mBackButton;
	}
}
