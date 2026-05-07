using System;

namespace Spine
{
	public class Event
	{
		public Event(float time, EventData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			this.time = time;
			this.data = data;
		}

		public EventData Data
		{
			get
			{
				return this.data;
			}
		}

		public float Time
		{
			get
			{
				return this.time;
			}
		}

		public int Int
		{
			get
			{
				return this.intValue;
			}
			set
			{
				this.intValue = value;
			}
		}

		public float Float
		{
			get
			{
				return this.floatValue;
			}
			set
			{
				this.floatValue = value;
			}
		}

		public string String
		{
			get
			{
				return this.stringValue;
			}
			set
			{
				this.stringValue = value;
			}
		}

		public override string ToString()
		{
			return this.data.Name;
		}

		internal readonly EventData data;

		internal readonly float time;

		internal int intValue;

		internal float floatValue;

		internal string stringValue;
	}
}
