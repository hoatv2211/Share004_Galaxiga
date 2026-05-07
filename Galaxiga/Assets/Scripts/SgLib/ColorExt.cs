using System;
using UnityEngine;

namespace SgLib
{
	public static class ColorExt
	{
		public static string ToRGBAHexString(this Color32 c)
		{
			return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
			{
				c.r,
				c.g,
				c.b,
				c.a
			});
		}

		public static string ToRGBAHexString(this Color color)
		{
			Color32 c = color;
			return c.ToRGBAHexString();
		}

		public static string ToARGBHexString(this Color32 c)
		{
			return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
			{
				c.a,
				c.r,
				c.g,
				c.b
			});
		}

		public static string ToARGBHexString(this Color color)
		{
			Color32 c = color;
			return c.ToARGBHexString();
		}

		public static int ToRGBAHex(this Color32 c)
		{
			return (int)c.r << 24 | (int)c.g << 16 | (int)c.b << 8 | (int)c.a;
		}

		public static int ToRGBAHex(this Color color)
		{
			Color32 c = color;
			return c.ToRGBAHex();
		}

		public static int ToARGBHex(this Color32 c)
		{
			return (int)c.a << 24 | (int)c.r << 16 | (int)c.g << 8 | (int)c.b;
		}

		public static int ToARGBHex(this Color color)
		{
			Color32 c = color;
			return c.ToARGBHex();
		}
	}
}
