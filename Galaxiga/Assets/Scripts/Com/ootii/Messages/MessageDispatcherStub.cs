using System;
using UnityEngine;

namespace com.ootii.Messages
{
	public sealed class MessageDispatcherStub : MonoBehaviour
	{
		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		private void Update()
		{
			MessageDispatcher.Update();
		}

		public void OnDisable()
		{
			MessageDispatcher.ClearMessages();
			MessageDispatcher.ClearListeners();
		}
	}
}
