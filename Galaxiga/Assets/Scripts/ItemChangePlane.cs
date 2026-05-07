using System;
using PathologicalGames;
using UnityEngine;

public class ItemChangePlane : ItemGeneral
{
	public void SwitchPlane()
	{
		switch (this.plane)
		{
		}
	}

	public override void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			PlaneIngameManager.current.ChangePlane(this.plane);
			if (PoolManager.Pools["ItemPool"].IsSpawned(base.transform))
			{
				PoolManager.Pools["ItemPool"].Despawn(base.transform);
			}
		}
		base.OnTriggerEnter2D(col);
	}

	public GameContext.Plane plane;
}
