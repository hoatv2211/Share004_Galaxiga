using System;
using System.Collections.Generic;
using System.Reflection;

namespace SkyGameKit
{
	[Serializable]
	public class EnemyEventInfo
	{
		public EnemyEventInfo(FieldInfo fieldInfo)
		{
			this.name = fieldInfo.Name;
			this.hasParameter = (ReflectionUtils.GetEventParamType(fieldInfo) != null);
			if (this.hasParameter)
			{
				this.parameter = new EnemyFieldInfo(ReflectionUtils.GetEventParamType(fieldInfo));
			}
		}

		public List<EnemyActionInfo> RefreshMethods(List<MethodInfo> enemyMethods)
		{
			List<EnemyActionInfo> list = this.listAction;
			List<EnemyActionInfo> list2 = new List<EnemyActionInfo>();
			foreach (EnemyActionInfo enemyActionInfo in list)
			{
				foreach (MethodInfo methodInfo in enemyMethods)
				{
					if (enemyActionInfo.name == methodInfo.Name)
					{
						EnemyActionInfo enemyActionInfo2 = new EnemyActionInfo
						{
							name = methodInfo.Name
						};
						enemyActionInfo2.parameters = enemyActionInfo.RefreshParameters(methodInfo);
						list2.Add(enemyActionInfo2);
					}
				}
			}
			return list2;
		}

		public bool Equals(EnemyEventInfo other)
		{
			if (this.name != other.name)
			{
				return false;
			}
			if (this.hasParameter != other.hasParameter)
			{
				return false;
			}
			if (this.listAction.Count != other.listAction.Count)
			{
				return false;
			}
			for (int i = 0; i < this.listAction.Count; i++)
			{
				if (!this.listAction[i].Equals(other.listAction[i]))
				{
					return false;
				}
			}
			return true;
		}

		public string name;

		public bool hasParameter;

		public EnemyFieldInfo parameter;

		public List<EnemyActionInfo> listAction = new List<EnemyActionInfo>();
	}
}
