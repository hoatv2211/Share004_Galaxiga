using System;

namespace SuperScrollView
{
	public class ItemSizeGroup
	{
		public ItemSizeGroup(int index, float itemDefaultSize)
		{
			this.mGroupIndex = index;
			this.mItemDefaultSize = itemDefaultSize;
			this.Init();
		}

		public void Init()
		{
			this.mItemSizeArray = new float[100];
			if (this.mItemDefaultSize != 0f)
			{
				for (int i = 0; i < this.mItemSizeArray.Length; i++)
				{
					this.mItemSizeArray[i] = this.mItemDefaultSize;
				}
			}
			this.mItemStartPosArray = new float[100];
			this.mItemStartPosArray[0] = 0f;
			this.mItemCount = 100;
			this.mGroupSize = this.mItemDefaultSize * (float)this.mItemSizeArray.Length;
			if (this.mItemDefaultSize != 0f)
			{
				this.mDirtyBeginIndex = 0;
			}
			else
			{
				this.mDirtyBeginIndex = 100;
			}
		}

		public float GetItemStartPos(int index)
		{
			return this.mGroupStartPos + this.mItemStartPosArray[index];
		}

		public bool IsDirty
		{
			get
			{
				return this.mDirtyBeginIndex < this.mItemCount;
			}
		}

		public float SetItemSize(int index, float size)
		{
			if (index > this.mMaxNoZeroIndex && size > 0f)
			{
				this.mMaxNoZeroIndex = index;
			}
			float num = this.mItemSizeArray[index];
			if (num == size)
			{
				return 0f;
			}
			this.mItemSizeArray[index] = size;
			if (index < this.mDirtyBeginIndex)
			{
				this.mDirtyBeginIndex = index;
			}
			float num2 = size - num;
			this.mGroupSize += num2;
			return num2;
		}

		public void SetItemCount(int count)
		{
			if (count < this.mMaxNoZeroIndex)
			{
				this.mMaxNoZeroIndex = count;
			}
			if (this.mItemCount == count)
			{
				return;
			}
			this.mItemCount = count;
			this.RecalcGroupSize();
		}

		public void RecalcGroupSize()
		{
			this.mGroupSize = 0f;
			for (int i = 0; i < this.mItemCount; i++)
			{
				this.mGroupSize += this.mItemSizeArray[i];
			}
		}

		public int GetItemIndexByPos(float pos)
		{
			if (this.mItemCount == 0)
			{
				return -1;
			}
			int i = 0;
			int num = this.mItemCount - 1;
			if (this.mItemDefaultSize == 0f)
			{
				if (this.mMaxNoZeroIndex < 0)
				{
					this.mMaxNoZeroIndex = 0;
				}
				num = this.mMaxNoZeroIndex;
			}
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				float num3 = this.mItemStartPosArray[num2];
				float num4 = num3 + this.mItemSizeArray[num2];
				if (num3 <= pos && num4 >= pos)
				{
					return num2;
				}
				if (pos > num4)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return -1;
		}

		public void UpdateAllItemStartPos()
		{
			if (this.mDirtyBeginIndex >= this.mItemCount)
			{
				return;
			}
			int num = (this.mDirtyBeginIndex >= 1) ? this.mDirtyBeginIndex : 1;
			for (int i = num; i < this.mItemCount; i++)
			{
				this.mItemStartPosArray[i] = this.mItemStartPosArray[i - 1] + this.mItemSizeArray[i - 1];
			}
			this.mDirtyBeginIndex = this.mItemCount;
		}

		public void ClearOldData()
		{
			for (int i = this.mItemCount; i < 100; i++)
			{
				this.mItemSizeArray[i] = 0f;
			}
		}

		public float[] mItemSizeArray;

		public float[] mItemStartPosArray;

		public int mItemCount;

		private int mDirtyBeginIndex = 100;

		public float mGroupSize;

		public float mGroupStartPos;

		public float mGroupEndPos;

		public int mGroupIndex;

		private float mItemDefaultSize;

		private int mMaxNoZeroIndex;
	}
}
