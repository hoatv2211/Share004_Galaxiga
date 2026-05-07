using System;
using PathologicalGames;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (this.needDestroy)
		{
			this.needDestroy = false;
			base.Invoke("DespawEffect", this.time);
		}
	}

	private void DespawEffect()
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (this.isPool)
			{
				PoolManager.Pools["Effect"].Despawn(base.gameObject.transform);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
		this.needDestroy = true;
	}

	public float time;

	public bool isPool;

	private bool needDestroy = true;
}
