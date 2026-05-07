using System;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
	private void Start()
	{
		this.ps = base.GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if (!this.ps.IsAlive())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private ParticleSystem ps;
}
