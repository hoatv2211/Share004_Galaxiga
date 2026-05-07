using System;

[Serializable]
public class AccountData
{
	public AccountData()
	{
	}

	public AccountData(ClientData clientData, ServerData serverData)
	{
		this.clientData = clientData;
		this.serverData = serverData;
	}

	public ClientData clientData;

	public ServerData serverData;
}
