using System;
using SkyGameKit;
using UnityEngine;

public class MoveBorderCollision : EnemyMove
{
	private void Update()
	{
		this.CheckDirection();
		base.Move();
	}

	private void CheckDirection()
	{
		if (base.transform.position.x > 4.5f && this.direction.x > 0f)
		{
			this.direction = new Vector2(this.direction.x * -1f, this.direction.y);
		}
		if (base.transform.position.x < -4.5f && this.direction.x < 0f)
		{
			this.direction = new Vector2(this.direction.x * -1f, this.direction.y);
		}
		if (base.transform.position.y > SgkCamera.topRight.y && this.direction.y > 0f)
		{
			this.direction = new Vector2(this.direction.x, this.direction.y * -1f);
		}
		if (base.transform.position.y < SgkCamera.bottomLeft.y && this.direction.y < 0f)
		{
			this.direction = new Vector2(this.direction.x, this.direction.y * -1f);
		}
	}
}
