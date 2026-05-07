using System;
using UnityEngine;

namespace I2.Loc
{
	public abstract class LocalizeTarget<T> : ILocalizeTarget where T : UnityEngine.Object
	{
		public override bool IsValid(Localize cmp)
		{
			if (this.mTarget != null)
			{
				Component component = this.mTarget as Component;
				if (component != null && component.gameObject != cmp.gameObject)
				{
					this.mTarget = (T)((object)null);
				}
			}
			if (this.mTarget == null)
			{
				this.mTarget = cmp.GetComponent<T>();
			}
			return this.mTarget != null;
		}

		public T mTarget;
	}
}
