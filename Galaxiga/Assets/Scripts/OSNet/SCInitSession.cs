using System;
using UnityEngine;

namespace OSNet
{
	public class SCInitSession : SCMessage
	{
		public override string GetEvent()
		{
			return "sc_init_session";
		}

		public override void OnData()
		{
			UnityEngine.Debug.Log("type " + this.type);
			if (this.type == -1)
			{
				AccountManager.Instance.Code = -1;
				AccountManager.Instance.Token = string.Empty;
				new CSInitSession(AccountManager.Instance.GetWhatChangedString(), AccountManager.Instance.GetAccountData()).Send(false);
			}
			else if (this.type == 1 || this.type == 2 || this.account.clientData.sequence > AccountManager.Instance.Sequence)
			{
				AccountManager.Instance.SetAccountData(this.account);
				AccountManager.Instance.DictWhatChanged.ClearExt();
				NetManager.Instance.StartSession();
			}
			else
			{
				AccountManager.Instance.DictWhatChanged.ClearExt();
				NetManager.Instance.StartSession();
			}
		}

		public int type;

		public AccountData account;

		public const int TYPE_INVALID_TOKEN = -1;

		public const int TYPE_OK = 0;

		public const int TYPE_CREATE_NEW = 1;

		public const int TYPE_NEED_UPDATE = 2;
	}
}
