using System;
using DG.Tweening;
using UnityEngine;

namespace SkyGameKit.Demo
{
	public class TankDemo : BaseEnemy
	{
		private void FixedUpdate()
		{
			if (this.isMoving)
			{
				base.transform.localPosition += 2f * Time.fixedDeltaTime * ((!this.moveRight) ? Vector3.left : Vector3.right);
				if (base.transform.localPosition.magnitude > 3f)
				{
					this.moveRight = false;
					this.isMoving = false;
					base.transform.DORotate(180f * Vector3.forward, 1f, RotateMode.Fast).OnComplete(delegate
					{
						this.isMoving = true;
					});
				}
				else if (base.transform.localPosition.magnitude < 0.1f)
				{
					this.moveRight = true;
					this.isMoving = false;
					base.transform.DORotate(Vector3.zero, 1f, RotateMode.Fast).OnComplete(delegate
					{
						this.isMoving = true;
					});
				}
			}
		}

		private bool moveRight = true;

		private bool isMoving = true;
	}
}
