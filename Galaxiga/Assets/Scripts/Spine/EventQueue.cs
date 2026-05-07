using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Spine
{
	internal class EventQueue
	{
		internal EventQueue(AnimationState state, Action HandleAnimationsChanged, Pool<TrackEntry> trackEntryPool)
		{
			this.state = state;
			this.AnimationsChanged += HandleAnimationsChanged;
			this.trackEntryPool = trackEntryPool;
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action AnimationsChanged;

		internal void Start(TrackEntry entry)
		{
			this.eventQueueEntries.Add(new EventQueue.EventQueueEntry(EventQueue.EventType.Start, entry, null));
			if (this.AnimationsChanged != null)
			{
				this.AnimationsChanged();
			}
		}

		internal void Interrupt(TrackEntry entry)
		{
			this.eventQueueEntries.Add(new EventQueue.EventQueueEntry(EventQueue.EventType.Interrupt, entry, null));
		}

		internal void End(TrackEntry entry)
		{
			this.eventQueueEntries.Add(new EventQueue.EventQueueEntry(EventQueue.EventType.End, entry, null));
			if (this.AnimationsChanged != null)
			{
				this.AnimationsChanged();
			}
		}

		internal void Dispose(TrackEntry entry)
		{
			this.eventQueueEntries.Add(new EventQueue.EventQueueEntry(EventQueue.EventType.Dispose, entry, null));
		}

		internal void Complete(TrackEntry entry)
		{
			this.eventQueueEntries.Add(new EventQueue.EventQueueEntry(EventQueue.EventType.Complete, entry, null));
		}

		internal void Event(TrackEntry entry, Event e)
		{
			this.eventQueueEntries.Add(new EventQueue.EventQueueEntry(EventQueue.EventType.Event, entry, e));
		}

		internal void Drain()
		{
			if (this.drainDisabled)
			{
				return;
			}
			this.drainDisabled = true;
			List<EventQueue.EventQueueEntry> list = this.eventQueueEntries;
			AnimationState animationState = this.state;
			int i = 0;
			while (i < list.Count)
			{
				EventQueue.EventQueueEntry eventQueueEntry = list[i];
				TrackEntry entry = eventQueueEntry.entry;
				switch (eventQueueEntry.type)
				{
				case EventQueue.EventType.Start:
					entry.OnStart();
					animationState.OnStart(entry);
					break;
				case EventQueue.EventType.Interrupt:
					entry.OnInterrupt();
					animationState.OnInterrupt(entry);
					break;
				case EventQueue.EventType.End:
					entry.OnEnd();
					animationState.OnEnd(entry);
					goto IL_A2;
				case EventQueue.EventType.Dispose:
					goto IL_A2;
				case EventQueue.EventType.Complete:
					entry.OnComplete();
					animationState.OnComplete(entry);
					break;
				case EventQueue.EventType.Event:
					entry.OnEvent(eventQueueEntry.e);
					animationState.OnEvent(entry, eventQueueEntry.e);
					break;
				}
				IL_F9:
				i++;
				continue;
				IL_A2:
				entry.OnDispose();
				animationState.OnDispose(entry);
				this.trackEntryPool.Free(entry);
				goto IL_F9;
			}
			this.eventQueueEntries.Clear();
			this.drainDisabled = false;
		}

		internal void Clear()
		{
			this.eventQueueEntries.Clear();
		}

		private readonly List<EventQueue.EventQueueEntry> eventQueueEntries = new List<EventQueue.EventQueueEntry>();

		internal bool drainDisabled;

		private readonly AnimationState state;

		private readonly Pool<TrackEntry> trackEntryPool;

		private struct EventQueueEntry
		{
			public EventQueueEntry(EventQueue.EventType eventType, TrackEntry trackEntry, Event e = null)
			{
				this.type = eventType;
				this.entry = trackEntry;
				this.e = e;
			}

			public EventQueue.EventType type;

			public TrackEntry entry;

			public Event e;
		}

		private enum EventType
		{
			Start,
			Interrupt,
			End,
			Dispose,
			Complete,
			Event
		}
	}
}
