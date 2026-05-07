using System;

[Serializable]
public class ClientData
{
	public string deviceId;

	public string fbId;

	public string name;

	public int sequence;

	public string language;

	public string appVersion;

	public string gameId;

	public MobileDeviceInfo mobileDeviceInfo;

	public ExtClientData extClientData;
}
