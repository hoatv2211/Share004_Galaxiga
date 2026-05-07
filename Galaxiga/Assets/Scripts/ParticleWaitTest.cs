using System;
using System.Collections;
using PathologicalGames;
using UnityEngine;

public class ParticleWaitTest : MonoBehaviour
{
	private IEnumerator Start()
	{
		SpawnPool particlesPool = PoolManager.Pools[this.particlesPoolName];
		ParticleSystem prefab = this.particleSystemPrefab;
		Vector3 prefabXform = this.particleSystemPrefab.transform.position;
		Quaternion prefabRot = this.particleSystemPrefab.transform.rotation;
		for (;;)
		{
			yield return new WaitForSeconds(this.spawnInterval);
			ParticleSystem emitter = particlesPool.Spawn(prefab, prefabXform, prefabRot);
			while (emitter.IsAlive(true))
			{
				yield return new WaitForSeconds(3f);
			}
			ParticleSystem inst = particlesPool.Spawn(prefab, prefabXform, prefabRot);
			particlesPool.Despawn(inst.transform, 2f);
			yield return new WaitForSeconds(2f);
			particlesPool.Spawn(prefab, prefabXform, prefabRot);
		}
		yield break;
	}

	public float spawnInterval = 0.25f;

	public string particlesPoolName;

	public ParticleSystem particleSystemPrefab;
}
