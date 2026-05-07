using System;
using UnityEngine;

public class MyFx : MonoBehaviour
{
	private void Start()
	{
		this.particleSystem = base.gameObject.GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if (Time.timeScale < 0.01f)
		{
			this.particleSystem.Simulate(Time.unscaledDeltaTime, true, false);
		}
	}

	[SerializeField]
	private ParticleSystem particleSystem;
}
