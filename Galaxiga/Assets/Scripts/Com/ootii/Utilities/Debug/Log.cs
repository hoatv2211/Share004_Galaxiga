using System;
using System.Collections;
using UnityEngine;

namespace com.ootii.Utilities.Debug
{
	public class Log : MonoBehaviour
	{
		public IEnumerator Start()
		{
			Log.FilePath = this._FilePath;
			Log.FontSize = this._ScreenFontSize;
			Log.ForeColor = this._ScreenForeColor;
			Log.LineHeight = this._ScreenFontSize + 6;
			Log.PrefixTime = this._PrefixTime;
			Log.IsFileEnabled = this._IsFileEnabled;
			Log.IsScreenEnabled = this._IsScreenEnabled;
			Log.IsConsoleEnabled = this._IsConsoleEnabled;
			Log.FileFlushPerWrite = this._FileFlushPerWrite;
			WaitForEndOfFrame lWaitForEndOfFrame = new WaitForEndOfFrame();
			for (;;)
			{
				yield return lWaitForEndOfFrame;
				Log.Clear();
			}
			yield break;
		}

		public void OnDestroy()
		{
			Log.Close();
		}

		public void OnGUI()
		{
			Log.Render();
		}

		public static string FilePath
		{
			get
			{
				return Log.mFilePath;
			}
			set
			{
				Log.mFilePath = value;
			}
		}

		public static bool PrefixTime
		{
			get
			{
				return Log.mPrefixTime;
			}
			set
			{
				Log.mPrefixTime = value;
			}
		}

		public static int LineHeight
		{
			get
			{
				return Log.mLineHeight;
			}
			set
			{
				Log.mLineHeight = value;
			}
		}

		public static bool IsEnabled
		{
			get
			{
				return Log.mIsEnabled;
			}
			set
			{
				Log.mIsEnabled = value;
			}
		}

		public static bool IsFileEnabled
		{
			get
			{
				return Log.mIsFileEnabled;
			}
			set
			{
				Log.mIsFileEnabled = value;
			}
		}

		public static bool IsScreenEnabled
		{
			get
			{
				return Log.mIsScreenEnabled;
			}
			set
			{
				Log.mIsScreenEnabled = value;
			}
		}

		public static bool IsConsoleEnabled
		{
			get
			{
				return Log.mIsConsoleEnabled;
			}
			set
			{
				Log.mIsConsoleEnabled = value;
			}
		}

		public static bool FileFlushPerWrite
		{
			get
			{
				return Log.mFileFlushPerWrite;
			}
			set
			{
				Log.mFileFlushPerWrite = value;
			}
		}

		public static int FontSize
		{
			get
			{
				return Log.mFontSize;
			}
			set
			{
				Log.mFontSize = value;
			}
		}

		public static Color ForeColor
		{
			get
			{
				return Log.mForeColor;
			}
			set
			{
				Log.mForeColor = value;
			}
		}

		public static void Write(string rText)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			if (Log.mIsFileEnabled)
			{
				Log.FileWrite(rText);
			}
			if (Log.mIsScreenEnabled)
			{
				Log.ScreenWrite(rText);
			}
			if (Log.mIsConsoleEnabled)
			{
				Log.ConsoleWrite(rText);
			}
		}

