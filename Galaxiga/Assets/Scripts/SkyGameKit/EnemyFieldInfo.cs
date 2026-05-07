using System;
using System.Reflection;
using UnityEngine;

namespace SkyGameKit
{
	[Serializable]
	public class EnemyFieldInfo
	{
		public EnemyFieldInfo(Type type)
		{
			this.isUnityObject = type.IsUnityObject();
			if (!this.isUnityObject)
			{
				this.stringValue = ReflectionUtils.GetDefaultValue(type);
			}
		}

		public EnemyFieldInfo(FieldInfo enemyField) : this(enemyField.FieldType)
		{
			this.name = enemyField.Name;
		}

		public EnemyFieldInfo(ParameterInfo enemyParameter)
		{
			this.name = enemyParameter.Name;
			Type parameterType = enemyParameter.ParameterType;
			this.isUnityObject = parameterType.IsUnityObject();
			if (!this.isUnityObject)
			{
				if (enemyParameter.HasDefaultValue)
				{
					this.stringValue = ReflectionUtils.Encode(enemyParameter.DefaultValue);
				}
				else
				{
					this.stringValue = ReflectionUtils.GetDefaultValue(parameterType);
				}
			}
		}

		public void TryImport(EnemyFieldInfo other, Type fieldType)
		{
			if (other == null)
			{
				return;
			}
			if (this.isUnityObject)
			{
				this.objectValue = other.objectValue;
			}
			else if (ReflectionUtils.Decode(fieldType, other.stringValue) != null)
			{
				this.stringValue = other.stringValue;
			}
		}

		public bool Equals(EnemyFieldInfo other)
		{
			if (this.isUnityObject)
			{
				return this.objectValue == other.objectValue;
			}
			return this.stringValue == other.stringValue;
		}

		public string name;

		public bool isUnityObject;

		public string stringValue;

		public UnityEngine.Object objectValue;
	}
}
