using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Spine
{
	public class AnimationState
	{
		public AnimationState(AnimationStateData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			this.data = data;
			this.queue = new EventQueue(this, delegate()
			{
				this.animationsChanged = true;
			}, this.trackEntryPool);
		}

		public AnimationStateData Data
		{
			get
			{
				return this.data;
			}
		}

		public ExposedList<TrackEntry> Tracks
		{
			get
			{
				return this.tracks;
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

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AnimationState.TrackEntryDelegate Start;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AnimationState.TrackEntryEventDelegate Event;

		public void Update(float delta)
		{
			delta *= this.timeScale;
			TrackEntry[] items = this.tracks.Items;
			int i = 0;
			int count = this.tracks.Count;
			while (i < count)
			{
				TrackEntry trackEntry = items[i];
				if (trackEntry != null)
				{
					trackEntry.animationLast = trackEntry.nextAnimationLast;
					trackEntry.trackLast = trackEntry.nextTrackLast;
					float num = delta * trackEntry.timeScale;
					if (trackEntry.delay > 0f)
					{
						trackEntry.delay -= num;
						if (trackEntry.delay > 0f)
						{
							goto IL_1C9;
						}
						num = -trackEntry.delay;
						trackEntry.delay = 0f;
					}
					TrackEntry trackEntry2 = trackEntry.next;
					if (trackEntry2 != null)
					{
						float num2 = trackEntry.trackLast - trackEntry2.delay;
						if (num2 >= 0f)
						{
							trackEntry2.delay = 0f;
							trackEntry2.trackTime = num2 + delta * trackEntry2.timeScale;
							trackEntry.trackTime += num;
							this.SetCurrent(i, trackEntry2, true);
							while (trackEntry2.mixingFrom != null)
							{
								trackEntry2.mixTime += num;
								trackEntry2 = trackEntry2.mixingFrom;
							}
							goto IL_1C9;
						}
					}
					else if (trackEntry.trackLast >= trackEntry.trackEnd && trackEntry.mixingFrom == null)
					{
						items[i] = null;
						this.queue.End(trackEntry);
						this.DisposeNext(trackEntry);
						goto IL_1C9;
					}
					if (trackEntry.mixingFrom != null && this.UpdateMixingFrom(trackEntry, delta))
					{
						TrackEntry mixingFrom = trackEntry.mixingFrom;
						trackEntry.mixingFrom = null;
						while (mixingFrom != null)
						{
							this.queue.End(mixingFrom);
							mixingFrom = mixingFrom.mixingFrom;
						}
					}
					trackEntry.trackTime += num;
				}
				IL_1C9:
				i++;
			}
			this.queue.Drain();
		}

		private bool UpdateMixingFrom(TrackEntry to, float delta)
		{
			TrackEntry mixingFrom = to.mixingFrom;
			if (mixingFrom == null)
			{
				return true;
			}
			bool result = this.UpdateMixingFrom(mixingFrom, delta);
			mixingFrom.animationLast = mixingFrom.nextAnimationLast;
			mixingFrom.trackLast = mixingFrom.nextTrackLast;
			if (to.mixTime > 0f && (to.mixTime >= to.mixDuration || to.timeScale == 0f))
			{
				if (mixingFrom.totalAlpha == 0f || to.mixDuration == 0f)
				{
					to.mixingFrom = mixingFrom.mixingFrom;
					to.interruptAlpha = mixingFrom.interruptAlpha;
					this.queue.End(mixingFrom);
				}
				return result;
			}
			mixingFrom.trackTime += delta * mixingFrom.timeScale;
			to.mixTime += delta * to.timeScale;
			return false;
		}

		public bool Apply(Skeleton skeleton)
		{
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			if (this.animationsChanged)
			{
				this.AnimationsChanged();
			}
			ExposedList<Event> exposedList = this.events;
			bool result = false;
			TrackEntry[] items = this.tracks.Items;
			int i = 0;
			int count = this.tracks.Count;
			while (i < count)
			{
				TrackEntry trackEntry = items[i];
				if (trackEntry != null && trackEntry.delay <= 0f)
				{
					result = true;
					MixPose mixPose = (i != 0) ? MixPose.CurrentLayered : MixPose.Current;
					float num = trackEntry.alpha;
					if (trackEntry.mixingFrom != null)
					{
						num *= this.ApplyMixingFrom(trackEntry, skeleton, mixPose);
					}
					else if (trackEntry.trackTime >= trackEntry.trackEnd && trackEntry.next == null)
					{
						num = 0f;
					}
					float animationLast = trackEntry.animationLast;
					float animationTime = trackEntry.AnimationTime;
					int count2 = trackEntry.animation.timelines.Count;
					ExposedList<Timeline> timelines = trackEntry.animation.timelines;
					Timeline[] items2 = timelines.Items;
					if (num == 1f)
					{
						for (int j = 0; j < count2; j++)
						{
							items2[j].Apply(skeleton, animationLast, animationTime, exposedList, 1f, MixPose.Setup, MixDirection.In);
						}
					}
					else
					{
						int[] items3 = trackEntry.timelineData.Items;
						bool flag = trackEntry.timelinesRotation.Count == 0;
						if (flag)
						{
							trackEntry.timelinesRotation.EnsureCapacity(timelines.Count << 1);
						}
						float[] items4 = trackEntry.timelinesRotation.Items;
						for (int k = 0; k < count2; k++)
						{
							Timeline timeline = items2[k];
							MixPose pose = (items3[k] < 1) ? mixPose : MixPose.Setup;
							RotateTimeline rotateTimeline = timeline as RotateTimeline;
							if (rotateTimeline != null)
							{
								AnimationState.ApplyRotateTimeline(rotateTimeline, skeleton, animationTime, num, pose, items4, k << 1, flag);
							}
							else
							{
								timeline.Apply(skeleton, animationLast, animationTime, exposedList, num, pose, MixDirection.In);
							}
						}
					}
					this.QueueEvents(trackEntry, animationTime);
					exposedList.Clear(false);
					trackEntry.nextAnimationLast = animationTime;
					trackEntry.nextTrackLast = trackEntry.trackTime;
				}
				i++;
			}
			this.queue.Drain();
			return result;
		}

		private float ApplyMixingFrom(TrackEntry to, Skeleton skeleton, MixPose currentPose)
		{
			TrackEntry mixingFrom = to.mixingFrom;
			if (mixingFrom.mixingFrom != null)
			{
				this.ApplyMixingFrom(mixingFrom, skeleton, currentPose);
			}
			float num;
			if (to.mixDuration == 0f)
			{
				num = 1f;
				currentPose = MixPose.Setup;
			}
			else
			{
				num = to.mixTime / to.mixDuration;
				if (num > 1f)
				{
					num = 1f;
				}
			}
			ExposedList<Event> exposedList = (num >= mixingFrom.eventThreshold) ? null : this.events;
			bool flag = num < mixingFrom.attachmentThreshold;
			bool flag2 = num < mixingFrom.drawOrderThreshold;
			float animationLast = mixingFrom.animationLast;
			float animationTime = mixingFrom.AnimationTime;
			ExposedList<Timeline> timelines = mixingFrom.animation.timelines;
			int count = timelines.Count;
			Timeline[] items = timelines.Items;
			int[] items2 = mixingFrom.timelineData.Items;
			TrackEntry[] items3 = mixingFrom.timelineDipMix.Items;
			bool flag3 = mixingFrom.timelinesRotation.Count == 0;
			if (flag3)
			{
				mixingFrom.timelinesRotation.Resize(timelines.Count << 1);
			}
			float[] items4 = mixingFrom.timelinesRotation.Items;
			float num2 = mixingFrom.alpha * to.interruptAlpha;
			float num3 = num2 * (1f - num);
			mixingFrom.totalAlpha = 0f;
			int i = 0;
			while (i < count)
			{
				Timeline timeline = items[i];
				MixPose pose;
				float num4;
				switch (items2[i])
				{
				case 0:
					if (flag || !(timeline is AttachmentTimeline))
					{
						if (flag2 || !(timeline is DrawOrderTimeline))
						{
							pose = currentPose;
							num4 = num3;
							goto IL_1E3;
						}
					}
					break;
				case 1:
					pose = MixPose.Setup;
					num4 = num3;
					goto IL_1E3;
				case 2:
					pose = MixPose.Setup;
					num4 = num2;
					goto IL_1E3;
				default:
				{
					pose = MixPose.Setup;
					TrackEntry trackEntry = items3[i];
					num4 = num2 * Math.Max(0f, 1f - trackEntry.mixTime / trackEntry.mixDuration);
					goto IL_1E3;
				}
				}
				IL_22F:
				i++;
				continue;
				IL_1E3:
				mixingFrom.totalAlpha += num4;
				RotateTimeline rotateTimeline = timeline as RotateTimeline;
				if (rotateTimeline != null)
				{
					AnimationState.ApplyRotateTimeline(rotateTimeline, skeleton, animationTime, num4, pose, items4, i << 1, flag3);
					goto IL_22F;
				}
				timeline.Apply(skeleton, animationLast, animationTime, exposedList, num4, pose, MixDirection.Out);
				goto IL_22F;
			}
			if (to.mixDuration > 0f)
			{
				this.QueueEvents(mixingFrom, animationTime);
			}
			this.events.Clear(false);
			mixingFrom.nextAnimationLast = animationTime;
			mixingFrom.nextTrackLast = mixingFrom.trackTime;
			return num;
		}

		private static void ApplyRotateTimeline(RotateTimeline rotateTimeline, Skeleton skeleton, float time, float alpha, MixPose pose, float[] timelinesRotation, int i, bool firstFrame)
		{
			if (firstFrame)
			{
				timelinesRotation[i] = 0f;
			}
			if (alpha == 1f)
			{
				rotateTimeline.Apply(skeleton, 0f, time, null, 1f, pose, MixDirection.In);
				return;
			}
			Bone bone = skeleton.bones.Items[rotateTimeline.boneIndex];
			float[] frames = rotateTimeline.frames;
			if (time < frames[0])
			{
				if (pose == MixPose.Setup)
				{
					bone.rotation = bone.data.rotation;
				}
				return;
			}
			float num;
			if (time >= frames[frames.Length - 2])
			{
				num = bone.data.rotation + frames[frames.Length + -1];
			}
			else
			{
				int num2 = Animation.BinarySearch(frames, time, 2);
				float num3 = frames[num2 + -1];
				float num4 = frames[num2];
				float curvePercent = rotateTimeline.GetCurvePercent((num2 >> 1) - 1, 1f - (time - num4) / (frames[num2 + -2] - num4));
				num = frames[num2 + 1] - num3;
				num -= (float)((16384 - (int)(16384.499999999996 - (double)(num / 360f))) * 360);
				num = num3 + num * curvePercent + bone.data.rotation;
				num -= (float)((16384 - (int)(16384.499999999996 - (double)(num / 360f))) * 360);
			}
			float num5 = (pose != MixPose.Setup) ? bone.rotation : bone.data.rotation;
			float num6 = num - num5;
			float num7;
			if (num6 == 0f)
			{
				num7 = timelinesRotation[i];
			}
			else
			{
				num6 -= (float)((16384 - (int)(16384.499999999996 - (double)(num6 / 360f))) * 360);
				float num8;
				float value;
				if (firstFrame)
				{
					num8 = 0f;
					value = num6;
				}
				else
				{
					num8 = timelinesRotation[i];
					value = timelinesRotation[i + 1];
				}
				bool flag = num6 > 0f;
				bool flag2 = num8 >= 0f;
				if (Math.Sign(value) != Math.Sign(num6) && Math.Abs(value) <= 90f)
				{
					if (Math.Abs(num8) > 180f)
					{
						num8 += (float)(360 * Math.Sign(num8));
					}
					flag2 = flag;
				}
				num7 = num6 + num8 - num8 % 360f;
				if (flag2 != flag)
				{
					num7 += (float)(360 * Math.Sign(num8));
				}
				timelinesRotation[i] = num7;
			}
			timelinesRotation[i + 1] = num6;
			num5 += num7 * alpha;
			bone.rotation = num5 - (float)((16384 - (int)(16384.499999999996 - (double)(num5 / 360f))) * 360);
		}

		private void QueueEvents(TrackEntry entry, float animationTime)
		{
			float animationStart = entry.animationStart;
			float animationEnd = entry.animationEnd;
			float num = animationEnd - animationStart;
			float num2 = entry.trackLast % num;
			ExposedList<Event> exposedList = this.events;
			Event[] items = exposedList.Items;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				Event @event = items[i];
				if (@event.time < num2)
				{
					break;
				}
				if (@event.time <= animationEnd)
				{
					this.queue.Event(entry, @event);
				}
				i++;
			}
			bool flag;
			if (entry.loop)
			{
				flag = (num == 0f || num2 > entry.trackTime % num);
			}
			else
			{
				flag = (animationTime >= animationEnd && entry.animationLast < animationEnd);
			}
			if (flag)
			{
				this.queue.Complete(entry);
			}
			while (i < count)
			{
				Event event2 = items[i];
				if (event2.time >= animationStart)
				{
					this.queue.Event(entry, items[i]);
				}
				i++;
			}
		}

		public void ClearTracks()
		{
			bool drainDisabled = this.queue.drainDisabled;
			this.queue.drainDisabled = true;
			int i = 0;
			int count = this.tracks.Count;
			while (i < count)
			{
				this.ClearTrack(i);
				i++;
			}
			this.tracks.Clear(true);
			this.queue.drainDisabled = drainDisabled;
			this.queue.Drain();
		}

		public void ClearTrack(int trackIndex)
		{
			if (trackIndex >= this.tracks.Count)
			{
				return;
			}
			TrackEntry trackEntry = this.tracks.Items[trackIndex];
			if (trackEntry == null)
			{
				return;
			}
			this.queue.End(trackEntry);
			this.DisposeNext(trackEntry);
			TrackEntry trackEntry2 = trackEntry;
			for (;;)
			{
				TrackEntry mixingFrom = trackEntry2.mixingFrom;
				if (mixingFrom == null)
				{
					break;
				}
				this.queue.End(mixingFrom);
				trackEntry2.mixingFrom = null;
				trackEntry2 = mixingFrom;
			}
			this.tracks.Items[trackEntry.trackIndex] = null;
			this.queue.Drain();
		}

		private void SetCurrent(int index, TrackEntry current, bool interrupt)
		{
			TrackEntry trackEntry = this.ExpandToIndex(index);
			this.tracks.Items[index] = current;
			if (trackEntry != null)
			{
				if (interrupt)
				{
					this.queue.Interrupt(trackEntry);
				}
				current.mixingFrom = trackEntry;
				current.mixTime = 0f;
				if (trackEntry.mixingFrom != null && trackEntry.mixDuration > 0f)
				{
					current.interruptAlpha *= Math.Min(1f, trackEntry.mixTime / trackEntry.mixDuration);
				}
				trackEntry.timelinesRotation.Clear(true);
			}
			this.queue.Start(current);
		}

		public TrackEntry SetAnimation(int trackIndex, string animationName, bool loop)
		{
			Animation animation = this.data.skeletonData.FindAnimation(animationName);
			if (animation == null)
			{
				throw new ArgumentException("Animation not found: " + animationName, "animationName");
			}
			return this.SetAnimation(trackIndex, animation, loop);
		}

		public TrackEntry SetAnimation(int trackIndex, Animation animation, bool loop)
		{
			if (animation == null)
			{
				throw new ArgumentNullException("animation", "animation cannot be null.");
			}
			bool interrupt = true;
			TrackEntry trackEntry = this.ExpandToIndex(trackIndex);
			if (trackEntry != null)
			{
				if (trackEntry.nextTrackLast == -1f)
				{
					this.tracks.Items[trackIndex] = trackEntry.mixingFrom;
					this.queue.Interrupt(trackEntry);
					this.queue.End(trackEntry);
					this.DisposeNext(trackEntry);
					trackEntry = trackEntry.mixingFrom;
					interrupt = false;
				}
				else
				{
					this.DisposeNext(trackEntry);
				}
			}
			TrackEntry trackEntry2 = this.NewTrackEntry(trackIndex, animation, loop, trackEntry);
			this.SetCurrent(trackIndex, trackEntry2, interrupt);
			this.queue.Drain();
			return trackEntry2;
		}

		public TrackEntry AddAnimation(int trackIndex, string animationName, bool loop, float delay)
		{
			Animation animation = this.data.skeletonData.FindAnimation(animationName);
			if (animation == null)
			{
				throw new ArgumentException("Animation not found: " + animationName, "animationName");
			}
			return this.AddAnimation(trackIndex, animation, loop, delay);
		}

		public TrackEntry AddAnimation(int trackIndex, Animation animation, bool loop, float delay)
		{
			if (animation == null)
			{
				throw new ArgumentNullException("animation", "animation cannot be null.");
			}
			TrackEntry trackEntry = this.ExpandToIndex(trackIndex);
			if (trackEntry != null)
			{
				while (trackEntry.next != null)
				{
					trackEntry = trackEntry.next;
				}
			}
			TrackEntry trackEntry2 = this.NewTrackEntry(trackIndex, animation, loop, trackEntry);
			if (trackEntry == null)
			{
				this.SetCurrent(trackIndex, trackEntry2, true);
				this.queue.Drain();
			}
			else
			{
				trackEntry.next = trackEntry2;
				if (delay <= 0f)
				{
					float num = trackEntry.animationEnd - trackEntry.animationStart;
					if (num != 0f)
					{
						if (trackEntry.loop)
						{
							delay += num * (float)(1 + (int)(trackEntry.trackTime / num));
						}
						else
						{
							delay += num;
						}
						delay -= this.data.GetMix(trackEntry.animation, animation);
					}
					else
					{
						delay = 0f;
					}
				}
			}
			trackEntry2.delay = delay;
			return trackEntry2;
		}

		public TrackEntry SetEmptyAnimation(int trackIndex, float mixDuration)
		{
			TrackEntry trackEntry = this.SetAnimation(trackIndex, AnimationState.EmptyAnimation, false);
			trackEntry.mixDuration = mixDuration;
			trackEntry.trackEnd = mixDuration;
			return trackEntry;
		}

		public TrackEntry AddEmptyAnimation(int trackIndex, float mixDuration, float delay)
		{
			if (delay <= 0f)
			{
				delay -= mixDuration;
			}
			TrackEntry trackEntry = this.AddAnimation(trackIndex, AnimationState.EmptyAnimation, false, delay);
			trackEntry.mixDuration = mixDuration;
			trackEntry.trackEnd = mixDuration;
			return trackEntry;
		}

		public void SetEmptyAnimations(float mixDuration)
		{
			bool drainDisabled = this.queue.drainDisabled;
			this.queue.drainDisabled = true;
			int i = 0;
			int count = this.tracks.Count;
			while (i < count)
			{
				TrackEntry trackEntry = this.tracks.Items[i];
				if (trackEntry != null)
				{
					this.SetEmptyAnimation(i, mixDuration);
				}
				i++;
			}
			this.queue.drainDisabled = drainDisabled;
			this.queue.Drain();
		}

		private TrackEntry ExpandToIndex(int index)
		{
			if (index < this.tracks.Count)
			{
				return this.tracks.Items[index];
			}
			while (index >= this.tracks.Count)
			{
				this.tracks.Add(null);
			}
			return null;
		}

		private TrackEntry NewTrackEntry(int trackIndex, Animation animation, bool loop, TrackEntry last)
		{
			TrackEntry trackEntry = this.trackEntryPool.Obtain();
			trackEntry.trackIndex = trackIndex;
			trackEntry.animation = animation;
			trackEntry.loop = loop;
			trackEntry.eventThreshold = 0f;
			trackEntry.attachmentThreshold = 0f;
			trackEntry.drawOrderThreshold = 0f;
			trackEntry.animationStart = 0f;
			trackEntry.animationEnd = animation.Duration;
			trackEntry.animationLast = -1f;
			trackEntry.nextAnimationLast = -1f;
			trackEntry.delay = 0f;
			trackEntry.trackTime = 0f;
			trackEntry.trackLast = -1f;
			trackEntry.nextTrackLast = -1f;
			trackEntry.trackEnd = float.MaxValue;
			trackEntry.timeScale = 1f;
			trackEntry.alpha = 1f;
			trackEntry.interruptAlpha = 1f;
			trackEntry.mixTime = 0f;
			trackEntry.mixDuration = ((last != null) ? this.data.GetMix(last.animation, animation) : 0f);
			return trackEntry;
		}

		private void DisposeNext(TrackEntry entry)
		{
			for (TrackEntry next = entry.next; next != null; next = next.next)
			{
				this.queue.Dispose(next);
			}
			entry.next = null;
		}

		private void AnimationsChanged()
		{
			this.animationsChanged = false;
			HashSet<int> hashSet = this.propertyIDs;
			hashSet.Clear();
			ExposedList<TrackEntry> mixingToArray = this.mixingTo;
			TrackEntry[] items = this.tracks.Items;
			int i = 0;
			int count = this.tracks.Count;
			while (i < count)
			{
				TrackEntry trackEntry = items[i];
				if (trackEntry != null)
				{
					trackEntry.SetTimelineData(null, mixingToArray, hashSet);
				}
				i++;
			}
		}

		public TrackEntry GetCurrent(int trackIndex)
		{
			return (trackIndex < this.tracks.Count) ? this.tracks.Items[trackIndex] : null;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			int count = this.tracks.Count;
			while (i < count)
			{
				TrackEntry trackEntry = this.tracks.Items[i];
				if (trackEntry != null)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(trackEntry.ToString());
				}
				i++;
			}
			return (stringBuilder.Length != 0) ? stringBuilder.ToString() : "<none>";
		}

		internal void OnStart(TrackEntry entry)
		{
			if (this.Start != null)
			{
				this.Start(entry);
			}
		}

		internal void OnInterrupt(TrackEntry entry)
		{
			if (this.Interrupt != null)
			{
				this.Interrupt(entry);
			}
		}

		internal void OnEnd(TrackEntry entry)
		{
			if (this.End != null)
			{
				this.End(entry);
			}
		}

		internal void OnDispose(TrackEntry entry)
		{
			if (this.Dispose != null)
			{
				this.Dispose(entry);
			}
		}

		internal void OnComplete(TrackEntry entry)
		{
			if (this.Complete != null)
			{
				this.Complete(entry);
			}
		}

		internal void OnEvent(TrackEntry entry, Event e)
		{
			if (this.Event != null)
			{
				this.Event(entry, e);
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AnimationState.TrackEntryDelegate Interrupt;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AnimationState.TrackEntryDelegate End;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AnimationState.TrackEntryDelegate Dispose;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event AnimationState.TrackEntryDelegate Complete;

		private static readonly Animation EmptyAnimation = new Animation("<empty>", new ExposedList<Timeline>(), 0f);

		internal const int Subsequent = 0;

		internal const int First = 1;

		internal const int Dip = 2;

		internal const int DipMix = 3;

		private AnimationStateData data;

		private Pool<TrackEntry> trackEntryPool = new Pool<TrackEntry>(16, int.MaxValue);

		private readonly ExposedList<TrackEntry> tracks = new ExposedList<TrackEntry>();

		private readonly ExposedList<Event> events = new ExposedList<Event>();

		private readonly EventQueue queue;

		private readonly HashSet<int> propertyIDs = new HashSet<int>();

		private readonly ExposedList<TrackEntry> mixingTo = new ExposedList<TrackEntry>();

		private bool animationsChanged;

		private float timeScale = 1f;

		public delegate void TrackEntryDelegate(TrackEntry trackEntry);

		public delegate void TrackEntryEventDelegate(TrackEntry trackEntry, Event e);
	}
}
