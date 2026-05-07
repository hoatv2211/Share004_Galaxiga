using System;
using SkyGameKit;

public class TweenEnemyExample : TweenEnemy
{
	public override void OnHoldPositionEnd(int shapeIndex, string shapeName)
	{
	}

	public override void OnHoldPositionStart(int shapeIndex, string shapeName)
	{
	}

	public override void OnTransformShapeComplete(int shapeIndex, string shapeName)
	{
	}

	public override void OnChoosingMe()
	{
		if (this.onChoosingMe != null)
		{
			this.onChoosingMe();
		}
	}

	private void Awake()
	{
		this.shotCtrl = base.GetComponentInChildren<UbhShotCtrl>();
	}

	[EnemyAction]
	public void Shoot()
	{
		if (this.shotCtrl != null)
		{
			this.shotCtrl.StartShotRoutine();
		}
	}

	public EnemyEvent onChoosingMe;

	private UbhShotCtrl shotCtrl;
}
