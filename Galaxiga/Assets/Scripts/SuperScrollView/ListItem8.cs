using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class ListItem8 : MonoBehaviour
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
			this.mExpandBtn.onClick.AddListener(new UnityAction(this.OnExpandBtnClicked));
		}

		public void OnExpandChanged()
		{
			RectTransform component = base.gameObject.GetComponent<RectTransform>();
			if (this.mIsExpand)
			{
				component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 284f);
				this.mExpandContentRoot.SetActive(true);
				this.mClickTip.text = "Shrink";
			}
			else
			{
				component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 143f);
				this.mExpandContentRoot.SetActive(false);
				this.mClickTip.text = "Expand";
			}
		}

		private void OnExpandBtnClicked()
		{
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(this.mItemDataIndex);
			if (itemDataByIndex == null)
			{
				return;
			}
			this.mIsExpand = !this.mIsExpand;
			itemDataByIndex.mIsExpand = this.mIsExpand;
			this.OnExpandChanged();
			LoopListViewItem2 component = base.gameObject.GetComponent<LoopListViewItem2>();
			component.ParentListView.OnItemSizeChanged(component.ItemIndex);
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
			this.mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
			this.SetStarCount(itemData.mStarCount);
			this.mIsExpand = itemData.mIsExpand;
			this.OnExpandChanged();
		}

		public Text mNameText;

		public Image mIcon;

		public Image[] mStarArray;

		public Text mDescText;

		public GameObject mExpandContentRoot;

		public Text mClickTip;

		public Button mExpandBtn;

		public Color32 mRedStarColor = new Color32(249, 227, 101, byte.MaxValue);

		public Color32 mGrayStarColor = new Color32(215, 215, 215, byte.MaxValue);

		private int mItemDataIndex = -1;

		private bool mIsExpand;
	}
}
