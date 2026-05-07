using System;

namespace Spine
{
	public class TransformConstraintTimeline : CurveTimeline
	{
		public TransformConstraintTimeline(int frameCount) : base(frameCount)
		{
			this.frames = new float[frameCount * 5];
		}

		public int TransformConstraintIndex
		{
			get
			{
				return this.transformConstraintIndex;
			}
			set
			{
				this.transformConstraintIndex = value;
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
				return 167772160 + this.transformConstraintIndex;
			}
		}

		public void SetFrame(int frameIndex, float time, float rotateMix, float translateMix, float scaleMix, float shearMix)
		{
			frameIndex *= 5;
			this.frames[frameIndex] = time;
			this.frames[frameIndex + 1] = rotateMix;
			this.frames[frameIndex + 2] = translateMix;
			this.frames[frameIndex + 3] = scaleMix;
			this.frames[frameIndex + 4] = shearMix;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			TransformConstraint transformConstraint = skeleton.transformConstraints.Items[this.transformConstraintIndex];
			float[] array = this.frames;
			if (time >= array[0])
			{
				float num2;
				float num3;
				float num4;
				float num5;
				if (time >= array[array.Length - 5])
				{
					int num = array.Length;
					num2 = array[num + -4];
					num3 = array[num + -3];
					num4 = array[num + -2];
					num5 = array[num + -1];
				}
				else
				{
					int num6 = Animation.BinarySearch(array, time, 5);
					num2 = array[num6 + -4];
					num3 = array[num6 + -3];
					num4 = array[num6 + -2];
					num5 = array[num6 + -1];
					float num7 = array[num6];
					float curvePercent = base.GetCurvePercent(num6 / 5 - 1, 1f - (time - num7) / (array[num6 + -5] - num7));
					num2 += (array[num6 + 1] - num2) * curvePercent;
					num3 += (array[num6 + 2] - num3) * curvePercent;
					num4 += (array[num6 + 3] - num4) * curvePercent;
					num5 += (array[num6 + 4] - num5) * curvePercent;
				}
				if (pose == MixPose.Setup)
				{
					TransformConstraintData data = transformConstraint.data;
					transformConstraint.rotateMix = data.rotateMix + (num2 - data.rotateMix) * alpha;
					transformConstraint.translateMix = data.translateMix + (num3 - data.translateMix) * alpha;
					transformConstraint.scaleMix = data.scaleMix + (num4 - data.scaleMix) * alpha;
					transformConstraint.shearMix = data.shearMix + (num5 - data.shearMix) * alpha;
				}
				else
				{
					transformConstraint.rotateMix += (num2 - transformConstraint.rotateMix) * alpha;
					transformConstraint.translateMix += (num3 - transformConstraint.translateMix) * alpha;
					transformConstraint.scaleMix += (num4 - transformConstraint.scaleMix) * alpha;
					transformConstraint.shearMix += (num5 - transformConstraint.shearMix) * alpha;
				}
				return;
			}
			TransformConstraintData data2 = transformConstraint.data;
			if (pose == MixPose.Setup)
			{
				transformConstraint.rotateMix = data2.rotateMix;
				transformConstraint.translateMix = data2.translateMix;
				transformConstraint.scaleMix = data2.scaleMix;
				transformConstraint.shearMix = data2.shearMix;
				return;
			}
			if (pose != MixPose.Current)
			{
				return;
			}
			transformConstraint.rotateMix += (data2.rotateMix - transformConstraint.rotateMix) * alpha;
			transformConstraint.translateMix += (data2.translateMix - transformConstraint.translateMix) * alpha;
			transformConstraint.scaleMix += (data2.scaleMix - transformConstraint.scaleMix) * alpha;
			transformConstraint.shearMix += (data2.shearMix - transformConstraint.shearMix) * alpha;
		}

		public const int ENTRIES = 5;

		private const int PREV_TIME = -5;

		private const int PREV_ROTATE = -4;

		private const int PREV_TRANSLATE = -3;

		private const int PREV_SCALE = -2;

		private const int PREV_SHEAR = -1;

		private const int ROTATE = 1;

		private const int TRANSLATE = 2;

		private const int SCALE = 3;

		private const int SHEAR = 4;

		internal int transformConstraintIndex;

		internal float[] frames;
	}
}
