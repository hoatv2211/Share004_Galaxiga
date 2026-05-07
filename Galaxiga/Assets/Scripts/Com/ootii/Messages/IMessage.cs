using System;

namespace com.ootii.Messages
{
	public interface IMessage
	{
		string Type { get; set; }

		object Sender { get; set; }

		object Recipient { get; set; }

		float Delay { get; set; }

		object Data { get; set; }

		bool IsSent { get; set; }

		bool IsHandled { get; set; }

		void Clear();
	}
}
