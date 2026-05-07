using System;
using UnityEngine;

namespace SkyGameKit.Demo
{
	public class PlayerLaserDemo : ReflectiveLaser
	{
		protected override void OnHit(LaserHitInfo target)
		{
			base.OnHit(target);
			target.collider.GetComponent<EnemyHit>().m_BaseEnemy.CurrentHP -= Mathf.CeilToInt((float)this.power * target.deltaTime);
		}

		public int power;
	}
}
