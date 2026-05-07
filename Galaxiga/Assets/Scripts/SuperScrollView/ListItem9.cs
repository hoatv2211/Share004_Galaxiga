using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class ListItem9 : MonoBehaviour
	{
		public void Init()
		{
			ClickEventListener clickEventListener = ClickEventListener.Get(this.mStarIcon.gameObject);
			clickEventListener.SetClickEventHandler(new Action<GameObject>(this.OnStarClicked));
			this.mToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
		}

		private void OnToggleValueChanged(bool check)
		{
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(this.mItemDataIndex);
			if (itemDataByIndex == null)
			{
				return;
			}
			itemDataByIndex.mChecked = check;
		}

		private void OnStarClicked(GameObject obj)
		{
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(this.mItemDataIndex);
			if (itemDataByIndex == null)
			{
				return;
			}
			if (itemDataByIndex.mStarCount == 5)
			{
				itemDataByIndex.mStarCount = 0;
			}
			else
			{
				itemDataByIndex.mStarCount++;
			}
			this.SetStarCount(itemDataByIndex.mStarCount);
		}

		public void SetStarCount(int count)
		{
			this.mStarCount.text = count.ToString();
			if (count == 0)
			{
				this.mStarIcon.color = this.mGrayStarColor;
			}
			else
			{
				this.mStarIcon.color = this.mRedStarColor;
			}
		}

		public void SetItemData(ItemData itemData, int itemIndex)
		{
			this.mItemDataIndex = itemIndex;
			this.mNameText.text = itemData.mName;
			this.mDescText.text = itemData.mFileSize.ToString() + "KB";
			this.mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
			this.SetStarCount(itemData.mStarCount);
			this.mToggle.isOn = itemData.mChecked;
		}

		public Text mNameText;

		public Image mIcon;

		public Image mStarIcon;

		public Text mStarCount;

		public Text mDescText;

		public Color32 mRedStarColor = new Color32(236, 217, 103, byte.MaxValue);

		public Color32 mGrayStarColor = new Color32(215, 215, 215, byte.MaxValue);

		public Toggle mToggle;

		private int mItemDataIndex = -1;
	}
}
