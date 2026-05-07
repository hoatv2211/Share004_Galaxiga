using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
public class UbhConditionalHideAttribute : PropertyAttribute
{
	public UbhConditionalHideAttribute(string conditionalSourceField)
	{
		this.m_conditionalSourceField = conditionalSourceField;
		this.m_hideInInspector = false;
	}

	public UbhConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
	{
		this.m_conditionalSourceField = conditionalSourceField;
		this.m_hideInInspector = hideInInspector;
	}

	public string m_conditionalSourceField = string.Empty;

	public bool m_hideInInspector;
}
