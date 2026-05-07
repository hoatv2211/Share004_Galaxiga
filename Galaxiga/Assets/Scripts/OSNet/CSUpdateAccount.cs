using System;

namespace OSNet
{
	public class CSUpdateAccount : CSMessage
	{
		public CSUpdateAccount()
		{
		}

		public CSUpdateAccount(string whatChanged, AccountData accountData)
		{
			this.whatChanged = whatChanged;
			this.accountData = accountData;
		}

		public override string GetEvent()
		{
			return "cs_update_account";
		}

		public string whatChanged;

		public AccountData accountData;
	}
}
