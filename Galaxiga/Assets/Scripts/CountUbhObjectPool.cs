using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CountUbhObjectPool : UbhObjectPool
{
	public override UbhBullet GetBullet(GameObject goPrefab, Vector3 position, bool forceInstantiate = false)
	{
		UbhBullet bullet = base.GetBullet(goPrefab, position, forceInstantiate);
		this.cachePrefab[bullet.transform] = goPrefab.transform;
		return bullet;
	}

	public Transform GetPrefab(Transform instance)
	{
		return this.cachePrefab[instance];
	}

	private Dictionary<Transform, Transform> cachePrefab = new Dictionary<Transform, Transform>();
}
