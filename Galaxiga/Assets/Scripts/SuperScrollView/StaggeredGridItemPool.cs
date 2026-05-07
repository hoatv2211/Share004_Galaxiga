using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	public class StaggeredGridItemPool
	{
		public void Init(GameObject prefabObj, float padding, int createCount, RectTransform parent)
		{
			this.mPrefabObj = prefabObj;
			this.mPrefabName = this.mPrefabObj.name;
			this.mInitCreateCount = createCount;
			this.mPadding = padding;
			this.mItemParent = parent;
			this.mPrefabObj.SetActive(false);
			for (int i = 0; i < this.mInitCreateCount; i++)
			{
				LoopStaggeredGridViewItem item = this.CreateItem();
				this.RecycleItemReal(item);
			}
		}

		public LoopStaggeredGridViewItem GetItem()
		{
			StaggeredGridItemPool.mCurItemIdCount++;
			LoopStaggeredGridViewItem loopStaggeredGridViewItem;
			if (this.mTmpPooledItemList.Count > 0)
			{
				int count = this.mTmpPooledItemList.Count;
				loopStaggeredGridViewItem = this.mTmpPooledItemList[count - 1];
				this.mTmpPooledItemList.RemoveAt(count - 1);
				loopStaggeredGridViewItem.gameObject.SetActive(true);
			}
			else
			{
				int count2 = this.mPooledItemList.Count;
				if (count2 == 0)
				{
					loopStaggeredGridViewItem = this.CreateItem();
				}
				else
				{
					loopStaggeredGridViewItem = this.mPooledItemList[count2 - 1];
					this.mPooledItemList.RemoveAt(count2 - 1);
					loopStaggeredGridViewItem.gameObject.SetActive(true);
				}
			}
			loopStaggeredGridViewItem.Padding = this.mPadding;
			loopStaggeredGridViewItem.ItemId = StaggeredGridItemPool.mCurItemIdCount;
			return loopStaggeredGridViewItem;
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

		public LoopStaggeredGridViewItem CreateItem()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mPrefabObj, Vector3.zero, Quaternion.identity, this.mItemParent);
			gameObject.SetActive(true);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.localScale = Vector3.one;
			component.anchoredPosition3D = Vector3.zero;
			component.localEulerAngles = Vector3.zero;
			LoopStaggeredGridViewItem component2 = gameObject.GetComponent<LoopStaggeredGridViewItem>();
			component2.ItemPrefabName = this.mPrefabName;
			component2.StartPosOffset = 0f;
			return component2;
		}

		private void RecycleItemReal(LoopStaggeredGridViewItem item)
		{
			item.gameObject.SetActive(false);
			this.mPooledItemList.Add(item);
		}

		public void RecycleItem(LoopStaggeredGridViewItem item)
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

		private List<LoopStaggeredGridViewItem> mTmpPooledItemList = new List<LoopStaggeredGridViewItem>();

		private List<LoopStaggeredGridViewItem> mPooledItemList = new List<LoopStaggeredGridViewItem>();

		private static int mCurItemIdCount;

		private RectTransform mItemParent;
	}
}
