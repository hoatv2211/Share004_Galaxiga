using System;
using UnityEngine;

namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBoltPrefabScript : LightningBoltPrefabScriptBase
	{
		public override void CreateLightningBolt(LightningBoltParameters parameters)
		{
			parameters.Start = ((!(this.Source == null)) ? this.Source.transform.position : parameters.Start);
			parameters.End = ((!(this.Destination == null)) ? this.Destination.transform.position : parameters.End);
			parameters.StartVariance = this.StartVariance;
			parameters.EndVariance = this.EndVariance;
			base.CreateLightningBolt(parameters);
		}

		[Header("Start/end")]
		[Tooltip("The source game object, can be null")]
		public GameObject Source;

		[Tooltip("The destination game object, can be null")]
		public GameObject Destination;

		[Tooltip("X, Y and Z for variance from the start point. Use positive values.")]
		public Vector3 StartVariance;

		[Tooltip("X, Y and Z for variance from the end point. Use positive values.")]
		public Vector3 EndVariance;
	}
}
