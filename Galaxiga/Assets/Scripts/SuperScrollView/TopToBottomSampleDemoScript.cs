using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class TopToBottomSampleDemoScript : MonoBehaviour
	{
		private void Start()
		{
			this.InitData();
			this.mLoopListView.InitListView(this.mDataList.Count, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mAppendItemButton = GameObject.Find("ButtonPanel/buttonGroup3/AppendItemButton").GetComponent<Button>();
			this.mInsertItemButton = GameObject.Find("ButtonPanel/buttonGroup3/InsertItemButton").GetComponent<Button>();
			this.mScrollToInput = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputField").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mAppendItemButton.onClick.AddListener(new UnityAction(this.OnAppendItemBtnClicked));
			this.mInsertItemButton.onClick.AddListener(new UnityAction(this.OnInsertItemBtnClicked));
		}

		private void InitData()
		{
			this.mDataList = new List<CustomData>();
			int num = 100;
			for (int i = 0; i < num; i++)
			{
				CustomData customData = new CustomData();
				customData.mContent = "Item" + i;
				this.mDataList.Add(customData);
			}
		}

		private void AppendOneData()
		{
			CustomData customData = new CustomData();
			customData.mContent = "Item" + this.mDataList.Count;
			this.mDataList.Add(customData);
		}

		private void InsertOneData()
		{
			this.mTotalInsertedCount++;
			CustomData customData = new CustomData();
			customData.mContent = "Item(-" + this.mTotalInsertedCount + ")";
			this.mDataList.Insert(0, customData);
		}

		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0 || index >= this.mDataList.Count)
			{
				return null;
			}
			CustomData customData = this.mDataList[index];
			if (customData == null)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem16 component = loopListViewItem.GetComponent<ListItem16>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.mNameText.text = customData.mContent;
			return loopListViewItem;
		}

		private void OnJumpBtnClicked()
		{
			int itemIndex = 0;
			if (!int.TryParse(this.mScrollToInput.text, out itemIndex))
			{
				return;
			}
			this.mLoopListView.MovePanelToItemIndex(itemIndex, 0f);
		}

		private void OnAppendItemBtnClicked()
		{
			this.AppendOneData();
			this.mLoopListView.SetListItemCount(this.mDataList.Count, false);
			this.mLoopListView.RefreshAllShownItem();
		}

		private void OnInsertItemBtnClicked()
		{
			this.InsertOneData();
			this.mLoopListView.SetListItemCount(this.mDataList.Count, false);
			this.mLoopListView.RefreshAllShownItem();
		}

		public LoopListView2 mLoopListView;

		private Button mScrollToButton;

		private Button mAppendItemButton;

		private Button mInsertItemButton;

		private InputField mScrollToInput;

		private List<CustomData> mDataList;

		private int mTotalInsertedCount;
	}
}
