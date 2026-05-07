using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class SpinDatePickerDemoScript : MonoBehaviour
	{
		public int CurSelectedMonth
		{
			get
			{
				return this.mCurSelectedMonth;
			}
		}

		public int CurSelectedDay
		{
			get
			{
				return this.mCurSelectedDay;
			}
		}

		public int CurSelectedHour
		{
			get
			{
				return this.mCurSelectedHour;
			}
		}

		private void Start()
		{
			this.mLoopListViewMonth.mOnSnapNearestChanged = new Action<LoopListView2, LoopListViewItem2>(this.OnMonthSnapTargetChanged);
			this.mLoopListViewDay.mOnSnapNearestChanged = new Action<LoopListView2, LoopListViewItem2>(this.OnDaySnapTargetChanged);
			this.mLoopListViewHour.mOnSnapNearestChanged = new Action<LoopListView2, LoopListViewItem2>(this.OnHourSnapTargetChanged);
			this.mLoopListViewMonth.InitListView(-1, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndexForMonth), null);
			this.mLoopListViewDay.InitListView(-1, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndexForDay), null);
			this.mLoopListViewHour.InitListView(-1, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndexForHour), null);
			this.mLoopListViewMonth.mOnSnapItemFinished = new Action<LoopListView2, LoopListViewItem2>(this.OnMonthSnapTargetFinished);
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
		}

		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		private LoopListViewItem2 OnGetItemByIndexForHour(LoopListView2 listView, int index)
		{
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem7 component = loopListViewItem.GetComponent<ListItem7>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			int num = 1;
			int num2 = 24;
			int num3;
			if (index >= 0)
			{
				num3 = index % num2;
			}
			else
			{
				num3 = num2 + (index + 1) % num2 - 1;
			}
			num3 += num;
			component.Value = num3;
			component.mText.text = num3.ToString();
			return loopListViewItem;
		}

		private LoopListViewItem2 OnGetItemByIndexForMonth(LoopListView2 listView, int index)
		{
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem7 component = loopListViewItem.GetComponent<ListItem7>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			int num = 1;
			int num2 = 12;
			int num3;
			if (index >= 0)
			{
				num3 = index % num2;
			}
			else
			{
				num3 = num2 + (index + 1) % num2 - 1;
			}
			num3 += num;
			component.Value = num3;
			component.mText.text = SpinDatePickerDemoScript.mMonthNameArray[num3 - 1];
			return loopListViewItem;
		}

		private LoopListViewItem2 OnGetItemByIndexForDay(LoopListView2 listView, int index)
		{
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem7 component = loopListViewItem.GetComponent<ListItem7>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			int num = 1;
			int num2 = SpinDatePickerDemoScript.mMonthDayCountArray[this.mCurSelectedMonth - 1];
			int num3;
			if (index >= 0)
			{
				num3 = index % num2;
			}
			else
			{
				num3 = num2 + (index + 1) % num2 - 1;
			}
			num3 += num;
			component.Value = num3;
			component.mText.text = num3.ToString();
			return loopListViewItem;
		}

		private void OnMonthSnapTargetChanged(LoopListView2 listView, LoopListViewItem2 item)
		{
			int indexInShownItemList = listView.GetIndexInShownItemList(item);
			if (indexInShownItemList < 0)
			{
				return;
			}
			ListItem7 component = item.GetComponent<ListItem7>();
			this.mCurSelectedMonth = component.Value;
			this.OnListViewSnapTargetChanged(listView, indexInShownItemList);
		}

		private void OnDaySnapTargetChanged(LoopListView2 listView, LoopListViewItem2 item)
		{
			int indexInShownItemList = listView.GetIndexInShownItemList(item);
			if (indexInShownItemList < 0)
			{
				return;
			}
			ListItem7 component = item.GetComponent<ListItem7>();
			this.mCurSelectedDay = component.Value;
			this.OnListViewSnapTargetChanged(listView, indexInShownItemList);
		}

		private void OnHourSnapTargetChanged(LoopListView2 listView, LoopListViewItem2 item)
		{
			int indexInShownItemList = listView.GetIndexInShownItemList(item);
			if (indexInShownItemList < 0)
			{
				return;
			}
			ListItem7 component = item.GetComponent<ListItem7>();
			this.mCurSelectedHour = component.Value;
			this.OnListViewSnapTargetChanged(listView, indexInShownItemList);
		}

		private void OnMonthSnapTargetFinished(LoopListView2 listView, LoopListViewItem2 item)
		{
			LoopListViewItem2 shownItemByIndex = this.mLoopListViewDay.GetShownItemByIndex(0);
			ListItem7 component = shownItemByIndex.GetComponent<ListItem7>();
			int firstItemIndex = component.Value - 1;
			this.mLoopListViewDay.RefreshAllShownItemWithFirstIndex(firstItemIndex);
		}

		private void OnListViewSnapTargetChanged(LoopListView2 listView, int targetIndex)
		{
			int shownItemCount = listView.ShownItemCount;
			for (int i = 0; i < shownItemCount; i++)
			{
				LoopListViewItem2 shownItemByIndex = listView.GetShownItemByIndex(i);
				ListItem7 component = shownItemByIndex.GetComponent<ListItem7>();
				if (i == targetIndex)
				{
					component.mText.color = Color.red;
				}
				else
				{
					component.mText.color = Color.black;
				}
			}
		}

		public LoopListView2 mLoopListViewMonth;

		public LoopListView2 mLoopListViewDay;

		public LoopListView2 mLoopListViewHour;

		public Button mBackButton;

		private static int[] mMonthDayCountArray = new int[]
		{
			31,
			28,
			31,
			30,
			31,
			30,
			31,
			31,
			30,
			31,
			30,
			31
		};

		private static string[] mMonthNameArray = new string[]
		{
			"Jan.",
			"Feb.",
			"Mar.",
			"Apr.",
			"May.",
			"Jun.",
			"Jul.",
			"Aug.",
			"Sep.",
			"Oct.",
			"Nov.",
			"Dec."
		};

		private int mCurSelectedMonth = 2;

		private int mCurSelectedDay = 2;

		private int mCurSelectedHour = 2;
	}
}
