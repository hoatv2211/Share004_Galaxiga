using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class UbhAutoDespawnParticle : UbhMonoBehaviour
{
	private void OnEnable()
	{
		base.StartCoroutine(this.CheckIfAliveCoroutine());
	}

	private IEnumerator CheckIfAliveCoroutine()
	{
		ParticleSystem pSystem = base.GetComponent<ParticleSystem>();
		for (;;)
		{
			yield return new WaitForSeconds(1f);
			if (!pSystem.IsAlive(true))
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		yield break;
	}
}
