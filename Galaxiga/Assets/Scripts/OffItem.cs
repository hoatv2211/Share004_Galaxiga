using System;
using System.Collections;
using PathologicalGames;
using UnityEngine;

public class OffItem : MonoBehaviour
{
	private void OnBecameVisible()
	{
		base.StartCoroutine(this.OffItemIE());
	}

	private IEnumerator OffItemIE()
	{
		yield return new WaitForSeconds(this.timeOff);
		if (PoolManager.Pools["ItemPool"].IsSpawned(base.transform))
		{
			PoolManager.Pools["ItemPool"].Despawn(base.transform);
		}
		yield return null;
		yield break;
	}

	public float timeOff = 2f;
}
