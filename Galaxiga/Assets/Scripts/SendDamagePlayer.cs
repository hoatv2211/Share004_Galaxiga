using System;
using UnityEngine;

public class SendDamagePlayer : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D col)
	{
		string tag = col.tag;
		if (tag != null)
		{
			if (!(tag == "Player"))
			{
				if (tag == "PlayerBullet")
				{
					if (col.GetComponent<NewBulletDrone>() != null && col.GetComponent<NewBulletDrone>().isBullet_godofthunder_1())
					{
						UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(base.GetComponent<UbhBullet>(), false);
					}
				}
			}
			else
			{
				PlaneIngameManager.current.TakeDamagePlayer();
				UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(base.GetComponent<UbhBullet>(), false);
			}
		}
	}

	private void OnBecameInvisible()
	{
		if (base.gameObject.activeInHierarchy)
		{
			UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(base.GetComponent<UbhBullet>(), false);
		}
	}

	public bool changeScale;
}
