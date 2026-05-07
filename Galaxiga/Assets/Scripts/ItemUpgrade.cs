using System;
using UnityEngine;

public class ItemUpgrade : ItemGeneral
{
	public override void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			if (!this.superItem)
			{
				PlaneIngameManager.current.PowerUpPlane();
			}
			else
			{
				PlaneIngameManager.current.StartSkillPlane();
			}
		}
		base.OnTriggerEnter2D(col);
	}

	public bool superItem;
}
