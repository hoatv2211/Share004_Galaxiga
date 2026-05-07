using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.ootii.Messages
{
	public class MessageDispatcher
	{
		public static int RecipientType
		{
			get
			{
				return MessageDispatcher.mRecipientType;
			}
			set
			{
				MessageDispatcher.mRecipientType = value;
			}
		}

		public static void ClearMessages()
		{
			MessageDispatcher.mMessages.Clear();
		}

		public static void ClearListeners()
		{
			foreach (string key in MessageDispatcher.mMessageHandlers.Keys)
			{
				MessageDispatcher.mMessageHandlers[key].Clear();
			}
			MessageDispatcher.mMessageHandlers.Clear();
			MessageDispatcher.mListenerAdds.Clear();
			MessageDispatcher.mListenerRemoves.Clear();
		}

		public static void AddListener(string rMessageType, MessageHandler rHandler)
		{
			MessageDispatcher.AddListener(rMessageType, string.Empty, rHandler, false);
		}

		public static void AddListener(string rMessageType, MessageHandler rHandler, bool rImmediate)
		{
			MessageDispatcher.AddListener(rMessageType, string.Empty, rHandler, rImmediate);
		}

		public static void AddListener(UnityEngine.Object rOwner, string rMessageType, MessageHandler rHandler)
		{
			MessageDispatcher.AddListener(rOwner, rMessageType, rHandler, false);
		}

		public static void AddListener(UnityEngine.Object rOwner, string rMessageType, MessageHandler rHandler, bool rImmediate)
		{
			if (rOwner == null)
			{
				MessageDispatcher.AddListener(rMessageType, string.Empty, rHandler, rImmediate);
			}
			else if (MessageDispatcher.mRecipientType == EnumMessageRecipientType.NAME)
			{
				if (rOwner != null)
				{
					MessageDispatcher.AddListener(rMessageType, rOwner.name, rHandler, rImmediate);
				}
			}
			else if (MessageDispatcher.mRecipientType == EnumMessageRecipientType.TAG)
			{
				if (rOwner is GameObject)
				{
					MessageDispatcher.AddListener(rMessageType, ((GameObject)rOwner).tag, rHandler, rImmediate);
				}
			}
			else
			{
				MessageDispatcher.AddListener(rMessageType, string.Empty, rHandler, rImmediate);
			}
		}

		public static void AddListener(string rMessageType, string rFilter, MessageHandler rHandler)
		{
			MessageDispatcher.AddListener(rMessageType, rFilter, rHandler, false);
		}

		public static void AddListener(string rMessageType, string rFilter, MessageHandler rHandler, bool rImmediate)
		{
			MessageListenerDefinition messageListenerDefinition = MessageListenerDefinition.Allocate();
			messageListenerDefinition.MessageType = rMessageType;
			messageListenerDefinition.Filter = rFilter;
			messageListenerDefinition.Handler = rHandler;
			if (rImmediate)
			{
				MessageDispatcher.AddListener(messageListenerDefinition);
				MessageListenerDefinition.Release(messageListenerDefinition);
			}
			else
			{
				MessageDispatcher.mListenerAdds.Add(messageListenerDefinition);
			}
		}

		private static void AddListener(MessageListenerDefinition rListener)
		{
			Dictionary<string, MessageHandler> dictionary = null;
			if (MessageDispatcher.mMessageHandlers.ContainsKey(rListener.MessageType))
			{
				dictionary = MessageDispatcher.mMessageHandlers[rListener.MessageType];
			}
			else if (!MessageDispatcher.mMessageHandlers.ContainsKey(rListener.MessageType))
			{
				dictionary = new Dictionary<string, MessageHandler>();
				MessageDispatcher.mMessageHandlers.Add(rListener.MessageType, dictionary);
			}
			if (!dictionary.ContainsKey(rListener.Filter))
			{
				dictionary.Add(rListener.Filter, null);
			}
			Dictionary<string, MessageHandler> dictionary2;
			string filter;
			(dictionary2 = dictionary)[filter = rListener.Filter] = (MessageHandler)Delegate.Combine(dictionary2[filter], rListener.Handler);
			MessageListenerDefinition.Release(rListener);
		}

		public static void RemoveListener(string rMessageType, MessageHandler rHandler)
		{
			MessageDispatcher.RemoveListener(rMessageType, string.Empty, rHandler, false);
		}

		public static void RemoveListener(string rMessageType, MessageHandler rHandler, bool rImmediate)
		{
			MessageDispatcher.RemoveListener(rMessageType, string.Empty, rHandler, rImmediate);
		}

		public static void RemoveListener(UnityEngine.Object rOwner, string rMessageType, MessageHandler rHandler)
		{
			MessageDispatcher.RemoveListener(rOwner, rMessageType, rHandler, false);
		}

		public static void RemoveListener(UnityEngine.Object rOwner, string rMessageType, MessageHandler rHandler, bool rImmediate)
		{
			if (rOwner == null)
			{
				MessageDispatcher.RemoveListener(rMessageType, string.Empty, rHandler, rImmediate);
			}
			else if (MessageDispatcher.mRecipientType == EnumMessageRecipientType.NAME)
			{
				if (rOwner != null)
				{
					MessageDispatcher.RemoveListener(rMessageType, rOwner.name, rHandler, rImmediate);
				}
			}
			else if (MessageDispatcher.mRecipientType == EnumMessageRecipientType.TAG)
			{
				if (rOwner is GameObject)
				{
					MessageDispatcher.RemoveListener(rMessageType, ((GameObject)rOwner).tag, rHandler, rImmediate);
				}
			}
			else
			{
				MessageDispatcher.RemoveListener(rMessageType, string.Empty, rHandler, rImmediate);
			}
		}

		public static void RemoveListener(string rMessageType, string rFilter, MessageHandler rHandler)
		{
			MessageDispatcher.RemoveListener(rMessageType, rFilter, rHandler, false);
		}

		public static void RemoveListener(string rMessageType, string rFilter, MessageHandler rHandler, bool rImmediate)
		{
			MessageListenerDefinition messageListenerDefinition = MessageListenerDefinition.Allocate();
			messageListenerDefinition.MessageType = rMessageType;
			messageListenerDefinition.Filter = rFilter;
			messageListenerDefinition.Handler = rHandler;
			if (rImmediate)
			{
				MessageDispatcher.RemoveListener(messageListenerDefinition);
				MessageListenerDefinition.Release(messageListenerDefinition);
			}
			else
			{
				MessageDispatcher.mListenerRemoves.Add(messageListenerDefinition);
			}
		}

		private static void RemoveListener(MessageListenerDefinition rListener)
		{
			if (MessageDispatcher.mMessageHandlers.ContainsKey(rListener.MessageType) && MessageDispatcher.mMessageHandlers[rListener.MessageType].ContainsKey(rListener.Filter))
			{
				if (MessageDispatcher.mMessageHandlers[rListener.MessageType][rListener.Filter] != null && rListener.Handler != null)
				{
					Dictionary<string, MessageHandler> dictionary;
					string filter;
					(dictionary = MessageDispatcher.mMessageHandlers[rListener.MessageType])[filter = rListener.Filter] = (MessageHandler)Delegate.Remove(dictionary[filter], rListener.Handler);
				}
				if (MessageDispatcher.mMessageHandlers[rListener.MessageType][rListener.Filter] == null)
				{
					MessageDispatcher.mMessageHandlers[rListener.MessageType].Remove(rListener.Filter);
				}
				if (MessageDispatcher.mMessageHandlers[rListener.MessageType].Count == 0)
				{
					MessageDispatcher.mMessageHandlers.Remove(rListener.MessageType);
				}
			}
			MessageListenerDefinition.Release(rListener);
		}

		public static void SendMessage(string rType)
		{
			Message message = Message.Allocate();
			message.Sender = null;
			message.Recipient = string.Empty;
			message.Type = rType;
			message.Data = null;
			message.Delay = EnumMessageDelay.IMMEDIATE;
			MessageDispatcher.SendMessage(message);
			Message.Release(message);
		}

		public static void SendMessage(string rType, string rFilter)
		{
			Message message = Message.Allocate();
			message.Sender = null;
			message.Recipient = rFilter;
			message.Type = rType;
			message.Data = null;
			message.Delay = EnumMessageDelay.IMMEDIATE;
			MessageDispatcher.SendMessage(message);
			Message.Release(message);
		}

		public static void SendMessage(string rType, float rDelay)
		{
			Message message = Message.Allocate();
			message.Sender = null;
			message.Recipient = string.Empty;
			message.Type = rType;
			message.Data = null;
			message.Delay = rDelay;
			MessageDispatcher.SendMessage(message);
			if (rDelay == EnumMessageDelay.IMMEDIATE)
			{
				Message.Release(message);
			}
		}

		public static void SendMessage(string rType, string rFilter, float rDelay)
		{
			Message message = Message.Allocate();
			message.Sender = null;
			message.Recipient = rFilter;
			message.Type = rType;
			message.Data = null;
			message.Delay = rDelay;
			MessageDispatcher.SendMessage(message);
			if (rDelay == EnumMessageDelay.IMMEDIATE)
			{
				Message.Release(message);
			}
		}

		public static void SendMessage(object rSender, string rType, object rData, float rDelay)
		{
			Message message = Message.Allocate();
			message.Sender = rSender;
			message.Recipient = string.Empty;
			message.Type = rType;
			message.Data = rData;
			message.Delay = rDelay;
			MessageDispatcher.SendMessage(message);
			if (rDelay == EnumMessageDelay.IMMEDIATE)
			{
				Message.Release(message);
			}
		}

		public static void SendMessage(object rSender, object rRecipient, string rType, object rData, float rDelay)
		{
			Message message = Message.Allocate();
			message.Sender = rSender;
			message.Recipient = ((rRecipient == null) ? string.Empty : rRecipient);
			message.Type = rType;
			message.Data = rData;
			message.Delay = rDelay;
			MessageDispatcher.SendMessage(message);
			if (rDelay == EnumMessageDelay.IMMEDIATE)
			{
				Message.Release(message);
			}
		}

		public static void SendMessage(object rSender, string rRecipient, string rType, object rData, float rDelay)
		{
			Message message = Message.Allocate();
			message.Sender = rSender;
			message.Recipient = rRecipient;
			message.Type = rType;
			message.Data = rData;
			message.Delay = rDelay;
			MessageDispatcher.SendMessage(message);
			if (rDelay == EnumMessageDelay.IMMEDIATE)
			{
				Message.Release(message);
			}
		}

		public static void SendMessage(IMessage rMessage)
		{
			bool flag = true;
			if (rMessage.Delay > 0f || rMessage.Delay < 0f)
			{
				if (!MessageDispatcher.mMessages.Contains(rMessage))
				{
					MessageDispatcher.mMessages.Add(rMessage);
				}
				flag = false;
			}
			else if (MessageDispatcher.mMessageHandlers.ContainsKey(rMessage.Type))
			{
				Dictionary<string, MessageHandler> dictionary = MessageDispatcher.mMessageHandlers[rMessage.Type];
				foreach (string text in dictionary.Keys)
				{
					if (dictionary[text] == null)
					{
						MessageDispatcher.RemoveListener(rMessage.Type, text, null);
					}
					else if (text == string.Empty)
					{
						rMessage.IsSent = true;
						dictionary[text](rMessage);
						flag = false;
					}
					else if (MessageDispatcher.mRecipientType == EnumMessageRecipientType.NAME && rMessage.Recipient is UnityEngine.Object)
					{
						if (text.ToLower() == ((UnityEngine.Object)rMessage.Recipient).name.ToLower())
						{
							rMessage.IsSent = true;
							dictionary[text](rMessage);
							flag = false;
						}
					}
					else if (MessageDispatcher.mRecipientType == EnumMessageRecipientType.TAG && rMessage.Recipient is GameObject)
					{
						if (text.ToLower() == ((GameObject)rMessage.Recipient).tag.ToLower())
						{
							rMessage.IsSent = true;
							dictionary[text](rMessage);
							flag = false;
						}
					}
					else if (rMessage.Recipient is string && text.ToLower() == ((string)rMessage.Recipient).ToLower())
					{
						rMessage.IsSent = true;
						dictionary[text](rMessage);
						flag = false;
					}
				}
			}
			if (flag)
			{
				if (MessageDispatcher.MessageNotHandled == null)
				{
					UnityEngine.Debug.LogWarning("MessageDispatcher: Unhandled Message of type " + rMessage.Type);
				}
				else
				{
					MessageDispatcher.MessageNotHandled(rMessage);
				}
				rMessage.IsHandled = true;
			}
		}

		public static void Update()
		{
			for (int i = 0; i < MessageDispatcher.mMessages.Count; i++)
			{
				IMessage message = MessageDispatcher.mMessages[i];
				message.Delay -= Time.deltaTime;
				if (message.Delay < 0f)
				{
					message.Delay = EnumMessageDelay.IMMEDIATE;
				}
				if (!message.IsSent && message.Delay == EnumMessageDelay.IMMEDIATE)
				{
					MessageDispatcher.SendMessage(message);
				}
			}
			for (int j = MessageDispatcher.mMessages.Count - 1; j >= 0; j--)
			{
				IMessage message2 = MessageDispatcher.mMessages[j];
				if (message2.IsSent || message2.IsHandled)
				{
					MessageDispatcher.mMessages.RemoveAt(j);
					if (message2.IsHandled)
					{
						Message.Release(message2);
					}
				}
			}
			for (int k = MessageDispatcher.mListenerRemoves.Count - 1; k >= 0; k--)
			{
				MessageDispatcher.RemoveListener(MessageDispatcher.mListenerRemoves[k]);
			}
			MessageDispatcher.mListenerRemoves.Clear();
			for (int l = MessageDispatcher.mListenerAdds.Count - 1; l >= 0; l--)
			{
				MessageDispatcher.AddListener(MessageDispatcher.mListenerAdds[l]);
			}
			MessageDispatcher.mListenerAdds.Clear();
		}

		private static int mRecipientType = EnumMessageRecipientType.NAME;

		public static MessageHandler MessageNotHandled = null;

		private static MessageDispatcherStub sStub = new GameObject("MessageDispatcherStub").AddComponent<MessageDispatcherStub>();

		private static List<IMessage> mMessages = new List<IMessage>();

		private static Dictionary<string, Dictionary<string, MessageHandler>> mMessageHandlers = new Dictionary<string, Dictionary<string, MessageHandler>>();

		private static List<MessageListenerDefinition> mListenerAdds = new List<MessageListenerDefinition>();

		private static List<MessageListenerDefinition> mListenerRemoves = new List<MessageListenerDefinition>();
	}
}
