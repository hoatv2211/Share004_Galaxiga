using System;

namespace OSNet
{
	public class SCUpdateAccountRsp : SCMessage
	{
		public override string GetEvent()
		{
			return "sc_update_account_rsp";
		}

		public override void OnData()
		{
			AccountManager.Instance.DictWhatChanged.RemoveExt(this.sequence);
		}

		public int sequence;
	}
}
