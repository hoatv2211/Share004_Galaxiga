#if UNITY_ANDROID
using System;
using UnityEngine;

namespace FalconSDK.CrossPromotion.Scripts.Utils
{
	public static class AndroidHelper
	{
		public static bool IsAppInstalled(string bundleId)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getPackageManager", Array.Empty<object>());
			UnityEngine.Debug.Log(" ********LaunchOtherApp ");
			AndroidJavaObject result = null;
			try
			{
				result = androidJavaObject.Call<AndroidJavaObject>("getLaunchIntentForPackage", new object[]
				{
					bundleId
				});
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log("exception" + ex.Message);
			}
			return result != null;
		}
	}
}

#endif // UNITY_ANDROID
