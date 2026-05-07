using System;
using UnityEngine;

public class EnemyFollow : EnemyMove
{
	private void Update()
	{
		base.SetTimeCheck();
		if (this.isCheck)
		{
			this.isCheck = false;
			this.CheckRotate();
		}
		base.Move();
	}

	private void CheckRotate()
	{
		Vector2 direction = base.transform.position - this.targetPlayer.position;
		this.direction = direction;
	}

	public Transform targetPlayer;
}
