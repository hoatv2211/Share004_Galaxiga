using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	public class ItemPool
	{
		public void Init(GameObject prefabObj, float padding, float startPosOffset, int createCount, RectTransform parent)
		{
			this.mPrefabObj = prefabObj;
			this.mPrefabName = this.mPrefabObj.name;
			this.mInitCreateCount = createCount;
			this.mPadding = padding;
			this.mStartPosOffset = startPosOffset;
			this.mItemParent = parent;
			this.mPrefabObj.SetActive(false);
			for (int i = 0; i < this.mInitCreateCount; i++)
			{
				LoopListViewItem2 item = this.CreateItem();
				this.RecycleItemReal(item);
			}
		}

		public LoopListViewItem2 GetItem()
		{
			ItemPool.mCurItemIdCount++;
			LoopListViewItem2 loopListViewItem;
			if (this.mTmpPooledItemList.Count > 0)
			{
				int count = this.mTmpPooledItemList.Count;
				loopListViewItem = this.mTmpPooledItemList[count - 1];
				this.mTmpPooledItemList.RemoveAt(count - 1);
				loopListViewItem.gameObject.SetActive(true);
			}
			else
			{
				int count2 = this.mPooledItemList.Count;
				if (count2 == 0)
				{
					loopListViewItem = this.CreateItem();
				}
				else
				{
					loopListViewItem = this.mPooledItemList[count2 - 1];
					this.mPooledItemList.RemoveAt(count2 - 1);
					loopListViewItem.gameObject.SetActive(true);
				}
			}
			loopListViewItem.Padding = this.mPadding;
			loopListViewItem.ItemId = ItemPool.mCurItemIdCount;
			return loopListViewItem;
		}

		public void DestroyAllItem()
		{
			this.ClearTmpRecycledItem();
			int count = this.mPooledItemList.Count;
			for (int i = 0; i < count; i++)
			{
				UnityEngine.Object.DestroyImmediate(this.mPooledItemList[i].gameObject);
			}
			this.mPooledItemList.Clear();
		}

		public LoopListViewItem2 CreateItem()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mPrefabObj, Vector3.zero, Quaternion.identity, this.mItemParent);
			gameObject.SetActive(true);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			LoopListViewItem2 component2 = gameObject.GetComponent<LoopListViewItem2>();
			component2.ItemPrefabName = this.mPrefabName;
			component2.StartPosOffset = this.mStartPosOffset;
			return component2;
		}

		private void RecycleItemReal(LoopListViewItem2 item)
		{
			item.gameObject.SetActive(false);
			this.mPooledItemList.Add(item);
		}

		public void RecycleItem(LoopListViewItem2 item)
		{
			this.mTmpPooledItemList.Add(item);
		}

		public void ClearTmpRecycledItem()
		{
			int count = this.mTmpPooledItemList.Count;
			if (count == 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				this.RecycleItemReal(this.mTmpPooledItemList[i]);
			}
			this.mTmpPooledItemList.Clear();
		}

		private GameObject mPrefabObj;

		private string mPrefabName;

		private int mInitCreateCount = 1;

		private float mPadding;

		private float mStartPosOffset;

		private List<LoopListViewItem2> mTmpPooledItemList = new List<LoopListViewItem2>();

		private List<LoopListViewItem2> mPooledItemList = new List<LoopListViewItem2>();

		private static int mCurItemIdCount;

		private RectTransform mItemParent;
	}
}
