using System;
using UnityEngine;

public class BossMode_3Mat_Horn : MonoBehaviour
{
	private void DrawPath()
	{
		this.pathLeft = new Vector3[]
		{
			this.posPathLeft[0].position,
			this.posPathLeft[1].position,
			this.posPathLeft[2].position,
			this.posPathLeft[3].position
		};
		this.pathLeft1 = new Vector3[]
		{
			this.posPathLeft[0].position,
			this.posPathLeft[1].position,
			this.posPathLeft[2].position,
			this.posPathLeft[4].position
		};
		this.pathRight = new Vector3[]
		{
			this.posPathRight[0].position,
			this.posPathRight[1].position,
			this.posPathRight[2].position,
			this.posPathRight[3].position
		};
		this.pathRight1 = new Vector3[]
		{
			this.posPathRight[0].position,
			this.posPathRight[1].position,
			this.posPathRight[2].position,
			this.posPathRight[4].position
		};
	}

	public void Attack()
	{
		this.DrawPath();
		UbhBullet bullet = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.prefabsBullet, this.posPathLeft[0].position, false);
		UbhBullet bullet2 = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.prefabsBullet, this.posPathLeft[0].position, false);
		LeanTween.move(bullet.gameObject, this.pathLeft, this.speedBullet).setOrientToPath2d(true);
		LeanTween.move(bullet2.gameObject, this.pathRight, this.speedBullet).setOrientToPath2d(true);
	}

	public float speedBullet = 5f;

	public GameObject prefabsBullet;

	public Transform[] posPathLeft;

	public Transform[] posPathRight;

	private Vector3[] pathLeft;

	private Vector3[] pathLeft1;

	private Vector3[] pathRight;

	private Vector3[] pathRight1;
}
