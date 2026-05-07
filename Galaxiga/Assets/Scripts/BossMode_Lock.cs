using System;
using SkyGameKit;
using UnityEngine;

public class BossMode_Lock : BaseEnemy
{
	public void SetStat(int _hpLock)
	{
		this.startHP = _hpLock;
		this.Lock();
		this.isStart = true;
	}

	private void Update()
	{
		if (this.isStart)
		{
			if (this.CurrentHP <= 0)
			{
				this.UnLock();
			}
			else
			{
				this.healthBar.transform.localScale = new Vector3((float)this.CurrentHP * 1f / (float)this.startHP, this.healthBar.transform.localScale.y, 0f);
			}
		}
	}

	public void Lock()
	{
		if (!this.isLock)
		{
			this.isLock = true;
			SgkSingleton<LevelManager>.Instance.AliveEnemy.Add(this);
			this.spriteRenderer.sprite = this.listSprite[0];
			this.CurrentHP = this.startHP;
			this.parenthealthBar.SetActive(true);
			this.healthBar.transform.localScale = new Vector3((float)this.CurrentHP * 1f / (float)this.startHP, this.healthBar.transform.localScale.y, 0f);
			this.boxCheckBullet.enabled = true;
		}
	}

	private void UnLock()
	{
		if (this.isLock)
		{
			this.isLock = false;
			SgkSingleton<LevelManager>.Instance.AliveEnemy.Remove(this);
			this.ubhShotCtrl.StartShotRoutine();
			this.bossMode_Protect.TakeDamge();
			this.parenthealthBar.SetActive(false);
			this.spriteRenderer.sprite = this.listSprite[1];
			this.boxCheckBullet.enabled = false;
		}
	}

	private bool isStart;

	private bool isLock;

	[SerializeField]
	private GameObject parenthealthBar;

	[SerializeField]
	private GameObject healthBar;

	[SerializeField]
	private BossMode_Protect bossMode_Protect;

	[SerializeField]
	private CircleCollider2D boxCheckBullet;

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private Sprite[] listSprite;

	[SerializeField]
	private UbhShotCtrl ubhShotCtrl;
}
