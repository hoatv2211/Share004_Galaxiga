using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBoltPathScript : LightningBoltPathScriptBase
	{
		public override void CreateLightningBolt(LightningBoltParameters parameters)
		{
			Vector3? vector = null;
			List<GameObject> currentPathObjects = base.GetCurrentPathObjects();
			if (currentPathObjects.Count < 2)
			{
				return;
			}
			if (this.nextIndex >= currentPathObjects.Count)
			{
				if (!this.Repeat)
				{
					return;
				}
				if (currentPathObjects[currentPathObjects.Count - 1] == currentPathObjects[0])
				{
					this.nextIndex = 1;
				}
				else
				{
					this.nextIndex = 0;
					this.lastPoint = null;
				}
			}
			try
			{
				vector = this.lastPoint;
				if (vector == null)
				{
					this.lastPoint = new Vector3?(currentPathObjects[this.nextIndex++].transform.position);
				}
				Vector3? vector2 = new Vector3?(currentPathObjects[this.nextIndex].transform.position);
				Vector3? vector3 = this.lastPoint;
				if (vector3 != null && vector2 != null)
				{
					parameters.Start = this.lastPoint.Value;
					parameters.End = vector2.Value;
					base.CreateLightningBolt(parameters);
					if ((this.nextInterval -= this.Speed) <= 0f)
					{
						float num = UnityEngine.Random.Range(this.SpeedIntervalRange.Minimum, this.SpeedIntervalRange.Maximum);
						this.nextInterval = num + this.nextInterval;
						this.lastPoint = vector2;
						this.nextIndex++;
					}
				}
			}
			catch (NullReferenceException)
			{
			}
		}

		public void Reset()
		{
			this.lastPoint = null;
			this.nextIndex = 0;
			this.nextInterval = 1f;
		}

		[Tooltip("How fast the lightning moves through the points or objects. 1 is normal speed, 0.01 is slower, so the lightning will move slowly between the points or objects.")]
		[Range(0.01f, 1f)]
		public float Speed = 1f;

		[SingleLineClamp("When each new point is moved to, this can provide a random value to make the movement to the next point appear more staggered or random. Leave as 1 and 1 to have constant speed. Use a higher maximum to create more randomness.", 1.0, 500.0)]
		public RangeOfFloats SpeedIntervalRange = new RangeOfFloats
		{
			Minimum = 1f,
			Maximum = 1f
		};

		[Tooltip("Repeat when the path completes?")]
		public bool Repeat = true;

		private float nextInterval = 1f;

		private int nextIndex;

		private Vector3? lastPoint;
	}
}
