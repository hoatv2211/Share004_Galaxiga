using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class ListItem4 : MonoBehaviour
	{
		public int ItemIndex
		{
			get
			{
				return this.mItemIndex;
			}
		}

		public void Init()
		{
		}

		public void SetItemData(ChatMsg itemData, int itemIndex)
		{
			this.mIndexText.text = itemIndex.ToString();
			PersonInfo personInfo = ChatMsgDataSourceMgr.Get.GetPersonInfo(itemData.mPersonId);
			this.mItemIndex = itemIndex;
			if (itemData.mMsgType == MsgTypeEnum.Str)
			{
				this.mMsgPic.gameObject.SetActive(false);
				this.mMsgText.gameObject.SetActive(true);
				this.mMsgText.text = itemData.mSrtMsg;
				this.mMsgText.GetComponent<ContentSizeFitter>().SetLayoutVertical();
				this.mIcon.sprite = ResManager.Get.GetSpriteByName(personInfo.mHeadIcon);
				Vector2 sizeDelta = this.mItemBg.GetComponent<RectTransform>().sizeDelta;
				sizeDelta.x = this.mMsgText.GetComponent<RectTransform>().sizeDelta.x + 20f;
				sizeDelta.y = this.mMsgText.GetComponent<RectTransform>().sizeDelta.y + 20f;
				this.mItemBg.GetComponent<RectTransform>().sizeDelta = sizeDelta;
				if (personInfo.mId == 0)
				{
					this.mItemBg.color = new Color32(160, 231, 90, byte.MaxValue);
					this.mArrow.color = this.mItemBg.color;
				}
				else
				{
					this.mItemBg.color = Color.white;
					this.mArrow.color = this.mItemBg.color;
				}
				RectTransform component = base.gameObject.GetComponent<RectTransform>();
				float num = sizeDelta.y;
				if (num < 75f)
				{
					num = 75f;
				}
				component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num);
			}
			else
			{
				this.mMsgPic.gameObject.SetActive(true);
				this.mMsgText.gameObject.SetActive(false);
				this.mMsgPic.sprite = ResManager.Get.GetSpriteByName(itemData.mPicMsgSpriteName);
				this.mMsgPic.SetNativeSize();
				this.mIcon.sprite = ResManager.Get.GetSpriteByName(personInfo.mHeadIcon);
				Vector2 sizeDelta2 = this.mItemBg.GetComponent<RectTransform>().sizeDelta;
				sizeDelta2.x = this.mMsgPic.GetComponent<RectTransform>().sizeDelta.x + 20f;
				sizeDelta2.y = this.mMsgPic.GetComponent<RectTransform>().sizeDelta.y + 20f;
				this.mItemBg.GetComponent<RectTransform>().sizeDelta = sizeDelta2;
				if (personInfo.mId == 0)
				{
					this.mItemBg.color = new Color32(160, 231, 90, byte.MaxValue);
					this.mArrow.color = this.mItemBg.color;
				}
				else
				{
					this.mItemBg.color = Color.white;
					this.mArrow.color = this.mItemBg.color;
				}
				RectTransform component2 = base.gameObject.GetComponent<RectTransform>();
				float num2 = sizeDelta2.y;
				if (num2 < 75f)
				{
					num2 = 75f;
				}
				component2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num2);
			}
		}

		public Text mMsgText;

		public Image mMsgPic;

		public Image mIcon;

		public Image mItemBg;

		public Image mArrow;

		public Text mIndexText;

		private int mItemIndex = -1;
	}
}
