using System;
using System.Diagnostics;
using UnityEngine;

namespace Spine.Unity.Playables
{
	public abstract class SpinePlayableHandleBase : MonoBehaviour
	{
		public abstract SkeletonData SkeletonData { get; }

		public abstract Skeleton Skeleton { get; }

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event SpineEventDelegate AnimationEvents;

		public virtual void HandleEvents(ExposedList<Event> eventBuffer)
		{
			if (eventBuffer == null || this.AnimationEvents == null)
			{
				return;
			}
			int i = 0;
			int count = eventBuffer.Count;
			while (i < count)
			{
				this.AnimationEvents(eventBuffer.Items[i]);
				i++;
			}
		}
	}
}
