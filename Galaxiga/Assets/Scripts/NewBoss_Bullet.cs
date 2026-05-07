using System;
using System.Collections;
using Hellmade.Sound;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

public class NewBoss_Bullet : MonoBehaviour
{
	private void Start()
	{
		this.trans = base.gameObject.transform;
		this.ubhBullet = base.GetComponent<UbhBullet>();
		this.posBotLeftDestroy = SgkCamera.bottomLeft;
		this.posTopRightDestroy = SgkCamera.topRight;
	}

	private void Update()
	{
		if (!this.isTimeBomb && this.typeBullet != NewBoss_Bullet.TypeBullet.cocoonEnemy && (this.trans.position.x <= this.posBotLeftDestroy.x + this.posMore || this.trans.position.x >= this.posTopRightDestroy.x + this.posMore || this.trans.position.y <= this.posBotLeftDestroy.y + this.posMore || this.trans.position.y >= this.posTopRightDestroy.y - this.posMore))
		{
			this.DestroyBomb();
		}
	}

	private void OnEnable()
	{
		this.isDestroy = false;
		if (this.isTimeBomb)
		{
			base.StartCoroutine(this.CountDownTimeBomb());
		}
		if (this.ubhBomb != null)
		{
			this.ubhBomb.StopShotRoutine();
		}
		if (this.isTakeDamage)
		{
			this.currentHP = this.startHP;
		}
		if (this.isDisableBox)
		{
			this.box.enabled = true;
			base.StartCoroutine(this.CountDownDisableBox());
		}
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "PlayerBullet" && this.isTakeDamage)
		{
			if (this.currentHP > 0)
			{
				SgkBullet component = coll.GetComponent<SgkBullet>();
				component.Explosion();
				this.currentHP--;
			}
			if (this.currentHP == 0)
			{
				this.currentHP--;
				this.isDestroy = true;
				this.DestroyBomb();
			}
		}
	}

	private IEnumerator CountDownTimeBomb()
	{
		yield return new WaitForSeconds(this.timeDestroy);
		this.DestroyBomb();
		yield break;
	}

	private IEnumerator CountDownDisableBox()
	{
		yield return new WaitForSeconds(this.timeDisableBox);
		this.box.enabled = false;
		yield break;
	}

	private void DestroyBomb()
	{
		switch (this.typeBullet)
		{
		case NewBoss_Bullet.TypeBullet.bombUbh:
			this.ubhBomb.StartShotRoutine();
			break;
		case NewBoss_Bullet.TypeBullet.bombBlachHole:
			Fu.SpawnExplosion(this.blackHole, this.trans.position, Quaternion.identity);
			break;
		case NewBoss_Bullet.TypeBullet.bombUbhxBlackHole:
			this.ubhBomb.StartShotRoutine();
			Fu.SpawnExplosion(this.fx_Destroy, this.trans.position, Quaternion.identity);
			Fu.SpawnExplosion(this.blackHole, this.trans.position, Quaternion.identity);
			CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
			break;
		case NewBoss_Bullet.TypeBullet.cocoonEnemy:
			base.StartCoroutine(this.CountDownSpawnEnemy());
			break;
		}
		if (this.typeBullet != NewBoss_Bullet.TypeBullet.cocoonEnemy)
		{
			if (this.soundDestroy != null)
			{
				EazySoundManager.PlaySound(this.soundDestroy, 5f);
			}
			if (this.fx_Destroy != null)
			{
				Fu.SpawnExplosion(this.fx_Destroy, this.trans.position, Quaternion.identity);
			}
			UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this.ubhBullet, false);
		}
	}

	private IEnumerator CountDownSpawnEnemy()
	{
		if (!this.isDestroy)
		{
			yield return new WaitForSeconds(2f);
			if (this.fx_Destroy != null)
			{
				Fu.SpawnExplosion(this.fx_Destroy, this.trans.position, Quaternion.identity);
				Fu.SpawnExplosion(this.fx_Destroy, new Vector3(this.trans.position.x + 1f, this.trans.position.y + 1f, 0f), Quaternion.identity);
				Fu.SpawnExplosion(this.fx_Destroy, new Vector3(this.trans.position.x - 1f, this.trans.position.y, 0f), Quaternion.identity);
				Fu.SpawnExplosion(this.blackHole, this.trans.position, Quaternion.identity);
				Fu.SpawnExplosion(this.blackHole, new Vector3(this.trans.position.x + 1f, this.trans.position.y + 1f, 0f), Quaternion.identity);
				Fu.SpawnExplosion(this.blackHole, new Vector3(this.trans.position.x - 1f, this.trans.position.y, 0f), Quaternion.identity);
			}
		}
		else
		{
			Fu.SpawnExplosion(this.fx_Destroy, this.trans.position, Quaternion.identity);
		}
		yield return null;
		if (this.soundDestroy != null)
		{
			EazySoundManager.PlaySound(this.soundDestroy, 5f);
		}
		UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this.ubhBullet, false);
		yield break;
	}

	private UbhBullet ubhBullet;

	private Transform trans;

	public NewBoss_Bullet.TypeBullet typeBullet;

	public bool isTimeBomb;

	[ShowIf("isTimeBomb", true)]
	public float timeDestroy;

	public bool isTakeDamage;

	[ShowIf("isTakeDamage", true)]
	public int startHP;

	private int currentHP;

	private bool isDestroy;

	public bool isDisableBox;

	[ShowIf("isDisableBox", true)]
	public float timeDisableBox;

	private float posMore = 0.5f;

	private Vector2 posBotLeftDestroy;

	private Vector2 posTopRightDestroy;

	[SerializeField]
	private GameObject fx_Destroy;

	[Header("BOMB UBH")]
	[SerializeField]
	private UbhShotCtrl ubhBomb;

	[Header("BOMB BLACKHOLE")]
	[SerializeField]
	private GameObject blackHole;

	[SerializeField]
	private AudioClip soundDestroy;

	[SerializeField]
	private BoxCollider2D box;

	public enum TypeBullet
	{
		bombNormal,
		bombUbh,
		bombBlachHole,
		bombUbhxBlackHole,
		cocoonEnemy
	}
}
