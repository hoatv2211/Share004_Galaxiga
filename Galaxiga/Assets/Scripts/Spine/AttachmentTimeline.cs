using System;

namespace Spine
{
	public class AttachmentTimeline : Timeline
	{
		public AttachmentTimeline(int frameCount)
		{
			this.frames = new float[frameCount];
			this.attachmentNames = new string[frameCount];
		}

		public int SlotIndex
		{
			get
			{
				return this.slotIndex;
			}
			set
			{
				this.slotIndex = value;
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

		public string[] AttachmentNames
		{
			get
			{
				return this.attachmentNames;
			}
			set
			{
				this.attachmentNames = value;
			}
		}

		public int FrameCount
		{
			get
			{
				return this.frames.Length;
			}
		}

		public int PropertyId
		{
			get
			{
				return 67108864 + this.slotIndex;
			}
		}

		public void SetFrame(int frameIndex, float time, string attachmentName)
		{
			this.frames[frameIndex] = time;
			this.attachmentNames[frameIndex] = attachmentName;
		}

		public void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			Slot slot = skeleton.slots.Items[this.slotIndex];
			string text;
			if (direction == MixDirection.Out && pose == MixPose.Setup)
			{
				text = slot.data.attachmentName;
				slot.Attachment = ((text != null) ? skeleton.GetAttachment(this.slotIndex, text) : null);
				return;
			}
			float[] array = this.frames;
			if (time < array[0])
			{
				if (pose == MixPose.Setup)
				{
					text = slot.data.attachmentName;
					slot.Attachment = ((text != null) ? skeleton.GetAttachment(this.slotIndex, text) : null);
				}
				return;
			}
			int num;
			if (time >= array[array.Length - 1])
			{
				num = array.Length - 1;
			}
			else
			{
				num = Animation.BinarySearch(array, time, 1) - 1;
			}
			text = this.attachmentNames[num];
			slot.Attachment = ((text != null) ? skeleton.GetAttachment(this.slotIndex, text) : null);
		}

		internal int slotIndex;

		internal float[] frames;

		internal string[] attachmentNames;
	}
}
