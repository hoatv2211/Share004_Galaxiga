using System;
using System.Collections.Generic;
using SkyGameKit;
using SWS;
using UnityEngine;

public class ItemChainsaw : MonoBehaviour
{
	private void Start()
	{
		this.listEnemy = new List<EnemyHit>();
		this.power = (int)((float)PlaneIngameManager.current.currentPlayerController.power * 0.5f);
		this.splineMove.events[this.splineMove.pathContainer.GetWaypointCount() - 1].AddListener(delegate()
		{
			this.splineMove.ResetToStart();
			base.gameObject.SetActive(false);
		});
	}

	private void Update()
	{
	}

	private void TakeDamage()
	{
		for (int i = 0; i < this.listEnemy.Count; i++)
		{
		}
	}

	private void OnEnable()
	{
		this.splineMove.StartMove();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		EnemyHit component = collision.GetComponent<EnemyHit>();
		if (component != null && !this.listEnemy.Contains(component))
		{
			this.listEnemy.Add(component);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (this.listEnemy.Count > 0)
		{
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		EnemyHit component = collision.GetComponent<EnemyHit>();
		if (component != null && this.listEnemy.Contains(component))
		{
			this.listEnemy.Remove(component);
		}
	}

	public List<EnemyHit> listEnemy;

	public splineMove splineMove;

	public float fireRate = 0.1f;

	public int power;
}
