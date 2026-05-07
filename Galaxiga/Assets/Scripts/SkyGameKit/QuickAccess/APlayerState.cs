using System;
using UnityEngine;

namespace SkyGameKit.QuickAccess
{
	public abstract class APlayerState : MonoBehaviour
	{
		public virtual void PickUpItem(SgkItem item)
		{
			if (this.onPickUpItem != null)
			{
				this.onPickUpItem(item);
			}
		}

		public virtual int Star
		{
			get
			{
				return this._star;
			}
			set
			{
				int obj = value - this._star;
				this._star = value;
				if (this.onAddStar != null)
				{
					this.onAddStar(obj);
				}
			}
		}

		public abstract void SetShieldActive(float activeTime = 8f);

		public Action<SgkItem> onPickUpItem;

		public float itemActiveTime;

		private int _star;

		public Action<int> onAddStar;
	}
}
