using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace com.ootii.Utilities
{
	public class Profiler
	{
		public Profiler(string rTag)
		{
			this.Tag = rTag;
			this.mTicksPerMillisecond = 10000f;
			this.mMinTime = 2.14748365E+09f;
			this.mMaxTime = -2.14748365E+09f;
		}

		public Profiler(string rTag, string rSpacing)
		{
			this.Tag = rTag;
			this.mSpacing = rSpacing;
			this.mTicksPerMillisecond = 10000f;
			this.mMinTime = 2.14748365E+09f;
			this.mMaxTime = -2.14748365E+09f;
		}

		public void Reset()
		{
			this.mCount = 0;
			this.mRunTime = 0f;
			this.mTotalTime = 0f;
			this.mMinTime = 0f;
			this.mMaxTime = 0f;
		}

		public float AverageTime
		{
			get
			{
				if (this.mCount == 0)
				{
					return 0f;
				}
				return this.mTotalTime / (float)this.mCount;
			}
		}

		public float MinTime
		{
			get
			{
				return this.mMinTime;
			}
		}

		public float MaxTime
		{
			get
			{
				return this.mMaxTime;
			}
		}

		public float TotalTime
		{
			get
			{
				return this.mTotalTime;
			}
		}

		public float Time
		{
			get
			{
				return this.mRunTime;
			}
		}

		public float ElapsedTime
		{
			get
			{
				if (this.mTimer.IsRunning)
				{
					return (float)this.mTimer.ElapsedTicks / this.mTicksPerMillisecond;
				}
				return this.mRunTime;
			}
		}

		public int Count
		{
			get
			{
				return this.mCount;
			}
		}

		public void Start()
		{
			this.mTimer.Reset();
			this.mTimer.Start();
		}

		public float Stop()
		{
			this.mTimer.Stop();
			this.mRunTime = (float)this.mTimer.ElapsedTicks / this.mTicksPerMillisecond;
			this.mTotalTime += this.mRunTime;
			if (this.mMinTime == 0f || this.mRunTime < this.mMinTime)
			{
				this.mMinTime = this.mRunTime;
			}
			if (this.mMaxTime == 0f || this.mRunTime > this.mMaxTime)
			{
				this.mMaxTime = this.mRunTime;
			}
			this.mCount++;
			return this.mRunTime;
		}

		public override string ToString()
		{
			return string.Format("{0} {1} - time:{2:f4}ms cnt:{3} avg:{4:f4}ms min:{5:f4}ms max:{6:f4}ms", new object[]
			{
				this.mSpacing,
				this.Tag,
				this.mRunTime,
				this.mCount,
				this.AverageTime,
				this.mMinTime,
				this.mMaxTime
			});
		}

		public static Profiler Start(string rProfiler)
		{
			if (!Profiler.sProfilers.ContainsKey(rProfiler))
			{
				Profiler.sProfilers.Add(rProfiler, new Profiler(rProfiler, string.Empty));
			}
			Profiler.sProfilers[rProfiler].Start();
			return Profiler.sProfilers[rProfiler];
		}

		public static Profiler Start(string rProfiler, string rSpacing)
		{
			if (!Profiler.sProfilers.ContainsKey(rProfiler))
			{
				Profiler.sProfilers.Add(rProfiler, new Profiler(rProfiler, rSpacing));
			}
			Profiler.sProfilers[rProfiler].Start();
			return Profiler.sProfilers[rProfiler];
		}

		public static float Stop(string rProfiler)
		{
			if (!Profiler.sProfilers.ContainsKey(rProfiler))
			{
				return 0f;
			}
			return Profiler.sProfilers[rProfiler].Stop();
		}

		public static float ProfilerTime(string rProfiler)
		{
			if (!Profiler.sProfilers.ContainsKey(rProfiler))
			{
				return 0f;
			}
			return Profiler.sProfilers[rProfiler].ElapsedTime;
		}

		public static string ToString(string rProfiler)
		{
			if (rProfiler.Length == 0)
			{
				float num = 0f;
				float num2 = 0f;
				foreach (Profiler profiler in Profiler.sProfilers.Values)
				{
					num += profiler.Time;
					num2 += profiler.AverageTime;
				}
				string text = string.Format("Profiles - Time:{0:f4}ms Avg:{1:f4}ms\r\n", num, num2);
				foreach (Profiler profiler2 in Profiler.sProfilers.Values)
				{
					text += string.Format("{0} Prc:{1:f3} AvgPrc:{2:f3}\r\n", profiler2.ToString(), profiler2.Time / num, profiler2.AverageTime / num2);
				}
				return text;
			}
			if (!Profiler.sProfilers.ContainsKey(rProfiler))
			{
				return string.Empty;
			}
			return Profiler.sProfilers[rProfiler].ToString();
		}

		public static void ScreenWrite(string rProfiler, int rLine)
		{
			if (rProfiler.Length == 0)
			{
				float num = 0f;
				float num2 = 0f;
				foreach (Profiler profiler in Profiler.sProfilers.Values)
				{
					num += profiler.Time;
					num2 += profiler.AverageTime;
				}
				int num3 = 0;
				foreach (Profiler profiler2 in Profiler.sProfilers.Values)
				{
					num3++;
				}
			}
			else if (!Profiler.sProfilers.ContainsKey(rProfiler))
			{
				return;
			}
		}

		public string Tag = string.Empty;

		private string mSpacing = string.Empty;

		private int mCount;

		private float mRunTime;

		private float mTotalTime;

		private float mMinTime;

		private float mMaxTime;

		private Stopwatch mTimer = new Stopwatch();

		private float mTicksPerMillisecond;

		private static Dictionary<string, Profiler> sProfilers = new Dictionary<string, Profiler>();
	}
}
