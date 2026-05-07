using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	public class DataSourceMgr : MonoBehaviour
	{
		public static DataSourceMgr Get
		{
			get
			{
				if (DataSourceMgr.instance == null)
				{
					DataSourceMgr.instance = UnityEngine.Object.FindObjectOfType<DataSourceMgr>();
				}
				return DataSourceMgr.instance;
			}
		}

		private void Awake()
		{
			this.Init();
		}

		public void Init()
		{
			this.DoRefreshDataSource();
		}

		public ItemData GetItemDataByIndex(int index)
		{
			if (index < 0 || index >= this.mItemDataList.Count)
			{
				return null;
			}
			return this.mItemDataList[index];
		}

		public ItemData GetItemDataById(int itemId)
		{
			int count = this.mItemDataList.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.mItemDataList[i].mId == itemId)
				{
					return this.mItemDataList[i];
				}
			}
			return null;
		}

		public int TotalItemCount
		{
			get
			{
				return this.mItemDataList.Count;
			}
		}

		public void RequestRefreshDataList(Action onReflushFinished)
		{
			this.mDataRefreshLeftTime = 1f;
			this.mOnRefreshFinished = onReflushFinished;
			this.mIsWaittingRefreshData = true;
		}

		public void RequestLoadMoreDataList(int loadCount, Action onLoadMoreFinished)
		{
			this.mLoadMoreCount = loadCount;
			this.mDataLoadLeftTime = 1f;
			this.mOnLoadMoreFinished = onLoadMoreFinished;
			this.mIsWaitLoadingMoreData = true;
		}

		public void Update()
		{
			if (this.mIsWaittingRefreshData)
			{
				this.mDataRefreshLeftTime -= Time.deltaTime;
				if (this.mDataRefreshLeftTime <= 0f)
				{
					this.mIsWaittingRefreshData = false;
					this.DoRefreshDataSource();
					if (this.mOnRefreshFinished != null)
					{
						this.mOnRefreshFinished();
					}
				}
			}
			if (this.mIsWaitLoadingMoreData)
			{
				this.mDataLoadLeftTime -= Time.deltaTime;
				if (this.mDataLoadLeftTime <= 0f)
				{
					this.mIsWaitLoadingMoreData = false;
					this.DoLoadMoreDataSource();
					if (this.mOnLoadMoreFinished != null)
					{
						this.mOnLoadMoreFinished();
					}
				}
			}
		}

		public void SetDataTotalCount(int count)
		{
			this.mTotalDataCount = count;
			this.DoRefreshDataSource();
		}

		public void ExchangeData(int index1, int index2)
		{
			ItemData value = this.mItemDataList[index1];
			ItemData value2 = this.mItemDataList[index2];
			this.mItemDataList[index1] = value2;
			this.mItemDataList[index2] = value;
		}

		public void RemoveData(int index)
		{
			this.mItemDataList.RemoveAt(index);
		}

		public void InsertData(int index, ItemData data)
		{
			this.mItemDataList.Insert(index, data);
		}

		private void DoRefreshDataSource()
		{
			this.mItemDataList.Clear();
			for (int i = 0; i < this.mTotalDataCount; i++)
			{
				ItemData itemData = new ItemData();
				itemData.mId = i;
				itemData.mName = "Item" + i;
				itemData.mDesc = "Item Desc For Item " + i;
				itemData.mIcon = ResManager.Get.GetSpriteNameByIndex(UnityEngine.Random.Range(0, 24));
				itemData.mStarCount = UnityEngine.Random.Range(0, 6);
				itemData.mFileSize = UnityEngine.Random.Range(20, 999);
				itemData.mChecked = false;
				itemData.mIsExpand = false;
				this.mItemDataList.Add(itemData);
			}
		}

		private void DoLoadMoreDataSource()
		{
			int count = this.mItemDataList.Count;
			for (int i = 0; i < this.mLoadMoreCount; i++)
			{
				int num = i + count;
				ItemData itemData = new ItemData();
				itemData.mId = num;
				itemData.mName = "Item" + num;
				itemData.mDesc = "Item Desc For Item " + num;
				itemData.mIcon = ResManager.Get.GetSpriteNameByIndex(UnityEngine.Random.Range(0, 24));
				itemData.mStarCount = UnityEngine.Random.Range(0, 6);
				itemData.mFileSize = UnityEngine.Random.Range(20, 999);
				itemData.mChecked = false;
				itemData.mIsExpand = false;
				this.mItemDataList.Add(itemData);
			}
			this.mTotalDataCount = this.mItemDataList.Count;
		}

		public void CheckAllItem()
		{
			int count = this.mItemDataList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemDataList[i].mChecked = true;
			}
		}

		public void UnCheckAllItem()
		{
			int count = this.mItemDataList.Count;
			for (int i = 0; i < count; i++)
			{
				this.mItemDataList[i].mChecked = false;
			}
		}

		public bool DeleteAllCheckedItem()
		{
			int count = this.mItemDataList.Count;
			this.mItemDataList.RemoveAll((ItemData it) => it.mChecked);
			return count != this.mItemDataList.Count;
		}

		private List<ItemData> mItemDataList = new List<ItemData>();

		private Action mOnRefreshFinished;

		private Action mOnLoadMoreFinished;

		private int mLoadMoreCount = 20;

		private float mDataLoadLeftTime;

		private float mDataRefreshLeftTime;

		private bool mIsWaittingRefreshData;

		private bool mIsWaitLoadingMoreData;

		public int mTotalDataCount = 10000;

		private static DataSourceMgr instance;
	}
}
