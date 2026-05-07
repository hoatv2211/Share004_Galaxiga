using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SkyGameKit
{
	public class EnemyEvent<T> : IEnumerable<EnemyEventUnit<T>>, IEnumerable
	{
		public void AddEvent(EnemyFinalEvent eventInfo, BaseEnemy enemy)
		{
			if (eventInfo.param is T)
			{
				EnemyEventUnit<T> enemyEventUnit = new EnemyEventUnit<T>
				{
					param = (T)((object)eventInfo.param)
				};
				if (eventInfo.FinalAction != null)
				{
					enemyEventUnit.action = delegate()
					{
						eventInfo.FinalAction(enemy);
					};
				}
				else
				{
					SgkLog.ReflectionLogError("Tồn tại Event không có action");
				}
				this.events.Add(enemyEventUnit);
			}
			else
			{
				SgkLog.ReflectionLogError(string.Concat(new object[]
				{
					"Parameter ",
					eventInfo.param,
					" không phải kiểu ",
					typeof(T)
				}));
			}
		}

		public void ClearEvent()
		{
			this.events.Clear();
		}

		public void ForEach(Action<EnemyEventUnit<T>> action)
		{
			this.events.ForEach(action);
		}

		public IEnumerator<EnemyEventUnit<T>> GetEnumerator()
		{
			return this.events.Cast<EnemyEventUnit<T>>().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.events.GetEnumerator();
		}

		private List<EnemyEventUnit<T>> events = new List<EnemyEventUnit<T>>();
	}
}
