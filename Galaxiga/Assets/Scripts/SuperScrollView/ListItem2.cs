using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class ListItem2 : MonoBehaviour
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
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(this.mItemDataIndex);
			if (itemDataByIndex == null)
			{
				return;
			}
			if (index == 0 && itemDataByIndex.mStarCount == 1)
			{
				itemDataByIndex.mStarCount = 0;
			}
			else
			{
				itemDataByIndex.mStarCount = index + 1;
			}
			this.SetStarCount(itemDataByIndex.mStarCount);
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

		public void SetItemData(ItemData itemData, int itemIndex)
		{
			this.mItemDataIndex = itemIndex;
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

		public LoopListView2 mLoopListView;
	}
}
