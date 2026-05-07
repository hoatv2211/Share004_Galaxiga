using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	public void Move()
	{
		Vector3 vector = new Vector3(this.speed * this.direction.x, this.speed * this.direction.y, 0f);
		vector *= Time.deltaTime;
		base.transform.Translate(vector);
	}

	public void SetTimeCheck()
	{
		if (Time.time > this.timeFire)
		{
			this.isCheck = true;
			this.timeFire = Time.time + this.timeRate;
		}
	}

	[Header("MOVE")]
	public float speed = 2f;

	public Vector2 direction = new Vector2(1f, 0f);

	[Header("CHECKDIRECTIONMOVE")]
	public float timeRate = 2f;

	public bool isCheck;

	private float timeFire;
}
