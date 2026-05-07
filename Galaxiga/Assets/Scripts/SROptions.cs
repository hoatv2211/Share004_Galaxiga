using System;
using System.Diagnostics;

public class SROptions
{
	public static SROptions Current
	{
		get
		{
			return SROptions._current;
		}
	}

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event SROptionsPropertyChanged PropertyChanged;

	public void OnPropertyChanged(string propertyName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, propertyName);
		}
	}

	private static readonly SROptions _current = new SROptions();

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NumberRangeAttribute : Attribute
	{
		public NumberRangeAttribute(double min, double max)
		{
			this.Min = min;
			this.Max = max;
		}

		public readonly double Max;

		public readonly double Min;
	}

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IncrementAttribute : Attribute
	{
		public IncrementAttribute(double increment)
		{
			this.Increment = increment;
		}

		public readonly double Increment;
	}

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class SortAttribute : Attribute
	{
		public SortAttribute(int priority)
		{
			this.SortPriority = priority;
		}

		public readonly int SortPriority;
	}

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class DisplayNameAttribute : Attribute
	{
		public DisplayNameAttribute(string name)
		{
			this.Name = name;
		}

		public readonly string Name;
	}
}
