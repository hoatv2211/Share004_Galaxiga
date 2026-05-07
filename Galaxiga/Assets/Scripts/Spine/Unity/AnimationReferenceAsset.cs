using System;
using UnityEngine;

namespace Spine.Unity
{
	[CreateAssetMenu(menuName = "Spine/Animation Reference Asset")]
	public class AnimationReferenceAsset : ScriptableObject, IHasSkeletonDataAsset
	{
		public SkeletonDataAsset SkeletonDataAsset
		{
			get
			{
				return this.skeletonDataAsset;
			}
		}

		public Animation Animation
		{
			get
			{
				if (this.animation == null)
				{
					this.Initialize();
				}
				return this.animation;
			}
		}

		public void Initialize()
		{
			if (this.skeletonDataAsset == null)
			{
				return;
			}
			this.animation = this.skeletonDataAsset.GetSkeletonData(true).FindAnimation(this.animationName);
			if (this.animation == null)
			{
				UnityEngine.Debug.LogWarningFormat("Animation '{0}' not found in SkeletonData : {1}.", new object[]
				{
					this.animationName,
					this.skeletonDataAsset.name
				});
			}
		}

		public static implicit operator Animation(AnimationReferenceAsset asset)
		{
			return asset.Animation;
		}

		private const bool QuietSkeletonData = true;

		[SerializeField]
		protected SkeletonDataAsset skeletonDataAsset;

		[SerializeField]
		[SpineAnimation("", "", true, false)]
		protected string animationName;

		private Animation animation;
	}
}
