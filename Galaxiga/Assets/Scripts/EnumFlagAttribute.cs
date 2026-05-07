using System;
using UnityEngine;

public class EnumFlagAttribute : PropertyAttribute
{
	public EnumFlagAttribute()
	{
	}

	public EnumFlagAttribute(string name)
	{
		this.enumName = name;
	}

	public string enumName;
}
