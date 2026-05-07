using System;
using com.ootii.Collections;

namespace com.ootii.Utilities.Debug
{
	public class LogText
	{
		public static int Length
		{
			get
			{
				return LogText.sPool.Length;
			}
		}

		public static LogText Allocate()
		{
			LogText logText = LogText.sPool.Allocate();
			logText.Text = string.Empty;
			logText.X = 0;
			logText.Y = 0;
			return logText;
		}

		public static LogText Allocate(string rText, int rX, int rY)
		{
			LogText logText = LogText.sPool.Allocate();
			logText.Text = rText;
			logText.X = rX;
			logText.Y = rY;
			return logText;
		}

		public static void Release(LogText rInstance)
		{
			if (rInstance == null)
			{
				return;
			}
			LogText.sPool.Release(rInstance);
		}

		public string Text;

		public int X;

		public int Y;

		private static ObjectPool<LogText> sPool = new ObjectPool<LogText>(20, 5);
	}
}
