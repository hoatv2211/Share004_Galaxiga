using System;
using System.Collections;
using SkyGameKit;
using UnityEngine;

public class BulletEnemyTakeDamge : MonoBehaviour
{
	private void Start()
	{
		this.ubhBullet = base.GetComponent<UbhBullet>();
	}

	private void OnEnable()
	{
		this.isDestroy = true;
		this.currentHP = this.startHP;
		if (this.isBoom)
		{
			this.boxExplosive.enabled = false;
			if (this.ubhBigBoom != null)
			{
				this.ubhBigBoom.StopShotRoutine();
			}
			base.StartCoroutine(this.WaitExplosiveBoom());
		}
		else if (this.isBigBoom)
		{
			this.ubhBigBoom.StopShotRoutine();
			base.StartCoroutine(this.WaitExplosiveBigBoom());
		}
		else
		{
			base.StartCoroutine(this.WaitDestroy());
		}
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "PlayerBullet")
		{
			if (this.currentHP > 0)
			{
				SgkBullet component = coll.GetComponent<SgkBullet>();
				component.Explosion();
				this.currentHP--;
			}
			if (this.currentHP == 0)
			{
				CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
				this.isDestroy = false;
				Fu.SpawnExplosion(this.effectDestroy, base.gameObject.transform.position, Quaternion.identity);
				UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this.ubhBullet, false);
			}
		}
	}

	private IEnumerator WaitDestroy()
	{
		yield return new WaitForSeconds(this.timeExplosive);
		Fu.SpawnExplosion(this.effectDestroy, base.gameObject.transform.position, Quaternion.identity);
		if (this.isDestroy)
		{
			UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this.ubhBullet, false);
		}
		yield break;
	}

	private IEnumerator WaitExplosiveBoom()
	{
		yield return new WaitForSeconds(this.timeExplosive);
		CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
		this.boxExplosive.enabled = true;
		Fu.SpawnExplosion(this.effectDestroy, base.gameObject.transform.position, Quaternion.identity);
		this.ubhBigBoom.StartShotRoutine();
		yield return new WaitForSeconds(0.05f);
		if (this.isDestroy)
		{
			UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this.ubhBullet, false);
		}
		yield break;
	}

	private IEnumerator WaitExplosiveBigBoom()
	{
		yield return new WaitForSeconds(this.timeExplosive);
		this.ubhBigBoom.StartShotRoutine();
		CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
		Fu.SpawnExplosion(this.effectDestroy, base.gameObject.transform.position, Quaternion.identity);
		Fu.SpawnExplosion(this.Aura, base.gameObject.transform.position, Quaternion.identity);
		if (this.isDestroy)
		{
			UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this.ubhBullet, false);
		}
		yield break;
	}

	private UbhBullet ubhBullet;

	private bool isDestroy;

	public int startHP;

	public int currentHP;

	public float timeExplosive = 2.5f;

	[SerializeField]
	private bool isBoom;

	[SerializeField]
	private GameObject effectDestroy;

	[SerializeField]
	private CircleCollider2D boxExplosive;

	[SerializeField]
	private UbhShotCtrl ubhBigBoom;

	[SerializeField]
	private bool isBigBoom;

	[SerializeField]
	private GameObject Aura;
}
