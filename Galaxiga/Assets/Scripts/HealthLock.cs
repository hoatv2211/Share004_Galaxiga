using System;
using SkyGameKit;
using UnityEngine;

public class HealthLock : BaseEnemy
{
	private void Start()
	{
		this.box = base.GetComponent<CircleCollider2D>();
		this.healthStart = this.healthLock;
		this.startHP = (int)this.healthStart;
	}

	public override void Restart()
	{
		this.CurrentHP = (int)this.healthStart;
		base.Restart();
		this.startHP = this.CurrentHP;
		SgkSingleton<LevelManager>.Instance.AliveEnemy.Add(this);
	}

	public void ResetObj()
	{
		base.gameObject.GetComponent<SpriteRenderer>().sprite = this.imgLock;
		this.box.enabled = true;
		this.Restart();
		this.send = false;
		bool flag = false;
		for (int i = 0; i < SgkSingleton<LevelManager>.Instance.AliveEnemy.Count; i++)
		{
			if (this == SgkSingleton<LevelManager>.Instance.AliveEnemy[i])
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			SgkSingleton<LevelManager>.Instance.AliveEnemy.Add(this);
		}
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		this.send = true;
		base.gameObject.GetComponent<SpriteRenderer>().sprite = this.imgUnLock;
		this.box.enabled = false;
		this.damageShield.TakeHealthShield(1);
		for (int i = 0; i < SgkSingleton<LevelManager>.Instance.AliveEnemy.Count; i++)
		{
			if (this == SgkSingleton<LevelManager>.Instance.AliveEnemy[i])
			{
				SgkSingleton<LevelManager>.Instance.AliveEnemy.Remove(SgkSingleton<LevelManager>.Instance.AliveEnemy[i]);
				break;
			}
		}
	}

	public override int CurrentHP
	{
		get
		{
			return base.CurrentHP;
		}
		set
		{
			base.CurrentHP = value;
			float num = 1f;
			if ((float)this.CurrentHP * 1f / (float)this.startHP < 1f)
			{
				num = (float)this.CurrentHP * 1f / (float)this.startHP;
			}
			if (num < 0f)
			{
				num = 0f;
			}
			this.healthBar.transform.localScale = new Vector3(num, this.healthBar.transform.localScale.y, 0f);
		}
	}

	public float healthLock = 1000f;

	public Sprite imgUnLock;

	public Sprite imgLock;

	private CircleCollider2D box;

	public bool send;

	private float healthStart;

	public TakeDameShield damageShield;

	public GameObject parenthealthBar;

	public GameObject healthBar;

	public GameObject bossParent;
}
