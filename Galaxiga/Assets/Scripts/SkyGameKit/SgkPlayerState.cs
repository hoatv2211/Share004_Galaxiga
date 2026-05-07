using System;
using SkyGameKit.QuickAccess;
using UnityEngine;

namespace SkyGameKit
{
	public class SgkPlayerState : APlayerState
	{
		protected virtual void Start()
		{
			this.playerH = base.GetComponentInChildren<APlayerHealth>();
			this.playerA = base.GetComponent<APlayerAttack>();
			if (this.itemActiveTime <= 0f)
			{
				this.itemActiveTime = 8f;
			}
		}

		public override void PickUpItem(SgkItem item)
		{
			base.PickUpItem(item);
			if (item.name.Contains("Item"))
			{
				if (item.name.Contains("Shield"))
				{
					this.SetShieldActive(this.itemActiveTime);
				}
				else if (item.name.Contains("Live"))
				{
					this.playerH.CurrentHP++;
				}
				else if (item.name.Contains("PowerUp"))
				{
					this.playerA.BulletLevel++;
				}
				else if (item.name.Contains("Star"))
				{
					this.AddStar(item.value);
				}
			}
		}

		public virtual void AddStar(int number = 1)
		{
			this.Star += number;
		}

		public override void SetShieldActive(float activeTime = 8f)
		{
			this.shieldObj.SetActive(true);
			this.playerH.playerCanTakeDamage = false;
			this.Delay(activeTime, delegate
			{
				this.shieldObj.SetActive(false);
				this.playerH.playerCanTakeDamage = true;
			}, false);
		}

		public GameObject shieldObj;

		private APlayerHealth playerH;

		private APlayerAttack playerA;
	}
}
