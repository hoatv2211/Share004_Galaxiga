using System;
using DG.Tweening;
using SkyGameKit;
using UnityEngine;

public class SpawnRandomPosition : SpawnByNumber
{
	public override void StartWithDelayAndDisplay()
	{
		if (SpawnRandomPosition.bottomLeft.x == 0f)
		{
			SpawnRandomPosition.topRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));
			SpawnRandomPosition.bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane));
			SpawnRandomPosition.cameraSize = SpawnRandomPosition.topRight - SpawnRandomPosition.bottomLeft;
		}
		this.top = ((this.angle <= 0f) ? SpawnRandomPosition.topRight : new Vector2(SpawnRandomPosition.bottomLeft.x, SpawnRandomPosition.topRight.y));
		Vector2 b = Fu.RotateVector2(Vector2.up * 1f, this.angle);
		this.top += b;
		this.point1 = this.top + Fu.RotateVector2(b.normalized * SpawnRandomPosition.cameraSize.x * Mathf.Cos(Mathf.Abs(this.angle) * 0.0174532924f), (float)((this.angle <= 0f) ? 90 : -90));
		this.point2 = this.top + Fu.RotateVector2(b.normalized * SpawnRandomPosition.cameraSize.y * Mathf.Sin(Mathf.Abs(this.angle) * 0.0174532924f), (float)((this.angle >= 0f) ? 90 : -90));
		base.StartWithDelayAndDisplay();
	}

	protected override bool BeforeSetFieldAndAction(BaseEnemy enemy)
	{
		enemy.transform.position = Vector2.Lerp(this.point1, this.point2, UnityEngine.Random.Range(0f, 1f));
		enemy.transform.rotation = Quaternion.Euler(0f, 0f, this.angle);
		enemy.BaseMoveStraight(this.speed, Ease.Linear, Fu.RotateVector2(Vector2.right * (SpawnRandomPosition.cameraSize.magnitude + 2f), this.angle - 90f)).OnComplete(delegate
		{
			if (this.autoDie)
			{
				enemy.Die(EnemyKilledBy.TimeOut);
			}
		});
		return base.BeforeSetFieldAndAction(enemy);
	}

	[Tooltip("Góc bay")]
	[Range(-90f, 90f)]
	public float angle = -90f;

	public float speed = 2f;

	public bool autoDie = true;

	private static Vector2 bottomLeft;

	private static Vector2 topRight;

	private static Vector2 cameraSize;

	private static float height;

	private const float distance = 1f;

	private Vector2 point1;

	private Vector2 point2;

	private Vector2 top;
}
