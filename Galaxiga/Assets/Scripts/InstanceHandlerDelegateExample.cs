using System;
using PathologicalGames;
using UnityEngine;

public class InstanceHandlerDelegateExample : MonoBehaviour
{
	private void Awake()
	{
		InstanceHandler.InstantiateDelegates = (InstanceHandler.InstantiateDelegate)Delegate.Combine(InstanceHandler.InstantiateDelegates, new InstanceHandler.InstantiateDelegate(this.InstantiateDelegate));
		InstanceHandler.DestroyDelegates = (InstanceHandler.DestroyDelegate)Delegate.Combine(InstanceHandler.DestroyDelegates, new InstanceHandler.DestroyDelegate(this.DestroyDelegate));
	}

	private void Start()
	{
		SpawnPool spawnPool = PoolManager.Pools["Shapes"];
		spawnPool.instantiateDelegates = (SpawnPool.InstantiateDelegate)Delegate.Combine(spawnPool.instantiateDelegates, new SpawnPool.InstantiateDelegate(this.InstantiateDelegateForShapesPool));
	}

	public GameObject InstantiateDelegate(GameObject prefab, Vector3 pos, Quaternion rot)
	{
		UnityEngine.Debug.Log("Using my own instantiation delegate on prefab '" + prefab.name + "'!");
		return UnityEngine.Object.Instantiate<GameObject>(prefab, pos, rot);
	}

	public void DestroyDelegate(GameObject instance)
	{
		UnityEngine.Debug.Log("Using my own destroy delegate on '" + instance.name + "'!");
		UnityEngine.Object.Destroy(instance);
	}

	public GameObject InstantiateDelegateForShapesPool(GameObject prefab, Vector3 pos, Quaternion rot)
	{
		UnityEngine.Debug.Log("Using my own instantiation delegate for just the 'Shapes' pool on prefab '" + prefab.name + "'!");
		return UnityEngine.Object.Instantiate<GameObject>(prefab, pos, rot);
	}
}
