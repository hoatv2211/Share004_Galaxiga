using System;
using UnityEngine;

public class EnemyMoveLine : EnemyMove
{
	private void Start()
	{
		if (this.stUpDown)
		{
			this.direction = new Vector2(0f, -1f);
		}
		else
		{
			this.direction = new Vector2(1f, 0f);
		}
	}

	private void Update()
	{
		base.Move();
	}

	public bool stUpDown;
}
