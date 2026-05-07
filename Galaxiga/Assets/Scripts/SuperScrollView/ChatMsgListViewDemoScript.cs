using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class ChatMsgListViewDemoScript : MonoBehaviour
	{
		private void Start()
		{
			this.mLoopListView.InitListView(ChatMsgDataSourceMgr.Get.TotalItemCount, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mScrollToInput = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputField").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
			this.mAppendMsgButton = GameObject.Find("ButtonPanel/buttonGroup1/AppendButton").GetComponent<Button>();
			this.mAppendMsgButton.onClick.AddListener(new UnityAction(this.OnAppendMsgBtnClicked));
		}

		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		private void OnAppendMsgBtnClicked()
		{
			ChatMsgDataSourceMgr.Get.AppendOneMsg();
			this.mLoopListView.SetListItemCount(ChatMsgDataSourceMgr.Get.TotalItemCount, false);
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

		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0 || index >= ChatMsgDataSourceMgr.Get.TotalItemCount)
			{
				return null;
			}
			ChatMsg chatMsgByIndex = ChatMsgDataSourceMgr.Get.GetChatMsgByIndex(index);
			if (chatMsgByIndex == null)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem;
			if (chatMsgByIndex.mPersonId == 0)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			}
			else
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab2");
			}
			ListItem4 component = loopListViewItem.GetComponent<ListItem4>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.SetItemData(chatMsgByIndex, index);
			return loopListViewItem;
		}

		public LoopListView2 mLoopListView;

		private Button mScrollToButton;

		private InputField mScrollToInput;

		private Button mBackButton;

		private Button mAppendMsgButton;
	}
}
