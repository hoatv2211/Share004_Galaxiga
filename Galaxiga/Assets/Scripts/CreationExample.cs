using System;
using System.Collections;
using PathologicalGames;
using UnityEngine;

public class CreationExample : MonoBehaviour
{
	private void Start()
	{
		this.pool = PoolManager.Pools.Create(this.poolName);
		this.pool.group.parent = base.transform;
		this.pool.group.localPosition = new Vector3(1.5f, 0f, 0f);
		this.pool.group.localRotation = Quaternion.identity;
		PrefabPool prefabPool = new PrefabPool(this.testPrefab);
		prefabPool.preloadAmount = 5;
		prefabPool.cullDespawned = true;
		prefabPool.cullAbove = 10;
		prefabPool.cullDelay = 1;
		prefabPool.limitInstances = true;
		prefabPool.limitAmount = 5;
		prefabPool.limitFIFO = true;
		this.pool.CreatePrefabPool(prefabPool);
		base.StartCoroutine(this.Spawner());
		Transform prefab = PoolManager.Pools["Shapes"].prefabs["Cube"];
		Transform transform = PoolManager.Pools["Shapes"].Spawn(prefab);
		transform.name = "Cube (Spawned By CreationExample.cs)";
	}

	private IEnumerator Spawner()
	{
		int count = this.spawnAmount;
		while (count > 0)
		{
			Transform inst = this.pool.Spawn(this.testPrefab, Vector3.zero, Quaternion.identity);
			inst.localPosition = new Vector3((float)(this.spawnAmount - count), 0f, 0f);
			count--;
			yield return new WaitForSeconds(this.spawnInterval);
		}
		base.StartCoroutine(this.Despawner());
		yield break;
	}

	private IEnumerator Despawner()
	{
		while (this.pool.Count > 0)
		{
			Transform instance = this.pool[this.pool.Count - 1];
			this.pool.Despawn(instance);
			yield return new WaitForSeconds(this.spawnInterval);
		}
		yield break;
	}

	public Transform testPrefab;

	public string poolName = "Creator";

	public int spawnAmount = 50;

	public float spawnInterval = 0.25f;

	private SpawnPool pool;
}
