using System;
using UnityEngine;

public class LookGun : MonoBehaviour
{
	private void Start()
	{
		if (this.player == null && PlaneIngameManager.current.CurrentTransformPlayer != null)
		{
			this.player = PlaneIngameManager.current.CurrentTransformPlayer;
		}
	}

	private void Update()
	{
		if (this.player == null && PlaneIngameManager.current.CurrentTransformPlayer != null)
		{
			this.player = PlaneIngameManager.current.CurrentTransformPlayer;
		}
		this.LookRotation();
	}

	private void LookRotation()
	{
		Vector3 vector = new Vector3(this.player.position.x, this.player.position.y, this.player.position.z);
		if (this.player.position.x < base.transform.position.x)
		{
			if (this.parent.transform.localScale.y < 0f)
			{
				this.parent.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			Quaternion b = Quaternion.LookRotation(vector - base.transform.position, Vector3.up);
			b.x = 0f;
			b.y = 0f;
			base.transform.localRotation = Quaternion.Slerp(base.transform.localRotation, b, Time.deltaTime * this.rotateSpeed);
		}
		else
		{
			Quaternion b2 = Quaternion.LookRotation(base.transform.position - vector, Vector3.up);
			b2.x = 0f;
			b2.y = 0f;
			b2.z *= -1f;
			if (this.parent.transform.localScale.y > 0f)
			{
				this.parent.transform.localScale = new Vector3(1f, -1f, 1f);
			}
			base.transform.localRotation = Quaternion.Slerp(base.transform.localRotation, b2, Time.deltaTime * this.rotateSpeed);
		}
	}

	public Transform player;

	public float rotateSpeed;

	public GameObject parent;

	public float x;
}
