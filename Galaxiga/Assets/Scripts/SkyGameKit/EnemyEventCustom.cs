using System;

namespace SkyGameKit
{
	[AttributeUsage(AttributeTargets.Field)]
	public class EnemyEventCustom : Attribute
	{
		public string displayName;

		public string paramName = "Parameter";
	}
}
