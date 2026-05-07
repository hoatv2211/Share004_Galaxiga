using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class StaggeredGridView_LeftToRightDemoScript : MonoBehaviour
	{
		private void Start()
		{
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
			this.InitItemHeightArrayForDemo();
			GridViewLayoutParam gridViewLayoutParam = new GridViewLayoutParam();
			gridViewLayoutParam.mPadding1 = 10f;
			gridViewLayoutParam.mPadding2 = 10f;
			gridViewLayoutParam.mColumnOrRowCount = 2;
			gridViewLayoutParam.mItemWidthOrHeight = 219f;
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount, gridViewLayoutParam, new Func<LoopStaggeredGridView, int, LoopStaggeredGridViewItem>(this.OnGetItemByIndex), null);
		}

		private LoopStaggeredGridViewItem OnGetItemByIndex(LoopStaggeredGridView listView, int index)
		{
			if (index < 0)
			{
				return null;
			}
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(index);
			if (itemDataByIndex == null)
			{
				return null;
			}
			LoopStaggeredGridViewItem loopStaggeredGridViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem5 component = loopStaggeredGridViewItem.GetComponent<ListItem5>();
			if (!loopStaggeredGridViewItem.IsInitHandlerCalled)
			{
				loopStaggeredGridViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.SetItemData(itemDataByIndex, index);
			float size = 390f + (float)this.mItemWidthArrayForDemo[index % this.mItemWidthArrayForDemo.Length] * 10f;
			loopStaggeredGridViewItem.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
			return loopStaggeredGridViewItem;
		}

		private void InitItemHeightArrayForDemo()
		{
			this.mItemWidthArrayForDemo = new int[100];
			for (int i = 0; i < this.mItemWidthArrayForDemo.Length; i++)
			{
				this.mItemWidthArrayForDemo[i] = UnityEngine.Random.Range(0, 20);
			}
		}

		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
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
			this.mLoopListView.MovePanelToItemIndex(num, 0f);
		}

		private void OnAddItemBtnClicked()
		{
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

		public LoopStaggeredGridView mLoopListView;

		private Button mScrollToButton;

		private Button mAddItemButton;

		private Button mSetCountButton;

		private InputField mScrollToInput;

		private InputField mAddItemInput;

		private InputField mSetCountInput;

		private Button mBackButton;

		private int mItemTotalCount;

		private int[] mItemWidthArrayForDemo;
	}
}
