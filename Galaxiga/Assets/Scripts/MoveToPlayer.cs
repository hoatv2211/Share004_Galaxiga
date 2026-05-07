using System;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
	private void Start()
	{
		this.targetPlayer = PlaneIngameManager.current.CurrentTransformPlayer;
	}

	private void Update()
	{
		if (this.targetPlayer != null)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.targetPlayer.position, this.speed * Time.deltaTime);
		}
	}

	public float speed;

	private Transform targetPlayer;
}
