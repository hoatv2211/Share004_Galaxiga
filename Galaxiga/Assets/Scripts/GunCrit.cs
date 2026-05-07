using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GunCrit : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (Time.time > this.nextTime)
		{
			this.nextTime = Time.time + this.fireRate;
			this.SpawBullet();
		}
	}

	private void SpawBullet()
	{
		this.bulletCrits = new List<BulletCrit>();
		for (int i = 0; i < this.arrPosToMove.Length; i++)
		{
			GameObject gameObject = GameUtil.ObjectPoolSpawnGameObject("BulletPlayer", "Bullet Crit", base.transform.position, Quaternion.identity);
			BulletCrit component = gameObject.GetComponent<BulletCrit>();
			this.bulletCrits.Add(component);
			this.MoveBullet(gameObject, this.arrPosToMove[i].position);
		}
	}

	private void MoveBullet(GameObject obj, Vector2 posToMove)
	{
		TweenParams tweenParams = new TweenParams().SetEase(Ease.InOutSine, null, null).OnComplete(new TweenCallback(this.MoveComplete));
		obj.transform.DOMove(posToMove, 0.4f, false).SetAs(tweenParams);
	}

	private void MoveComplete()
	{
		for (int i = 0; i < this.bulletCrits.Count; i++)
		{
			this.bulletCrits[i].SetMove();
		}
	}

	public Transform[] arrPosToMove;

	private List<BulletCrit> bulletCrits;

	private float fireRate = 2f;

	private float nextTime;
}
