using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FalconSDK.CrossPromotion.cs_sc.item;
using FalconSDK.CrossPromotion.Scripts.Utils;
using Sirenix.Utilities;
using UnityEngine;

namespace FalconSDK.CrossPromotion.Scripts.Controller
{
	public static class CacheController
	{
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

		public static List<CrossGameItem> GetCachePromoInfos()
		{
			string text = CacheController.ReadJson("promo.json");
			return (text != null) ? JsonHelper.FromJson<CrossGameItem>(text).ToList<CrossGameItem>() : new List<CrossGameItem>();
		}

		public static List<CrossGameItem> CompareToGetNewPromo()
		{
			List<CrossGameItem> list = new List<CrossGameItem>();
			List<CrossGameItem> cachePromoInfos = CacheController.GetCachePromoInfos();
			if (cachePromoInfos.Count == 0)
			{
#if UNITY_ANDROID
				return (from p in FSDK.CrossPromotion.CrossGameItems
				where !AndroidHelper.IsAppInstalled(p.packageName)
				select p).ForEach(delegate(CrossGameItem p)
				{
					p.videoFixedCache = string.Empty;
					p.videoFloatCache = string.Empty;
				}).ToList<CrossGameItem>();
#else
				return FSDK.CrossPromotion.CrossGameItems.ToList<CrossGameItem>();
#endif
			}
			bool flag = false;
			foreach (CrossGameItem crossGameItem in FSDK.CrossPromotion.CrossGameItems)
			{
				bool flag2 = true;
				foreach (CrossGameItem crossGameItem2 in cachePromoInfos)
				{
					if (!(crossGameItem2.name != crossGameItem.name))
					{
						if (crossGameItem2.version == crossGameItem.version)
						{
							flag2 = ((string.IsNullOrEmpty(crossGameItem2.videoFixedCache) && !string.IsNullOrEmpty(crossGameItem.videoFixedUrl)) || (string.IsNullOrEmpty(crossGameItem2.videoFloatCache) && !string.IsNullOrEmpty(crossGameItem.videoFloatUrl)));
							bool flag3 = CacheController.IsFileExisted(crossGameItem2.videoFixedCache);
							bool flag4 = CacheController.IsFileExisted(crossGameItem2.videoFloatCache);
							if (!flag3)
							{
								flag2 = true;
							}
							else
							{
								crossGameItem.videoFixedCache = crossGameItem2.videoFixedCache;
							}
							if (!flag4)
							{
								flag2 = true;
							}
							else
							{
								crossGameItem.videoFloatCache = crossGameItem2.videoFloatCache;
							}
							break;
						}
						if (crossGameItem2.version != crossGameItem.version)
						{
							crossGameItem2.version = crossGameItem.version;
							if (!string.IsNullOrEmpty(crossGameItem2.videoFixedCache))
							{
								CacheController.DeleteFile(crossGameItem2.videoFixedCache);
							}
							if (!string.IsNullOrEmpty(crossGameItem2.videoFloatCache))
							{
								CacheController.DeleteFile(crossGameItem2.videoFloatCache);
							}
							crossGameItem2.videoFixedCache = string.Empty;
							crossGameItem2.videoFloatCache = string.Empty;
							flag = true;
						}
					}
				}
				if (flag2)
				{
					list.Add(crossGameItem);
				}
			}
			if (flag)
			{
				CacheController.WriteJson("promo.json", JsonHelper.ToJson<CrossGameItem>(cachePromoInfos.ToArray(), true));
			}
#if UNITY_ANDROID
			return (from p in list
			where !AndroidHelper.IsAppInstalled(p.packageName)
			select p).ToList<CrossGameItem>();
#else
			return list;
#endif
		}

		private static void DeleteFile(string filePath)
		{
			try
			{
				File.Delete(filePath);
			}
			catch (Exception arg)
			{
				UnityEngine.Debug.LogWarning("Deleting file failed " + arg);
			}
		}

		private static bool IsFileExisted(string filePath)
		{
			try
			{
				return File.Exists(filePath);
			}
			catch (Exception arg)
			{
				UnityEngine.Debug.LogWarning("Check file existed failed " + arg);
			}
			return false;
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
