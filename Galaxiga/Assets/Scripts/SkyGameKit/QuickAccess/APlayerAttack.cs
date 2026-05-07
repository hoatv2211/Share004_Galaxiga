using System;
using UnityEngine;

namespace SkyGameKit.QuickAccess
{
	public abstract class APlayerAttack : MonoBehaviour
	{
		public virtual bool LockShot { get; set; }

		public virtual int BulletLevel
		{
			get
			{
				return this._bulletLevel;
			}
			set
			{
				int obj = value - this._bulletLevel;
				this._bulletLevel = value;
				if (this.onBulletLevelChange != null)
				{
					this.onBulletLevelChange(obj);
				}
			}
		}

		public virtual void UseSkill(string skillName = "default")
		{
			if (this.onUseSkill != null)
			{
				this.onUseSkill(skillName);
			}
		}

		public Action<int> onBulletLevelChange;

		private int _bulletLevel;

		public Action<string> onUseSkill;
	}
}
