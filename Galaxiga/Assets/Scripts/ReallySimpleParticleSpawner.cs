using System;
using System.Collections;
using PathologicalGames;
using UnityEngine;

public class ReallySimpleParticleSpawner : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.ParticleSpawner());
	}

	private IEnumerator ParticleSpawner()
	{
		for (;;)
		{
			PoolManager.Pools[this.poolName].Spawn(this.prefab, base.transform.position, base.transform.rotation);
			yield return new WaitForSeconds(this.spawnInterval);
		}
		yield break;
	}

	public string poolName;

	public ParticleSystem prefab;

	public float spawnInterval = 1f;
}
