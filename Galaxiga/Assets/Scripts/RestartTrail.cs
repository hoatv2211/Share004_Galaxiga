using System;
using UnityEngine;

public class RestartTrail : MonoBehaviour
{
	private void Awake()
	{
		this.trail = base.GetComponent<TrailRenderer>();
	}

	private void OnEnable()
	{
		this.trail.Clear();
	}

	private TrailRenderer trail;
}
