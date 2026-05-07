using System;
using UnityEngine;

namespace SkyGameKit.QuickAccess
{
	public abstract class APlayerHealth : MonoBehaviour
	{
		public virtual int CurrentHP
		{
			get
			{
				return this._currentHP;
			}
			set
			{
				int obj = value - this._currentHP;
				this._currentHP = value;
				if (this.onCurrentHPChange != null)
				{
					this.onCurrentHPChange(obj);
				}
			}
		}

		public virtual void Respawn()
		{
		}

		public int maxHP;

		private int _currentHP;

		public Action<int> onCurrentHPChange;

		public bool playerCanTakeDamage;

		public int armor;
	}
}
