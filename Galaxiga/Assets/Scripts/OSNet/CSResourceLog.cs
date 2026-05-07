using System;

namespace OSNet
{
	public class CSResourceLog : CSMessage
	{
		public CSResourceLog(string flowType, string itemType, string itemId, string currency, int amount)
		{
			this.flowType = flowType;
			this.itemType = itemType;
			this.itemId = itemId;
			this.currency = currency;
			this.amount = amount;
		}

		public override string GetEvent()
		{
			return "cs_resource_log";
		}

		public string flowType;

		public string itemType;

		public string itemId;

		public string currency;

		public int amount;
	}
}
