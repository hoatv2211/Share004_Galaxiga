using System;

namespace Spine
{
	public class PathConstraintPositionTimeline : CurveTimeline
	{
		public PathConstraintPositionTimeline(int frameCount) : base(frameCount)
		{
			this.frames = new float[frameCount * 2];
		}

		public override int PropertyId
		{
			get
			{
				return 184549376 + this.pathConstraintIndex;
			}
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

		public void SetFrame(int frameIndex, float time, float value)
		{
			frameIndex *= 2;
			this.frames[frameIndex] = time;
			this.frames[frameIndex + 1] = value;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			PathConstraint pathConstraint = skeleton.pathConstraints.Items[this.pathConstraintIndex];
			float[] array = this.frames;
			if (time >= array[0])
			{
				float num;
				if (time >= array[array.Length - 2])
				{
					num = array[array.Length + -1];
				}
				else
				{
					int num2 = Animation.BinarySearch(array, time, 2);
					num = array[num2 + -1];
					float num3 = array[num2];
					float curvePercent = base.GetCurvePercent(num2 / 2 - 1, 1f - (time - num3) / (array[num2 + -2] - num3));
					num += (array[num2 + 1] - num) * curvePercent;
				}
				if (pose == MixPose.Setup)
				{
					pathConstraint.position = pathConstraint.data.position + (num - pathConstraint.data.position) * alpha;
				}
				else
				{
					pathConstraint.position += (num - pathConstraint.position) * alpha;
				}
				return;
			}
			if (pose == MixPose.Setup)
			{
				pathConstraint.position = pathConstraint.data.position;
				return;
			}
			if (pose != MixPose.Current)
			{
				return;
			}
			pathConstraint.position += (pathConstraint.data.position - pathConstraint.position) * alpha;
		}

		public const int ENTRIES = 2;

		protected const int PREV_TIME = -2;

		protected const int PREV_VALUE = -1;

		protected const int VALUE = 1;

		internal int pathConstraintIndex;

		internal float[] frames;
	}
}
