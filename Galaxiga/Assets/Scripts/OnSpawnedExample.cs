using System;
using PathologicalGames;
using UnityEngine;

public class OnSpawnedExample : MonoBehaviour
{
	private void OnSpawned(SpawnPool pool)
	{
		UnityEngine.Debug.Log(string.Format("OnSpawnedExample | OnSpawned running for '{0}' in pool '{1}'.", base.name, pool.poolName));
	}

	private void OnDespawned(SpawnPool pool)
	{
		UnityEngine.Debug.Log(string.Format("OnSpawnedExample | OnDespawned unning for '{0}' in pool '{1}'.", base.name, pool.poolName));
	}
}
