using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using SkyGameKit;
using TwoDLaserPack;
using UnityEngine;

public class Laser : MonoBehaviour
{
	public virtual void Awake()
	{
		this.spriteBasedLaser = this.objLaser.GetComponent<SpriteBasedLaser>();
		if (this.isIgnoreCollision)
		{
			this.boxCollider2D = base.GetComponent<BoxCollider2D>();
		}
	}

	public virtual void Start()
	{
		this.spriteBasedLaser.collisionTriggerInterval = this.fireRate;
		this.spriteBasedLaser.OnLaserHitTriggered += this.LaserOnOnLaserHitTriggered;
		this.triggerList = new List<Collider2D>();
		if (this.isIgnoreCollision)
		{
			this.spriteBasedLaser.ignoreCollisions = true;
		}
	}

	public virtual void StartFireLaser()
	{
		if (!this.objLaser.activeInHierarchy)
		{
			this.objLaser.SetActive(true);
		}
		this.spriteBasedLaser.waitingForTriggerTime = false;
		base.StartCoroutine("FireNow");
		if (this.soundLaser == null)
		{
			this.soundIndex = EazySoundManager.PlaySound(AudioCache.Sound.twilight_laser);
			this.soundLaser = EazySoundManager.GetAudio(this.soundIndex);
		}
	}

	public virtual void StopFireLaser()
	{
		base.StopCoroutine("FireNow");
		base.StopCoroutine("DisableFire");
		if (this.objLaser.activeInHierarchy)
		{
			this.objLaser.SetActive(false);
		}
		if (this.soundLaser != null)
		{
			this.soundLaser.Stop();
		}
	}

	public virtual void EnableLaser()
	{
		if (this.soundLaser == null)
		{
			this.soundIndex = EazySoundManager.PlaySound(AudioCache.Sound.twilight_laser);
			this.soundLaser = EazySoundManager.GetAudio(this.soundIndex);
		}
		base.StopCoroutine("FireNow");
		if (!this.objLaser.activeInHierarchy)
		{
			this.objLaser.SetActive(true);
		}
		if (this.isIgnoreCollision)
		{
			this.boxCollider2D.enabled = true;
		}
	}

	public virtual void DisableLaser()
	{
		if (this.soundLaser != null)
		{
			this.soundLaser.Stop();
		}
		base.StopCoroutine("FireNow");
		if (this.objLaser.activeInHierarchy)
		{
			this.objLaser.SetActive(false);
		}
		if (this.isIgnoreCollision)
		{
			this.boxCollider2D.enabled = false;
		}
	}

	private IEnumerator FireNow()
	{
		if (!this.objLaser.activeInHierarchy)
		{
			this.objLaser.SetActive(true);
		}
		if (this.isIgnoreCollision)
		{
			this.boxCollider2D.enabled = true;
		}
		yield return new WaitForSeconds(this.timeLaserActive);
		base.StartCoroutine("DisableFire");
		yield break;
	}

	private IEnumerator DisableFire()
	{
		if (this.objLaser.activeInHierarchy)
		{
			this.objLaser.SetActive(false);
		}
		if (this.isIgnoreCollision)
		{
			this.boxCollider2D.enabled = false;
		}
		yield return new WaitForSeconds(this.timeLaserDeactive);
		base.StartCoroutine("FireNow");
		yield break;
	}

	public virtual void Update()
	{
		if (this.isIgnoreCollision)
		{
			this.TakeDamage();
		}
	}

	public virtual void LaserOnOnLaserHitTriggered(RaycastHit2D hitInfo)
	{
	}

	public virtual void OnTriggerEnter2D(Collider2D c)
	{
	}

	public virtual void OnTriggerExit2D(Collider2D c)
	{
	}

	public virtual void TakeDamage()
	{
	}

	public void SetIgnoreCollision(bool isIgnore)
	{
		this.spriteBasedLaser.ignoreCollisions = isIgnore;
	}

	public void SpawFxBulletTriggerEnemy(Vector3 pos)
	{
		Fu.SpawnExplosion(this.fxTriggerLaser, pos, Quaternion.identity);
	}

	public GameObject objLaser;

	public float power;

	public float fireRate;

	[Header("Time Laser Active")]
	[Range(0.1f, 5f)]
	public float timeLaserActive;

	[Header("Time Laser Deactive")]
	[Range(0.1f, 5f)]
	public float timeLaserDeactive;

	public bool isIgnoreCollision;

	public GameObject fxTriggerLaser;

	private SpriteBasedLaser spriteBasedLaser;

	private BoxCollider2D boxCollider2D;

	public List<Collider2D> triggerList;

	private Audio soundLaser;

	private int soundIndex;
}
