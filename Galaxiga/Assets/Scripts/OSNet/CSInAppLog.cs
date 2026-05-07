using System;

namespace OSNet
{
	public class CSInAppLog : CSMessage
	{
		public CSInAppLog(string productId, string transactionId, string purchaseToken, string currencyCode, string price, string where)
		{
			this.productId = productId;
			this.transactionId = transactionId;
			this.purchaseToken = purchaseToken;
			this.currencyCode = currencyCode;
			this.price = price;
			this.where = where;
		}

		public override string GetEvent()
		{
			return "cs_inapp_log";
		}

		public string productId;

		public string transactionId;

		public string purchaseToken;

		public string currencyCode;

		public string price;

		public string where;
	}
}