		public static void FileScreenWrite(string rText, int rLine)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			if (Log.mIsScreenEnabled)
			{
				Log.ScreenWrite(rText, rLine);
			}
		}

		public static void FileWrite(string rText)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			if (Log.mPrefixTime)
			{
				rText = string.Format("[{0:f4}] {1}", Time.time, rText);
			}
		}

		public static void ConsoleScreenWrite(string rText)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			Log.ConsoleWrite(rText);
			Log.ScreenWrite(rText);
		}

		public static void ConsoleScreenWrite(string rText, int rLine)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			Log.ConsoleWrite(rText);
			Log.ScreenWrite(rText, rLine);
		}

		public static void ConsoleWrite(string rText)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			if (Log.mPrefixTime)
			{
				rText = string.Format("[{0:f4}] {1}", Time.time, rText);
			}
			UnityEngine.Debug.Log(rText);
		}

		public static void ConsoleWrite(string rText, bool rPrefixTime)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			if (Log.mPrefixTime && rPrefixTime)
			{
				rText = string.Format("[{0:f4}] {1}", Time.time, rText);
			}
			UnityEngine.Debug.Log(rText);
		}

		public static void ConsoleWriteWarning(string rText)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			if (Log.mPrefixTime)
			{
				rText = string.Format("[{0:f4}] {1}", Time.time, rText);
			}
			UnityEngine.Debug.LogWarning(rText);
		}

		public static void ConsoleWriteError(string rText)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			if (Log.mPrefixTime)
			{
				rText = string.Format("[{0:f4}] {1}", Time.time, rText);
			}
			UnityEngine.Debug.LogError(rText);
		}

		public static void ScreenWrite(string rText)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			Log.ScreenWrite(rText, 10, Log.mLineIndex * Log.mLineHeight);
		}

		public static void ScreenWrite(int rLine, params string[] rText)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			string rText2 = string.Join(" ", rText);
			Log.ScreenWrite(rText2, 10, rLine * Log.mLineHeight);
		}

		public static void ScreenWrite(string rText, int rLine)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			Log.ScreenWrite(rText, 10, rLine * Log.mLineHeight);
		}

		public static void ScreenWrite(string rText, int rX, int rY)
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			if (Log.mPrefixTime)
			{
				rText = string.Format("[{0:f4}] {1}", Time.time, rText);
			}
			LogText logText = LogText.Allocate(rText, rX, rY);
			Log.mLines[Log.mLineIndex % 50] = logText;
			Log.mLineIndex++;
			if (Log.mLineIndex >= 50)
			{
				Log.mLineIndex = 49;
			}
		}

		public static void Render()
		{
			if (!Log.mIsEnabled)
			{
				return;
			}
			GUIStyle guistyle = new GUIStyle();
			guistyle.alignment = TextAnchor.UpperLeft;
			guistyle.normal.textColor = Color.white;
			guistyle.fontSize = Log.mFontSize;
			GUI.contentColor = Log.mForeColor;
			GUI.backgroundColor = Color.green;
			for (int i = 0; i < Log.mLineIndex; i++)
			{
				LogText logText = Log.mLines[i];
				Log.mLineRect.x = (float)logText.X;
				Log.mLineRect.y = (float)logText.Y;
				Log.mLineRect.width = 900f;
				Log.mLineRect.height = (float)Log.mLineHeight;
				GUI.Label(Log.mLineRect, logText.Text, guistyle);
			}
		}

		public static void Clear()
		{
			for (int i = 0; i < Log.mLineIndex; i++)
			{
				LogText.Release(Log.mLines[i]);
			}
			Log.mLineIndex = 0;
		}

		public static void Close()
		{
		}

		private const int LOG_TEXT_COUNT = 50;

		public bool _PrefixTime = true;

		public bool _IsConsoleEnabled = true;

		public bool _IsScreenEnabled = true;

		public int _ScreenFontSize = 12;

		public Color _ScreenForeColor = Color.black;

		public bool _IsFileEnabled;

		public string _FilePath = ".\\Log.txt";

		public bool _FileFlushPerWrite;

		private static string mFilePath = ".\\Log.txt";

		private static bool mPrefixTime = true;

		private static int mLineHeight = 18;

		private static bool mIsEnabled = true;

		private static bool mIsFileEnabled = false;

		private static bool mIsScreenEnabled = true;

		private static bool mIsConsoleEnabled = true;

		private static bool mFileFlushPerWrite = true;

		private static int mFontSize = 12;

		private static Color mForeColor = Color.black;

		private static LogText[] mLines = new LogText[50];

		private static int mLineIndex = 0;

		private static Rect mLineRect = default(Rect);
	}
}
