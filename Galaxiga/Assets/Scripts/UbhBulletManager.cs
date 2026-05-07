using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class UbhBulletManager : UbhSingletonMonoBehavior<UbhBulletManager>
{
	public int activeBulletCount
	{
		get
		{
			return this.m_bulletList.Count;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (UbhSingletonMonoBehavior<UbhTimer>.instance == null)
		{
		}
	}

	public void UpdateBullets(float deltaTime)
	{
		for (int i = this.m_bulletList.Count - 1; i >= 0; i--)
		{
			UbhBullet ubhBullet = this.m_bulletList[i];
			if (ubhBullet == null)
			{
				this.m_bulletList.Remove(ubhBullet);
			}
			else
			{
				ubhBullet.UpdateMove(deltaTime);
			}
		}
	}

	public void AddBullet(UbhBullet bullet)
	{
		if (this.m_bulletHashSet.Contains(bullet))
		{
			UnityEngine.Debug.LogWarning("This bullet is already added in m_bulletList.");
			return;
		}
		this.m_bulletList.Add(bullet);
		this.m_bulletHashSet.Add(bullet);
	}

	public void RemoveBullet(UbhBullet bullet)
	{
		if (!this.m_bulletHashSet.Contains(bullet))
		{
			UnityEngine.Debug.LogWarning("This bullet is not found in m_bulletList.");
			return;
		}
		this.m_bulletList.Remove(bullet);
		this.m_bulletHashSet.Remove(bullet);
	}

	private List<UbhBullet> m_bulletList = new List<UbhBullet>(2048);

	private HashSet<UbhBullet> m_bulletHashSet = new HashSet<UbhBullet>();
}
