using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Spine.Unity.Playables
{
	public class SpineSkeletonFlipMixerBehaviour : PlayableBehaviour
	{
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			this.playableHandle = (playerData as SpinePlayableHandleBase);
			if (this.playableHandle == null)
			{
				return;
			}
			Skeleton skeleton = this.playableHandle.Skeleton;
			if (!this.m_FirstFrameHappened)
			{
				this.defaultFlipX = skeleton.flipX;
				this.defaultFlipY = skeleton.flipY;
				this.m_FirstFrameHappened = true;
			}
			int inputCount = playable.GetInputCount<Playable>();
			float num = 0f;
			float num2 = 0f;
			int num3 = 0;
			for (int i = 0; i < inputCount; i++)
			{
				float inputWeight = playable.GetInputWeight(i);
				SpineSkeletonFlipBehaviour behaviour = ((ScriptPlayable<SpineSkeletonFlipBehaviour>)playable.GetInput(i)).GetBehaviour();
				num += inputWeight;
				if (inputWeight > num2)
				{
					skeleton.flipX = behaviour.flipX;
					skeleton.flipY = behaviour.flipY;
					num2 = inputWeight;
				}
				if (!Mathf.Approximately(inputWeight, 0f))
				{
					num3++;
				}
			}
			if (num3 != 1 && 1f - num > num2)
			{
				skeleton.flipX = this.defaultFlipX;
				skeleton.flipY = this.defaultFlipY;
			}
		}

		public override void OnGraphStop(Playable playable)
		{
			this.m_FirstFrameHappened = false;
			if (this.playableHandle == null)
			{
				return;
			}
			Skeleton skeleton = this.playableHandle.Skeleton;
			skeleton.flipX = this.defaultFlipX;
			skeleton.flipY = this.defaultFlipY;
		}

		private bool defaultFlipX;

		private bool defaultFlipY;

		private SpinePlayableHandleBase playableHandle;

		private bool m_FirstFrameHappened;
	}
}
