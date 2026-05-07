using System;
using UnityEngine;

public class ItemPlane : ItemGeneral
{
	public override void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			PlaneIngameManager.current.ChangePlaneID(this.plane);
		}
		base.OnTriggerEnter2D(col);
	}

	public GameContext.Plane plane;
}
