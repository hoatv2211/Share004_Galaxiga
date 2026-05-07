using System;

namespace OSNet
{
	public class SCClientConfig : SCMessage
	{
		public override string GetEvent()
		{
			return "sc_client_config";
		}

		public override void OnData()
		{
			ConfigManager.Instance.Data = this.configData;
		}

		public ConfigManager.ConfigData configData;
	}
}
