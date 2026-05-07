using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OneSoftCrossPromotion.Scripts.SampleData;
using OneSoftCrossPromotion.Scripts.Structs;
using OneSoftCrossPromotion.Scripts.Utils;
using UnityEngine;

namespace OneSoftCrossPromotion.Scripts.Controller
{
	public static class CacheController
	{
		public static void CreateCacheIfNoExist()
		{
			try
			{
				CacheController.CreateDirectoriesIfNoExist(CacheController.CachePath, false);
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log(ex.ToString());
			}
		}

		public static void WriteFile(string filePath, byte[] bytes)
		{
			try
			{
				CacheController.CreateDirectoriesIfNoExist(CacheController.CachePath + filePath, true);
				File.WriteAllBytes(CacheController.CachePath + filePath, bytes);
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log(ex.ToString());
			}
		}

		public static void WriteJson(string filePath, string json)
		{
			try
			{
				CacheController.CreateDirectoriesIfNoExist(CacheController.CachePath + filePath, true);
				File.WriteAllText(CacheController.CachePath + filePath, json);
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log(ex.ToString());
			}
		}

		public static string ReadJson(string filePath)
		{
			try
			{
				CacheController.CreateDirectoriesIfNoExist(CacheController.CachePath + filePath, true);
				return File.ReadAllText(CacheController.CachePath + filePath);
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log(ex.ToString());
			}
			return null;
		}

		public static List<GamePromoInfo> GetCachePromoInfos()
		{
			string text = CacheController.ReadJson("promo.json");
			return (text != null) ? JsonHelper.FromJson<GamePromoInfo>(text).ToList<GamePromoInfo>() : new List<GamePromoInfo>();
		}

		public static List<GamePromoInfo> CompareToGetNewPromo()
		{
			List<GamePromoInfo> list = new List<GamePromoInfo>();
			List<GamePromoInfo> cachePromoInfos = CacheController.GetCachePromoInfos();
			foreach (GamePromoInfo item in GamePromos.List)
			{
				bool flag = true;
				foreach (GamePromoInfo gamePromoInfo in cachePromoInfos)
				{
					if (gamePromoInfo.name == item.name && gamePromoInfo.version == item.version)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					list.Add(item);
				}
			}
#if UNITY_ANDROID
			return (from p in list
			where !AndroidHelper.IsAppInstalled(p.bundleId)
			select p).ToList<GamePromoInfo>();
#else
			return list;
#endif
		}

		public static string[] GetAllFilesFromCache()
		{
			string[] result;
			try
			{
				result = Directory.GetFiles(CacheController.CachePath, "*.mp4*", SearchOption.AllDirectories);
			}
			catch (Exception)
			{
				result = new string[0];
			}
			return result;
		}

		public static bool IsFileExist(string filePath)
		{
			bool result;
			try
			{
				result = File.Exists(CacheController.CachePath + filePath);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public static void DeleteFile(string filePath)
		{
			try
			{
				File.Delete(filePath);
			}
			catch (Exception arg)
			{
				UnityEngine.Debug.Log("Deleting file failed " + arg);
			}
		}

		public static string GetCurrentDirectory(string filePath)
		{
			return Path.GetFileName(Path.GetDirectoryName(filePath));
		}

		private static void CreateDirectoriesIfNoExist(string path, bool fromFilePath = false)
		{
			if (fromFilePath)
			{
				string directoryName = Path.GetDirectoryName(path);
				if (string.IsNullOrEmpty(directoryName))
				{
					throw new Exception("Wrong Directory Name");
				}
				path = directoryName;
			}
			if (Directory.Exists(path))
			{
				return;
			}
			Directory.CreateDirectory(path);
		}

		private const string CacheDirectoryName = "OneSoftAds";

		public static readonly string CachePath = string.Format("{0}/OneSoftAds/", Application.temporaryCachePath);
	}
}
