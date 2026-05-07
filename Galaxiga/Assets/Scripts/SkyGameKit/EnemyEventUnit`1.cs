using System;

namespace SkyGameKit
{
	public class EnemyEventUnit<T>
	{
		public void Invoke()
		{
			if (this.action != null)
			{
				this.action();
			}
		}

		public T param;

		public EnemyEvent action;
	}
}
