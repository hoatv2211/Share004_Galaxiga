using System;
using UnityEngine;

namespace SkyGameKit
{
	public class EnemyHit : MonoBehaviour
	{
		protected virtual void Awake()
		{
			if (this.m_BaseEnemy == null)
			{
				this.m_BaseEnemy = base.GetComponent<BaseEnemy>();
			}
		}

		protected virtual void OnTriggerEnter2D(Collider2D col)
		{
			string tag = col.tag;
			if (tag != null)
			{
				if (!(tag == "DeadZone"))
				{
					if (tag == "PlayerBullet")
					{
						SgkBullet component = col.GetComponent<SgkBullet>();
						if (component.spreadDamage)
						{
							this.m_BaseEnemy.CurrentHP -= component.power;
						}
						else if (component.gameObject.activeInHierarchy)
						{
							this.m_BaseEnemy.CurrentHP -= component.power;
						}
						component.Explosion();
					}
				}
				else
				{
					this.m_BaseEnemy.Die(EnemyKilledBy.DeadZone);
				}
			}
		}

		public BaseEnemy m_BaseEnemy;
	}
}
