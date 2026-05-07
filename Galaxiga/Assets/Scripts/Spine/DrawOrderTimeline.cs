using System;

namespace Spine
{
	public class DrawOrderTimeline : Timeline
	{
		public DrawOrderTimeline(int frameCount)
		{
			this.frames = new float[frameCount];
			this.drawOrders = new int[frameCount][];
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

		public int[][] DrawOrders
		{
			get
			{
				return this.drawOrders;
			}
			set
			{
				this.drawOrders = value;
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
				return 134217728;
			}
		}

		public void SetFrame(int frameIndex, float time, int[] drawOrder)
		{
			this.frames[frameIndex] = time;
			this.drawOrders[frameIndex] = drawOrder;
		}

		public void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			ExposedList<Slot> drawOrder = skeleton.drawOrder;
			ExposedList<Slot> slots = skeleton.slots;
			if (direction == MixDirection.Out && pose == MixPose.Setup)
			{
				Array.Copy(slots.Items, 0, drawOrder.Items, 0, slots.Count);
				return;
			}
			float[] array = this.frames;
			if (time < array[0])
			{
				if (pose == MixPose.Setup)
				{
					Array.Copy(slots.Items, 0, drawOrder.Items, 0, slots.Count);
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
				num = Animation.BinarySearch(array, time) - 1;
			}
			int[] array2 = this.drawOrders[num];
			if (array2 == null)
			{
				drawOrder.Clear(true);
				int i = 0;
				int count = slots.Count;
				while (i < count)
				{
					drawOrder.Add(slots.Items[i]);
					i++;
				}
			}
			else
			{
				Slot[] items = drawOrder.Items;
				Slot[] items2 = slots.Items;
				int j = 0;
				int num2 = array2.Length;
				while (j < num2)
				{
					items[j] = items2[array2[j]];
					j++;
				}
			}
		}

		internal float[] frames;

		private int[][] drawOrders;
	}
}
