using System;

namespace OSNet
{
	public class CSHomeOK : CSMessage
	{
		public override string GetEvent()
		{
			return "cs_home_ok";
		}
	}
}
