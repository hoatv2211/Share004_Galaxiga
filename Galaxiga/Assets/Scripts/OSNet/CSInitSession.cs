using System;

namespace OSNet
{
	public class CSInitSession : CSMessage
	{
		public CSInitSession()
		{
		}

		public CSInitSession(string whatChanged, AccountData accountData)
		{
			this.whatChanged = whatChanged;
			this.accountData = accountData;
		}

		public override string GetEvent()
		{
			return "cs_init_session";
		}

		public string whatChanged;

		public AccountData accountData;
	}
}
