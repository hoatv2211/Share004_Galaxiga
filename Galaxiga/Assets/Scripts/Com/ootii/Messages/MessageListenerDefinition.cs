using System;
using com.ootii.Collections;

namespace com.ootii.Messages
{
	public class MessageListenerDefinition
	{
		public static MessageListenerDefinition Allocate()
		{
			MessageListenerDefinition messageListenerDefinition = MessageListenerDefinition.sPool.Allocate();
			messageListenerDefinition.MessageType = string.Empty;
			messageListenerDefinition.Filter = string.Empty;
			messageListenerDefinition.Handler = null;
			if (messageListenerDefinition == null)
			{
				messageListenerDefinition = new MessageListenerDefinition();
			}
			return messageListenerDefinition;
		}

		public static void Release(MessageListenerDefinition rInstance)
		{
			if (rInstance == null)
			{
				return;
			}
			rInstance.MessageType = string.Empty;
			rInstance.Filter = string.Empty;
			rInstance.Handler = null;
			MessageListenerDefinition.sPool.Release(rInstance);
		}

		public string MessageType;

		public string Filter;

		public MessageHandler Handler;

		private static ObjectPool<MessageListenerDefinition> sPool = new ObjectPool<MessageListenerDefinition>(40, 10);
	}
}
