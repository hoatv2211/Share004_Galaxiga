using System;
using UnityEngine;

namespace Spine.Unity.Playables
{
	[AddComponentMenu("Spine/Playables/SkeletonAnimation Playable Handle (Playables)")]
	public class SkeletonAnimationPlayableHandle : SpinePlayableHandleBase
	{
		public override Skeleton Skeleton
		{
			get
			{
				return this.skeletonAnimation.Skeleton;
			}
		}

		public override SkeletonData SkeletonData
		{
			get
			{
				return this.skeletonAnimation.Skeleton.data;
			}
		}

		private void Awake()
		{
			if (this.skeletonAnimation == null)
			{
				this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			}
		}

		public SkeletonAnimation skeletonAnimation;
	}
}
