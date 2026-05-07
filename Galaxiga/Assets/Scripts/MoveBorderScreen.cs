using System;
using SkyGameKit;
using UnityEngine;

public class MoveBorderScreen : EnemyMove
{
	private void Update()
	{
		base.SetTimeCheck();
		this.CheckDirection();
		base.Move();
	}

	private void CheckDirection()
	{
		if (base.transform.position.x > 4.5f && !this.checkRight)
		{
			this.checkLeft = false;
			this.checkRight = true;
			if (base.transform.position.y > 0f)
			{
				this.direction = new Vector2(0f, -1f);
			}
			else
			{
				this.direction = new Vector2(0f, 1f);
			}
		}
		if (base.transform.position.x < -4.5f && !this.checkLeft)
		{
			this.checkLeft = true;
			this.checkRight = false;
			if (base.transform.position.y > 0f)
			{
				this.direction = new Vector2(0f, -1f);
			}
			else
			{
				this.direction = new Vector2(0f, 1f);
			}
		}
		if (base.transform.position.y > SgkCamera.topRight.y && !this.checkTop)
		{
			this.checkTop = true;
			this.checkBottom = false;
			if (base.transform.position.x > 0f)
			{
				this.direction = new Vector2(-1f, 0f);
			}
			else
			{
				this.direction = new Vector2(1f, 0f);
			}
		}
		if (base.transform.position.y < SgkCamera.bottomLeft.y && !this.checkBottom)
		{
			this.checkTop = false;
			this.checkBottom = true;
			if (base.transform.position.x > 0f)
			{
				this.direction = new Vector2(-1f, 0f);
			}
			else
			{
				this.direction = new Vector2(1f, 0f);
			}
		}
	}

	private bool checkLeft;

	private bool checkRight;

	private bool checkTop;

	private bool checkBottom;
}
