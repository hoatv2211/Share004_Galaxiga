using System;
using DG.Tweening;
using SkyGameKit;
using UnityEngine;

public class VongXoayEnemy : BoxItemEnemy
{
	[EnemyAction(displayName = "Xoay/Xoay")]
	public void Xoay(bool rotLeft, float speedMove, float speedRotate, float sizeLazeX, float sizeLazeY)
	{
		base.transform.GetComponent<EnemyMoveLine>().speed = speedMove;
		this.laze1.localScale = new Vector3(sizeLazeX, sizeLazeY, 1f);
		this.laze2.localScale = new Vector3(sizeLazeX, sizeLazeY, 1f);
		this.laze3.localScale = new Vector3(sizeLazeX, sizeLazeY, 1f);
		this.laze4.localScale = new Vector3(sizeLazeX, sizeLazeY, 1f);
		if (rotLeft)
		{
			this.vongxoay.DORotate(new Vector3(0f, 0f, 360f), speedRotate, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetSpeedBased(true);
		}
		else
		{
			this.vongxoay.DORotate(new Vector3(0f, 0f, -360f), speedRotate, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetSpeedBased(true);
		}
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		this.vongxoay.DOKill(false);
		this.vongxoay.localRotation = Quaternion.Euler(0f, 0f, 0f);
		base.Die(type);
	}

	public Transform laze1;

	public Transform laze2;

	public Transform laze3;

	public Transform laze4;

	public Transform vongxoay;
}
