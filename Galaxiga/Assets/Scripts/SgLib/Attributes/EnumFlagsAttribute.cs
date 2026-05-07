using System;
using UnityEngine;

namespace SgLib.Attributes
{
	public class EnumFlagsAttribute : PropertyAttribute
	{
		public EnumFlagsAttribute()
		{
		}

		public EnumFlagsAttribute(string name)
		{
			this.enumName = name;
		}

		public string enumName;
	}
}
