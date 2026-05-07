using System;
using System.Collections.Generic;
using I2.Loc;
using OSNet;
using UnityEngine;

public class AccountManager
{
	public static AccountManager Instance
	{
		get
		{
			if (AccountManager._instance == null)
			{
				AccountManager._instance = new AccountManager();
				AccountManager._instance.LoadData();
			}
			return AccountManager._instance;
		}
	}

	public string DeviceId
	{
		get
		{
			if (this.deviceId == null && SaveLoadHandler.Exist("key_device_id"))
			{
				this.deviceId = SaveLoadHandler.Load<string>("key_device_id");
			}
			return this.deviceId;
		}
		set
		{
			this.deviceId = value;
			SaveLoadHandler.Save<string>("key_device_id", value);
		}
	}

	public string FbId
	{
		get
		{
			if (this.fbId == null && SaveLoadHandler.Exist("key_fb_id"))
			{
				this.fbId = SaveLoadHandler.Load<string>("key_fb_id");
			}
			return this.fbId;
		}
		set
		{
			this.fbId = value;
			SaveLoadHandler.Save<string>("key_fb_id", value);
		}
	}

	public string Name
	{
		get
		{
			if (this.name == null && SaveLoadHandler.Exist("key_name"))
			{
				this.name = SaveLoadHandler.Load<string>("key_name");
			}
			return this.name;
		}
		set
		{
			this.name = value;
			SaveLoadHandler.Save<string>("key_name", value);
		}
	}

	public string FbImageUrl
	{
		get
		{
			if (this.fbImageUrl == null && SaveLoadHandler.Exist("key_fb_image_url"))
			{
				this.fbImageUrl = SaveLoadHandler.Load<string>("key_fb_image_url");
			}
			return this.fbImageUrl;
		}
		set
		{
			this.fbImageUrl = value;
			SaveLoadHandler.Save<string>("key_fb_image_url", value);
		}
	}

	public int Sequence
	{
		get
		{
			if (this.sequence == 1 && SaveLoadHandler.Exist("key_sequence"))
			{
				this.sequence = SaveLoadHandler.Load<int>("key_sequence");
			}
			return this.sequence;
		}
		set
		{
			this.sequence = value;
			SaveLoadHandler.Save<int>("key_sequence", value);
		}
	}

	public string ConversionData
	{
		get
		{
			if (this.conversionData == null && SaveLoadHandler.Exist("key_appsflyer_convertion_data"))
			{
				this.conversionData = SaveLoadHandler.Load<string>("key_appsflyer_convertion_data");
			}
			return this.conversionData;
		}
		set
		{
			this.conversionData = value;
			SaveLoadHandler.Save<string>("key_appsflyer_convertion_data", value);
		}
	}

	public string AppsflyerId
	{
		get
		{
			if (this.appsflyerId == null && SaveLoadHandler.Exist("key_appsflyer_id"))
			{
				this.appsflyerId = SaveLoadHandler.Load<string>("key_appsflyer_id");
			}
			return this.appsflyerId;
		}
		set
		{
			this.appsflyerId = value;
			SaveLoadHandler.Save<string>("key_appsflyer_id", value);
		}
	}

	public int Code
	{
		get
		{
			if (this.code == -1 && SaveLoadHandler.Exist("key_code"))
			{
				this.code = SaveLoadHandler.Load<int>("key_code");
			}
			return this.code;
		}
		set
		{
			this.code = value;
			SaveLoadHandler.Save<int>("key_code", value);
		}
	}

	public string Token
	{
		get
		{
			if (this.token == null && SaveLoadHandler.Exist("key_token"))
			{
				this.token = SaveLoadHandler.Load<string>("key_token");
			}
			return this.token;
		}
		set
		{
			this.token = value;
			SaveLoadHandler.Save<string>("key_token", value);
		}
	}

	public Dictionary<int, string> DictWhatChanged
	{
		get
		{
			if (this.dictWhatChanged.Count == 0 && SaveLoadHandler.Exist("key_what_changed"))
			{
				this.dictWhatChanged = SaveLoadHandler.Load<Dictionary<int, string>>("key_what_changed");
			}
			return this.dictWhatChanged;
		}
		set
		{
			this.dictWhatChanged = value;
			SaveLoadHandler.Save<Dictionary<int, string>>("key_what_changed", value);
		}
	}

	public string GetWhatChangedString()
	{
		string text = string.Empty;
		foreach (int num in this.DictWhatChanged.Keys)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"{",
				num,
				":",
				this.DictWhatChanged[num],
				"}"
			});
		}
		return text;
	}

	private void LoadData()
	{
	}

	//public void UpdateToServer(string whatChanged)
	//{
	//	if (this.isRecovering)
	//	{
	//		return;
	//	}
	//	this.Sequence++;
	//	this.DictWhatChanged.AddExt(this.Sequence, whatChanged);
	//	new CSUpdateAccount(this.Sequence + ":" + whatChanged, this.GetAccountData()).Send(false);
	//}

	public void OnFbLogin(FBUser user)
	{
		if (AccountManager.Instance.FbId == null)
		{
			AccountManager.Instance.FbId = user.id;
			AccountManager.Instance.Name = user.name;
			NetManager.Instance.Restart();
		}
	}

	public AccountData GetAccountData()
	{
		return new AccountData(new ClientData
		{
			deviceId = this.DeviceId,
			fbId = this.FbId,
			name = this.Name,
			sequence = this.Sequence,
			gameId = Application.identifier,
			appVersion = Application.version,
			mobileDeviceInfo = MobileDeviceInfo.GetDeviceInfo(),
			language = LocalizationManager.CurrentLanguageCode
		}, new ServerData
		{
			code = this.Code,
			token = this.Token,
			extServerData = ExtAccountManager.Instance.GetExtServerData()
		});
	}

	public void SetAccountData(AccountData accountData)
	{
		this.isRecovering = true;
		this.Code = accountData.serverData.code;
		this.Token = accountData.serverData.token;
		this.Sequence = accountData.clientData.sequence;
		ExtAccountManager.Instance.SetAccountData(accountData);
		this.isRecovering = false;
	}

	public void DeleteAccount()
	{
		new CSDeleteAccount().Send(false);
		this.Code = -1;
		this.Token = null;
		this.Sequence = 1;
		ExtAccountManager.Instance.DeleteAccountData();
		NetManager.Instance.Restart();
	}

	private const string KEY_DEVICE_ID = "key_device_id";

	private const string KEY_FB_ID = "key_fb_id";

	private const string KEY_NAME = "key_name";

	private const string KEY_FB_IMAGE_URL = "key_fb_image_url";

	private const string KEY_SEQUENCE = "key_sequence";

	private const string KEY_CODE = "key_code";

	private const string KEY_TOKEN = "key_token";

	public const string KEY_WHAT_CHANGED = "key_what_changed";

	public const string KEY_APPSFLYER_CONVERTION_DATA = "key_appsflyer_convertion_data";

	public const string KEY_APPSFLYER_ID = "key_appsflyer_id";

	private static AccountManager _instance;

	private string deviceId;

	private string fbId;

	private string name;

	private string fbImageUrl;

	private int sequence = 1;

	private string conversionData;

	private string appsflyerId;

	[SerializeField]
	private int code = -1;

	[SerializeField]
	private string token;

	private Dictionary<int, string> dictWhatChanged = new Dictionary<int, string>();

	private bool isRecovering;
}
