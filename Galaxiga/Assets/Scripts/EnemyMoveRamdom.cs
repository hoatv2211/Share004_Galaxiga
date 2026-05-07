using System;
using UnityEngine;

public class EnemyMoveRamdom : EnemyMove
{
	private void Start()
	{
	}

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
		Vector3 b = new Vector3(UnityEngine.Random.Range(this.minmaxX.x, this.minmaxX.y), UnityEngine.Random.Range(this.minmaxY.x, this.minmaxY.y), 0f);
		Vector2 direction = base.transform.position - b;
		this.direction = direction;
	}

	public Vector2 minmaxX = new Vector2(-5f, 5f);

	public Vector2 minmaxY = new Vector2(-9f, 9f);
}
