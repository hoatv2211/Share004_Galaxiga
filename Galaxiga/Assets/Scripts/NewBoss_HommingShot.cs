using System;
using UnityEngine;

public class NewBoss_HommingShot : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (this.needFollow)
		{
			if (this.target != null && this.target.gameObject.activeInHierarchy)
			{
				this.point2Target = (Vector2)this.target.position - this.rb.position;
				this.point2Target.Normalize();
				this.value = Vector3.Cross(this.point2Target, base.transform.up).z;
				this.rb.angularVelocity = -this.value * this.rotatingSpeed;
				this.rb.linearVelocity = base.transform.up * this.speedMove;
			}
			else if (this.needFindTarget)
			{
				this.FindRandomTarget();
			}
			else
			{
				this.MoveBullet();
			}
		}
	}

	private void MoveBullet()
	{
		this.rb.angularVelocity = -this.value * this.rotatingSpeedNoTarget;
		this.rb.linearVelocity = base.transform.up * this.speedMove;
	}

	public void FindRandomTarget()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, this.rangeAttack, this.layerEnemy);
		if (array.Length > 0)
		{
			GameObject gameObject = array[UnityEngine.Random.Range(0, array.Length)].gameObject;
			this.target = gameObject.transform;
		}
		else
		{
			this.target = null;
			this.MoveBullet();
		}
	}

	public bool needFollow = true;

	public bool needFindTarget = true;

	public float speedMove = 11f;

	public Transform target;

	private Rigidbody2D rb;

	private float value;

	public float rotatingSpeed = 360f;

	private float rotatingSpeedNoTarget = 10f;

	private Vector2 point2Target;

	private float rangeAttack = 30f;

	public LayerMask layerEnemy;
}
