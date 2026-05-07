using System;
using SkyGameKit;
using UnityEngine;

public class BulletLaserPlayer : Laser
{
	public override void LaserOnOnLaserHitTriggered(RaycastHit2D hitInfo)
	{
		if (hitInfo.collider.gameObject.CompareTag("Enemy"))
		{
			GameObject gameObject = hitInfo.collider.gameObject;
			EnemyHit component = gameObject.GetComponent<EnemyHit>();
			if (component != null)
			{
				base.SpawFxBulletTriggerEnemy(hitInfo.point);
				component.m_BaseEnemy.CurrentHP -= (int)this.power;
			}
		}
	}

	public override void OnTriggerEnter2D(Collider2D c)
	{
		if (c.CompareTag("Enemy") && this.isIgnoreCollision && !this.triggerList.Contains(c))
		{
			this.triggerList.Add(c);
		}
	}

	public override void OnTriggerExit2D(Collider2D c)
	{
		if (c.CompareTag("Enemy") && this.isIgnoreCollision && this.triggerList.Contains(c))
		{
			this.triggerList.Remove(c);
		}
	}

	public override void TakeDamage()
	{
		if (Time.time > this.nextTime)
		{
			this.nextTime = Time.time + this.fireRate;
			int count = this.triggerList.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					EnemyHit component = this.triggerList[i].gameObject.GetComponent<EnemyHit>();
					if (component != null)
					{
						component.m_BaseEnemy.CurrentHP -= (int)this.power;
					}
				}
			}
		}
	}

	private float nextTime;
}
