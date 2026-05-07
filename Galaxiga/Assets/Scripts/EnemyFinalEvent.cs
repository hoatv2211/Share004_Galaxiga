using System;
using System.Reflection;

namespace SkyGameKit
{
	public struct EnemyFinalEvent
	{
		public FinalActionDelegate FinalAction { get; private set; }

		public void AddFinalActions(MethodInfo method, object[] paramList)
		{
			this.FinalAction = (FinalActionDelegate)Delegate.Combine(this.FinalAction, new FinalActionDelegate(delegate(BaseEnemy x)
			{
				method.Invoke(x, paramList);
			}));
		}

		public object param;
	}
}
