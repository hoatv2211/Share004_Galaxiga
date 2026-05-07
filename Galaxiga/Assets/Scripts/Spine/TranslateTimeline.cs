using System;

namespace Spine
{
	public class TranslateTimeline : CurveTimeline
	{
		public TranslateTimeline(int frameCount) : base(frameCount)
		{
			this.frames = new float[frameCount * 3];
		}

		public int BoneIndex
		{
			get
			{
				return this.boneIndex;
			}
			set
			{
				this.boneIndex = value;
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
				return 16777216 + this.boneIndex;
			}
		}

		public void SetFrame(int frameIndex, float time, float x, float y)
		{
			frameIndex *= 3;
			this.frames[frameIndex] = time;
			this.frames[frameIndex + 1] = x;
			this.frames[frameIndex + 2] = y;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[this.boneIndex];
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
					bone.x = bone.data.x + num * alpha;
					bone.y = bone.data.y + num2 * alpha;
				}
				else
				{
					bone.x += (bone.data.x + num - bone.x) * alpha;
					bone.y += (bone.data.y + num2 - bone.y) * alpha;
				}
				return;
			}
			if (pose == MixPose.Setup)
			{
				bone.x = bone.data.x;
				bone.y = bone.data.y;
				return;
			}
			if (pose != MixPose.Current)
			{
				return;
			}
			bone.x += (bone.data.x - bone.x) * alpha;
			bone.y += (bone.data.y - bone.y) * alpha;
		}

		public const int ENTRIES = 3;

		protected const int PREV_TIME = -3;

		protected const int PREV_X = -2;

		protected const int PREV_Y = -1;

		protected const int X = 1;

		protected const int Y = 2;

		internal int boneIndex;

		internal float[] frames;
	}
}
