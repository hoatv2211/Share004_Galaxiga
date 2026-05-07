using System;
using PathologicalGames;
using UnityEngine;

public class ItemSupport : ItemGeneral
{
	public override void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			switch (this.supportItem)
			{
			case ItemSupport.SupportItem.ChainSaw:
				ItemSupportPlayerInGameManager.current.ActiveChainsaw();
				break;
			case ItemSupport.SupportItem.Shield:
				ItemSupportPlayerInGameManager.current.ActiveShieldPlayer();
				break;
			case ItemSupport.SupportItem.TowerHoming:
				ItemSupportPlayerInGameManager.current.ActiveTurretHoming();
				break;
			case ItemSupport.SupportItem.TowerLaser:
				ItemSupportPlayerInGameManager.current.ActiveTurretLaser();
				break;
			case ItemSupport.SupportItem.Instancehomingmissile:
				ItemSupportPlayerInGameManager.current.ActiveIntanceHomingMissile();
				break;
			case ItemSupport.SupportItem.Towermachinegun:
				ItemSupportPlayerInGameManager.current.ActiveTurretMachineGun();
				break;
			}
			if (PoolManager.Pools["ItemPool"].IsSpawned(base.transform))
			{
				PoolManager.Pools["ItemPool"].Despawn(base.transform);
			}
		}
		base.OnTriggerEnter2D(col);
	}

	public ItemSupport.SupportItem supportItem;

	public enum SupportItem
	{
		ChainSaw,
		Shield,
		TowerHoming,
		TowerLaser,
		Instancehomingmissile,
		Towermachinegun
	}
}
