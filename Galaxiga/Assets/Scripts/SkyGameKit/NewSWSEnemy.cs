using System;
using SWS;
using UnityEngine;

namespace SkyGameKit
{
	[RequireComponent(typeof(SgkSplineMove))]
	public class NewSWSEnemy : SWSEnemy
	{
		protected override void StartMove()
		{
			this.m_splineMove.SetPath(this.path);
		}

		[EnemyField]
		public BezierPathManager path;
	}
}
