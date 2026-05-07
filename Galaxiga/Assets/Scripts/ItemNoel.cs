using System;
using Hellmade.Sound;
using SkyGameKit;
using UnityEngine;

public class ItemNoel : ItemGold
{
	protected override void OnTriggerEnter2D(Collider2D c)
	{
		if (c.name.Equals("BulletCheck"))
		{
			this.OnItemNoel();
			this.Despawn();
		}
		else if (!this.goToPlayer && c.CompareTag("Get_Item"))
		{
			this.goToPlayer = true;
		}
	}

	private void OnItemNoel()
	{
		EazySoundManager.PlaySound(AudioCache.Sound.get_coin);
		UIGameManager.current.UpDateNumberCandy();
		SgkSingleton<LevelEventInfo>.Instance.numberItemEvent = SgkSingleton<LevelEventInfo>.Instance.numberItemEvent + 1;
	}
}
