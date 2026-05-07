using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method)]
public class EnumActionAttribute : PropertyAttribute
{
	public EnumActionAttribute(Type enumType)
	{
		this.enumType = enumType;
	}

	public Type enumType;
}
