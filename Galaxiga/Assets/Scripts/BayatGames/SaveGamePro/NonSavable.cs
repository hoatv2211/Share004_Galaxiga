using System;

namespace BayatGames.SaveGamePro
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class NonSavable : Attribute
	{
	}
}
