using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Spine.Unity.Modules
{
	public class SpineEventUnityHandler : MonoBehaviour
	{
		private void Start()
		{
            skeletonComponent = skeletonComponent ?? GetComponent<ISkeletonComponent>();
            if (skeletonComponent == null)
            {
                return;
            }
            animationStateComponent = animationStateComponent ?? (skeletonComponent as IAnimationStateComponent);
            if (animationStateComponent == null)
            {
                return;
            }
            Skeleton skeleton = skeletonComponent.Skeleton;
            if (skeleton == null)
            {
                return;
            }
            SkeletonData data = skeleton.Data;
            Spine.AnimationState animationState = animationStateComponent.AnimationState;
            foreach (EventPair ep in events)
            {
                EventData eventData = data.FindEvent(ep.spineEvent);
                ep.eventDelegate = ep.eventDelegate ?? ((Spine.AnimationState.TrackEntryEventDelegate)delegate (TrackEntry trackEntry, Spine.Event e)
                {
                    if (e.Data == eventData)
                    {
                        ep.unityHandler.Invoke();
                    }
                });
                animationState.Event += ep.eventDelegate;
            }
        }

		private void OnDestroy()
		{
			this.animationStateComponent = (this.animationStateComponent ?? base.GetComponent<IAnimationStateComponent>());
			if (this.animationStateComponent == null)
			{
				return;
			}
			AnimationState animationState = this.animationStateComponent.AnimationState;
			foreach (SpineEventUnityHandler.EventPair eventPair in this.events)
			{
				if (eventPair.eventDelegate != null)
				{
					animationState.Event -= eventPair.eventDelegate;
				}
				eventPair.eventDelegate = null;
			}
		}

		public List<SpineEventUnityHandler.EventPair> events = new List<SpineEventUnityHandler.EventPair>();

		private ISkeletonComponent skeletonComponent;

		private IAnimationStateComponent animationStateComponent;

		[Serializable]
		public class EventPair
		{
			[SpineEvent("", "", true, false)]
			public string spineEvent;

			public UnityEvent unityHandler;

			public AnimationState.TrackEntryEventDelegate eventDelegate;
		}
	}
}
