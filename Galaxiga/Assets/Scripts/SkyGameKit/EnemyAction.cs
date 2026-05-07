using System;

namespace SkyGameKit
{
	[AttributeUsage(AttributeTargets.Method)]
	public class EnemyAction : Attribute
	{
		public string displayName;
	}
}
