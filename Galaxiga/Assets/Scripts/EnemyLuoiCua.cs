using System;
using SkyGameKit;
using UnityEngine;

public class EnemyLuoiCua : MonoBehaviour
{
	private void Start()
	{
		this.transCache = base.transform;
		this.playerTrans = PlaneIngameManager.current.CurrentTransformPlayer;
	}

	private void Update()
	{
		if (this.startFindPlayer)
		{
			this.playerTrans = PlaneIngameManager.current.CurrentTransformPlayer;
			this.FollowTarget(this.playerTrans, 2.5f);
		}
	}

	private void OnEnable()
	{
		this.Delay(1f, delegate
		{
			this.StartFindPlayerAction();
		}, false);
	}

	private void OnDisable()
	{
		this.startFindPlayer = false;
		this.playerInZone = false;
		this.speedAdd = 1f;
	}

	private void StartFindPlayerAction()
	{
		this.startFindPlayer = true;
	}

	private void FollowTarget(Transform target, float speed)
	{
		if (Vector2.Distance(target.position, this.transCache.position) < this.distancePlayer)
		{
			this.speedAdd += 1.2f * Time.deltaTime;
			this.transCache.position = Vector3.MoveTowards(this.transCache.position, target.position, Time.deltaTime * speed * 1f * this.speedAdd);
			this.transCache.rotation = Fu.LookAt2D(base.transform.position - target.position, 0f);
			if (!this.playerInZone)
			{
				this.animEnemy.SetBool("attack", true);
				this.playerInZone = true;
			}
		}
		else
		{
			this.transCache.rotation = Quaternion.Euler(0f, 0f, 0f);
			this.transCache.position -= this.transCache.up * Time.deltaTime * speed;
			if (this.playerInZone)
			{
				this.speedAdd = 1f;
				this.animEnemy.SetBool("attack", false);
				this.playerInZone = false;
			}
		}
	}

	private Transform transCache;

	private Transform playerTrans;

	public Animator animEnemy;

	public float distancePlayer = 5f;

	private bool startFindPlayer;

	private bool playerInZone;

	private float speedAdd = 1f;
}
