using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class ListItem3 : MonoBehaviour
	{
		public void Init()
		{
			this.mToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
		}

		private void OnToggleValueChanged(bool check)
		{
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(this.mItemIndex);
			if (itemDataByIndex == null)
			{
				return;
			}
			itemDataByIndex.mChecked = check;
		}

		public void SetItemData(ItemData itemData, int itemIndex)
		{
			this.mItemIndex = itemIndex;
			this.mNameText.text = itemData.mName;
			this.mDescText.text = itemData.mDesc;
			this.mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
			this.mToggle.isOn = itemData.mChecked;
		}

		public Text mNameText;

		public Image mIcon;

		public Text mDescText;

		private int mItemIndex = -1;

		public Toggle mToggle;
	}
}
