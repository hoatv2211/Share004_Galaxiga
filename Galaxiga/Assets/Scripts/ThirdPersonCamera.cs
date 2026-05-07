using System;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	private void Start()
	{
		this.follow = GameObject.FindWithTag("Player").transform;
	}

	private void LateUpdate()
	{
		this.targetPosition = this.follow.position + Vector3.up * this.distanceUp - this.follow.forward * this.distanceAway;
		base.transform.position = Vector3.Lerp(base.transform.position, this.targetPosition, Time.deltaTime * this.smooth);
		base.transform.LookAt(this.follow);
	}

	public float distanceAway;

	public float distanceUp;

	public float smooth;

	private GameObject hovercraft;

	private Vector3 targetPosition;

	private Transform follow;
}
