using System;
using UnityEngine;

[Serializable]
public class MobileDeviceInfo
{
	public static MobileDeviceInfo GetDeviceInfo()
	{
		MobileDeviceInfo mobileDeviceInfo = new MobileDeviceInfo();
		if (Application.platform == RuntimePlatform.Android)
		{
			mobileDeviceInfo.platform = "android";
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			mobileDeviceInfo.platform = "ios";
		}
		mobileDeviceInfo.os = SystemInfo.operatingSystem;
		mobileDeviceInfo.appVersion = Application.version;
		mobileDeviceInfo.deviceName = SystemInfo.deviceName;
		return mobileDeviceInfo;
	}

	public string platform;

	public string os;

	public string deviceName;

	public string appVersion;
}
