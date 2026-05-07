using System;

public class ConfigManager
{
	public static ConfigManager Instance
	{
		get
		{
			if (ConfigManager._instance == null)
			{
				ConfigManager._instance = new ConfigManager();
			}
			return ConfigManager._instance;
		}
	}

	public ConfigManager.ConfigData Data
	{
		get
		{
			if (this._configData == null && SaveLoadHandler.Exist(this.KEY))
			{
				this._configData = SaveLoadHandler.Load<ConfigManager.ConfigData>(this.KEY);
			}
			else
			{
				this._configData = new ConfigManager.ConfigData();
				SaveLoadHandler.Save<ConfigManager.ConfigData>(this.KEY, this._configData);
			}
			return this._configData;
		}
		set
		{
			this._configData = value;
			SaveLoadHandler.Save<ConfigManager.ConfigData>(this.KEY, this._configData);
		}
	}

	private static ConfigManager _instance;

	private string KEY = "config_manager_key";

	private ConfigManager.ConfigData _configData;

	public class ConfigData
	{
		public int levelToStartShowInterstitial = 5;

		public int percentToShowInterstitial;
	}
}
