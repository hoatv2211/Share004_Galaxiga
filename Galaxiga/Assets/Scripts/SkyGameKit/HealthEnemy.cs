using System;
using UnityEngine;

namespace SkyGameKit
{
	public class HealthEnemy : MonoBehaviour
	{
		private void Start()
		{
			this.m_BaseEnemy.CurrentHP = this.m_BaseEnemy.startHP;
		}

		public void TakeDamage(int health)
		{
		}

		public BaseEnemy m_BaseEnemy;
	}
}
