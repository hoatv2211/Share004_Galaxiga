using System;

namespace OSNet
{
	public abstract class SCMessage : IMessage
	{
		public abstract string GetEvent();

		public abstract void OnData();

		public override string ToString()
		{
			return base.GetType() + ": " + this.message;
		}

		public const int SUCCESS = 0;

		public const int ERROR = 1;

		public const string MESSAGE_SUCCESS = "Success";

		public const string MESSAGE_ERROR = "Error";

		public int status;

		public string message;

		public long timeServer;

		public string localTimeString;
	}
}
