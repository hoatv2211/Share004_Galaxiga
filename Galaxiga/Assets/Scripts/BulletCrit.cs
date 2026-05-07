using System;
using System.Collections;
using UnityEngine;

public class BulletCrit : Bullet
{
	public override void Update()
	{
		if (this.needMove)
		{
			base.transform.Translate(Vector2.up * this.speedMove * Time.deltaTime);
		}
	}

	public override void DespawnBullet()
	{
		this.needMove = false;
		this.fxBullet.SetActive(false);
		if (base.gameObject.activeInHierarchy)
		{
			GameUtil.ObjectPoolDespawn("BulletPlayer", base.gameObject);
		}
		base.DespawnBullet();
	}

	public void SetMove()
	{
		this.fxBullet.SetActive(true);
		base.StopCoroutine("DelaySetMove");
		base.StartCoroutine("DelaySetMove");
	}

	public override void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D(collision);
	}

	private IEnumerator DelaySetMove()
	{
		yield return new WaitForSeconds(0.15f);
		this.needMove = true;
		yield break;
	}

	public bool needMove;

	public GameObject fxBullet;

	public float speedMove = 2f;
}
