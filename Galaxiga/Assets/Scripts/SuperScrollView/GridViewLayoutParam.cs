using System;
using UnityEngine;

namespace SuperScrollView
{
	public class GridViewLayoutParam
	{
		public bool CheckParam()
		{
			if (this.mColumnOrRowCount <= 0)
			{
				UnityEngine.Debug.LogError("mColumnOrRowCount shoud be > 0");
				return false;
			}
			if (this.mItemWidthOrHeight <= 0f)
			{
				UnityEngine.Debug.LogError("mItemWidthOrHeight shoud be > 0");
				return false;
			}
			if (this.mCustomColumnOrRowOffsetArray != null && this.mCustomColumnOrRowOffsetArray.Length != this.mColumnOrRowCount)
			{
				UnityEngine.Debug.LogError("mGroupOffsetArray.Length != mColumnOrRowCount");
				return false;
			}
			return true;
		}

		public int mColumnOrRowCount;

		public float mItemWidthOrHeight;

		public float mPadding1;

		public float mPadding2;

		public float[] mCustomColumnOrRowOffsetArray;
	}
}
