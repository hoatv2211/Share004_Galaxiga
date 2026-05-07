using System;
using UnityEngine;

namespace SuperScrollView
{
	public class LoopListViewItem2 : MonoBehaviour
	{
		public object UserObjectData
		{
			get
			{
				return this.mUserObjectData;
			}
			set
			{
				this.mUserObjectData = value;
			}
		}

		public int UserIntData1
		{
			get
			{
				return this.mUserIntData1;
			}
			set
			{
				this.mUserIntData1 = value;
			}
		}

		public int UserIntData2
		{
			get
			{
				return this.mUserIntData2;
			}
			set
			{
				this.mUserIntData2 = value;
			}
		}

		public string UserStringData1
		{
			get
			{
				return this.mUserStringData1;
			}
			set
			{
				this.mUserStringData1 = value;
			}
		}

		public string UserStringData2
		{
			get
			{
				return this.mUserStringData2;
			}
			set
			{
				this.mUserStringData2 = value;
			}
		}

		public float DistanceWithViewPortSnapCenter
		{
			get
			{
				return this.mDistanceWithViewPortSnapCenter;
			}
			set
			{
				this.mDistanceWithViewPortSnapCenter = value;
			}
		}

		public float StartPosOffset
		{
			get
			{
				return this.mStartPosOffset;
			}
			set
			{
				this.mStartPosOffset = value;
			}
		}

		public int ItemCreatedCheckFrameCount
		{
			get
			{
				return this.mItemCreatedCheckFrameCount;
			}
			set
			{
				this.mItemCreatedCheckFrameCount = value;
			}
		}

		public float Padding
		{
			get
			{
				return this.mPadding;
			}
			set
			{
				this.mPadding = value;
			}
		}

		public RectTransform CachedRectTransform
		{
			get
			{
				if (this.mCachedRectTransform == null)
				{
					this.mCachedRectTransform = base.gameObject.GetComponent<RectTransform>();
				}
				return this.mCachedRectTransform;
			}
		}

		public string ItemPrefabName
		{
			get
			{
				return this.mItemPrefabName;
			}
			set
			{
				this.mItemPrefabName = value;
			}
		}

		public int ItemIndex
		{
			get
			{
				return this.mItemIndex;
			}
			set
			{
				this.mItemIndex = value;
			}
		}

		public int ItemId
		{
			get
			{
				return this.mItemId;
			}
			set
			{
				this.mItemId = value;
			}
		}

		public bool IsInitHandlerCalled
		{
			get
			{
				return this.mIsInitHandlerCalled;
			}
			set
			{
				this.mIsInitHandlerCalled = value;
			}
		}

		public LoopListView2 ParentListView
		{
			get
			{
				return this.mParentListView;
			}
			set
			{
				this.mParentListView = value;
			}
		}

		public float TopY
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.TopToBottom)
				{
					return this.CachedRectTransform.anchoredPosition3D.y;
				}
				if (arrangeType == ListItemArrangeType.BottomToTop)
				{
					return this.CachedRectTransform.anchoredPosition3D.y + this.CachedRectTransform.rect.height;
				}
				return 0f;
			}
		}

		public float BottomY
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.TopToBottom)
				{
					return this.CachedRectTransform.anchoredPosition3D.y - this.CachedRectTransform.rect.height;
				}
				if (arrangeType == ListItemArrangeType.BottomToTop)
				{
					return this.CachedRectTransform.anchoredPosition3D.y;
				}
				return 0f;
			}
		}

		public float LeftX
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.LeftToRight)
				{
					return this.CachedRectTransform.anchoredPosition3D.x;
				}
				if (arrangeType == ListItemArrangeType.RightToLeft)
				{
					return this.CachedRectTransform.anchoredPosition3D.x - this.CachedRectTransform.rect.width;
				}
				return 0f;
			}
		}

		public float RightX
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.LeftToRight)
				{
					return this.CachedRectTransform.anchoredPosition3D.x + this.CachedRectTransform.rect.width;
				}
				if (arrangeType == ListItemArrangeType.RightToLeft)
				{
					return this.CachedRectTransform.anchoredPosition3D.x;
				}
				return 0f;
			}
		}

		public float ItemSize
		{
			get
			{
				if (this.ParentListView.IsVertList)
				{
					return this.CachedRectTransform.rect.height;
				}
				return this.CachedRectTransform.rect.width;
			}
		}

		public float ItemSizeWithPadding
		{
			get
			{
				return this.ItemSize + this.mPadding;
			}
		}

		private int mItemIndex = -1;

		private int mItemId = -1;

		private LoopListView2 mParentListView;

		private bool mIsInitHandlerCalled;

		private string mItemPrefabName;

		private RectTransform mCachedRectTransform;

		private float mPadding;

		private float mDistanceWithViewPortSnapCenter;

		private int mItemCreatedCheckFrameCount;

		private float mStartPosOffset;

		private object mUserObjectData;

		private int mUserIntData1;

		private int mUserIntData2;

		private string mUserStringData1;

		private string mUserStringData2;
	}
}
