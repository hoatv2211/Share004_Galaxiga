using System;
using System.Collections.Generic;

namespace OSNet
{
	public static class Extension
	{
		public static bool IsGenericList(this Type oType)
		{
			return oType.IsGenericType && oType.GetGenericTypeDefinition() == typeof(List<>);
		}
	}
}
