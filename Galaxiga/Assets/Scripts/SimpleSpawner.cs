using System;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.Spawner());
		if (this.particlesPoolName != string.Empty)
		{
			base.StartCoroutine(this.ParticleSpawner());
		}
	}

	private IEnumerator ParticleSpawner()
	{
		SpawnPool particlesPool = PoolManager.Pools[this.particlesPoolName];
		ParticleSystem prefab = this.particleSystemPrefab;
		Vector3 prefabXform = this.particleSystemPrefab.transform.position;
		Quaternion prefabRot = this.particleSystemPrefab.transform.rotation;
		for (;;)
		{
			ParticleSystem emitter = particlesPool.Spawn(prefab, prefabXform, prefabRot);
			while (emitter.IsAlive(true))
			{
				yield return new WaitForSeconds(1f);
			}
			ParticleSystem inst = particlesPool.Spawn(prefab, prefabXform, prefabRot);
			particlesPool.Despawn(inst.transform, 2f);
			yield return new WaitForSeconds(2f);
		}
		yield break;
	}

	private IEnumerator Spawner()
	{
		int count = this.spawnAmount;
		SpawnPool shapesPool = PoolManager.Pools[this.poolName];
		while (count > 0)
		{
			Transform inst = shapesPool.Spawn(this.testPrefab);
			inst.localPosition = new Vector3((float)(this.spawnAmount + 2 - count), 0f, 0f);
			count--;
			yield return new WaitForSeconds(this.spawnInterval);
		}
		base.StartCoroutine(this.Despawner());
		yield return null;
		yield break;
	}

	private IEnumerator Despawner()
	{
		SpawnPool shapesPool = PoolManager.Pools[this.poolName];
		List<Transform> spawnedCopy = new List<Transform>(shapesPool);
		UnityEngine.Debug.Log(shapesPool.ToString());
		foreach (Transform instance in spawnedCopy)
		{
			shapesPool.Despawn(instance);
			yield return new WaitForSeconds(this.spawnInterval);
		}
		base.StartCoroutine(this.Spawner());
		yield return null;
		yield break;
	}

	public string poolName;

	public Transform testPrefab;

	public int spawnAmount = 50;

	public float spawnInterval = 0.25f;

	public string particlesPoolName;

	public ParticleSystem particleSystemPrefab;
}
