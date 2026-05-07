using System;
using System.IO;

namespace SgLib
{
	public static class FileUtil
	{
		public static byte[] ReadAllBytes(string path)
		{
			return File.ReadAllBytes(path);
		}

		public static void WriteAllBytes(string path, byte[] bytes)
		{
			File.WriteAllBytes(path, bytes);
		}

		public static void WriteAllLines(string path, string[] lines)
		{
			File.WriteAllLines(path, lines);
		}
	}
}
