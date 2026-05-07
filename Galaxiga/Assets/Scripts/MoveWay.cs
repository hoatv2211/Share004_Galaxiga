using System;
using SkyGameKit;
using UnityEngine;

public class MoveWay : MonoBehaviour
{
	private void OnBecameVisible()
	{
	}

	private void Start()
	{
		this.unitcheck = 4.5f - this.unitToMove;
		this.wave = base.GetComponent<WaveManager>();
	}

	private void Update()
	{
		if (!this.startMove && !this.checkmove && this.wave.State == WaveState.Running)
		{
			this.checkmove = true;
			base.Invoke("StartMove", this.timeStartMove);
		}
		if (this.startMove)
		{
			this.CheckDontMove();
			this.Move();
		}
	}

	private void StartMove()
	{
		this.startMove = true;
	}

	public void CheckDontMove()
	{
		if (base.transform.position.x + this.unitcheck > 4.5f)
		{
			this.direction = new Vector2(-1f, 0f);
		}
		if (base.transform.position.x - this.unitcheck < -4.5f)
		{
			this.direction = new Vector2(1f, 0f);
		}
	}

	public void Move()
	{
		Vector3 vector = new Vector3(this.speed * this.direction.x, this.speed * this.direction.y, 0f);
		vector *= Time.deltaTime;
		base.transform.Translate(vector);
	}

	[Header("MOVE_WAY")]
	public float speed = 1f;

	public Vector2 direction = new Vector2(1f, 0f);

	public float unitToMove = 4f;

	private float unitcheck = 4f;

	public bool startMove = true;

	public float timeStartMove = 10f;

	private WaveManager wave;

	private bool checkmove;
}
