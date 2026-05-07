using System;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

public class BulletPlayer : Bullet
{
	private void Awake()
	{
		this.sgkBullet = base.GetComponent<SgkBullet>();
		this.ubhBullet = base.GetComponent<UbhBullet>();
		if (this.needSetSpriteBullet)
		{
			this.SetSpriteBulletFollowRank();
		}
	}

	public override void Start()
	{
		base.Start();
	}

	private void OnEnable()
	{
		if (this.savePowerBullet != GameContext.power)
		{
			this.savePowerBullet = GameContext.power;
			if (this.needSetPower)
			{
				this.SetPower();
			}
		}
	}

	private void SetSpriteBulletFollowRank()
	{
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		if (component != null)
		{
			string currentRank = this.planeData.CurrentRank;
			for (int i = 0; i < this.spriteBullets.Length; i++)
			{
				if (this.spriteBullets[i].rank.ToString() == currentRank)
				{
					component.sprite = this.spriteBullets[i].spriteBullet;
					break;
				}
			}
		}
	}

	public override void SetPower()
	{
		if (this.isBulletSkill)
		{
			this._power = this.planeData.CurrentSuperDamage;
		}
		else if (this.isBulletSubPower)
		{
			this._power = this.planeData.CurrentSubPower;
		}
		else
		{
			this._power = this.planeData.CurrentMainDamage;
		}
		if (this.sgkBullet != null)
		{
			this.sgkBullet.power = this._power;
		}
		this.power = (float)this._power;
	}

	public override void OnBecameInvisible()
	{
		base.OnBecameInvisible();
	}

	public override void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D(collision);
	}

	public override void DespawnBullet()
	{
		base.DespawnBullet();
	}

	public PlaneData planeData;

	[HideIf("isBulletSubPower", true)]
	public bool isBulletSkill;

	[HideIf("isBulletSkill", true)]
	public bool isBulletSubPower;

	public bool needSetSpriteBullet;

	[ShowIf("needSetSpriteBullet", true)]
	public BulletPlayer.SpriteBullet[] spriteBullets;

	private UbhBullet ubhBullet;

	private SgkBullet sgkBullet;

	private int _power;

	private int savePowerBullet;

	[Serializable]
	public class SpriteBullet
	{
		public GameContext.Rank rank;

		public Sprite spriteBullet;
	}
}
