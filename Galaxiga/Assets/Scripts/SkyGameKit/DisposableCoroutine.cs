using System;
using UnityEngine;

namespace SkyGameKit
{
	public class DisposableCoroutine : IDisposable
	{
		public DisposableCoroutine(MonoBehaviour target, Coroutine routine)
		{
			this.target = target;
			this.routine = routine;
		}

		public void Dispose()
		{
			if (this.routine != null)
			{
				this.target.StopCoroutine(this.routine);
			}
		}

		private readonly MonoBehaviour target;

		private readonly Coroutine routine;
	}
}
