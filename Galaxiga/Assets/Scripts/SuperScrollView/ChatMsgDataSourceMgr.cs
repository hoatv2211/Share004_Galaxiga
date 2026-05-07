using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	public class ChatMsgDataSourceMgr : MonoBehaviour
	{
		public static ChatMsgDataSourceMgr Get
		{
			get
			{
				if (ChatMsgDataSourceMgr.instance == null)
				{
					ChatMsgDataSourceMgr.instance = UnityEngine.Object.FindObjectOfType<ChatMsgDataSourceMgr>();
				}
				return ChatMsgDataSourceMgr.instance;
			}
		}

		private void Awake()
		{
			this.Init();
		}

		public PersonInfo GetPersonInfo(int personId)
		{
			PersonInfo result = null;
			if (this.mPersonInfoDict.TryGetValue(personId, out result))
			{
				return result;
			}
			return null;
		}

		public void Init()
		{
			this.mPersonInfoDict.Clear();
			PersonInfo personInfo = new PersonInfo();
			personInfo.mHeadIcon = "grid_pencil_128_g8";
			personInfo.mId = 0;
			personInfo.mName = "Jaci";
			this.mPersonInfoDict.Add(personInfo.mId, personInfo);
			personInfo = new PersonInfo();
			personInfo.mHeadIcon = "grid_pencil_128_g5";
			personInfo.mId = 1;
			personInfo.mName = "Toc";
			this.mPersonInfoDict.Add(personInfo.mId, personInfo);
			this.InitChatDataSource();
		}

		public ChatMsg GetChatMsgByIndex(int index)
		{
			if (index < 0 || index >= this.mChatMsgList.Count)
			{
				return null;
			}
			return this.mChatMsgList[index];
		}

		public int TotalItemCount
		{
			get
			{
				return this.mChatMsgList.Count;
			}
		}

		private void InitChatDataSource()
		{
			this.mChatMsgList.Clear();
			int num = ChatMsgDataSourceMgr.mChatDemoStrList.Length;
			int num2 = ChatMsgDataSourceMgr.mChatDemoPicList.Length;
			for (int i = 0; i < 100; i++)
			{
				ChatMsg chatMsg = new ChatMsg();
				chatMsg.mMsgType = (MsgTypeEnum)(UnityEngine.Random.Range(0, 99) % 2);
				chatMsg.mPersonId = UnityEngine.Random.Range(0, 99) % 2;
				chatMsg.mSrtMsg = ChatMsgDataSourceMgr.mChatDemoStrList[UnityEngine.Random.Range(0, 99) % num];
				chatMsg.mPicMsgSpriteName = ChatMsgDataSourceMgr.mChatDemoPicList[UnityEngine.Random.Range(0, 99) % num2];
				this.mChatMsgList.Add(chatMsg);
			}
		}

		public void AppendOneMsg()
		{
			int num = ChatMsgDataSourceMgr.mChatDemoStrList.Length;
			int num2 = ChatMsgDataSourceMgr.mChatDemoPicList.Length;
			ChatMsg chatMsg = new ChatMsg();
			chatMsg.mMsgType = (MsgTypeEnum)(UnityEngine.Random.Range(0, 99) % 2);
			chatMsg.mPersonId = UnityEngine.Random.Range(0, 99) % 2;
			chatMsg.mSrtMsg = ChatMsgDataSourceMgr.mChatDemoStrList[UnityEngine.Random.Range(0, 99) % num];
			chatMsg.mPicMsgSpriteName = ChatMsgDataSourceMgr.mChatDemoPicList[UnityEngine.Random.Range(0, 99) % num2];
			this.mChatMsgList.Add(chatMsg);
		}

		private Dictionary<int, PersonInfo> mPersonInfoDict = new Dictionary<int, PersonInfo>();

		private List<ChatMsg> mChatMsgList = new List<ChatMsg>();

		private static ChatMsgDataSourceMgr instance = null;

		private static string[] mChatDemoStrList = new string[]
		{
			"Support ListView and GridView.",
			"Support Infinity Vertical and Horizontal ScrollView.",
			"Support items in different sizes such as widths or heights. Support items with unknown size at init time.",
			"Support changing item count and item size at runtime. Support looping items such as spinners. Support item padding.",
			"Use only one C# script to help the UGUI ScrollRect to support any count items with high performance."
		};

		private static string[] mChatDemoPicList = new string[]
		{
			"grid_pencil_128_g2",
			"grid_flower_200_3",
			"grid_pencil_128_g3",
			"grid_flower_200_7"
		};
	}
}
