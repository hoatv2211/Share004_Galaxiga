using System;

namespace BestHTTP.SignalRCore
{
	internal struct CallbackDescriptor
	{
		public CallbackDescriptor(Type[] paramTypes, Action<object[]> callback)
		{
			this.ParamTypes = paramTypes;
			this.Callback = callback;
		}

		public readonly Type[] ParamTypes;

		public readonly Action<object[]> Callback;
	}
}
