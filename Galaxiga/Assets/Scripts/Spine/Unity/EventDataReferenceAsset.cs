using System;
using UnityEngine;

namespace Spine.Unity
{
	[CreateAssetMenu(menuName = "Spine/EventData Reference Asset")]
	public class EventDataReferenceAsset : ScriptableObject
	{
		public EventData EventData
		{
			get
			{
				if (this.eventData == null)
				{
					this.Initialize();
				}
				return this.eventData;
			}
		}

		public void Initialize()
		{
			if (this.skeletonDataAsset == null)
			{
				return;
			}
			this.eventData = this.skeletonDataAsset.GetSkeletonData(true).FindEvent(this.eventName);
			if (this.eventData == null)
			{
				UnityEngine.Debug.LogWarningFormat("Event Data '{0}' not found in SkeletonData : {1}.", new object[]
				{
					this.eventName,
					this.skeletonDataAsset.name
				});
			}
		}

		public static implicit operator EventData(EventDataReferenceAsset asset)
		{
			return asset.EventData;
		}

		private const bool QuietSkeletonData = true;

		[SerializeField]
		protected SkeletonDataAsset skeletonDataAsset;

		[SerializeField]
		[SpineEvent("", "skeletonDataAsset", true, false)]
		protected string eventName;

		private EventData eventData;
	}
}
