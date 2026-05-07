using System;
using System.Reflection;

namespace BayatGames.SaveGamePro.Reflection
{
	public static class PropertyInfoUtils
	{
		public static bool IsSavable(this PropertyInfo property)
		{
			return !property.IsDefined(typeof(NonSavable), false) && (property.IsDefined(typeof(Savable), false) || (!property.IsDefined(typeof(ObsoleteAttribute), false) && property.CanRead && property.CanWrite && property.GetIndexParameters().Length == 0));
		}
	}
}
