using System;

namespace Spine
{
	public class PathConstraintMixTimeline : CurveTimeline
	{
		public PathConstraintMixTimeline(int frameCount) : base(frameCount)
		{
			this.frames = new float[frameCount * 3];
		}

		public int PathConstraintIndex
		{
			get
			{
				return this.pathConstraintIndex;
			}
			set
			{
				this.pathConstraintIndex = value;
			}
		}

		public float[] Frames
		{
			get
			{
				return this.frames;
			}
			set
			{
				this.frames = value;
			}
		}

		public override int PropertyId
		{
			get
			{
				return 218103808 + this.pathConstraintIndex;
			}
		}

		public void SetFrame(int frameIndex, float time, float rotateMix, float translateMix)
		{
			frameIndex *= 3;
			this.frames[frameIndex] = time;
			this.frames[frameIndex + 1] = rotateMix;
			this.frames[frameIndex + 2] = translateMix;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			PathConstraint pathConstraint = skeleton.pathConstraints.Items[this.pathConstraintIndex];
			float[] array = this.frames;
			if (time >= array[0])
			{
				float num;
				float num2;
				if (time >= array[array.Length - 3])
				{
					num = array[array.Length + -2];
					num2 = array[array.Length + -1];
				}
				else
				{
					int num3 = Animation.BinarySearch(array, time, 3);
					num = array[num3 + -2];
					num2 = array[num3 + -1];
					float num4 = array[num3];
					float curvePercent = base.GetCurvePercent(num3 / 3 - 1, 1f - (time - num4) / (array[num3 + -3] - num4));
					num += (array[num3 + 1] - num) * curvePercent;
					num2 += (array[num3 + 2] - num2) * curvePercent;
				}
				if (pose == MixPose.Setup)
				{
					pathConstraint.rotateMix = pathConstraint.data.rotateMix + (num - pathConstraint.data.rotateMix) * alpha;
					pathConstraint.translateMix = pathConstraint.data.translateMix + (num2 - pathConstraint.data.translateMix) * alpha;
				}
				else
				{
					pathConstraint.rotateMix += (num - pathConstraint.rotateMix) * alpha;
					pathConstraint.translateMix += (num2 - pathConstraint.translateMix) * alpha;
				}
				return;
			}
			if (pose == MixPose.Setup)
			{
				pathConstraint.rotateMix = pathConstraint.data.rotateMix;
				pathConstraint.translateMix = pathConstraint.data.translateMix;
				return;
			}
			if (pose != MixPose.Current)
			{
				return;
			}
			pathConstraint.rotateMix += (pathConstraint.data.rotateMix - pathConstraint.rotateMix) * alpha;
			pathConstraint.translateMix += (pathConstraint.data.translateMix - pathConstraint.translateMix) * alpha;
		}

		public const int ENTRIES = 3;

		private const int PREV_TIME = -3;

		private const int PREV_ROTATE = -2;

		private const int PREV_TRANSLATE = -1;

		private const int ROTATE = 1;

		private const int TRANSLATE = 2;

		internal int pathConstraintIndex;

		internal float[] frames;
	}
}
