using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BayatGames.SaveGamePro.Reflection
{
	public static class FieldInfoUtils
	{
		public static bool IsSavable(this FieldInfo field)
		{
			return !field.IsDefined(typeof(NonSavable), false) && (field.IsDefined(typeof(Savable), false) || (!field.IsDefined(typeof(ObsoleteAttribute), false) && !field.IsInitOnly && !field.IsLiteral && !field.IsDefined(typeof(NonSerializedAttribute), false) && !field.IsBackingField()));
		}

		public static bool IsBackingField(this FieldInfo field)
		{
			return field.IsDefined(typeof(CompilerGeneratedAttribute), false);
		}
	}
}
