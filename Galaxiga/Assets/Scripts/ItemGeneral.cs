using System;
using PathologicalGames;
using UnityEngine;

public class ItemGeneral : MonoBehaviour
{
	private void OnBecameVisible()
	{
		this.stMovePlayer = false;
	}

	private void OnBecameInvisible()
	{
		if (base.gameObject.activeInHierarchy && PoolManager.Pools["ItemPool"].IsSpawned(base.transform))
		{
			PoolManager.Pools["ItemPool"].Despawn(base.transform);
			this.stMovePlayer = false;
		}
	}

	public virtual void OnTriggerEnter2D(Collider2D col)
	{
		string tag = col.tag;
		if (tag != null)
		{
			if (!(tag == "Player"))
			{
				if (!(tag == "DeadZone"))
				{
					if (tag == "Get_Item")
					{
						this.targetPlayer = PlaneIngameManager.current.CurrentTransformPlayer;
						this.stMovePlayer = true;
					}
				}
				else if (PoolManager.Pools["ItemPool"].IsSpawned(base.transform))
				{
					PoolManager.Pools["ItemPool"].Despawn(base.transform);
					this.stMovePlayer = false;
				}
			}
			else if (PoolManager.Pools["ItemPool"].IsSpawned(base.transform))
			{
				PoolManager.Pools["ItemPool"].Despawn(base.transform);
				this.stMovePlayer = false;
			}
		}
	}

	private void Update()
	{
		if (!this.stMovePlayer)
		{
			base.transform.position += Vector3.down * this.speed * Time.deltaTime;
		}
		else if (this.stMoveToPlayer)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.targetPlayer.position, this.speedToPlayer * Time.deltaTime);
		}
		else
		{
			base.transform.position += Vector3.down * this.speed * Time.deltaTime;
		}
	}

	public float speed = 5f;

	private bool stMovePlayer;

	private Transform targetPlayer;

	public float speedToPlayer = 7f;

	public bool stMoveToPlayer = true;
}
