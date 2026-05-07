using System;
using com.ootii.Collections;

namespace com.ootii.Messages
{
	public class Message : IMessage
	{
		public string Type
		{
			get
			{
				return this.mType;
			}
			set
			{
				this.mType = value;
			}
		}

		public object Sender
		{
			get
			{
				return this.mSender;
			}
			set
			{
				this.mSender = value;
			}
		}

		public object Recipient
		{
			get
			{
				return this.mRecipient;
			}
			set
			{
				this.mRecipient = value;
			}
		}

		public float Delay
		{
			get
			{
				return this.mDelay;
			}
			set
			{
				this.mDelay = value;
			}
		}

		public object Data
		{
			get
			{
				return this.mData;
			}
			set
			{
				this.mData = value;
			}
		}

		public bool IsSent
		{
			get
			{
				return this.mIsSent;
			}
			set
			{
				this.mIsSent = value;
			}
		}

		public bool IsHandled
		{
			get
			{
				return this.mIsHandled;
			}
			set
			{
				this.mIsHandled = value;
			}
		}

		public virtual void Clear()
		{
			this.mType = string.Empty;
			this.mSender = null;
			this.mRecipient = null;
			this.mData = null;
			this.mIsSent = false;
			this.mIsHandled = false;
			this.mDelay = 0f;
		}

		public static Message Allocate()
		{
			Message message = Message.sPool.Allocate();
			message.IsSent = false;
			message.IsHandled = false;
			if (message == null)
			{
				message = new Message();
			}
			return message;
		}

		public static void Release(Message rInstance)
		{
			if (rInstance == null)
			{
				return;
			}
			rInstance.IsSent = true;
			rInstance.IsHandled = true;
			Message.sPool.Release(rInstance);
		}

		public static void Release(IMessage rInstance)
		{
			if (rInstance == null)
			{
				return;
			}
			rInstance.Clear();
			rInstance.IsSent = true;
			rInstance.IsHandled = true;
			if (rInstance is Message)
			{
				Message.sPool.Release((Message)rInstance);
			}
		}

		protected string mType = string.Empty;

		protected object mSender;

		protected object mRecipient;

		protected float mDelay;

		protected object mData;

		protected bool mIsSent;

		protected bool mIsHandled;

		private static ObjectPool<Message> sPool = new ObjectPool<Message>(40, 10);
	}
}
