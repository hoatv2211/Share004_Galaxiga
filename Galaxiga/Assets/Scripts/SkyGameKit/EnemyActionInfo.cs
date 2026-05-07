using System;
using System.Collections.Generic;
using System.Reflection;

namespace SkyGameKit
{
	[Serializable]
	public class EnemyActionInfo
	{
		public EnemyActionInfo()
		{
		}

		public EnemyActionInfo(MethodInfo enemyMethod)
		{
			this.name = enemyMethod.Name;
			this.parameters = this.RefreshParameters(enemyMethod);
		}

		public List<EnemyFieldInfo> RefreshParameters(MethodInfo method)
		{
			List<EnemyFieldInfo> list = this.parameters;
			List<EnemyFieldInfo> list2 = new List<EnemyFieldInfo>();
			foreach (ParameterInfo parameterInfo in method.GetParameters())
			{
				EnemyFieldInfo enemyFieldInfo = new EnemyFieldInfo(parameterInfo);
				foreach (EnemyFieldInfo enemyFieldInfo2 in list)
				{
					if (enemyFieldInfo2.name == parameterInfo.Name)
					{
						enemyFieldInfo.TryImport(enemyFieldInfo2, parameterInfo.ParameterType);
						break;
					}
				}
				list2.Add(enemyFieldInfo);
			}
			return list2;
		}

		public bool Equals(EnemyActionInfo other)
		{
			if (this.name != other.name)
			{
				return false;
			}
			if (this.parameters.Count != other.parameters.Count)
			{
				return false;
			}
			for (int i = 0; i < this.parameters.Count; i++)
			{
				if (this.parameters[i].isUnityObject != other.parameters[i].isUnityObject)
				{
					return false;
				}
				if (this.parameters[i].name != other.parameters[i].name)
				{
					return false;
				}
			}
			return true;
		}

		public string name;

		public List<EnemyFieldInfo> parameters = new List<EnemyFieldInfo>();
	}
}
