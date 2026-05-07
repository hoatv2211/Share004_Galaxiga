using System;
using Hellmade.Sound;
using SkyGameKit;
using UnityEngine;

public class ItemEndLess : ItemGold
{
	protected override void OnTriggerEnter2D(Collider2D c)
	{
		if (c.name.Equals("BulletCheck"))
		{
			this.OnItemEndLess();
			this.Despawn();
		}
		else if (!this.goToPlayer && c.CompareTag("Get_Item"))
		{
			this.goToPlayer = true;
		}
	}

	private void OnItemEndLess()
	{
		EazySoundManager.PlaySound(AudioCache.Sound.get_coin);
		UIGameManager.current.UpdateBlueStar();
		SgkSingleton<LevelInfo>.Instance.numberItemEvent = SgkSingleton<LevelInfo>.Instance.numberItemEvent + 1;
	}
}
