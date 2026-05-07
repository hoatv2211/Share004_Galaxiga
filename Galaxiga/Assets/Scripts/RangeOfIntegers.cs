using System;
using UnityEngine;

namespace DigitalRuby.ThunderAndLightning
{
	[Serializable]
	public struct RangeOfIntegers
	{
		public int Random()
		{
			return UnityEngine.Random.Range(this.Minimum, this.Maximum + 1);
		}

		public int Random(System.Random r)
		{
			return r.Next(this.Minimum, this.Maximum + 1);
		}

		[Tooltip("Minimum value (inclusive)")]
		public int Minimum;

		[Tooltip("Maximum value (inclusive)")]
		public int Maximum;
	}
}
