using System;

namespace Spine
{
	public class IkConstraintTimeline : CurveTimeline
	{
		public IkConstraintTimeline(int frameCount) : base(frameCount)
		{
			this.frames = new float[frameCount * 3];
		}

		public int IkConstraintIndex
		{
			get
			{
				return this.ikConstraintIndex;
			}
			set
			{
				this.ikConstraintIndex = value;
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
				return 150994944 + this.ikConstraintIndex;
			}
		}

		public void SetFrame(int frameIndex, float time, float mix, int bendDirection)
		{
			frameIndex *= 3;
			this.frames[frameIndex] = time;
			this.frames[frameIndex + 1] = mix;
			this.frames[frameIndex + 2] = (float)bendDirection;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			IkConstraint ikConstraint = skeleton.ikConstraints.Items[this.ikConstraintIndex];
			float[] array = this.frames;
			if (time < array[0])
			{
				if (pose == MixPose.Setup)
				{
					ikConstraint.mix = ikConstraint.data.mix;
					ikConstraint.bendDirection = ikConstraint.data.bendDirection;
					return;
				}
				if (pose != MixPose.Current)
				{
					return;
				}
				ikConstraint.mix += (ikConstraint.data.mix - ikConstraint.mix) * alpha;
				ikConstraint.bendDirection = ikConstraint.data.bendDirection;
				return;
			}
			else
			{
				if (time >= array[array.Length - 3])
				{
					if (pose == MixPose.Setup)
					{
						ikConstraint.mix = ikConstraint.data.mix + (array[array.Length + -2] - ikConstraint.data.mix) * alpha;
						ikConstraint.bendDirection = ((direction != MixDirection.Out) ? ((int)array[array.Length + -1]) : ikConstraint.data.bendDirection);
					}
					else
					{
						ikConstraint.mix += (array[array.Length + -2] - ikConstraint.mix) * alpha;
						if (direction == MixDirection.In)
						{
							ikConstraint.bendDirection = (int)array[array.Length + -1];
						}
					}
					return;
				}
				int num = Animation.BinarySearch(array, time, 3);
				float num2 = array[num + -2];
				float num3 = array[num];
				float curvePercent = base.GetCurvePercent(num / 3 - 1, 1f - (time - num3) / (array[num + -3] - num3));
				if (pose == MixPose.Setup)
				{
					ikConstraint.mix = ikConstraint.data.mix + (num2 + (array[num + 1] - num2) * curvePercent - ikConstraint.data.mix) * alpha;
					ikConstraint.bendDirection = ((direction != MixDirection.Out) ? ((int)array[num + -1]) : ikConstraint.data.bendDirection);
				}
				else
				{
					ikConstraint.mix += (num2 + (array[num + 1] - num2) * curvePercent - ikConstraint.mix) * alpha;
					if (direction == MixDirection.In)
					{
						ikConstraint.bendDirection = (int)array[num + -1];
					}
				}
				return;
			}
		}

		public const int ENTRIES = 3;

		private const int PREV_TIME = -3;

		private const int PREV_MIX = -2;

		private const int PREV_BEND_DIRECTION = -1;

		private const int MIX = 1;

		private const int BEND_DIRECTION = 2;

		internal int ikConstraintIndex;

		internal float[] frames;
	}
}
