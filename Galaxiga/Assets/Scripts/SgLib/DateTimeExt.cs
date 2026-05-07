using System;

namespace SgLib
{
	public static class DateTimeExt
	{
		public static TimeSpan SameTimeZoneSubtract(this DateTime minuend, DateTime subtrahend)
		{
			return minuend.ToLocalTime().Subtract(subtrahend.ToLocalTime());
		}
	}
}
