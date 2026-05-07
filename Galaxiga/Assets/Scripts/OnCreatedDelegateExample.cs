using System;
using PathologicalGames;
using UnityEngine;

public class OnCreatedDelegateExample : MonoBehaviour
{
	private void Awake()
	{
		PoolManager.Pools.AddOnCreatedDelegate(this.poolName, new SpawnPoolsDict.OnCreatedDelegate(this.RunMe));
	}

	public void RunMe(SpawnPool pool)
	{
		UnityEngine.Debug.Log("OnCreatedDelegateExample RunMe ran for pool " + pool.poolName);
	}

	public string poolName = "Shapes";
}
