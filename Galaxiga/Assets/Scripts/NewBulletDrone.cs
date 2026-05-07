using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

public class NewBulletDrone : MonoBehaviour
{
	public bool isBullet_nighturge_1()
	{
		return this.typeBullet == NewBulletDrone.TypeBullet.bullet_nighturge_1;
	}

	public bool isBullet_godofthunder_1()
	{
		return this.typeBullet == NewBulletDrone.TypeBullet.bullet_godofthunder_1;
	}

	public bool isSkill_godofthunfer()
	{
		return this.typeBullet == NewBulletDrone.TypeBullet.skill_godofthunfer;
	}

	public bool isSkill_terigon()
	{
		return this.typeBullet == NewBulletDrone.TypeBullet.skill_terigon;
	}

	private void Awake()
	{
		if (this.isSkill_terigon())
		{
			this.sgkBullet = base.GetComponent<NewSgkBullet>();
		}
		else
		{
			this.sgkBullet = base.GetComponent<SgkBullet>();
		}
	}

	private void Start()
	{
		this.sgkBullet.power = this.droneData.CurrentSuperDamage;
		MonoBehaviour.print(string.Concat(new object[]
		{
			"typeBullet: ",
			this.typeBullet,
			" Damge: ",
			this.sgkBullet.power
		}));
		if (this.isSkill_godofthunfer())
		{
			for (int i = 0; i < this.lightningPlayerTakeDamages.Length; i++)
			{
				this.lightningPlayerTakeDamages[i].power = this.sgkBullet.power;
			}
		}
	}

	private void OnEnable()
	{
		if (this.isTimeBomb)
		{
			base.StartCoroutine(this.CountDownTimeBomb());
		}
		if (this.isBullet_nighturge_1())
		{
			base.gameObject.transform.DOScale(new Vector3(3f, 3f, 3f), 3f).SetEase(Ease.OutBack);
		}
	}

	private void Update()
	{
		if (this.typeBullet == NewBulletDrone.TypeBullet.skill_godofthunfer)
		{
			base.gameObject.transform.position = Vector3.Lerp(base.gameObject.transform.position, new Vector3(0f, 3f, 0f), 0.02f);
			if (base.gameObject.transform.position.y >= 12f)
			{
				UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(base.gameObject.GetComponent<UbhBullet>(), false);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!this.isSkill_terigon())
		{
			return;
		}
		if (collision.tag.Equals("EnemyBullet"))
		{
			if (collision.gameObject.GetComponent<SgkBullet>() != null)
			{
				collision.gameObject.GetComponent<SgkBullet>().Explosion();
			}
			else
			{
				UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(collision.gameObject.GetComponent<UbhBullet>(), false);
				Fu.SpawnExplosion(this.fx_Destroy, base.gameObject.transform.position, Quaternion.identity);
			}
		}
	}

	private void DestroyBullet()
	{
		switch (this.typeBullet)
		{
		case NewBulletDrone.TypeBullet.bulletNormal:
			Fu.SpawnExplosion(this.fx_Destroy, base.gameObject.transform.position, Quaternion.identity);
			break;
		case NewBulletDrone.TypeBullet.bullet_nighturge_0:
			UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.bombArea, base.gameObject.transform.position, false);
			Fu.SpawnExplosion(this.fx_Destroy, base.gameObject.transform.position, Quaternion.identity);
			break;
		case NewBulletDrone.TypeBullet.bullet_nighturge_1:
			UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.bombArea, base.gameObject.transform.position, false);
			Fu.SpawnExplosion(this.fx_Destroy, base.gameObject.transform.position, Quaternion.identity);
			break;
		case NewBulletDrone.TypeBullet.skill_godofthunfer:
			Fu.SpawnExplosion(this.fx_Destroy, base.gameObject.transform.position, Quaternion.identity);
			break;
		case NewBulletDrone.TypeBullet.skill_terigon:
			Fu.SpawnExplosion(this.fx_Destroy, base.gameObject.transform.position, Quaternion.identity);
			break;
		}
		UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(base.gameObject.GetComponent<UbhBullet>(), false);
	}

	private IEnumerator CountDownTimeBomb()
	{
		yield return new WaitForSeconds(this.timeDestroy);
		this.DestroyBullet();
		yield break;
	}

	public DroneData droneData;

	private SgkBullet sgkBullet;

	public NewBulletDrone.TypeBullet typeBullet = NewBulletDrone.TypeBullet.bulletNormal;

	public bool isTimeBomb;

	[ShowIf("isTimeBomb", true)]
	public float timeDestroy;

	[SerializeField]
	private GameObject bombArea;

	[SerializeField]
	private GameObject fx_Destroy;

	[ShowIf("isSkill_godofthunfer", true)]
	[SerializeField]
	private LightningPlayerTakeDamage[] lightningPlayerTakeDamages;

	public enum TypeBullet
	{
		auraBomb,
		bulletNormal,
		bullet_nighturge_0,
		bullet_nighturge_1,
		bullet_godofthunder_0,
		bullet_godofthunder_1,
		skill_godofthunfer,
		skill_terigon
	}
}
