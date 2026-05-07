using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Spine
{
	public class TrackEntry : Pool<TrackEntry>.IPoolable
	{
		public void Reset()
		{
			this.next = null;
			this.mixingFrom = null;
			this.animation = null;
			this.timelineData.Clear(true);
			this.timelineDipMix.Clear(true);
			this.timelinesRotation.Clear(true);
			this.Start = null;
			this.Interrupt = null;
			this.End = null;
			this.Dispose = null;
			this.Complete = null;
			this.Event = null;
		}

		internal TrackEntry SetTimelineData(TrackEntry to, ExposedList<TrackEntry> mixingToArray, HashSet<int> propertyIDs)
		{
			if (to != null)
			{
				mixingToArray.Add(to);
			}
			TrackEntry result = (this.mixingFrom == null) ? this : this.mixingFrom.SetTimelineData(this, mixingToArray, propertyIDs);
			if (to != null)
			{
				mixingToArray.Pop();
			}
			TrackEntry[] items = mixingToArray.Items;
			int num = mixingToArray.Count - 1;
			Timeline[] items2 = this.animation.timelines.Items;
			int count = this.animation.timelines.Count;
			int[] items3 = this.timelineData.Resize(count).Items;
			this.timelineDipMix.Clear(true);
			TrackEntry[] items4 = this.timelineDipMix.Resize(count).Items;
			for (int i = 0; i < count; i++)
			{
				int propertyId = items2[i].PropertyId;
				if (!propertyIDs.Add(propertyId))
				{
					items3[i] = 0;
				}
				else if (to == null || !to.HasTimeline(propertyId))
				{
					items3[i] = 1;
				}
				else
				{
					int j = num;
					while (j >= 0)
					{
						TrackEntry trackEntry = items[j];
						if (!trackEntry.HasTimeline(propertyId))
						{
							if (trackEntry.mixDuration > 0f)
							{
								items3[i] = 3;
								items4[i] = trackEntry;
								goto IL_142;
							}
							break;
						}
						else
						{
							j--;
						}
					}
					items3[i] = 2;
				}
				IL_142:;
			}
			return result;
		}

		private bool HasTimeline(int id)
		{
			Timeline[] items = this.animation.timelines.Items;
			int i = 0;
			int count = this.animation.timelines.Count;
			while (i < count)
			{
				if (items[i].PropertyId == id)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		public int TrackIndex
		{
			get
			{
				return this.trackIndex;
			}
		}

		public Animation Animation
		{
			get
			{
				return this.animation;
			}
		}

		public bool Loop
		{
			get
			{
				return this.loop;
			}
			set
			{
				this.loop = value;
			}
		}

		public float Delay
		{
			get
			{
				return this.delay;
			}
			set
			{
				this.delay = value;
			}
		}

		public float TrackTime
		{
			get
			{
				return this.trackTime;
			}
			set
			{
				this.trackTime = value;
			}
		}

		public float TrackEnd
		{
			get
			{
				return this.trackEnd;
			}
			set
			{
				this.trackEnd = value;
			}
		}

		public float AnimationStart
		{
			get
			{
				return this.animationStart;
			}
			set
			{
				this.animationStart = value;
			}
		}

		public float AnimationEnd
		{
			get
			{
				return this.animationEnd;
			}
			set
			{
				this.animationEnd = value;
			}
		}

		public float AnimationLast
		{
			get
			{
				return this.animationLast;
			}
			set
			{
				this.animationLast = value;
				this.nextAnimationLast = value;
			}
		}

		public float AnimationTime
		{
			get
			{
				if (!this.loop)
				{
					return Math.Min(this.trackTime + this.animationStart, this.animationEnd);
				}
				float num = this.animationEnd - this.animationStart;
				if (num == 0f)
				{
					return this.animationStart;
				}
				return this.trackTime % num + this.animationStart;
			}
		}

		public float TimeScale
		{
			get
			{
				return this.timeScale;
			}
			set
			{
				this.timeScale = value;
			}
		}

		public float Alpha
		{
			get
			{
				return this.alpha;
			}
			set
			{
				this.alpha = value;
			}
		}

		public float EventThreshold
		{
			get
			{
				return this.eventThreshold;
			}
			set
			{
				this.eventThreshold = value;
			}
		}

		public float AttachmentThreshold
		{
			get
			{
				return this.attachmentThreshold;
			}
			set
			{
				this.attachmentThreshold = value;
			}
		}

		public float DrawOrderThreshold
		{
			get
			{
				return this.drawOrderThreshold;
			}
			set
			{
				this.drawOrderThreshold = value;
			}
		}

		public TrackEntry Next
		{
			get
			{
				return this.next;
			}
		}

		public bool IsComplete
		{
			get
			{
				return this.trackTime >= this.animationEnd - this.animationStart;
			}
		}

		public float MixTime
		{
			get
			{
				return this.mixTime;
			}
			set
			{
				this.mixTime = value;
			}
		}

		public float MixDuration
		{
			get
			{
				return this.mixDuration;
			}
			set
			{
				this.mixDuration = value;
			}
		}

		public TrackEntry MixingFrom
		{
			get
			{
				return this.mixingFrom;
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AnimationState.TrackEntryDelegate Start;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AnimationState.TrackEntryEventDelegate Event;

		internal void OnStart()
		{
			if (this.Start != null)
			{
				this.Start(this);
			}
		}

		internal void OnInterrupt()
		{
			if (this.Interrupt != null)
			{
				this.Interrupt(this);
			}
		}

		internal void OnEnd()
		{
			if (this.End != null)
			{
				this.End(this);
			}
		}

		internal void OnDispose()
		{
			if (this.Dispose != null)
			{
				this.Dispose(this);
			}
		}

		internal void OnComplete()
		{
			if (this.Complete != null)
			{
				this.Complete(this);
			}
		}

		internal void OnEvent(Event e)
		{
			if (this.Event != null)
			{
				this.Event(this, e);
			}
		}

		public void ResetRotationDirections()
		{
			this.timelinesRotation.Clear(true);
		}

		public override string ToString()
		{
			return (this.animation != null) ? this.animation.name : "<none>";
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AnimationState.TrackEntryDelegate Interrupt;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AnimationState.TrackEntryDelegate End;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AnimationState.TrackEntryDelegate Dispose;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AnimationState.TrackEntryDelegate Complete;

		internal Animation animation;

		internal TrackEntry next;

		internal TrackEntry mixingFrom;

		internal int trackIndex;

		internal bool loop;

		internal float eventThreshold;

		internal float attachmentThreshold;

		internal float drawOrderThreshold;

		internal float animationStart;

		internal float animationEnd;

		internal float animationLast;

		internal float nextAnimationLast;

		internal float delay;

		internal float trackTime;

		internal float trackLast;

		internal float nextTrackLast;

		internal float trackEnd;

		internal float timeScale = 1f;

		internal float alpha;

		internal float mixTime;

		internal float mixDuration;

		internal float interruptAlpha;

		internal float totalAlpha;

		internal readonly ExposedList<int> timelineData = new ExposedList<int>();

		internal readonly ExposedList<TrackEntry> timelineDipMix = new ExposedList<TrackEntry>();

		internal readonly ExposedList<float> timelinesRotation = new ExposedList<float>();
	}
}
