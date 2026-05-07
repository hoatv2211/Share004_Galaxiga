using System;

namespace OSNet
{
	public class CSDeleteAccount : CSMessage
	{
		public override string GetEvent()
		{
			return "cs_delete_account";
		}
	}
}
