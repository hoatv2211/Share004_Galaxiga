using System;

namespace OSNet
{
	[Serializable]
	public abstract class CSMessage : IMessage
	{
		public abstract string GetEvent();

		public void Send(bool isImportant = false)
		{
			NetManager.Instance.Send(this.GetEvent(), this, isImportant);
		}

		public long clientTime = (long)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
	}
}
