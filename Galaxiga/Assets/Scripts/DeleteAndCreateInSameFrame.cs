using System;
using System.Collections;
using PathologicalGames;
using UnityEngine;

public class DeleteAndCreateInSameFrame : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.DoIt());
	}

	private IEnumerator DoIt()
	{
		for (;;)
		{
			SpawnPool pool = UnityEngine.Object.Instantiate<SpawnPool>(this.poolPrefab);
			yield return new WaitForSeconds(2f);
			PoolManager.Pools.Destroy(pool.poolName);
			pool = UnityEngine.Object.Instantiate<SpawnPool>(this.poolPrefab);
			yield return new WaitForSeconds(2f);
			PoolManager.Pools.DestroyAll();
		}
		yield break;
	}

	public SpawnPool poolPrefab;
}
