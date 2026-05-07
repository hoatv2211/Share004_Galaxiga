using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class ListItem13 : MonoBehaviour
	{
		public void Init()
		{
			for (int i = 0; i < this.mStarArray.Length; i++)
			{
				int index = i;
				ClickEventListener clickEventListener = ClickEventListener.Get(this.mStarArray[i].gameObject);
				clickEventListener.SetClickEventHandler(delegate(GameObject obj)
				{
					this.OnStarClicked(index);
				});
			}
		}

		private void OnStarClicked(int index)
		{
			ItemData itemChildDataByIndex = TreeViewDataSourceMgr.Get.GetItemChildDataByIndex(this.mItemDataIndex, this.mChildDataIndex);
			if (itemChildDataByIndex == null)
			{
				return;
			}
			if (index == 0 && itemChildDataByIndex.mStarCount == 1)
			{
				itemChildDataByIndex.mStarCount = 0;
			}
			else
			{
				itemChildDataByIndex.mStarCount = index + 1;
			}
			this.SetStarCount(itemChildDataByIndex.mStarCount);
		}

		public void SetStarCount(int count)
		{
			int i;
			for (i = 0; i < count; i++)
			{
				this.mStarArray[i].color = this.mRedStarColor;
			}
			while (i < this.mStarArray.Length)
			{
				this.mStarArray[i].color = this.mGrayStarColor;
				i++;
			}
		}

		public void SetItemData(ItemData itemData, int itemIndex, int childIndex)
		{
			this.mItemDataIndex = itemIndex;
			this.mChildDataIndex = childIndex;
			this.mNameText.text = itemData.mName;
			this.mDescText.text = itemData.mFileSize.ToString() + "KB";
			this.mDescText2.text = itemData.mDesc;
			this.mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
			this.SetStarCount(itemData.mStarCount);
		}

		public Text mNameText;

		public Image mIcon;

		public Image[] mStarArray;

		public Text mDescText;

		public Text mDescText2;

		public Color32 mRedStarColor = new Color32(249, 227, 101, byte.MaxValue);

		public Color32 mGrayStarColor = new Color32(215, 215, 215, byte.MaxValue);

		public GameObject mContentRootObj;

		private int mItemDataIndex = -1;

		private int mChildDataIndex = -1;
	}
}
